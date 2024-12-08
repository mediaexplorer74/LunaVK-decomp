// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.ViewModels.DialogsViewModel
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
using App1uwp.Library;
using App1uwp.Library.Events;
using App1uwp.Network.DataObjects;
using App1uwp.Network.DataVM;
using App1uwp.Network.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;

#nullable disable
namespace App1uwp.Network.ViewModels
{
  public class DialogsViewModel : 
    ViewModelBase,
    ISubscriber<VKMessageVM>,
    ISubscriber<MessageHasBeenReadEvent>,
    ISupportLoadMore
  {
    private static DialogsViewModel _instance;

    public ObservableCollection<VKDialogVM> Dialogs { get; private set; }

    public static DialogsViewModel Instance
    {
      get
      {
        if (DialogsViewModel._instance == null)
          DialogsViewModel._instance = new DialogsViewModel();
        return DialogsViewModel._instance;
      }
      set => DialogsViewModel._instance = value;
    }

    public string Title => LocalizedStrings.GetString("Menu_Messages/Title");

    public int DialogsSource { get; set; }

    public bool HasMoreItems => true;

    public void SetDialogsSource(int value)
    {
      this.DialogsSource = value;
      this.Dialogs.Clear();
      this.LoadData(true);
    }

    public DialogsViewModel()
    {
      this.Dialogs = new ObservableCollection<VKDialogVM>();
      EventAggregator.Instance.SubsribeEvent((object) this);
    }

    public void OnEventHandler(UserIsTyping e)
    {
      (e._chatId <= 0L ? this.Dialogs.FirstOrDefault<VKDialogVM>((Func<VKDialogVM, bool>) (dialog => !dialog.IsChat && (long) dialog.message.user_id == e._userId)) : this.Dialogs.FirstOrDefault<VKDialogVM>((Func<VKDialogVM, bool>) (dialog => (long) dialog.message.chat_id == e._chatId)))?.AnimTyping(e._userId, e._chatId);
    }

    public void OnEventHandler(VKMessageVM m)
    {
      VKDialogVM vkDialogVm = m.chat_id <= 0 ? this.Dialogs.FirstOrDefault<VKDialogVM>((Func<VKDialogVM, bool>) (dialog => !dialog.IsChat && dialog.message.user_id == m.user_id)) : this.Dialogs.FirstOrDefault<VKDialogVM>((Func<VKDialogVM, bool>) (dialog => dialog.message.chat_id == m.chat_id));
      if (vkDialogVm == null)
      {
        vkDialogVm = new VKDialogVM();
        vkDialogVm.message = new VKMessage();
        vkDialogVm.message.chat_id = m.chat_id;
      }
      vkDialogVm.message.user_id = m.user_id;
      vkDialogVm.message.@out = m.@out;
      vkDialogVm.message.read_state = false;
      vkDialogVm.message.date = m.date;
      if (m.@out == VKMessageType.Received)
        ++vkDialogVm.unread;
      string messageHeaderText = DialogsViewModel.GetMessageHeaderText((VKMessage) m, (VKProfileBase) null, (VKProfileBase) null);
      vkDialogVm.UIBody = messageHeaderText;
      vkDialogVm.StopAnimTyping();
      if (!this.Dialogs.Contains(vkDialogVm))
      {
        this.Dialogs.Insert(0, vkDialogVm);
      }
      else
      {
        if (this.Dialogs.IndexOf(vkDialogVm) != 0)
        {
          this.Dialogs.Remove(vkDialogVm);
          this.Dialogs.Insert(0, vkDialogVm);
        }
        vkDialogVm.RefreshUIProperties();
      }
    }

    public void OnEventHandler(MessageHasBeenReadEvent msg_readed)
    {
      VKDialogVM vkDialogVm;
      if (msg_readed.is_chat)
      {
        if (msg_readed.chat_id < 0L)
        {
          msg_readed.chat_id += 2000000000L;
          vkDialogVm = this.Dialogs.FirstOrDefault<VKDialogVM>((Func<VKDialogVM, bool>) (dialog => (long) dialog.message.user_id == msg_readed.chat_id));
        }
        else
          vkDialogVm = this.Dialogs.FirstOrDefault<VKDialogVM>((Func<VKDialogVM, bool>) (dialog => (long) dialog.message.chat_id == msg_readed.chat_id));
      }
      else
        vkDialogVm = this.Dialogs.FirstOrDefault<VKDialogVM>((Func<VKDialogVM, bool>) (dialog => dialog.message.user_id == msg_readed.user_id));
      if (vkDialogVm == null)
        return;
      vkDialogVm.message.read_state = true;
      vkDialogVm.unread = 0;
      vkDialogVm.RefreshUIProperties();
    }

    public async Task LoadData(bool reload = false)
    {
      VKResponse<VKDialogsGetObject> o = (VKResponse<VKDialogsGetObject>) null;
      Dictionary<string, string> parameters = new Dictionary<string, string>();
      parameters["count"] = "40";
      if (this.DialogsSource == 1)
      {
        parameters["filters"] = "8";
      }
      else
      {
        parameters["fields"] = "first_name_acc,last_name_acc,online,online_mobile,photo_200,photo_50,last_seen,verified,push_settings,domain";
        parameters["extended"] = "1";
        if (this.DialogsSource == 2)
          parameters["unread"] = "1";
        if (!reload)
          parameters["offset"] = this.Dialogs.Count.ToString();
        o = await RequestsDispatcher.GetResponse<VKDialogsGetObject>("messages.getDialogs", parameters);
        if (o == null)
          return;
        if (reload && this.Dialogs.Count > 40)
        {
          for (int index = 40; index < this.Dialogs.Count; ++index)
            this.Dialogs.RemoveAt(index);
        }
        UsersService.Instance.SetCachedUsers((IEnumerable<VKBaseDataForGroupOrUser>) o.response.profiles);
        UsersService.Instance.SetCachedUsers((IEnumerable<VKBaseDataForGroupOrUser>) o.response.groups);
        uint pos = 0;
        foreach (VKDialogVM vkDialogVm in o.response.items)
        {
          VKDialogVM dialog = vkDialogVm;
          ConversationAvatarViewModel conversationAvatarViewModel = new ConversationAvatarViewModel((double) ((IDictionary<object, object>) Application.Current.Resources)[(object) "Double72"]);
          if (dialog.IsChat)
          {
            if (dialog.message.action == VKChatMessageActionType.ChatKickUser && (long) dialog.message.action_mid == Settings.Instance.auth.UserId)
            {
              conversationAvatarViewModel.UsersCount = 1;
              conversationAvatarViewModel.UIChatImage1Url = Constants.AVATAR_COMMUNITY + "_200.png";
            }
            else if (!string.IsNullOrEmpty(dialog.message.photo_200))
            {
              conversationAvatarViewModel.UsersCount = 1;
              conversationAvatarViewModel.UIChatImage1Url = dialog.message.photo_200;
            }
            else
            {
              conversationAvatarViewModel.UsersCount = dialog.message.chat_active.Count;
              if (dialog.message.chat_active.Count > 0)
              {
                VKBaseDataForGroupOrUser cachedUser = UsersService.Instance.GetCachedUser((long) dialog.message.chat_active[0]);
                conversationAvatarViewModel.UIChatImage1Url = cachedUser.photo_200;
              }
              if (dialog.message.chat_active.Count > 1)
              {
                VKBaseDataForGroupOrUser cachedUser = UsersService.Instance.GetCachedUser((long) dialog.message.chat_active[1]);
                conversationAvatarViewModel.UIChatImage2Url = cachedUser.photo_200;
              }
              if (dialog.message.chat_active.Count > 2)
              {
                VKBaseDataForGroupOrUser cachedUser = UsersService.Instance.GetCachedUser((long) dialog.message.chat_active[2]);
                conversationAvatarViewModel.UIChatImage3Url = cachedUser.photo_200;
              }
              if (dialog.message.chat_active.Count > 3)
              {
                VKBaseDataForGroupOrUser cachedUser = UsersService.Instance.GetCachedUser((long) dialog.message.chat_active[3]);
                conversationAvatarViewModel.UIChatImage4Url = cachedUser.photo_200;
              }
              if (dialog.message.chat_active.Count == 0)
              {
                conversationAvatarViewModel.UIChatImage1Url = dialog.message.photo_200;
                conversationAvatarViewModel.UsersCount = 1;
              }
            }
          }
          else
          {
            VKBaseDataForGroupOrUser dataForGroupOrUser;
            if (dialog.message.user_id < 0)
            {
              dataForGroupOrUser = (VKBaseDataForGroupOrUser) o.response.groups.Find((Predicate<VKGroup>) (pro => pro.id == -dialog.message.user_id));
            }
            else
            {
              dataForGroupOrUser = (VKBaseDataForGroupOrUser) o.response.profiles.Find((Predicate<VKProfileBase>) (pro => pro.id == dialog.message.user_id));
              conversationAvatarViewModel.Online = ((VKProfileBase) dataForGroupOrUser).online;
              conversationAvatarViewModel.online_app = ((VKProfileBase) dataForGroupOrUser).online_app;
              if (((VKProfileBase) dataForGroupOrUser).last_seen != null)
                conversationAvatarViewModel.platform = ((VKProfileBase) dataForGroupOrUser).last_seen.platform;
            }
            if (dataForGroupOrUser != null)
            {
              conversationAvatarViewModel.UIChatImage1Url = dataForGroupOrUser.deactivated != VKIsDeactivated.None ? Constants.AVATAR_DEACTIVATED + "_200.png" : (dataForGroupOrUser.photo_200 != null ? dataForGroupOrUser.photo_200 : dataForGroupOrUser.photo_50);
              dialog.message.title = dataForGroupOrUser.Title;
            }
            conversationAvatarViewModel.UsersCount = 1;
          }
          dialog.ConversationAvatarVM = conversationAvatarViewModel;
          VKProfileBase user = o.response.profiles.Find((Predicate<VKProfileBase>) (pro => pro.id == dialog.message.user_id));
          VKProfileBase user2 = o.response.profiles.Find((Predicate<VKProfileBase>) (pro => pro.id == dialog.message.action_mid));
          dialog.UIBody = DialogsViewModel.GetMessageHeaderText(dialog.message, user, user2);
          if (reload)
            this.Merge(dialog, pos);
          else
            this.Dialogs.Add(dialog);
          ++pos;
        }
      }
    }

    private void Merge(VKDialogVM dialog, uint position)
    {
      if ((long) position >= (long) this.Dialogs.Count)
      {
        this.Dialogs.Add(dialog);
      }
      else
      {
        VKDialogVM dialog1 = this.Dialogs[(int) position];
        if (dialog1.message.chat_id != dialog.message.chat_id || dialog.message.chat_id == 0 && dialog.message.user_id != dialog1.message.user_id)
        {
          VKDialogVM vkDialogVm = dialog.message.chat_id <= 0 ? this.Dialogs.FirstOrDefault<VKDialogVM>((Func<VKDialogVM, bool>) (di => di.message.user_id == dialog.message.user_id)) : this.Dialogs.FirstOrDefault<VKDialogVM>((Func<VKDialogVM, bool>) (di => di.message.chat_id == dialog.message.chat_id));
          if (vkDialogVm == null)
          {
            this.Dialogs.Insert((int) position, dialog);
          }
          else
          {
            this.Dialogs.RemoveAt(this.Dialogs.IndexOf(vkDialogVm));
            this.Dialogs.Insert((int) position, vkDialogVm);
          }
        }
        else
        {
          dialog1.UIBody = dialog.UIBody;
          dialog1.unread = dialog.unread;
          dialog1.message.@out = dialog.message.@out;
          dialog1.message.read_state = dialog.message.read_state;
          dialog1.message.date = dialog.message.date;
          dialog1.RefreshUIProperties();
        }
      }
    }

    public static string GetMessageHeaderText(
      VKMessage message,
      VKProfileBase user,
      VKProfileBase user2)
    {
      if (!string.IsNullOrWhiteSpace(message.body))
        return message.body;
      if (message.action != VKChatMessageActionType.None)
        return DialogsViewModel.GenerateText(message, user, user2, false);
      if (message.attachments != null && message.attachments.Count > 0)
      {
        VKAttachment firstAttachment = message.attachments[0];
        int count = message.attachments.FindAll((Predicate<VKAttachment>) (a => a.type == firstAttachment.type)).Count;
        if (firstAttachment.type == VKAttachmentType.Link)
          return LocalizedStrings.GetString("Link");
        if (firstAttachment.type == VKAttachmentType.Wall)
          return LocalizedStrings.GetString("WallPost");
        if (firstAttachment.type == VKAttachmentType.Gift)
          return LocalizedStrings.GetString("Gift");
        if (firstAttachment.type == VKAttachmentType.Photo)
        {
          if (count == 1)
            return LocalizedStrings.GetString("OnePhoto");
          return count < 5 ? string.Format(LocalizedStrings.GetString("TwoFourPhotosFrm"), (object) count) : string.Format(LocalizedStrings.GetString("FiveOrMorePhotosFrm"), (object) count);
        }
        if (firstAttachment.type == VKAttachmentType.Wall_reply)
          return LocalizedStrings.GetString("Comment");
        if (firstAttachment.type == VKAttachmentType.Sticker)
          return LocalizedStrings.GetString("Sticker");
        if (firstAttachment.type == VKAttachmentType.Doc)
        {
          VKDocument doc = firstAttachment.doc;
          if (count == 1)
            return LocalizedStrings.GetString("OneDocument");
          return count < 5 ? string.Format(LocalizedStrings.GetString("TwoFourDocumentsFrm"), (object) count) : string.Format(LocalizedStrings.GetString("FiveMoreDocumentsFrm"), (object) count);
        }
        if (firstAttachment.type == VKAttachmentType.Audio)
        {
          if (count == 1)
            return LocalizedStrings.GetString("OneAudio");
          return count < 5 ? string.Format(LocalizedStrings.GetString("TwoFourAudioFrm"), (object) count) : string.Format(LocalizedStrings.GetString("FiveOrMoreAudioFrm"), (object) count);
        }
        if (firstAttachment.type == VKAttachmentType.Video)
        {
          if (count == 1)
            return LocalizedStrings.GetString("OneVideo");
          return count < 5 ? string.Format(LocalizedStrings.GetString("TwoFourVideosFrm"), (object) count) : string.Format(LocalizedStrings.GetString("FiveOrMoreVideosFrm"), (object) count);
        }
      }
      if (message.geo != null)
        return LocalizedStrings.GetString("Location");
      if (message.fwd_messages == null || message.fwd_messages.Count <= 0)
        return string.Empty;
      int count1 = message.fwd_messages.Count;
      if (count1 == 1)
        return LocalizedStrings.GetString("OneForwardedMessage");
      return count1 < 5 ? string.Format(LocalizedStrings.GetString("TwoFourForwardedMessagesFrm"), (object) count1) : string.Format(LocalizedStrings.GetString("FiveMoreForwardedMessagesFrm"), (object) count1);
    }

    public static string GenerateText(
      VKMessage message,
      VKProfileBase user1,
      VKProfileBase user2,
      bool extendedText)
    {
      if (message == null)
        return "";
      if (user1 == null)
        user1 = new VKProfileBase();
      if (user2 == null)
        user2 = new VKProfileBase();
      string userNameText = DialogsViewModel.CreateUserNameText(user1, false, extendedText);
      string str1 = user2.id > -2000000000 ? DialogsViewModel.CreateUserNameText(user2, true, extendedText) : message.action_email;
      string str2 = "";
      switch (message.action)
      {
        case VKChatMessageActionType.ChatPhotoUpdate:
          str2 = string.Format(LocalizedStrings.GetString(user1.IsFemale ? "ChatPhotoUpdateFemaleFrm" : "ChatPhotoUpdateMaleFrm"), (object) userNameText);
          break;
        case VKChatMessageActionType.ChatPhotoRemove:
          str2 = string.Format(LocalizedStrings.GetString(user1.IsFemale ? "ChatPhotoDeleteFemaleFrm" : "ChatPhotoDeleteMaleFrm"), (object) userNameText);
          break;
        case VKChatMessageActionType.ChatCreate:
          str2 = string.Format(LocalizedStrings.GetString(user1.IsFemale ? "ChatCreateFemaleFrm" : "ChatCreateMaleFrm"), (object) userNameText, (object) message.action_text);
          break;
        case VKChatMessageActionType.ChatTitleUpdate:
          str2 = string.Format(LocalizedStrings.GetString(user1.IsFemale ? "ChatTitleUpdateFemaleFrm" : "ChatTitleUpdateMaleFrm"), (object) userNameText, (object) message.action_text);
          break;
        case VKChatMessageActionType.ChatInviteUser:
          string str3;
          if ((long) message.action_mid == (long) message.user_id)
            str3 = string.Format(LocalizedStrings.GetString(user1.IsFemale ? "ChatReturnedToConversationFemaleFrm" : "ChatReturnedToConversationMaleFrm"), (object) userNameText);
          else
            str3 = string.Format(LocalizedStrings.GetString(user1.IsFemale ? "ChatInviteFemaleFrm" : "ChatInviteMaleFrm"), (object) userNameText, (object) str1);
          str2 = str3;
          break;
        case VKChatMessageActionType.ChatKickUser:
          string str4;
          if ((long) message.action_mid == (long) message.user_id)
            str4 = string.Format(LocalizedStrings.GetString(user1.IsFemale ? "ChatLeftConversationFemaleFrm" : "ChatLeftConversationMaleFrm"), (object) userNameText);
          else
            str4 = string.Format(LocalizedStrings.GetString(user1.IsFemale ? "ChatKickoutFemaleFrm" : "ChatKickoutMaleFrm"), (object) userNameText, (object) str1);
          str2 = str4;
          break;
        case VKChatMessageActionType.ChatInviteUserByLink:
          str2 = string.Format(LocalizedStrings.GetString(user1.IsFemale ? "ChatInviteUserByLinkFemaleFrm" : "ChatInviteUserByLinkMaleFrm"), (object) userNameText);
          break;
      }
      return str2.Trim();
    }

    private static string CreateUserNameText(VKProfileBase user, bool isAcc, bool extendedText)
    {
      string userNameText = isAcc ? user.NameAcc : user.FullName;
      if (!extendedText)
        return userNameText;
      return user.id > 0 ? string.Format("[id{0}|{1}]", (object) user.id, (object) userNameText) : string.Format("[club{0}|{1}]", (object) -user.id, (object) userNameText);
    }
  }
}
