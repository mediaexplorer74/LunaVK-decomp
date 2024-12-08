// Decompiled with JetBrains decompiler
// Type: App1uwp.SettingsGeneralPage
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
using App1uwp.Network;
using App1uwp.UC;
using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

#nullable disable
namespace App1uwp
{
  public sealed class SettingsGeneralPage : PageBase, IComponentConnector
  {
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Rectangle Offset;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private ToggleSwitch _switchProxy;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private StackPanel _panelStatus;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBlock _textStatus;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public SettingsGeneralPage()
    {
      this.InitializeComponent();
      SettingsGeneralPage settingsGeneralPage = this;
      WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(((FrameworkElement) settingsGeneralPage).add_Loaded), new Action<EventRegistrationToken>(((FrameworkElement) settingsGeneralPage).remove_Loaded), (RoutedEventHandler) ((s, e) =>
      {
        (Window.Current.Content as CustomFrame).HeaderWithMenu.SetTitle(LocalizedStrings.GetString("SettingsGeneral"));
        double num = 0.0;
        if (((UIElement) (Window.Current.Content as CustomFrame).HeaderWithMenu).Visibility == null)
          num = (Window.Current.Content as CustomFrame).HeaderWithMenu.HeaderHeight;
        ((FrameworkElement) this.Offset).put_Height(num);
      }));
      ((FrameworkElement) this).put_DataContext((object) Settings.Instance);
      this._switchProxy.IsChecked = Settings.Instance.UseProxy;
    }

    protected override void HandleOnNavigatingFrom(NavigatingCancelEventArgs e)
    {
      Settings.Instance.Save();
    }

    private async void Checked_Changed(object sender, RoutedEventArgs args)
    {
      ToggleSwitch t = sender as ToggleSwitch;
      if (t.IsChecked)
      {
        ((UIElement) this._panelStatus).put_Visibility((Visibility) 0);
        this._textStatus.put_Text("Проверка Proxy-сервера (1)");
        bool proxy = await RequestsDispatcher.Ping("userapi.com");
        if (!proxy)
          return;
        this._textStatus.put_Text("(1) успешно");
        Settings.Instance.ProxyAdress = "userapi.com";
        Settings.Instance.UseProxy = true;
      }
      else
      {
        ((UIElement) this._panelStatus).put_Visibility((Visibility) 1);
        Settings.Instance.UseProxy = false;
      }
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///SettingsGeneralPage.xaml"), (ComponentResourceLocation) 0);
      this.Offset = (Rectangle) ((FrameworkElement) this).FindName("Offset");
      this._switchProxy = (ToggleSwitch) ((FrameworkElement) this).FindName("_switchProxy");
      this._panelStatus = (StackPanel) ((FrameworkElement) this).FindName("_panelStatus");
      this._textStatus = (TextBlock) ((FrameworkElement) this).FindName("_textStatus");
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void Connect(int connectionId, object target)
    {
      if (connectionId == 1)
        ((ToggleSwitch) target).Checked += new EventHandler<RoutedEventArgs>(this.Checked_Changed);
      this._contentLoaded = true;
    }
  }
}
