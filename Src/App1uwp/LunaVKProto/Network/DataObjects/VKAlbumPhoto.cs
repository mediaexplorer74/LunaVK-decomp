// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.DataObjects.VKAlbumPhoto
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using System;
using System.Collections.Generic;
using Windows.Graphics.Display;
using Windows.UI.Xaml;

#nullable disable
namespace App1uwp.Network.DataObjects
{
  public class VKAlbumPhoto
  {
    public int id { get; set; }

    public int thumb_id { get; set; }

    public int owner_id { get; set; }

    public string title { get; set; }

    public string description { get; set; }

    public int created { get; set; }

    public int updated { get; set; }

    public int size { get; set; }

    public int can_upload { get; set; }

    public List<string> privacy_view { get; set; }

    public List<string> privacy_comment { get; set; }

    public string thumb_src { get; set; }

    public List<DocPreviewPhotoSize> sizes { get; set; }

    public string Optimalthumb
    {
      get
      {
        double width = Window.Current.Bounds.Width;
        double logicalDpi1 = (double) DisplayProperties.LogicalDpi;
        double height = Window.Current.Bounds.Height;
        double logicalDpi2 = (double) DisplayProperties.LogicalDpi;
        DocPreviewPhotoSize previewPhotoSize = this.sizes.Find((Predicate<DocPreviewPhotoSize>) (element => element.type == "z" || element.type == "y"));
        return previewPhotoSize == null ? this.sizes[0].src : previewPhotoSize.src;
      }
    }
  }
}
