// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.DataObjects.VKGroupMarket
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.json;
using Newtonsoft.Json;

#nullable disable
namespace App1uwp.Network.DataObjects
{
  public class VKGroupMarket
  {
    [JsonConverter(typeof (VKBooleanConverter))]
    public bool enabled { get; set; }

    public int price_min { get; set; }

    public int price_max { get; set; }

    public int main_album_id { get; set; }

    public int contact_id { get; set; }

    public VKGroupMarket.VKCurrency currency { get; set; }

    public string currency_text { get; set; }

    public class VKCurrency
    {
      public int id { get; set; }

      public string name { get; set; }
    }
  }
}
