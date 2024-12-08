// Decompiled with JetBrains decompiler
// Type: App1uwp.UC.SwipeThroughControl
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.DataObjects;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;

#nullable disable
namespace App1uwp.UC
{
  public sealed class SwipeThroughControl : UserControl, INotifyPropertyChanged, IComponentConnector
  {
    private ObservableCollection<object> _items;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private FlipView flip;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private ScrollViewer filtersScrollViewer;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TranslateTransform tr;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private ItemsControl _items_control;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public event EventHandler<int> StickerTapped;

    public event PropertyChangedEventHandler PropertyChanged;

    public SwipeThroughControl()
    {
      this.InitializeComponent();
      ((FrameworkElement) this).put_DataContext((object) this);
      ItemsControl itemsControl = this._items_control;
      WindowsRuntimeMarshal.AddEventHandler<SizeChangedEventHandler>(new Func<SizeChangedEventHandler, EventRegistrationToken>(((FrameworkElement) itemsControl).add_SizeChanged), new Action<EventRegistrationToken>(((FrameworkElement) itemsControl).remove_SizeChanged), new SizeChangedEventHandler(this._items_control_SizeChanged));
    }

    public ObservableCollection<object> Items
    {
      get => this._items;
      set
      {
        this._items = value;
        this.OnPropertyChanged(nameof (Items));
      }
    }

    private void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }

    private void GridView_SizeChanged(object sender, SizeChangedEventArgs e)
    {
      ItemsWrapGrid itemsPanelRoot = (ItemsWrapGrid) ((ItemsControl) (sender as GridView)).ItemsPanelRoot;
      itemsPanelRoot.put_Orientation((Orientation) 1);
      double num1 = e.NewSize.Width / 100.0;
      itemsPanelRoot.put_MaximumRowsOrColumns((int) num1);
      ItemsWrapGrid itemsWrapGrid1 = itemsPanelRoot;
      ItemsWrapGrid itemsWrapGrid2 = itemsPanelRoot;
      Size newSize = e.NewSize;
      double num2;
      double num3 = num2 = newSize.Width / (double) (int) num1;
      itemsWrapGrid2.put_ItemWidth(num2);
      double num4 = num3;
      itemsWrapGrid1.put_ItemHeight(num4);
    }

    private void Image_Tapped(object sender, TappedRoutedEventArgs e)
    {
      FrameworkElement frameworkElement = sender as FrameworkElement;
      if (this.StickerTapped == null)
        return;
      this.StickerTapped((object) this, int.Parse(frameworkElement.Tag.ToString()));
    }

    private void ScrollViewer_ViewChanging(object sender, ScrollViewerViewChangingEventArgs e)
    {
      ScrollViewer scrollViewer = sender as ScrollViewer;
      double num = e.NextView.VerticalOffset - scrollViewer.VerticalOffset;
      if (num > 0.0)
      {
        TranslateTransform tr = this.tr;
        tr.put_Y(tr.Y + num);
        if (this.tr.Y <= 64.0)
          return;
        this.tr.put_Y(64.0);
      }
      else
      {
        TranslateTransform tr = this.tr;
        tr.put_Y(tr.Y + num);
        if (this.tr.Y >= 0.0)
          return;
        this.tr.put_Y(0.0);
      }
    }

    private void FlipView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      this.tr.put_Y(0.0);
      FlipView flipView = sender as FlipView;
      if (((Selector) flipView).SelectedIndex < 0 || ((ICollection<object>) this._items_control.Items).Count == 0)
        return;
      for (int index = 0; index < this._items.Count; ++index)
        (((IList<object>) this._items_control.Items)[index] as StockItem).IsSelected = index == ((Selector) flipView).SelectedIndex;
    }

    private void Border_Tapped(object sender, TappedRoutedEventArgs e)
    {
      ((Selector) this.flip).put_SelectedItem((object) ((sender as FrameworkElement).DataContext as StockItem));
    }

    private void _items_control_SizeChanged(object sender, SizeChangedEventArgs e)
    {
      ItemsControl itemsControl = sender as ItemsControl;
      if (((ICollection<object>) itemsControl.Items).Count <= 0)
        return;
      (((IList<object>) itemsControl.Items)[0] as StockItem).IsSelected = true;
      WindowsRuntimeMarshal.RemoveEventHandler<SizeChangedEventHandler>(new Action<EventRegistrationToken>(((FrameworkElement) this._items_control).remove_SizeChanged), new SizeChangedEventHandler(this._items_control_SizeChanged));
    }

    private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
    {
      (Window.Current.Content as Frame).Navigate(typeof (StickersStorePage));
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///UC/SwipeThroughControl.xaml"), (ComponentResourceLocation) 0);
      this.flip = (FlipView) ((FrameworkElement) this).FindName("flip");
      this.filtersScrollViewer = (ScrollViewer) ((FrameworkElement) this).FindName("filtersScrollViewer");
      this.tr = (TranslateTransform) ((FrameworkElement) this).FindName("tr");
      this._items_control = (ItemsControl) ((FrameworkElement) this).FindName("_items_control");
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          Selector selector = (Selector) target;
          WindowsRuntimeMarshal.AddEventHandler<SelectionChangedEventHandler>(new Func<SelectionChangedEventHandler, EventRegistrationToken>(selector.add_SelectionChanged), new Action<EventRegistrationToken>(selector.remove_SelectionChanged), new SelectionChangedEventHandler(this.FlipView_SelectionChanged));
          break;
        case 2:
          UIElement uiElement1 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement1.add_Tapped), new Action<EventRegistrationToken>(uiElement1.remove_Tapped), new TappedEventHandler(this.Grid_Tapped));
          break;
        case 3:
          UIElement uiElement2 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement2.add_Tapped), new Action<EventRegistrationToken>(uiElement2.remove_Tapped), new TappedEventHandler(this.Border_Tapped));
          break;
        case 4:
          ScrollViewer scrollViewer = (ScrollViewer) target;
          WindowsRuntimeMarshal.AddEventHandler<EventHandler<ScrollViewerViewChangingEventArgs>>(new Func<EventHandler<ScrollViewerViewChangingEventArgs>, EventRegistrationToken>(scrollViewer.add_ViewChanging), new Action<EventRegistrationToken>(scrollViewer.remove_ViewChanging), new EventHandler<ScrollViewerViewChangingEventArgs>(this.ScrollViewer_ViewChanging));
          break;
        case 5:
          FrameworkElement frameworkElement = (FrameworkElement) target;
          WindowsRuntimeMarshal.AddEventHandler<SizeChangedEventHandler>(new Func<SizeChangedEventHandler, EventRegistrationToken>(frameworkElement.add_SizeChanged), new Action<EventRegistrationToken>(frameworkElement.remove_SizeChanged), new SizeChangedEventHandler(this.GridView_SizeChanged));
          break;
        case 6:
          UIElement uiElement3 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement3.add_Tapped), new Action<EventRegistrationToken>(uiElement3.remove_Tapped), new TappedEventHandler(this.Image_Tapped));
          break;
      }
      this._contentLoaded = true;
    }
  }
}
