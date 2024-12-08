// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.DataObjects.VKDocument
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.Enums;

#nullable disable
namespace App1uwp.Network.DataObjects
{
  public class VKDocument
  {
    public long id { get; set; }

    public long owner_id { get; set; }

    public string title { get; set; }

    public long size { get; set; }

    public string ext { get; set; }

    public string url { get; set; }

    public VKDocumentType type { get; set; }

    public int date { get; set; }

    public string access_key { get; set; }

    public DocPreview preview { get; set; }

    public bool IsGraffiti => this.preview?.graffiti != null;
  }
}
