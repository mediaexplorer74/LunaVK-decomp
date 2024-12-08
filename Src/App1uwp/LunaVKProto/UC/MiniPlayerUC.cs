// Decompiled with JetBrains decompiler
// Type: App1uwp.UC.MiniPlayerUC
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Audio;
using BackgroundAudio;
using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;

#nullable disable
namespace App1uwp.UC
{
  public sealed class MiniPlayerUC : UserControl, IComponentConnector
  {
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBlock TrackName;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBlock ArtistName;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private IconUC _icon;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public MiniPlayerUC()
    {
      this.InitializeComponent();
      ForegroundPlaylistManager.Instance.TrackChanged += new EventHandler<int>(this.CurrentTrackChanged);
      ForegroundPlaylistManager.Instance.CurrentStateChanged += new EventHandler<MediaPlayerState>(this.CurrentStateChanged);
    }

    private void CurrentStateChanged(object sender, MediaPlayerState state)
    {
      if (state == 3)
      {
        this._icon.put_Glyph("\uE769");
      }
      else
      {
        if (state != 4)
          return;
        this._icon.put_Glyph("\uE768");
      }
    }

    private void CurrentTrackChanged(object sender, int index)
    {
      ((UIElement) this).put_Visibility((Visibility) 0);
      // ISSUE: variable of a compiler-generated type
      AudioHeader currentAudioTrack = ForegroundPlaylistManager.Instance.CurrentAudioTrack;
      this.TrackName.put_Text(currentAudioTrack.title);
      this.ArtistName.put_Text(currentAudioTrack.artist);
    }

    private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
    {
      if (BackgroundMediaPlayer.Current.CurrentState != 3)
        BackgroundMediaPlayer.Current.Play();
      else
        BackgroundMediaPlayer.Current.Pause();
    }

    private void Grid_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
    {
      if (e.Cumulative.Translation.X > 100.0)
      {
        ForegroundPlaylistManager.Instance.PlayNext();
      }
      else
      {
        if (e.Cumulative.Translation.X >= -100.0)
          return;
        ForegroundPlaylistManager.Instance.PlayPrev();
      }
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///UC/MiniPlayerUC.xaml"), (ComponentResourceLocation) 0);
      this.TrackName = (TextBlock) ((FrameworkElement) this).FindName("TrackName");
      this.ArtistName = (TextBlock) ((FrameworkElement) this).FindName("ArtistName");
      this._icon = (IconUC) ((FrameworkElement) this).FindName("_icon");
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          UIElement uiElement1 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement1.add_Tapped), new Action<EventRegistrationToken>(uiElement1.remove_Tapped), new TappedEventHandler(this.Grid_Tapped));
          break;
        case 2:
          UIElement uiElement2 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<ManipulationCompletedEventHandler>(new Func<ManipulationCompletedEventHandler, EventRegistrationToken>(uiElement2.add_ManipulationCompleted), new Action<EventRegistrationToken>(uiElement2.remove_ManipulationCompleted), new ManipulationCompletedEventHandler(this.Grid_ManipulationCompleted));
          break;
      }
      this._contentLoaded = true;
    }
  }
}
