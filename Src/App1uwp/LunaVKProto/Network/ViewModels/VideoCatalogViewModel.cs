// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.ViewModels.VideoCatalogViewModel
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.DataObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

#nullable disable
namespace App1uwp.Network.ViewModels
{
  public class VideoCatalogViewModel
  {
    private bool InLoading;
    private string nextFrom = "";

    public string Title => LocalizedStrings.GetString("Menu_Videos/Title");

    public ObservableCollection<GetCatalogResponse.VideoCatalogCategory> Catalog { get; private set; }

    public Action LoadMore { get; private set; }

    public Action Refresh { get; private set; }

    public VideoCatalogViewModel()
    {
      this.Catalog = new ObservableCollection<GetCatalogResponse.VideoCatalogCategory>();
    }

    public async void LoadData(bool reload = false, Action<bool> calback = null)
    {
      this.InLoading = true;
      if (reload)
        await Task.Delay(100);
      VKResponse<GetCatalogResponse> temp = await RequestsDispatcher.GetResponse<GetCatalogResponse>("video.getCatalog", new Dictionary<string, string>()
      {
        ["items_count"] = "3",
        ["extended"] = "1",
        ["filters"] = "other,top,series,ugc",
        ["from"] = this.nextFrom
      });
      if (temp == null)
      {
        if (calback != null)
          calback(false);
        this.InLoading = false;
      }
      else
      {
        List<VKGroup> groups = temp.response.groups;
        List<VKProfileBase> profiles = temp.response.profiles;
        foreach (GetCatalogResponse.VideoCatalogCategory videoCatalogCategory in temp.response.items)
        {
          int owner_id = 0;
          if (int.TryParse(videoCatalogCategory.id, out owner_id))
          {
            VKBaseDataForGroupOrUser dataForGroupOrUser = owner_id >= 0 || groups == null ? (VKBaseDataForGroupOrUser) profiles.Find((Predicate<VKProfileBase>) (ow => ow.id == owner_id)) : (VKBaseDataForGroupOrUser) groups.Find((Predicate<VKGroup>) (ow => ow.id == -owner_id));
            if (dataForGroupOrUser != null)
              videoCatalogCategory.icon_2x = dataForGroupOrUser.photo_50;
            videoCatalogCategory.uc_icon = "";
          }
          else
          {
            videoCatalogCategory.uc_icon = this.GetGlyph(videoCatalogCategory.id);
            videoCatalogCategory.icon_2x = "";
          }
          foreach (VKVideoBase vkVideoBase in videoCatalogCategory.items)
          {
            VKVideoBase item = vkVideoBase;
            VKBaseDataForGroupOrUser dataForGroupOrUser = item.owner_id >= 0L || groups == null ? (VKBaseDataForGroupOrUser) profiles.Find((Predicate<VKProfileBase>) (ow => (long) ow.id == item.owner_id)) : (VKBaseDataForGroupOrUser) groups.Find((Predicate<VKGroup>) (ow => (long) ow.id == -item.owner_id));
          }
          this.Catalog.Add(videoCatalogCategory);
        }
      }
    }

    private string GetGlyph(string type)
    {
      switch (type)
      {
        case "top":
          return "\uE735";
        case "ugc":
          return "\uECAD";
        case "series":
          return "\uE714";
        default:
          return "";
      }
    }
  }
}
