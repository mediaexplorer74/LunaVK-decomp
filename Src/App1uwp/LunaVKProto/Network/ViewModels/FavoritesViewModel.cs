// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.ViewModels.FavoritesViewModel
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Library;
using App1uwp.Network.DataObjects;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

#nullable disable
namespace App1uwp.Network.ViewModels
{
  public class FavoritesViewModel : ISupportLoadMore
  {
    public int SubPage;
    private uint _maximumPhotos;
    private uint _maximumVideos;

    public ObservableCollection<VKPhoto> Photos { get; private set; }

    public ObservableCollection<VKVideoBase> Videos { get; private set; }

    public ObservableCollection<VKPhoto> Posts { get; private set; }

    public ObservableCollection<VKPhoto> Users { get; private set; }

    public ObservableCollection<VKPhoto> Links { get; private set; }

    public ObservableCollection<VKPhoto> Products { get; private set; }

    public FavoritesViewModel()
    {
      this.Photos = new ObservableCollection<VKPhoto>();
      this.Videos = new ObservableCollection<VKVideoBase>();
    }

    public bool HasMoreItems
    {
      get
      {
        switch (this.SubPage)
        {
          case 0:
            return this.Photos.Count == 0 || (long) this.Photos.Count < (long) this._maximumPhotos;
          case 1:
            return this.Videos.Count == 0 || (long) this.Videos.Count < (long) this._maximumVideos;
          default:
            return false;
        }
      }
    }

    public async Task LoadData(bool reload = false)
    {
      if (!this.HasMoreItems)
        return;
      switch (this.SubPage)
      {
        case 0:
          await this.LoadDataPhotos();
          break;
        case 1:
          await this.LoadDataVideos();
          break;
      }
    }

    private async Task LoadDataPhotos()
    {
      Dictionary<string, string> parameters = new Dictionary<string, string>();
      parameters["count"] = "40";
      if (this.Photos.Count > 0)
        parameters["offset"] = this.Photos.Count.ToString();
      VKResponse<VKCountedItemsObject<VKPhoto>> temp = await RequestsDispatcher.GetResponse<VKCountedItemsObject<VKPhoto>>("fave.getPhotos", parameters);
      this._maximumPhotos = temp.response.count;
      foreach (VKPhoto vkPhoto in temp.response.items)
        this.Photos.Add(vkPhoto);
    }

    private async Task LoadDataVideos()
    {
      Dictionary<string, string> parameters = new Dictionary<string, string>();
      parameters["count"] = "40";
      if (this.Videos.Count > 0)
        parameters["offset"] = this.Videos.Count.ToString();
      VKResponse<VKCountedItemsObject<VKVideoBase>> temp = await RequestsDispatcher.GetResponse<VKCountedItemsObject<VKVideoBase>>("fave.getVideos", parameters);
      this._maximumVideos = temp.response.count;
      foreach (VKVideoBase vkVideoBase in temp.response.items)
        this.Videos.Add(vkVideoBase);
    }
  }
}
