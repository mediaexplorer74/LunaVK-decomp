// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.DataObjects.VKProfileinfoGetObject
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using Newtonsoft.Json;

#nullable disable
namespace App1uwp.Network.DataObjects
{
  public class VKProfileinfoGetObject
  {
    [JsonProperty("first_name")]
    public string FirstName { get; set; }

    [JsonProperty("last_name")]
    public string LastName { get; set; }

    [JsonProperty("maiden_name")]
    public string MaidenName { get; set; }

    [JsonProperty("screen_name")]
    public string ScreenName { get; set; }

    [JsonProperty("sex")]
    public int Sex { get; set; }

    [JsonProperty("relation")]
    public int Relation { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }
  }
}
