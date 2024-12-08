// Decompiled with JetBrains decompiler
// Type: App1uwp.Framework.Execute
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using System;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace App1uwp.Framework
{
  public class Execute
  {
    public static void ExecuteOnUIThread(Action action)
    {
      CoreWindow.GetForCurrentThread();
      if (Window.Current != null && ((DependencyObject) (Window.Current.Content as Frame)).Dispatcher.HasThreadAccess)
        action();
      else
        Execute.DO(action);
    }

    private static async void DO(Action action)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: method pointer
      await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync((CoreDispatcherPriority) 0, new DispatchedHandler((object) new Execute.\u003C\u003Ec__DisplayClass1()
      {
        action = action
      }, __methodptr(\u003CDO\u003Eb__0)));
    }
  }
}
