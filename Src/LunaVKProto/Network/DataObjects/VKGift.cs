// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.DataObjects.VKGift
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using Newtonsoft.Json;

#nullable disable
namespace App1uwp.Network.DataObjects
{
  public class VKGift
  {
    public long stickers_product_id { get; set; }

    [JsonProperty("id")]
    public long ID { get; set; }

    [JsonProperty("thumb_256")]
    public string Thumb256 { get; set; }

    [JsonProperty("thumb_96")]
    public string Thumb96 { get; set; }

    [JsonProperty("thumb_48")]
    public string Thumb48 { get; set; }
  }
}
