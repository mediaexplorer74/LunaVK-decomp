// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.DataObjects.ChatInfo
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using System.Collections.Generic;

#nullable disable
namespace App1uwp.Network.DataObjects
{
  public class ChatInfo
  {
    public ChatInfo.Chat chat { get; set; }

    public List<VKProfileBase> chat_participants { get; set; }

    public class Chat
    {
      public List<int> users { get; set; }

      public string type { get; set; }

      public long id { get; set; }

      public string title { get; set; }

      public string admin_id { get; set; }

      public string photo_50 { get; set; }

      public string photo_100 { get; set; }

      public string photo_200 { get; set; }

      public VKPushSettings push_settings { get; set; }
    }
  }
}
