// Decompiled with JetBrains decompiler
// Type: HtmlAgilityPack.HtmlAttributeCollection
// Assembly: HtmlAgilityPack, Version=1.7.1.0, Culture=neutral, PublicKeyToken=bd319b19eaf3b43a
// MVID: B5498F67-4C4E-4FFB-BDE4-EB084EEE38F2
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\HtmlAgilityPack.dll

using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace HtmlAgilityPack
{
  public class HtmlAttributeCollection : 
    IList<HtmlAttribute>,
    ICollection<HtmlAttribute>,
    IEnumerable<HtmlAttribute>,
    IEnumerable
  {
    internal Dictionary<string, HtmlAttribute> Hashitems = new Dictionary<string, HtmlAttribute>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    private HtmlNode _ownernode;
    private List<HtmlAttribute> items = new List<HtmlAttribute>();

    internal HtmlAttributeCollection(HtmlNode ownernode) => this._ownernode = ownernode;

    public int Count => this.items.Count;

    public bool IsReadOnly => false;

    public HtmlAttribute this[int index]
    {
      get => this.items[index];
      set
      {
        HtmlAttribute htmlAttribute = this.items[index];
        this.items[index] = value;
        if (htmlAttribute.Name != value.Name)
          this.Hashitems.Remove(htmlAttribute.Name);
        this.Hashitems[value.Name] = value;
        value._ownernode = this._ownernode;
        this._ownernode.SetChanged();
      }
    }

    public HtmlAttribute this[string name]
    {
      get
      {
        if (name == null)
          throw new ArgumentNullException(nameof (name));
        HtmlAttribute htmlAttribute;
        return !this.Hashitems.TryGetValue(name, out htmlAttribute) ? (HtmlAttribute) null : htmlAttribute;
      }
      set
      {
        HtmlAttribute htmlAttribute;
        if (!this.Hashitems.TryGetValue(name, out htmlAttribute))
          this.Append(value);
        this[this.items.IndexOf(htmlAttribute)] = value;
      }
    }

    public void Add(HtmlAttribute item) => this.Append(item);

    void ICollection<HtmlAttribute>.Clear() => this.items.Clear();

    public bool Contains(HtmlAttribute item) => this.items.Contains(item);

    public void CopyTo(HtmlAttribute[] array, int arrayIndex)
    {
      this.items.CopyTo(array, arrayIndex);
    }

    IEnumerator<HtmlAttribute> IEnumerable<HtmlAttribute>.GetEnumerator()
    {
      return (IEnumerator<HtmlAttribute>) this.items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.items.GetEnumerator();

    public int IndexOf(HtmlAttribute item) => this.items.IndexOf(item);

    public void Insert(int index, HtmlAttribute item)
    {
      this.Hashitems[item.Name] = item != null ? item : throw new ArgumentNullException(nameof (item));
      item._ownernode = this._ownernode;
      this.items.Insert(index, item);
      this._ownernode.SetChanged();
    }

    bool ICollection<HtmlAttribute>.Remove(HtmlAttribute item) => this.items.Remove(item);

    public void RemoveAt(int index)
    {
      this.Hashitems.Remove(this.items[index].Name);
      this.items.RemoveAt(index);
      this._ownernode.SetChanged();
    }

    public void Add(string name, string value) => this.Append(name, value);

    public HtmlAttribute Append(HtmlAttribute newAttribute)
    {
      this.Hashitems[newAttribute.Name] = newAttribute != null ? newAttribute : throw new ArgumentNullException(nameof (newAttribute));
      newAttribute._ownernode = this._ownernode;
      this.items.Add(newAttribute);
      this._ownernode.SetChanged();
      return newAttribute;
    }

    public HtmlAttribute Append(string name)
    {
      return this.Append(this._ownernode._ownerdocument.CreateAttribute(name));
    }

    public HtmlAttribute Append(string name, string value)
    {
      return this.Append(this._ownernode._ownerdocument.CreateAttribute(name, value));
    }

    public bool Contains(string name)
    {
      for (int index = 0; index < this.items.Count; ++index)
      {
        if (string.Equals(this.items[index].Name, name, StringComparison.OrdinalIgnoreCase))
          return true;
      }
      return false;
    }

    public HtmlAttribute Prepend(HtmlAttribute newAttribute)
    {
      this.Insert(0, newAttribute);
      return newAttribute;
    }

    public void Remove(HtmlAttribute attribute)
    {
      int index = attribute != null ? this.GetAttributeIndex(attribute) : throw new ArgumentNullException(nameof (attribute));
      if (index == -1)
        throw new IndexOutOfRangeException();
      this.RemoveAt(index);
    }

    public void Remove(string name)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      for (int index = 0; index < this.items.Count; ++index)
      {
        if (string.Equals(this.items[index].Name, name, StringComparison.OrdinalIgnoreCase))
          this.RemoveAt(index);
      }
    }

    public void RemoveAll()
    {
      this.Hashitems.Clear();
      this.items.Clear();
      this._ownernode.SetChanged();
    }

    public IEnumerable<HtmlAttribute> AttributesWithName(string attributeName)
    {
      for (int i = 0; i < this.items.Count; ++i)
      {
        if (string.Equals(this.items[i].Name, attributeName, StringComparison.OrdinalIgnoreCase))
          yield return this.items[i];
      }
    }

    public void Remove() => this.items.Clear();

    internal void Clear()
    {
      this.Hashitems.Clear();
      this.items.Clear();
    }

    internal int GetAttributeIndex(HtmlAttribute attribute)
    {
      if (attribute == null)
        throw new ArgumentNullException(nameof (attribute));
      for (int index = 0; index < this.items.Count; ++index)
      {
        if (this.items[index] == attribute)
          return index;
      }
      return -1;
    }

    internal int GetAttributeIndex(string name)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      for (int index = 0; index < this.items.Count; ++index)
      {
        if (string.Equals(this.items[index].Name, name, StringComparison.OrdinalIgnoreCase))
          return index;
      }
      return -1;
    }
  }
}
