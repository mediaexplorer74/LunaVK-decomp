// Decompiled with JetBrains decompiler
// Type: HtmlAgilityPack.HtmlCommentNode
// Assembly: HtmlAgilityPack, Version=1.7.1.0, Culture=neutral, PublicKeyToken=bd319b19eaf3b43a
// MVID: B5498F67-4C4E-4FFB-BDE4-EB084EEE38F2
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\HtmlAgilityPack.dll

#nullable disable
namespace HtmlAgilityPack
{
  public class HtmlCommentNode : HtmlNode
  {
    private string _comment;

    internal HtmlCommentNode(HtmlDocument ownerdocument, int index)
      : base(HtmlNodeType.Comment, ownerdocument, index)
    {
    }

    public string Comment
    {
      get => this._comment == null ? base.InnerHtml : this._comment;
      set => this._comment = value;
    }

    public override string InnerHtml
    {
      get => this._comment == null ? base.InnerHtml : this._comment;
      set => this._comment = value;
    }

    public override string OuterHtml
    {
      get => this._comment == null ? base.OuterHtml : "<!--" + this._comment + "-->";
    }
  }
}
