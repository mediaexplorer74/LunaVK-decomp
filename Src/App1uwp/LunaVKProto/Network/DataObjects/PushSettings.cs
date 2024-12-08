// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.DataObjects.PushSettings
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using Newtonsoft.Json;
using System.Collections.Generic;

#nullable disable
namespace App1uwp.Network.DataObjects
{
  public class PushSettings
  {
    public static readonly string On = "on";
    public static readonly string Off = "off";

    public bool msg { get; set; }

    public bool msg_no_text { get; set; }

    public bool chat { get; set; }

    public bool chat_no_text { get; set; }

    public bool friend { get; set; }

    public bool friend_mutual { get; set; }

    public bool friend_found { get; set; }

    public bool friend_accepted { get; set; }

    public bool reply { get; set; }

    public bool comment { get; set; }

    public bool comment_fr_of_fr { get; set; }

    public bool mention { get; set; }

    public bool mention_fr_of_fr { get; set; }

    public bool like { get; set; }

    public bool like_fr_of_fr { get; set; }

    public bool repost { get; set; }

    public bool repost_fr_of_fr { get; set; }

    public bool wall_post { get; set; }

    public bool wall_publish { get; set; }

    public bool group_invite { get; set; }

    public bool group_accepted { get; set; }

    public bool event_soon { get; set; }

    public bool tag_photo { get; set; }

    public bool tag_photo_fr_of_fr { get; set; }

    public bool app_request { get; set; }

    public bool sdk_open { get; set; }

    public bool new_post { get; set; }

    public bool birthday { get; set; }

    public override string ToString()
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      dictionary["msg"] = PushSettings.GetOnOffStr(this.msg);
      if (this.msg_no_text)
        dictionary["msg"] = "no_text";
      dictionary["chat"] = PushSettings.GetOnOffStr(this.chat);
      if (this.chat_no_text)
        dictionary["chat"] = "no_text";
      dictionary["friend"] = PushSettings.GetOnOffStr(this.friend);
      if (this.friend_mutual)
        dictionary["friend"] = "mutual";
      dictionary["friend_found"] = PushSettings.GetOnOffStr(this.friend_found);
      dictionary["friend_accepted"] = "on";
      dictionary["reply"] = PushSettings.GetOnOffStr(this.reply);
      dictionary["comment"] = PushSettings.GetOnOffStr(this.comment);
      if (this.comment_fr_of_fr)
        dictionary["comment"] = "fr_of_fr";
      dictionary["mention"] = PushSettings.GetOnOffStr(this.mention);
      if (this.mention_fr_of_fr)
        dictionary["mention"] = "fr_of_fr";
      dictionary["like"] = PushSettings.GetOnOffStr(this.like);
      if (this.like_fr_of_fr)
        dictionary["like"] = "fr_of_fr";
      dictionary["repost"] = PushSettings.GetOnOffStr(this.repost);
      if (this.repost_fr_of_fr)
        dictionary["repost"] = "fr_of_fr";
      dictionary["wall_post"] = PushSettings.GetOnOffStr(this.wall_post);
      dictionary["wall_publish"] = "on";
      dictionary["group_invite"] = PushSettings.GetOnOffStr(this.group_invite);
      dictionary["group_accepted"] = "on";
      dictionary["event_soon"] = PushSettings.GetOnOffStr(this.event_soon);
      dictionary["tag_photo"] = PushSettings.GetOnOffStr(this.tag_photo);
      if (this.tag_photo_fr_of_fr)
        dictionary["tag_photo"] = "fr_of_fr";
      dictionary["app_request"] = PushSettings.GetOnOffStr(this.app_request);
      dictionary["sdk_open"] = "on";
      dictionary["open_url"] = "on";
      dictionary["new_post"] = "on";
      dictionary["birthday"] = PushSettings.GetOnOffStr(this.birthday);
      dictionary["money_transfer"] = "on";
      return JsonConvert.SerializeObject((object) dictionary);
    }

    public static string GetOnOffStr(bool b) => !b ? PushSettings.Off : PushSettings.On;

    public PushSettings()
    {
      this.msg = true;
      this.chat = true;
      this.friend = true;
      this.friend_found = true;
      this.friend_accepted = true;
      this.reply = true;
      this.comment = true;
      this.mention = true;
      this.like = true;
      this.repost = true;
      this.wall_post = true;
      this.wall_publish = true;
      this.group_invite = true;
      this.group_accepted = true;
      this.event_soon = true;
      this.app_request = true;
      this.new_post = true;
      this.birthday = true;
    }
  }
}
