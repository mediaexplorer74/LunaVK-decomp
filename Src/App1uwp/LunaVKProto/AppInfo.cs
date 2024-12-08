// Decompiled with JetBrains decompiler
// Type: App1uwp.AppInfo
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using Windows.ApplicationModel;

#nullable disable
namespace App1uwp
{
  public static class AppInfo
  {
    public static string Version
    {
      get
      {
        PackageVersion version = Package.Current.Id.Version;
        return string.Format("{0}.{1}.{0}", (object) version.Major, (object) version.Minor, (object) version.Build);
      }
    }

    public static string AppVersionForUserAgent
    {
      get
      {
        return "Mozilla/5.0 (Windows Phone 8.1; ARM; Trident/8.0; Touch; WebView/2.0; rv:11.0; IEMobile/11.0; NOKIA; Lumia 1520) like Gecko";
      }
    }

    public static string OSTypeAndVersion => string.Format("WindowsPhone {0}", (object) "10");

    public static string Device => "NOKIA";
  }
}
