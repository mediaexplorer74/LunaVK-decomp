// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.DataObjects.VKVideoBase
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.json;
using Newtonsoft.Json;
using System;

#nullable disable
namespace App1uwp.Network.DataObjects
{
  public class VKVideoBase
  {
    public long id { get; set; }

    public long owner_id { get; set; }

    public string title { get; set; }

    public string description { get; set; }

    [JsonConverter(typeof (SecondsToTimeSpanConverter))]
    public TimeSpan duration { get; set; }

    public string photo_130 { get; set; }

    public string photo_320 { get; set; }

    public string photo_640 { get; set; }

    public string photo_800 { get; set; }

    [JsonConverter(typeof (UnixtimeToDateTimeConverter))]
    public DateTime date { get; set; }

    [JsonConverter(typeof (UnixtimeToDateTimeConverter))]
    public DateTime adding_date { get; set; }

    public long views { get; set; }

    public long comments { get; set; }

    public string player { get; set; }

    public string platform { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool can_edit { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool can_add { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool is_private { get; set; }

    public string access_key { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool processing { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool live { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool upcoming { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool repeat { get; set; }

    public string first_frame_320 { get; set; }

    public string first_frame_160 { get; set; }

    public string first_frame_130 { get; set; }

    public string first_frame_800 { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool can_comment { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool can_repost { get; set; }

    public VKLikes likes { get; set; }

    public int width { get; set; }

    public int height { get; set; }

    public VKVideoBase.VideoFiles files { get; set; }

    public class VideoFiles
    {
      public string mp4_240 { get; set; }

      public string mp4_360 { get; set; }

      public string mp4_480 { get; set; }

      public string mp4_720 { get; set; }

      public string mp4_1080 { get; set; }
    }
  }
}
