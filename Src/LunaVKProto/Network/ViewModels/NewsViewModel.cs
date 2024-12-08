// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.ViewModels.NewsViewModel
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Library;
using App1uwp.Network.DataObjects;
using App1uwp.Network.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

#nullable disable
namespace App1uwp.Network.ViewModels
{
  public class NewsViewModel : ViewModelBase, ISupportLoadMore
  {
    private static NewsViewModel _instance;
    private string nextFrom = "";
    private string _title = LocalizedStrings.GetString("Menu_News/Title");

    public static NewsViewModel Instance
    {
      get
      {
        if (NewsViewModel._instance == null)
          NewsViewModel._instance = new NewsViewModel();
        return NewsViewModel._instance;
      }
    }

    public bool HasMoreItems => true;

    public ObservableCollection<VKBaseDataForPostOrNews> Items { get; private set; }

    public string Title
    {
      get => this._title;
      set
      {
        this._title = value;
        this.NotifyPropertyChanged(nameof (Title));
      }
    }

    public int NewsSource { get; set; }

    public bool InterestingFirst { get; set; }

    public Action<ProfileLoadingStatus> LoadingStatusUpdated { get; set; }

    public NewsViewModel() => this.Items = new ObservableCollection<VKBaseDataForPostOrNews>();

    public void SetNewsSource(int value)
    {
      this.NewsSource = value;
      this.LoadData(true);
    }

    public async Task LoadData(bool reload = false)
    {
      if (reload)
      {
        this.nextFrom = "";
        this.Items.Clear();
        if (this.LoadingStatusUpdated != null)
          this.LoadingStatusUpdated(ProfileLoadingStatus.Reloading);
      }
      else if (this.LoadingStatusUpdated != null)
        this.LoadingStatusUpdated(ProfileLoadingStatus.Loading);
      Dictionary<string, string> parameters = new Dictionary<string, string>();
      if (!string.IsNullOrEmpty(this.nextFrom))
      {
        parameters["start_from"] = this.nextFrom;
        parameters["count"] = "30";
      }
      else
        parameters["count"] = "10";
      switch (this.NewsSource)
      {
        case 0:
          parameters["source_ids"] = "groups,pages,friends";
          parameters["filters"] = "post";
          break;
        case 2:
          parameters["source_ids"] = "friends";
          parameters["filters"] = "photo,wall_photo";
          break;
        case 3:
          parameters["source_ids"] = "groups,pages,friends";
          parameters["filters"] = "video";
          break;
        case 4:
          parameters["source_ids"] = "friends,following";
          parameters["filters"] = "post";
          parameters["return_banned"] = "1";
          break;
      }
      VKResponse<NewsViewModel.VKNewsfeedRequest> result = await RequestsDispatcher.GetResponse<NewsViewModel.VKNewsfeedRequest>(this.NewsSource == 1 ? "newsfeed.getRecommended" : "newsfeed.get", parameters);
      if (result.error.error_code != VKErrors.None)
      {
        if (this.LoadingStatusUpdated == null)
          return;
        this.LoadingStatusUpdated(ProfileLoadingStatus.LoadingFailed);
      }
      else
      {
        if (this.LoadingStatusUpdated != null)
          this.LoadingStatusUpdated(ProfileLoadingStatus.Loaded);
        List<VKGroup> groups = result.response.groups;
        List<VKProfileBase> profiles = result.response.profiles;
        UsersService.Instance.SetCachedUsers((IEnumerable<VKBaseDataForGroupOrUser>) result.response.groups);
        UsersService.Instance.SetCachedUsers((IEnumerable<VKBaseDataForGroupOrUser>) result.response.profiles);
        foreach (VKBaseDataForPostOrNews dataForPostOrNews in result.response.items)
        {
          if (dataForPostOrNews is VKNewsfeedPost)
          {
            VKNewsfeedPost p = (VKNewsfeedPost) dataForPostOrNews;
            VKBaseDataForGroupOrUser dataForGroupOrUser = p.source_id >= 0L || groups == null ? (VKBaseDataForGroupOrUser) profiles.Find((Predicate<VKProfileBase>) (ow => (long) ow.id == p.source_id)) : (VKBaseDataForGroupOrUser) groups.Find((Predicate<VKGroup>) (ow => (long) ow.id == -p.source_id));
            p.Owner = dataForGroupOrUser;
            if (p.copy_history != null)
            {
              for (int index = 0; index < p.copy_history.Count; ++index)
              {
                VKNewsfeedPost j = p.copy_history[index];
                if (j.owner_id < 0 && groups != null)
                  j.Owner = (VKBaseDataForGroupOrUser) groups.Find((Predicate<VKGroup>) (ow => ow.id == -j.owner_id));
                else
                  j.Owner = (VKBaseDataForGroupOrUser) profiles.Find((Predicate<VKProfileBase>) (ow => ow.id == j.owner_id));
              }
            }
          }
          this.Items.Add(dataForPostOrNews);
        }
        this.nextFrom = result.response.next_from;
      }
    }

    public class VKNewsfeedRequest
    {
      public List<VKProfileBase> profiles { get; set; }

      public List<VKGroup> groups { get; set; }

      public string next_from { get; set; }

      public List<VKNewsfeedPost> items { get; set; }
    }
  }
}
