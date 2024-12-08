// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.DataObjects.VKPostSource
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.Enums;
using Newtonsoft.Json;

#nullable disable
namespace App1uwp.Network.DataObjects
{
  public sealed class VKPostSource
  {
    private const string _vk = "vk";
    private const string _widget = "widget";
    private const string _api = "api";
    private const string _rss = "rss";
    private const string _sms = "sms";
    private const string _android = "android";
    private const string _ios = "ios";
    private const string _wphone = "wphone";

    [JsonProperty("type")]
    private string _type { get; set; }

    [JsonProperty("platform")]
    private string _platform { get; set; }

    public VKPostSourceType Type
    {
      get
      {
        switch (this._type)
        {
          case "widget":
            return VKPostSourceType.Widget;
          case "api":
            return VKPostSourceType.API;
          case "rss":
            return VKPostSourceType.RSS;
          case "sms":
            return VKPostSourceType.SMS;
          default:
            return VKPostSourceType.VK;
        }
      }
      set
      {
        switch (value)
        {
          case VKPostSourceType.VK:
            this._type = "vk";
            break;
          case VKPostSourceType.Widget:
            this._type = "widget";
            break;
          case VKPostSourceType.API:
            this._type = "api";
            break;
          case VKPostSourceType.RSS:
            this._type = "rss";
            break;
          case VKPostSourceType.SMS:
            this._type = "sms";
            break;
        }
      }
    }

    public VKPostSourcePlatform Platform
    {
      get
      {
        switch (this._platform)
        {
          case "android":
            return VKPostSourcePlatform.Android;
          case "ios":
            return VKPostSourcePlatform.iOS;
          case "wphone":
            return VKPostSourcePlatform.WinPhone;
          default:
            return VKPostSourcePlatform.NotSpecified;
        }
      }
      set
      {
        switch (value)
        {
          case VKPostSourcePlatform.NotSpecified:
            this._platform = "";
            break;
          case VKPostSourcePlatform.Android:
            this._platform = "android";
            break;
          case VKPostSourcePlatform.iOS:
            this._platform = "ios";
            break;
          case VKPostSourcePlatform.WinPhone:
            this._platform = "wphone";
            break;
        }
      }
    }

    [JsonProperty("url")]
    public string Url { get; set; }
  }
}
