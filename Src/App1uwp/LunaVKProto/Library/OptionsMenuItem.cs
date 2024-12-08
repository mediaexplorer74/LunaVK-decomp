// Decompiled with JetBrains decompiler
// Type: App1uwp.Library.OptionsMenuItem
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.ViewModels;

#nullable disable
namespace App1uwp.Library
{
  public class OptionsMenuItem : ViewModelBase
  {
    private string _icon;

    public string Icon
    {
      get => this._icon;
      set
      {
        if (value == this._icon)
          return;
        this._icon = value;
        this.NotifyPropertyChanged(nameof (Icon));
      }
    }
  }
}
