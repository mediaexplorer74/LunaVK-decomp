// Decompiled with JetBrains decompiler
// Type: App1uwp.MusicPage
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Audio;
using App1uwp.Framework;
using App1uwp.Network.DataObjects;
using App1uwp.Network.Enums;
using App1uwp.Network.ViewModels;
using App1uwp.UC;
using BackgroundAudio;
using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Navigation;

#nullable disable
namespace App1uwp
{
  public sealed class MusicPage : PageBase, IComponentConnector
  {
    private AutoResetEvent SererInitialized;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private ExtendedListView2 MainScroll;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private LoadingUC loading;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public MusicPage()
    {
      this.SererInitialized = new AutoResetEvent(false);
      this.InitializeComponent();
      MusicPage musicPage = this;
      WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(((FrameworkElement) musicPage).add_Loaded), new Action<EventRegistrationToken>(((FrameworkElement) musicPage).remove_Loaded), (RoutedEventHandler) ((s, e) => (Window.Current.Content as CustomFrame).HeaderWithMenu.SetTitle(LocalizedStrings.GetString("Menu_Audios/Title"))));
      ForegroundPlaylistManager.Instance.TrackChanged += new EventHandler<int>(this.CurrentTrackChanged);
      ForegroundPlaylistManager.Instance.CurrentStateChanged += new EventHandler<MediaPlayerState>(this.CurrentStateChanged);
    }

    private void CurrentStateChanged(object sender, MediaPlayerState state)
    {
      this.VM.Items[ForegroundPlaylistManager.Instance.CurrentTrack].UpdateUI();
    }

    private void CurrentTrackChanged(object sender, int index)
    {
      foreach (VKAudio vkAudio in (Collection<VKAudio>) this.VM.Items)
      {
        if (vkAudio.IsCurrent)
        {
          vkAudio.IsCurrent = false;
          vkAudio.UpdateUI();
        }
      }
      VKAudio vkAudio1 = this.VM.Items[index];
      vkAudio1.IsCurrent = true;
      vkAudio1.UpdateUI();
    }

    public MusicViewModel VM => ((FrameworkElement) this).DataContext as MusicViewModel;

    protected override void HandleOnNavigatedTo(NavigationEventArgs e)
    {
      MediaPlayer current = BackgroundMediaPlayer.Current;
      ((FrameworkElement) this).put_DataContext((object) new MusicViewModel((e.Parameter as PagesParams).user_id));
      base.HandleOnNavigatedTo(e);
      this.VM.LoadingStatusUpdated += new Action<ProfileLoadingStatus>(this.HandleLoadingStatusUpdated);
      if (ForegroundPlaylistManager.Instance.Tracks.Count > 0)
      {
        int num = 0;
        foreach (AudioHeader track in ForegroundPlaylistManager.Instance.Tracks)
        {
          this.VM.Items.Add(new VKAudio()
          {
            IsCurrent = num == ForegroundPlaylistManager.Instance.CurrentTrack,
            artist = track.artist,
            title = track.title,
            url = track.url,
            cover = track.cover,
            UIIndex = num,
            duration = track.duration
          });
          ++num;
        }
      }
      else
        this.VM.LoadData(true);
    }

    private void HandleLoadingStatusUpdated(ProfileLoadingStatus status)
    {
      VisualStateManager.GoToState((Control) this.loading, status.ToString(), false);
    }

    private void AudioTrackUC_Tapped(object sender, TappedRoutedEventArgs e)
    {
      VKAudio dataContext = (sender as FrameworkElement).DataContext as VKAudio;
      int currentTrack = ForegroundPlaylistManager.Instance.CurrentTrack;
      int number = this.VM.Items.IndexOf(dataContext);
      if (currentTrack == number)
      {
        if (BackgroundMediaPlayer.Current.CurrentState == 3)
          BackgroundMediaPlayer.Current.Pause();
        else
          BackgroundMediaPlayer.Current.Play();
      }
      else
      {
        if (ForegroundPlaylistManager.Instance.Tracks.Count == 0)
        {
          foreach (VKAudio vkAudio in (Collection<VKAudio>) this.VM.Items)
          {
            // ISSUE: object of a compiler-generated type is created
            ForegroundPlaylistManager.Instance.AddTrack(new AudioHeader()
            {
              artist = vkAudio.artist,
              title = vkAudio.title,
              url = vkAudio.url,
              cover = vkAudio.cover,
              duration = vkAudio.duration
            });
          }
        }
        ForegroundPlaylistManager.Instance.PlayTrack(number);
      }
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///MusicPage.xaml"), (ComponentResourceLocation) 0);
      this.MainScroll = (ExtendedListView2) ((FrameworkElement) this).FindName("MainScroll");
      this.loading = (LoadingUC) ((FrameworkElement) this).FindName("loading");
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void Connect(int connectionId, object target)
    {
      if (connectionId == 1)
      {
        UIElement uiElement = (UIElement) target;
        WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement.add_Tapped), new Action<EventRegistrationToken>(uiElement.remove_Tapped), new TappedEventHandler(this.AudioTrackUC_Tapped));
      }
      this._contentLoaded = true;
    }
  }
}
