// Decompiled with JetBrains decompiler
// Type: App1uwp.Library.OutboundMessageViewModel
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network;
using App1uwp.Network.DataObjects;
using App1uwp.Network.Enums;
using App1uwp.Network.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Storage;
using Windows.UI.Xaml;

#nullable disable
namespace App1uwp.Library
{
  public class OutboundMessageViewModel : ViewModelBase
  {
    public List<IOutboundAttachment> Attachments;
    private double _uploadProgress;
    private bool _isUploading;
    public int StickerItem;
    public long UserId;
    public long ChatId;
    public string MessageText = string.Empty;
    private OutboundMessageStatus _outboundMessageStatus;
    private Guid _uploadJobId = Guid.Empty;

    public double UploadProgress
    {
      get => this._uploadProgress;
      set
      {
        this._uploadProgress = value;
        this.NotifyPropertyChanged(nameof (UploadProgress));
      }
    }

    public bool IsUploading
    {
      get => this._isUploading;
      set
      {
        this._isUploading = value;
        this.NotifyPropertyChanged("IsUploadingVisibility");
      }
    }

    public Visibility IsUploadingVisibility
    {
      get
      {
        return this.IsUploading && this.CountUploadableAttachments > 0 ? (Visibility) 0 : (Visibility) 1;
      }
    }

    public event EventHandler<int> MessageSent;

    public event EventHandler UploadFinished;

    public OutboundMessageViewModel(long userId, long chatId)
    {
      this.UserId = userId;
      this.ChatId = chatId;
    }

    internal void RemoveAttachment(IOutboundAttachment outboundAttCont)
    {
      this.Attachments.Remove(outboundAttCont);
    }

    public event EventHandler<OutboundMessageStatus> OutboundMessageStatusChanged;

    public OutboundMessageStatus OutboundMessageStatus
    {
      get => this._outboundMessageStatus;
      set
      {
        if (this._outboundMessageStatus == value)
          return;
        this._outboundMessageStatus = value;
        if (this.OutboundMessageStatusChanged == null)
          return;
        this.OutboundMessageStatusChanged((object) this, value);
      }
    }

    public void Send()
    {
      this.IsUploading = true;
      this._uploadJobId = Guid.NewGuid();
      this.UploadProgress = 0.0;
      this.StartSendingByAttachmentInd(0, this._uploadJobId);
    }

    private void StartSendingByAttachmentInd(int attachmentInd, Guid jobId)
    {
      if (jobId != this._uploadJobId)
        return;
      if (this.Attachments == null || attachmentInd >= this.Attachments.Count)
      {
        if (this.UploadFinished != null)
          this.UploadFinished((object) this, EventArgs.Empty);
        this.DoSend();
      }
      else
      {
        IOutboundAttachment attachment = this.Attachments[attachmentInd];
        if (attachment.IsUploadAttachment)
        {
          double previousProgress = 0.0;
          attachment.Upload((Action) (() =>
          {
            if (jobId != this._uploadJobId)
              return;
            this.StartSendingByAttachmentInd(attachmentInd + 1, jobId);
          }), (Action<double>) (progress =>
          {
            this.UploadProgressHandler(progress - previousProgress);
            previousProgress = progress;
          }));
        }
        else
          this.StartSendingByAttachmentInd(attachmentInd + 1, jobId);
      }
    }

    private async void DoSend()
    {
      List<int> ForwardedMessagesIds = (List<int>) null;
      List<IOutboundAttachment> temp_a = (List<IOutboundAttachment>) null;
      if (this.Attachments != null && this.Attachments.Count > 0)
      {
        IOutboundAttachment forwardedMessages = this.Attachments.FirstOrDefault<IOutboundAttachment>((Func<IOutboundAttachment, bool>) (a => a is OutboundForwardedMessages));
        if (forwardedMessages != null)
          ForwardedMessagesIds = new List<int>((forwardedMessages as OutboundForwardedMessages).Messages.Select<VKMessage, int>((Func<VKMessage, int>) (m => m.id)));
        temp_a = new List<IOutboundAttachment>();
        for (int i = 0; i < this.Attachments.Count && i < 10; ++i)
        {
          IOutboundAttachment oa = this.Attachments[i];
          switch (oa)
          {
            case OutboundPhotoAttachment _:
              StorageFile f = (oa as OutboundPhotoAttachment).sf;
              UploadPhotoResponseData ret = await DocumentsService.Instance.UploadPhotoToDialog(f);
              if (ret == null)
                return;
              VKPhoto p = await DocumentsService.Instance.SavePhoto(ret);
              if (p == null)
                return;
              (oa as OutboundPhotoAttachment).MediaId = p.id;
              temp_a.Add(oa);
              break;
            case OutboundWallPostAttachment _:
              temp_a.Add(oa);
              break;
          }
        }
      }
      Dictionary<string, string> parameters = new Dictionary<string, string>();
      if (!string.IsNullOrWhiteSpace(this.MessageText))
        parameters["message"] = this.MessageText;
      parameters["peer_id"] = this.GetPeerId().ToString();
      if (temp_a != null && temp_a.Count > 0)
        parameters["attachment"] = string.Join<IOutboundAttachment>(",", (IEnumerable<IOutboundAttachment>) temp_a);
      if (ForwardedMessagesIds != null && ForwardedMessagesIds.Count > 0)
        parameters["forward_messages"] = string.Join<int>(",", (IEnumerable<int>) ForwardedMessagesIds);
      if (this.StickerItem != 0)
        parameters["sticker_id"] = this.StickerItem.ToString();
      VKResponse<int> temp = await RequestsDispatcher.GetResponse<int>("messages.send", parameters);
      if (temp == null || temp.response == 0)
      {
        this.OutboundMessageStatus = OutboundMessageStatus.Failed;
        if (this.MessageSent != null)
          this.MessageSent((object) this, 0);
      }
      else
      {
        this.OutboundMessageStatus = OutboundMessageStatus.Delivered;
        if (this.MessageSent != null)
          this.MessageSent((object) this, temp.response);
      }
      this.IsUploading = false;
    }

    private long GetPeerId() => this.ChatId > 0L ? this.ChatId + 2000000000L : this.UserId;

    private void UploadProgressHandler(double deltaProgress)
    {
      if (this.CountUploadableAttachments <= 0)
        return;
      this.UploadProgress += deltaProgress / (double) this.CountUploadableAttachments;
    }

    public int CountUploadableAttachments
    {
      get
      {
        return this.Attachments.Count<IOutboundAttachment>((Func<IOutboundAttachment, bool>) (a => a.IsUploadAttachment));
      }
    }
  }
}
