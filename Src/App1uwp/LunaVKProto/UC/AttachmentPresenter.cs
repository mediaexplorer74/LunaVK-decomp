// Decompiled with JetBrains decompiler
// Type: App1uwp.UC.AttachmentPresenter
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
using App1uwp.Library;
using App1uwp.Network.DataObjects;
using App1uwp.Network.Enums;
using App1uwp.UC.Attachment;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

#nullable disable
namespace App1uwp.UC
{
  public class AttachmentPresenter : StackPanel
  {
    public AttachmentPresenter(List<VKVideoBase> videos, double screen_width = 0.0)
    {
    }

    public AttachmentPresenter(List<VKAttachment> attachments, double screen_width = 0.0)
    {
      ((ICollection<UIElement>) ((Panel) this).Children).Clear();
      if (attachments == null)
        return;
      List<VKPhoto> list = new List<VKPhoto>();
      foreach (VKAttachment attachment in attachments)
      {
        if (attachment.type != VKAttachmentType.Photo && list.Count > 0)
        {
          AttachPhotosUC attachPhotosUc = new AttachPhotosUC(list, screen_width);
          list.Clear();
          ((ICollection<UIElement>) ((Panel) this).Children).Add((UIElement) attachPhotosUc);
        }
        switch (attachment.type)
        {
          case VKAttachmentType.Photo:
            list.Add(attachment.photo);
            continue;
          case VKAttachmentType.Video:
            ((ICollection<UIElement>) ((Panel) this).Children).Add((UIElement) new AttachVideoUC(attachment.video));
            continue;
          case VKAttachmentType.Audio:
            ((ICollection<UIElement>) ((Panel) this).Children).Add((UIElement) new AttachAudioUC(attachment.audio));
            continue;
          case VKAttachmentType.Doc:
            ((ICollection<UIElement>) ((Panel) this).Children).Add((UIElement) new AttachDocumentUC(attachment.doc));
            continue;
          case VKAttachmentType.Link:
            ((ICollection<UIElement>) ((Panel) this).Children).Add((UIElement) new AttachLinkUC(attachment.link));
            continue;
          case VKAttachmentType.Wall:
            ((ICollection<UIElement>) ((Panel) this).Children).Add((UIElement) new ForwardedMessagesUC(attachment.wall, screen_width));
            continue;
          case VKAttachmentType.Sticker:
            VKSticker sticker = attachment.sticker;
            Image img = new Image();
            ImageExtensions.ImageFromCache3(sticker.photo_256, (Action<string>) (s => img.put_Source((ImageSource) new BitmapImage(new Uri(s)))));
            Image image1 = img;
            double num1;
            ((FrameworkElement) img).put_Height(num1 = (double) (180 * Settings.Instance.UIScale / 100));
            double num2 = num1;
            ((FrameworkElement) image1).put_Width(num2);
            ((ICollection<UIElement>) ((Panel) this).Children).Add((UIElement) img);
            continue;
          case VKAttachmentType.Gift:
            ((ICollection<UIElement>) ((Panel) this).Children).Add((UIElement) new AttachGiftUC(attachment.gift));
            continue;
          case VKAttachmentType.Poll:
            ((ICollection<UIElement>) ((Panel) this).Children).Add((UIElement) new AttachPollUC(attachment.poll));
            continue;
          case VKAttachmentType.Graffiti:
            VKGraffiti graffiti = attachment.graffiti;
            Image image2 = new Image();
            image2.put_Source((ImageSource) new BitmapImage(new Uri(graffiti.photo_200)));
            ((FrameworkElement) image2).put_Width(200.0);
            ((FrameworkElement) image2).put_Height(200.0);
            ((ICollection<UIElement>) ((Panel) this).Children).Add((UIElement) image2);
            continue;
          default:
            continue;
        }
      }
      if (list.Count <= 0)
        return;
      ((ICollection<UIElement>) ((Panel) this).Children).Add((UIElement) new AttachPhotosUC(list, screen_width));
    }

    private void Asy(List<IOutboundAttachment> attachments, double width, bool aligment_to_right)
    {
      if (attachments == null)
        return;
      List<OutboundPhotoAttachment> list = new List<OutboundPhotoAttachment>();
      foreach (IOutboundAttachment attachment in attachments)
      {
        if (!(attachment is OutboundPhotoAttachment) && list.Count > 0)
        {
          AttachPhotosUC attachPhotosUc = new AttachPhotosUC(list, width, aligment_to_right);
          list.Clear();
          ((ICollection<UIElement>) ((Panel) this).Children).Add((UIElement) attachPhotosUc);
        }
        if (attachment is OutboundPhotoAttachment)
          list.Add(attachment as OutboundPhotoAttachment);
      }
      if (list.Count <= 0)
        return;
      ((ICollection<UIElement>) ((Panel) this).Children).Add((UIElement) new AttachPhotosUC(list, width, aligment_to_right));
    }

    public AttachmentPresenter(
      List<IOutboundAttachment> attachments,
      double width,
      bool aligment_to_right)
    {
      ((ICollection<UIElement>) ((Panel) this).Children).Clear();
      this.Asy(attachments, width, aligment_to_right);
    }
  }
}
