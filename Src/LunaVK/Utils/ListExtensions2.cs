// Decompiled with JetBrains decompiler
// Type: App1uwp.Utils.ListExtensions2
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace App1uwp.Utils
{
  public static class ListExtensions2
  {
    public static List<T> Sublist<T>(this List<T> list, int begin, int end)
    {
      List<T> objList = new List<T>();
      for (int index = begin; index < end; ++index)
        objList.Add(list[index]);
      return objList;
    }

    public static void Apply<T>(this IEnumerable<T> enumerable, Action<T> action)
    {
      foreach (T obj in enumerable)
        action(obj);
    }

    public static string GetCommaSeparated(this List<int> ids, bool invert = false)
    {
      StringBuilder stringBuilder = new StringBuilder();
      int count = ids.Count;
      for (int index = 0; index < count; ++index)
      {
        long num = invert ? (long) -ids[index] : (long) ids[index];
        stringBuilder = stringBuilder.Append(num.ToString());
        if (index != count - 1)
          stringBuilder = stringBuilder.Append(",");
      }
      return stringBuilder.ToString();
    }
  }
}
