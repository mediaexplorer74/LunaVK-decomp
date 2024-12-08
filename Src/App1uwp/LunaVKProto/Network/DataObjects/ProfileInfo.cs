// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.DataObjects.ProfileInfo
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

#nullable disable
namespace App1uwp.Network.DataObjects
{
  public class ProfileInfo
  {
    private string _fName = "";
    private string _lName = "";
    private string _sName = "";

    public string photo_max { get; set; }

    public string first_name
    {
      get => this._fName;
      set => this._fName = value;
    }

    public string last_name
    {
      get => this._lName;
      set => this._lName = value;
    }

    public string screen_name
    {
      get => this._sName;
      set => this._sName = value;
    }

    public int sex { get; set; }

    public int relation { get; set; }

    public int relation_pending { get; set; }

    public string bdate { get; set; }

    public int bdate_visibility { get; set; }

    public string home_town { get; set; }

    public string phone { get; set; }
  }
}
