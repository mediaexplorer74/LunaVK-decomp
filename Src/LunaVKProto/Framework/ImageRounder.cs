// Decompiled with JetBrains decompiler
// Type: App1uwp.Framework.ImageRounder
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace App1uwp.Framework
{
  public static class ImageRounder
  {
    public static void LetsRound(this Border target)
    {
      double uniformRadius = (double) Settings.Instance.RoundAvatar * ((FrameworkElement) target).Width / 100.0 / 2.0;
      target.put_CornerRadius(new CornerRadius(uniformRadius));
    }
  }
}
