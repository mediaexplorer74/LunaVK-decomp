// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.DataObjects.VKPhoto
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.json;
using Newtonsoft.Json;
using System;

#nullable disable
namespace App1uwp.Network.DataObjects
{
  public class VKPhoto
  {
    public ulong id { get; set; }

    public long album_id { get; set; }

    public long owner_id { get; set; }

    public long user_id { get; set; }

    public string text { get; set; }

    [JsonConverter(typeof (UnixtimeToDateTimeConverter))]
    public DateTime date { get; set; }

    public string photo_75 { get; set; }

    public string photo_130 { get; set; }

    public string photo_604 { get; set; }

    public string photo_807 { get; set; }

    public string photo_1280 { get; set; }

    public string photo_2560 { get; set; }

    public int width { get; set; }

    public int height { get; set; }
  }
}
