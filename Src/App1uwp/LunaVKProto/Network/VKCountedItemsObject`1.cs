﻿// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.VKCountedItemsObject`1
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using System.Collections.Generic;

#nullable disable
namespace App1uwp.Network
{
  public class VKCountedItemsObject<T>
  {
    public uint count { get; set; }

    public List<T> items { get; set; }
  }
}