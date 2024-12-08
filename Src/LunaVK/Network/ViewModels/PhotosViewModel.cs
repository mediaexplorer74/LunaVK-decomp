// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.ViewModels.PhotosViewModel
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.DataObjects;
using System.Collections.Generic;
using System.Collections.ObjectModel;

#nullable disable
namespace App1uwp.Network.ViewModels
{
  public class PhotosViewModel
  {
    private int _albumId;
    private int _ownerId;
    private uint maximum;

    public ObservableCollection<VKPhoto> Photos { get; private set; }

    public PhotosViewModel(int albumId, int ownerId)
    {
      this._albumId = albumId;
      this._ownerId = ownerId;
      this.Photos = new ObservableCollection<VKPhoto>();
    }

    public async void LoadData(bool reload = false)
    {
      Dictionary<string, string> parameters = new Dictionary<string, string>();
      parameters["count"] = "40";
      parameters["rev"] = "1";
      parameters["owner_id"] = this._ownerId.ToString();
      parameters["album_id"] = this._albumId.ToString();
      if (this.Photos.Count > 0)
        parameters["offset"] = this.Photos.Count.ToString();
      VKResponse<VKCountedItemsObject<VKPhoto>> temp = await RequestsDispatcher.GetResponse<VKCountedItemsObject<VKPhoto>>("photos.get", parameters);
      if (temp == null)
        return;
      if (reload)
        this.Photos.Clear();
      if (reload)
        this.maximum = temp.response.count;
      foreach (VKPhoto vkPhoto in temp.response.items)
        this.Photos.Add(vkPhoto);
    }
  }
}
