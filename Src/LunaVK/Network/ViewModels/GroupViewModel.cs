// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.ViewModels.GroupViewModel
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
using App1uwp.Library;
using App1uwp.Network.DataObjects;
using App1uwp.Network.Enums;
using App1uwp.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml;

#nullable disable
namespace App1uwp.Network.ViewModels
{
  public class GroupViewModel : ViewModelBase
  {
    private bool _inLoading;
    private int offset;
    private uint _maximum;
    private VKGroup _groupData;

    public long Id { get; private set; }

    public ObservableCollection<GroupViewModel.CounterVM> Counters { get; private set; }

    public ObservableCollection<VKWallPost> Items { get; private set; }

    public Action LoadMore { get; private set; }

    public Action Refresh { get; private set; }

    public List<GroupViewModel.InfoLink> Links { get; private set; }

    public List<GroupViewModel.InfoLink> Contacts { get; private set; }

    public Action<ProfileLoadingStatus> LoadingStatusUpdated { get; set; }

    public Visibility VerifiedVisibility { get; private set; }

    public Visibility CoverImageVisibility { get; private set; }

    public Visibility DataVisibility { get; private set; }

    public string ProfileImageUrl { get; private set; }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public string CoverImageUrl { get; private set; }

    public string Avatar { get; private set; }

    public string Activity { get; private set; }

    public Visibility MsgButtonVisibility { get; private set; }

    public Visibility CountersVisibility { get; private set; }

    public Visibility LinksVisibility { get; private set; }

    public Visibility ContactsVisibility { get; private set; }

    public string TextButtonSecondaryAction { get; private set; }

    public string Status { get; private set; }

    public Visibility StatusVisibility { get; private set; }

    public Visibility SecondaryButtonVisibility { get; private set; }

    public double CoverImageHeight { get; private set; }

    public GroupViewModel(long id)
    {
      this.Id = id;
      this.Counters = new ObservableCollection<GroupViewModel.CounterVM>();
      this.Items = new ObservableCollection<VKWallPost>();
      this.LoadMore = (Action) (() =>
      {
        if (this._inLoading && (long) this.Items.Count >= (long) this._maximum)
          return;
        this.LoadData();
      });
      this.Refresh = (Action) (() => this.LoadData(true));
    }

    public VKGroup GroupData => this._groupData;

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
      VKResponse<GroupViewModel.VKGroupRequest> temp = (VKResponse<GroupViewModel.VKGroupRequest>) null;
      if (reload)
      {
        string code = "var owner = API.groups.getById({group_id:\"" + (object) -this.Id + "\",fields:\"description,photo_100,verified,activity,cover,market,counters,can_message,member_status,status,members_count,site,screen_name,links,contacts,can_post,is_subscribed,is_favorite\"});";
        ref GroupViewModel.\u003CLoadData\u003Ed__13 local = this;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        local.\u003Ccode\u003E5__15 = local.\u003Ccode\u003E5__15 + "var wall = API.wall.get({owner_id:\"" + (object) this.Id + "\",count:20, extended:\"1\",fields:\"photo_100,verified,is_pinned\"});";
        // ISSUE: explicit reference operation
        // ISSUE: reference to a compiler-generated field
        (^this).\u003Ccode\u003E5__15 += "return {owner:owner[0],profiles:wall.profiles,groups:wall.groups,items:wall.items};";
        temp = await RequestsDispatcher.Execute<GroupViewModel.VKGroupRequest>(code, (Func<string, string>) (jsonStr => RequestsDispatcher.FixArrayToObject(jsonStr, "counters")));
      }
      else
        temp = await RequestsDispatcher.GetResponse<GroupViewModel.VKGroupRequest>("wall.get", new Dictionary<string, string>()
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
        UsersService.Instance.SetCachedUsers((IEnumerable<VKBaseDataForGroupOrUser>) temp.response.groups);
        UsersService.Instance.SetCachedUsers((IEnumerable<VKBaseDataForGroupOrUser>) temp.response.profiles);
        if (reload)
        {
          this._maximum = temp.response.count;
          VKGroup owner = temp.response.owner;
          this._groupData = owner;
          if (this.LoadingStatusUpdated != null)
            this.LoadingStatusUpdated(ProfileLoadingStatus.Loaded);
          this.Name = owner.name;
          this.Avatar = owner.photo_100;
          this.VerifiedVisibility = owner.verified ? (Visibility) 0 : (Visibility) 1;
          this.Description = owner.description;
          this.Activity = owner.activity;
          this.MsgButtonVisibility = owner.can_message ? (Visibility) 0 : (Visibility) 1;
          this.TextButtonSecondaryAction = this.FormatSecondaryAction(owner);
          if (owner.cover != null && owner.cover.enabled)
          {
            this.CoverImageUrl = owner.cover.CurrentImage;
            this.CoverImageVisibility = (Visibility) 0;
            this.CoverImageHeight = ((FrameworkElement) (Window.Current.Content as CustomFrame)).ActualWidth * ((double) owner.cover.images[1].height / (double) owner.cover.images[1].width);
          }
          if (owner.counters != null)
          {
            if (owner.counters.albums > 0)
              this.Counters.Add(new GroupViewModel.CounterVM("albums", owner.counters.albums));
            if (owner.counters.topics > 0)
              this.Counters.Add(new GroupViewModel.CounterVM("topics", owner.counters.topics));
            if (owner.counters.photos > 0)
              this.Counters.Add(new GroupViewModel.CounterVM("photos", owner.counters.photos));
            if (owner.counters.docs > 0)
              this.Counters.Add(new GroupViewModel.CounterVM("docs", owner.counters.docs));
          }
          if (owner.links != null)
          {
            this.LinksVisibility = owner.links.Count > 0 ? (Visibility) 0 : (Visibility) 1;
            // ISSUE: reference to a compiler-generated method
            this.\u003C\u003En__FabricatedMethod19("LinksVisibility");
            this.Links = new List<GroupViewModel.InfoLink>();
            foreach (VKGroupLink link in owner.links)
              this.Links.Add(new GroupViewModel.InfoLink()
              {
                Title = link.name,
                SubTitle = link.desc,
                Img = link.photo_50
              });
          }
          if (owner.contacts != null)
          {
            this.Contacts = new List<GroupViewModel.InfoLink>();
            foreach (VKGroupContact contact in owner.contacts)
            {
              VKBaseDataForGroupOrUser dataForGroupOrUser = contact.user_id >= 0 ? UsersService.Instance.GetCachedUser((long) contact.user_id) : UsersService.Instance.GetCachedUser((long) -contact.user_id);
              GroupViewModel.InfoLink infoLink = new GroupViewModel.InfoLink();
              if (dataForGroupOrUser != null)
              {
                infoLink.Title = dataForGroupOrUser.Title;
                infoLink.Img = dataForGroupOrUser.photo_50;
              }
              else
              {
                infoLink.Title = "";
                infoLink.Img = (string) null;
              }
              infoLink.SubTitle = contact.desc;
              this.Contacts.Add(infoLink);
            }
            this.ContactsVisibility = this.Contacts.Count > 0 ? (Visibility) 0 : (Visibility) 1;
            // ISSUE: reference to a compiler-generated method
            this.\u003C\u003En__FabricatedMethod19("ContactsVisibility");
          }
          this.CountersVisibility = this.Counters.Count > 0 ? (Visibility) 0 : (Visibility) 1;
          this.Status = owner.status;
          this.StatusVisibility = string.IsNullOrEmpty(owner.status) ? (Visibility) 1 : (Visibility) 0;
          // ISSUE: reference to a compiler-generated method
          this.\u003C\u003En__FabricatedMethod19("Name");
          // ISSUE: reference to a compiler-generated method
          this.\u003C\u003En__FabricatedMethod19("Avatar");
          // ISSUE: reference to a compiler-generated method
          this.\u003C\u003En__FabricatedMethod19("VerifiedVisibility");
          // ISSUE: reference to a compiler-generated method
          this.\u003C\u003En__FabricatedMethod19("Description");
          // ISSUE: reference to a compiler-generated method
          this.\u003C\u003En__FabricatedMethod19("Activity");
          // ISSUE: reference to a compiler-generated method
          this.\u003C\u003En__FabricatedMethod19("MsgButtonVisibility");
          // ISSUE: reference to a compiler-generated method
          this.\u003C\u003En__FabricatedMethod19("CountersVisibility");
          // ISSUE: reference to a compiler-generated method
          this.\u003C\u003En__FabricatedMethod19("TextButtonSecondaryAction");
          // ISSUE: reference to a compiler-generated method
          this.\u003C\u003En__FabricatedMethod19("Status");
          // ISSUE: reference to a compiler-generated method
          this.\u003C\u003En__FabricatedMethod19("StatusVisibility");
          // ISSUE: reference to a compiler-generated method
          this.\u003C\u003En__FabricatedMethod19("CoverImageUrl");
          // ISSUE: reference to a compiler-generated method
          this.\u003C\u003En__FabricatedMethod19("CoverImageVisibility");
          // ISSUE: reference to a compiler-generated method
          this.\u003C\u003En__FabricatedMethod19("SecondaryButtonVisibility");
          // ISSUE: reference to a compiler-generated method
          this.\u003C\u003En__FabricatedMethod19("CoverImageHeight");
          // ISSUE: reference to a compiler-generated method
          this.\u003C\u003En__FabricatedMethod19("InfoItems");
          // ISSUE: reference to a compiler-generated method
          this.\u003C\u003En__FabricatedMethod19("Links");
          // ISSUE: reference to a compiler-generated method
          this.\u003C\u003En__FabricatedMethod19("Contacts");
        }
        foreach (VKWallPost vkWallPost in temp.response.items)
        {
          VKWallPost p = vkWallPost;
          VKBaseDataForGroupOrUser dataForGroupOrUser = p.from_id >= 0 || temp.response.groups == null ? (VKBaseDataForGroupOrUser) temp.response.profiles.Find((Predicate<VKProfileBase>) (ow => ow.id == p.from_id)) : (VKBaseDataForGroupOrUser) temp.response.groups.Find((Predicate<VKGroup>) (ow => ow.id == -p.from_id));
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
        await Task.Delay(1000);
        this._inLoading = false;
      }
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

    public List<InfoListItem> InfoItems
    {
      get
      {
        List<InfoListItem> infoItems = new List<InfoListItem>();
        if (this._groupData == null)
          return infoItems;
        if (!string.IsNullOrEmpty(this._groupData.status))
          infoItems.Add(new InfoListItem()
          {
            IconUrl = "\uEC42",
            Text = this._groupData.status
          });
        if (this._groupData.members_count > 0)
          infoItems.Add(new InfoListItem()
          {
            IconUrl = "\uE77B",
            Text = this.ComposeInlinesForMembers()
          });
        if (!string.IsNullOrEmpty(this._groupData.description))
          infoItems.Add(new InfoListItem()
          {
            IconUrl = "\uE71D",
            Text = this._groupData.description
          });
        if (!string.IsNullOrEmpty(this._groupData.screen_name))
          infoItems.Add(new InfoListItem()
          {
            IconUrl = "\uE71B",
            Text = this._groupData.screen_name
          });
        if (!string.IsNullOrEmpty(this._groupData.site))
          infoItems.Add(new InfoListItem()
          {
            IconUrl = "\uE774",
            Text = this._groupData.site
          });
        return infoItems;
      }
    }

    private string ComposeInlinesForMembers()
    {
      bool flag = this._groupData.type == VKGroupType.Page;
      return UIStringFormatterHelper.FormatNumberOfSomething(this._groupData.members_count, flag ? LocalizedStrings.GetString("OneSubscriberFrm") : LocalizedStrings.GetString("OneMemberFrm"), flag ? LocalizedStrings.GetString("TwoFourSubscribersFrm") : LocalizedStrings.GetString("TwoFourMembersFrm"), flag ? LocalizedStrings.GetString("FiveSubscribersFrm") : LocalizedStrings.GetString("FiveMembersFrm"));
    }

    public async void SubscribeUnsubscribe(Action<bool> callback = null)
    {
      if (this._groupData == null)
        return;
      VKResponse<int> temp = await RequestsDispatcher.GetResponse<int>(this._groupData.is_subscribed ? "wall.unsubscribe" : "wall.subscribe", new Dictionary<string, string>()
      {
        ["owner_id"] = this.Id.ToString()
      });
      if (temp != null)
      {
        if (callback == null)
          return;
        callback(true);
      }
      else
      {
        if (callback == null)
          return;
        callback(false);
      }
    }

    public async void FaveUnfave(Action<bool> callback = null)
    {
      if (this._groupData == null)
        return;
      VKResponse<int> temp = await RequestsDispatcher.GetResponse<int>(this._groupData.is_favorite ? "fave.removeGroup" : "fave.addGroup", new Dictionary<string, string>()
      {
        ["group_id"] = (-this.Id).ToString()
      });
      if (temp != null)
      {
        if (callback == null)
          return;
        callback(true);
      }
      else
      {
        if (callback == null)
          return;
        callback(false);
      }
    }

    public async void JoinLeave(Action<bool> callback = null)
    {
      if (this._groupData == null)
        return;
      VKResponse<int> temp = await RequestsDispatcher.GetResponse<int>(this._groupData.is_member ? "groups.leave" : "groups.join", new Dictionary<string, string>()
      {
        ["group_id"] = (-this.Id).ToString()
      });
      if (temp != null)
      {
        if (callback == null)
          return;
        callback(true);
      }
      else
      {
        if (callback == null)
          return;
        callback(false);
      }
    }

    public bool CanFaveUnfave
    {
      get
      {
        return this._groupData != null && this._groupData.is_closed != VKGroupIsClosed.Private && this._groupData.deactivated == VKIsDeactivated.None;
      }
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

    public class VKGroupRequest : VKCountedItemsObject<VKWallPost>
    {
      public List<VKProfileBase> profiles { get; set; }

      public List<VKGroup> groups { get; set; }

      public VKGroup owner { get; set; }
    }

    public class InfoLink
    {
      public string Title { get; set; }

      public string SubTitle { get; set; }

      public string Img { get; set; }
    }
  }
}
