// Decompiled with JetBrains decompiler
// Type: HtmlAgilityPack.Trace
// Assembly: HtmlAgilityPack, Version=1.7.1.0, Culture=neutral, PublicKeyToken=bd319b19eaf3b43a
// MVID: B5498F67-4C4E-4FFB-BDE4-EB084EEE38F2
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\HtmlAgilityPack.dll

#nullable disable
namespace HtmlAgilityPack
{
  internal class Trace
  {
    internal static Trace _current;

    internal static Trace Current
    {
      get
      {
        if (Trace._current == null)
          Trace._current = new Trace();
        return Trace._current;
      }
    }

    private void WriteLineIntern(string message, string category)
    {
    }

    public static void WriteLine(string message, string category)
    {
      Trace.Current.WriteLineIntern(message, category);
    }
  }
}
