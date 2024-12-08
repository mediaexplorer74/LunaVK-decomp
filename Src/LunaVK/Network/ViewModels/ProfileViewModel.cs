// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.ViewModels.ProfileViewModel
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
using App1uwp.Network.DataObjects;
using App1uwp.Network.Enums;
using App1uwp.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;

#nullable disable
namespace App1uwp.Network.ViewModels
{
  public class ProfileViewModel : ViewModelBase
  {
    private bool _inLoading;
    private int offset;
    private uint _maximum;

    public long Id { get; private set; }

    public ObservableCollection<ProfileViewModel.CounterVM> Counters { get; private set; }

    public VKProfileBase UserData { get; private set; }

    public List<VKPhoto> Photos { get; private set; }

    public ObservableCollection<VKWallPost> Items { get; private set; }

    public Action LoadMore { get; private set; }

    public Action Refresh { get; private set; }

    public Rect ProfileImageClipRect { get; private set; }

    public Thickness ProfileImageMargin { get; private set; }

    public Action<ProfileLoadingStatus> LoadingStatusUpdated { get; set; }

    public ProfileViewModel(long id)
    {
      this.Photos = new List<VKPhoto>();
      this.VerifiedVisibility = (Visibility) 1;
      this.DataVisibility = (Visibility) 1;
      this.Id = id;
      this.Counters = new ObservableCollection<ProfileViewModel.CounterVM>();
      this.Items = new ObservableCollection<VKWallPost>();
      this.LoadMore = (Action) (() =>
      {
        if (this._inLoading && (long) this.Items.Count >= (long) this._maximum)
          return;
        this.LoadData();
      });
      this.Refresh = (Action) (() => this.LoadData(true));
    }

    public Visibility VerifiedVisibility { get; private set; }

    public Visibility DataVisibility { get; private set; }

    public string ProfileImageUrl { get; private set; }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public string Avatar { get; private set; }

    public string Activity { get; private set; }

    public Visibility CountersVisibility { get; private set; }

    public string TextButtonSecondaryAction { get; private set; }

    public string Status { get; private set; }

    public Visibility StatusVisibility { get; private set; }

    public double CoverImageHeight { get; private set; }

    public double ProfileImageWidth { get; private set; }

    public double ProfileImageHeight { get; private set; }

    public double CoverImageOffset { get; private set; }

    public async void LoadData(bool reload = false, Action<bool> calback = null)
    {
      this._inLoading = true;
      if (reload)
      {
        this.Items.Clear();
        this.Counters.Clear();
        this.offset = 0;
        if (this.LoadingStatusUpdated != null)
          this.LoadingStatusUpdated(ProfileLoadingStatus.Reloading);
      }
      else if (this.LoadingStatusUpdated != null)
        this.LoadingStatusUpdated(ProfileLoadingStatus.Loading);
      VKResponse<ProfileViewModel.VKUserRequest> temp = (VKResponse<ProfileViewModel.VKUserRequest>) null;
      if (reload)
      {
        string code = "var owner = API.users.get({user_ids:\"" + (object) this.Id + "\",fields:\"description,photo_100,verified,activity,counters,can_write_private_message,status,friend_status,crop_photo,education,bdate,occupation,city,last_seen,sex,online\"});";
        ref ProfileViewModel.\u003CLoadData\u003Ed__10 local1 = this;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        local1.\u003Ccode\u003E5__12 = local1.\u003Ccode\u003E5__12 + "var wall = API.wall.get({owner_id:" + (object) this.Id + ",count:20, extended:1,fields:\"photo_100,verified\"});";
        ref ProfileViewModel.\u003CLoadData\u003Ed__10 local2 = this;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        local2.\u003Ccode\u003E5__12 = local2.\u003Ccode\u003E5__12 + "var profile_photos = API.photos.getAll({owner_id:" + (object) this.Id + "});";
        // ISSUE: explicit reference operation
        // ISSUE: reference to a compiler-generated field
        (^this).\u003Ccode\u003E5__12 += "return {owner:owner[0],profiles:wall.profiles,groups:wall.groups,items:wall.items,profile_photos:profile_photos};";
        temp = await RequestsDispatcher.Execute<ProfileViewModel.VKUserRequest>(code, (Func<string, string>) (jsonStr => RequestsDispatcher.FixArrayToObject(jsonStr, "counters")));
      }
      else
        temp = await RequestsDispatcher.GetResponse<ProfileViewModel.VKUserRequest>("wall.get", new Dictionary<string, string>()
        {
          ["owner_id"] = this.Id.ToString(),
          ["offset"] = this.offset.ToString(),
          ["extended"] = "1"
        });
      if (temp == null)
      {
        this._inLoading = false;
        if (this.LoadingStatusUpdated == null)
          return;
        this.LoadingStatusUpdated(ProfileLoadingStatus.LoadingFailed);
      }
      else
      {
        this.offset += 20;
        if (reload)
        {
          this._maximum = temp.response.count;
          VKProfileBase owner = temp.response.owner;
          this.UserData = owner;
          if (this.LoadingStatusUpdated != null)
            this.LoadingStatusUpdated(ProfileLoadingStatus.Loaded);
          this.Name = owner.FullName;
          this.Avatar = owner.photo_100;
          this.VerifiedVisibility = owner.verified ? (Visibility) 0 : (Visibility) 1;
          this.Activity = owner.activity;
          this.TextButtonSecondaryAction = this.FormatSecondaryAction(owner);
          if (owner.counters != null)
          {
            if (owner.counters.albums > 0)
              this.Counters.Add(new ProfileViewModel.CounterVM(UIStringFormatterHelper.FormatNumberOfSomething(owner.counters.albums, LocalizedStrings.GetString("OneAlbumFrm"), LocalizedStrings.GetString("TwoFourAlbumsFrm"), LocalizedStrings.GetString("FiveAlbumsFrm"), false), owner.counters.albums));
            if (owner.counters.photos > 0)
              this.Counters.Add(new ProfileViewModel.CounterVM(UIStringFormatterHelper.FormatNumberOfSomething(owner.counters.photos, LocalizedStrings.GetString("AlbumsPhotosCountOneFrm"), LocalizedStrings.GetString("AlbumsPhotosCountTwoFrm"), LocalizedStrings.GetString("AlbumsPhotosCountFiveFrm"), false), owner.counters.photos));
            if (owner.counters.docs > 0)
              this.Counters.Add(new ProfileViewModel.CounterVM(UIStringFormatterHelper.FormatNumberOfSomething(owner.counters.docs, LocalizedStrings.GetString("OneDocument"), LocalizedStrings.GetString("TwoFourDocumentsFrm"), LocalizedStrings.GetString("FiveMoreDocumentsFrm"), false), owner.counters.docs));
            if (owner.counters.gifts > 0)
              this.Counters.Add(new ProfileViewModel.CounterVM(UIStringFormatterHelper.FormatNumberOfSomething(owner.counters.gifts, LocalizedStrings.GetString("OneGift"), LocalizedStrings.GetString("TwoFourGiftsFrm"), LocalizedStrings.GetString("FiveMoreGiftsFrm"), false), owner.counters.gifts));
            if (owner.counters.audios > 0)
              this.Counters.Add(new ProfileViewModel.CounterVM(UIStringFormatterHelper.FormatNumberOfSomething(owner.counters.audios, LocalizedStrings.GetString("OneAudio"), LocalizedStrings.GetString("TwoFourAudioFrm"), LocalizedStrings.GetString("FiveOrMoreAudioFrm"), false), owner.counters.audios));
            if (owner.counters.groups > 0)
              this.Counters.Add(new ProfileViewModel.CounterVM(UIStringFormatterHelper.FormatNumberOfSomething(owner.counters.groups, LocalizedStrings.GetString("OneGroup"), LocalizedStrings.GetString("TwoFourGroupsFrm"), LocalizedStrings.GetString("FiveOrMoreGroupsFrm"), false), owner.counters.groups));
            if (owner.counters.pages > 0)
              this.Counters.Add(new ProfileViewModel.CounterVM("pages", owner.counters.pages));
            if (owner.counters.videos > 0)
              this.Counters.Add(new ProfileViewModel.CounterVM(UIStringFormatterHelper.FormatNumberOfSomething(owner.counters.videos, LocalizedStrings.GetString("OneVideo"), LocalizedStrings.GetString("TwoFourVideosFrm"), LocalizedStrings.GetString("FiveOrMoreVideosFrm"), false), owner.counters.videos));
            if (owner.counters.user_photos > 0)
              this.Counters.Add(new ProfileViewModel.CounterVM(UIStringFormatterHelper.FormatNumberOfSomething(owner.counters.user_photos, LocalizedStrings.GetString("OneMark"), LocalizedStrings.GetString("TwoFourMarksFrm"), LocalizedStrings.GetString("FiveOrMoreMarksFrm"), false), owner.counters.user_photos));
          }
          this.CountersVisibility = this.Counters.Count > 0 ? (Visibility) 0 : (Visibility) 1;
          this.StatusVisibility = (Visibility) 1;
          this.UpdateProfilePhoto(((FrameworkElement) (Window.Current.Content as CustomFrame)).ActualWidth, 1.3);
          foreach (VKPhoto vkPhoto in temp.response.profile_photos.items)
            this.Photos.Add(vkPhoto);
          this.UserData.last_seen.Online = this.UserData.online;
          this.UserData.last_seen.OnlineApp = this.UserData.online_app;
          // ISSUE: reference to a compiler-generated method
          this.\u003C\u003En__FabricatedMethod16("Name");
          // ISSUE: reference to a compiler-generated method
          this.\u003C\u003En__FabricatedMethod16("Avatar");
          // ISSUE: reference to a compiler-generated method
          this.\u003C\u003En__FabricatedMethod16("VerifiedVisibility");
          // ISSUE: reference to a compiler-generated method
          this.\u003C\u003En__FabricatedMethod16("Description");
          // ISSUE: reference to a compiler-generated method
          this.\u003C\u003En__FabricatedMethod16("Activity");
          // ISSUE: reference to a compiler-generated method
          this.\u003C\u003En__FabricatedMethod16("CountersVisibility");
          // ISSUE: reference to a compiler-generated method
          this.\u003C\u003En__FabricatedMethod16("Status");
          // ISSUE: reference to a compiler-generated method
          this.\u003C\u003En__FabricatedMethod16("StatusVisibility");
          // ISSUE: reference to a compiler-generated method
          this.\u003C\u003En__FabricatedMethod16("InfoItems");
          // ISSUE: reference to a compiler-generated method
          this.\u003C\u003En__FabricatedMethod16("OnlineStatus");
        }
        if (temp.response.owner.deactivated == VKIsDeactivated.None)
        {
          foreach (VKWallPost vkWallPost in temp.response.items)
          {
            VKWallPost p = vkWallPost;
            VKBaseDataForGroupOrUser dataForGroupOrUser = (VKBaseDataForGroupOrUser) null;
            if (p.from_id != 0)
              dataForGroupOrUser = p.from_id >= 0 || temp.response.groups == null ? (VKBaseDataForGroupOrUser) temp.response.profiles.Find((Predicate<VKProfileBase>) (ow => ow.id == p.from_id)) : (VKBaseDataForGroupOrUser) temp.response.groups.Find((Predicate<VKGroup>) (ow => ow.id == -p.from_id));
            p.Owner = dataForGroupOrUser;
            if (p.copy_history != null)
            {
              for (int index = 0; index < p.copy_history.Count; ++index)
              {
                VKWallPost item = p.copy_history[index];
                if (item.owner_id < 0 && temp.response.groups != null)
                  item.Owner = (VKBaseDataForGroupOrUser) temp.response.groups.Find((Predicate<VKGroup>) (ow => ow.id == -item.owner_id));
                else
                  item.Owner = (VKBaseDataForGroupOrUser) temp.response.profiles.Find((Predicate<VKProfileBase>) (ow => ow.id == item.owner_id));
              }
            }
            this.Items.Add(p);
          }
        }
        await Task.Delay(1000);
        this._inLoading = false;
      }
    }

    public void UpdateProfilePhoto(double width, double ratio)
    {
      if (this.UserData.crop_photo == null)
        return;
      bool flag = true;
      double height1 = width / ratio;
      VKPhoto photo = this.UserData.crop_photo.photo;
      double num1 = photo.height > 0 ? (double) photo.width / (double) photo.height : 1.0;
      double width1 = width;
      double height2 = width / num1;
      if (num1 > ratio)
      {
        height2 = height1;
        width1 = height1 * num1;
        flag = false;
      }
      this.ProfileImageWidth = (double) (int) width1;
      this.ProfileImageHeight = (double) (int) height2;
      Rect croppingRectangle1 = this.UserData.crop_photo.crop.GetCroppingRectangle(width1, height2);
      Rect croppingRectangle2 = this.UserData.crop_photo.rect.GetCroppingRectangle(croppingRectangle1.Width, croppingRectangle1.Height);
      double num2 = croppingRectangle2.X + croppingRectangle2.Width / 2.0;
      double num3 = croppingRectangle2.Y + croppingRectangle2.Height;
      if (flag)
      {
        double num4 = croppingRectangle2.Height <= height1 ? 2.0 : 2.56;
        double num5 = num3 - croppingRectangle2.Height - croppingRectangle2.Height / num4;
        double val1 = height1 / 2.0 - num5;
        if (croppingRectangle2.Height > height1 && num5 - croppingRectangle2.Height / 2.0 >= 0.0)
          val1 = -croppingRectangle2.Y;
        double top = Math.Min(0.0, Math.Max(val1, height1 - height2));
        this.ProfileImageMargin = new Thickness(0.0, top, 0.0, 0.0);
        this.ProfileImageClipRect = new Rect(0.0, -(top + 1.0), width, height1 + 1.0);
      }
      else
      {
        double left = Math.Min(0.0, Math.Max(width / 2.0 - num2, width - width1));
        this.ProfileImageMargin = new Thickness(left, 0.0, 0.0, 0.0);
        this.ProfileImageClipRect = new Rect(-(left + 1.0), 0.0, width + 1.0, height1);
      }
      this.ProfileImageUrl = this.UserData.crop_photo.photo.photo_604;
      double headerHeight = (Window.Current.Content as CustomFrame).HeaderWithMenu.HeaderHeight;
      this.CoverImageHeight = height1;
      this.CoverImageOffset = this.CoverImageHeight - headerHeight;
      this.NotifyPropertyChanged("CoverImageHeight");
      this.NotifyPropertyChanged("ProfileImageClipRect");
      this.NotifyPropertyChanged("ProfileImageUrl");
      this.NotifyPropertyChanged("ProfileImageMargin");
      this.NotifyPropertyChanged("ProfileImageWidth");
      this.NotifyPropertyChanged("ProfileImageHeight");
      this.NotifyPropertyChanged("CoverImageOffset");
    }

    private string FormatSecondaryAction(VKGroup g)
    {
      if (g == null)
        return "";
      switch (g.member_status)
      {
        case VKGroupMembershipType.Member:
          switch (g.type)
          {
            case VKGroupType.Group:
              return LocalizedStrings.GetString("Group_Joined");
            case VKGroupType.Page:
              return LocalizedStrings.GetString("Group_Following");
            case VKGroupType.Event:
              return LocalizedStrings.GetString("Group_Attending");
            default:
              return "";
          }
        case VKGroupMembershipType.NotSure:
          return LocalizedStrings.GetString("Group_MayAttend");
        case VKGroupMembershipType.RequestSent:
          return LocalizedStrings.GetString("Group_RequestSent");
        default:
          return "";
      }
    }

    private string FormatSecondaryAction(VKProfileBase u)
    {
      if (u == null)
        return "";
      switch (u.friend_status)
      {
        case VKUsetMembershipType.RequestSent:
          return LocalizedStrings.GetString("Profile_YouAreFollowing");
        case VKUsetMembershipType.Friends:
          return LocalizedStrings.GetString("Profile_YouAreFriends");
        default:
          return "";
      }
    }

    public List<InfoListItem> InfoItems
    {
      get
      {
        List<InfoListItem> infoItems = new List<InfoListItem>();
        if (this.UserData == null)
          return infoItems;
        VKCounters counters = this.UserData.counters;
        if (!string.IsNullOrEmpty(this.UserData.activity))
          infoItems.Add(new InfoListItem()
          {
            IconUrl = "\uEC42",
            Text = this.UserData.activity
          });
        if (counters.friends > 0)
          infoItems.Add(new InfoListItem()
          {
            IconUrl = "\uE716",
            Text = this.ComposeInlinesForFriends()
          });
        if (counters.followers > 0)
          infoItems.Add(new InfoListItem()
          {
            IconUrl = "\uF081",
            Text = this.ComposeInlinesForFollowers()
          });
        if (!string.IsNullOrEmpty(this.UserData.bdate) && (long) this.UserData.id != Settings.Instance.auth.UserId)
        {
          InfoListItem infoListItem = new InfoListItem();
          infoListItem.IconUrl = "\uE1DC";
          string str = this.ComposeTextForBirthday(this.UserData.bdate);
          infoListItem.Text = str;
          int num = this.GetBirthday(this.UserData.bdate) > DateTime.MinValue ? 1 : 0;
          infoItems.Add(infoListItem);
        }
        if (this.UserData.city != null && this.UserData.city.id > 0 && (long) this.UserData.id != Settings.Instance.auth.UserId)
        {
          List<InfoListItem> infoListItemList = infoItems;
          InfoListItem infoListItem = new InfoListItem();
          infoListItem.IconUrl = "\uE80F";
          string str = string.Format("{0}: {1}", (object) LocalizedStrings.GetString("City"), (object) this.UserData.city.title);
          infoListItem.Text = str;
          infoListItemList.Add(infoListItem);
        }
        if (this.UserData.occupation != null)
        {
          InfoListItem infoListItem = new InfoListItem()
          {
            IconUrl = this.UserData.occupation.type == VKOccupation.OccupationType.work ? "\uE821" : "\uE7BE",
            Text = string.Format("{0}: {1}", this.UserData.occupation.type == VKOccupation.OccupationType.work ? (object) LocalizedStrings.GetString("OccupationType_Work") : (object) LocalizedStrings.GetString("ProfilePage_Info_Education"), (object) this.UserData.occupation.name)
          };
          if (this.UserData.occupation.type == VKOccupation.OccupationType.work)
            infoListItem.Text = string.Format("{0}: {1}", (object) LocalizedStrings.GetString("OccupationType_Work"), (object) this.UserData.occupation.name);
          else if (this.UserData.education != null)
          {
            int graduation = this.UserData.education.graduation;
            string str1;
            if (graduation <= 0)
              str1 = this.UserData.occupation.name;
            else
              str1 = string.Format("{0} '{1:00}", (object) this.UserData.occupation.name, (object) (graduation % 100));
            string str2 = str1;
            infoListItem.Text = string.Format("{0}: {1}", (object) LocalizedStrings.GetString("ProfilePage_Info_Education"), (object) str2);
          }
          infoItems.Add(infoListItem);
        }
        return infoItems;
      }
    }

    private string ComposeInlinesForFriends()
    {
      string str = UIStringFormatterHelper.FormatNumberOfSomething(this.UserData.counters.friends, LocalizedStrings.GetString("OneFriendFrm"), LocalizedStrings.GetString("TwoFourFriendsFrm"), LocalizedStrings.GetString("FiveFriendsFrm"));
      if (this.UserData.counters.mutual_friends > 0)
        str = str + " · " + UIStringFormatterHelper.FormatNumberOfSomething(this.UserData.counters.mutual_friends, LocalizedStrings.GetString("OneMutualFrm"), LocalizedStrings.GetString("TwoFourMutualFrm"), LocalizedStrings.GetString("FiveMutualFrm"));
      return str;
    }

    private string ComposeInlinesForFollowers()
    {
      return UIStringFormatterHelper.FormatNumberOfSomething(this.UserData.counters.followers, LocalizedStrings.GetString("OneFollowerFrm"), LocalizedStrings.GetString("TwoFourFollowersFrm"), LocalizedStrings.GetString("FiveFollowersFrm"));
    }

    private string ComposeTextForBirthday(string bdate)
    {
      string[] strArray = bdate.Split('.');
      if (strArray.Length < 2)
        return "";
      int num1 = int.Parse(strArray[0]);
      int month = int.Parse(strArray[1]);
      int num2 = 0;
      if (strArray.Length > 2)
        num2 = int.Parse(strArray[2]);
      string str = string.Format("{0} {1}", (object) num1, (object) UIStringFormatterHelper.GetOfMonthStr(month));
      if (num2 != 0)
        str += string.Format(" {0}", (object) num2);
      return string.Format("{0}: {1}", (object) LocalizedStrings.GetString("ProfilePage_Info_Birthday"), (object) str);
    }

    private DateTime GetBirthday(string bdate)
    {
      string[] strArray = bdate.Split('.');
      if (strArray.Length < 2)
        return DateTime.MinValue;
      int day = int.Parse(strArray[0]);
      DateTime birthday = new DateTime(DateTime.Now.Year, int.Parse(strArray[1]), day);
      if (DateTime.Now > birthday)
        birthday = birthday.AddYears(1);
      return birthday;
    }

    public bool CanSendGift
    {
      get
      {
        return this.UserData != null && this.Id != Settings.Instance.auth.UserId && !this.UserData.blacklisted && this.UserData.deactivated == VKIsDeactivated.None;
      }
    }

    public bool CanFaveUnfave
    {
      get
      {
        if (this.UserData == null || this.Id == Settings.Instance.auth.UserId)
          return false;
        return this.UserData.deactivated == VKIsDeactivated.None || this.UserData.is_favorite;
      }
    }

    public string OnlineStatus
    {
      get
      {
        return this.UserData == null ? string.Empty : this.UserData.last_seen.GetUserStatusString(this.UserData.sex);
      }
    }

    public class VKUserRequest : VKCountedItemsObject<VKWallPost>
    {
      public List<VKProfileBase> profiles { get; set; }

      public List<VKGroup> groups { get; set; }

      public VKProfileBase owner { get; set; }

      public VKCountedItemsObject<VKPhoto> profile_photos { get; set; }
    }

    public class CounterVM
    {
      public string Title { get; set; }

      public int Count { get; set; }

      public CounterVM(string title, int count)
      {
        this.Title = title;
        this.Count = count;
      }
    }
  }
}
