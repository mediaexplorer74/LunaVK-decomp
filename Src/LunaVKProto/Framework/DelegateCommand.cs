// Decompiled with JetBrains decompiler
// Type: App1uwp.Framework.DelegateCommand
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using System;
using System.Windows.Input;

#nullable disable
namespace App1uwp.Framework
{
  public class DelegateCommand : ICommand
  {
    private Func<object, bool> canExecute;
    private Action<object> executeAction;

    public event EventHandler CanExecuteChanged;

    public DelegateCommand(Action<object> executeAction)
      : this(executeAction, (Func<object, bool>) null)
    {
    }

    public DelegateCommand(Action<object> executeAction, Func<object, bool> canExecute)
    {
      this.executeAction = executeAction != null ? executeAction : throw new ArgumentNullException(nameof (executeAction));
      this.canExecute = canExecute;
    }

    public bool CanExecute(object parameter)
    {
      bool flag = true;
      if (this.canExecute != null)
        flag = this.canExecute(parameter);
      return flag;
    }

    public void Execute(object parameter) => this.executeAction(parameter);
  }
}
