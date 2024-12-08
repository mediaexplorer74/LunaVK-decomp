// Decompiled with JetBrains decompiler
// Type: App1uwp.Audio.ForegroundPlaylistManager
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
using BackgroundAudio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Playback;

#nullable disable
namespace App1uwp.Audio
{
  public class ForegroundPlaylistManager
  {
    public int CurrentTrack = -1;
    public EventHandler<int> TrackChanged;
    public List<AudioHeader> Tracks;
    public EventHandler<MediaPlayerState> CurrentStateChanged;
    private static ForegroundPlaylistManager _instance;

    public AudioHeader CurrentAudioTrack
    {
      get => this.CurrentTrack < 0 ? (AudioHeader) null : this.Tracks[this.CurrentTrack];
    }

    public static ForegroundPlaylistManager Instance
    {
      get
      {
        if (ForegroundPlaylistManager._instance == null)
          ForegroundPlaylistManager._instance = new ForegroundPlaylistManager();
        return ForegroundPlaylistManager._instance;
      }
    }

    public ForegroundPlaylistManager()
    {
      this.Tracks = new List<AudioHeader>();
      WindowsRuntimeMarshal.AddEventHandler<EventHandler<MediaPlayerDataReceivedEventArgs>>(new Func<EventHandler<MediaPlayerDataReceivedEventArgs>, EventRegistrationToken>(BackgroundMediaPlayer.add_MessageReceivedFromBackground), new Action<EventRegistrationToken>(BackgroundMediaPlayer.remove_MessageReceivedFromBackground), new EventHandler<MediaPlayerDataReceivedEventArgs>(this.BackgroundMediaPlayer_MessageReceivedFromBackground));
      MediaPlayer current = BackgroundMediaPlayer.Current;
      // ISSUE: method pointer
      WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<MediaPlayer, object>>(new Func<TypedEventHandler<MediaPlayer, object>, EventRegistrationToken>(current.add_CurrentStateChanged), new Action<EventRegistrationToken>(current.remove_CurrentStateChanged), new TypedEventHandler<MediaPlayer, object>((object) this, __methodptr(Current_CurrentStateChanged)));
    }

    private void Current_CurrentStateChanged(MediaPlayer sender, object args)
    {
      if (this.CurrentStateChanged == null)
        return;
      Execute.ExecuteOnUIThread((Action) (() => this.CurrentStateChanged((object) this, sender.CurrentState)));
    }

    private void BackgroundMediaPlayer_MessageReceivedFromBackground(
      object sender,
      MediaPlayerDataReceivedEventArgs e)
    {
      for (int index1 = 0; index1 < ((ICollection<KeyValuePair<string, object>>) e.Data).Count; ++index1)
      {
        // ISSUE: variable of a compiler-generated type
        BackgroundAudio.Constants constants1 = (BackgroundAudio.Constants) Enum.Parse(typeof (BackgroundAudio.Constants), ((IDictionary<string, object>) e.Data).Keys.ElementAt<string>(index1));
        // ISSUE: variable of a compiler-generated type
        BackgroundAudio.Constants constants2 = constants1;
        switch (constants2)
        {
          case BackgroundAudio.Constants.StartPlayback:
            int index = int.Parse(((IDictionary<string, object>) e.Data).Values.ElementAt<object>(index1) as string);
            this.CurrentTrack = index;
            if (this.TrackChanged != null)
            {
              Execute.ExecuteOnUIThread((Action) (() => this.TrackChanged((object) this, index)));
              break;
            }
            break;
        }
      }
    }

    public void ClearPlayList()
    {
      ValueSet valueSet = new ValueSet();
      ((IDictionary<string, object>) valueSet).Add(BackgroundAudio.Constants.ClearPlayList.ToString(), (object) "");
      BackgroundMediaPlayer.SendMessageToBackground(valueSet);
      this.Tracks.Clear();
    }

    public void AddTrack(AudioHeader audio)
    {
      ValueSet valueSet = new ValueSet();
      ((IDictionary<string, object>) valueSet).Add(BackgroundAudio.Constants.AddTrack.ToString(), (object) JsonConvert.SerializeObject((object) audio));
      BackgroundMediaPlayer.SendMessageToBackground(valueSet);
      this.Tracks.Add(audio);
    }

    public void PlayNext() => this.PlayTrack(this.CurrentTrack + 1);

    public void PlayPrev() => this.PlayTrack(this.CurrentTrack - 1);

    public async void PlayTrack(int number)
    {
      if (number >= this.Tracks.Count || number < 0)
        return;
      this.CurrentTrack = number;
      ValueSet valueSet = new ValueSet();
      ((IDictionary<string, object>) valueSet).Add(BackgroundAudio.Constants.StartPlayback.ToString(), (object) number.ToString());
      BackgroundMediaPlayer.SendMessageToBackground(valueSet);
    }
  }
}
