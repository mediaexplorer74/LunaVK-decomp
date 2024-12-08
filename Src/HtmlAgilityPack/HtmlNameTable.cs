
// Type: HtmlAgilityPack.HtmlNameTable
// Assembly: HtmlAgilityPack, Version=1.7.1.0, Culture=neutral, PublicKeyToken=bd319b19eaf3b43a
// MVID: B5498F67-4C4E-4FFB-BDE4-EB084EEE38F2
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\HtmlAgilityPack.dll

using System.Xml;

#nullable disable
namespace HtmlAgilityPack
{
  internal class HtmlNameTable : XmlNameTable
  {
    private NameTable _nametable = new NameTable();

    public override string Add(string array) => this._nametable.Add(array);

    public override string Add(char[] array, int offset, int length)
    {
      return this._nametable.Add(array, offset, length);
    }

    public override string Get(string array) => this._nametable.Get(array);

    public override string Get(char[] array, int offset, int length)
    {
      return this._nametable.Get(array, offset, length);
    }

    internal string GetOrAdd(string array) => this.Get(array) ?? this.Add(array);
  }
}
