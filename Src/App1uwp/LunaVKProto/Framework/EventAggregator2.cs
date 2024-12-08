// Decompiled with JetBrains decompiler
// Type: App1uwp.Framework.EventAggregator2
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.DataVM;
using App1uwp.Network.Enums;
using System;

#nullable disable
namespace App1uwp.Framework
{
  public class EventAggregator2
  {
    private static EventAggregator2 _instance;
    public EventHandler<EventAggregator2.MessageEvent> MessageEventAction;

    public static EventAggregator2 Instance
    {
      get
      {
        if (EventAggregator2._instance == null)
          EventAggregator2._instance = new EventAggregator2();
        return EventAggregator2._instance;
      }
    }

    public void PublishMsg(VKMessageVM msg, LongPollServerUpdateType evnt)
    {
    }

    public class MessageEvent
    {
      private VKMessageVM msg { get; set; }

      private LongPollServerUpdateType action { get; set; }
    }
  }
}
