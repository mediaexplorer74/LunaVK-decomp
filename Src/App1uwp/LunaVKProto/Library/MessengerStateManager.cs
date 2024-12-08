// Decompiled with JetBrains decompiler
// Type: App1uwp.Library.MessengerStateManager
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
using App1uwp.Network.DataObjects;
using App1uwp.Network.DataVM;
using App1uwp.Network.Enums;
using App1uwp.Network.ViewModels;
using System;
using System.Collections.Generic;
using Windows.ApplicationModel;
using Windows.Phone.Devices.Notification;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace App1uwp.Library
{
  public class MessengerStateManager : ISubscriber<VKMessageVM>
  {
    private DateTime _lastSound = DateTime.Now;
    private static MessengerStateManager _instance;

    public static MessengerStateManager Instance
    {
      get
      {
        if (MessengerStateManager._instance == null)
          MessengerStateManager._instance = new MessengerStateManager();
        return MessengerStateManager._instance;
      }
    }

    public void Initialize() => EventAggregator.Instance.SubsribeEvent((object) this);

    public async void OnEventHandler(VKMessageVM m)
    {
      if (m.@out == VKMessageType.Sent)
        return;
      VKBaseDataForGroupOrUser u = UsersService.Instance.GetCachedUser((long) m.user_id);
      if (u == null)
      {
        await UsersService.Instance.GetUsers(new List<int>()
        {
          m.user_id
        });
        u = UsersService.Instance.GetCachedUser((long) m.user_id);
      }
      if (u == null)
        return;
      this.HandleInAppNotification(u.Title, DialogsViewModel.GetMessageHeaderText((VKMessage) m, (VKProfileBase) null, (VKProfileBase) null), m.user_id, m.chat_id, u.photo_50);
    }

    public async void HandleInAppNotification(
      string title,
      string message,
      int user_id,
      int chat_id,
      string imageSrc)
    {
      if (((ContentControl) (Window.Current.Content as Frame)).Content as PageBase is DialogsPage)
        return;
      if (((ContentControl) (Window.Current.Content as Frame)).Content as PageBase is ConversationPage)
      {
        ConversationPage content = ((ContentControl) (Window.Current.Content as Frame)).Content as PageBase as ConversationPage;
        if (chat_id > 0)
        {
          if (content.ConversationVM._chatId == chat_id)
            return;
        }
        else if (content.ConversationVM._userId == user_id)
          return;
      }
      if (Settings.Instance.NotificationsEnabled)
        (Window.Current.Content as CustomFrame).NotificationsPanel.AddAndShowNotification(imageSrc, title, message, (Action) (() => NavigatorImpl.Instance.NavigateToConversation(user_id, chat_id)));
      if (Settings.Instance.VibrationsEnabled)
      {
        TimeSpan timeSpan = TimeSpan.FromMilliseconds(60.0);
        VibrationDevice.GetDefault().Vibrate(timeSpan);
      }
      if (!Settings.Instance.SoundEnabled || (DateTime.Now - this._lastSound).TotalSeconds < 1.0)
        return;
      this._lastSound = DateTime.Now;
      MediaElement mysong = new MediaElement();
      StorageFolder folder = await Package.Current.InstalledLocation.GetFolderAsync("Assets");
      StorageFile file = await folder.GetFileAsync("chat_sound.wav");
      IRandomAccessStream stream = await file.OpenAsync((FileAccessMode) 0);
      mysong.SetSource(stream, file.ContentType);
      mysong.Play();
    }
  }
}
