// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.ViewModels.ViewModelBase
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

#nullable disable
namespace App1uwp.Network.ViewModels
{
  public class ViewModelBase : INotifyPropertyChanged
  {
    protected void NotifyPropertyChanged<T>(Expression<Func<T>> propertyExpression)
    {
      if (propertyExpression.Body.NodeType != ExpressionType.MemberAccess)
        return;
      this.RaisePropertyChanged((propertyExpression.Body as MemberExpression).Member.Name);
    }

    protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
    {
      this.RaisePropertyChanged(propertyName);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void RaisePropertyChanged(string property)
    {
      if (this.PropertyChanged == null)
        return;
      Execute.ExecuteOnUIThread((Action) (() =>
      {
        if (this.PropertyChanged == null)
          return;
        this.PropertyChanged((object) this, new PropertyChangedEventArgs(property));
      }));
    }
  }
}
