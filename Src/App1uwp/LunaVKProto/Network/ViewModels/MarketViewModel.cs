// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.ViewModels.MarketViewModel
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.DataObjects;
using App1uwp.Network.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

#nullable disable
namespace App1uwp.Network.ViewModels
{
  public class MarketViewModel
  {
    public ObservableCollection<VKMarketItem> Items { get; private set; }

    public Action Refresh { get; private set; }

    public Action<ProfileLoadingStatus> LoadingStatusUpdated { get; set; }

    public MarketViewModel()
    {
      this.Items = new ObservableCollection<VKMarketItem>();
      this.Refresh = (Action) (() => this.Items.Clear());
    }

    private async Task<VKCountedItemsObject<VKMarketItem>> LoadData()
    {
      Dictionary<string, string> parameters = new Dictionary<string, string>();
      parameters["count"] = "30";
      VKCountedItemsObject<VKMarketItem> temp = new VKCountedItemsObject<VKMarketItem>();
      temp.items = new List<VKMarketItem>();
      VKResponse<VKCountedItemsObject<VKMarketItem>> result = await RequestsDispatcher.GetResponse<VKCountedItemsObject<VKMarketItem>>("market.get", parameters);
      if (result.error.error_code == VKErrors.None)
        return temp;
      if (this.LoadingStatusUpdated != null)
        this.LoadingStatusUpdated(ProfileLoadingStatus.LoadingFailed);
      return (VKCountedItemsObject<VKMarketItem>) null;
    }
  }
}
