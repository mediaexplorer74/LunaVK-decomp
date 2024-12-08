// Decompiled with JetBrains decompiler
// Type: App1uwp.VideoCatalogPage
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
using App1uwp.Network.ViewModels;
using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Navigation;

#nullable disable
namespace App1uwp
{
  public sealed class VideoCatalogPage : PageBase, IComponentConnector
  {
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Grid _subheader;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private ExtendedListView2 MainScroll;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Border _offset;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public VideoCatalogPage()
    {
      this.InitializeComponent();
      VideoCatalogPage videoCatalogPage = this;
      WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(((FrameworkElement) videoCatalogPage).add_Loaded), new Action<EventRegistrationToken>(((FrameworkElement) videoCatalogPage).remove_Loaded), (RoutedEventHandler) ((s, e) => (Window.Current.Content as CustomFrame).HeaderWithMenu.SetTitle(LocalizedStrings.GetString("Menu_Videos/Title"))));
    }

    protected override void HandleOnNavigatedTo(NavigationEventArgs e)
    {
      ((FrameworkElement) this).put_DataContext((object) new VideoCatalogViewModel());
      (((FrameworkElement) this).DataContext as VideoCatalogViewModel).LoadData(true);
    }

    private void MainScroll_Loaded(object sender, RoutedEventArgs e)
    {
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///VideoCatalogPage.xaml"), (ComponentResourceLocation) 0);
      this._subheader = (Grid) ((FrameworkElement) this).FindName("_subheader");
      this.MainScroll = (ExtendedListView2) ((FrameworkElement) this).FindName("MainScroll");
      this._offset = (Border) ((FrameworkElement) this).FindName("_offset");
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void Connect(int connectionId, object target)
    {
      if (connectionId == 1)
      {
        FrameworkElement frameworkElement = (FrameworkElement) target;
        WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(frameworkElement.add_Loaded), new Action<EventRegistrationToken>(frameworkElement.remove_Loaded), new RoutedEventHandler(this.MainScroll_Loaded));
      }
      this._contentLoaded = true;
    }
  }
}
