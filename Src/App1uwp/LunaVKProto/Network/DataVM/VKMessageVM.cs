// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.DataVM.VKMessageVM
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
using App1uwp.Library;
using App1uwp.Network.DataObjects;
using App1uwp.Network.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

#nullable disable
namespace App1uwp.Network.DataVM
{
  public class VKMessageVM : VKMessage, INotifyPropertyChanged
  {
    private bool _isSelected;

    public event PropertyChangedEventHandler PropertyChanged;

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }

    public bool IsSelected
    {
      get => this._isSelected;
      set
      {
        this._isSelected = value;
        this.NotifyPropertyChanged("BGBrush");
      }
    }

    public SolidColorBrush BGBrush
    {
      get
      {
        if (this.IsSelected)
          return (SolidColorBrush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "AccentBrushHigh"];
        if (this.action != VKChatMessageActionType.None)
          return new SolidColorBrush(Colors.Transparent);
        if (this.attachments != null)
        {
          Func<VKAttachment, bool> func = (Func<VKAttachment, bool>) (a =>
          {
            if (a.type == VKAttachmentType.Sticker)
              return true;
            return a.type == VKAttachmentType.Doc && (a.doc?.IsGraffiti ?? false);
          });
          if (this.attachments.Any<VKAttachment>(VKMessageVM.CS\u0024\u003C\u003E9__CachedAnonymousMethodDelegate2))
            return new SolidColorBrush(Colors.Transparent);
          if (this.attachments.Any<VKAttachment>((Func<VKAttachment, bool>) (a => a.type == VKAttachmentType.Gift)))
            return (SolidColorBrush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "PhoneDialogGiftMessageBackgroundBrush"];
        }
        return this.@out == VKMessageType.Sent ? (SolidColorBrush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "AccentBrushMediumLow"] : (SolidColorBrush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "AccentBrushLow"];
      }
    }

    public SolidColorBrush MsgStateBrush
    {
      get
      {
        return this.OutboundMessageVM != null && this.OutboundMessageVM.OutboundMessageStatus == OutboundMessageStatus.Failed ? new SolidColorBrush(Colors.Red) : (SolidColorBrush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "TextColorContent"];
      }
    }

    public VKBaseDataForGroupOrUser User { get; set; }

    public string UserThumb
    {
      get
      {
        if (this.chat_id == 0)
          return (string) null;
        if (this.User == null)
          this.User = UsersService.Instance.GetCachedUser((long) this.user_id);
        return this.User.photo_50;
      }
    }

    public Visibility UserThumbVisibility
    {
      get
      {
        return this.chat_id <= 0 || this.@out != VKMessageType.Received ? (Visibility) 1 : (Visibility) 0;
      }
    }

    public void RefreshUIProperties()
    {
      Execute.ExecuteOnUIThread((Action) (() =>
      {
        this.NotifyPropertyChanged("MsgState");
        this.NotifyPropertyChanged("MsgStateBrush");
        this.NotifyPropertyChanged("important");
      }));
    }

    public string MsgState
    {
      get
      {
        if (this.@out == VKMessageType.Received || this.action != VKChatMessageActionType.None)
          return "";
        if (this.read_state)
          return "\uE18E";
        if (this.OutboundMessageVM != null && this.OutboundMessageVM.OutboundMessageStatus != OutboundMessageStatus.Delivered)
        {
          if (this.OutboundMessageVM.OutboundMessageStatus == OutboundMessageStatus.Failed)
            return "\uE783";
          if (this.OutboundMessageVM.OutboundMessageStatus == OutboundMessageStatus.SendingNow)
            return "\uED5A";
        }
        return "\uE73E";
      }
    }

    public OutboundMessageViewModel OutboundMessageVM { get; set; }

    public void Send()
    {
      this.OutboundMessageVM.MessageSent += new EventHandler<int>(this.OutboundMessageVM_MessageSent);
      this.OutboundMessageVM.UploadFinished += new EventHandler(this.OutboundMessageVM_UploadFinished);
      this.OutboundMessageVM.Send();
    }

    private void OutboundMessageVM_UploadFinished(object sender, EventArgs e)
    {
      this.OutboundMessageVM.IsUploading = false;
      this.RefreshUIProperties();
    }

    private void OutboundMessageVM_MessageSent(object sender, int e)
    {
      this.id = e;
      this.RefreshUIProperties();
      this.OutboundMessageVM.MessageSent -= new EventHandler<int>(this.OutboundMessageVM_MessageSent);
    }

    public Visibility EditedVisibility
    {
      get => this.update_time == DateTime.MinValue ? (Visibility) 1 : (Visibility) 0;
    }
  }
}
