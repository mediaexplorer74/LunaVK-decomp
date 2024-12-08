// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.ViewModels.FriendsViewModel
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.DataObjects;
using App1uwp.Network.DataVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

#nullable disable
namespace App1uwp.Network.ViewModels
{
  public class FriendsViewModel : ViewModelBase
  {
    private bool InLoading;
    private int offset;
    private uint maximum;

    public ObservableCollection<VKFriendVM> Friends { get; private set; }

    public ObservableCollection<VKFriendVM> Requests { get; private set; }

    public bool RequestsVisible => this.Requests.Count > 0;

    public Action LoadMore { get; private set; }

    public Action Refresh { get; private set; }

    public string Title => LocalizedStrings.GetString("Menu_Friends/Title");

    public FriendsViewModel()
    {
      this.Friends = new ObservableCollection<VKFriendVM>();
      this.Requests = new ObservableCollection<VKFriendVM>();
      this.LoadMore = (Action) (() =>
      {
        if (this.InLoading || (long) this.Friends.Count >= (long) this.maximum)
          return;
        this.LoadData();
      });
      this.Refresh = (Action) (() => this.LoadData(true));
    }

    public async void LoadData(bool reload = false, Action<bool> calback = null)
    {
      this.InLoading = true;
      if (reload)
        this.offset = 0;
      string code = "var requests=[]; var users=[]; var friends=API.friends.get({\"fields\":\"online,online_mobile,photo_100,verified,last_seen,occupation\",\"order\":\"hints\",\"count\":20,\"offset\":" + (object) this.offset + "});";
      if (reload)
      {
        // ISSUE: explicit reference operation
        // ISSUE: reference to a compiler-generated field
        (^this).\u003Ccode\u003E5__5 += "requests = API.friends.getRequests();";
        // ISSUE: explicit reference operation
        // ISSUE: reference to a compiler-generated field
        (^this).\u003Ccode\u003E5__5 += "if (requests.items.length>0){users=API.users.get({user_ids:requests.items,\"fields\":\"city,country,photo_100,occupation\"});}";
      }
      // ISSUE: explicit reference operation
      // ISSUE: reference to a compiler-generated field
      (^this).\u003Ccode\u003E5__5 += "return {friends:friends,requests:users};";
      this.offset += 20;
      VKResponse<VKFriendsGetObject> temp = await RequestsDispatcher.Execute<VKFriendsGetObject>(code);
      if (temp.error.error_code != VKErrors.None)
      {
        if (calback != null)
          calback(false);
        this.InLoading = false;
      }
      else
      {
        if (reload)
        {
          this.Friends.Clear();
          this.Requests.Clear();
        }
        if (reload)
          this.maximum = temp.response.friends.count;
        foreach (VKFriendVM vkFriendVm in temp.response.friends.items)
          this.Friends.Add(vkFriendVm);
        if (temp.response.requests != null && temp.response.requests.Count > 0)
        {
          foreach (VKFriendVM request in temp.response.requests)
            this.Requests.Add(request);
          // ISSUE: reference to a compiler-generated method
          this.\u003C\u003En__FabricatedMethod9("RequestsVisible");
        }
        if (calback != null)
          calback(true);
        await Task.Delay(1000);
        this.InLoading = false;
      }
    }

    public async Task AddFriend(VKFriendVM user)
    {
      VKResponse<int> response = await RequestsDispatcher.GetResponse<int>("friends.add", new Dictionary<string, string>()
      {
        ["user_id"] = user.id.ToString()
      });
      this.Requests.Remove(user);
      // ISSUE: reference to a compiler-generated method
      this.\u003C\u003En__FabricatedMethod9("RequestsVisible");
    }

    public async Task DeleteFriend(VKFriendVM user)
    {
      VKResponse<FriendsViewModel.DeleteFriendAnswer> response = await RequestsDispatcher.GetResponse<FriendsViewModel.DeleteFriendAnswer>("friends.delete", new Dictionary<string, string>()
      {
        ["user_id"] = user.id.ToString()
      });
      this.Requests.Remove(user);
      // ISSUE: reference to a compiler-generated method
      this.\u003C\u003En__FabricatedMethod9("RequestsVisible");
    }

    public class DeleteFriendAnswer
    {
      public int success { get; set; }

      public int in_request_deleted { get; set; }

      public int friend_deleted { get; set; }

      public int out_request_deleted { get; set; }

      public int suggestion_deleted { get; set; }
    }
  }
}
