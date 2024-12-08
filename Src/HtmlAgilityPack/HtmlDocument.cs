
// Type: HtmlAgilityPack.HtmlDocument
// Assembly: HtmlAgilityPack, Version=1.7.1.0, Culture=neutral, PublicKeyToken=bd319b19eaf3b43a
// MVID: B5498F67-4C4E-4FFB-BDE4-EB084EEE38F2
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\HtmlAgilityPack.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

#nullable disable
namespace HtmlAgilityPack
{
  public class HtmlDocument
  {
    public static bool DisableBehavaiorTagP;
    private static int _maxDepthLevel = int.MaxValue;
    private int _c;
    private Crc32 _crc32;
    private HtmlAttribute _currentattribute;
    private HtmlNode _currentnode;
    private Encoding _declaredencoding;
    private HtmlNode _documentnode;
    private bool _fullcomment;
    private int _index;
    internal Dictionary<string, HtmlNode> Lastnodes = new Dictionary<string, HtmlNode>();
    private HtmlNode _lastparentnode;
    private int _line;
    private int _lineposition;
    private int _maxlineposition;
    internal Dictionary<string, HtmlNode> Nodesid;
    private HtmlDocument.ParseState _oldstate;
    private bool _onlyDetectEncoding;
    internal Dictionary<int, HtmlNode> Openednodes;
    private List<HtmlParseError> _parseerrors = new List<HtmlParseError>();
    private string _remainder;
    private int _remainderOffset;
    private HtmlDocument.ParseState _state;
    private Encoding _streamencoding;
    internal string Text;
    public bool BackwardCompatibility = true;
    public bool OptionAddDebuggingAttributes;
    public bool OptionAutoCloseOnEnd;
    public bool OptionCheckSyntax = true;
    public bool OptionComputeChecksum;
    public bool OptionEmptyCollection;
    public bool DisableServerSideCode;
    public Encoding OptionDefaultStreamEncoding;
    public bool OptionExtractErrorSourceText;
    public int OptionExtractErrorSourceTextMaxLength = 100;
    public bool OptionFixNestedTags;
    public bool OptionOutputAsXml;
    public bool OptionOutputOptimizeAttributeValues;
    public bool OptionOutputOriginalCase;
    public bool OptionOutputUpperCase;
    public bool OptionReadEncoding = true;
    public string OptionStopperNodeName;
    public bool OptionUseIdAttribute = true;
    public bool OptionWriteEmptyNodes;
    internal static readonly string HtmlExceptionRefNotChild = "Reference node must be a child of this node";
    internal static readonly string HtmlExceptionUseIdAttributeFalse = "You need to set UseIdAttribute property to true to enable this feature";
    internal static readonly string HtmlExceptionClassDoesNotExist = "Class name doesn't exist";
    internal static readonly string HtmlExceptionClassExists = "Class name already exists";
    internal static readonly Dictionary<string, string[]> HtmlResetters = new Dictionary<string, string[]>()
    {
      {
        "li",
        new string[2]{ "ul", "ol" }
      },
      {
        "tr",
        new string[1]{ "table" }
      },
      {
        "th",
        new string[2]{ "tr", "table" }
      },
      {
        "td",
        new string[2]{ "tr", "table" }
      }
    };

    public static Action<HtmlDocument> DefaultBuilder { get; set; }

    public HtmlDocument()
    {
      if (HtmlDocument.DefaultBuilder != null)
        HtmlDocument.DefaultBuilder(this);
      this._documentnode = this.CreateNode(HtmlNodeType.Document, 0);
      this.OptionDefaultStreamEncoding = Encoding.UTF8;
    }

    public string ParsedText => this.Text;

    public static int MaxDepthLevel
    {
      get => HtmlDocument._maxDepthLevel;
      set => HtmlDocument._maxDepthLevel = value;
    }

    public int CheckSum => this._crc32 != null ? (int) this._crc32.CheckSum : 0;

    public Encoding DeclaredEncoding => this._declaredencoding;

    public HtmlNode DocumentNode => this._documentnode;

    public Encoding Encoding => this.GetOutEncoding();

    public IEnumerable<HtmlParseError> ParseErrors
    {
      get => (IEnumerable<HtmlParseError>) this._parseerrors;
    }

    public string Remainder => this._remainder;

    public int RemainderOffset => this._remainderOffset;

    public Encoding StreamEncoding => this._streamencoding;

    public static string GetXmlName(string name) => HtmlDocument.GetXmlName(name, false);

    public static string GetXmlName(string name, bool isAttribute)
    {
      string empty = string.Empty;
      bool flag = true;
      for (int index = 0; index < name.Length; ++index)
      {
        if (name[index] >= 'a' && name[index] <= 'z' || name[index] >= 'A' && name[index] <= 'Z' || name[index] >= '0' && name[index] <= '9' || isAttribute && name[index] == ':' || name[index] == '_' || name[index] == '-' || name[index] == '.')
        {
          empty += name[index].ToString();
        }
        else
        {
          flag = false;
          Encoding utF8 = Encoding.UTF8;
          char[] chars = new char[1]{ name[index] };
          foreach (byte num in utF8.GetBytes(chars))
            empty += num.ToString("x2");
          empty += "_";
        }
      }
      return flag ? empty : "_" + empty;
    }

    public static string HtmlEncode(string html) => HtmlDocument.HtmlEncodeWithCompatibility(html);

    internal static string HtmlEncodeWithCompatibility(string html, bool backwardCompatibility = true)
    {
      if (html == null)
        throw new ArgumentNullException(nameof (html));
      return (backwardCompatibility ? new Regex("&(?!(amp;)|(lt;)|(gt;)|(quot;))", RegexOptions.IgnoreCase) : new Regex("&(?!(amp;)|(lt;)|(gt;)|(quot;)|(nbsp;)|(reg;))", RegexOptions.IgnoreCase)).Replace(html, "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;");
    }

    public static bool IsWhiteSpace(int c) => c == 10 || c == 13 || c == 32 || c == 9;

    public HtmlAttribute CreateAttribute(string name)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      HtmlAttribute attribute = this.CreateAttribute();
      attribute.Name = name;
      return attribute;
    }

    public HtmlAttribute CreateAttribute(string name, string value)
    {
      HtmlAttribute attribute = name != null ? this.CreateAttribute(name) : throw new ArgumentNullException(nameof (name));
      attribute.Value = value;
      return attribute;
    }

    public HtmlCommentNode CreateComment()
    {
      return (HtmlCommentNode) this.CreateNode(HtmlNodeType.Comment);
    }

    public HtmlCommentNode CreateComment(string comment)
    {
      if (comment == null)
        throw new ArgumentNullException(nameof (comment));
      HtmlCommentNode comment1 = this.CreateComment();
      comment1.Comment = comment;
      return comment1;
    }

    public HtmlNode CreateElement(string name)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      HtmlNode node = this.CreateNode(HtmlNodeType.Element);
      node.Name = name;
      return node;
    }

    public HtmlTextNode CreateTextNode() => (HtmlTextNode) this.CreateNode(HtmlNodeType.Text);

    public HtmlTextNode CreateTextNode(string text)
    {
      if (text == null)
        throw new ArgumentNullException(nameof (text));
      HtmlTextNode textNode = this.CreateTextNode();
      textNode.Text = text;
      return textNode;
    }

    public Encoding DetectEncoding(Stream stream)
    {
      return stream != null ? this.DetectEncoding((TextReader) new StreamReader(stream)) : throw new ArgumentNullException(nameof (stream));
    }

    public Encoding DetectEncoding(TextReader reader)
    {
      if (reader == null)
        throw new ArgumentNullException(nameof (reader));
      this._onlyDetectEncoding = true;
      this.Openednodes = !this.OptionCheckSyntax ? (Dictionary<int, HtmlNode>) null : new Dictionary<int, HtmlNode>();
      this.Nodesid = !this.OptionUseIdAttribute ? (Dictionary<string, HtmlNode>) null : new Dictionary<string, HtmlNode>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      if (reader is StreamReader streamReader)
      {
        this.Text = streamReader.ReadToEnd();
        this._streamencoding = streamReader.CurrentEncoding;
        return this._streamencoding;
      }
      this._streamencoding = (Encoding) null;
      this._declaredencoding = (Encoding) null;
      this.Text = reader.ReadToEnd();
      this._documentnode = this.CreateNode(HtmlNodeType.Document, 0);
      try
      {
        this.Parse();
      }
      catch (EncodingFoundException ex)
      {
        return ex.Encoding;
      }
      return this._streamencoding;
    }

    public Encoding DetectEncodingHtml(string html)
    {
      if (html == null)
        throw new ArgumentNullException(nameof (html));
      using (StringReader reader = new StringReader(html))
        return this.DetectEncoding((TextReader) reader);
    }

    public HtmlNode GetElementbyId(string id)
    {
      if (id == null)
        throw new ArgumentNullException(nameof (id));
      if (this.Nodesid == null)
        throw new Exception(HtmlDocument.HtmlExceptionUseIdAttributeFalse);
      return !this.Nodesid.ContainsKey(id) ? (HtmlNode) null : this.Nodesid[id];
    }

    public void Load(Stream stream)
    {
      this.Load((TextReader) new StreamReader(stream, this.OptionDefaultStreamEncoding));
    }

    public void Load(Stream stream, bool detectEncodingFromByteOrderMarks)
    {
      this.Load((TextReader) new StreamReader(stream, detectEncodingFromByteOrderMarks));
    }

    public void Load(Stream stream, Encoding encoding)
    {
      this.Load((TextReader) new StreamReader(stream, encoding));
    }

    public void Load(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks)
    {
      this.Load((TextReader) new StreamReader(stream, encoding, detectEncodingFromByteOrderMarks));
    }

    public void Load(
      Stream stream,
      Encoding encoding,
      bool detectEncodingFromByteOrderMarks,
      int buffersize)
    {
      this.Load((TextReader) new StreamReader(stream, encoding, detectEncodingFromByteOrderMarks, buffersize));
    }

    public void Load(TextReader reader)
    {
      if (reader == null)
        throw new ArgumentNullException(nameof (reader));
      this._onlyDetectEncoding = false;
      this.Openednodes = !this.OptionCheckSyntax ? (Dictionary<int, HtmlNode>) null : new Dictionary<int, HtmlNode>();
      this.Nodesid = !this.OptionUseIdAttribute ? (Dictionary<string, HtmlNode>) null : new Dictionary<string, HtmlNode>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      if (reader is StreamReader streamReader)
      {
        try
        {
          streamReader.Peek();
        }
        catch (Exception ex)
        {
           System.Diagnostics.Debug.WriteLine(ex.Message);
        }
        this._streamencoding = streamReader.CurrentEncoding;
      }
      else
        this._streamencoding = (Encoding) null;
      this._declaredencoding = (Encoding) null;
      this.Text = reader.ReadToEnd();
      this._documentnode = this.CreateNode(HtmlNodeType.Document, 0);
      this.Parse();
      if (!this.OptionCheckSyntax || this.Openednodes == null)
        return;
      foreach (HtmlNode htmlNode in this.Openednodes.Values)
      {
        if (htmlNode._starttag)
        {
          string sourceText;
          if (this.OptionExtractErrorSourceText)
          {
            sourceText = htmlNode.OuterHtml;
            if (sourceText.Length > this.OptionExtractErrorSourceTextMaxLength)
              sourceText = sourceText.Substring(0, this.OptionExtractErrorSourceTextMaxLength);
          }
          else
            sourceText = string.Empty;
          this.AddError(HtmlParseErrorCode.TagNotClosed, htmlNode._line, htmlNode._lineposition, htmlNode._streamposition, sourceText, "End tag </" + htmlNode.Name + "> was not found");
        }
      }
      this.Openednodes.Clear();
    }

    public void LoadHtml(string html)
    {
      if (html == null)
        throw new ArgumentNullException(nameof (html));
      using (StringReader reader = new StringReader(html))
        this.Load((TextReader) reader);
    }

    public void Save(Stream outStream)
    {
      this.Save(new StreamWriter(outStream, this.GetOutEncoding()));
    }

    public void Save(Stream outStream, Encoding encoding)
    {
      if (outStream == null)
        throw new ArgumentNullException(nameof (outStream));
      if (encoding == null)
        throw new ArgumentNullException(nameof (encoding));
      this.Save(new StreamWriter(outStream, encoding));
    }

    public void Save(StreamWriter writer) => this.Save((TextWriter) writer);

    public void Save(TextWriter writer)
    {
      if (writer == null)
        throw new ArgumentNullException(nameof (writer));
      this.DocumentNode.WriteTo(writer);
      writer.Flush();
    }

    public void Save(XmlWriter writer)
    {
      this.DocumentNode.WriteTo(writer);
      writer.Flush();
    }

    internal HtmlAttribute CreateAttribute() => new HtmlAttribute(this);

    internal HtmlNode CreateNode(HtmlNodeType type) => this.CreateNode(type, -1);

    internal HtmlNode CreateNode(HtmlNodeType type, int index)
    {
      if (type == HtmlNodeType.Comment)
        return (HtmlNode) new HtmlCommentNode(this, index);
      return type == HtmlNodeType.Text ? (HtmlNode) new HtmlTextNode(this, index) : new HtmlNode(type, this, index);
    }

    internal Encoding GetOutEncoding()
    {
      return this._declaredencoding ?? this._streamencoding ?? this.OptionDefaultStreamEncoding;
    }

    internal HtmlNode GetXmlDeclaration()
    {
      if (!this._documentnode.HasChildNodes)
        return (HtmlNode) null;
      foreach (HtmlNode childnode in (IEnumerable<HtmlNode>) this._documentnode._childnodes)
      {
        if (childnode.Name == "?xml")
          return childnode;
      }
      return (HtmlNode) null;
    }

    internal void SetIdForNode(HtmlNode node, string id)
    {
      if (!this.OptionUseIdAttribute || this.Nodesid == null || id == null)
        return;
      if (node == null)
        this.Nodesid.Remove(id);
      else
        this.Nodesid[id] = node;
    }

    internal void UpdateLastParentNode()
    {
      do
      {
        if (this._lastparentnode.Closed)
          this._lastparentnode = this._lastparentnode.ParentNode;
      }
      while (this._lastparentnode != null && this._lastparentnode.Closed);
      if (this._lastparentnode != null)
        return;
      this._lastparentnode = this._documentnode;
    }

    private void AddError(
      HtmlParseErrorCode code,
      int line,
      int linePosition,
      int streamPosition,
      string sourceText,
      string reason)
    {
      this._parseerrors.Add(new HtmlParseError(code, line, linePosition, streamPosition, sourceText, reason));
    }

    private void CloseCurrentNode()
    {
      if (this._currentnode.Closed)
        return;
      bool flag = false;
      HtmlNode dictionaryValueOrDefault = Utilities.GetDictionaryValueOrDefault<string, HtmlNode>(this.Lastnodes, this._currentnode.Name);
      if (dictionaryValueOrDefault == null)
      {
        if (HtmlNode.IsClosedElement(this._currentnode.Name))
        {
          this._currentnode.CloseNode(this._currentnode);
          if (this._lastparentnode != null)
          {
            HtmlNode htmlNode1 = (HtmlNode) null;
            Stack<HtmlNode> htmlNodeStack = new Stack<HtmlNode>();
            for (HtmlNode htmlNode2 = this._lastparentnode.LastChild; htmlNode2 != null; htmlNode2 = htmlNode2.PreviousSibling)
            {
              if (htmlNode2.Name == this._currentnode.Name && !htmlNode2.HasChildNodes)
              {
                htmlNode1 = htmlNode2;
                break;
              }
              htmlNodeStack.Push(htmlNode2);
            }
            if (htmlNode1 != null)
            {
              while (htmlNodeStack.Count != 0)
              {
                HtmlNode htmlNode3 = htmlNodeStack.Pop();
                this._lastparentnode.RemoveChild(htmlNode3);
                htmlNode1.AppendChild(htmlNode3);
              }
            }
            else
              this._lastparentnode.AppendChild(this._currentnode);
          }
        }
        else if (HtmlNode.CanOverlapElement(this._currentnode.Name))
        {
          HtmlNode node = this.CreateNode(HtmlNodeType.Text, this._currentnode._outerstartindex);
          node._outerlength = this._currentnode._outerlength;
          ((HtmlTextNode) node).Text = ((HtmlTextNode) node).Text.ToLowerInvariant();
          if (this._lastparentnode != null)
            this._lastparentnode.AppendChild(node);
        }
        else if (HtmlNode.IsEmptyElement(this._currentnode.Name))
        {
          this.AddError(HtmlParseErrorCode.EndTagNotRequired, this._currentnode._line, this._currentnode._lineposition, this._currentnode._streamposition, this._currentnode.OuterHtml, "End tag </" + this._currentnode.Name + "> is not required");
        }
        else
        {
          this.AddError(HtmlParseErrorCode.TagNotOpened, this._currentnode._line, this._currentnode._lineposition, this._currentnode._streamposition, this._currentnode.OuterHtml, "Start tag <" + this._currentnode.Name + "> was not found");
          flag = true;
        }
      }
      else
      {
        if (this.OptionFixNestedTags && this.FindResetterNodes(dictionaryValueOrDefault, this.GetResetters(this._currentnode.Name)))
        {
          this.AddError(HtmlParseErrorCode.EndTagInvalidHere, this._currentnode._line, this._currentnode._lineposition, this._currentnode._streamposition, this._currentnode.OuterHtml, "End tag </" + this._currentnode.Name + "> invalid here");
          flag = true;
        }
        if (!flag)
        {
          this.Lastnodes[this._currentnode.Name] = dictionaryValueOrDefault._prevwithsamename;
          dictionaryValueOrDefault.CloseNode(this._currentnode);
        }
      }
      if (flag || this._lastparentnode == null || HtmlNode.IsClosedElement(this._currentnode.Name) && !this._currentnode._starttag)
        return;
      this.UpdateLastParentNode();
    }

    private string CurrentNodeName()
    {
      return this.Text.Substring(this._currentnode._namestartindex, this._currentnode._namelength);
    }

    private void DecrementPosition()
    {
      --this._index;
      if (this._lineposition == 1)
      {
        this._lineposition = this._maxlineposition;
        --this._line;
      }
      else
        --this._lineposition;
    }

    private HtmlNode FindResetterNode(HtmlNode node, string name)
    {
      HtmlNode dictionaryValueOrDefault = Utilities.GetDictionaryValueOrDefault<string, HtmlNode>(this.Lastnodes, name);
      if (dictionaryValueOrDefault == null)
        return (HtmlNode) null;
      if (dictionaryValueOrDefault.Closed)
        return (HtmlNode) null;
      return dictionaryValueOrDefault._streamposition < node._streamposition ? (HtmlNode) null : dictionaryValueOrDefault;
    }

    private bool FindResetterNodes(HtmlNode node, string[] names)
    {
      if (names == null)
        return false;
      for (int index = 0; index < names.Length; ++index)
      {
        if (this.FindResetterNode(node, names[index]) != null)
          return true;
      }
      return false;
    }

    private void FixNestedTag(string name, string[] resetters)
    {
      if (resetters == null)
        return;
      HtmlNode dictionaryValueOrDefault = Utilities.GetDictionaryValueOrDefault<string, HtmlNode>(this.Lastnodes, this._currentnode.Name);
      if (dictionaryValueOrDefault == null || this.Lastnodes[name].Closed || this.FindResetterNodes(dictionaryValueOrDefault, resetters))
        return;
      HtmlNode endnode = new HtmlNode(dictionaryValueOrDefault.NodeType, this, -1);
      endnode._endnode = endnode;
      dictionaryValueOrDefault.CloseNode(endnode);
    }

    private void FixNestedTags()
    {
      if (!this._currentnode._starttag)
        return;
      string name = this.CurrentNodeName();
      this.FixNestedTag(name, this.GetResetters(name));
    }

    private string[] GetResetters(string name)
    {
      string[] strArray;
      return !HtmlDocument.HtmlResetters.TryGetValue(name, out strArray) ? (string[]) null : strArray;
    }

    private void IncrementPosition()
    {
      if (this._crc32 != null)
      {
        int crC32 = (int) this._crc32.AddToCRC32(this._c);
      }
      ++this._index;
      this._maxlineposition = this._lineposition;
      if (this._c == 10)
      {
        this._lineposition = 1;
        ++this._line;
      }
      else
        ++this._lineposition;
    }

    private bool IsValidTag()
    {
      if (this._c != 60 || this._index >= this.Text.Length)
        return false;
      return char.IsLetter(this.Text[this._index]) || this.Text[this._index] == '/' || this.Text[this._index] == '!' || this.Text[this._index] == '%';
    }

    private bool NewCheck()
    {
      if (this._c != 60 || !this.IsValidTag())
        return false;
      if (this._index < this.Text.Length && this.Text[this._index] == '%')
      {
        if (this.DisableServerSideCode)
          return false;
        switch (this._state)
        {
          case HtmlDocument.ParseState.WhichTag:
            this.PushNodeNameStart(true, this._index - 1);
            this._state = HtmlDocument.ParseState.Tag;
            break;
          case HtmlDocument.ParseState.BetweenAttributes:
            this.PushAttributeNameStart(this._index - 1);
            break;
          case HtmlDocument.ParseState.AttributeAfterEquals:
            this.PushAttributeValueStart(this._index - 1);
            break;
        }
        this._oldstate = this._state;
        this._state = HtmlDocument.ParseState.ServerSideCode;
        return true;
      }
      if (!this.PushNodeEnd(this._index - 1, true))
      {
        this._index = this.Text.Length;
        return true;
      }
      this._state = HtmlDocument.ParseState.WhichTag;
      if (this._index - 1 <= this.Text.Length - 2 && this.Text[this._index] == '!')
      {
        this.PushNodeStart(HtmlNodeType.Comment, this._index - 1);
        this.PushNodeNameStart(true, this._index);
        this.PushNodeNameEnd(this._index + 1);
        this._state = HtmlDocument.ParseState.Comment;
        if (this._index < this.Text.Length - 2)
          this._fullcomment = this.Text[this._index + 1] == '-' && this.Text[this._index + 2] == '-';
        return true;
      }
      this.PushNodeStart(HtmlNodeType.Element, this._index - 1);
      return true;
    }

    private void Parse()
    {
      int num = 0;
      if (this.OptionComputeChecksum)
        this._crc32 = new Crc32();
      this.Lastnodes = new Dictionary<string, HtmlNode>();
      this._c = 0;
      this._fullcomment = false;
      this._parseerrors = new List<HtmlParseError>();
      this._line = 1;
      this._lineposition = 1;
      this._maxlineposition = 1;
      this._state = HtmlDocument.ParseState.Text;
      this._oldstate = this._state;
      this._documentnode._innerlength = this.Text.Length;
      this._documentnode._outerlength = this.Text.Length;
      this._remainderOffset = this.Text.Length;
      this._lastparentnode = this._documentnode;
      this._currentnode = this.CreateNode(HtmlNodeType.Text, 0);
      this._currentattribute = (HtmlAttribute) null;
      this._index = 0;
      this.PushNodeStart(HtmlNodeType.Text, 0);
      while (this._index < this.Text.Length)
      {
        this._c = (int) this.Text[this._index];
        this.IncrementPosition();
        switch (this._state)
        {
          case HtmlDocument.ParseState.Text:
            if (!this.NewCheck())
              continue;
            continue;
          case HtmlDocument.ParseState.WhichTag:
            if (!this.NewCheck())
            {
              if (this._c == 47)
              {
                this.PushNodeNameStart(false, this._index);
              }
              else
              {
                this.PushNodeNameStart(true, this._index - 1);
                this.DecrementPosition();
              }
              this._state = HtmlDocument.ParseState.Tag;
              continue;
            }
            continue;
          case HtmlDocument.ParseState.Tag:
            if (!this.NewCheck())
            {
              if (HtmlDocument.IsWhiteSpace(this._c))
              {
                this.CloseParentImplicitExplicitNode();
                this.PushNodeNameEnd(this._index - 1);
                if (this._state == HtmlDocument.ParseState.Tag)
                {
                  this._state = HtmlDocument.ParseState.BetweenAttributes;
                  continue;
                }
                continue;
              }
              if (this._c == 47)
              {
                this.CloseParentImplicitExplicitNode();
                this.PushNodeNameEnd(this._index - 1);
                if (this._state == HtmlDocument.ParseState.Tag)
                {
                  this._state = HtmlDocument.ParseState.EmptyTag;
                  continue;
                }
                continue;
              }
              if (this._c == 62)
              {
                this.CloseParentImplicitExplicitNode();
                this.PushNodeNameEnd(this._index - 1);
                if (this._state == HtmlDocument.ParseState.Tag)
                {
                  if (!this.PushNodeEnd(this._index, false))
                  {
                    this._index = this.Text.Length;
                    continue;
                  }
                  if (this._state == HtmlDocument.ParseState.Tag)
                  {
                    this._state = HtmlDocument.ParseState.Text;
                    this.PushNodeStart(HtmlNodeType.Text, this._index);
                    continue;
                  }
                  continue;
                }
                continue;
              }
              continue;
            }
            continue;
          case HtmlDocument.ParseState.BetweenAttributes:
            if (!this.NewCheck() && !HtmlDocument.IsWhiteSpace(this._c))
            {
              if (this._c == 47 || this._c == 63)
              {
                this._state = HtmlDocument.ParseState.EmptyTag;
                continue;
              }
              if (this._c == 62)
              {
                if (!this.PushNodeEnd(this._index, false))
                {
                  this._index = this.Text.Length;
                  continue;
                }
                if (this._state == HtmlDocument.ParseState.BetweenAttributes)
                {
                  this._state = HtmlDocument.ParseState.Text;
                  this.PushNodeStart(HtmlNodeType.Text, this._index);
                  continue;
                }
                continue;
              }
              this.PushAttributeNameStart(this._index - 1);
              this._state = HtmlDocument.ParseState.AttributeName;
              continue;
            }
            continue;
          case HtmlDocument.ParseState.EmptyTag:
            if (!this.NewCheck())
            {
              if (this._c == 62)
              {
                if (!this.PushNodeEnd(this._index, true))
                {
                  this._index = this.Text.Length;
                  continue;
                }
                if (this._state == HtmlDocument.ParseState.EmptyTag)
                {
                  this._state = HtmlDocument.ParseState.Text;
                  this.PushNodeStart(HtmlNodeType.Text, this._index);
                  continue;
                }
                continue;
              }
              if (!HtmlDocument.IsWhiteSpace(this._c))
              {
                this.DecrementPosition();
                this._state = HtmlDocument.ParseState.BetweenAttributes;
                continue;
              }
              this._state = HtmlDocument.ParseState.BetweenAttributes;
              continue;
            }
            continue;
          case HtmlDocument.ParseState.AttributeName:
            if (!this.NewCheck())
            {
              if (HtmlDocument.IsWhiteSpace(this._c))
              {
                this.PushAttributeNameEnd(this._index - 1);
                this._state = HtmlDocument.ParseState.AttributeBeforeEquals;
                continue;
              }
              if (this._c == 61)
              {
                this.PushAttributeNameEnd(this._index - 1);
                this._state = HtmlDocument.ParseState.AttributeAfterEquals;
                continue;
              }
              if (this._c == 62)
              {
                this.PushAttributeNameEnd(this._index - 1);
                if (!this.PushNodeEnd(this._index, false))
                {
                  this._index = this.Text.Length;
                  continue;
                }
                if (this._state == HtmlDocument.ParseState.AttributeName)
                {
                  this._state = HtmlDocument.ParseState.Text;
                  this.PushNodeStart(HtmlNodeType.Text, this._index);
                  continue;
                }
                continue;
              }
              continue;
            }
            continue;
          case HtmlDocument.ParseState.AttributeBeforeEquals:
            if (!this.NewCheck() && !HtmlDocument.IsWhiteSpace(this._c))
            {
              if (this._c == 62)
              {
                if (!this.PushNodeEnd(this._index, false))
                {
                  this._index = this.Text.Length;
                  continue;
                }
                if (this._state == HtmlDocument.ParseState.AttributeBeforeEquals)
                {
                  this._state = HtmlDocument.ParseState.Text;
                  this.PushNodeStart(HtmlNodeType.Text, this._index);
                  continue;
                }
                continue;
              }
              if (this._c == 61)
              {
                this._state = HtmlDocument.ParseState.AttributeAfterEquals;
                continue;
              }
              this._state = HtmlDocument.ParseState.BetweenAttributes;
              this.DecrementPosition();
              continue;
            }
            continue;
          case HtmlDocument.ParseState.AttributeAfterEquals:
            if (!this.NewCheck() && !HtmlDocument.IsWhiteSpace(this._c))
            {
              if (this._c == 39 || this._c == 34)
              {
                this._state = HtmlDocument.ParseState.QuotedAttributeValue;
                this.PushAttributeValueStart(this._index, this._c);
                num = this._c;
                continue;
              }
              if (this._c == 62)
              {
                if (!this.PushNodeEnd(this._index, false))
                {
                  this._index = this.Text.Length;
                  continue;
                }
                if (this._state == HtmlDocument.ParseState.AttributeAfterEquals)
                {
                  this._state = HtmlDocument.ParseState.Text;
                  this.PushNodeStart(HtmlNodeType.Text, this._index);
                  continue;
                }
                continue;
              }
              this.PushAttributeValueStart(this._index - 1);
              this._state = HtmlDocument.ParseState.AttributeValue;
              continue;
            }
            continue;
          case HtmlDocument.ParseState.AttributeValue:
            if (!this.NewCheck())
            {
              if (HtmlDocument.IsWhiteSpace(this._c))
              {
                this.PushAttributeValueEnd(this._index - 1);
                this._state = HtmlDocument.ParseState.BetweenAttributes;
                continue;
              }
              if (this._c == 62)
              {
                this.PushAttributeValueEnd(this._index - 1);
                if (!this.PushNodeEnd(this._index, false))
                {
                  this._index = this.Text.Length;
                  continue;
                }
                if (this._state == HtmlDocument.ParseState.AttributeValue)
                {
                  this._state = HtmlDocument.ParseState.Text;
                  this.PushNodeStart(HtmlNodeType.Text, this._index);
                  continue;
                }
                continue;
              }
              continue;
            }
            continue;
          case HtmlDocument.ParseState.Comment:
            if (this._c == 62 && (!this._fullcomment || this.Text[this._index - 2] == '-' && this.Text[this._index - 3] == '-' || this.Text[this._index - 2] == '!' && this.Text[this._index - 3] == '-' && this.Text[this._index - 4] == '-'))
            {
              if (!this.PushNodeEnd(this._index, false))
              {
                this._index = this.Text.Length;
                continue;
              }
              this._state = HtmlDocument.ParseState.Text;
              this.PushNodeStart(HtmlNodeType.Text, this._index);
              continue;
            }
            continue;
          case HtmlDocument.ParseState.QuotedAttributeValue:
            if (this._c == num)
            {
              this.PushAttributeValueEnd(this._index - 1);
              this._state = HtmlDocument.ParseState.BetweenAttributes;
              continue;
            }
            if (this._c == 60 && this._index < this.Text.Length && this.Text[this._index] == '%')
            {
              this._oldstate = this._state;
              this._state = HtmlDocument.ParseState.ServerSideCode;
              continue;
            }
            continue;
          case HtmlDocument.ParseState.ServerSideCode:
            if (this._c == 37)
            {
              if (this._index < this.Text.Length && this.Text[this._index] == '>')
              {
                switch (this._oldstate)
                {
                  case HtmlDocument.ParseState.BetweenAttributes:
                    this.PushAttributeNameEnd(this._index + 1);
                    this._state = HtmlDocument.ParseState.BetweenAttributes;
                    break;
                  case HtmlDocument.ParseState.AttributeAfterEquals:
                    this._state = HtmlDocument.ParseState.AttributeValue;
                    break;
                  default:
                    this._state = this._oldstate;
                    break;
                }
                this.IncrementPosition();
                continue;
              }
              continue;
            }
            if (this._oldstate == HtmlDocument.ParseState.QuotedAttributeValue && this._c == num)
            {
              this._state = this._oldstate;
              this.DecrementPosition();
              continue;
            }
            continue;
          case HtmlDocument.ParseState.PcData:
            if (this._currentnode._namelength + 3 <= this.Text.Length - (this._index - 1) && string.Compare(this.Text.Substring(this._index - 1, this._currentnode._namelength + 2), "</" + this._currentnode.Name, StringComparison.OrdinalIgnoreCase) == 0)
            {
              int c = (int) this.Text[this._index - 1 + 2 + this._currentnode.Name.Length];
              if (c == 62 || HtmlDocument.IsWhiteSpace(c))
              {
                HtmlNode node = this.CreateNode(HtmlNodeType.Text, this._currentnode._outerstartindex + this._currentnode._outerlength);
                node._outerlength = this._index - 1 - node._outerstartindex;
                this._currentnode.AppendChild(node);
                this.PushNodeStart(HtmlNodeType.Element, this._index - 1);
                this.PushNodeNameStart(false, this._index - 1 + 2);
                this._state = HtmlDocument.ParseState.Tag;
                this.IncrementPosition();
                continue;
              }
              continue;
            }
            continue;
          default:
            continue;
        }
      }
      if (this._currentnode._namestartindex > 0)
        this.PushNodeNameEnd(this._index);
      this.PushNodeEnd(this._index, false);
      this.Lastnodes.Clear();
    }

    private void PushAttributeNameEnd(int index)
    {
      this._currentattribute._namelength = index - this._currentattribute._namestartindex;
      this._currentnode.Attributes.Append(this._currentattribute);
    }

    private void PushAttributeNameStart(int index)
    {
      this._currentattribute = this.CreateAttribute();
      this._currentattribute._namestartindex = index;
      this._currentattribute.Line = this._line;
      this._currentattribute._lineposition = this._lineposition;
      this._currentattribute._streamposition = index;
    }

    private void PushAttributeValueEnd(int index)
    {
      this._currentattribute._valuelength = index - this._currentattribute._valuestartindex;
    }

    private void PushAttributeValueStart(int index) => this.PushAttributeValueStart(index, 0);

    private void CloseParentImplicitExplicitNode()
    {
      bool flag = true;
      while (flag && !this._lastparentnode.Closed)
      {
        flag = false;
        if (this.IsParentImplicitEnd())
        {
          this.CloseParentImplicitEnd();
          flag = true;
        }
        if (this.IsParentExplicitEnd())
        {
          this.CloseParentExplicitEnd();
          flag = true;
        }
      }
    }

    private bool IsParentImplicitEnd()
    {
      if (!this._currentnode._starttag)
        return false;
      bool flag = false;
      string name = this._lastparentnode.Name;
      string str = this.Text.Substring(this._currentnode._namestartindex, this._index - this._currentnode._namestartindex - 1);
      switch (name)
      {
        case "a":
          flag = str == "a";
          break;
        case "dd":
          flag = str == "dt" || str == "dd";
          break;
        case "dt":
          flag = str == "dt" || str == "dd";
          break;
        case "p":
          flag = str == "p";
          break;
        case "option":
          flag = str == "option";
          break;
      }
      return flag;
    }

    private bool IsParentExplicitEnd()
    {
      if (!this._currentnode._starttag)
        return false;
      bool flag = false;
      string name = this._lastparentnode.Name;
      string str = this.Text.Substring(this._currentnode._namestartindex, this._index - this._currentnode._namestartindex - 1);
      switch (name)
      {
        case "h1":
          flag = str == "h2" || str == "h3" || str == "h4" || str == "h5";
          break;
        case "h2":
          flag = str == "h1" || str == "h3" || str == "h4" || str == "h5";
          break;
        case "h3":
          flag = str == "h1" || str == "h2" || str == "h4" || str == "h5";
          break;
        case "h4":
          flag = str == "h1" || str == "h2" || str == "h3" || str == "h5";
          break;
        case "h5":
          flag = str == "h1" || str == "h2" || str == "h3" || str == "h4";
          break;
        case "table":
          flag = str == "table";
          break;
        case "td":
          flag = str == "td" || str == "th" || str == "tr";
          break;
        case "th":
          flag = str == "td" || str == "th" || str == "tr";
          break;
        case "title":
          flag = str == "title";
          break;
        case "tr":
          flag = str == "tr";
          break;
      }
      return flag;
    }

    private void CloseParentImplicitEnd()
    {
      HtmlNode endnode = new HtmlNode(this._lastparentnode.NodeType, this, -1);
      endnode._endnode = endnode;
      endnode._isImplicitEnd = true;
      this._lastparentnode._isImplicitEnd = true;
      this._lastparentnode.CloseNode(endnode);
    }

    private void CloseParentExplicitEnd()
    {
      HtmlNode endnode = new HtmlNode(this._lastparentnode.NodeType, this, -1);
      endnode._endnode = endnode;
      this._lastparentnode.CloseNode(endnode);
    }

    private void PushAttributeValueStart(int index, int quote)
    {
      this._currentattribute._valuestartindex = index;
      if (quote != 39)
        return;
      this._currentattribute.QuoteType = AttributeValueQuote.SingleQuote;
    }

    private bool PushNodeEnd(int index, bool close)
    {
      this._currentnode._outerlength = index - this._currentnode._outerstartindex;
      if (this._currentnode._nodetype == HtmlNodeType.Text || this._currentnode._nodetype == HtmlNodeType.Comment)
      {
        if (this._currentnode._outerlength > 0)
        {
          this._currentnode._innerlength = this._currentnode._outerlength;
          this._currentnode._innerstartindex = this._currentnode._outerstartindex;
          if (this._lastparentnode != null)
            this._lastparentnode.AppendChild(this._currentnode);
        }
      }
      else if (this._currentnode._starttag && this._lastparentnode != this._currentnode)
      {
        if (this._lastparentnode != null)
          this._lastparentnode.AppendChild(this._currentnode);
        this.ReadDocumentEncoding(this._currentnode);
        this._currentnode._prevwithsamename = Utilities.GetDictionaryValueOrDefault<string, HtmlNode>(this.Lastnodes, this._currentnode.Name);
        this.Lastnodes[this._currentnode.Name] = this._currentnode;
        if (this._currentnode.NodeType == HtmlNodeType.Document || this._currentnode.NodeType == HtmlNodeType.Element)
          this._lastparentnode = this._currentnode;
        if (HtmlNode.IsCDataElement(this.CurrentNodeName()))
        {
          this._state = HtmlDocument.ParseState.PcData;
          return true;
        }
        if (HtmlNode.IsClosedElement(this._currentnode.Name) || HtmlNode.IsEmptyElement(this._currentnode.Name))
          close = true;
      }
      if (close || !this._currentnode._starttag)
      {
        if (this.OptionStopperNodeName != null && this._remainder == null && string.Compare(this._currentnode.Name, this.OptionStopperNodeName, StringComparison.OrdinalIgnoreCase) == 0)
        {
          this._remainderOffset = index;
          this._remainder = this.Text.Substring(this._remainderOffset);
          this.CloseCurrentNode();
          return false;
        }
        this.CloseCurrentNode();
      }
      return true;
    }

    private void PushNodeNameEnd(int index)
    {
      this._currentnode._namelength = index - this._currentnode._namestartindex;
      if (!this.OptionFixNestedTags)
        return;
      this.FixNestedTags();
    }

    private void PushNodeNameStart(bool starttag, int index)
    {
      this._currentnode._starttag = starttag;
      this._currentnode._namestartindex = index;
    }

    private void PushNodeStart(HtmlNodeType type, int index)
    {
      this._currentnode = this.CreateNode(type, index);
      this._currentnode._line = this._line;
      this._currentnode._lineposition = this._lineposition;
      if (type == HtmlNodeType.Element)
        --this._currentnode._lineposition;
      this._currentnode._streamposition = index;
    }

    private void ReadDocumentEncoding(HtmlNode node)
    {
      if (!this.OptionReadEncoding || node._namelength != 4 || node.Name != "meta")
        return;
      HtmlAttribute attribute1 = node.Attributes["http-equiv"];
      if (attribute1 == null || string.Compare(attribute1.Value, "content-type", StringComparison.OrdinalIgnoreCase) != 0)
        return;
      HtmlAttribute attribute2 = node.Attributes["content"];
      if (attribute2 == null)
        return;
      string str = NameValuePairList.GetNameValuePairsValue(attribute2.Value, "charset");
      if (string.IsNullOrEmpty(str))
        return;
      if (string.Equals(str, "utf8", StringComparison.OrdinalIgnoreCase))
        str = "utf-8";
      try
      {
        this._declaredencoding = Encoding.GetEncoding(str);
      }
      catch //(ArgumentException ex)
      {
        this._declaredencoding = (Encoding) null;
      }
      if (this._onlyDetectEncoding)
        throw new EncodingFoundException(this._declaredencoding);
      if (this._streamencoding == null || !(this._declaredencoding.WebName != this._streamencoding.WebName))
        return;
      this.AddError(HtmlParseErrorCode.CharsetMismatch, this._line, this._lineposition, this._index, node.OuterHtml, "Encoding mismatch between StreamEncoding: " + this._streamencoding.WebName + " and DeclaredEncoding: " + this._declaredencoding.WebName);
    }

    private enum ParseState
    {
      Text,
      WhichTag,
      Tag,
      BetweenAttributes,
      EmptyTag,
      AttributeName,
      AttributeBeforeEquals,
      AttributeAfterEquals,
      AttributeValue,
      Comment,
      QuotedAttributeValue,
      ServerSideCode,
      PcData,
    }
  }
}
