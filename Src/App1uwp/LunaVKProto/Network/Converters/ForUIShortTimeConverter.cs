// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.Converters.ForUIShortTimeConverter
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using System;
using Windows.UI.Xaml.Data;

#nullable disable
namespace App1uwp.Network.Converters
{
  public class ForUIShortTimeConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, string language)
    {
      return value is DateTime dateTime ? (object) this.FormatDateForUIShort(dateTime.ToLocalTime()) : throw new ArgumentException();
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
      throw new NotImplementedException();
    }

    private string FormatDateForUIShort(DateTime dateTime)
    {
      DateTime now = DateTime.Now;
      DateTime dateTime1 = new DateTime(now.Year, now.Month, now.Day);
      DateTime dateTime2 = new DateTime(now.Year, 1, 1);
      if (dateTime.Year == now.Year && dateTime.Month == now.Month && dateTime.Day == now.Day)
        return dateTime.ToString("HH:mm");
      DateTime dateTime3 = dateTime.AddDays(-1.0);
      DateTime dateTime4 = now.AddDays(-1.0);
      if (dateTime3.Year == dateTime4.Year && dateTime3.Month == dateTime4.Month && dateTime3.Day == dateTime4.Day)
        return LocalizedStrings.GetString("Yesterday");
      return dateTime.Year == now.Year ? dateTime.ToString("dd.MM") : dateTime.ToString("dd.MM.yy");
    }
  }
}
