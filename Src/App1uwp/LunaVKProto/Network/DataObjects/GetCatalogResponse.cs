// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.DataObjects.GetCatalogResponse
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using System.Collections.Generic;

#nullable disable
namespace App1uwp.Network.DataObjects
{
  public class GetCatalogResponse
  {
    public List<GetCatalogResponse.VideoCatalogCategory> items { get; set; }

    public List<VKProfileBase> profiles { get; set; }

    public List<VKGroup> groups { get; set; }

    public string next { get; set; }

    public class VideoCatalogCategory
    {
      public const string ID_MUSIC = "1";
      public const string ID_SPORT = "2";
      public const string ID_GAMES = "3";
      public const string ID_HUMOUR = "4";
      public const string ID_ANIMALS = "5";
      public const string ID_DANCE = "6";
      public const string ID_COOKING = "7";
      public const string ID_VEHICLES = "8";
      public const string ID_BEAUTY = "9";
      public const string ID_SCIENCE = "10";
      public const string ID_FUN = "12";
      public const string ID_FAMILY = "13";
      public const string ID_EROTICS = "15";
      public const string ID_EDUCATION = "16";
      public const string ID_TRAILER = "17";
      public const string ID_SOCIAL = "18";
      public const string ID_GAME_STREAMS = "19";
      public const string ID_COGNITIVE = "63";
      public const string ID_CARTOONS = "69";
      public const string ID_FITNESS = "108";
      public const string ID_MY = "my";
      public const string ID_SERIES = "series";
      public const string ID_FEED = "feed";
      public const string ID_UGC = "ugc";
      public const string ID_TOP = "top";
      public const string VIEW_TYPE_HORIZONTAL = "horizontal";
      public const string VIEW_TYPE_HORIZONTAL_COMPACT = "horizontal_compact";
      public const string VIEW_TYPE_VERTICAL = "vertical";
      public const string VIEW_TYPE_VERTICAL_COMPACT = "vertical_compact";
      public const string CATEGORY_TYPE_CHANNEL = "channel";
      public const string CATEGORY_TYPE_CATEGORY = "category";

      public string id { get; set; }

      public string view { get; set; }

      public string next { get; set; }

      public string name { get; set; }

      public int can_hide { get; set; }

      public string type { get; set; }

      public List<VKVideoBase> items { get; set; }

      public string icon { get; set; }

      public string icon_2x { get; set; }

      public string uc_icon { get; set; }
    }
  }
}
