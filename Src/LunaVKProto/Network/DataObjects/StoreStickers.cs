// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.DataObjects.StoreStickers
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using System.Collections.Generic;

#nullable disable
namespace App1uwp.Network.DataObjects
{
  public class StoreStickers
  {
    private List<int> _sticker_ids;

    public string base_url { get; set; }

    public List<int> sticker_ids
    {
      get => this._sticker_ids;
      set
      {
        this._sticker_ids = value;
        this.sticker_ids_str = new List<string>();
        foreach (int num in value)
          this.sticker_ids_str.Add(num.ToString());
      }
    }

    public List<string> sticker_ids_str { get; set; }
  }
}
