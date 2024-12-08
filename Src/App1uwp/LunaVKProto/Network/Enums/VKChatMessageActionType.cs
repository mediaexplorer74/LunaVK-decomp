// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.Enums.VKChatMessageActionType
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.json;
using Newtonsoft.Json;

#nullable disable
namespace App1uwp.Network.Enums
{
  [JsonConverter(typeof (VKChatMessageActionTypeConverter))]
  public enum VKChatMessageActionType : byte
  {
    None,
    ChatPhotoUpdate,
    ChatPhotoRemove,
    ChatCreate,
    ChatTitleUpdate,
    ChatInviteUser,
    ChatKickUser,
    ChatPinMessage,
    ChatUnpinMessage,
    ChatInviteUserByLink,
  }
}
