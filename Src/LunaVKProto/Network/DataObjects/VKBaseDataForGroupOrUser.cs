// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.DataObjects.VKBaseDataForGroupOrUser
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.Enums;
using App1uwp.Network.json;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

#nullable disable
namespace App1uwp.Network.DataObjects
{
  public class VKBaseDataForGroupOrUser
  {
    public int id { get; set; }

    [JsonConverter(typeof (StringEnumConverter))]
    public VKIsDeactivated deactivated { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool can_post { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool can_see_all_posts { get; set; }

    public VKCounters counters { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool is_favorite { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool is_hidden_from_feed { get; set; }

    public string photo_50 { get; set; }

    public string photo_100 { get; set; }

    public string photo_200 { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool verified { get; set; }

    public virtual string Title { get; set; }

    public string domain { get; set; }
  }
}
