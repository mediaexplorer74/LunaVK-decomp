// Decompiled with JetBrains decompiler
// Type: App1uwp.Library.SettingsViewModel
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.DataObjects;
using App1uwp.Network.ViewModels;
using System;
using System.Linq.Expressions;

#nullable disable
namespace App1uwp.Library
{
  public class SettingsViewModel : ViewModelBase
  {
    private bool _isInProgress;

    public bool PushNotificationsEnabledAndNotTempDisabled
    {
      get => this.PushNotificationsEnabled && !this.TempDisabled;
    }

    public bool TempDisabled
    {
      get
      {
        return this.PushNotificationsEnabled && Settings.Instance.PushNotificationsBlockedUntil >= DateTime.UtcNow;
      }
    }

    public bool PushNotificationsEnabled
    {
      get => Settings.Instance.PushNotificationsEnabled;
      set
      {
        if (this._isInProgress || this.PushNotificationsEnabled == value)
          return;
        Settings.Instance.PushNotificationsEnabled = value;
        PushNotifications.Instance.UpdateDeviceRegistration();
        this.NotifyPropertyChanged(nameof (PushNotificationsEnabled));
        this.NotifyPropertyChanged("PushNotificationsEnabledAndNotTempDisabled");
      }
    }

    public bool NewPrivateMessagesNotifications
    {
      get => Settings.Instance.PushSettings.msg;
      set
      {
        if (this._isInProgress || this.NewPrivateMessagesNotifications == value)
          return;
        Settings.Instance.PushSettings.msg = value;
        this.NotifyPropertyChanged(nameof (NewPrivateMessagesNotifications));
        PushNotifications.Instance.SetPushSettings("msg", PushSettings.GetOnOffStr(value));
      }
    }

    public bool NewChatMessagesNotifications
    {
      get => Settings.Instance.PushSettings.chat;
      set
      {
        if (this._isInProgress || this.NewChatMessagesNotifications == value)
          return;
        Settings.Instance.PushSettings.chat = value;
        this.NotifyPropertyChanged(nameof (NewChatMessagesNotifications));
        PushNotifications.Instance.SetPushSettings("chat", PushSettings.GetOnOffStr(value));
      }
    }

    public bool LikesNotifications
    {
      get => Settings.Instance.PushSettings.like;
      set
      {
        if (this._isInProgress || this.LikesNotifications == value)
          return;
        Settings.Instance.PushSettings.like = value;
        this.NotifyPropertyChanged(nameof (LikesNotifications));
        PushNotifications.Instance.SetPushSettings("like", PushSettings.GetOnOffStr(value));
      }
    }

    public bool RepostsNotifications
    {
      get => Settings.Instance.PushSettings.repost;
      set
      {
        if (this._isInProgress || this.RepostsNotifications == value)
          return;
        Settings.Instance.PushSettings.repost = value;
        this.NotifyPropertyChanged(nameof (RepostsNotifications));
        PushNotifications.Instance.SetPushSettings("repost", PushSettings.GetOnOffStr(value));
      }
    }

    public bool CommentsNotifications
    {
      get => Settings.Instance.PushSettings.comment;
      set
      {
        if (this._isInProgress || this.CommentsNotifications == value)
          return;
        Settings.Instance.PushSettings.comment = value;
        this.NotifyPropertyChanged(nameof (CommentsNotifications));
        PushNotifications.Instance.SetPushSettings("comment", PushSettings.GetOnOffStr(value));
      }
    }

    public bool MentionsNotifications
    {
      get => Settings.Instance.PushSettings.mention;
      set
      {
        if (this._isInProgress || this.MentionsNotifications == value)
          return;
        Settings.Instance.PushSettings.mention = value;
        this.NotifyPropertyChanged(nameof (MentionsNotifications));
        PushNotifications.Instance.SetPushSettings("mention", PushSettings.GetOnOffStr(value));
      }
    }

    public bool WallPostsNotifications
    {
      get => Settings.Instance.PushSettings.wall_post;
      set
      {
        if (this._isInProgress || this.WallPostsNotifications == value)
          return;
        Settings.Instance.PushSettings.wall_post = value;
        this.NotifyPropertyChanged(nameof (WallPostsNotifications));
        PushNotifications.Instance.SetPushSettings("wall_post", PushSettings.GetOnOffStr(value));
      }
    }

    public bool RepliesNotifications
    {
      get => Settings.Instance.PushSettings.reply;
      set
      {
        if (this._isInProgress || this.RepliesNotifications == value)
          return;
        Settings.Instance.PushSettings.reply = value;
        this.NotifyPropertyChanged(nameof (RepliesNotifications));
        PushNotifications.Instance.SetPushSettings("reply", PushSettings.GetOnOffStr(value));
      }
    }

    public bool FriendRequestsNotifications
    {
      get => Settings.Instance.PushSettings.friend;
      set
      {
        if (this._isInProgress || this.FriendRequestsNotifications == value)
          return;
        Settings.Instance.PushSettings.friend = value;
        this.NotifyPropertyChanged(nameof (FriendRequestsNotifications));
        PushNotifications.Instance.SetPushSettings("friend", PushSettings.GetOnOffStr(value));
      }
    }

    public bool GroupInvitationsNotifications
    {
      get => Settings.Instance.PushSettings.group_invite;
      set
      {
        if (this._isInProgress || this.GroupInvitationsNotifications == value)
          return;
        Settings.Instance.PushSettings.group_invite = value;
        this.NotifyPropertyChanged(nameof (GroupInvitationsNotifications));
        PushNotifications.Instance.SetPushSettings("group_invite", PushSettings.GetOnOffStr(value));
      }
    }

    public bool BirthdaysNotifications
    {
      get => Settings.Instance.PushSettings.birthday;
      set
      {
        if (this._isInProgress || this.BirthdaysNotifications == value)
          return;
        Settings.Instance.PushSettings.birthday = value;
        this.NotifyPropertyChanged(nameof (BirthdaysNotifications));
        PushNotifications.Instance.SetPushSettings("birthday", PushSettings.GetOnOffStr(value));
      }
    }

    public bool ForthcomingEventsNotifications
    {
      get => Settings.Instance.PushSettings.event_soon;
      set
      {
        if (this._isInProgress || this.ForthcomingEventsNotifications == value)
          return;
        Settings.Instance.PushSettings.event_soon = value;
        this.NotifyPropertyChanged(nameof (ForthcomingEventsNotifications));
        PushNotifications.Instance.SetPushSettings("event_soon", PushSettings.GetOnOffStr(value));
      }
    }

    public bool InAppSound
    {
      get => Settings.Instance.SoundEnabled;
      set
      {
        Settings.Instance.SoundEnabled = value;
        this.NotifyPropertyChanged(nameof (InAppSound));
      }
    }

    public bool IsAppVibration
    {
      get => Settings.Instance.VibrationsEnabled;
      set
      {
        Settings.Instance.VibrationsEnabled = value;
        this.NotifyPropertyChanged(nameof (IsAppVibration));
      }
    }

    public bool InAppBanner
    {
      get => Settings.Instance.NotificationsEnabled;
      set
      {
        Settings.Instance.NotificationsEnabled = value;
        this.NotifyPropertyChanged(nameof (InAppBanner));
      }
    }

    public string TempDisabledString
    {
      get
      {
        return this.TempDisabled ? LocalizedStrings.GetString("Settings_DisabledNotifications") + " " + (Settings.Instance.PushNotificationsBlockedUntil + (DateTime.Now - DateTime.UtcNow)).ToString() : "";
      }
    }

    public async void Disable(int seconds)
    {
      if (this._isInProgress)
        return;
      DateTime savedValue = Settings.Instance.PushNotificationsBlockedUntil;
      Settings.Instance.PushNotificationsBlockedUntil = seconds != 0 ? DateTime.UtcNow + TimeSpan.FromSeconds((double) seconds) : DateTime.MinValue;
      this.NotifyPropertyChanged<string>((Expression<Func<string>>) (() => this.TempDisabledString));
      this.NotifyPropertyChanged<bool>((Expression<Func<bool>>) (() => this.TempDisabled));
      this.NotifyPropertyChanged<bool>((Expression<Func<bool>>) (() => this.PushNotificationsEnabledAndNotTempDisabled));
      this._isInProgress = true;
      bool res = await PushNotifications.Instance.SetSilenceMode(seconds);
      this._isInProgress = false;
      if (res)
        return;
      Settings.Instance.PushNotificationsBlockedUntil = savedValue;
      this.NotifyPropertyChanged<string>((Expression<Func<string>>) (() => this.TempDisabledString));
      this.NotifyPropertyChanged<bool>((Expression<Func<bool>>) (() => this.TempDisabled));
      this.NotifyPropertyChanged<bool>((Expression<Func<bool>>) (() => this.PushNotificationsEnabledAndNotTempDisabled));
    }
  }
}
