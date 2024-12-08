// Decompiled with JetBrains decompiler
// Type: App1uwp.Library.Events.MessageHasBeenReadEvent
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

#nullable disable
namespace App1uwp.Library.Events
{
  public class MessageHasBeenReadEvent
  {
    public long msg_id;
    public int user_id;
    public long chat_id;
    public bool is_chat;

    public MessageHasBeenReadEvent(long _msg_id, int _user_id, long _chat_id, bool _is_chat)
    {
      this.msg_id = _msg_id;
      this.user_id = _user_id;
      this.chat_id = _chat_id;
      this.is_chat = _is_chat;
    }
  }
}
