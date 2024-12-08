// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.DataObjects.VKBaseDataForPostOrNews
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.Enums;
using App1uwp.Network.json;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

#nullable disable
namespace App1uwp.Network.DataObjects
{
  public class VKBaseDataForPostOrNews
  {
    [JsonConverter(typeof (UnixtimeToDateTimeConverter))]
    public DateTime date { get; set; }

    [JsonConverter(typeof (StringEnumConverter))]
    public VKNewsfeedPostType post_type { get; set; }

    public string text { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool can_edit { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool can_delete { get; set; }

    public VKComments comments { get; set; }

    public VKLikes likes { get; set; }

    public VKReposts reposts { get; set; }

    public List<VKAttachment> attachments { get; set; }

    public VKGeo geo { get; set; }

    public VKBaseDataForPostOrNews.VKViews views { get; set; }

    public VKBaseDataForGroupOrUser Owner { get; set; }

    public virtual long OwnerId { get; set; }

    public virtual long PostId { get; set; }

    public class VKViews
    {
      public long count { get; set; }
    }
  }
}
