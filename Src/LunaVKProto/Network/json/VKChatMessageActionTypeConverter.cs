// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.json.VKChatMessageActionTypeConverter
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.Enums;
using Newtonsoft.Json;
using System;

#nullable disable
namespace App1uwp.Network.json
{
  public class VKChatMessageActionTypeConverter : JsonConverter
  {
    public override bool CanConvert(Type objectType)
    {
      return objectType == typeof (VKChatMessageActionType);
    }

    public override object ReadJson(
      JsonReader reader,
      Type objectType,
      object existingValue,
      JsonSerializer serializer)
    {
      switch (reader.Value.ToString())
      {
        case "chat_photo_update":
          return (object) VKChatMessageActionType.ChatPhotoUpdate;
        case "chat_photo_remove":
          return (object) VKChatMessageActionType.ChatPhotoRemove;
        case "chat_create":
          return (object) VKChatMessageActionType.ChatCreate;
        case "chat_title_update":
          return (object) VKChatMessageActionType.ChatTitleUpdate;
        case "chat_invite_user":
          return (object) VKChatMessageActionType.ChatInviteUser;
        case "chat_kick_user":
          return (object) VKChatMessageActionType.ChatKickUser;
        case "chat_pin_message":
          return (object) VKChatMessageActionType.ChatPinMessage;
        case "chat_unpin_message":
          return (object) VKChatMessageActionType.ChatUnpinMessage;
        case "chat_invite_user_by_link":
          return (object) VKChatMessageActionType.ChatInviteUserByLink;
        default:
          return (object) VKChatMessageActionType.None;
      }
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
      switch ((VKChatMessageActionType) value)
      {
        case VKChatMessageActionType.ChatPhotoUpdate:
          writer.WriteValue("chat_photo_update");
          break;
        case VKChatMessageActionType.ChatPhotoRemove:
          writer.WriteValue("chat_photo_remove");
          break;
        case VKChatMessageActionType.ChatCreate:
          writer.WriteValue("chat_create");
          break;
        case VKChatMessageActionType.ChatTitleUpdate:
          writer.WriteValue("chat_title_update");
          break;
        case VKChatMessageActionType.ChatInviteUser:
          writer.WriteValue("chat_invite_user");
          break;
        case VKChatMessageActionType.ChatKickUser:
          writer.WriteValue("chat_kick_user");
          break;
        default:
          writer.WriteValue("none");
          break;
      }
    }
  }
}
