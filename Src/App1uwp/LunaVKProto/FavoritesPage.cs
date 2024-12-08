// Decompiled with JetBrains decompiler
// Type: App1uwp.FavoritesPage
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
using App1uwp.Network.DataObjects;
using App1uwp.Network.ViewModels;
using App1uwp.UC;
using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Shapes;

#nullable disable
namespace App1uwp
{
  public sealed class FavoritesPage : PageBase, IComponentConnector
  {
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Rectangle Offset;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private FlipView _flip;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private ExtendedGridView _gridView;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Border _brdPhoto;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Border _brdVideo;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Border _brdPosts;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Border _brdUsers;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Border _brdLinks;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Border _brdProducts;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public FavoritesPage()
    {
      ((FrameworkElement) this).put_DataContext((object) new FavoritesViewModel());
      this.InitializeComponent();
      FavoritesPage favoritesPage = this;
      WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(((FrameworkElement) favoritesPage).add_Loaded), new Action<EventRegistrationToken>(((FrameworkElement) favoritesPage).remove_Loaded), (RoutedEventHandler) ((s, e) =>
      {
        (Window.Current.Content as CustomFrame).HeaderWithMenu.SetTitle(LocalizedStrings.GetString("Menu_Bookmarks/Title"));
        ((FrameworkElement) this.Offset).put_Height((Window.Current.Content as CustomFrame).HeaderWithMenu.HeaderHeight);
      }));
    }

    private FavoritesViewModel VM => ((FrameworkElement) this).DataContext as FavoritesViewModel;

    private void FlipView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      this.VM.SubPage = ((Selector) (sender as FlipView)).SelectedIndex;
      this.VM.LoadData(false);
    }

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

    private Image GetImageFunc(int index)
    {
      return (((ContentControl) (((ItemsControl) this._gridView).ContainerFromIndex(index) as GridViewItem)).ContentTemplateRoot as Border).Child as Image;
    }

    private void Border_Tapped(object sender, TappedRoutedEventArgs e)
    {
      int ind = this.VM.Photos.IndexOf((sender as FrameworkElement).DataContext as VKPhoto);
      ImageViewerDecoratorUC viewerDecoratorUc = new ImageViewerDecoratorUC();
      viewerDecoratorUc.Initialize(this.VM.Photos.ToList<VKPhoto>(), (Func<int, Image>) (i => this.GetImageFunc(i)));
      viewerDecoratorUc.Show(ind);
    }

    private void _brdPhoto_Tapped(object sender, TappedRoutedEventArgs e)
    {
      ((Selector) this._flip).put_SelectedIndex(0);
    }

    private void _brdVideo_Tapped(object sender, TappedRoutedEventArgs e)
    {
      ((Selector) this._flip).put_SelectedIndex(1);
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///FavoritesPage.xaml"), (ComponentResourceLocation) 0);
      this.Offset = (Rectangle) ((FrameworkElement) this).FindName("Offset");
      this._flip = (FlipView) ((FrameworkElement) this).FindName("_flip");
      this._gridView = (ExtendedGridView) ((FrameworkElement) this).FindName("_gridView");
      this._brdPhoto = (Border) ((FrameworkElement) this).FindName("_brdPhoto");
      this._brdVideo = (Border) ((FrameworkElement) this).FindName("_brdVideo");
      this._brdPosts = (Border) ((FrameworkElement) this).FindName("_brdPosts");
      this._brdUsers = (Border) ((FrameworkElement) this).FindName("_brdUsers");
      this._brdLinks = (Border) ((FrameworkElement) this).FindName("_brdLinks");
      this._brdProducts = (Border) ((FrameworkElement) this).FindName("_brdProducts");
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          Selector selector = (Selector) target;
          WindowsRuntimeMarshal.AddEventHandler<SelectionChangedEventHandler>(new Func<SelectionChangedEventHandler, EventRegistrationToken>(selector.add_SelectionChanged), new Action<EventRegistrationToken>(selector.remove_SelectionChanged), new SelectionChangedEventHandler(this.FlipView_SelectionChanged));
          break;
        case 2:
          FrameworkElement frameworkElement = (FrameworkElement) target;
          WindowsRuntimeMarshal.AddEventHandler<SizeChangedEventHandler>(new Func<SizeChangedEventHandler, EventRegistrationToken>(frameworkElement.add_SizeChanged), new Action<EventRegistrationToken>(frameworkElement.remove_SizeChanged), new SizeChangedEventHandler(this.GridView_SizeChanged));
          break;
        case 3:
          UIElement uiElement1 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement1.add_Tapped), new Action<EventRegistrationToken>(uiElement1.remove_Tapped), new TappedEventHandler(this.Border_Tapped));
          break;
        case 4:
          UIElement uiElement2 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement2.add_Tapped), new Action<EventRegistrationToken>(uiElement2.remove_Tapped), new TappedEventHandler(this._brdPhoto_Tapped));
          break;
        case 5:
          UIElement uiElement3 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement3.add_Tapped), new Action<EventRegistrationToken>(uiElement3.remove_Tapped), new TappedEventHandler(this._brdVideo_Tapped));
          break;
      }
      this._contentLoaded = true;
    }
  }
}
