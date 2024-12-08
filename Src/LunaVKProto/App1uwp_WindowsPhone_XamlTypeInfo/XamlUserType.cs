﻿// Decompiled with JetBrains decompiler
// Type: App1uwp.App1uwp_WindowsPhone_XamlTypeInfo.XamlUserType
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Markup;

#nullable disable
namespace App1uwp.App1uwp_WindowsPhone_XamlTypeInfo
{
  [DebuggerNonUserCode]
  [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")]
  internal class XamlUserType : XamlSystemBaseType
  {
    private XamlTypeInfoProvider _provider;
    private IXamlType _baseType;
    private bool _isArray;
    private bool _isMarkupExtension;
    private bool _isBindable;
    private bool _isReturnTypeStub;
    private bool _isLocalType;
    private string _contentPropertyName;
    private string _itemTypeName;
    private string _keyTypeName;
    private Dictionary<string, string> _memberNames;
    private Dictionary<string, object> _enumValues;

    public XamlUserType(
      XamlTypeInfoProvider provider,
      string fullName,
      Type fullType,
      IXamlType baseType)
      : base(fullName, fullType)
    {
      this._provider = provider;
      this._baseType = baseType;
    }

    public override IXamlType BaseType => this._baseType;

    public override bool IsArray => this._isArray;

    public override bool IsCollection => this.CollectionAdd != null;

    public override bool IsConstructible => this.Activator != null;

    public override bool IsDictionary => this.DictionaryAdd != null;

    public override bool IsMarkupExtension => this._isMarkupExtension;

    public override bool IsBindable => this._isBindable;

    public override bool IsReturnTypeStub => this._isReturnTypeStub;

    public override bool IsLocalType => this._isLocalType;

    public override IXamlMember ContentProperty
    {
      get => this._provider.GetMemberByLongName(this._contentPropertyName);
    }

    public override IXamlType ItemType => this._provider.GetXamlTypeByName(this._itemTypeName);

    public override IXamlType KeyType => this._provider.GetXamlTypeByName(this._keyTypeName);

    public override IXamlMember GetMember(string name)
    {
      if (this._memberNames == null)
        return (IXamlMember) null;
      string longMemberName;
      return this._memberNames.TryGetValue(name, out longMemberName) ? this._provider.GetMemberByLongName(longMemberName) : (IXamlMember) null;
    }

    public override object ActivateInstance() => this.Activator();

    public override void AddToMap(object instance, object key, object item)
    {
      this.DictionaryAdd(instance, key, item);
    }

    public override void AddToVector(object instance, object item)
    {
      this.CollectionAdd(instance, item);
    }

    public override void RunInitializer()
    {
      RuntimeHelpers.RunClassConstructor(this.UnderlyingType.TypeHandle);
    }

    public override object CreateFromString(string input)
    {
      if (this._enumValues == null)
        throw new ArgumentException(input, this.FullName);
      int fromString = 0;
      string str1 = input;
      char[] chArray = new char[1]{ ',' };
      foreach (string str2 in str1.Split(chArray))
      {
        int num = 0;
        try
        {
          object obj;
          if (this._enumValues.TryGetValue(str2.Trim(), out obj))
          {
            num = Convert.ToInt32(obj);
          }
          else
          {
            try
            {
              num = Convert.ToInt32(str2.Trim());
            }
            catch (FormatException ex)
            {
              foreach (string key in this._enumValues.Keys)
              {
                if (string.Compare(str2.Trim(), key, StringComparison.OrdinalIgnoreCase) == 0 && this._enumValues.TryGetValue(key.Trim(), out obj))
                {
                  num = Convert.ToInt32(obj);
                  break;
                }
              }
            }
          }
          fromString |= num;
        }
        catch (FormatException ex)
        {
          throw new ArgumentException(input, this.FullName);
        }
      }
      return (object) fromString;
    }

    public Activator Activator { get; set; }

    public AddToCollection CollectionAdd { get; set; }

    public AddToDictionary DictionaryAdd { get; set; }

    public void SetContentPropertyName(string contentPropertyName)
    {
      this._contentPropertyName = contentPropertyName;
    }

    public void SetIsArray() => this._isArray = true;

    public void SetIsMarkupExtension() => this._isMarkupExtension = true;

    public void SetIsBindable() => this._isBindable = true;

    public void SetIsReturnTypeStub() => this._isReturnTypeStub = true;

    public void SetIsLocalType() => this._isLocalType = true;

    public void SetItemTypeName(string itemTypeName) => this._itemTypeName = itemTypeName;

    public void SetKeyTypeName(string keyTypeName) => this._keyTypeName = keyTypeName;

    public void AddMemberName(string shortName)
    {
      if (this._memberNames == null)
        this._memberNames = new Dictionary<string, string>();
      this._memberNames.Add(shortName, this.FullName + "." + shortName);
    }

    public void AddEnumValue(string name, object value)
    {
      if (this._enumValues == null)
        this._enumValues = new Dictionary<string, object>();
      this._enumValues.Add(name, value);
    }
  }
}