// Decompiled with JetBrains decompiler
// Type: HtmlAgilityPack.HtmlParseError
// Assembly: HtmlAgilityPack, Version=1.7.1.0, Culture=neutral, PublicKeyToken=bd319b19eaf3b43a
// MVID: B5498F67-4C4E-4FFB-BDE4-EB084EEE38F2
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\HtmlAgilityPack.dll

#nullable disable
namespace HtmlAgilityPack
{
  public class HtmlParseError
  {
    private HtmlParseErrorCode _code;
    private int _line;
    private int _linePosition;
    private string _reason;
    private string _sourceText;
    private int _streamPosition;

    internal HtmlParseError(
      HtmlParseErrorCode code,
      int line,
      int linePosition,
      int streamPosition,
      string sourceText,
      string reason)
    {
      this._code = code;
      this._line = line;
      this._linePosition = linePosition;
      this._streamPosition = streamPosition;
      this._sourceText = sourceText;
      this._reason = reason;
    }

    public HtmlParseErrorCode Code => this._code;

    public int Line => this._line;

    public int LinePosition => this._linePosition;

    public string Reason => this._reason;

    public string SourceText => this._sourceText;

    public int StreamPosition => this._streamPosition;
  }
}
