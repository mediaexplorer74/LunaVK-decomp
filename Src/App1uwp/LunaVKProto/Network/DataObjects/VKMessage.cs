// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.DataObjects.VKMessage
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.Enums;
using App1uwp.Network.json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

#nullable disable
namespace App1uwp.Network.DataObjects
{
  public class VKMessage
  {
    public int id { get; set; }

    public int user_id { get; set; }

    public int from_id { get; set; }

    [JsonConverter(typeof (UnixtimeToDateTimeConverter))]
    public DateTime date { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool read_state { get; set; }

    public VKMessageType @out { get; set; }

    public string title { get; set; }

    public string body { get; set; }

    public VKGeo geo { get; set; }

    public List<VKAttachment> attachments { get; set; }

    public List<VKMessage> fwd_messages { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool emoji { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool important { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool deleted { get; set; }

    public int random_id { get; set; }

    public int chat_id { get; set; }

    public List<int> chat_active { get; set; }

    public VKPushSettings push_settings { get; set; }

    public int users_count { get; set; }

    public int admin_id { get; set; }

    public VKChatMessageActionType action { get; set; }

    public int action_mid { get; set; }

    public string action_email { get; set; }

    public string action_text { get; set; }

    public string photo_50 { get; set; }

    public string photo_100 { get; set; }

    public string photo_200 { get; set; }

    [JsonConverter(typeof (UnixtimeToDateTimeConverter))]
    public DateTime update_time { get; set; }
  }
}
