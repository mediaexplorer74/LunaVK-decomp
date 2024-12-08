// Decompiled with JetBrains decompiler
// Type: HtmlAgilityPack.NameValuePair
// Assembly: HtmlAgilityPack, Version=1.7.1.0, Culture=neutral, PublicKeyToken=bd319b19eaf3b43a
// MVID: B5498F67-4C4E-4FFB-BDE4-EB084EEE38F2
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\HtmlAgilityPack.dll

#nullable disable
namespace HtmlAgilityPack
{
  internal class NameValuePair
  {
    internal readonly string Name;
    internal string Value;

    internal NameValuePair()
    {
    }

    internal NameValuePair(string name)
      : this()
    {
      this.Name = name;
    }

    internal NameValuePair(string name, string value)
      : this(name)
    {
      this.Value = value;
    }
  }
}
