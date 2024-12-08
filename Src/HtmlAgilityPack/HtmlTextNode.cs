// Decompiled with JetBrains decompiler
// Type: HtmlAgilityPack.HtmlTextNode
// Assembly: HtmlAgilityPack, Version=1.7.1.0, Culture=neutral, PublicKeyToken=bd319b19eaf3b43a
// MVID: B5498F67-4C4E-4FFB-BDE4-EB084EEE38F2
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\HtmlAgilityPack.dll

#nullable disable
namespace HtmlAgilityPack
{
  public class HtmlTextNode : HtmlNode
  {
    private string _text;

    internal HtmlTextNode(HtmlDocument ownerdocument, int index)
      : base(HtmlNodeType.Text, ownerdocument, index)
    {
    }

    public override string InnerHtml
    {
      get => this.OuterHtml;
      set => this._text = value;
    }

    public override string OuterHtml => this._text == null ? base.OuterHtml : this._text;

    public string Text
    {
      get => this._text == null ? base.OuterHtml : this._text;
      set => this._text = value;
    }
  }
}
