// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.DataObjects.VKStory
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.json;
using Newtonsoft.Json;
using System;

#nullable disable
namespace App1uwp.Network.DataObjects
{
  public class VKStory
  {
    public int id { get; set; }

    public int owner_id { get; set; }

    [JsonConverter(typeof (UnixtimeToDateTimeConverter))]
    public DateTime date { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool is_expired { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool is_deleted { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool can_see { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool seen { get; set; }

    public string type { get; set; }

    public VKPhoto photo { get; set; }

    public VKVideoBase video { get; set; }

    public VKStory.StoryLink link { get; set; }

    public int parent_story_owner_id { get; set; }

    public int parent_story_id { get; set; }

    public VKStory parent_story { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool can_reply { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool can_share { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool can_comment { get; set; }

    public class StoryLink
    {
      public string text { get; set; }

      public string url { get; set; }
    }
  }
}
