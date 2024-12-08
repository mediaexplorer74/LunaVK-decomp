// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.ViewModels.StickersStoreViewModel
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Emoji;
using App1uwp.Network.DataObjects;
using App1uwp.Network.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

#nullable disable
namespace App1uwp.Network.ViewModels
{
  public class StickersStoreViewModel
  {
    private bool InLoading;

    public ObservableCollection<StockItem> StickersNew { get; private set; }

    public ObservableCollection<StockItem> StickersPopular { get; private set; }

    public ObservableCollection<StockItem> StickersFree { get; private set; }

    public string Title => "Магазин стикеров";

    public StickersStoreViewModel()
    {
      this.StickersNew = new ObservableCollection<StockItem>();
      this.StickersPopular = new ObservableCollection<StockItem>();
      this.StickersFree = new ObservableCollection<StockItem>();
    }

    public async void LoadData(bool reload = false, Action<bool> calback = null)
    {
      this.InLoading = true;
      int num = reload ? 1 : 0;
      List<StoreProductFilter> l = new List<StoreProductFilter>();
      List<StockItem> temp = await StickersSettings.Instance.GetStickers(l);
      if (temp == null)
      {
        if (calback != null)
          calback(false);
        this.InLoading = false;
      }
      else
      {
        if (reload)
        {
          this.StickersNew.Clear();
          this.StickersPopular.Clear();
          this.StickersFree.Clear();
        }
        foreach (StockItem stockItem in temp)
        {
          if (stockItem.free)
            this.StickersFree.Add(stockItem);
          else if (stockItem.@new)
            this.StickersNew.Add(stockItem);
          else
            this.StickersPopular.Add(stockItem);
        }
      }
    }
  }
}
