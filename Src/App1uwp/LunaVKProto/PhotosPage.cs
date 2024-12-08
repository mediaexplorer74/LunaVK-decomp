// Decompiled with JetBrains decompiler
// Type: App1uwp.PhotosPage
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
using App1uwp.Network.ViewModels;
using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Navigation;

#nullable disable
namespace App1uwp
{
  public sealed class PhotosPage : PageBase, IComponentConnector
  {
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private GridView _gridView;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public PhotosPage()
    {
      this.InitializeComponent();
      PhotosPage photosPage = this;
      WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(((FrameworkElement) photosPage).add_Loaded), new Action<EventRegistrationToken>(((FrameworkElement) photosPage).remove_Loaded), (RoutedEventHandler) ((s, e) => ((FrameworkElement) this._gridView).put_Margin(new Thickness(0.0, (Window.Current.Content as CustomFrame).HeaderWithMenu.HeaderHeight, 0.0, 0.0))));
    }

    public PhotosViewModel PhotosVM => ((FrameworkElement) this).DataContext as PhotosViewModel;

    private void GridView_SizeChanged(object sender, SizeChangedEventArgs e)
    {
      ItemsWrapGrid itemsPanelRoot = (ItemsWrapGrid) ((ItemsControl) (sender as GridView)).ItemsPanelRoot;
      ItemsWrapGrid itemsWrapGrid1 = itemsPanelRoot;
      ItemsWrapGrid itemsWrapGrid2 = itemsPanelRoot;
      Size newSize = e.NewSize;
      double num1;
      double num2 = num1 = newSize.Width / 4.0;
      itemsWrapGrid2.put_ItemWidth(num1);
      double num3 = num2;
      itemsWrapGrid1.put_ItemHeight(num3);
    }

    protected override void HandleOnNavigatedTo(NavigationEventArgs e)
    {
      ((FrameworkElement) this).put_DataContext((object) new PhotosViewModel((int) e.Parameter, (int) Settings.Instance.auth.UserId));
      this.PhotosVM.LoadData(true);
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///PhotosPage.xaml"), (ComponentResourceLocation) 0);
      this._gridView = (GridView) ((FrameworkElement) this).FindName("_gridView");
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void Connect(int connectionId, object target)
    {
      if (connectionId == 1)
      {
        FrameworkElement frameworkElement = (FrameworkElement) target;
        WindowsRuntimeMarshal.AddEventHandler<SizeChangedEventHandler>(new Func<SizeChangedEventHandler, EventRegistrationToken>(frameworkElement.add_SizeChanged), new Action<EventRegistrationToken>(frameworkElement.remove_SizeChanged), new SizeChangedEventHandler(this.GridView_SizeChanged));
      }
      this._contentLoaded = true;
    }
  }
}
