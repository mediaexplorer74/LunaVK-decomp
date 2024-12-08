// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.DataObjects.StoreProduct
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.json;
using Newtonsoft.Json;

#nullable disable
namespace App1uwp.Network.DataObjects
{
  public class StoreProduct
  {
    public int id { get; set; }

    public string type { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool purchased { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool active { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool promoted { get; set; }

    public int purchase_date { get; set; }

    public string title { get; set; }

    public string base_url { get; set; }

    public StoreStickers stickers { get; set; }
  }
}
