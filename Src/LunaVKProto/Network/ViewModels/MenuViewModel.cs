// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.ViewModels.MenuViewModel
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
using App1uwp.Library.Events;
using App1uwp.Network.Enums;
using System;
using System.Linq.Expressions;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace App1uwp.Network.ViewModels
{
  public class MenuViewModel : ViewModelBase, ISubscriber<CountersChanged>
  {
    private static MenuViewModel _instance;
    private string _UserPhoto;
    private string _UserName;
    private string _Status;

    public static MenuViewModel Instance
    {
      get
      {
        if (MenuViewModel._instance == null)
          MenuViewModel._instance = new MenuViewModel();
        return MenuViewModel._instance;
      }
      set => MenuViewModel._instance = value;
    }

    public MenuItemViewModel MarketItem { get; set; }

    public MenuItemViewModel NewsItem { get; private set; }

    public MenuItemViewModel NotificationsItem { get; private set; }

    public MenuItemViewModel MessagesItem { get; private set; }

    public MenuItemViewModel FriendsItem { get; private set; }

    public MenuItemViewModel CommunitiesItem { get; private set; }

    public MenuItemViewModel PhotosItem { get; private set; }

    public MenuItemViewModel VideosItem { get; private set; }

    public MenuItemViewModel AudiosItem { get; private set; }

    public MenuItemViewModel GamesItem { get; private set; }

    public MenuItemViewModel BookmarksItem { get; private set; }

    public MenuItemViewModel SettingsItem { get; private set; }

    public MenuItemViewModel SearchItem { get; private set; }

    public MenuViewModel()
    {
      this.NewsItem = new MenuItemViewModel(MenuSectionName.News);
      this.NotificationsItem = new MenuItemViewModel(MenuSectionName.Notifications);
      this.MessagesItem = new MenuItemViewModel(MenuSectionName.Messages);
      this.FriendsItem = new MenuItemViewModel(MenuSectionName.Friends);
      this.CommunitiesItem = new MenuItemViewModel(MenuSectionName.Communities);
      this.PhotosItem = new MenuItemViewModel(MenuSectionName.Photos);
      this.VideosItem = new MenuItemViewModel(MenuSectionName.Videos);
      this.AudiosItem = new MenuItemViewModel(MenuSectionName.Audios);
      this.GamesItem = new MenuItemViewModel(MenuSectionName.Games);
      this.BookmarksItem = new MenuItemViewModel(MenuSectionName.Bookmarks);
      this.SettingsItem = new MenuItemViewModel(MenuSectionName.Settings);
      this.SearchItem = new MenuItemViewModel(MenuSectionName.Search);
      this.MarketItem = new MenuItemViewModel(MenuSectionName.Market);
      EventAggregator.Instance.SubsribeEvent((object) this);
    }

    public string UserPhoto
    {
      set
      {
        this._UserPhoto = value;
        this.NotifyPropertyChanged<string>((Expression<Func<string>>) (() => this.UserPhoto));
      }
      get => this._UserPhoto;
    }

    public string UserName
    {
      set
      {
        this._UserName = value;
        this.NotifyPropertyChanged<string>((Expression<Func<string>>) (() => this.UserName));
      }
      get => this._UserName;
    }

    public string Status
    {
      set
      {
        this._Status = value;
        this.NotifyPropertyChanged(nameof (Status));
      }
      get => this._Status;
    }

    public void OnEventHandler(CountersChanged message)
    {
      this.FriendsItem.Count = message.Counts.friends;
      this.MessagesItem.Count = message.Counts.messages;
      this.CommunitiesItem.Count = message.Counts.groups;
      this.NotificationsItem.Count = message.Counts.notifications;
      this.NotifyPropertyChanged<string>((Expression<Func<string>>) (() => this.TotalCountString));
      this.NotifyPropertyChanged<Visibility>((Expression<Func<Visibility>>) (() => this.HaveAnyNotificationsVisibility));
    }

    private int TotalCount
    {
      get
      {
        return this.NotificationsItem.Count + this.MessagesItem.Count + this.FriendsItem.Count + this.CommunitiesItem.Count + this.GamesItem.Count;
      }
    }

    public string TotalCountString => this.TotalCount.ToString();

    public Visibility HaveAnyNotificationsVisibility
    {
      get => this.TotalCount == 0 ? (Visibility) 1 : (Visibility) 0;
    }

    public void UpdateSelectedItem()
    {
      MenuSectionName selectedSection = MenuSectionName.Unknown;
      if (((ContentControl) (Window.Current.Content as Frame)).Content as PageBase is NewsPage)
        selectedSection = MenuSectionName.News;
      else if (((ContentControl) (Window.Current.Content as Frame)).Content as PageBase is DialogsPage)
        selectedSection = MenuSectionName.Messages;
      else if (((ContentControl) (Window.Current.Content as Frame)).Content as PageBase is NotificationsPage)
        selectedSection = MenuSectionName.Notifications;
      else if (((ContentControl) (Window.Current.Content as Frame)).Content as PageBase is FriendsPage)
        selectedSection = MenuSectionName.Friends;
      else if (((ContentControl) (Window.Current.Content as Frame)).Content as PageBase is GroupsPage)
        selectedSection = MenuSectionName.Communities;
      else if (((ContentControl) (Window.Current.Content as Frame)).Content as PageBase is PhotoAlbumPage)
        selectedSection = MenuSectionName.Photos;
      else if (((ContentControl) (Window.Current.Content as Frame)).Content as PageBase is VideoCatalogPage)
        selectedSection = MenuSectionName.Videos;
      else if (((ContentControl) (Window.Current.Content as Frame)).Content as PageBase is SettingsPage)
        selectedSection = MenuSectionName.Settings;
      this.NewsItem.UpdateSelectionState(selectedSection);
      this.NotificationsItem.UpdateSelectionState(selectedSection);
      this.MessagesItem.UpdateSelectionState(selectedSection);
      this.FriendsItem.UpdateSelectionState(selectedSection);
      this.CommunitiesItem.UpdateSelectionState(selectedSection);
      this.PhotosItem.UpdateSelectionState(selectedSection);
      this.VideosItem.UpdateSelectionState(selectedSection);
      this.AudiosItem.UpdateSelectionState(selectedSection);
      this.GamesItem.UpdateSelectionState(selectedSection);
      this.BookmarksItem.UpdateSelectionState(selectedSection);
      this.SettingsItem.UpdateSelectionState(selectedSection);
      this.SearchItem.UpdateSelectionState(selectedSection);
      this.MarketItem.UpdateSelectionState(selectedSection);
    }
  }
}
