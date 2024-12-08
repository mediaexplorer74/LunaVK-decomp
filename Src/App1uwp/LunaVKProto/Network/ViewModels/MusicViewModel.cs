// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.ViewModels.MusicViewModel
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.DataObjects;
using App1uwp.Network.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

#nullable disable
namespace App1uwp.Network.ViewModels
{
  public class MusicViewModel
  {
    private bool InLoading;
    private int offset;
    private uint maximum;
    private int OwnerId;

    public ObservableCollection<VKAudio> Items { get; private set; }

    public Action Refresh { get; private set; }

    public Action<ProfileLoadingStatus> LoadingStatusUpdated { get; set; }

    public MusicViewModel(int owner)
    {
      this.Items = new ObservableCollection<VKAudio>();
      this.OwnerId = owner;
      this.Refresh = (Action) (() => this.LoadData(true));
    }

    public async void LoadData(bool reload = false, Action<bool> calback = null)
    {
      this.InLoading = true;
      if (reload)
      {
        this.offset = 0;
        if (this.LoadingStatusUpdated != null)
          this.LoadingStatusUpdated(ProfileLoadingStatus.Reloading);
      }
      else if (this.LoadingStatusUpdated != null)
        this.LoadingStatusUpdated(ProfileLoadingStatus.Loading);
      Dictionary<string, string> parameters = new Dictionary<string, string>();
      parameters["owner_id"] = this.OwnerId.ToString();
      parameters["audio_offset"] = this.offset.ToString();
      parameters["audio_count"] = "20";
      this.offset += 20;
      VKResponse<VKCountedItemsObject<VKAudio>> response = await RequestsDispatcher.GetResponse<VKCountedItemsObject<VKAudio>>("execute.getMusicPage", parameters);
      if (response.error.error_code != VKErrors.None)
      {
        if (this.LoadingStatusUpdated != null)
          this.LoadingStatusUpdated(ProfileLoadingStatus.LoadingFailed);
        this.InLoading = false;
      }
      else
      {
        if (this.LoadingStatusUpdated != null)
          this.LoadingStatusUpdated(ProfileLoadingStatus.Loaded);
        int i = 0;
        foreach (VKAudio vkAudio in response.response.items)
        {
          vkAudio.UIIndex = i;
          this.Items.Add(vkAudio);
          ++i;
        }
        if (!reload)
          return;
        this.maximum = response.response.count;
      }
    }
  }
}
