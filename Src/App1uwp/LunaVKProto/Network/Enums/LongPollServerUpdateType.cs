// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.Enums.LongPollServerUpdateType
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

#nullable disable
namespace App1uwp.Network.Enums
{
  public enum LongPollServerUpdateType
  {
    ProcessReplaceFlag = 1,
    ProcessAddFlags = 2,
    ClearFlags = 3,
    MessageAdd = 4,
    MessageUpdate = 5,
    IncomingMessagesRead = 6,
    OutcominggMessagesRead = 7,
    UserBecameOnline = 8,
    UserBecameOffline = 9,
    MessageHasBeenRead = 10, // 0x0000000A
    MessageHasBeenAdded = 11, // 0x0000000B
    ChatParamsWereChanged = 12, // 0x0000000C
    ChatDeleteMsgs = 13, // 0x0000000D
    RestoreMsgs = 14, // 0x0000000E
    MessageHasBeenDeleted = 20, // 0x00000014
    ChatParamsChanged = 51, // 0x00000033
    UserIsTyping = 61, // 0x0000003D
    UserIsTypingInChat = 62, // 0x0000003E
    UserCalled = 70, // 0x00000046
    NewCounter = 80, // 0x00000050
    processnotyfysettings = 114, // 0x00000072
    MessageHasBeenRestored = 1000000, // 0x000F4240
  }
}
