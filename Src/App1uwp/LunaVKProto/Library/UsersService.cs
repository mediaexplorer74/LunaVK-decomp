// Decompiled with JetBrains decompiler
// Type: App1uwp.Library.UsersService
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network;
using App1uwp.Network.DataObjects;
using App1uwp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable
namespace App1uwp.Library
{
  public class UsersService
  {
    private readonly object _lockObj = new object();
    private Dictionary<long, VKBaseDataForGroupOrUser> _usersCache = new Dictionary<long, VKBaseDataForGroupOrUser>();
    private static UsersService _instance;

    public static UsersService Instance
    {
      get => UsersService._instance ?? (UsersService._instance = new UsersService());
    }

    public void SetCachedUsers(IEnumerable<VKBaseDataForGroupOrUser> users)
    {
      if (users == null)
        return;
      lock (this._lockObj)
      {
        foreach (VKBaseDataForGroupOrUser user in users)
          this._usersCache[(long) user.id] = user;
      }
    }

    public VKBaseDataForGroupOrUser GetCachedUser(long userId)
    {
      lock (this._lockObj)
      {
        if (userId < 0L)
          userId = -userId;
        if (this._usersCache.ContainsKey(userId))
          return this._usersCache[userId];
      }
      return (VKBaseDataForGroupOrUser) null;
    }

    public async Task GetUsers(List<int> userIds)
    {
      List<int> to_get = new List<int>();
      foreach (int userId in userIds)
      {
        if (!this._usersCache.ContainsKey(userId < 0 ? (long) -userId : (long) userId))
          to_get.Add(userId);
      }
      if (to_get.Count == 0)
        return;
      VKResponse<UsersAndGroups> temp = await RequestsDispatcher.Execute<UsersAndGroups>(string.Format("var userIds = \"{0}\";var groupIds = \"{1}\";var users = [];var groups = [];\r\n\r\nif (userIds.length > 0)\r\n{{\r\n   users = API.users.get({{\"user_ids\":userIds, \"fields\":\"first_name,last_name,photo_50,photo_200\" }});\r\n}}\r\n\r\nif (groupIds.length > 0)\r\n{{\r\n   groups = API.groups.getById({{\"group_ids\":groupIds, \"fields\":\"photo_200,photo_50\"}});\r\n}}\r\n\r\nreturn {{\"users\":users, \"groups\":groups}};", (object) to_get.FindAll((Predicate<int>) (uid => uid > 0)).GetCommaSeparated(), (object) to_get.Where<int>((Func<int, bool>) (uid => uid < 0)).Select<int, int>((Func<int, int>) (uid => -uid)).ToList<int>().GetCommaSeparated(), (object) true));
      this.SetCachedUsers((IEnumerable<VKBaseDataForGroupOrUser>) temp.response.groups);
      this.SetCachedUsers((IEnumerable<VKBaseDataForGroupOrUser>) temp.response.users);
    }
  }
}
