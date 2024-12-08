// Decompiled with JetBrains decompiler
// Type: App1uwp.Framework.ExtendedListView2
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Library;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

#nullable disable
namespace App1uwp.Framework
{
  public class ExtendedListView2 : ItemsControl
  {
    private const double offsetTreshhold = 100.0;
    public static readonly DependencyProperty IsFlatProperty = DependencyProperty.Register(nameof (IsFlat), typeof (bool), typeof (ExtendedListView2), new PropertyMetadata((object) false));
    public static readonly DependencyProperty UseHeaderOffsetProperty = DependencyProperty.Register(nameof (UseHeaderOffset), typeof (bool), typeof (ExtendedListView2), new PropertyMetadata((object) true));
    public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(nameof (Header), typeof (object), typeof (ExtendedListView2), new PropertyMetadata((object) null));
    public static readonly DependencyProperty FooterProperty = DependencyProperty.Register(nameof (Footer), typeof (object), typeof (ExtendedListView2), new PropertyMetadata((object) null));
    public static readonly DependencyProperty ReversPullProperty = DependencyProperty.Register(nameof (ReversPull), typeof (bool), typeof (ExtendedListView2), new PropertyMetadata((object) false));
    public static readonly DependencyProperty UseFooterOffsetProperty = DependencyProperty.Register(nameof (UseFooterOffset), typeof (bool), typeof (ExtendedListView2), new PropertyMetadata((object) false));
    public static readonly DependencyProperty IsPullEnabledProperty = DependencyProperty.Register(nameof (IsPullEnabled), typeof (bool), typeof (ExtendedListView2), new PropertyMetadata((object) true, new PropertyChangedCallback(ExtendedListView2.IsPullEnabledChanged)));
    private Rectangle offsetForHeader;
    private Rectangle offsetForFooter;
    private Rectangle rect;
    private ScrollViewer inside_scrollViewer;
    private ListView listView;
    private Grid headerGrid;
    private Grid footerGrid;
    private double lastpullvalue;
    public Action InsideScrollViewerLoaded;
    private DispatcherTimer timer = new DispatcherTimer();
    private bool InLoading;
    private bool IsInertial;
    private bool IsLocked;

    public ItemsPresenter ContentItemsPresenter { get; private set; }

    public bool IsFlat
    {
      get => (bool) ((DependencyObject) this).GetValue(ExtendedListView2.IsFlatProperty);
      set => ((DependencyObject) this).SetValue(ExtendedListView2.IsFlatProperty, (object) value);
    }

    public bool ReversPull
    {
      get => (bool) ((DependencyObject) this).GetValue(ExtendedListView2.ReversPullProperty);
      set
      {
        ((DependencyObject) this).SetValue(ExtendedListView2.ReversPullProperty, (object) value);
      }
    }

    private static void IsPullEnabledChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      ((ExtendedListView2) d).ActivateTimer((bool) e.NewValue);
    }

    public bool IsPullEnabled
    {
      get => (bool) ((DependencyObject) this).GetValue(ExtendedListView2.IsPullEnabledProperty);
      set
      {
        ((DependencyObject) this).SetValue(ExtendedListView2.IsPullEnabledProperty, (object) value);
      }
    }

    public void ActivateTimer(bool status)
    {
      if (status)
      {
        if (this.timer.IsEnabled)
          return;
        this.timer.Start();
      }
      else
        this.timer.Stop();
    }

    public bool UseHeaderOffset
    {
      get => (bool) ((DependencyObject) this).GetValue(ExtendedListView2.UseHeaderOffsetProperty);
      set
      {
        if (this.offsetForHeader != null)
          ((FrameworkElement) this.offsetForHeader).put_Height(value ? (Window.Current.Content as CustomFrame).HeaderWithMenu.HeaderHeight : 0.0);
        ((DependencyObject) this).SetValue(ExtendedListView2.UseHeaderOffsetProperty, (object) value);
      }
    }

    public bool UseFooterOffset
    {
      get => (bool) ((DependencyObject) this).GetValue(ExtendedListView2.UseFooterOffsetProperty);
      set
      {
        ((DependencyObject) this).SetValue(ExtendedListView2.UseFooterOffsetProperty, (object) value);
      }
    }

    public object Header
    {
      get => ((DependencyObject) this).GetValue(ExtendedListView2.HeaderProperty);
      set => ((DependencyObject) this).SetValue(ExtendedListView2.HeaderProperty, value);
    }

    public object Footer
    {
      get => ((DependencyObject) this).GetValue(ExtendedListView2.FooterProperty);
      set => ((DependencyObject) this).SetValue(ExtendedListView2.FooterProperty, value);
    }

    public ScrollViewer GetInsideScrollViewer => this.inside_scrollViewer;

    public ListView GetListView => this.listView;

    public ExtendedListView2()
    {
      ((Control) this).put_DefaultStyleKey((object) typeof (ExtendedListView2));
    }

    protected virtual void OnApplyTemplate()
    {
      ((FrameworkElement) this).OnApplyTemplate();
      this.rect = ((Control) this).GetTemplateChild("rect") as Rectangle;
      this.listView = ((Control) this).GetTemplateChild("listView") as ListView;
      this.offsetForHeader = ((Control) this).GetTemplateChild("offsetForHeader") as Rectangle;
      this.offsetForFooter = ((Control) this).GetTemplateChild("offsetForFooter") as Rectangle;
      this.headerGrid = ((Control) this).GetTemplateChild("headerGrid") as Grid;
      this.footerGrid = ((Control) this).GetTemplateChild("footerGrid") as Grid;
      ListView listView1 = this.listView;
      WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(((FrameworkElement) listView1).add_Loaded), new Action<EventRegistrationToken>(((FrameworkElement) listView1).remove_Loaded), new RoutedEventHandler(this.listView_Loaded));
      if (this.IsFlat)
        ((ItemsControl) this.listView).put_ItemContainerStyle((Style) ((IDictionary<object, object>) Application.Current.Resources)[(object) "ListViewItemFlatStyle"]);
      if (this.Header != null)
        ((ICollection<UIElement>) ((Panel) this.headerGrid).Children).Add(this.Header as UIElement);
      if (this.Footer != null)
        ((ICollection<UIElement>) ((Panel) this.footerGrid).Children).Add(this.Footer as UIElement);
      ListView listView2 = this.listView;
      WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(((FrameworkElement) listView2).add_Unloaded), new Action<EventRegistrationToken>(((FrameworkElement) listView2).remove_Unloaded), new RoutedEventHandler(this.listView_Unloaded));
    }

    private void listView_Unloaded(object sender, RoutedEventArgs e)
    {
      this.timer.Stop();
      WindowsRuntimeMarshal.RemoveEventHandler<EventHandler<object>>(new Action<EventRegistrationToken>(this.timer.remove_Tick), new EventHandler<object>(this.Timer_Tick));
    }

    private void listView_Loaded(object sender, RoutedEventArgs e)
    {
      this.inside_scrollViewer = (ScrollViewer) ((Border) VisualTreeHelper.GetChild((DependencyObject) this.listView, 0)).Child;
      ScrollViewer insideScrollViewer1 = this.inside_scrollViewer;
      WindowsRuntimeMarshal.AddEventHandler<EventHandler<ScrollViewerViewChangedEventArgs>>(new Func<EventHandler<ScrollViewerViewChangedEventArgs>, EventRegistrationToken>(insideScrollViewer1.add_ViewChanged), new Action<EventRegistrationToken>(insideScrollViewer1.remove_ViewChanged), new EventHandler<ScrollViewerViewChangedEventArgs>(this.sv_ViewChanged));
      ScrollViewer insideScrollViewer2 = this.inside_scrollViewer;
      WindowsRuntimeMarshal.AddEventHandler<EventHandler<ScrollViewerViewChangingEventArgs>>(new Func<EventHandler<ScrollViewerViewChangingEventArgs>, EventRegistrationToken>(insideScrollViewer2.add_ViewChanging), new Action<EventRegistrationToken>(insideScrollViewer2.remove_ViewChanging), new EventHandler<ScrollViewerViewChangingEventArgs>(this.inside_scrollViewer_ViewChanging));
      ScrollViewer insideScrollViewer3 = this.inside_scrollViewer;
      WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(((FrameworkElement) insideScrollViewer3).add_Loaded), new Action<EventRegistrationToken>(((FrameworkElement) insideScrollViewer3).remove_Loaded), new RoutedEventHandler(this.inside_scrollViewer_Loaded));
      this.ContentItemsPresenter = (ItemsPresenter) ((ContentControl) this.inside_scrollViewer).Content;
      ((UIElement) this.ContentItemsPresenter).put_ManipulationMode((ManipulationModes) 65537);
      this.timer.put_Interval(TimeSpan.FromMilliseconds(1.0));
      DispatcherTimer timer = this.timer;
      WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(timer.add_Tick), new Action<EventRegistrationToken>(timer.remove_Tick), new EventHandler<object>(this.Timer_Tick));
      if (this.IsPullEnabled)
        this.timer.Start();
      (Window.Current.Content as CustomFrame).MenuStateChanged += new EventHandler<CustomFrame.MenuStates>(this.MenuStateChanged);
      this.MenuStateChanged((object) this, (Window.Current.Content as CustomFrame).MenuState);
      ((FrameworkElement) this.offsetForHeader).put_Height(this.UseHeaderOffset ? (Window.Current.Content as CustomFrame).HeaderWithMenu.HeaderHeight : 0.0);
      ((FrameworkElement) this.offsetForFooter).put_Height(this.UseFooterOffset ? (Window.Current.Content as CustomFrame).HeaderWithMenu.HeaderHeight : 0.0);
    }

    private void MenuStateChanged(object sender, CustomFrame.MenuStates e)
    {
      switch (e)
      {
        case CustomFrame.MenuStates.StateWide:
          ((FrameworkElement) this).put_Margin(new Thickness());
          break;
        case CustomFrame.MenuStates.StateNarrow:
          ((FrameworkElement) this).put_Margin(new Thickness(Constants.MENU_NARROW_WIDTH, 0.0, 0.0, 0.0));
          break;
        case CustomFrame.MenuStates.StateCollapsed:
          ((FrameworkElement) this).put_Margin(new Thickness());
          break;
      }
    }

    private void inside_scrollViewer_Loaded(object sender, RoutedEventArgs e)
    {
      if (this.InsideScrollViewerLoaded == null)
        return;
      this.InsideScrollViewerLoaded();
    }

    private void inside_scrollViewer_ViewChanging(
      object sender,
      ScrollViewerViewChangingEventArgs e)
    {
      if (e.NextView.VerticalOffset == 0.0)
      {
        this.IsInertial = e.IsInertial;
        this.IsLocked = false;
        if (this.timer.IsEnabled)
          return;
        this.timer.Start();
      }
      else
      {
        if (!this.timer.IsEnabled)
          return;
        this.timer.Stop();
        if (this.OnPullPercentageChanged == null)
          return;
        this.OnPullPercentageChanged(0.0);
      }
    }

    private async void Timer_Tick(object sender, object e)
    {
      if (this.InLoading || this.IsLocked)
        return;
      GeneralTransform ttv = ((UIElement) this.rect).TransformToVisual(Window.Current.Content);
      Point screenCoords = ttv.TransformPoint(new Point(0.0, 0.0));
      if (this.ReversPull)
      {
        screenCoords.Y -= ((FrameworkElement) this.inside_scrollViewer).ActualHeight;
        screenCoords.Y *= -1.0;
      }
      if (this.IsInertial)
      {
        if (screenCoords.Y >= 1.0)
          return;
        this.IsInertial = false;
      }
      else
      {
        double percent = screenCoords.Y / 100.0 * 100.0;
        if (percent < 0.0)
        {
          this.timer.Stop();
        }
        else
        {
          if (this.lastpullvalue == percent)
            return;
          if (percent >= 100.0)
          {
            this.IsLocked = true;
            (Window.Current.Content as CustomFrame).HeaderWithMenu.ShowProgress(true);
            this.PercentAction(0.0);
            this.InLoading = true;
            if (((FrameworkElement) this).DataContext is ISupportLoadMore)
              await (((FrameworkElement) this).DataContext as ISupportLoadMore).LoadData(true);
            (Window.Current.Content as CustomFrame).HeaderWithMenu.ShowProgress(false);
            this.InLoading = false;
          }
          else
          {
            this.PercentAction(percent);
            this.lastpullvalue = percent;
          }
        }
      }
    }

    private void PercentAction(double percent)
    {
      if (this.OnPullPercentageChanged == null)
        return;
      this.OnPullPercentageChanged(percent);
    }

    public Action<double> OnPullPercentageChanged { get; set; }

    private async void sv_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
    {
      if (!e.IsIntermediate || this.InLoading)
        return;
      ScrollViewer sv = sender as ScrollViewer;
      if (sv.ScrollableHeight - sv.VerticalOffset >= 700.0 || !(((FrameworkElement) this).DataContext is ISupportLoadMore) || !(((FrameworkElement) this).DataContext as ISupportLoadMore).HasMoreItems)
        return;
      this.InLoading = true;
      await (((FrameworkElement) this).DataContext as ISupportLoadMore).LoadData();
      this.InLoading = false;
    }
  }
}
