// Decompiled with JetBrains decompiler
// Type: App1uwp.Library.OutboundPhotoAttachment
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network;
using App1uwp.Network.DataObjects;
using System;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

#nullable disable
namespace App1uwp.Library
{
  public class OutboundPhotoAttachment : IOutboundAttachment
  {
    private VKPhoto server_photo;
    public ulong MediaId;

    public StorageFile sf { get; set; }

    public double Width => 133.0;

    public double Height => 100.0;

    public bool IsUploadAttachment => true;

    public BitmapImage LocalUrl2 { get; set; }

    public double ImgWidth { get; set; }

    public double ImgHeight { get; set; }

    public override string ToString()
    {
      return string.Format("{0}{1}_{2}", (object) "photo", (object) Settings.Instance.auth.UserId, (object) this.MediaId);
    }

    public void Upload(Action completionCallback, Action<double> progressCallback = null)
    {
      DocumentsService.Instance.ReadFully(this.sf, (Action<byte[]>) (bytes => this.UploadImpl(bytes, (Action<VKPhoto, ResultCode>) ((photo, rescode) =>
      {
        this.server_photo = photo;
        completionCallback();
      }), progressCallback)));
    }

    private void UploadImpl(
      byte[] bytes,
      Action<VKPhoto, ResultCode> completionCallback,
      Action<double> progressCallback)
    {
      DocumentsService.Instance.UploadPhoto(bytes, completionCallback, progressCallback);
    }
  }
}
