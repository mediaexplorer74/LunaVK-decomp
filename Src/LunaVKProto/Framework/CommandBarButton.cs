﻿// Decompiled with JetBrains decompiler
// Type: App1uwp.Framework.CommandBarButton
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using System.Windows.Input;

#nullable disable
namespace App1uwp.Framework
{
  public class CommandBarButton
  {
    public string Label { get; set; }

    public string Icon { get; set; }

    public ICommand Command { get; set; }
  }
}