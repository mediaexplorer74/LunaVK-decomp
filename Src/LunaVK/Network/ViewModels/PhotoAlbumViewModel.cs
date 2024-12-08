// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.ViewModels.PhotoAlbumViewModel
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
  public class PhotoAlbumViewModel
  {
    private bool InLoading;
    private int offset;
    private uint maximum;

    public ObservableCollection<VKAlbumPhoto> Albums { get; private set; }

    public Action LoadMore { get; private set; }

    public string Title => LocalizedStrings.GetString("Menu_Photos/Title");

    public PhotoAlbumViewModel()
    {
      this.Albums = new ObservableCollection<VKAlbumPhoto>();
      this.LoadMore = (Action) (() =>
      {
        if (this.InLoading || (long) this.Albums.Count >= (long) this.maximum)
          return;
        this.LoadData();
      });
    }

    public async void LoadData(bool reload = false, Action<bool> calback = null)
    {
      this.InLoading = true;
      if (reload)
        this.offset = 0;
      Dictionary<string, string> parameters = new Dictionary<string, string>();
      parameters["count"] = "20";
      parameters["need_covers"] = "1";
      parameters["photo_sizes"] = "1";
      if (this.offset > 0)
        parameters["offset"] = this.offset.ToString();
      this.offset += 20;
      VKResponse<VKCountedItemsObject<VKAlbumPhoto>> temp = await RequestsDispatcher.GetResponse<VKCountedItemsObject<VKAlbumPhoto>>("photos.getAlbums", parameters);
      if (temp == null)
      {
        if (calback != null)
          calback(false);
        this.InLoading = false;
      }
      else
      {
        if (reload)
          this.Albums.Clear();
        if (reload)
          this.maximum = temp.response.count;
        foreach (VKAlbumPhoto vkAlbumPhoto in temp.response.items)
          this.Albums.Add(vkAlbumPhoto);
        if (calback != null)
          calback(true);
        await Task.Delay(1000);
        this.InLoading = false;
      }
    }
  }
}
