// Decompiled with JetBrains decompiler
// Type: App1uwp.Emoji.StickersSettings
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network;
using App1uwp.Network.DataObjects;
using App1uwp.Network.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable
namespace App1uwp.Emoji
{
  public class StickersSettings
  {
    private static StickersSettings _instance;

    public static StickersSettings Instance
    {
      get => StickersSettings._instance ?? (StickersSettings._instance = new StickersSettings());
    }

    public async Task<List<StockItem>> GetStickers(List<StoreProductFilter> productFilters = null)
    {
      string code = "return API.store.getStockItems({type:\"stickers\",";
      if (productFilters != null && productFilters.Count > 0)
      {
        //ref StickersSettings.\u003CGetStickers\u003Ed__2 local = this;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        //local.\u003Ccode\u003E5__3 = local.\u003Ccode\u003E5__3 + "filters:\"" + string.Join(",", productFilters.Select<StoreProductFilter, string>((Func<StoreProductFilter, string>) (filter => filter.ToString().ToLowerInvariant()))) + "\",";
      }
      // ISSUE: explicit reference operation
      // ISSUE: reference to a compiler-generated field
      //(^this).\u003Ccode\u003E5__3 += "merchant:\"microsoft\"});";
      //VKResponse<VKCountedItemsObject<StockItem>> temp = await RequestsDispatcher.Execute<VKCountedItemsObject<StockItem>>(code);
      return temp.response.items;
    }

    public async void Save(List<StockItem> items)
    {
      int num = await FileHelper.WriteTextToFile<List<StockItem>>("_Stickers", items) ? 1 : 0;
    }
  }
}
