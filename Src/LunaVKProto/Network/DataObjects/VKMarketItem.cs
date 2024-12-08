// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.DataObjects.VKMarketItem
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.json;
using Newtonsoft.Json;
using System;

#nullable disable
namespace App1uwp.Network.DataObjects
{
  public class VKMarketItem
  {
    public int id { get; set; }

    public int owner_id { get; set; }

    public string title { get; set; }

    public string description { get; set; }

    public VKMarketItem.MarketItemPrice price { get; set; }

    public VKMarketItem.MarketItemCategory category { get; set; }

    public string thumb_photo { get; set; }

    [JsonConverter(typeof (UnixtimeToDateTimeConverter))]
    public DateTime date { get; set; }

    public int availability { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool can_comment { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool can_repost { get; set; }

    public class MarketItemPrice
    {
      public int amount { get; set; }

      public VKMarketItem.MarketItemPrice.Currency currency { get; set; }

      public string text { get; set; }

      public class Currency
      {
        public int id { get; set; }

        public string name { get; set; }
      }
    }

    public class MarketItemCategory
    {
      public int id { get; set; }

      public string name { get; set; }

      public VKMarketItem.MarketItemCategory.Section section { get; set; }

      public class Section
      {
        public int id { get; set; }

        public string name { get; set; }
      }
    }
  }
}
