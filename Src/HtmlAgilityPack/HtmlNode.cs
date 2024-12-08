
// Type: HtmlAgilityPack.HtmlNode
// Assembly: HtmlAgilityPack, Version=1.7.1.0, Culture=neutral, PublicKeyToken=bd319b19eaf3b43a
// MVID: B5498F67-4C4E-4FFB-BDE4-EB084EEE38F2
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\HtmlAgilityPack.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;

#nullable disable
namespace HtmlAgilityPack
{
  [DebuggerDisplay("Name: {OriginalName}")]
  public class HtmlNode
  {
    internal const string DepthLevelExceptionMessage = "The document is too complex to parse";
    internal HtmlAttributeCollection _attributes;
    internal HtmlNodeCollection _childnodes;
    internal HtmlNode _endnode;
    private bool _changed;
    internal string _innerhtml;
    internal int _innerlength;
    internal int _innerstartindex;
    internal int _line;
    internal int _lineposition;
    private string _name;
    internal int _namelength;
    internal int _namestartindex;
    internal HtmlNode _nextnode;
    internal HtmlNodeType _nodetype;
    internal string _outerhtml;
    internal int _outerlength;
    internal int _outerstartindex;
    private string _optimizedName;
    internal HtmlDocument _ownerdocument;
    internal HtmlNode _parentnode;
    internal HtmlNode _prevnode;
    internal HtmlNode _prevwithsamename;
    internal bool _starttag;
    internal int _streamposition;
    internal bool _isImplicitEnd;
    public static readonly string HtmlNodeTypeNameComment = "#comment";
    public static readonly string HtmlNodeTypeNameDocument = "#document";
    public static readonly string HtmlNodeTypeNameText = "#text";
    public static Dictionary<string, HtmlElementFlag> ElementsFlags = new Dictionary<string, HtmlElementFlag>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);

    static HtmlNode()
    {
      HtmlNode.ElementsFlags.Add("script", HtmlElementFlag.CData);
      HtmlNode.ElementsFlags.Add("style", HtmlElementFlag.CData);
      HtmlNode.ElementsFlags.Add("noxhtml", HtmlElementFlag.CData);
      HtmlNode.ElementsFlags.Add("base", HtmlElementFlag.Empty);
      HtmlNode.ElementsFlags.Add("link", HtmlElementFlag.Empty);
      HtmlNode.ElementsFlags.Add("meta", HtmlElementFlag.Empty);
      HtmlNode.ElementsFlags.Add("isindex", HtmlElementFlag.Empty);
      HtmlNode.ElementsFlags.Add("hr", HtmlElementFlag.Empty);
      HtmlNode.ElementsFlags.Add("col", HtmlElementFlag.Empty);
      HtmlNode.ElementsFlags.Add("img", HtmlElementFlag.Empty);
      HtmlNode.ElementsFlags.Add("param", HtmlElementFlag.Empty);
      HtmlNode.ElementsFlags.Add("embed", HtmlElementFlag.Empty);
      HtmlNode.ElementsFlags.Add("frame", HtmlElementFlag.Empty);
      HtmlNode.ElementsFlags.Add("wbr", HtmlElementFlag.Empty);
      HtmlNode.ElementsFlags.Add("bgsound", HtmlElementFlag.Empty);
      HtmlNode.ElementsFlags.Add("spacer", HtmlElementFlag.Empty);
      HtmlNode.ElementsFlags.Add("keygen", HtmlElementFlag.Empty);
      HtmlNode.ElementsFlags.Add("area", HtmlElementFlag.Empty);
      HtmlNode.ElementsFlags.Add("input", HtmlElementFlag.Empty);
      HtmlNode.ElementsFlags.Add("basefont", HtmlElementFlag.Empty);
      HtmlNode.ElementsFlags.Add("source", HtmlElementFlag.Empty);
      HtmlNode.ElementsFlags.Add("form", HtmlElementFlag.CanOverlap);
      HtmlNode.ElementsFlags.Add("br", HtmlElementFlag.Empty | HtmlElementFlag.Closed);
      if (HtmlDocument.DisableBehavaiorTagP)
        return;
      HtmlNode.ElementsFlags.Add("p", HtmlElementFlag.Empty | HtmlElementFlag.Closed);
    }

    public HtmlNode(HtmlNodeType type, HtmlDocument ownerdocument, int index)
    {
      this._nodetype = type;
      this._ownerdocument = ownerdocument;
      this._outerstartindex = index;
      switch (type)
      {
        case HtmlNodeType.Document:
          this.Name = HtmlNode.HtmlNodeTypeNameDocument;
          this._endnode = this;
          break;
        case HtmlNodeType.Comment:
          this.Name = HtmlNode.HtmlNodeTypeNameComment;
          this._endnode = this;
          break;
        case HtmlNodeType.Text:
          this.Name = HtmlNode.HtmlNodeTypeNameText;
          this._endnode = this;
          break;
      }
      if (this._ownerdocument.Openednodes != null && !this.Closed && -1 != index)
        this._ownerdocument.Openednodes.Add(index, this);
      if (-1 != index || type == HtmlNodeType.Comment || type == HtmlNodeType.Text)
        return;
      this.SetChanged();
    }

    public HtmlAttributeCollection Attributes
    {
      get
      {
        if (!this.HasAttributes)
          this._attributes = new HtmlAttributeCollection(this);
        return this._attributes;
      }
      internal set => this._attributes = value;
    }

    public HtmlNodeCollection ChildNodes
    {
      get => this._childnodes ?? (this._childnodes = new HtmlNodeCollection(this));
      internal set => this._childnodes = value;
    }

    public bool Closed => this._endnode != null;

    public HtmlAttributeCollection ClosingAttributes
    {
      get
      {
        return this.HasClosingAttributes ? this._endnode.Attributes : new HtmlAttributeCollection(this);
      }
    }

    internal HtmlNode EndNode => this._endnode;

    public HtmlNode FirstChild => this.HasChildNodes ? this._childnodes[0] : (HtmlNode) null;

    public bool HasAttributes => this._attributes != null && this._attributes.Count > 0;

    public bool HasChildNodes => this._childnodes != null && this._childnodes.Count > 0;

    public bool HasClosingAttributes
    {
      get
      {
        return this._endnode != null && this._endnode != this && this._endnode._attributes != null && this._endnode._attributes.Count > 0;
      }
    }

    public string Id
    {
      get
      {
        if (this._ownerdocument.Nodesid == null)
          throw new Exception(HtmlDocument.HtmlExceptionUseIdAttributeFalse);
        return this.GetId();
      }
      set
      {
        if (this._ownerdocument.Nodesid == null)
          throw new Exception(HtmlDocument.HtmlExceptionUseIdAttributeFalse);
        if (value == null)
          throw new ArgumentNullException(nameof (value));
        this.SetId(value);
      }
    }

    public virtual string InnerHtml
    {
      get
      {
        if (this._changed)
        {
          this.UpdateHtml();
          return this._innerhtml;
        }
        if (this._innerhtml != null)
          return this._innerhtml;
        return this._innerstartindex < 0 || this._innerlength < 0 ? string.Empty : this._ownerdocument.Text.Substring(this._innerstartindex, this._innerlength);
      }
      set
      {
        HtmlDocument htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(value);
        this.RemoveAllChildren();
        this.AppendChildren(htmlDocument.DocumentNode.ChildNodes);
      }
    }

    public virtual string InnerText
    {
      get
      {
        if (!this._ownerdocument.BackwardCompatibility)
        {
          if (!this.HasChildNodes)
            return this.GetCurrentNodeText();
          StringBuilder sb = new StringBuilder();
          this.AppendInnerText(sb);
          return sb.ToString();
        }
        if (this._nodetype == HtmlNodeType.Text)
          return ((HtmlTextNode) this).Text;
        if (this._nodetype == HtmlNodeType.Comment)
          return "";
        if (!this.HasChildNodes)
          return string.Empty;
        string innerText = (string) null;
        foreach (HtmlNode childNode in (IEnumerable<HtmlNode>) this.ChildNodes)
          innerText += childNode.InnerText;
        return innerText;
      }
    }

    internal string GetCurrentNodeText()
    {
      if (this._nodetype != HtmlNodeType.Text)
        return "";
      string currentNodeText = ((HtmlTextNode) this).Text;
      if (this.ParentNode.Name != "pre")
        currentNodeText = currentNodeText.Replace("\n", "").Replace("\r", "").Replace("\t", "");
      return currentNodeText;
    }

    internal void AppendInnerText(StringBuilder sb)
    {
      if (this._nodetype == HtmlNodeType.Text)
        sb.Append(this.GetCurrentNodeText());
      if (!this.HasChildNodes)
        return;
      foreach (HtmlNode childNode in (IEnumerable<HtmlNode>) this.ChildNodes)
        childNode.AppendInnerText(sb);
    }

    public HtmlNode LastChild
    {
      get => this.HasChildNodes ? this._childnodes[this._childnodes.Count - 1] : (HtmlNode) null;
    }

    public int Line
    {
      get => this._line;
      internal set => this._line = value;
    }

    public int LinePosition
    {
      get => this._lineposition;
      internal set => this._lineposition = value;
    }

    public string Name
    {
      get
      {
        if (this._optimizedName == null)
        {
          if (this._name == null)
            this.Name = this._ownerdocument.Text.Substring(this._namestartindex, this._namelength);
          this._optimizedName = this._name != null ? this._name.ToLowerInvariant() : string.Empty;
        }
        return this._optimizedName;
      }
      set
      {
        this._name = value;
        this._optimizedName = (string) null;
      }
    }

    public HtmlNode NextSibling
    {
      get => this._nextnode;
      internal set => this._nextnode = value;
    }

    public HtmlNodeType NodeType
    {
      get => this._nodetype;
      internal set => this._nodetype = value;
    }

    public string OriginalName => this._name;

    public virtual string OuterHtml
    {
      get
      {
        if (this._changed)
        {
          this.UpdateHtml();
          return this._outerhtml;
        }
        if (this._outerhtml != null)
          return this._outerhtml;
        return this._outerstartindex < 0 || this._outerlength < 0 ? string.Empty : this._ownerdocument.Text.Substring(this._outerstartindex, this._outerlength);
      }
    }

    public HtmlDocument OwnerDocument
    {
      get => this._ownerdocument;
      internal set => this._ownerdocument = value;
    }

    public HtmlNode ParentNode
    {
      get => this._parentnode;
      internal set => this._parentnode = value;
    }

    public HtmlNode PreviousSibling
    {
      get => this._prevnode;
      internal set => this._prevnode = value;
    }

    public int StreamPosition => this._streamposition;

    public string XPath
    {
      get
      {
        return (this.ParentNode == null || this.ParentNode.NodeType == HtmlNodeType.Document ? "/" : this.ParentNode.XPath + "/") + this.GetRelativeXpath();
      }
    }

    public static bool CanOverlapElement(string name)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      HtmlElementFlag htmlElementFlag;
      return HtmlNode.ElementsFlags.TryGetValue(name, out htmlElementFlag) && (htmlElementFlag & HtmlElementFlag.CanOverlap) != 0;
    }

    public static HtmlNode CreateNode(string html)
    {
      HtmlDocument htmlDocument = new HtmlDocument();
      htmlDocument.LoadHtml(html);
      if (!htmlDocument.DocumentNode.IsSingleElementNode())
        throw new Exception("Multiple node elments can't be created.");
      for (HtmlNode node = htmlDocument.DocumentNode.FirstChild; node != null; node = node.NextSibling)
      {
        if (node.NodeType == HtmlNodeType.Element && node.OuterHtml != "\r\n")
          return node;
      }
      return htmlDocument.DocumentNode.FirstChild;
    }

    public static bool IsCDataElement(string name)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      HtmlElementFlag htmlElementFlag;
      return HtmlNode.ElementsFlags.TryGetValue(name, out htmlElementFlag) && (htmlElementFlag & HtmlElementFlag.CData) != 0;
    }

    public static bool IsClosedElement(string name)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      HtmlElementFlag htmlElementFlag;
      return HtmlNode.ElementsFlags.TryGetValue(name, out htmlElementFlag) && (htmlElementFlag & HtmlElementFlag.Closed) != 0;
    }

    public static bool IsEmptyElement(string name)
    {
      switch (name)
      {
        case null:
          throw new ArgumentNullException(nameof (name));
        case "":
          return true;
        default:
          if ('!' == name[0] || '?' == name[0])
            return true;
          HtmlElementFlag htmlElementFlag;
          return HtmlNode.ElementsFlags.TryGetValue(name, out htmlElementFlag) && (htmlElementFlag & HtmlElementFlag.Empty) != 0;
      }
    }

    public static bool IsOverlappedClosingElement(string text)
    {
      if (text == null)
        throw new ArgumentNullException(nameof (text));
      return text.Length > 4 && text[0] == '<' && text[text.Length - 1] == '>' && text[1] == '/' && HtmlNode.CanOverlapElement(text.Substring(2, text.Length - 3));
    }

    public IEnumerable<HtmlNode> Ancestors()
    {
      HtmlNode node = this.ParentNode;
      if (node != null)
      {
        yield return node;
        for (; node.ParentNode != null; node = node.ParentNode)
          yield return node.ParentNode;
      }
    }

    public IEnumerable<HtmlNode> Ancestors(string name)
    {
      HtmlNode n;
      for (n = this.ParentNode; n != null; n = n.ParentNode)
      {
        if (n.Name == name)
          yield return n;
      }
      n = (HtmlNode) null;
    }

    public IEnumerable<HtmlNode> AncestorsAndSelf()
    {
      HtmlNode n;
      for (n = this; n != null; n = n.ParentNode)
        yield return n;
      n = (HtmlNode) null;
    }

    public IEnumerable<HtmlNode> AncestorsAndSelf(string name)
    {
      HtmlNode n;
      for (n = this; n != null; n = n.ParentNode)
      {
        if (n.Name == name)
          yield return n;
      }
      n = (HtmlNode) null;
    }

    public HtmlNode AppendChild(HtmlNode newChild)
    {
      if (newChild == null)
        throw new ArgumentNullException(nameof (newChild));
      this.ChildNodes.Append(newChild);
      this._ownerdocument.SetIdForNode(newChild, newChild.GetId());
      this.SetChildNodesId(newChild);
      this.SetChanged();
      return newChild;
    }

    public void SetChildNodesId(HtmlNode chilNode)
    {
      foreach (HtmlNode childNode in (IEnumerable<HtmlNode>) chilNode.ChildNodes)
      {
        this._ownerdocument.SetIdForNode(childNode, childNode.GetId());
        this.SetChildNodesId(childNode);
      }
    }

    public void AppendChildren(HtmlNodeCollection newChildren)
    {
      if (newChildren == null)
        throw new ArgumentNullException(nameof (newChildren));
      foreach (HtmlNode newChild in (IEnumerable<HtmlNode>) newChildren)
        this.AppendChild(newChild);
    }

    public IEnumerable<HtmlAttribute> ChildAttributes(string name)
    {
      return this.Attributes.AttributesWithName(name);
    }

    public HtmlNode Clone() => this.CloneNode(true);

    public HtmlNode CloneNode(string newName) => this.CloneNode(newName, true);

    public HtmlNode CloneNode(string newName, bool deep)
    {
      if (newName == null)
        throw new ArgumentNullException(nameof (newName));
      HtmlNode htmlNode = this.CloneNode(deep);
      htmlNode.Name = newName;
      return htmlNode;
    }

    public HtmlNode CloneNode(bool deep)
    {
      HtmlNode node = this._ownerdocument.CreateNode(this._nodetype);
      node.Name = this.Name;
      switch (this._nodetype)
      {
        case HtmlNodeType.Comment:
          ((HtmlCommentNode) node).Comment = ((HtmlCommentNode) this).Comment;
          return node;
        case HtmlNodeType.Text:
          ((HtmlTextNode) node).Text = ((HtmlTextNode) this).Text;
          return node;
        default:
          if (this.HasAttributes)
          {
            foreach (HtmlAttribute attribute in (IEnumerable<HtmlAttribute>) this._attributes)
            {
              HtmlAttribute newAttribute = attribute.Clone();
              node.Attributes.Append(newAttribute);
            }
          }
          if (this.HasClosingAttributes)
          {
            node._endnode = this._endnode.CloneNode(false);
            foreach (HtmlAttribute attribute in (IEnumerable<HtmlAttribute>) this._endnode._attributes)
            {
              HtmlAttribute newAttribute = attribute.Clone();
              node._endnode._attributes.Append(newAttribute);
            }
          }
          if (!deep || !this.HasChildNodes)
            return node;
          foreach (HtmlNode childnode in (IEnumerable<HtmlNode>) this._childnodes)
          {
            HtmlNode newChild = childnode.Clone();
            node.AppendChild(newChild);
          }
          return node;
      }
    }

    public void CopyFrom(HtmlNode node) => this.CopyFrom(node, true);

    public void CopyFrom(HtmlNode node, bool deep)
    {
      if (node == null)
        throw new ArgumentNullException(nameof (node));
      this.Attributes.RemoveAll();
      if (node.HasAttributes)
      {
        foreach (HtmlAttribute attribute in (IEnumerable<HtmlAttribute>) node.Attributes)
          this.Attributes.Append(attribute.Clone());
      }
      if (!deep)
        return;
      this.RemoveAllChildren();
      if (!node.HasChildNodes)
        return;
      foreach (HtmlNode childNode in (IEnumerable<HtmlNode>) node.ChildNodes)
        this.AppendChild(childNode.CloneNode(true));
    }

    [Obsolete("Use Descendants() instead, the results of this function will change in a future version")]
    public IEnumerable<HtmlNode> DescendantNodes(int level = 0)
    {
      if (level > HtmlDocument.MaxDepthLevel)
        throw new ArgumentException("The document is too complex to parse");
      foreach (HtmlNode node in (IEnumerable<HtmlNode>) this.ChildNodes)
      {
        yield return node;
        foreach (HtmlNode descendantNode in node.DescendantNodes(level + 1))
          yield return descendantNode;
      }
    }

    [Obsolete("Use DescendantsAndSelf() instead, the results of this function will change in a future version")]
    public IEnumerable<HtmlNode> DescendantNodesAndSelf() => this.DescendantsAndSelf();

    public IEnumerable<HtmlNode> Descendants() => this.Descendants(0);

    public IEnumerable<HtmlNode> Descendants(int level)
    {
      if (level > HtmlDocument.MaxDepthLevel)
        throw new ArgumentException("The document is too complex to parse");
      foreach (HtmlNode node in (IEnumerable<HtmlNode>) this.ChildNodes)
      {
        yield return node;
        foreach (HtmlNode descendant in node.Descendants(level + 1))
          yield return descendant;
      }
    }

    public IEnumerable<HtmlNode> Descendants(string name)
    {
      foreach (HtmlNode descendant in this.Descendants())
      {
        if (string.Equals(descendant.Name, name, StringComparison.OrdinalIgnoreCase))
          yield return descendant;
      }
    }

    public IEnumerable<HtmlNode> DescendantsAndSelf()
    {
      HtmlNode htmlNode = this;
      yield return htmlNode;
      foreach (HtmlNode descendant in htmlNode.Descendants())
      {
        if (descendant != null)
          yield return descendant;
      }
    }

    public IEnumerable<HtmlNode> DescendantsAndSelf(string name)
    {
      HtmlNode htmlNode = this;
      yield return htmlNode;
      foreach (HtmlNode descendant in htmlNode.Descendants())
      {
        if (descendant.Name == name)
          yield return descendant;
      }
    }

    public HtmlNode Element(string name)
    {
      foreach (HtmlNode childNode in (IEnumerable<HtmlNode>) this.ChildNodes)
      {
        if (childNode.Name == name)
          return childNode;
      }
      return (HtmlNode) null;
    }

    public IEnumerable<HtmlNode> Elements(string name)
    {
      foreach (HtmlNode childNode in (IEnumerable<HtmlNode>) this.ChildNodes)
      {
        if (childNode.Name == name)
          yield return childNode;
      }
    }

    public string GetAttributeValue(string name, string def)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (!this.HasAttributes)
        return def;
      HtmlAttribute attribute = this.Attributes[name];
      return attribute == null ? def : attribute.Value;
    }

    public int GetAttributeValue(string name, int def)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (!this.HasAttributes)
        return def;
      HtmlAttribute attribute = this.Attributes[name];
      if (attribute == null)
        return def;
      try
      {
        return Convert.ToInt32(attribute.Value);
      }
      catch
      {
        return def;
      }
    }

    public bool GetAttributeValue(string name, bool def)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (!this.HasAttributes)
        return def;
      HtmlAttribute attribute = this.Attributes[name];
      if (attribute == null)
        return def;
      try
      {
        return Convert.ToBoolean(attribute.Value);
      }
      catch
      {
        return def;
      }
    }

    public HtmlNode InsertAfter(HtmlNode newChild, HtmlNode refChild)
    {
      if (newChild == null)
        throw new ArgumentNullException(nameof (newChild));
      if (refChild == null)
        return this.PrependChild(newChild);
      if (newChild == refChild)
        return newChild;
      int num = -1;
      if (this._childnodes != null)
        num = this._childnodes[refChild];
      if (num == -1)
        throw new ArgumentException(HtmlDocument.HtmlExceptionRefNotChild);
      if (this._childnodes != null)
        this._childnodes.Insert(num + 1, newChild);
      this._ownerdocument.SetIdForNode(newChild, newChild.GetId());
      this.SetChildNodesId(newChild);
      this.SetChanged();
      return newChild;
    }

    public HtmlNode InsertBefore(HtmlNode newChild, HtmlNode refChild)
    {
      if (newChild == null)
        throw new ArgumentNullException(nameof (newChild));
      if (refChild == null)
        return this.AppendChild(newChild);
      if (newChild == refChild)
        return newChild;
      int index = -1;
      if (this._childnodes != null)
        index = this._childnodes[refChild];
      if (index == -1)
        throw new ArgumentException(HtmlDocument.HtmlExceptionRefNotChild);
      if (this._childnodes != null)
        this._childnodes.Insert(index, newChild);
      this._ownerdocument.SetIdForNode(newChild, newChild.GetId());
      this.SetChildNodesId(newChild);
      this.SetChanged();
      return newChild;
    }

    public HtmlNode PrependChild(HtmlNode newChild)
    {
      if (newChild == null)
        throw new ArgumentNullException(nameof (newChild));
      this.ChildNodes.Prepend(newChild);
      this._ownerdocument.SetIdForNode(newChild, newChild.GetId());
      this.SetChildNodesId(newChild);
      this.SetChanged();
      return newChild;
    }

    public void PrependChildren(HtmlNodeCollection newChildren)
    {
      if (newChildren == null)
        throw new ArgumentNullException(nameof (newChildren));
      foreach (HtmlNode newChild in (IEnumerable<HtmlNode>) newChildren)
        this.PrependChild(newChild);
    }

    public void Remove()
    {
      if (this.ParentNode == null)
        return;
      this.ParentNode.ChildNodes.Remove(this);
    }

    public void RemoveAll()
    {
      this.RemoveAllChildren();
      if (this.HasAttributes)
        this._attributes.Clear();
      if (this._endnode != null && this._endnode != this && this._endnode._attributes != null)
        this._endnode._attributes.Clear();
      this.SetChanged();
    }

    public void RemoveAllChildren()
    {
      if (!this.HasChildNodes)
        return;
      if (this._ownerdocument.OptionUseIdAttribute)
      {
        foreach (HtmlNode childnode in (IEnumerable<HtmlNode>) this._childnodes)
        {
          this._ownerdocument.SetIdForNode((HtmlNode) null, childnode.GetId());
          this.RemoveAllIDforNode(childnode);
        }
      }
      this._childnodes.Clear();
      this.SetChanged();
    }

    public void RemoveAllIDforNode(HtmlNode node)
    {
      foreach (HtmlNode childNode in (IEnumerable<HtmlNode>) node.ChildNodes)
      {
        this._ownerdocument.SetIdForNode((HtmlNode) null, childNode.GetId());
        this.RemoveAllIDforNode(childNode);
      }
    }

    public HtmlNode RemoveChild(HtmlNode oldChild)
    {
      if (oldChild == null)
        throw new ArgumentNullException(nameof (oldChild));
      int index = -1;
      if (this._childnodes != null)
        index = this._childnodes[oldChild];
      if (index == -1)
        throw new ArgumentException(HtmlDocument.HtmlExceptionRefNotChild);
      if (this._childnodes != null)
        this._childnodes.Remove(index);
      this._ownerdocument.SetIdForNode((HtmlNode) null, oldChild.GetId());
      this.RemoveAllIDforNode(oldChild);
      this.SetChanged();
      return oldChild;
    }

    public HtmlNode RemoveChild(HtmlNode oldChild, bool keepGrandChildren)
    {
      if (oldChild == null)
        throw new ArgumentNullException(nameof (oldChild));
      if (oldChild._childnodes != null & keepGrandChildren)
      {
        HtmlNode refChild = oldChild.PreviousSibling;
        foreach (HtmlNode childnode in (IEnumerable<HtmlNode>) oldChild._childnodes)
          refChild = this.InsertAfter(childnode, refChild);
      }
      this.RemoveChild(oldChild);
      this.SetChanged();
      return oldChild;
    }

    public HtmlNode ReplaceChild(HtmlNode newChild, HtmlNode oldChild)
    {
      if (newChild == null)
        return this.RemoveChild(oldChild);
      if (oldChild == null)
        return this.AppendChild(newChild);
      int index = -1;
      if (this._childnodes != null)
        index = this._childnodes[oldChild];
      if (index == -1)
        throw new ArgumentException(HtmlDocument.HtmlExceptionRefNotChild);
      if (this._childnodes != null)
        this._childnodes.Replace(index, newChild);
      this._ownerdocument.SetIdForNode((HtmlNode) null, oldChild.GetId());
      this.RemoveAllIDforNode(oldChild);
      this._ownerdocument.SetIdForNode(newChild, newChild.GetId());
      this.SetChildNodesId(newChild);
      this.SetChanged();
      return newChild;
    }

    public HtmlAttribute SetAttributeValue(string name, string value)
    {
      HtmlAttribute htmlAttribute = name != null ? this.Attributes[name] : throw new ArgumentNullException(nameof (name));
      if (htmlAttribute == null)
        return this.Attributes.Append(this._ownerdocument.CreateAttribute(name, value));
      htmlAttribute.Value = value;
      return htmlAttribute;
    }

    public void WriteContentTo(TextWriter outText, int level = 0)
    {
      if (level > HtmlDocument.MaxDepthLevel)
        throw new ArgumentException("The document is too complex to parse");
      if (this._childnodes == null)
        return;
      foreach (HtmlNode childnode in (IEnumerable<HtmlNode>) this._childnodes)
        childnode.WriteTo(outText, level + 1);
    }

    public string WriteContentTo()
    {
      StringWriter outText = new StringWriter();
      this.WriteContentTo((TextWriter) outText);
      outText.Flush();
      return outText.ToString();
    }

    public virtual void WriteTo(TextWriter outText, int level = 0)
    {
      switch (this._nodetype)
      {
        case HtmlNodeType.Document:
          if (this._ownerdocument.OptionOutputAsXml)
          {
            outText.Write("<?xml version=\"1.0\" encoding=\"" + this._ownerdocument.GetOutEncoding().WebName + "\"?>");
            if (this._ownerdocument.DocumentNode.HasChildNodes)
            {
              int count = this._ownerdocument.DocumentNode._childnodes.Count;
              if (count > 0)
              {
                if (this._ownerdocument.GetXmlDeclaration() != null)
                  --count;
                if (count > 1)
                {
                  if (!this._ownerdocument.BackwardCompatibility)
                  {
                    this.WriteContentTo(outText, level);
                    break;
                  }
                  if (this._ownerdocument.OptionOutputUpperCase)
                  {
                    outText.Write("<SPAN>");
                    this.WriteContentTo(outText, level);
                    outText.Write("</SPAN>");
                    break;
                  }
                  outText.Write("<span>");
                  this.WriteContentTo(outText, level);
                  outText.Write("</span>");
                  break;
                }
              }
            }
          }
          this.WriteContentTo(outText, level);
          break;
        case HtmlNodeType.Element:
          string name = this._ownerdocument.OptionOutputUpperCase ? this.Name.ToUpperInvariant() : this.Name;
          if (this._ownerdocument.OptionOutputOriginalCase)
            name = this.OriginalName;
          if (this._ownerdocument.OptionOutputAsXml)
          {
            if (name.Length <= 0 || name[0] == '?' || name.Trim().Length == 0)
              break;
            name = HtmlDocument.GetXmlName(name);
          }
          outText.Write("<" + name);
          this.WriteAttributes(outText, false);
          if (this.HasChildNodes)
          {
            outText.Write(">");
            bool flag = false;
            if (this._ownerdocument.OptionOutputAsXml && HtmlNode.IsCDataElement(this.Name))
            {
              flag = true;
              outText.Write("\r\n//<![CDATA[\r\n");
            }
            if (flag)
            {
              if (this.HasChildNodes)
                this.ChildNodes[0].WriteTo(outText, level);
              outText.Write("\r\n//]]>//\r\n");
            }
            else
              this.WriteContentTo(outText, level);
            if (this._isImplicitEnd)
              break;
            outText.Write("</" + name);
            if (!this._ownerdocument.OptionOutputAsXml)
              this.WriteAttributes(outText, true);
            outText.Write(">");
            break;
          }
          if (HtmlNode.IsEmptyElement(this.Name))
          {
            if (this._ownerdocument.OptionWriteEmptyNodes || this._ownerdocument.OptionOutputAsXml)
            {
              outText.Write(" />");
              break;
            }
            if (this.Name.Length > 0 && this.Name[0] == '?')
              outText.Write("?");
            outText.Write(">");
            break;
          }
          if (this._isImplicitEnd)
            break;
          outText.Write("></" + name + ">");
          break;
        case HtmlNodeType.Comment:
          string comment1 = ((HtmlCommentNode) this).Comment;
          if (this._ownerdocument.OptionOutputAsXml)
          {
            HtmlCommentNode comment2 = (HtmlCommentNode) this;
            if (!this._ownerdocument.BackwardCompatibility && comment2.Comment.ToLowerInvariant().StartsWith("<!doctype"))
            {
              outText.Write(comment2.Comment);
              break;
            }
            outText.Write("<!--" + HtmlNode.GetXmlComment(comment2) + " -->");
            break;
          }
          outText.Write(comment1);
          break;
        case HtmlNodeType.Text:
          string text = ((HtmlTextNode) this).Text;
          outText.Write(this._ownerdocument.OptionOutputAsXml ? HtmlDocument.HtmlEncodeWithCompatibility(text, this._ownerdocument.BackwardCompatibility) : text);
          break;
      }
    }

    public void WriteTo(XmlWriter writer)
    {
      switch (this._nodetype)
      {
        case HtmlNodeType.Document:
          writer.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"" + this._ownerdocument.GetOutEncoding().WebName + "\"");
          if (!this.HasChildNodes)
            break;
          using (IEnumerator<HtmlNode> enumerator = ((IEnumerable<HtmlNode>) this.ChildNodes).GetEnumerator())
          {
            while (enumerator.MoveNext())
              enumerator.Current.WriteTo(writer);
            break;
          }
        case HtmlNodeType.Element:
          string localName = this._ownerdocument.OptionOutputUpperCase ? this.Name.ToUpperInvariant() : this.Name;
          if (this._ownerdocument.OptionOutputOriginalCase)
            localName = this.OriginalName;
          writer.WriteStartElement(localName);
          HtmlNode.WriteAttributes(writer, this);
          if (this.HasChildNodes)
          {
            foreach (HtmlNode childNode in (IEnumerable<HtmlNode>) this.ChildNodes)
              childNode.WriteTo(writer);
          }
          writer.WriteEndElement();
          break;
        case HtmlNodeType.Comment:
          writer.WriteComment(HtmlNode.GetXmlComment((HtmlCommentNode) this));
          break;
        case HtmlNodeType.Text:
          string text = ((HtmlTextNode) this).Text;
          writer.WriteString(text);
          break;
      }
    }

    public string WriteTo()
    {
      using (StringWriter outText = new StringWriter())
      {
        this.WriteTo((TextWriter) outText);
        outText.Flush();
        return outText.ToString();
      }
    }

    internal void SetChanged()
    {
      this._changed = true;
      if (this.ParentNode == null)
        return;
      this.ParentNode.SetChanged();
    }

    private void UpdateHtml()
    {
      this._innerhtml = this.WriteContentTo();
      this._outerhtml = this.WriteTo();
      this._changed = false;
    }

    internal static string GetXmlComment(HtmlCommentNode comment)
    {
      string comment1 = comment.Comment;
      return comment1.Substring(4, comment1.Length - 7).Replace("--", " - -");
    }

    internal static void WriteAttributes(XmlWriter writer, HtmlNode node)
    {
      if (!node.HasAttributes)
        return;
      foreach (HtmlAttribute htmlAttribute in node.Attributes.Hashitems.Values)
        writer.WriteAttributeString(htmlAttribute.XmlName, htmlAttribute.Value);
    }

    internal void CloseNode(HtmlNode endnode, int level = 0)
    {
      if (level > HtmlDocument.MaxDepthLevel)
        throw new ArgumentException("The document is too complex to parse");
      if (!this._ownerdocument.OptionAutoCloseOnEnd && this._childnodes != null)
      {
        foreach (HtmlNode childnode in (IEnumerable<HtmlNode>) this._childnodes)
        {
          if (!childnode.Closed)
          {
            HtmlNode endnode1 = new HtmlNode(this.NodeType, this._ownerdocument, -1);
            endnode1._endnode = endnode1;
            childnode.CloseNode(endnode1, level + 1);
          }
        }
      }
      if (this.Closed)
        return;
      this._endnode = endnode;
      if (this._ownerdocument.Openednodes != null)
        this._ownerdocument.Openednodes.Remove(this._outerstartindex);
      if (Utilities.GetDictionaryValueOrDefault<string, HtmlNode>(this._ownerdocument.Lastnodes, this.Name) == this)
      {
        this._ownerdocument.Lastnodes.Remove(this.Name);
        this._ownerdocument.UpdateLastParentNode();
      }
      if (endnode == this)
        return;
      this._innerstartindex = this._outerstartindex + this._outerlength;
      this._innerlength = endnode._outerstartindex - this._innerstartindex;
      this._outerlength = endnode._outerstartindex + endnode._outerlength - this._outerstartindex;
    }

    internal string GetId()
    {
      HtmlAttribute attribute = this.Attributes["id"];
      return attribute != null ? attribute.Value : string.Empty;
    }

    internal void SetId(string id)
    {
      HtmlAttribute htmlAttribute = this.Attributes[nameof (id)] ?? this._ownerdocument.CreateAttribute(nameof (id));
      htmlAttribute.Value = id;
      this._ownerdocument.SetIdForNode(this, htmlAttribute.Value);
      this.Attributes.Add(htmlAttribute);
      this.SetChanged();
    }

    internal void WriteAttribute(TextWriter outText, HtmlAttribute att)
    {
      if (att.Value == null)
        return;
      string str1 = att.QuoteType == AttributeValueQuote.DoubleQuote ? "\"" : "'";
      if (this._ownerdocument.OptionOutputAsXml)
      {
        string str2 = this._ownerdocument.OptionOutputUpperCase ? att.XmlName.ToUpperInvariant() : att.XmlName;
        if (this._ownerdocument.OptionOutputOriginalCase)
          str2 = att.OriginalName;
        outText.Write(" " + str2 + "=" + str1 + HtmlDocument.HtmlEncodeWithCompatibility(att.XmlValue, this._ownerdocument.BackwardCompatibility) + str1);
      }
      else
      {
        string str3 = this._ownerdocument.OptionOutputUpperCase ? att.Name.ToUpperInvariant() : att.Name;
        if (this._ownerdocument.OptionOutputOriginalCase)
          str3 = att.OriginalName;
        if (att.Name.Length >= 4 && att.Name[0] == '<' && att.Name[1] == '%' && att.Name[att.Name.Length - 1] == '>' && att.Name[att.Name.Length - 2] == '%')
          outText.Write(" " + str3);
        else if (this._ownerdocument.OptionOutputOptimizeAttributeValues)
        {
          if (att.Value.IndexOfAny(new char[4]
          {
            '\n',
            '\r',
            '\t',
            ' '
          }) < 0)
            outText.Write(" " + str3 + "=" + att.Value);
          else
            outText.Write(" " + str3 + "=" + str1 + att.Value + str1);
        }
        else
          outText.Write(" " + str3 + "=" + str1 + att.Value + str1);
      }
    }

    internal void WriteAttributes(TextWriter outText, bool closing)
    {
      if (this._ownerdocument.OptionOutputAsXml)
      {
        if (this._attributes == null)
          return;
        foreach (HtmlAttribute att in this._attributes.Hashitems.Values)
          this.WriteAttribute(outText, att);
      }
      else if (!closing)
      {
        if (this._attributes != null)
        {
          foreach (HtmlAttribute attribute in (IEnumerable<HtmlAttribute>) this._attributes)
            this.WriteAttribute(outText, attribute);
        }
        if (!this._ownerdocument.OptionAddDebuggingAttributes)
          return;
        this.WriteAttribute(outText, this._ownerdocument.CreateAttribute("_closed", this.Closed.ToString()));
        this.WriteAttribute(outText, this._ownerdocument.CreateAttribute("_children", this.ChildNodes.Count.ToString()));
        int num = 0;
        foreach (HtmlNode childNode in (IEnumerable<HtmlNode>) this.ChildNodes)
        {
          this.WriteAttribute(outText, this._ownerdocument.CreateAttribute("_child_" + (object) num, childNode.Name));
          ++num;
        }
      }
      else
      {
        if (this._endnode == null || this._endnode._attributes == null || this._endnode == this)
          return;
        foreach (HtmlAttribute attribute in (IEnumerable<HtmlAttribute>) this._endnode._attributes)
          this.WriteAttribute(outText, attribute);
        if (!this._ownerdocument.OptionAddDebuggingAttributes)
          return;
        this.WriteAttribute(outText, this._ownerdocument.CreateAttribute("_closed", this.Closed.ToString()));
        this.WriteAttribute(outText, this._ownerdocument.CreateAttribute("_children", this.ChildNodes.Count.ToString()));
      }
    }

    private string GetRelativeXpath()
    {
      if (this.ParentNode == null)
        return this.Name;
      if (this.NodeType == HtmlNodeType.Document)
        return string.Empty;
      int num = 1;
      foreach (HtmlNode childNode in (IEnumerable<HtmlNode>) this.ParentNode.ChildNodes)
      {
        if (!(childNode.Name != this.Name))
        {
          if (childNode != this)
            ++num;
          else
            break;
        }
      }
      return this.Name + "[" + (object) num + "]";
    }

    private bool IsSingleElementNode()
    {
      int num = 0;
      for (HtmlNode htmlNode = this.FirstChild; htmlNode != null; htmlNode = htmlNode.NextSibling)
      {
        if (htmlNode.NodeType == HtmlNodeType.Element && htmlNode.OuterHtml != "\r\n")
          ++num;
      }
      return num <= 1;
    }

    public void AddClass(string name) => this.AddClass(name, false);

    public void AddClass(string name, bool throwError)
    {
      IEnumerable<HtmlAttribute> en = this.Attributes.AttributesWithName("class");
      if (!this.IsEmpty((IEnumerable) en))
      {
        foreach (HtmlAttribute htmlAttribute in en)
        {
          if (htmlAttribute.Value.Equals(name) || htmlAttribute.Value.Contains(name))
          {
            if (throwError)
              throw new Exception(HtmlDocument.HtmlExceptionClassExists);
          }
          else
            this.SetAttributeValue(htmlAttribute.Name, htmlAttribute.Value + " " + name);
        }
      }
      else
        this.Attributes.Append(this._ownerdocument.CreateAttribute("class", name));
    }

    public void RemoveClass() => this.RemoveClass(false);

    public void RemoveClass(bool throwError)
    {
      IEnumerable<HtmlAttribute> en = this.Attributes.AttributesWithName("class");
      if (this.IsEmpty((IEnumerable) en) & throwError)
        throw new Exception(HtmlDocument.HtmlExceptionClassDoesNotExist);
      foreach (HtmlAttribute attribute in en)
        this.Attributes.Remove(attribute);
    }

    public void RemoveClass(string name) => this.RemoveClass(name, false);

    public void RemoveClass(string name, bool throwError)
    {
      IEnumerable<HtmlAttribute> en = this.Attributes.AttributesWithName("class");
      if (this.IsEmpty((IEnumerable) en) & throwError)
        throw new Exception(HtmlDocument.HtmlExceptionClassDoesNotExist);
      foreach (HtmlAttribute attribute in en)
      {
        if (attribute.Value != null)
        {
          if (attribute.Value.Equals(name))
            this.Attributes.Remove(attribute);
          else if (attribute.Value.Contains(name))
          {
            string[] strArray = attribute.Value.Split(' ');
            string str1 = "";
            foreach (string str2 in strArray)
            {
              if (!str2.Equals(name))
                str1 = str1 + str2 + " ";
            }
            string str3 = str1.Trim();
            this.SetAttributeValue(attribute.Name, str3);
          }
          else if (throwError)
            throw new Exception(HtmlDocument.HtmlExceptionClassDoesNotExist);
          if (string.IsNullOrEmpty(attribute.Value))
            this.Attributes.Remove(attribute);
        }
      }
    }

    public void ReplaceClass(string newClass, string oldClass)
    {
      this.ReplaceClass(newClass, oldClass, false);
    }

    public void ReplaceClass(string newClass, string oldClass, bool throwError)
    {
      if (string.IsNullOrEmpty(newClass))
        this.RemoveClass(oldClass);
      if (string.IsNullOrEmpty(oldClass))
        this.AddClass(newClass);
      IEnumerable<HtmlAttribute> en = this.Attributes.AttributesWithName("class");
      if (this.IsEmpty((IEnumerable) en) & throwError)
        throw new Exception(HtmlDocument.HtmlExceptionClassDoesNotExist);
      foreach (HtmlAttribute htmlAttribute in en)
      {
        if (htmlAttribute.Value != null)
        {
          if (htmlAttribute.Value.Equals(oldClass) || htmlAttribute.Value.Contains(oldClass))
          {
            string str = htmlAttribute.Value.Replace(oldClass, newClass);
            this.SetAttributeValue(htmlAttribute.Name, str);
          }
          else if (throwError)
            throw new Exception(HtmlDocument.HtmlExceptionClassDoesNotExist);
        }
      }
    }

    public IEnumerable<string> GetClasses()
    {
      foreach (HtmlAttribute htmlAttribute in this.Attributes.AttributesWithName("class"))
      {
        string[] strArray = htmlAttribute.Value.Split(new string[1]
        {
          " "
        }, StringSplitOptions.RemoveEmptyEntries);
        for (int index = 0; index < strArray.Length; ++index)
          yield return strArray[index];
        strArray = (string[]) null;
      }
    }

    public bool HasClass(string className)
    {
      foreach (string str1 in this.GetClasses())
      {
        char[] chArray = new char[1]{ ' ' };
        foreach (string str2 in str1.Split(chArray))
        {
          if (str2 == className)
            return true;
        }
      }
      return false;
    }

    private bool IsEmpty(IEnumerable en)
    {
      IEnumerator enumerator = en.GetEnumerator();
      try
      {
        if (enumerator.MoveNext())
        {
          object current = enumerator.Current;
          return false;
        }
      }
      finally
      {
        if (enumerator is IDisposable disposable)
          disposable.Dispose();
      }
      return true;
    }
  }
}
