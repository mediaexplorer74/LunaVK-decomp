
// Type: HtmlAgilityPack.EncodingFoundException
// Assembly: HtmlAgilityPack, Version=1.7.1.0, Culture=neutral, PublicKeyToken=bd319b19eaf3b43a
// MVID: B5498F67-4C4E-4FFB-BDE4-EB084EEE38F2
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\HtmlAgilityPack.dll

using System;
using System.Text;

#nullable disable
namespace HtmlAgilityPack
{
  internal class EncodingFoundException : Exception
  {
    private Encoding _encoding;

    internal EncodingFoundException(Encoding encoding) => this._encoding = encoding;

    internal Encoding Encoding => this._encoding;
  }
}
