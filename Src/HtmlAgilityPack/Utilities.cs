
// Type: HtmlAgilityPack.Utilities
// Assembly: HtmlAgilityPack, Version=1.7.1.0, Culture=neutral, PublicKeyToken=bd319b19eaf3b43a
// MVID: B5498F67-4C4E-4FFB-BDE4-EB084EEE38F2
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\HtmlAgilityPack.dll

using System.Collections.Generic;

#nullable disable
namespace HtmlAgilityPack
{
  internal static class Utilities
  {
    public static TValue GetDictionaryValueOrDefault<TKey, TValue>(
      Dictionary<TKey, TValue> dict,
      TKey key,
      TValue defaultValue = default)
      where TKey : class
    {
      TValue obj;
      return !dict.TryGetValue(key, out obj) ? defaultValue : obj;
    }
  }
}
