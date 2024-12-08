// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.DataObjects.VKLastSeen
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.Enums;
using App1uwp.Network.json;
using Newtonsoft.Json;
using System;

#nullable disable
namespace App1uwp.Network.DataObjects
{
  public sealed class VKLastSeen
  {
    public bool Online;
    public string OnlineApp;

    public VKPlatform platform { get; set; }

    [JsonConverter(typeof (UnixtimeToDateTimeConverter))]
    public DateTime time { get; set; }

    public string AppString
    {
      get
      {
        string appString = "";
        switch (this.OnlineApp)
        {
          case "3502561":
            appString = "Windows Phone";
            break;
          case "3140623":
            appString = "iPhone";
            break;
          case "2274003":
            appString = "Android";
            break;
          case "2685278":
            appString = "KateMobile";
            break;
          case "3265802":
            appString = "Api console";
            break;
          case "3682744":
            appString = "iPad";
            break;
          case "5674548":
            appString = "ВКонтакте Pro";
            break;
          case "5316500":
            appString = "VFeed pro";
            break;
          case "4542624":
            appString = "Black VK";
            break;
          case "5632485":
            appString = "Space VK";
            break;
          case "6244854":
            appString = "Luna VK";
            break;
        }
        return appString;
      }
    }
  }
}
