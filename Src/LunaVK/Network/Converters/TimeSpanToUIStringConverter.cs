// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.Converters.TimeSpanToUIStringConverter
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using System;
using Windows.UI.Xaml.Data;

#nullable disable
namespace App1uwp.Network.Converters
{
  public class TimeSpanToUIStringConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, string language)
    {
      int num = (int) value;
      return num < 3600 ? (object) TimeSpan.FromSeconds((double) num).ToString("m\\:ss") : (object) TimeSpan.FromSeconds((double) num).ToString("h\\:mm\\:ss");
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
      throw new NotImplementedException();
    }
  }
}
