// Decompiled with JetBrains decompiler
// Type: App1uwp.SettingsNotificationsPage
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
using App1uwp.Library;
using App1uwp.UC;
using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

#nullable disable
namespace App1uwp
{
  public sealed class SettingsNotificationsPage : PageBase, IComponentConnector
  {
    private PopUP _pop;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Rectangle Offset;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public SettingsNotificationsPage()
    {
      this.InitializeComponent();
      SettingsNotificationsPage notificationsPage = this;
      WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(((FrameworkElement) notificationsPage).add_Loaded), new Action<EventRegistrationToken>(((FrameworkElement) notificationsPage).remove_Loaded), (RoutedEventHandler) ((s, e) =>
      {
        (Window.Current.Content as CustomFrame).HeaderWithMenu.SetTitle(LocalizedStrings.GetString("SettingsNotifications"));
        ((FrameworkElement) this.Offset).put_Height((Window.Current.Content as CustomFrame).HeaderWithMenu.HeaderHeight);
      }));
      ((FrameworkElement) this).put_DataContext((object) new SettingsViewModel());
    }

    private SettingsViewModel VM => ((FrameworkElement) this).DataContext as SettingsViewModel;

    protected override void HandleOnNavigatingFrom(NavigatingCancelEventArgs e)
    {
      Settings.Instance.Save();
    }

    private void CancelDNDClick(object sender, RoutedEventArgs e) => this.VM.Disable(0);

    private void Button_Tapped(object sender, TappedRoutedEventArgs e)
    {
      Point position = e.GetPosition((UIElement) null);
      if (this._pop == null)
      {
        this._pop = new PopUP();
        this._pop.ItemTapped += new EventHandler<int>(this._picker_ItemTapped);
        this._pop.AddItem(0, "1 час");
        this._pop.AddItem(1, "2 часа");
        this._pop.AddItem(2, "3 чаас");
        this._pop.AddItem(3, "5 часов");
        this._pop.AddItem(4, "8 часов");
      }
      this._pop.Show(position);
    }

    private void _picker_ItemTapped(object argument, int i)
    {
      ushort num = 0;
      switch (i)
      {
        case 0:
          num = (ushort) 1;
          break;
        case 1:
          num = (ushort) 2;
          break;
        case 2:
          num = (ushort) 3;
          break;
        case 3:
          num = (ushort) 5;
          break;
        case 4:
          num = (ushort) 8;
          break;
      }
      this.VM.Disable((int) num * 3600);
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///SettingsNotificationsPage.xaml"), (ComponentResourceLocation) 0);
      this.Offset = (Rectangle) ((FrameworkElement) this).FindName("Offset");
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          ButtonBase buttonBase = (ButtonBase) target;
          WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.CancelDNDClick));
          break;
        case 2:
          UIElement uiElement = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement.add_Tapped), new Action<EventRegistrationToken>(uiElement.remove_Tapped), new TappedEventHandler(this.Button_Tapped));
          break;
      }
      this._contentLoaded = true;
    }
  }
}
