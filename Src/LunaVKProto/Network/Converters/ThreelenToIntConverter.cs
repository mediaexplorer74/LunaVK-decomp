// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.Converters.ThreelenToIntConverter
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Library;
using System;
using Windows.UI.Xaml.Data;

#nullable disable
namespace App1uwp.Network.Converters
{
  public class ThreelenToIntConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, string language)
    {
      return (object) (byte) value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
      return Enum.Parse(typeof (Threelen), value.ToString(), true);
    }
  }
}
