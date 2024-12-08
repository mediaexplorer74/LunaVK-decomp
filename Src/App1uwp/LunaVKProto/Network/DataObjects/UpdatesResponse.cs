// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.DataObjects.UpdatesResponse
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.Enums;
using System.Collections.Generic;

#nullable disable
namespace App1uwp.Network.DataObjects
{
  public class UpdatesResponse
  {
    public long ts { get; set; }

    public List<UpdatesResponse.LongPollServerUpdateData> Updates { get; set; }

    public class LongPollServerUpdateData
    {
      public LongPollServerUpdateType UpdateType { get; set; }

      public bool IsHistoricData { get; set; }

      public int user_id { get; set; }

      public long message_id { get; set; }

      public long chat_id { get; set; }

      public long timestamp { get; set; }

      public string text { get; set; }

      public bool @out { get; set; }

      public VKMessage message { get; set; }

      public VKProfileBase user { get; set; }

      public int Platform { get; set; }

      public int Counter { get; set; }

      public bool isChat { get; set; }

      public bool hasAttachOrForward { get; set; }

      public override string ToString()
      {
        return string.Format("UpdateType:{0} user_id:{1} message_id:{2} chat_id:{3} is_chat:{4}", (object) this.UpdateType, (object) this.user_id, (object) this.message_id, (object) this.chat_id, (object) this.isChat);
      }
    }
  }
}
