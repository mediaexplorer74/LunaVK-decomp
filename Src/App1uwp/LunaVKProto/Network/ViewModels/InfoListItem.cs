// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.ViewModels.InfoListItem
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using System;

#nullable disable
namespace App1uwp.Network.ViewModels
{
  public class InfoListItem
  {
    public string IconUrl { get; set; }

    public string Text { get; set; }

    public string Preview1 { get; set; }

    public string Preview2 { get; set; }

    public string Preview3 { get; set; }

    public bool IsTiltEnabled { get; set; }

    public Action TapAction { get; set; }
  }
}
