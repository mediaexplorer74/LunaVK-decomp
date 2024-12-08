// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.ViewModels.MenuSearchViewModel
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.DataObjects;
using App1uwp.Network.json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

#nullable disable
namespace App1uwp.Network.ViewModels
{
  public class MenuSearchViewModel
  {
    private bool _inLoading;
    private int _offset;
    public string Query = "";

    public ObservableCollection<MenuSearchViewModel.SearchHint> Items { get; private set; }

    public Action LoadMore { get; private set; }

    public Action Refresh { get; private set; }

    public MenuSearchViewModel()
    {
      this.Items = new ObservableCollection<MenuSearchViewModel.SearchHint>();
      this.LoadMore = (Action) (() =>
      {
        if (this._inLoading)
          return;
        this.LoadData();
      });
      this.Refresh = (Action) (() => this.LoadData(true));
    }

    public async void LoadData(bool reload = false, Action<bool> calback = null)
    {
      this._inLoading = true;
      if (reload)
      {
        this._offset = 0;
        this.Items.Clear();
      }
      Dictionary<string, string> parameters = new Dictionary<string, string>();
      parameters["q"] = this.Query;
      parameters["limit"] = "30";
      if (this._offset > 0)
        parameters["offset"] = this._offset.ToString();
      else
        parameters["fields"] = "first_name,last_name,photo_100,verified";
      parameters["extended"] = "1";
      this._offset += 30;
      VKResponse<VKCountedItemsObject<MenuSearchViewModel.SearchHint>> temp = await RequestsDispatcher.GetResponse<VKCountedItemsObject<MenuSearchViewModel.SearchHint>>("search.getHints", parameters);
      if (temp == null || temp.response == null)
      {
        if (calback != null)
          calback(false);
        this._inLoading = false;
      }
      else
      {
        if (temp.response == null)
          return;
        foreach (MenuSearchViewModel.SearchHint searchHint in temp.response.items)
          this.Items.Add(searchHint);
        this._inLoading = false;
      }
    }

    public class SearchHint
    {
      public string type { get; set; }

      public VKGroup group { get; set; }

      public VKProfileBase profile { get; set; }

      public string section { get; set; }

      public string description { get; set; }

      [JsonConverter(typeof (VKBooleanConverter))]
      public bool global { get; set; }

      public string Photo
      {
        get
        {
          if (this.group != null)
            return this.group.photo_100;
          return this.profile != null ? this.profile.photo_100 : "";
        }
      }

      public string Title
      {
        get
        {
          if (this.group != null)
            return this.group.name;
          return this.profile != null ? this.profile.Title : "";
        }
      }

      public bool Verified
      {
        get
        {
          if (this.group != null)
            return this.group.verified;
          return this.profile != null && this.profile.verified;
        }
      }
    }
  }
}
