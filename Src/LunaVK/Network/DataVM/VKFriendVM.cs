// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.DataVM.VKFriendVM
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.DataObjects;
using App1uwp.Network.Enums;

#nullable disable
namespace App1uwp.Network.DataVM
{
  public class VKFriendVM : VKProfileBase
  {
    public string PlatformIcon
    {
      get
      {
        if (!this.online)
          return "";
        switch (this.last_seen.platform)
        {
          case VKPlatform.Mobile:
            return "\uEE64";
          case VKPlatform.iPhone:
          case VKPlatform.iPad:
            return "\uEE77";
          case VKPlatform.Android:
            return "\uEE79";
          case VKPlatform.WindowsPhone:
          case VKPlatform.Windows:
            return "\uEE63";
          case VKPlatform.Web:
            return "\uF137";
          default:
            return "\uF137";
        }
      }
    }

    public string CountryWithCity
    {
      get
      {
        string countryWithCity = "";
        if (this.country != null)
          countryWithCity = this.country.title;
        if (this.city != null)
          countryWithCity = countryWithCity + ", " + this.city.title;
        return countryWithCity;
      }
    }
  }
}
