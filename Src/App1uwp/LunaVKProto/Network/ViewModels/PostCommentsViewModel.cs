// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.ViewModels.PostCommentsViewModel
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Library;
using App1uwp.Network.DataObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

#nullable disable
namespace App1uwp.Network.ViewModels
{
  public class PostCommentsViewModel
  {
    private bool InLoading;
    private int offset;
    private ulong _postId;
    private long _ownerId;

    public ObservableCollection<VKComment> Items { get; private set; }

    public VKBaseDataForPostOrNews WallPostData { get; private set; }

    public Action LoadMore { get; private set; }

    public Action Refresh { get; private set; }

    public PostCommentsViewModel(ulong postId, long ownerId, VKBaseDataForPostOrNews postData = null)
    {
      this.Items = new ObservableCollection<VKComment>();
      this._postId = postId;
      this._ownerId = ownerId;
      this.WallPostData = postData;
    }

    public async void LoadData(bool reload = false, Action<bool> calback = null)
    {
      this.InLoading = true;
      if (reload)
        this.offset = 0;
      VKBaseDataForPostOrNews wallPostData = this.WallPostData;
      string temp = "";
      if (this.WallPostData == null)
      {
        temp = "var wallPost = API.wall.getById({{\"posts\":\"{1}_{0}\"}})[0];";
        // ISSUE: explicit reference operation
        // ISSUE: reference to a compiler-generated field
        (^this).\u003Ctemp\u003E5__b += "var likesAll = API.likes.getList({{\"item_id\":{0}, \"owner_id\":{1}, \"count\":20, type:wallPost.post_type}});";
      }
      else
        temp = "var likesAll = API.likes.getList({{\"item_id\":{0}, \"owner_id\":{1}, \"count\":20, type:\"post\"}});";
      ref PostCommentsViewModel.\u003CLoadData\u003Ed__a local = this;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      local.\u003Ctemp\u003E5__b = local.\u003Ctemp\u003E5__b + "var comments = API.wall.getComments({{\"post_id\":\"{0}\", \"owner_id\":\"{1}\", \"offset\":" + (object) this.offset + ", \"count\":20, \"need_likes\":1, \"sort\":\"desc\", \"allow_group_comments\":1}});";
      // ISSUE: explicit reference operation
      // ISSUE: reference to a compiler-generated field
      (^this).\u003Ctemp\u003E5__b += "var datUsersNames = comments.items@.reply_to_user + comments.items@.from_id;var users2 = API.users.get({{\"user_ids\":datUsersNames, \"fields\":\"first_name_dat,last_name_dat\"}});var userOrGroupIds = likesAll.items;";
      if (this.WallPostData == null)
      {
        // ISSUE: explicit reference operation
        // ISSUE: reference to a compiler-generated field
        (^this).\u003Ctemp\u003E5__b += "userOrGroupIds = userOrGroupIds + wallPost@.from_id + wallPost@.to_id + wallPost@.signer_id + wallPost[0].copy_history@.owner_id + wallPost[0].copy_history@.from_id;";
      }
      // ISSUE: explicit reference operation
      // ISSUE: reference to a compiler-generated field
      (^this).\u003Ctemp\u003E5__b += "userOrGroupIds = userOrGroupIds + comments.items@.from_id;var userIds = [];var groupIds = [];var i = 0;var length = userOrGroupIds.length;while (i < length){{  var id = parseInt(userOrGroupIds[i]); if (id > 0) {{ if (userIds.length > 0) {{ userIds = userIds + \",\"; }} userIds = userIds + id; }}  else if (id < 0)  {{ id = -id;    if (groupIds.length > 0){{groupIds = groupIds + \",\";}}groupIds = groupIds + id;  }}     i = i + 1;}}if ({1} < 0){{  if (groupIds.length > 0) groupIds = groupIds + \",\";  groupIds = groupIds + ({1} * -1);}}var users = API.users.get({{\"user_ids\":userIds, \"fields\":\"sex,photo_50,online,online_mobile\" }});var groups = API.groups.getById({{\"group_ids\":groupIds}});";
      // ISSUE: explicit reference operation
      // ISSUE: reference to a compiler-generated field
      (^this).\u003Ctemp\u003E5__b += "return {{LikesAll:likesAll, comments:comments, Users:users, Groups:groups, Users2:users2";
      if (this.WallPostData == null)
      {
        // ISSUE: explicit reference operation
        // ISSUE: reference to a compiler-generated field
        (^this).\u003Ctemp\u003E5__b += ", WallPost:wallPost";
      }
      // ISSUE: explicit reference operation
      // ISSUE: reference to a compiler-generated field
      (^this).\u003Ctemp\u003E5__b += "}};";
      string code = string.Format(temp, (object) this._postId.ToString(), (object) this._ownerId.ToString());
      this.offset += 20;
      VKResponse<PostCommentsViewModel.GetWallPostResponseData> temp2 = await RequestsDispatcher.Execute<PostCommentsViewModel.GetWallPostResponseData>(code);
      if (temp2 == null)
      {
        if (calback == null)
          return;
        calback(false);
      }
      else
      {
        if (this.WallPostData == null)
          this.WallPostData = (VKBaseDataForPostOrNews) temp2.response.WallPost;
        UsersService.Instance.SetCachedUsers((IEnumerable<VKBaseDataForGroupOrUser>) temp2.response.Users);
        UsersService.Instance.SetCachedUsers((IEnumerable<VKBaseDataForGroupOrUser>) temp2.response.Groups);
        foreach (VKComment vkComment in temp2.response.comments.items)
        {
          VKComment c = vkComment;
          if (c.reply_to_user != 0L)
          {
            if (c.reply_to_user < 0L)
            {
              VKGroup vkGroup = temp2.response.Groups.Find((Predicate<VKGroup>) (group => (long) group.id == -c.reply_to_user));
              c._replyToUserDat = vkGroup.Title;
            }
            else
            {
              VKProfileBase vkProfileBase = temp2.response.Users2.Find((Predicate<VKProfileBase>) (user => (long) user.id == c.reply_to_user));
              c._replyToUserDat = vkProfileBase.first_name_dat + " " + vkProfileBase.last_name_dat;
            }
          }
          VKBaseDataForGroupOrUser dataForGroupOrUser = c.from_id >= 0L ? (VKBaseDataForGroupOrUser) temp2.response.Users.Find((Predicate<VKProfileBase>) (pro => (long) pro.id == c.from_id)) : (VKBaseDataForGroupOrUser) temp2.response.Groups.Find((Predicate<VKGroup>) (pro => (long) pro.id == -c.from_id));
          c.User = dataForGroupOrUser;
          this.Items.Insert(0, c);
        }
        if (calback != null)
          calback(true);
        this.InLoading = false;
      }
    }

    public class GetWallPostResponseData
    {
      public VKCountedItemsObject<VKComment> comments { get; set; }

      public List<VKProfileBase> Users { get; set; }

      public List<VKGroup> Groups { get; set; }

      public VKWallPost WallPost { get; set; }

      public List<VKProfileBase> Users2 { get; set; }
    }
  }
}
