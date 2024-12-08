// Decompiled with JetBrains decompiler
// Type: App1uwp.App1uwp_WindowsPhone_XamlTypeInfo.XamlMember
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Windows.UI.Xaml.Markup;

#nullable disable
namespace App1uwp.App1uwp_WindowsPhone_XamlTypeInfo
{
  [DebuggerNonUserCode]
  [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")]
  internal class XamlMember : IXamlMember
  {
    private XamlTypeInfoProvider _provider;
    private string _name;
    private bool _isAttachable;
    private bool _isDependencyProperty;
    private bool _isReadOnly;
    private string _typeName;
    private string _targetTypeName;

    public XamlMember(XamlTypeInfoProvider provider, string name, string typeName)
    {
      this._name = name;
      this._typeName = typeName;
      this._provider = provider;
    }

    public string Name => this._name;

    public IXamlType Type => this._provider.GetXamlTypeByName(this._typeName);

    public void SetTargetTypeName(string targetTypeName) => this._targetTypeName = targetTypeName;

    public IXamlType TargetType => this._provider.GetXamlTypeByName(this._targetTypeName);

    public void SetIsAttachable() => this._isAttachable = true;

    public bool IsAttachable => this._isAttachable;

    public void SetIsDependencyProperty() => this._isDependencyProperty = true;

    public bool IsDependencyProperty => this._isDependencyProperty;

    public void SetIsReadOnly() => this._isReadOnly = true;

    public bool IsReadOnly => this._isReadOnly;

    public Getter Getter { get; set; }

    public object GetValue(object instance)
    {
      if (this.Getter != null)
        return this.Getter(instance);
      throw new InvalidOperationException(nameof (GetValue));
    }

    public Setter Setter { get; set; }

    public void SetValue(object instance, object value)
    {
      if (this.Setter == null)
        throw new InvalidOperationException(nameof (SetValue));
      this.Setter(instance, value);
    }
  }
}
