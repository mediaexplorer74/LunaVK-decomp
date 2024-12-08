// Decompiled with JetBrains decompiler
// Type: App1uwp.Framework.EventAggregator
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace App1uwp.Framework
{
  public class EventAggregator
  {
    public static Action<Action> DefaultPublicationThreadMarshaller = (Action<Action>) (action => Execute.ExecuteOnUIThread(action));
    private readonly List<EventAggregator.Handler> handlers = new List<EventAggregator.Handler>();
    private static EventAggregator _current;

    public static EventAggregator Instance
    {
      get
      {
        if (EventAggregator._current == null)
          EventAggregator._current = new EventAggregator();
        return EventAggregator._current;
      }
    }

    public Action<Action> PublicationThreadMarshaller { get; set; }

    public EventAggregator()
    {
      this.PublicationThreadMarshaller = EventAggregator.DefaultPublicationThreadMarshaller;
    }

    public void SubsribeEvent(object instance)
    {
      lock (this.handlers)
      {
        if (this.handlers.Any<EventAggregator.Handler>((Func<EventAggregator.Handler, bool>) (x => x.Matches(instance))))
          return;
        this.handlers.Add(new EventAggregator.Handler(instance));
      }
    }

    public void UnSubsribeEvent(object instance)
    {
      lock (this.handlers)
      {
        EventAggregator.Handler handler = this.handlers.FirstOrDefault<EventAggregator.Handler>((Func<EventAggregator.Handler, bool>) (x => x.Matches(instance)));
        if (handler == null)
          return;
        this.handlers.Remove(handler);
      }
    }

    public void PublishEvent(object message)
    {
      this.Publish(message, this.PublicationThreadMarshaller);
    }

    private void Publish(object message, Action<Action> marshal)
    {
      EventAggregator.Handler[] toNotify;
      lock (this.handlers)
        toNotify = this.handlers.ToArray();
      marshal((Action) (() =>
      {
        Type messageType = message.GetType();
        List<EventAggregator.Handler> list = ((IEnumerable<EventAggregator.Handler>) toNotify).Where<EventAggregator.Handler>((Func<EventAggregator.Handler, bool>) (handler => !handler.Handle(messageType, message))).ToList<EventAggregator.Handler>();
        if (!list.Any<EventAggregator.Handler>())
          return;
        lock (this.handlers)
          list.Apply<EventAggregator.Handler>((Action<EventAggregator.Handler>) (x => this.handlers.Remove(x)));
      }));
    }

    protected class Handler
    {
      private readonly Dictionary<Type, MethodInfo> supportedHandlers = new Dictionary<Type, MethodInfo>();
      private readonly WeakReference reference;

      public Handler(object handler)
      {
        this.reference = new WeakReference(handler);
        List<MethodInfo> list = handler.GetType().GetRuntimeMethods().Where<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name.StartsWith("OnEventHandler"))).ToList<MethodInfo>();
        for (int index = 0; index < list.Count; ++index)
        {
          MethodInfo methodInfo = list[index];
          this.supportedHandlers[methodInfo.GetParameters()[0].ParameterType] = methodInfo;
        }
      }

      public bool Matches(object instance) => this.reference.Target == instance;

      public bool Handle(Type messageType, object message)
      {
        object target = this.reference.Target;
        if (target == null)
          return false;
        foreach (KeyValuePair<Type, MethodInfo> supportedHandler in this.supportedHandlers)
        {
          if (supportedHandler.Key == messageType)
          {
            supportedHandler.Value.Invoke(target, new object[1]
            {
              message
            });
            return true;
          }
        }
        return true;
      }
    }
  }
}
