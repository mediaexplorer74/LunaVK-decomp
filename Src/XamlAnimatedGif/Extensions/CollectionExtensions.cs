// Decompiled with JetBrains decompiler
// Type: XamlAnimatedGif.Extensions.CollectionExtensions
// Assembly: XamlAnimatedGif, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 02CD40CB-7FCE-4EFE-9D7D-B0095CDCA3E7
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\XamlAnimatedGif.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;

#nullable disable
namespace XamlAnimatedGif.Extensions
{
  internal static class CollectionExtensions
  {
    public static ReadOnlyCollection<T> AsReadOnly<T>(this IList<T> list)
    {
      return new ReadOnlyCollection<T>(list);
    }
  }
}
