// Decompiled with JetBrains decompiler
// Type: HtmlAgilityPack.HtmlAttribute
// Assembly: HtmlAgilityPack, Version=1.7.1.0, Culture=neutral, PublicKeyToken=bd319b19eaf3b43a
// MVID: B5498F67-4C4E-4FFB-BDE4-EB084EEE38F2
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\HtmlAgilityPack.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace HtmlAgilityPack
{
  [DebuggerDisplay("Name: {OriginalName}, Value: {Value}")]
  public class HtmlAttribute : IComparable
  {
    private int _line;
    internal int _lineposition;
    internal string _name;
    internal int _namelength;
    internal int _namestartindex;
    internal HtmlDocument _ownerdocument;
    internal HtmlNode _ownernode;
    private AttributeValueQuote _quoteType = AttributeValueQuote.DoubleQuote;
    internal int _streamposition;
    internal string _value;
    internal int _valuelength;
    internal int _valuestartindex;

    internal HtmlAttribute(HtmlDocument ownerdocument) => this._ownerdocument = ownerdocument;

    public int Line
    {
      get => this._line;
      internal set => this._line = value;
    }

    public int LinePosition => this._lineposition;

    public string Name
    {
      get
      {
        if (this._name == null)
          this._name = this._ownerdocument.Text.Substring(this._namestartindex, this._namelength);
        return this._name.ToLowerInvariant();
      }
      set
      {
        this._name = value != null ? value : throw new ArgumentNullException(nameof (value));
        if (this._ownernode == null)
          return;
        this._ownernode.SetChanged();
      }
    }

    public string OriginalName => this._name;

    public HtmlDocument OwnerDocument => this._ownerdocument;

    public HtmlNode OwnerNode => this._ownernode;

    public AttributeValueQuote QuoteType
    {
      get => this._quoteType;
      set => this._quoteType = value;
    }

    public int StreamPosition => this._streamposition;

    public string Value
    {
      get
      {
        if (this._value == null && this._ownerdocument.Text == null && this._valuestartindex == 0 && this._valuelength == 0)
          return (string) null;
        if (this._value == null)
        {
          this._value = this._ownerdocument.Text.Substring(this._valuestartindex, this._valuelength);
          if (!this._ownerdocument.BackwardCompatibility)
            this._value = HtmlEntity.DeEntitize(this._value);
        }
        return this._value;
      }
      set
      {
        this._value = value;
        if (this._ownernode == null)
          return;
        this._ownernode.SetChanged();
      }
    }

    public string DeEntitizeValue => HtmlEntity.DeEntitize(this.Value);

    internal string XmlName => HtmlDocument.GetXmlName(this.Name, true);

    internal string XmlValue => this.Value;

    public string XPath
    {
      get => (this.OwnerNode == null ? "/" : this.OwnerNode.XPath + "/") + this.GetRelativeXpath();
    }

    public int CompareTo(object obj)
    {
      return obj is HtmlAttribute htmlAttribute ? this.Name.CompareTo(htmlAttribute.Name) : throw new ArgumentException(nameof (obj));
    }

    public HtmlAttribute Clone()
    {
      return new HtmlAttribute(this._ownerdocument)
      {
        Name = this.Name,
        Value = this.Value,
        QuoteType = this.QuoteType
      };
    }

    public void Remove() => this._ownernode.Attributes.Remove(this);

    private string GetRelativeXpath()
    {
      if (this.OwnerNode == null)
        return this.Name;
      int num = 1;
      foreach (HtmlAttribute attribute in (IEnumerable<HtmlAttribute>) this.OwnerNode.Attributes)
      {
        if (!(attribute.Name != this.Name))
        {
          if (attribute != this)
            ++num;
          else
            break;
        }
      }
      return "@" + this.Name + "[" + (object) num + "]";
    }
  }
}
