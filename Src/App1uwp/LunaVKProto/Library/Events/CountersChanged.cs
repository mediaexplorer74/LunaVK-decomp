// Decompiled with JetBrains decompiler
// Type: App1uwp.Library.Events.CountersChanged
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

#nullable disable
namespace App1uwp.Library.Events
{
  public class CountersChanged
  {
    public CountersChanged.OwnCounters Counts { get; set; }

    public CountersChanged() => this.Counts = new CountersChanged.OwnCounters();

    public CountersChanged(CountersChanged.OwnCounters o) => this.Counts = o;

    public class OwnCounters
    {
      public int friends { get; set; }

      public int messages { get; set; }

      public int photos { get; set; }

      public int videos { get; set; }

      public int groups { get; set; }

      public int notifications { get; set; }

      public int sdk { get; set; }

      public int app_requests { get; set; }
    }
  }
}
