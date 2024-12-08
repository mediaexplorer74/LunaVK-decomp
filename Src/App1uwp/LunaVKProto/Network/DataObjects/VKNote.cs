// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.DataObjects.VKNote
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

#nullable disable
namespace App1uwp.Network.DataObjects
{
  public class VKNote
  {
    public long id { get; set; }

    public long owner_id { get; set; }

    public int comments { get; set; }

    public int read_comments { get; set; }

    public int date { get; set; }

    public string title { get; set; }

    public string view_url { get; set; }
  }
}
