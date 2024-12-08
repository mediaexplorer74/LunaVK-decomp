// Decompiled with JetBrains decompiler
// Type: App1uwp.Utils.UIStringFormatterHelper
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

#nullable disable
namespace App1uwp.Utils
{
  public class UIStringFormatterHelper
  {
    private static readonly Regex _linksRegex = new Regex("(\\[.*?\\|.*?\\])|(https?://\\S+)|(#\\S+)");
    private static readonly Regex _userOrGroupRegEx = new Regex("\\[(id|club)\\d+.*?\\|.*?\\]");

    public static string AtStr(DateTime dt)
    {
      return dt.Hour == 1 ? LocalizedStrings.GetString("AtOneInTheNight") : LocalizedStrings.GetString("At");
    }

    public static string FormatDateTimeForUI(int unixTime)
    {
      return UIStringFormatterHelper.FormatDateTimeForUI(UIStringFormatterHelper.UnixTimeStampToDateTime((double) unixTime));
    }

    public static string FormatDateTimeForUI(DateTime dateTime)
    {
      DateTime now = DateTime.Now;
      int int32 = Convert.ToInt32(Math.Floor((now - dateTime).TotalMinutes));
      string str1 = dateTime.ToString("HH:mm");
      if (int32 >= -1 && int32 < 60)
        return int32 < 1 ? LocalizedStrings.GetString("MomentAgo") : UIStringFormatterHelper.FormatNumberOfSomething(int32, LocalizedStrings.GetString("OneMinuteFrm"), LocalizedStrings.GetString("TwoFourMinutes"), LocalizedStrings.GetString("FiveMinutes"));
      if (now.Date == dateTime.Date)
        return string.IsNullOrEmpty(UIStringFormatterHelper.AtStr(dateTime)) ? string.Format("{0} {1}", (object) LocalizedStrings.GetString("Today"), (object) str1) : string.Format("{0} {1} {2}", (object) LocalizedStrings.GetString("Today"), (object) UIStringFormatterHelper.AtStr(dateTime), (object) str1);
      if (now.AddDays(-1.0).Date == dateTime.Date)
        return string.IsNullOrEmpty(UIStringFormatterHelper.AtStr(dateTime)) ? string.Format("{0} {1}", (object) LocalizedStrings.GetString("Yesterday"), (object) str1) : string.Format("{0} {1} {2}", (object) LocalizedStrings.GetString("Yesterday"), (object) UIStringFormatterHelper.AtStr(dateTime), (object) str1);
      if (now.Year == dateTime.Year)
      {
        int day = dateTime.Day;
        string ofMonthStr = UIStringFormatterHelper.GetOfMonthStr(dateTime.Month);
        return string.IsNullOrEmpty(UIStringFormatterHelper.AtStr(dateTime)) ? string.Format("{0} {1} {2}", (object) day, (object) ofMonthStr, (object) str1) : string.Format("{0} {1} {2} {3}", (object) day, (object) ofMonthStr, (object) UIStringFormatterHelper.AtStr(dateTime), (object) str1);
      }
      int day1 = dateTime.Day;
      string ofMonthStr1 = UIStringFormatterHelper.GetOfMonthStr(dateTime.Month);
      int year = dateTime.Year;
      string str2 = "";
      string str3 = UIStringFormatterHelper.AtStr(dateTime);
      return string.IsNullOrEmpty(str3) ? string.Format("{0} {1} {2}", (object) day1, (object) ofMonthStr1, (object) str1) : (string.IsNullOrEmpty(str2) ? (string.IsNullOrEmpty(str3) ? string.Format("{0} {1} {2} {3}", (object) day1, (object) ofMonthStr1, (object) year, (object) str1) : string.Format("{0} {1} {2} {3} {4}", (object) day1, (object) ofMonthStr1, (object) year, (object) UIStringFormatterHelper.AtStr(dateTime), (object) str1)) : (string.IsNullOrEmpty(str3) ? string.Format("{0} {1} {2} {3} {4}", (object) day1, (object) ofMonthStr1, (object) str2, (object) year, (object) str1) : string.Format("{0} {1} {2} {3} {4} {5}", (object) day1, (object) ofMonthStr1, (object) str2, (object) year, (object) UIStringFormatterHelper.AtStr(dateTime), (object) str1)));
    }

    public static string FormatNumberOfSomething(
      int number,
      string oneSomethingFrm,
      string twoSomethingFrm,
      string fiveSomethingFrm,
      bool includeNumberInResult = true,
      string additionalFormatParam = null,
      bool includeZero = false)
    {
      if (number < 0)
        throw new Exception("Invalid number to format.");
      if (number == 0 && !includeZero)
        return "";
      int num = number % 10;
      string format = !(CultureInfo.CurrentUICulture.TwoLetterISOLanguageName != "en") || !(CultureInfo.CurrentUICulture.TwoLetterISOLanguageName != "pt") ? (number == 1 ? oneSomethingFrm : twoSomethingFrm) : (num != 1 || number % 100 == 11 ? (num < 2 || num > 4 || number % 100 >= 12 && number % 100 <= 14 ? fiveSomethingFrm : twoSomethingFrm) : oneSomethingFrm);
      if (!includeNumberInResult)
        return string.Format(format, (object) "").Trim();
      NumberFormatInfo provider = (NumberFormatInfo) CultureInfo.InvariantCulture.NumberFormat.Clone();
      provider.NumberGroupSeparator = " ";
      string str = number.ToString("#,#", (IFormatProvider) provider);
      return additionalFormatParam == null ? string.Format(format, (object) str) : string.Format(format, (object) str, (object) additionalFormatParam);
    }

    public static string GetOfMonthStr(int month)
    {
      switch (month)
      {
        case 1:
          return LocalizedStrings.GetString("OfJanuary");
        case 2:
          return LocalizedStrings.GetString("OfFebruary");
        case 3:
          return LocalizedStrings.GetString("OfMarch");
        case 4:
          return LocalizedStrings.GetString("OfApril");
        case 5:
          return LocalizedStrings.GetString("OfMay");
        case 6:
          return LocalizedStrings.GetString("OfJune");
        case 7:
          return LocalizedStrings.GetString("OfJuly");
        case 8:
          return LocalizedStrings.GetString("OfAugust");
        case 9:
          return LocalizedStrings.GetString("OfSeptember");
        case 10:
          return LocalizedStrings.GetString("OfOctober");
        case 11:
          return LocalizedStrings.GetString("OfNovember");
        case 12:
          return LocalizedStrings.GetString("OfDecember");
        default:
          return "";
      }
    }

    public static DateTime UnixTimeStampToDateTime(double unixTimeStamp, bool includeTimeDiff = true)
    {
      return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(unixTimeStamp).ToLocalTime();
    }

    public static string CutTextGently(string text, int preferredMaxLength)
    {
      if (text == null || text.Length <= preferredMaxLength)
        return text ?? "";
      MatchCollection matchCollection = UIStringFormatterHelper._linksRegex.Matches(text);
      int val1 = preferredMaxLength - 1;
      int num1 = text.IndexOf(' ', val1 + 1);
      if (num1 > 0 && num1 < preferredMaxLength + 30)
        val1 = num1;
      int num2 = text.IndexOfAny(new char[3]
      {
        '.',
        '!',
        '?'
      }, val1 + 1);
      if (num2 > 0 && num2 < preferredMaxLength + 200)
        val1 = num2;
      if (text.Length - val1 < 20)
        return text;
      foreach (Match match in matchCollection)
      {
        if (match.Index <= val1 && match.Index + match.Length - 1 >= val1)
          val1 = Math.Max(val1, match.Index + match.Length - 1);
      }
      return text.Substring(0, val1 + 1);
    }

    public static string BytesForUI(double size)
    {
      if (size < 1024.0)
        return Math.Round(size).ToString() + " B";
      if (size < 1048576.0)
        return Math.Round(size / 1024.0).ToString("#.#") + " KB";
      if (size < 1073741824.0)
        return (size / 1048576.0).ToString("#.#") + " MB";
      return size < 1099511627776.0 ? (size / 1073741824.0).ToString("#.#") + " GB" : (size / 1099511627776.0).ToString("#.#") + " TB";
    }

    public static string FormatDuration(int durationSeconds)
    {
      return durationSeconds < 3600 ? TimeSpan.FromSeconds((double) durationSeconds).ToString("m\\:ss") : TimeSpan.FromSeconds((double) durationSeconds).ToString("h\\:mm\\:ss");
    }

    public static string SubstituteMentionsWithNames(string text)
    {
      int startIndex = 0;
      MatchCollection matchCollection = UIStringFormatterHelper._userOrGroupRegEx.Matches(text);
      StringBuilder stringBuilder = new StringBuilder();
      foreach (Match match in matchCollection)
      {
        if (match.Index != startIndex)
          stringBuilder = stringBuilder.Append(text.Substring(startIndex, match.Index - startIndex));
        int num = match.Value.IndexOf("|");
        string str = match.Value.Substring(num + 1, match.Value.Length - num - 2);
        stringBuilder = stringBuilder.Append(str);
        startIndex = match.Index + match.Length;
      }
      if (startIndex < text.Length)
        stringBuilder = stringBuilder.Append(text.Substring(startIndex));
      return stringBuilder.ToString();
    }
  }
}
