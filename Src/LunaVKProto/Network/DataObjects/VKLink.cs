// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.DataObjects.VKLink
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.json;
using Newtonsoft.Json;
using Windows.UI.Xaml;

#nullable disable
namespace App1uwp.Network.DataObjects
{
  public class VKLink
  {
    public string url { get; set; }

    public string title { get; set; }

    public string caption { get; set; }

    public string description { get; set; }

    public VKPhoto photo { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool is_external { get; set; }

    public string preview_page { get; set; }

    public string preview_url { get; set; }

    public string ImageSrc
    {
      get
      {
        if (this.photo == null)
          return "";
        if (!string.IsNullOrEmpty(this.photo.photo_130))
          return this.photo.photo_130;
        return !string.IsNullOrEmpty(this.photo.photo_75) ? this.photo.photo_75 : "";
      }
    }

    public string CaptionUI
    {
      get
      {
        if (!string.IsNullOrEmpty(this.caption))
          return this.caption;
        string captionUi = this.url.Substring(this.url.IndexOf("//") + 2);
        int length = captionUi.IndexOf("/");
        if (length > 0)
          captionUi = captionUi.Substring(0, length);
        return captionUi;
      }
    }

    public bool HasImage => !string.IsNullOrWhiteSpace(this.ImageSrc);

    public Visibility IconLinkVisibility => !this.HasImage ? (Visibility) 0 : (Visibility) 1;
  }
}
