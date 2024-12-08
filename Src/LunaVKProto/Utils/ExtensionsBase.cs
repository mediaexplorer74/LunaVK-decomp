// Decompiled with JetBrains decompiler
// Type: App1uwp.Utils.ExtensionsBase
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Library;
using System;
using Windows.Data.Html;

#nullable disable
namespace App1uwp.Utils
{
  public static class ExtensionsBase
  {
    public static Uri ConvertToUri(this string uriStr)
    {
      if (string.IsNullOrEmpty(uriStr))
        return (Uri) null;
      return !uriStr.StartsWith(".") && !uriStr.StartsWith("/") ? new Uri(uriStr, UriKind.Absolute) : new Uri(uriStr, UriKind.Relative);
    }

    public static string GetShorterVersion(this string str)
    {
      return string.IsNullOrEmpty(str) || str.Length < 32 ? str : MD5Core.GetHashString(str);
    }

    public static string ForUI(this string backendTextString)
    {
      return string.IsNullOrEmpty(backendTextString) ? "" : HtmlUtilities.ConvertToText(backendTextString.Replace("<br>", Environment.NewLine).Replace("<br/>", Environment.NewLine).Replace("\r\n", "\n").Replace("\n", "\r\n")).Trim();
    }
  }
}
