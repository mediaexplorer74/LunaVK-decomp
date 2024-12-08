// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.DataVM.VKDialogVM
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Library;
using App1uwp.Network.DataObjects;
using App1uwp.Network.Enums;
using App1uwp.Network.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

#nullable disable
namespace App1uwp.Network.DataVM
{
  public class VKDialogVM : VKDialog, INotifyPropertyChanged
  {
    private Visibility _typingVisibility = (Visibility) 1;
    private string _typingStr;
    private string _UIBody = "";
    private DispatcherTimer dotsAnimationTimer;
    private DispatcherTimer stopAnimationTimer;
    private int _currentDotsNumber;
    private bool _timerInitialized;

    public event PropertyChangedEventHandler PropertyChanged;

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }

    public string UIBody
    {
      get => this._UIBody;
      set
      {
        this._UIBody = value;
        this.NotifyPropertyChanged(nameof (UIBody));
      }
    }

    public ConversationAvatarViewModel ConversationAvatarVM { get; set; }

    public Visibility CounterVisibility => this.unread <= 0 ? (Visibility) 1 : (Visibility) 0;

    public Visibility UserThumbVisibility
    {
      get
      {
        return !this.IsChat && this.message.@out != VKMessageType.Sent ? (Visibility) 1 : (Visibility) 0;
      }
    }

    public string UserThumb
    {
      get
      {
        if (this.message.@out == VKMessageType.Sent)
          return Settings.Instance.LoggedInUser.photo_200 == null ? (string) null : Settings.Instance.LoggedInUser.photo_200;
        if (!this.IsChat)
          return (string) null;
        VKBaseDataForGroupOrUser cachedUser = UsersService.Instance.GetCachedUser((long) this.message.user_id);
        string userThumb = cachedUser.photo_200;
        if (string.IsNullOrEmpty(userThumb))
          userThumb = cachedUser.photo_100;
        return userThumb;
      }
    }

    public Visibility TypingVisibility
    {
      get => this._typingVisibility;
      set
      {
        this._typingVisibility = value;
        this.NotifyPropertyChanged(nameof (TypingVisibility));
      }
    }

    public SolidColorBrush MainBackgroundBrush
    {
      get
      {
        return this.message.@out == VKMessageType.Received && !this.message.read_state ? (SolidColorBrush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "AccentBrushLow"] : (SolidColorBrush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "ItemBackgroundBrush"];
      }
    }

    public SolidColorBrush TextBackgroundBrush
    {
      get
      {
        return !this.message.read_state && this.message.@out == VKMessageType.Sent ? (SolidColorBrush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "AccentBrushLow"] : new SolidColorBrush(Colors.Transparent);
      }
    }

    public string TypingStr
    {
      get => this._typingStr;
      set
      {
        this._typingStr = value;
        this.NotifyPropertyChanged(nameof (TypingStr));
      }
    }

    public int Unread => this.unread;

    public void RefreshUIProperties()
    {
      this.NotifyPropertyChanged("MainBackgroundBrush");
      this.NotifyPropertyChanged("TextBackgroundBrush");
      this.NotifyPropertyChanged("Unread");
      this.NotifyPropertyChanged("UserThumbVisibility");
      this.NotifyPropertyChanged("CounterVisibility");
      this.NotifyPropertyChanged("UserThumb");
    }

    public bool IsChat => this.message.chat_id > 0;

    public void StopAnimTyping()
    {
      if (this.dotsAnimationTimer != null)
        this.dotsAnimationTimer.Stop();
      if (this.stopAnimationTimer != null)
        this.stopAnimationTimer.Stop();
      this._currentDotsNumber = 0;
      this._typingStr = "";
      this.TypingVisibility = (Visibility) 1;
    }

    public void AnimTyping(long userId, long chatId)
    {
      this.TypingVisibility = (Visibility) 0;
      if (!this._timerInitialized)
      {
        this.dotsAnimationTimer = new DispatcherTimer();
        this.stopAnimationTimer = new DispatcherTimer();
        this.dotsAnimationTimer.put_Interval(TimeSpan.FromSeconds(0.25));
        this.stopAnimationTimer.put_Interval(TimeSpan.FromSeconds(5.0));
        DispatcherTimer dotsAnimationTimer = this.dotsAnimationTimer;
        WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(dotsAnimationTimer.add_Tick), new Action<EventRegistrationToken>(dotsAnimationTimer.remove_Tick), (EventHandler<object>) ((o, e) =>
        {
          ++this._currentDotsNumber;
          if (this._currentDotsNumber > 3)
          {
            this._currentDotsNumber = 0;
          }
          else
          {
            string str1 = "";
            if (chatId > 0L)
            {
              VKBaseDataForGroupOrUser cachedUser = UsersService.Instance.GetCachedUser(userId);
              if (cachedUser != null)
                str1 = cachedUser.Title + " ";
            }
            string str2 = str1 + LocalizedStrings.GetString("IsTyping");
            for (int index = 0; index < this._currentDotsNumber; ++index)
              str2 += ".";
            this.TypingStr = str2;
          }
        }));
        DispatcherTimer stopAnimationTimer = this.stopAnimationTimer;
        WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(stopAnimationTimer.add_Tick), new Action<EventRegistrationToken>(stopAnimationTimer.remove_Tick), (EventHandler<object>) ((o, e) =>
        {
          this.dotsAnimationTimer.Stop();
          this._currentDotsNumber = 0;
          this._typingStr = "";
          this.TypingVisibility = (Visibility) 1;
        }));
        this._timerInitialized = true;
      }
      this.dotsAnimationTimer.Stop();
      this.stopAnimationTimer.Stop();
      this.dotsAnimationTimer.Start();
      this.stopAnimationTimer.Start();
    }

    public bool UserVerified
    {
      get
      {
        VKBaseDataForGroupOrUser cachedUser = UsersService.Instance.GetCachedUser((long) this.message.user_id);
        return cachedUser != null && cachedUser.verified;
      }
    }

    public SolidColorBrush HaveUnreadMessagesBackground
    {
      get
      {
        return !this.AreNotificationsDisabled ? (SolidColorBrush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "AccentBrushHigh"] : new SolidColorBrush(Colors.Gray);
      }
    }

    public bool AreNotificationsDisabled
    {
      get => this.message.push_settings != null && this.message.push_settings.sound;
    }
  }
}
