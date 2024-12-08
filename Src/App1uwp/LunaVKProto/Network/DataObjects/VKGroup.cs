// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.DataObjects.VKGroup
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.Enums;
using App1uwp.Network.json;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

#nullable disable
namespace App1uwp.Network.DataObjects
{
  public class VKGroup : VKBaseDataForGroupOrUser
  {
    public string name { get; set; }

    public string screen_name { get; set; }

    public VKGroupIsClosed is_closed { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool is_admin { get; set; }

    public VKAdminLevel admin_level { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool is_member { get; set; }

    public int invited_by { get; set; }

    [JsonConverter(typeof (StringEnumConverter))]
    public VKGroupType type { get; set; }

    public string activity { get; set; }

    public int age_limits { get; set; }

    public VKBanInfo ban_info { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool can_create_topic { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool can_message { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool can_upload_doc { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool can_upload_video { get; set; }

    public VKCity city { get; set; }

    public List<VKGroupContact> contacts { get; set; }

    public VKCountry country { get; set; }

    public VKCover cover { get; set; }

    public string description { get; set; }

    public int fixed_post { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool has_photo { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool is_messages_blocked { get; set; }

    public List<VKGroupLink> links { get; set; }

    public int main_album_id { get; set; }

    public int main_section { get; set; }

    public VKGroupMarket market { get; set; }

    public VKGroupMembershipType member_status { get; set; }

    public int members_count { get; set; }

    public VKPlace place { get; set; }

    public string public_date_label { get; set; }

    public string site { get; set; }

    public int start_date { get; set; }

    public int finish_date { get; set; }

    public string status { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool trending { get; set; }

    public string wiki_page { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool is_subscribed { get; set; }

    public override string Title => this.name;
  }
}
