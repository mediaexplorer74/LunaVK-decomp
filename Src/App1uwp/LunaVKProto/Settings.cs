// Decompiled with JetBrains decompiler
// Type: App1uwp.Settings
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Library;
using App1uwp.Network;
using App1uwp.Network.DataObjects;
using App1uwp.Network.ViewModels;
using System;
using System.Threading.Tasks;

#nullable disable
namespace App1uwp
{
  public class Settings
  {
    private static Settings _instance;
    public AutorizationData auth;

    public static Settings Instance
    {
      get => Settings._instance;
      set => Settings._instance = value;
    }

    public VKProfileBase LoggedInUser { get; set; }

    public bool PushNotificationsEnabled { get; set; }

    public string LastPushNotificationsUri { get; set; }

    public bool NotificationsEnabled { get; set; }

    public bool VibrationsEnabled { get; set; }

    public bool SoundEnabled { get; set; }

    public PushSettings PushSettings { get; set; }

    public DateTime PushNotificationsBlockedUntil { get; set; }

    public Threelen BackgroundType { get; set; }

    public Threelen GifAutoplayType { get; set; }

    public Threelen FriendListOrder { get; set; }

    public int UIScale { get; set; }

    public int RoundAvatar { get; set; }

    public bool UseProxy { get; set; }

    public string ProxyAdress { get; set; }

    public short AccentColor { get; set; }

    public bool MenuDivider { get; set; }

    public bool CmdBarDivider { get; set; }

    public int ServerMinusLocalTimeDelta { get; set; }

    public Settings()
    {
      this.auth = new AutorizationData();
      this.LoggedInUser = new VKProfileBase();
      this.PushSettings = new PushSettings();
      this.PushNotificationsEnabled = true;
      this.NotificationsEnabled = true;
      this.SoundEnabled = true;
      this.GifAutoplayType = Threelen.Third;
      this.BackgroundType = Threelen.Third;
      this.UIScale = 100;
      this.RoundAvatar = 10;
      this.UseProxy = false;
      this.ProxyAdress = "";
      this.AccentColor = (short) 0;
      this.MenuDivider = true;
      this.LastPushNotificationsUri = "";
      this.CmdBarDivider = true;
    }

    public bool IsAuthorized => !string.IsNullOrEmpty(this.auth.AccessToken);

    public async Task Load()
    {
      Settings._instance = await FileHelper.ReadTextFromFile<Settings>("_Settings");
      if (Settings._instance == null)
        Settings._instance = new Settings();
      if (!Settings.Instance.IsAuthorized)
        return;
      this.GetBaseData();
      LongPollServerService.Instance.GetLongPollServer();
    }

    public async void Save()
    {
      int num = await FileHelper.WriteTextToFile<Settings>("_Settings", Settings.Instance) ? 1 : 0;
    }

    public void Remove() => FileHelper.Remove("_Settings");

    public async void GetBaseData()
    {
      string code = "var u = API.users.get({\"fields\":\"photo_200,status\"});return u[0];";
      VKResponse<VKProfileBase> temp = await RequestsDispatcher.Execute<VKProfileBase>(code);
      if (temp == null)
        return;
      Settings.Instance.LoggedInUser = temp.response;
      MenuViewModel.Instance.UserName = Settings.Instance.LoggedInUser.FullName;
      MenuViewModel.Instance.UserPhoto = Settings.Instance.LoggedInUser.photo_200;
      MenuViewModel.Instance.Status = Settings.Instance.LoggedInUser.status;
    }
  }
}
