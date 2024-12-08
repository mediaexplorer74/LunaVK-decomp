// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.ViewModels.DialogHistoryViewModel
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Library;
using App1uwp.Network.DataObjects;
using App1uwp.Network.DataVM;
using App1uwp.Network.Enums;
using App1uwp.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

#nullable disable
namespace App1uwp.Network.ViewModels
{
  public class DialogHistoryViewModel : ViewModelBase, ISupportLoadMore
  {
    private uint _maximum;
    public int _userId;
    public int _chatId;
    private string _uiTitle = string.Empty;
    private string _uiSubtitle = string.Empty;
    private DateTime _lastTimeUserIsTypingWasCalled = DateTime.MinValue;

    public ObservableCollection<VKMessageVM> Items { get; private set; }

    public ConversationAvatarViewModel ConversationAvatarVM { get; set; }

    public string UITitle
    {
      get => this._uiTitle;
      set
      {
        if (this._uiTitle == value)
          return;
        this._uiTitle = value;
        this.NotifyPropertyChanged(nameof (UITitle));
      }
    }

    public bool AreNotificationsDisabled { get; private set; }

    public string UISubtitle
    {
      get => this._uiSubtitle;
      set
      {
        if (this._uiSubtitle == value)
          return;
        this._uiSubtitle = value;
        this.NotifyPropertyChanged(nameof (UISubtitle));
      }
    }

    public ObservableCollection<IOutboundAttachment> Attachments { get; set; }

    public bool HasMoreItems => (long) this.Items.Count < (long) this._maximum;

    public DialogHistoryViewModel(int UserId, int ChatId)
    {
      this._userId = UserId;
      this._chatId = ChatId;
      VKDialog vkDialog = (VKDialog) DialogsViewModel.Instance.Dialogs.FirstOrDefault<VKDialogVM>((Func<VKDialogVM, bool>) (d => d.message.user_id == this._userId && d.message.chat_id == this._chatId));
      if (vkDialog != null)
        this.UITitle = vkDialog.message.title;
      this.Items = new ObservableCollection<VKMessageVM>();
      this.Attachments = new ObservableCollection<IOutboundAttachment>();
    }

    private async void UpdateUITitle()
    {
      VKBaseDataForGroupOrUser author = UsersService.Instance.GetCachedUser((long) this._userId);
      if (author == null)
      {
        await UsersService.Instance.GetUsers(new List<int>()
        {
          this._userId
        });
        author = UsersService.Instance.GetCachedUser((long) this._userId);
        if (author == null)
          return;
      }
      this.UITitle = author.Title;
    }

    private async void LoadHeaderInfoAsync(VKMessage m)
    {
      int count = 0;
      if (m.users_count == 0)
      {
        VKResponse<ChatInfo.Chat> temp = await RequestsDispatcher.GetResponse<ChatInfo.Chat>("messages.getChat", new Dictionary<string, string>()
        {
          ["chat_id"] = this._chatId.ToString()
        });
        if (temp == null || temp.response == null)
          return;
        if (temp.response.push_settings != null && temp.response.push_settings.sound)
        {
          this.AreNotificationsDisabled = true;
          // ISSUE: reference to a compiler-generated method
          this.\u003C\u003En__FabricatedMethodf("AreNotificationsDisabled");
        }
        count = temp.response.users.Count;
        m.chat_active = temp.response.users;
        m.photo_50 = temp.response.photo_50;
        m.photo_100 = temp.response.photo_100;
        m.photo_200 = temp.response.photo_200;
        this.UITitle = temp.response.title;
        List<int> userIds = new List<int>();
        userIds.AddRange((IEnumerable<int>) temp.response.users);
        await UsersService.Instance.GetUsers(userIds);
        this.RefreshAvatar(m);
      }
      else
        count = m.users_count;
      string tempStr = UIStringFormatterHelper.FormatNumberOfSomething(count, LocalizedStrings.GetString("Conversation_OnePerson"), LocalizedStrings.GetString("Conversation_TwoToFourPersonsFrm"), LocalizedStrings.GetString("Conversation_FiveOrMorePersionsFrm"));
      this.UISubtitle = tempStr;
    }

    private void RefreshAvatar(VKMessage m)
    {
      ConversationAvatarViewModel conversationAvatarViewModel = new ConversationAvatarViewModel(40.0);
      if (this._chatId > 0)
      {
        if (!string.IsNullOrEmpty(m.photo_200))
        {
          conversationAvatarViewModel.UsersCount = 1;
          conversationAvatarViewModel.UIChatImage1Url = m.photo_200;
        }
        else
        {
          if (m.chat_active == null)
            return;
          conversationAvatarViewModel.UsersCount = m.chat_active.Count;
          if (m.chat_active.Count > 0)
          {
            VKBaseDataForGroupOrUser cachedUser = UsersService.Instance.GetCachedUser((long) m.chat_active[0]);
            conversationAvatarViewModel.UIChatImage1Url = cachedUser.photo_200;
          }
          if (m.chat_active.Count > 1)
          {
            VKBaseDataForGroupOrUser cachedUser = UsersService.Instance.GetCachedUser((long) m.chat_active[1]);
            conversationAvatarViewModel.UIChatImage2Url = cachedUser.photo_200;
          }
          if (m.chat_active.Count > 2)
          {
            VKBaseDataForGroupOrUser cachedUser = UsersService.Instance.GetCachedUser((long) m.chat_active[2]);
            conversationAvatarViewModel.UIChatImage3Url = cachedUser.photo_200;
          }
          if (m.chat_active.Count > 3)
          {
            VKBaseDataForGroupOrUser cachedUser = UsersService.Instance.GetCachedUser((long) m.chat_active[3]);
            conversationAvatarViewModel.UIChatImage4Url = cachedUser.photo_200;
          }
          if (m.chat_active.Count == 0)
          {
            conversationAvatarViewModel.UsersCount = 1;
            conversationAvatarViewModel.UIChatImage1Url = m.photo_100;
          }
        }
      }
      else
      {
        conversationAvatarViewModel.UsersCount = 1;
        VKBaseDataForGroupOrUser cachedUser = UsersService.Instance.GetCachedUser((long) this._userId);
        conversationAvatarViewModel.UIChatImage1Url = cachedUser.photo_200;
        if (this._userId < 0)
        {
          conversationAvatarViewModel.Online = false;
        }
        else
        {
          conversationAvatarViewModel.Online = (cachedUser as VKProfileBase).online;
          conversationAvatarViewModel.online_app = (cachedUser as VKProfileBase).online_app;
          conversationAvatarViewModel.platform = (cachedUser as VKProfileBase).last_seen.platform;
        }
      }
      this.ConversationAvatarVM = conversationAvatarViewModel;
      this.NotifyPropertyChanged("ConversationAvatarVM");
    }

    public async Task LoadData(bool reload = false)
    {
      if (reload)
      {
        this.Items.Clear();
        if (this._chatId == 0)
          this.UpdateUITitle();
      }
      Dictionary<string, string> parameters = new Dictionary<string, string>();
      parameters["count"] = "30";
      parameters["peer_id"] = this.GetPeerId().ToString();
      if (this.Items.Count > 0)
        parameters["offset"] = this.Items.Count.ToString();
      else
        parameters["fields"] = "first_name,last_name,first_name_acc,last_name_acc,online,online_mobile,photo_200,photo_50,is_messages_blocked,last_seen,sex,push_settings,domain";
      parameters["extended"] = "1";
      VKResponse<VKGetMessagesHistoryObject> temp = await RequestsDispatcher.GetResponse<VKGetMessagesHistoryObject>("messages.getHistory", parameters);
      if (temp == null)
        return;
      List<VKMessageVM> msgs = temp.response.items;
      UsersService.Instance.SetCachedUsers((IEnumerable<VKBaseDataForGroupOrUser>) temp.response.profiles);
      UsersService.Instance.SetCachedUsers((IEnumerable<VKBaseDataForGroupOrUser>) temp.response.groups);
      List<int> list = this.GetAssociatedUserIds(temp.response.items);
      await UsersService.Instance.GetUsers(list);
      foreach (VKMessageVM vkMessageVm in msgs)
      {
        vkMessageVm.User = UsersService.Instance.GetCachedUser((long) vkMessageVm.user_id);
        this.Items.Add(vkMessageVm);
      }
      if (reload)
      {
        this._maximum = temp.response.count;
        VKDialog vkDialog = (VKDialog) DialogsViewModel.Instance.Dialogs.FirstOrDefault<VKDialogVM>((Func<VKDialogVM, bool>) (d => d.message.user_id == this._userId && d.message.chat_id == this._chatId));
        if (vkDialog == null)
        {
          vkDialog = new VKDialog();
          vkDialog.message = new VKMessage();
        }
        if (this._chatId > 0)
          this.LoadHeaderInfoAsync(vkDialog.message);
        else if (this._userId < 0)
          this.UISubtitle = LocalizedStrings.GetString("Community");
        else if (temp.response.profiles.Count > 0)
          this.UpdateUISubtitle(temp.response.profiles.Find((Predicate<VKProfileBase>) (u => u.id == this._userId)));
        this.RefreshAvatar(vkDialog.message);
      }
      if (!reload)
        return;
      this.SetReadStatusIfNeeded();
    }

    private int GetPeerId() => this._chatId > 0 ? this._chatId + 2000000000 : this._userId;

    private List<int> GetAssociatedUserIds(List<VKMessageVM> messages, bool includeForwarded = true)
    {
      List<int> source = new List<int>();
      foreach (VKMessage message in messages)
        source.AddRange((IEnumerable<int>) this.GetAssociatedUserIds(message, includeForwarded));
      return source.Distinct<int>().ToList<int>();
    }

    private List<int> GetAssociatedUserIds(VKMessage messag, bool includeForwarded = true)
    {
      List<int> source = new List<int>();
      source.Add(messag.user_id);
      if (messag.action_mid > 0)
        source.Add(messag.action_mid);
      if (messag.chat_active != null)
        source.AddRange(messag.chat_active.Where<int>((Func<int, bool>) (ca => ca >= -2000000000)));
      if (messag.fwd_messages != null & includeForwarded)
      {
        foreach (VKMessage fwdMessage in messag.fwd_messages)
          source.AddRange((IEnumerable<int>) this.GetAssociatedUserIds(fwdMessage));
      }
      return source.Distinct<int>().ToList<int>();
    }

    public void UpdateUISubtitle(VKProfileBase profile)
    {
      if (profile == null || profile.last_seen == null)
        return;
      profile.last_seen.Online = profile.online;
      this.UISubtitle = profile.last_seen.GetUserStatusString(profile.sex);
    }

    public void SendMessage(int StickerID)
    {
      VKSticker vkSticker = new VKSticker()
      {
        id = StickerID,
        photo_256 = "https://vk.com/images/stickers/" + (object) StickerID + "/256b.png"
      };
      VKAttachment vkAttachment = new VKAttachment()
      {
        sticker = vkSticker,
        type = VKAttachmentType.Sticker
      };
      List<VKAttachment> vkAttachmentList = new List<VKAttachment>();
      vkAttachmentList.Add(vkAttachment);
      VKMessageVM vkMessageVm = new VKMessageVM();
      vkMessageVm.@out = VKMessageType.Sent;
      vkMessageVm.date = DateTime.Now;
      vkMessageVm.attachments = vkAttachmentList;
      vkMessageVm.OutboundMessageVM = new OutboundMessageViewModel((long) this._userId, (long) this._chatId);
      vkMessageVm.OutboundMessageVM.OutboundMessageStatus = OutboundMessageStatus.SendingNow;
      vkMessageVm.OutboundMessageVM.StickerItem = StickerID;
      vkMessageVm.chat_id = this._chatId;
      vkMessageVm.user_id = (int) Settings.Instance.auth.UserId;
      this.Items.Insert(0, vkMessageVm);
      vkMessageVm.Send();
    }

    public void SendMessage(string messageText)
    {
      VKMessageVM vkMessageVm = new VKMessageVM();
      vkMessageVm.body = messageText;
      vkMessageVm.@out = VKMessageType.Sent;
      vkMessageVm.date = DateTime.Now;
      vkMessageVm.OutboundMessageVM = new OutboundMessageViewModel((long) this._userId, (long) this._chatId);
      vkMessageVm.OutboundMessageVM.OutboundMessageStatus = OutboundMessageStatus.SendingNow;
      vkMessageVm.OutboundMessageVM.Attachments = this.Attachments.ToList<IOutboundAttachment>();
      vkMessageVm.OutboundMessageVM.MessageText = messageText;
      vkMessageVm.chat_id = this._chatId;
      vkMessageVm.user_id = (int) Settings.Instance.auth.UserId;
      if (this.Attachments != null && this.Attachments.Count > 0)
      {
        foreach (IOutboundAttachment attachment in (Collection<IOutboundAttachment>) this.Attachments)
        {
          if (attachment is OutboundForwardedMessages)
          {
            vkMessageVm.fwd_messages = new List<VKMessage>();
            foreach (VKMessage message in (attachment as OutboundForwardedMessages).Messages)
              vkMessageVm.fwd_messages.Add(message);
          }
          else if (attachment is OutboundWallPostAttachment)
          {
            if (vkMessageVm.attachments == null)
              vkMessageVm.attachments = new List<VKAttachment>();
            VKWallPost vkWallPost = new VKWallPost();
            VKBaseDataForPostOrNews wallPost = (attachment as OutboundWallPostAttachment)._wallPost;
            vkWallPost.id = (uint) wallPost.PostId;
            vkWallPost.date = wallPost.date;
            vkWallPost.owner_id = (int) wallPost.OwnerId;
            vkWallPost.text = wallPost.text;
            vkWallPost.attachments = wallPost.attachments;
            vkWallPost.from_id = (int) wallPost.OwnerId;
            vkMessageVm.attachments.Add(new VKAttachment()
            {
              wall = vkWallPost,
              type = VKAttachmentType.Wall
            });
          }
        }
      }
      this.Items.Insert(0, vkMessageVm);
      vkMessageVm.Send();
    }

    public void UserIsTyping()
    {
      if ((DateTime.Now - this._lastTimeUserIsTypingWasCalled).TotalSeconds <= 8.0)
        return;
      this.SetUserIsTyping();
      this._lastTimeUserIsTypingWasCalled = DateTime.Now;
    }

    private async void SetUserIsTyping()
    {
      VKResponse<int> response = await RequestsDispatcher.GetResponse<int>("messages.setActivity", new Dictionary<string, string>()
      {
        ["peer_id"] = this.GetPeerId().ToString(),
        ["type"] = "typing"
      });
    }

    public async void MarkAsImportant(VKMessageVM msg, bool important)
    {
      VKResponse<List<int>> temp = await RequestsDispatcher.GetResponse<List<int>>("messages.markAsImportant", new Dictionary<string, string>()
      {
        ["message_ids"] = msg.id.ToString(),
        [nameof (important)] = important ? "1" : "0"
      });
      if (temp == null)
        return;
      msg.important = !msg.important;
      msg.RefreshUIProperties();
    }

    public async void DeleteMessages(List<VKMessageVM> msgs, Action<bool> callback = null)
    {
      VKResponse<Dictionary<int, int>> temp = await RequestsDispatcher.GetResponse<Dictionary<int, int>>("messages.delete", new Dictionary<string, string>()
      {
        ["message_ids"] = msgs.Select<VKMessageVM, int>((Func<VKMessageVM, int>) (m => m.id)).ToList<int>().GetCommaSeparated()
      });
      if (temp != null)
      {
        foreach (VKMessageVM msg in msgs)
          this.Items.Remove(msg);
        if (callback != null)
          callback(true);
      }
      if (callback == null)
        return;
      callback(false);
    }

    public void SetReadStatusIfNeeded()
    {
      List<VKMessage> listToBeMarkedAsRead = new List<VKMessage>();
      foreach (VKMessage vkMessage in (Collection<VKMessageVM>) this.Items)
      {
        if (vkMessage != null && !vkMessage.read_state && (vkMessage.@out == VKMessageType.Received || (long) vkMessage.user_id == Settings.Instance.auth.UserId))
          listToBeMarkedAsRead.Add(vkMessage);
      }
      if (listToBeMarkedAsRead.Count <= 0)
        return;
      this.MarkAsRead(listToBeMarkedAsRead.Select<VKMessage, int>((Func<VKMessage, int>) (m => m.id)).ToList<int>(), this.GetPeerId(), (Action) (() =>
      {
        foreach (VKMessage vkMessage in listToBeMarkedAsRead)
          vkMessage.read_state = true;
      }));
    }

    public async void MarkAsRead(List<int> messageIds, int peerId, Action calback)
    {
      Dictionary<string, string> parameters = new Dictionary<string, string>();
      parameters["peer_id"] = parameters["peer_id"] = peerId.ToString();
      parameters["start_message_id"] = messageIds.Max().ToString();
      VKResponse<long> response = await RequestsDispatcher.GetResponse<long>("messages.markAsRead", parameters);
    }

    public void AddForwardedMessagesToOutboundMessage(IList<VKMessage> forwardedMessages)
    {
      this.Attachments.Remove(this.Attachments.FirstOrDefault<IOutboundAttachment>((Func<IOutboundAttachment, bool>) (a => a is OutboundForwardedMessages)));
      this.Attachments.Add((IOutboundAttachment) new OutboundForwardedMessages(forwardedMessages.ToList<VKMessage>()));
    }

    public bool IsKickedFromChat
    {
      get
      {
        VKDialog vkDialog = (VKDialog) DialogsViewModel.Instance.Dialogs.FirstOrDefault<VKDialogVM>((Func<VKDialogVM, bool>) (d => d.message.user_id == this._userId && d.message.chat_id == this._chatId));
        return vkDialog != null && vkDialog.message.action == VKChatMessageActionType.ChatKickUser && (long) vkDialog.message.action_mid == Settings.Instance.auth.UserId;
      }
    }

    public List<VKBaseDataForGroupOrUser> ChatMembers
    {
      get
      {
        VKDialog vkDialog = (VKDialog) DialogsViewModel.Instance.Dialogs.FirstOrDefault<VKDialogVM>((Func<VKDialogVM, bool>) (d => d.message.chat_id == this._chatId));
        if (vkDialog == null)
          return (List<VKBaseDataForGroupOrUser>) null;
        List<VKBaseDataForGroupOrUser> chatMembers = new List<VKBaseDataForGroupOrUser>();
        foreach (long userId in vkDialog.message.chat_active)
        {
          VKBaseDataForGroupOrUser cachedUser = UsersService.Instance.GetCachedUser(userId);
          chatMembers.Add(cachedUser);
        }
        return chatMembers;
      }
    }
  }
}
