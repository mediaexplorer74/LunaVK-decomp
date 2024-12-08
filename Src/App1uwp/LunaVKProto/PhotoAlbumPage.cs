// Decompiled with JetBrains decompiler
// Type: App1uwp.PhotoAlbumPage
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
using App1uwp.Network.DataObjects;
using App1uwp.Network.ViewModels;
using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Navigation;

#nullable disable
namespace App1uwp
{
  public sealed class PhotoAlbumPage : PageBase, IComponentConnector
  {
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private ExtendedListView2 MainScroll;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public PhotoAlbumPage()
    {
      this.InitializeComponent();
      PhotoAlbumPage photoAlbumPage = this;
      WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(((FrameworkElement) photoAlbumPage).add_Loaded), new Action<EventRegistrationToken>(((FrameworkElement) photoAlbumPage).remove_Loaded), (RoutedEventHandler) ((s, e) => (Window.Current.Content as CustomFrame).HeaderWithMenu.SetTitle(LocalizedStrings.GetString("Menu_Photos/Title"))));
    }

    public PhotoAlbumViewModel AlbumsVM
    {
      get => ((FrameworkElement) this).DataContext as PhotoAlbumViewModel;
    }

    protected override void HandleOnNavigatedTo(NavigationEventArgs e)
    {
      ((FrameworkElement) this).put_DataContext((object) new PhotoAlbumViewModel());
      this.AlbumsVM.LoadData(true);
    }

    private void PhotoAlbumUC_Tapped(object sender, TappedRoutedEventArgs e)
    {
      (Window.Current.Content as Frame).Navigate(typeof (PhotosPage), (object) ((sender as FrameworkElement).DataContext as VKAlbumPhoto).id);
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///PhotoAlbumPage.xaml"), (ComponentResourceLocation) 0);
      this.MainScroll = (ExtendedListView2) ((FrameworkElement) this).FindName("MainScroll");
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void Connect(int connectionId, object target)
    {
      if (connectionId == 1)
      {
        UIElement uiElement = (UIElement) target;
        WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement.add_Tapped), new Action<EventRegistrationToken>(uiElement.remove_Tapped), new TappedEventHandler(this.PhotoAlbumUC_Tapped));
      }
      this._contentLoaded = true;
    }
  }
}
