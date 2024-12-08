// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.ViewModels.NotificationsViewModel
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Library;
using App1uwp.Network.DataObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

#nullable disable
namespace App1uwp.Network.ViewModels
{
  public class NotificationsViewModel
  {
    private bool InLoading;
    private string nextFrom = "";

    public ObservableCollection<VKNotification> Notifications { get; private set; }

    public Action LoadMore { get; private set; }

    public Action Refresh { get; private set; }

    public string Title => LocalizedStrings.GetString("Menu_Notifications/Title");

    public NotificationsViewModel()
    {
      this.Notifications = new ObservableCollection<VKNotification>();
      this.LoadMore = (Action) (() =>
      {
        if (this.InLoading)
          return;
        this.LoadData();
      });
      this.Refresh = (Action) (() => this.LoadData(true));
    }

    public async void LoadData(bool reload = false, Action<bool> calback = null)
    {
      this.InLoading = true;
      if (reload)
      {
        this.nextFrom = "";
        this.Notifications.Clear();
      }
      Dictionary<string, string> parameters = new Dictionary<string, string>();
      if (!string.IsNullOrEmpty(this.nextFrom))
      {
        parameters["start_from"] = this.nextFrom;
        parameters["count"] = "30";
      }
      else
        parameters["count"] = "10";
      VKResponse<NotificationsViewModel.NotificationData> temp = await RequestsDispatcher.GetResponse<NotificationsViewModel.NotificationData>("notifications.get", parameters);
      if (temp == null)
      {
        if (calback != null)
          calback(false);
        this.InLoading = false;
      }
      else
      {
        UsersService.Instance.SetCachedUsers((IEnumerable<VKBaseDataForGroupOrUser>) temp.response.groups);
        UsersService.Instance.SetCachedUsers((IEnumerable<VKBaseDataForGroupOrUser>) temp.response.profiles);
        foreach (VKNotification vkNotification in temp.response.items)
          this.Notifications.Add(vkNotification);
        if (MenuViewModel.Instance.NotificationsItem.Count <= 0)
          return;
        await Task.Delay(1000);
        parameters = new Dictionary<string, string>();
        VKResponse<int> response = await RequestsDispatcher.GetResponse<int>("notifications.markAsViewed", parameters);
      }
    }

    public class NotificationData
    {
      public List<VKNotification> items { get; set; }

      public List<VKProfileBase> profiles { get; set; }

      public List<VKGroup> groups { get; set; }

      public string next_from { get; set; }
    }
  }
}
