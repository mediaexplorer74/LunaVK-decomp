// Decompiled with JetBrains decompiler
// Type: App1uwp.Library.NavigatorImpl
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
using App1uwp.Network.DataObjects;
using App1uwp.Utils;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

#nullable disable
namespace App1uwp.Library
{
  public class NavigatorImpl
  {
    private static NavigatorImpl _instance;
    private static readonly Regex _friendsReg = new Regex("/friends(\\?id=[0-9])?");
    private static readonly Regex _communitiesReg = new Regex("/groups(\\s|$)");
    private static readonly Regex _dialogsReg = new Regex("/(im|mail)(\\s|$)");
    private static readonly Regex _dialogReg = new Regex("/write[-0-9]+");
    private static readonly Regex _wallReg = new Regex("/wall[-0-9]+_[0-9]+");
    private static readonly Regex _feedWallReg = new Regex("/feed?w=wall[-0-9]+_[0-9]+");
    private static readonly Regex _audiosReg = new Regex("/audios[-0-9]+");
    private static readonly Regex _newsReg = new Regex("/feed(\\s|$)");
    private static readonly Regex _recommendedNewsReg = new Regex("/feed\\?section=recommended(\\s|$)");
    private static readonly Regex _feedbackReg = new Regex("/feed\\?section=notifications(\\s|$)");
    private static readonly Regex _profileReg = new Regex("/(id|wall)[0-9]+");
    private static readonly Regex _communityReg = new Regex("/(club|event|public|wall)[-0-9]+");
    private static readonly Regex _photosReg = new Regex("/(photos|albums)[-0-9]+");
    private static readonly Regex _photoReg = new Regex("/photo[-0-9]+_[0-9]+");
    private static readonly Regex _albumReg = new Regex("/album[-0-9]+_[0-9]+");
    private static readonly Regex _tagReg = new Regex("/tag[0-9]+");
    private static readonly Regex _videosReg = new Regex("/videos[-0-9]+");
    private static readonly Regex _videoReg = new Regex("/video[-0-9]+_[0-9]+");
    private static readonly Regex _boardReg = new Regex("/board[0-9]+");
    private static readonly Regex _topicReg = new Regex("/topic[-0-9]+_[0-9]+");
    private static readonly Regex _stickersSettingsReg = new Regex("/stickers/settings(\\s|$)");
    private static readonly Regex _settingsReg = new Regex("/settings(\\s|$)");
    private static readonly Regex _stickersReg = new Regex("/stickers(\\s|\\?|$)");
    private static readonly Regex _stickersPackReg = new Regex("/stickers([\\/A-Za-z0-9]+)");
    private static readonly Regex _faveReg = new Regex("/fave(\\s|$)");
    private static readonly Regex _appsReg = new Regex("/apps(\\s|$)");
    private static readonly Regex _appReg = new Regex("/app[-0-9]+_[-0-9]+");
    private static readonly Regex _marketAlbumReg = new Regex("/market[-0-9]+\\?section=album_[-0-9]+");
    private static readonly Regex _marketReg = new Regex("/market[-0-9]+");
    private static readonly Regex _productReg = new Regex("/product[-0-9]+_[0-9]+");
    private static readonly Regex _giftsReg = new Regex("/gifts[0-9]+");
    private static readonly Regex _giftsCatalog = new Regex("/gifts(\\s|$)");
    private static readonly Regex _namedObjReg = new Regex("/[A-Za-z0-9\\\\._-]+");
    private readonly List<NavigatorImpl.NavigationTypeMatch> _navTypesList = new List<NavigatorImpl.NavigationTypeMatch>()
    {
      new NavigatorImpl.NavigationTypeMatch(NavigatorImpl._friendsReg, NavigatorImpl.NavType.friends),
      new NavigatorImpl.NavigationTypeMatch(NavigatorImpl._communitiesReg, NavigatorImpl.NavType.communities),
      new NavigatorImpl.NavigationTypeMatch(NavigatorImpl._dialogsReg, NavigatorImpl.NavType.dialogs),
      new NavigatorImpl.NavigationTypeMatch(NavigatorImpl._dialogReg, NavigatorImpl.NavType.dialog),
      new NavigatorImpl.NavigationTypeMatch(NavigatorImpl._wallReg, NavigatorImpl.NavType.wallPost),
      new NavigatorImpl.NavigationTypeMatch(NavigatorImpl._feedWallReg, NavigatorImpl.NavType.wallPost),
      new NavigatorImpl.NavigationTypeMatch(NavigatorImpl._audiosReg, NavigatorImpl.NavType.audios),
      new NavigatorImpl.NavigationTypeMatch(NavigatorImpl._newsReg, NavigatorImpl.NavType.news),
      new NavigatorImpl.NavigationTypeMatch(NavigatorImpl._recommendedNewsReg, NavigatorImpl.NavType.recommendedNews),
      new NavigatorImpl.NavigationTypeMatch(NavigatorImpl._feedbackReg, NavigatorImpl.NavType.feedback),
      new NavigatorImpl.NavigationTypeMatch(NavigatorImpl._profileReg, NavigatorImpl.NavType.profile),
      new NavigatorImpl.NavigationTypeMatch(NavigatorImpl._communityReg, NavigatorImpl.NavType.community),
      new NavigatorImpl.NavigationTypeMatch(NavigatorImpl._photosReg, NavigatorImpl.NavType.albums),
      new NavigatorImpl.NavigationTypeMatch(NavigatorImpl._photoReg, NavigatorImpl.NavType.photo),
      new NavigatorImpl.NavigationTypeMatch(NavigatorImpl._albumReg, NavigatorImpl.NavType.album),
      new NavigatorImpl.NavigationTypeMatch(NavigatorImpl._tagReg, NavigatorImpl.NavType.tagPhoto),
      new NavigatorImpl.NavigationTypeMatch(NavigatorImpl._videosReg, NavigatorImpl.NavType.videos),
      new NavigatorImpl.NavigationTypeMatch(NavigatorImpl._videoReg, NavigatorImpl.NavType.video),
      new NavigatorImpl.NavigationTypeMatch(NavigatorImpl._boardReg, NavigatorImpl.NavType.board),
      new NavigatorImpl.NavigationTypeMatch(NavigatorImpl._topicReg, NavigatorImpl.NavType.topic),
      new NavigatorImpl.NavigationTypeMatch(NavigatorImpl._stickersSettingsReg, NavigatorImpl.NavType.stickersSettings),
      new NavigatorImpl.NavigationTypeMatch(NavigatorImpl._settingsReg, NavigatorImpl.NavType.settings),
      new NavigatorImpl.NavigationTypeMatch(NavigatorImpl._faveReg, NavigatorImpl.NavType.fave),
      new NavigatorImpl.NavigationTypeMatch(NavigatorImpl._appsReg, NavigatorImpl.NavType.apps),
      new NavigatorImpl.NavigationTypeMatch(NavigatorImpl._appReg, NavigatorImpl.NavType.app),
      new NavigatorImpl.NavigationTypeMatch(NavigatorImpl._marketAlbumReg, NavigatorImpl.NavType.marketAlbum),
      new NavigatorImpl.NavigationTypeMatch(NavigatorImpl._marketReg, NavigatorImpl.NavType.market),
      new NavigatorImpl.NavigationTypeMatch(NavigatorImpl._productReg, NavigatorImpl.NavType.product),
      new NavigatorImpl.NavigationTypeMatch(NavigatorImpl._stickersReg, NavigatorImpl.NavType.stickers),
      new NavigatorImpl.NavigationTypeMatch(NavigatorImpl._stickersPackReg, NavigatorImpl.NavType.stickersPack),
      new NavigatorImpl.NavigationTypeMatch(NavigatorImpl._giftsReg, NavigatorImpl.NavType.gifts),
      new NavigatorImpl.NavigationTypeMatch(NavigatorImpl._giftsCatalog, NavigatorImpl.NavType.giftsCatalog),
      new NavigatorImpl.NavigationTypeMatch(NavigatorImpl._namedObjReg, NavigatorImpl.NavType.namedObject)
    };

    public static NavigatorImpl Instance
    {
      get
      {
        if (NavigatorImpl._instance == null)
        {
          NavigatorImpl._instance = new NavigatorImpl();
          CustomFrame navigationService = NavigatorImpl.NavigationService;
          WindowsRuntimeMarshal.AddEventHandler<NavigatingCancelEventHandler>(new Func<NavigatingCancelEventHandler, EventRegistrationToken>(((Frame) navigationService).add_Navigating), new Action<EventRegistrationToken>(((Frame) navigationService).remove_Navigating), new NavigatingCancelEventHandler(NavigatorImpl.NavigationService_Navigating));
        }
        return NavigatorImpl._instance;
      }
    }

    private static void NavigationService_Navigating(object sender, NavigatingCancelEventArgs e)
    {
      if (e.Parameter == null && ((ContentControl) NavigatorImpl.NavigationService).Content != null && ((ContentControl) NavigatorImpl.NavigationService).Content.ToString() == e.SourcePageType.FullName)
      {
        e.put_Cancel(true);
      }
      else
      {
        if (e.Parameter == null || ((ContentControl) NavigatorImpl.NavigationService).Content == null)
          return;
        PagesParams navigationParameter = (((ContentControl) NavigatorImpl.NavigationService).Content as PageBase).NavigationParameter as PagesParams;
        PagesParams parameter = e.Parameter as PagesParams;
        if (navigationParameter == null || parameter == null)
          return;
        bool flag = false;
        if (parameter.chat_id > 0)
        {
          if (navigationParameter.chat_id == parameter.chat_id)
            flag = true;
        }
        else if (navigationParameter.user_id == parameter.user_id)
          flag = true;
        if (!flag)
          return;
        e.put_Cancel(true);
      }
    }

    private static CustomFrame NavigationService => Window.Current.Content as CustomFrame;

    public void NavigateToAudio(int OwnerId)
    {
      this.Navigate(typeof (MusicPage), (object) new PagesParams()
      {
        user_id = OwnerId
      });
    }

    public void NavigateToConversation(int UserId, int ChatId = 0, object attachment = null)
    {
      this.Navigate(typeof (ConversationPage), (object) new PagesParams()
      {
        user_id = (ChatId > 0 ? 0 : UserId),
        chat_id = ChatId,
        attachment = attachment
      });
    }

    public void NavigateToProfilePage(long Id)
    {
      this.Navigate(Id < 0L ? typeof (GroupPage) : typeof (UserPage), (object) new Dictionary<string, long>()
      {
        {
          nameof (Id),
          Id
        }
      });
    }

    public void NavigateToFeedback() => this.Navigate(typeof (NotificationsPage));

    public void NavigateToConversations(object attachment = null)
    {
      this.Navigate(typeof (DialogsPage), attachment);
    }

    public void NavigateToFriends(long userId)
    {
      this.Navigate(typeof (FriendsPage), (object) new Dictionary<string, long>()
      {
        {
          "Id",
          userId
        }
      });
    }

    public void NavigateToGroups(long userId)
    {
      this.Navigate(typeof (GroupsPage), (object) new Dictionary<string, long>()
      {
        {
          "Id",
          userId
        }
      });
    }

    public void NavigateToPhotoAlbums(long userId)
    {
      this.Navigate(typeof (PhotoAlbumPage), (object) new Dictionary<string, long>()
      {
        {
          "Id",
          userId
        }
      });
    }

    public void NavigateToSettings() => this.Navigate(typeof (SettingsPage));

    public void NavigateToFavorites() => this.Navigate(typeof (FavoritesPage));

    public static bool GoBack()
    {
      if (!NavigatorImpl.NavigationService.CanGoBack)
        return false;
      NavigatorImpl.NavigationService.GoBack();
      return true;
    }

    private void Navigate(Type navStr, object parameter = null)
    {
      NavigatorImpl.NavigationService.Navigate(navStr, parameter);
      if ((Window.Current.Content as CustomFrame).MenuState != CustomFrame.MenuStates.StateCollapsed)
        return;
      (Window.Current.Content as CustomFrame).HeaderWithMenu.OpenCloseMenu(new bool?(false));
    }

    public void ClearBackStack() => NavigatorImpl.NavigationService.BackStack.Clear();

    public void NavigateToNewsFeed() => this.Navigate(typeof (NewsPage));

    public void NavigateToWallPostComments(
      ulong postId,
      long ownerId,
      bool focusCommentsField = false,
      VKBaseDataForPostOrNews postData = null)
    {
      Dictionary<string, object> parameter = new Dictionary<string, object>();
      parameter.Add("PostId", (object) postId);
      parameter.Add("PostOwnerId", (object) ownerId);
      if (postData != null)
        parameter.Add("WallPost", (object) postData);
      this.Navigate(typeof (PostCommentsPage), (object) parameter);
    }

    public void NavigateToVideoWithComments(
      long ownerId,
      ulong videoId,
      string accessKey = "",
      VKVideoBase video = null)
    {
      Dictionary<string, object> parameter = new Dictionary<string, object>();
      parameter.Add("OwnerId", (object) ownerId);
      parameter.Add("VideoId", (object) videoId);
      if (!string.IsNullOrEmpty(accessKey))
        parameter.Add("AccessKey", (object) accessKey);
      if (video != null)
        parameter.Add("VideoContext", (object) video);
      this.Navigate(typeof (VideoCommentsPage), (object) parameter);
    }

    public void NavigateToCommunityManagement(long communityId)
    {
      this.Navigate(typeof (GroupManagementPage), (object) new PagesParams()
      {
        user_id = (int) communityId
      });
    }

    public void NavigateToMarketPage() => this.Navigate(typeof (MarketPage));

    public void NavigateToWebUri(string uri, bool forceWebNavigation = false, bool fromPush = false)
    {
      if (string.IsNullOrWhiteSpace(uri) || uri.StartsWith("tel:"))
        return;
      if (!uri.StartsWith("http://", StringComparison.CurrentCultureIgnoreCase) && !uri.StartsWith("https://", StringComparison.CurrentCultureIgnoreCase))
        uri = "http://" + uri;
      bool flag = false;
      if (!forceWebNavigation)
        flag = this.GetWithinAppNavigationUri(uri, fromPush);
      int num = flag ? 1 : 0;
    }

    public bool GetWithinAppNavigationUri(string uri, bool fromPush = false, Action<bool> customCallback = null)
    {
      if (!NavigatorImpl.IsVKUri(uri))
        return false;
      string uri1 = uri;
      int num = uri1.IndexOf("://");
      if (num > -1)
        uri1 = uri1.Remove(0, num + 3);
      int count = uri1.IndexOf("/");
      if (count > -1)
        uri1 = uri1.Remove(0, count);
      if (uri1.StartsWith("dev/") || uri1.StartsWith("dev") && uri1.Length == 3)
        return false;
      Dictionary<string, string> queryString = uri.ParseQueryString();
      if (uri1.StartsWith("/feed") && queryString.ContainsKey("section") && queryString["section"] == "search")
        return true;
      long id1;
      NavigatorImpl.NavType navigationType = this.GetNavigationType(uri1, out id1, out long _, out string _, out string _);
      if (navigationType == NavigatorImpl.NavType.none)
        return false;
      if (id1 == 0L)
        id1 = Settings.Instance.auth.UserId;
      bool appNavigationUri = true;
      switch (navigationType)
      {
        case NavigatorImpl.NavType.dialogs:
          this.NavigateToConversations();
          break;
        case NavigatorImpl.NavType.profile:
        case NavigatorImpl.NavType.community:
          this.NavigateToProfilePage(id1);
          break;
        case NavigatorImpl.NavType.topic:
          appNavigationUri = false;
          break;
        case NavigatorImpl.NavType.settings:
          this.NavigateToSettings();
          break;
        case NavigatorImpl.NavType.feedback:
          this.NavigateToFeedback();
          break;
        case NavigatorImpl.NavType.fave:
          this.NavigateToFavorites();
          break;
        case NavigatorImpl.NavType.apps:
          appNavigationUri = false;
          break;
      }
      return appNavigationUri;
    }

    private static bool IsVKUri(string uri)
    {
      uri = uri.ToLowerInvariant();
      uri = uri.Replace("http://", "").Replace("https://", "");
      if (uri.StartsWith("m.") || uri.StartsWith("t.") || uri.StartsWith("0."))
        uri = uri.Remove(0, 2);
      if (uri.StartsWith("www.") || uri.StartsWith("new."))
        uri = uri.Remove(0, 4);
      return uri.StartsWith("vk.com/") || uri.StartsWith("vkontakte.ru/") || uri.StartsWith("vk.me/");
    }

    private NavigatorImpl.NavType GetNavigationType(
      string uri,
      out long id1,
      out long id2,
      out string obj,
      out string objSub)
    {
      id1 = id2 = 0L;
      obj = objSub = "";
      foreach (NavigatorImpl.NavigationTypeMatch navTypes1 in this._navTypesList)
      {
        if (navTypes1.Check(uri))
        {
          if (navTypes1.SubTypes.Count > 0)
          {
            foreach (string subType in navTypes1.SubTypes)
            {
              foreach (NavigatorImpl.NavigationTypeMatch navTypes2 in this._navTypesList)
              {
                if (navTypes2.Check(subType))
                {
                  id1 = navTypes2.Id1;
                  id2 = navTypes2.Id2;
                  obj = navTypes2.ObjName;
                  objSub = navTypes2.ObjSubName;
                  return navTypes2.MatchType;
                }
              }
            }
          }
          id1 = navTypes1.Id1;
          id2 = navTypes1.Id2;
          obj = navTypes1.ObjName;
          objSub = navTypes1.ObjSubName;
          return navTypes1.MatchType;
        }
      }
      return NavigatorImpl.NavType.none;
    }

    public class NavigationTypeMatch
    {
      private readonly Regex _idsRegEx = new Regex("\\-?[0-9]+");
      private readonly Regex _queryParamsRegex = new Regex("(\\?|\\&)([^=]+)\\=([^&]+)");
      private readonly Regex _regEx;

      public NavigatorImpl.NavType MatchType { get; private set; }

      public long Id1 { get; private set; }

      public long Id2 { get; private set; }

      public List<string> SubTypes { get; private set; }

      public string ObjName { get; private set; }

      public string ObjSubName { get; private set; }

      public NavigationTypeMatch(Regex regExp, NavigatorImpl.NavType navType)
      {
        this._regEx = regExp;
        this.MatchType = navType;
      }

      public bool Check(string uri)
      {
        MatchCollection matchCollection1 = this._regEx.Matches(uri);
        if (matchCollection1.Count == 0)
          return false;
        Match match1 = matchCollection1[0];
        this.ObjName = match1.Value;
        if (match1.Groups.Count > 0)
          this.ObjSubName = match1.Groups[match1.Groups.Count - 1].Value;
        MatchCollection matchCollection2 = this._idsRegEx.Matches(this.ObjName);
        if (matchCollection2.Count > 0)
        {
          long result;
          this.Id1 = long.TryParse(matchCollection2[0].Value, out result) ? result : 0L;
        }
        if (matchCollection2.Count > 1)
        {
          long result;
          this.Id2 = long.TryParse(matchCollection2[1].Value, out result) ? result : 0L;
        }
        MatchCollection matchCollection3 = this._queryParamsRegex.Matches(uri);
        this.SubTypes = new List<string>();
        foreach (Match match2 in matchCollection3)
        {
          if (match2.Groups.Count == 4 && match2.Groups[2].Value == "w")
            this.SubTypes.Add("/" + match2.Groups[match2.Groups.Count - 1].Value);
        }
        return true;
      }
    }

    public enum NavType
    {
      none,
      friends,
      communities,
      dialogs,
      news,
      tagPhoto,
      albums,
      profile,
      dialog,
      community,
      board,
      album,
      video,
      audios,
      topic,
      photo,
      wallPost,
      namedObject,
      stickersSettings,
      settings,
      feedback,
      videos,
      fave,
      apps,
      marketAlbum,
      market,
      product,
      stickers,
      stickersPack,
      recommendedNews,
      app,
      gifts,
      giftsCatalog,
    }
  }
}
