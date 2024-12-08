// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.DataObjects.VKAudio
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Audio;
using App1uwp.Network.Enums;
using App1uwp.Network.ViewModels;
using App1uwp.Utils;
using Windows.Media.Playback;
using Windows.UI.Xaml;

#nullable disable
namespace App1uwp.Network.DataObjects
{
  public class VKAudio : ViewModelBase
  {
    public bool IsCurrent;

    public int id { get; set; }

    public int owner_id { get; set; }

    public string artist { get; set; }

    public string title { get; set; }

    public int duration { get; set; }

    public string url { get; set; }

    public int lyrics_id { get; set; }

    public int album_id { get; set; }

    public VKAudioGenre genre_id { get; set; }

    public int date { get; set; }

    public int no_search { get; set; }

    public string cover { get; set; }

    public string UIDuration => UIStringFormatterHelper.FormatDuration(this.duration);

    public Visibility PlayIconVisibility
    {
      get
      {
        return ForegroundPlaylistManager.Instance.CurrentTrack == this.UIIndex && BackgroundMediaPlayer.Current.CurrentState != 3 ? (Visibility) 0 : (Visibility) 1;
      }
    }

    public Visibility PauseIconVisibility
    {
      get
      {
        return ForegroundPlaylistManager.Instance.CurrentTrack == this.UIIndex && BackgroundMediaPlayer.Current.CurrentState == 3 ? (Visibility) 0 : (Visibility) 1;
      }
    }

    public Visibility BackVisibility
    {
      get
      {
        return ForegroundPlaylistManager.Instance.CurrentTrack == this.UIIndex ? (Visibility) 0 : (Visibility) 1;
      }
    }

    public int UIIndex { get; set; }

    public void UpdateUI()
    {
      this.NotifyPropertyChanged("PlayIconVisibility");
      this.NotifyPropertyChanged("PauseIconVisibility");
      this.NotifyPropertyChanged("BackVisibility");
    }
  }
}
