// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.DataObjects.VKProfileBase
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.Enums;
using App1uwp.Network.json;
using Newtonsoft.Json;
using Windows.Foundation;

#nullable disable
namespace App1uwp.Network.DataObjects
{
  public class VKProfileBase : VKBaseDataForGroupOrUser
  {
    public VKProfileBase.Education education { get; set; }

    public VKProfileBase.Exports exports { get; set; }

    public string first_name { get; set; }

    public string last_name { get; set; }

    public string about { get; set; }

    public string activity { get; set; }

    public string activities { get; set; }

    public string bdate { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool blacklisted { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool blacklisted_by_me { get; set; }

    public string books { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool can_see_audio { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool can_send_friend_request { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool can_write_private_message { get; set; }

    public VKCity city { get; set; }

    public VKCountry country { get; set; }

    public string first_name_acc { get; set; }

    public string first_name_dat { get; set; }

    public string first_name_gen { get; set; }

    public string last_name_acc { get; set; }

    public string last_name_dat { get; set; }

    public string last_name_gen { get; set; }

    public int followers_count { get; set; }

    public VKUsetMembershipType friend_status { get; set; }

    public string games { get; set; }

    public string home_town { get; set; }

    public string interests { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool is_friend { get; set; }

    public VKLastSeen last_seen { get; set; }

    public string movies { get; set; }

    public string music { get; set; }

    public string nickname { get; set; }

    public VKOccupation occupation { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool online { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool online_mobile { get; set; }

    public string online_app { get; set; }

    public string photo_id { get; set; }

    public string photo_max { get; set; }

    public string photo_max_orig { get; set; }

    public string quotes { get; set; }

    public VKUserSex sex { get; set; }

    public string status { get; set; }

    public VKProfileBase.CropPhoto crop_photo { get; set; }

    public string NameAcc => this.first_name_acc + " " + this.last_name_acc;

    public bool IsFemale => this.sex == VKUserSex.Female;

    public string FullName => this.first_name + " " + this.last_name;

    public override string Title => this.first_name + " " + this.last_name;

    public class CropPhoto
    {
      public VKPhoto photo { get; set; }

      public VKProfileBase.CropPhoto.CropRect crop { get; set; }

      public VKProfileBase.CropPhoto.CropRect rect { get; set; }

      public class CropRect
      {
        public double x { get; set; }

        public double y { get; set; }

        public double x2 { get; set; }

        public double y2 { get; set; }

        public Rect GetCroppingRectangle(double width, double height)
        {
          double x = this.x * width / 100.0;
          double num1 = this.x2 * width / 100.0;
          double num2 = this.y * height / 100.0;
          double num3 = this.y2 * height / 100.0;
          double width1 = num1 - x;
          double height1 = num3 - num2;
          return new Rect(x, num2 + 94.0, width1, height1);
        }
      }
    }

    public class Exports
    {
      public int twitter { get; set; }

      public int facebook { get; set; }

      public int livejournal { get; set; }

      public int instagram { get; set; }
    }

    public class Education
    {
      public int university { get; set; }

      public string university_name { get; set; }

      public int faculty { get; set; }

      public string faculty_name { get; set; }

      public int graduation { get; set; }
    }
  }
}
