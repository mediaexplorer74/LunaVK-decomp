// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.ViewModels.GroupsViewModel
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.DataObjects;
using App1uwp.Network.DataVM;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

#nullable disable
namespace App1uwp.Network.ViewModels
{
  public class GroupsViewModel : ViewModelBase
  {
    private bool InLoading;
    private int offset;
    private uint maximum;

    public ObservableCollection<VKGroupVM> Groups { get; private set; }

    public ObservableCollection<VKGroupVM> Invites { get; private set; }

    public Action LoadMore { get; private set; }

    public Action Refresh { get; private set; }

    public bool InvitationsVisible { get; private set; }

    public bool NeedRefresh { get; set; }

    public string Title => LocalizedStrings.GetString("Menu_Communities/Title");

    public GroupsViewModel()
    {
      this.Groups = new ObservableCollection<VKGroupVM>();
      this.Invites = new ObservableCollection<VKGroupVM>();
      this.LoadMore = (Action) (() =>
      {
        if (this.InLoading || (long) this.Groups.Count >= (long) this.maximum)
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
      string code = "var inviters=[];var invitations=[];";
      ref GroupsViewModel.\u003CLoadData\u003Ed__8 local = this;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      local.\u003Ccode\u003E5__9 = local.\u003Ccode\u003E5__9 + "var groups = API.groups.get({\"extended\":1,\"fields\":\"name,verified,photo_100,activity,members_count\",\"count\":20,\"offset\":" + (object) this.offset + "});";
      if (reload)
      {
        // ISSUE: explicit reference operation
        // ISSUE: reference to a compiler-generated field
        (^this).\u003Ccode\u003E5__9 += "invitations = API.groups.getInvites({fields:\"members_count\"});";
        // ISSUE: explicit reference operation
        // ISSUE: reference to a compiler-generated field
        (^this).\u003Ccode\u003E5__9 += "if (invitations.items.length>0){inviters=API.users.get({user_ids:invitations.items@.invited_by,fields:\"sex\"});return {invitations:invitations,inviters:inviters,groups:groups};}";
      }
      // ISSUE: explicit reference operation
      // ISSUE: reference to a compiler-generated field
      (^this).\u003Ccode\u003E5__9 += "return {groups:groups};";
      this.offset += 20;
      VKResponse<VKGroupsGetObject> temp = await RequestsDispatcher.Execute<VKGroupsGetObject>(code);
      if (temp == null)
      {
        if (calback != null)
          calback(false);
        this.InLoading = false;
      }
      else
      {
        if (reload)
        {
          this.Groups.Clear();
          this.Invites.Clear();
        }
        if (reload)
          this.maximum = temp.response.groups.count;
        foreach (VKGroupVM vkGroupVm in temp.response.groups.items)
          this.Groups.Add(vkGroupVm);
        if (temp.response.invitations != null && temp.response.invitations.count > 0U)
        {
          this.InvitationsVisible = true;
          // ISSUE: reference to a compiler-generated method
          this.\u003C\u003En__FabricatedMethodd("InvitationsVisible");
          foreach (VKGroupVM vkGroupVm in temp.response.invitations.items)
          {
            VKGroupVM group = vkGroupVm;
            VKProfileBase vkProfileBase = temp.response.inviters.Find((Predicate<VKProfileBase>) (u => u.id == group.invited_by));
            group.Inviter = vkProfileBase.Title;
            this.Invites.Add(group);
          }
        }
        if (calback != null)
          calback(true);
        await Task.Delay(1000);
        this.InLoading = false;
      }
    }
  }
}
