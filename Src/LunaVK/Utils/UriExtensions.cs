// Decompiled with JetBrains decompiler
// Type: App1uwp.Utils.UriExtensions
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

#nullable disable
namespace App1uwp.Utils
{
  public static class UriExtensions
  {
    private static readonly Regex QueryStringRegex = new Regex("[\\?&](?<name>[^&=]+)=(?<value>[^&=]+)");

    public static Dictionary<string, string> ParseQueryString(this Uri uri)
    {
      return !(uri == (Uri) null) ? uri.OriginalString.ParseQueryString() : throw new ArgumentException(nameof (uri));
    }

    public static Dictionary<string, string> ParseQueryString(this string uriString)
    {
      MatchCollection matchCollection = uriString != null ? UriExtensions.QueryStringRegex.Matches(uriString) : throw new ArgumentException("uri");
      Dictionary<string, string> queryString = new Dictionary<string, string>();
      for (int i = 0; i < matchCollection.Count; ++i)
      {
        Match match = matchCollection[i];
        queryString[match.Groups["name"].Value] = match.Groups["value"].Value;
      }
      return queryString;
    }

    public static bool IsExternal(this string uriString)
    {
      if (string.IsNullOrWhiteSpace(uriString))
        return false;
      return uriString.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || uriString.StartsWith("https://", StringComparison.OrdinalIgnoreCase);
    }
  }
}
