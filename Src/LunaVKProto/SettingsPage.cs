// Decompiled with JetBrains decompiler
// Type: App1uwp.SettingsPage
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
using App1uwp.Network.DataObjects;
using App1uwp.Network.ViewModels;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Shapes;

#nullable disable
namespace App1uwp
{
  public sealed class SettingsPage : PageBase, IComponentConnector
  {
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Rectangle Offset;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public SettingsPage()
    {
      this.InitializeComponent();
      SettingsPage settingsPage = this;
      WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(((FrameworkElement) settingsPage).add_Loaded), new Action<EventRegistrationToken>(((FrameworkElement) settingsPage).remove_Loaded), (RoutedEventHandler) ((s, e) =>
      {
        (Window.Current.Content as CustomFrame).HeaderWithMenu.SetTitle(LocalizedStrings.GetString("Settings"));
        ((FrameworkElement) this.Offset).put_Height((Window.Current.Content as CustomFrame).HeaderWithMenu.HeaderHeight);
      }));
    }

    private void SettingsSectionUC_Tapped(object sender, TappedRoutedEventArgs e)
    {
      (Window.Current.Content as Frame).Navigate(typeof (SettingsNotificationsPage));
    }

    private void SettingsSectionUC_Tapped_1(object sender, TappedRoutedEventArgs e)
    {
      (Window.Current.Content as Frame).Navigate(typeof (SettingsGeneralPage));
    }

    private void SettingsSectionUC_Tapped_2(object sender, TappedRoutedEventArgs e)
    {
      (Window.Current.Content as Frame).Navigate(typeof (SettingsPersonalizationPage));
    }

    private async void Border_Tapped(object sender, TappedRoutedEventArgs e)
    {
      MessageDialog dialog = new MessageDialog("Продолжить?");
      dialog.put_Title(LocalizedStrings.GetString("LogOut/Text"));
      IList<IUICommand> commands1 = dialog.Commands;
      UICommand uiCommand1 = new UICommand();
      uiCommand1.put_Label("Да");
      uiCommand1.put_Id((object) 0);
      UICommand uiCommand3 = uiCommand1;
      commands1.Add((IUICommand) uiCommand3);
      IList<IUICommand> commands2 = dialog.Commands;
      UICommand uiCommand2 = new UICommand();
      uiCommand2.put_Label("Нет");
      uiCommand2.put_Id((object) 1);
      UICommand uiCommand4 = uiCommand2;
      commands2.Add((IUICommand) uiCommand4);
      IUICommand res = await dialog.ShowAsync();
      if ((int) res.Id != 0)
        return;
      Settings.Instance.Remove();
      Settings.Instance.auth = (AutorizationData) null;
      DialogsViewModel.Instance.Dialogs.Clear();
      NewsViewModel.Instance.Items.Clear();
      (Window.Current.Content as Frame).Navigate(typeof (LoginPage));
    }

    private void SettingsSectionUC_Tapped_3(object sender, TappedRoutedEventArgs e)
    {
      (Window.Current.Content as Frame).Navigate(typeof (AboutPage));
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///SettingsPage.xaml"), (ComponentResourceLocation) 0);
      this.Offset = (Rectangle) ((FrameworkElement) this).FindName("Offset");
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          UIElement uiElement1 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement1.add_Tapped), new Action<EventRegistrationToken>(uiElement1.remove_Tapped), new TappedEventHandler(this.Border_Tapped));
          break;
        case 2:
          UIElement uiElement2 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement2.add_Tapped), new Action<EventRegistrationToken>(uiElement2.remove_Tapped), new TappedEventHandler(this.SettingsSectionUC_Tapped_3));
          break;
        case 3:
          UIElement uiElement3 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement3.add_Tapped), new Action<EventRegistrationToken>(uiElement3.remove_Tapped), new TappedEventHandler(this.SettingsSectionUC_Tapped));
          break;
        case 4:
          UIElement uiElement4 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement4.add_Tapped), new Action<EventRegistrationToken>(uiElement4.remove_Tapped), new TappedEventHandler(this.SettingsSectionUC_Tapped_1));
          break;
        case 5:
          UIElement uiElement5 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement5.add_Tapped), new Action<EventRegistrationToken>(uiElement5.remove_Tapped), new TappedEventHandler(this.SettingsSectionUC_Tapped_2));
          break;
      }
      this._contentLoaded = true;
    }
  }
}
