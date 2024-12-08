// Decompiled with JetBrains decompiler
// Type: App1uwp.Utils.Extensions
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.DataObjects;
using App1uwp.Network.Enums;
using System;

#nullable disable
namespace App1uwp.Utils
{
  public static class Extensions
  {
    public static string GetUserStatusString(this VKLastSeen Status, VKUserSex sex)
    {
      if (Status == null)
        return "";
      if (Status.Online)
      {
        string str = LocalizedStrings.GetString("Online");
        return !string.IsNullOrEmpty(Status.AppString) ? str + " (" + Status.AppString + ")" : str + " (" + Status.platform.ToString() + ")";
      }
      DateTime time = Status.time;
      DateTime now = DateTime.Now;
      int int32 = Convert.ToInt32(Math.Floor((now - time).TotalMinutes));
      string str1;
      if (int32 > 0 && int32 < 60)
      {
        if (int32 < 2)
        {
          str1 = sex == VKUserSex.Female ? LocalizedStrings.GetString("LastSeenAMomentAgoFemale") : LocalizedStrings.GetString("LastSeenAMomentAgoMale");
        }
        else
        {
          int num = int32 % 10;
          string str2;
          if (sex != VKUserSex.Female)
          {
            if (num == 1 && (int32 < 10 || int32 > 20))
              str2 = string.Format(LocalizedStrings.GetString("LastSeenX1MinutesAgoMaleFrm"), (object) int32);
            else if (num < 5 && num != 0 && (int32 < 10 || int32 > 20))
              str2 = string.Format(LocalizedStrings.GetString("LastSeenXTwoFourMinutesAgoMaleFrm"), (object) int32);
            else
              str2 = string.Format(LocalizedStrings.GetString("LastSeenXFiveMinutesAgoMaleFrm"), (object) int32);
          }
          else if (num == 1 && (int32 < 10 || int32 > 20))
            str2 = string.Format(LocalizedStrings.GetString("LastSeenX1MinutesAgoFemaleFrm"), (object) int32);
          else if (num < 5 && num != 0 && (int32 < 10 || int32 > 20))
            str2 = string.Format(LocalizedStrings.GetString("LastSeenXTwoFourMinutesAgoFemaleFrm"), (object) int32);
          else
            str2 = string.Format(LocalizedStrings.GetString("LastSeenXFiveMinutesAgoFemaleFrm"), (object) int32);
          str1 = str2;
        }
      }
      else
      {
        string str3;
        if (now.Date == time.Date)
        {
          if (sex != VKUserSex.Female)
            str3 = string.Format(LocalizedStrings.GetString("LastSeenTodayMaleFrm"), (object) time.ToString("HH:mm"));
          else
            str3 = string.Format(LocalizedStrings.GetString("LastSeenTodayFemaleFrm"), (object) time.ToString("HH:mm"));
        }
        else if (now.AddDays(-1.0).Date == time.Date)
        {
          if (sex != VKUserSex.Female)
            str3 = string.Format(LocalizedStrings.GetString("LastSeenYesterdayMaleFrm"), (object) time.ToString("HH:mm"));
          else
            str3 = string.Format(LocalizedStrings.GetString("LastSeenYesterdayFemaleFrm"), (object) time.ToString("HH:mm"));
        }
        else if (now.Year == time.Year)
        {
          if (sex != VKUserSex.Female)
            str3 = string.Format(LocalizedStrings.GetString("LastSeenOnMaleFrm"), (object) time.ToString("dd.MM"), (object) time.ToString("HH:mm"));
          else
            str3 = string.Format(LocalizedStrings.GetString("LastSeenOnFemaleFrm"), (object) time.ToString("dd.MM"), (object) time.ToString("HH:mm"));
        }
        else if (sex != VKUserSex.Female)
          str3 = string.Format(LocalizedStrings.GetString("LastSeenOnMaleFrm"), (object) time.ToString("dd.MM.yyyy"), (object) time.ToString("HH:mm"));
        else
          str3 = string.Format(LocalizedStrings.GetString("LastSeenOnFemaleFrm"), (object) time.ToString("dd.MM.yyyy"), (object) time.ToString("HH:mm"));
        str1 = str3;
      }
      return !string.IsNullOrEmpty(Status.AppString) ? str1 + " (" + Status.AppString + ")" : str1 + " (" + Status.platform.ToString() + ")";
    }

    public static int DateTimeToUnixTimestamp(DateTime dt, bool includeTimeDiff = true)
    {
      DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0);
      int unixTimestamp = (int) ((dt.Ticks - dateTime.Ticks) / 10000000L);
      if (includeTimeDiff)
      {
        int minusLocalTimeDelta = Settings.Instance.ServerMinusLocalTimeDelta;
        unixTimestamp += minusLocalTimeDelta;
      }
      return unixTimestamp;
    }
  }
}
