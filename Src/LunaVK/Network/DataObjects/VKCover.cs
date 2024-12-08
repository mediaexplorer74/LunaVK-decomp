// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.DataObjects.VKCover
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Graphics.Display;
using Windows.UI.Xaml;

#nullable disable
namespace App1uwp.Network.DataObjects
{
  public class VKCover
  {
    public List<VKCover.CoverImage> images;

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool enabled { get; set; }

    public string CurrentImage
    {
      get
      {
        if (this.images == null || this.images.Count == 0)
          return (string) null;
        double num = Window.Current.Bounds.Width * (double) DisplayProperties.LogicalDpi;
        foreach (VKCover.CoverImage coverImage in (IEnumerable<VKCover.CoverImage>) this.images.OrderBy<VKCover.CoverImage, int>((Func<VKCover.CoverImage, int>) (i => i.width)))
        {
          if ((double) coverImage.width >= num)
            return coverImage.url;
        }
        return this.images.LastOrDefault<VKCover.CoverImage>().url;
      }
    }

    public class CoverImage
    {
      public string url { get; set; }

      public int width { get; set; }

      public int height { get; set; }
    }
  }
}
