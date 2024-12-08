// Decompiled with JetBrains decompiler
// Type: App1uwp.UC.HeaderWithMenuUC
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
using App1uwp.Library;
using App1uwp.Network.ViewModels;
using App1uwp.Utils;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Shapes;

#nullable disable
namespace App1uwp.UC
{
  public sealed class HeaderWithMenuUC : UserControl, IComponentConnector
  {
    public bool _isMenuOpen;
    private EasingFunctionBase _menuEasing;
    private bool _hideSandwitchButton;
    private DispatcherTimer searchTimer;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Grid OverlayFromMenu;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Grid root;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Grid _menuGrid;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Grid serachHints;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private PullToRefreshUC ucPullToRefresh;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TranslateTransform _menuTransform;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Rectangle _shadow;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Rectangle _menuCallout;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private StackPanel _stack;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Border userPhoto;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Rectangle Offset;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Grid back;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private ProgressBar progressBar;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TranslateTransform _headerContentTransform;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Grid _headerGrid;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Grid _searchGrid;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Grid _onPageSearch;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBox _textBoxOnPageSearch;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TranslateTransform _searchTransform;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBox SearchTextBox;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Grid borderSandwich;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Grid ContentGrid;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Grid SubContentGrid;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private StackPanel itemsControlFilterMenu;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private ItemsControl itemsControlOptionsMenu;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Grid counterPanel;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Border brdCount;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBlock counterBlock;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public ObservableCollection<OptionsMenuItem> OptionsMenu { get; set; }

    public Grid MainContent => this.ContentGrid;

    public Grid SubContent => this.SubContentGrid;

    public double HeaderHeight
    {
      get
      {
        return ((FrameworkElement) this.Offset).Height + ((FrameworkElement) this._headerGrid).ActualHeight;
      }
    }

    public Grid BackBackground => this.back;

    public void ShowProgress(bool show)
    {
      ((UIElement) this.progressBar).put_Visibility(show ? (Visibility) 0 : (Visibility) 1);
    }

    public bool HideSandwitchButton
    {
      get => this._hideSandwitchButton;
      set
      {
        if (value == this._hideSandwitchButton)
          return;
        this._hideSandwitchButton = value;
        ((UIElement) this.borderSandwich).put_Visibility(this._hideSandwitchButton ? (Visibility) 1 : (Visibility) 0);
      }
    }

    public void ActivateSearch(bool active)
    {
      if (active)
      {
        ((UIElement) this._onPageSearch).put_Visibility((Visibility) 0);
        ((Control) this._textBoxOnPageSearch).Focus((FocusState) 2);
      }
      else
        ((UIElement) this._onPageSearch).put_Visibility((Visibility) 1);
    }

    public void SetTitle(string text)
    {
      ((ICollection<UIElement>) ((Panel) this.MainContent).Children).Clear();
      TextBlock textBlock = new TextBlock();
      textBlock.put_FontSize((double) ((IDictionary<object, object>) Application.Current.Resources)[(object) "FontSizeContent"]);
      textBlock.put_Foreground((Brush) new SolidColorBrush(Colors.White));
      textBlock.put_Text(text);
      ((ICollection<UIElement>) ((Panel) this.MainContent).Children).Add((UIElement) textBlock);
    }

    public PullToRefreshUC PullToRefresh => this.ucPullToRefresh;

    public Grid HeaderGrid => this._headerGrid;

    private MenuSearchViewModel SearchVM
    {
      get => ((FrameworkElement) this.serachHints).DataContext as MenuSearchViewModel;
    }

    public HeaderWithMenuUC()
    {
      this.OptionsMenu = new ObservableCollection<OptionsMenuItem>();
      ((FrameworkElement) this).put_DataContext((object) MenuViewModel.Instance);
      this.InitializeComponent();
      ApplicationView forCurrentView = ApplicationView.GetForCurrentView();
      // ISSUE: method pointer
      WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<ApplicationView, object>>(new Func<TypedEventHandler<ApplicationView, object>, EventRegistrationToken>(forCurrentView.add_VisibleBoundsChanged), new Action<EventRegistrationToken>(forCurrentView.remove_VisibleBoundsChanged), new TypedEventHandler<ApplicationView, object>((object) this, __methodptr(App_VisibleBoundsChanged)));
      QuadraticEase quadraticEase = new QuadraticEase();
      ((EasingFunctionBase) quadraticEase).put_EasingMode((EasingMode) 0);
      this._menuEasing = (EasingFunctionBase) quadraticEase;
      HeaderWithMenuUC headerWithMenuUc = this;
      WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(((FrameworkElement) headerWithMenuUc).add_Loaded), new Action<EventRegistrationToken>(((FrameworkElement) headerWithMenuUc).remove_Loaded), new RoutedEventHandler(this.HeaderWithMenuUC_Loaded));
      Frame content = Window.Current.Content as Frame;
      WindowsRuntimeMarshal.AddEventHandler<SizeChangedEventHandler>(new Func<SizeChangedEventHandler, EventRegistrationToken>(((FrameworkElement) content).add_SizeChanged), new Action<EventRegistrationToken>(((FrameworkElement) content).remove_SizeChanged), new SizeChangedEventHandler(this.HeaderWithMenuUC_SizeChanged));
      (Window.Current.Content as CustomFrame).MenuStateChanged += new EventHandler<CustomFrame.MenuStates>(this.MenuStateChanged);
      this.itemsControlOptionsMenu.put_ItemsSource((object) this.OptionsMenu);
      ((FrameworkElement) this.serachHints).put_DataContext((object) new MenuSearchViewModel());
      this.userPhoto.LetsRound();
    }

    private void HeaderWithMenuUC_SizeChanged(object sender, SizeChangedEventArgs e)
    {
      if ((Window.Current.Content as CustomFrame).IsDevicePhone)
        Constants.MENU_WIDE_WIDTH = Math.Min(((FrameworkElement) (Window.Current.Content as Frame)).ActualWidth, ((FrameworkElement) (Window.Current.Content as Frame)).ActualHeight) * 0.75;
      ((FrameworkElement) this._searchGrid).put_Width(Constants.MENU_WIDE_WIDTH);
      this._searchTransform.put_X(-Constants.MENU_WIDE_WIDTH);
    }

    private void HeaderWithMenuUC_Loaded(object sender, RoutedEventArgs e)
    {
      this.MenuStateChanged((object) this, (Window.Current.Content as CustomFrame).MenuState);
      ((FrameworkElement) this._menuGrid).put_Margin(new Thickness(0.0, ((FrameworkElement) this.Offset).Height + (double) ((IDictionary<object, object>) Application.Current.Resources)[(object) "Double55"], 0.0, 0.0));
      ((FrameworkElement) this.serachHints).put_Margin(new Thickness(0.0, ((FrameworkElement) this.Offset).Height + (double) ((IDictionary<object, object>) Application.Current.Resources)[(object) "Double55"], 0.0, 0.0));
    }

    private void App_VisibleBoundsChanged(ApplicationView sender, object args)
    {
      ApplicationView applicationView = sender;
      if ((Window.Current.Content as CustomFrame).CurrentOrientation == 1 && applicationView.VisibleBounds.Top == 0.0)
        return;
      ((FrameworkElement) this.Offset).put_Height(applicationView.VisibleBounds.Top);
      ((FrameworkElement) this._menuGrid).put_Margin(new Thickness(0.0, ((FrameworkElement) this.Offset).Height + (double) ((IDictionary<object, object>) Application.Current.Resources)[(object) "Double55"], 0.0, 0.0));
      ((FrameworkElement) this.serachHints).put_Margin(new Thickness(0.0, ((FrameworkElement) this.Offset).Height + (double) ((IDictionary<object, object>) Application.Current.Resources)[(object) "Double55"], 0.0, 0.0));
    }

    private void MenuStateChanged(object sender, CustomFrame.MenuStates e)
    {
      switch (e)
      {
        case CustomFrame.MenuStates.StateWide:
          this._headerContentTransform.put_X(Constants.MENU_WIDE_WIDTH);
          ((FrameworkElement) this._menuGrid).put_Width(Constants.MENU_WIDE_WIDTH);
          if ((Window.Current.Content as CustomFrame).CurrentOrientation == null)
            ((UIElement) this.OverlayFromMenu).put_Visibility((Visibility) 1);
          Border userPhoto1 = this.userPhoto;
          double num1;
          ((FrameworkElement) this.userPhoto).put_MaxWidth(num1 = double.PositiveInfinity);
          double num2 = num1;
          ((FrameworkElement) userPhoto1).put_MaxHeight(num2);
          ((UIElement) this._menuCallout).put_ManipulationMode((ManipulationModes) 1);
          ApplicationView.GetForCurrentView().put_SuppressSystemOverlays(false);
          break;
        case CustomFrame.MenuStates.StateNarrow:
          ((FrameworkElement) this._menuGrid).put_Width(Constants.MENU_NARROW_WIDTH);
          this._menuTransform.put_X(0.0);
          this._headerContentTransform.put_X(0.0);
          Border userPhoto2 = this.userPhoto;
          double num3;
          ((FrameworkElement) this.userPhoto).put_MaxWidth(num3 = 40.0);
          double num4 = num3;
          ((FrameworkElement) userPhoto2).put_MaxHeight(num4);
          ((UIElement) this._menuCallout).put_ManipulationMode((ManipulationModes) 0);
          ApplicationView.GetForCurrentView().put_SuppressSystemOverlays(true);
          break;
        case CustomFrame.MenuStates.StateCollapsed:
          ((FrameworkElement) this._menuGrid).put_Width(Constants.MENU_WIDE_WIDTH);
          if ((Window.Current.Content as CustomFrame).IsDevicePhone)
          {
            this._menuTransform.put_X(-((FrameworkElement) this._menuGrid).Width);
            this._headerContentTransform.put_X(0.0);
          }
          else
            this._menuTransform.put_X(-Constants.MENU_WIDE_WIDTH);
          Border userPhoto3 = this.userPhoto;
          double num5;
          ((FrameworkElement) this.userPhoto).put_MaxWidth(num5 = double.PositiveInfinity);
          double num6 = num5;
          ((FrameworkElement) userPhoto3).put_MaxHeight(num6);
          ((UIElement) this._menuCallout).put_ManipulationMode((ManipulationModes) 1);
          ApplicationView.GetForCurrentView().put_SuppressSystemOverlays(false);
          break;
      }
    }

    private void borderSandwich_Tapped(object sender, TappedRoutedEventArgs e)
    {
      e.put_Handled(true);
      if ((Window.Current.Content as CustomFrame).MenuState == CustomFrame.MenuStates.StateNarrow)
        return;
      this.SearchVM.Query = "";
      this.SearchVM.Items.Clear();
      this.OpenCloseMenu();
    }

    private void Canvas_Tapped(object sender, TappedRoutedEventArgs e)
    {
    }

    private void MenuItemUC_Tapped_Messages(object sender, TappedRoutedEventArgs e)
    {
      this.NavigateOnMenuClick((Action) (() => NavigatorImpl.Instance.NavigateToConversations()));
    }

    private void MenuItemUC_Tapped_News(object sender, TappedRoutedEventArgs e)
    {
      this.NavigateOnMenuClick((Action) (() => NavigatorImpl.Instance.NavigateToNewsFeed()));
    }

    private void MenuItemUC_Tapped(object sender, TappedRoutedEventArgs e)
    {
      this.NavigateOnMenuClick((Action) (() => NavigatorImpl.Instance.NavigateToSettings()));
    }

    private void MenuItemUC_Tapped_1(object sender, TappedRoutedEventArgs e)
    {
      this.NavigateOnMenuClick((Action) (() => NavigatorImpl.Instance.NavigateToGroups(Settings.Instance.auth.UserId)));
    }

    private void MenuItemUC_Tapped_2(object sender, TappedRoutedEventArgs e)
    {
      this.NavigateOnMenuClick((Action) (() => NavigatorImpl.Instance.NavigateToFriends(Settings.Instance.auth.UserId)));
    }

    private void MenuItemUC_Tapped_3(object sender, TappedRoutedEventArgs e)
    {
      this.NavigateOnMenuClick((Action) (() => NavigatorImpl.Instance.NavigateToFeedback()));
    }

    private void MenuItemUC_Tapped_4(object sender, TappedRoutedEventArgs e)
    {
      this.NavigateOnMenuClick((Action) (() => NavigatorImpl.Instance.NavigateToPhotoAlbums(Settings.Instance.auth.UserId)));
    }

    private void MenuItemUC_Tapped_5(object sender, TappedRoutedEventArgs e)
    {
      (Window.Current.Content as Frame).Navigate(typeof (VideoCatalogPage));
    }

    public void Rectangle_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
    {
      Point translation = e.Delta.Translation;
      TranslateTransform menuTransform = this._menuTransform;
      double num1 = menuTransform.X + translation.X;
      if (num1 >= 0.0)
        num1 = 0.0;
      else if (num1 < -((FrameworkElement) this._menuGrid).Width)
        num1 = -((FrameworkElement) this._menuGrid).Width;
      menuTransform.put_X(num1);
      this._headerContentTransform.put_X(num1 + ((FrameworkElement) this._menuGrid).Width);
      double num2 = 1.0 - -menuTransform.X / ((FrameworkElement) this._menuGrid).Width;
      Rectangle shadow = this._shadow;
      double num3;
      ((UIElement) this.OverlayFromMenu).put_Opacity(num3 = num2);
      double num4 = num3;
      ((UIElement) shadow).put_Opacity(num4);
      e.put_Handled(true);
    }

    public void Rectangle_ManipulationCompleted(
      object sender,
      ManipulationCompletedRoutedEventArgs e)
    {
      double x1 = e.Velocities.Linear.X;
      double x2 = e.Cumulative.Translation.X;
      if (this._isMenuOpen ? x2 < -((FrameworkElement) this._menuGrid).Width / 2.0 || x1 < -1.5 : x2 > ((FrameworkElement) this._menuGrid).Width / 2.0 || x1 > 1.5)
        this._isMenuOpen = !this._isMenuOpen;
      this.OpenCloseMenu(new bool?(this._isMenuOpen));
    }

    public void OpenCloseMenu(bool? open = null, bool withoutAnimation = false)
    {
      if (!open.HasValue)
        open = new bool?(!this._isMenuOpen);
      this._isMenuOpen = open.Value;
      this.AnimateMenu(open.Value, withoutAnimation ? 0 : 250);
      this.SearchTextBox.put_Text("");
      this.SearchVM.Items.Clear();
      ((UIElement) this.serachHints).put_Visibility((Visibility) 1);
    }

    private void AnimateMenu(bool open, int duration = 300)
    {
      double to1 = open ? 0.0 : -((FrameworkElement) this._menuGrid).Width;
      double to2 = open ? ((FrameworkElement) this._menuGrid).Width : 0.0;
      ((DependencyObject) this._menuTransform).Animate(this._menuTransform.X, to1, "X", duration, easing: this._menuEasing);
      ((DependencyObject) this._headerContentTransform).Animate(this._headerContentTransform.X, to2, "X", duration, easing: this._menuEasing);
      ((UIElement) this.OverlayFromMenu).put_Visibility(open ? (Visibility) 0 : (Visibility) 1);
      ((DependencyObject) this.OverlayFromMenu).Animate(((UIElement) this.OverlayFromMenu).Opacity, open ? 1.0 : 0.0, "Opacity", duration);
      ((DependencyObject) this._shadow).Animate(((UIElement) this._shadow).Opacity, open ? 1.0 : 0.0, "Opacity", duration);
      if (((UIElement) this.BackBackground).Opacity >= 1.0)
        return;
      ((DependencyObject) this.BackBackground).Animate(((UIElement) this.BackBackground).Opacity, 1.0, "Opacity", duration / 2);
    }

    private void MenuItemUC_Tapped_6(object sender, TappedRoutedEventArgs e)
    {
      NavigatorImpl.Instance.NavigateToMarketPage();
    }

    private void NavigateOnMenuClick(Action navigateAction, bool needClearStack = true)
    {
      if (needClearStack)
        (Window.Current.Content as CustomFrame)._shouldResetStack = true;
      navigateAction();
    }

    private void StackPanel_Tapped(object sender, TappedRoutedEventArgs e)
    {
      NavigatorImpl.Instance.NavigateToProfilePage((long) (int) Settings.Instance.auth.UserId);
    }

    public void SuppressMenu(bool status)
    {
      ((UIElement) this).put_Visibility(status ? (Visibility) 1 : (Visibility) 0);
    }

    public void _menuCallout_ManipulationStarted(
      object sender,
      ManipulationStartedRoutedEventArgs e)
    {
      if (this._menuTransform.X != 0.0)
        ((UIElement) this.OverlayFromMenu).put_Visibility((Visibility) 0);
      if (((Control) this.SearchTextBox).FocusState == 2 || ((Control) this.SearchTextBox).FocusState == 1)
      {
        this.SearchTextBox.put_Text("");
        this.SearchVM.Items.Clear();
        ((Control) this.SearchTextBox).put_IsEnabled(false);
        ((Control) this.SearchTextBox).put_IsEnabled(true);
      }
      if (((UIElement) this.BackBackground).Opacity < 1.0)
        ((DependencyObject) this.BackBackground).Animate(((UIElement) this.BackBackground).Opacity, 1.0, "Opacity", 100);
      e.put_Handled(true);
    }

    private void OverlayFromMenu_Tapped(object sender, TappedRoutedEventArgs e)
    {
      this.OpenCloseMenu(new bool?(false));
    }

    private void TextBox_GotFocus(object sender, RoutedEventArgs e)
    {
      if (this.searchTimer == null)
      {
        this.searchTimer = new DispatcherTimer();
        this.searchTimer.put_Interval(TimeSpan.FromSeconds(0.5));
        DispatcherTimer searchTimer = this.searchTimer;
        WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(searchTimer.add_Tick), new Action<EventRegistrationToken>(searchTimer.remove_Tick), new EventHandler<object>(this.Timer_Tick));
      }
      ((UIElement) this.serachHints).put_Visibility((Visibility) 0);
    }

    private void Timer_Tick(object sender, object e)
    {
      (sender as DispatcherTimer).Stop();
      if (!string.IsNullOrEmpty(this.SearchVM.Query))
        this.SearchVM.LoadData(true);
      else
        this.SearchVM.Items.Clear();
    }

    private void TextBox_LostFocus(object sender, RoutedEventArgs e)
    {
      if (!string.IsNullOrEmpty((sender as TextBox).Text))
        return;
      ((UIElement) this.serachHints).put_Visibility((Visibility) 1);
      this.SearchVM.Items.Clear();
    }

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
      if (this.searchTimer.IsEnabled)
        this.searchTimer.Stop();
      this.SearchVM.Query = (sender as TextBox).Text;
      if (!string.IsNullOrEmpty(this.SearchVM.Query))
        this.searchTimer.Start();
      else
        this.SearchVM.Items.Clear();
    }

    private void ItemSearchUC_Tapped(object sender, TappedRoutedEventArgs e)
    {
      MenuSearchViewModel.SearchHint dataContext = (sender as FrameworkElement).DataContext as MenuSearchViewModel.SearchHint;
      if (dataContext.profile != null)
        NavigatorImpl.Instance.NavigateToProfilePage((long) dataContext.profile.id);
      else if (dataContext.group != null)
        NavigatorImpl.Instance.NavigateToProfilePage((long) -dataContext.group.id);
      this.SearchTextBox.put_Text("");
      ((UIElement) this.serachHints).put_Visibility((Visibility) 1);
      this.SearchVM.Items.Clear();
    }

    private void MenuItemUC_Tapped_7(object sender, TappedRoutedEventArgs e)
    {
      NavigatorImpl.Instance.NavigateToAudio((int) Settings.Instance.auth.UserId);
    }

    private void MenuItemUC_Tapped_8(object sender, TappedRoutedEventArgs e)
    {
      NavigatorImpl.Instance.NavigateToFavorites();
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///UC/HeaderWithMenuUC.xaml"), (ComponentResourceLocation) 0);
      this.OverlayFromMenu = (Grid) ((FrameworkElement) this).FindName("OverlayFromMenu");
      this.root = (Grid) ((FrameworkElement) this).FindName("root");
      this._menuGrid = (Grid) ((FrameworkElement) this).FindName("_menuGrid");
      this.serachHints = (Grid) ((FrameworkElement) this).FindName("serachHints");
      this.ucPullToRefresh = (PullToRefreshUC) ((FrameworkElement) this).FindName("ucPullToRefresh");
      this._menuTransform = (TranslateTransform) ((FrameworkElement) this).FindName("_menuTransform");
      this._shadow = (Rectangle) ((FrameworkElement) this).FindName("_shadow");
      this._menuCallout = (Rectangle) ((FrameworkElement) this).FindName("_menuCallout");
      this._stack = (StackPanel) ((FrameworkElement) this).FindName("_stack");
      this.userPhoto = (Border) ((FrameworkElement) this).FindName("userPhoto");
      this.Offset = (Rectangle) ((FrameworkElement) this).FindName("Offset");
      this.back = (Grid) ((FrameworkElement) this).FindName("back");
      this.progressBar = (ProgressBar) ((FrameworkElement) this).FindName("progressBar");
      this._headerContentTransform = (TranslateTransform) ((FrameworkElement) this).FindName("_headerContentTransform");
      this._headerGrid = (Grid) ((FrameworkElement) this).FindName("_headerGrid");
      this._searchGrid = (Grid) ((FrameworkElement) this).FindName("_searchGrid");
      this._onPageSearch = (Grid) ((FrameworkElement) this).FindName("_onPageSearch");
      this._textBoxOnPageSearch = (TextBox) ((FrameworkElement) this).FindName("_textBoxOnPageSearch");
      this._searchTransform = (TranslateTransform) ((FrameworkElement) this).FindName("_searchTransform");
      this.SearchTextBox = (TextBox) ((FrameworkElement) this).FindName("SearchTextBox");
      this.borderSandwich = (Grid) ((FrameworkElement) this).FindName("borderSandwich");
      this.ContentGrid = (Grid) ((FrameworkElement) this).FindName("ContentGrid");
      this.SubContentGrid = (Grid) ((FrameworkElement) this).FindName("SubContentGrid");
      this.itemsControlFilterMenu = (StackPanel) ((FrameworkElement) this).FindName("itemsControlFilterMenu");
      this.itemsControlOptionsMenu = (ItemsControl) ((FrameworkElement) this).FindName("itemsControlOptionsMenu");
      this.counterPanel = (Grid) ((FrameworkElement) this).FindName("counterPanel");
      this.brdCount = (Border) ((FrameworkElement) this).FindName("brdCount");
      this.counterBlock = (TextBlock) ((FrameworkElement) this).FindName("counterBlock");
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          UIElement uiElement1 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement1.add_Tapped), new Action<EventRegistrationToken>(uiElement1.remove_Tapped), new TappedEventHandler(this.OverlayFromMenu_Tapped));
          UIElement uiElement2 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<ManipulationDeltaEventHandler>(new Func<ManipulationDeltaEventHandler, EventRegistrationToken>(uiElement2.add_ManipulationDelta), new Action<EventRegistrationToken>(uiElement2.remove_ManipulationDelta), new ManipulationDeltaEventHandler(this.Rectangle_ManipulationDelta));
          UIElement uiElement3 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<ManipulationCompletedEventHandler>(new Func<ManipulationCompletedEventHandler, EventRegistrationToken>(uiElement3.add_ManipulationCompleted), new Action<EventRegistrationToken>(uiElement3.remove_ManipulationCompleted), new ManipulationCompletedEventHandler(this.Rectangle_ManipulationCompleted));
          UIElement uiElement4 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<ManipulationStartedEventHandler>(new Func<ManipulationStartedEventHandler, EventRegistrationToken>(uiElement4.add_ManipulationStarted), new Action<EventRegistrationToken>(uiElement4.remove_ManipulationStarted), new ManipulationStartedEventHandler(this._menuCallout_ManipulationStarted));
          break;
        case 2:
          UIElement uiElement5 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement5.add_Tapped), new Action<EventRegistrationToken>(uiElement5.remove_Tapped), new TappedEventHandler(this.ItemSearchUC_Tapped));
          break;
        case 3:
          UIElement uiElement6 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<ManipulationDeltaEventHandler>(new Func<ManipulationDeltaEventHandler, EventRegistrationToken>(uiElement6.add_ManipulationDelta), new Action<EventRegistrationToken>(uiElement6.remove_ManipulationDelta), new ManipulationDeltaEventHandler(this.Rectangle_ManipulationDelta));
          UIElement uiElement7 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<ManipulationCompletedEventHandler>(new Func<ManipulationCompletedEventHandler, EventRegistrationToken>(uiElement7.add_ManipulationCompleted), new Action<EventRegistrationToken>(uiElement7.remove_ManipulationCompleted), new ManipulationCompletedEventHandler(this.Rectangle_ManipulationCompleted));
          UIElement uiElement8 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<ManipulationStartedEventHandler>(new Func<ManipulationStartedEventHandler, EventRegistrationToken>(uiElement8.add_ManipulationStarted), new Action<EventRegistrationToken>(uiElement8.remove_ManipulationStarted), new ManipulationStartedEventHandler(this._menuCallout_ManipulationStarted));
          break;
        case 4:
          UIElement uiElement9 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement9.add_Tapped), new Action<EventRegistrationToken>(uiElement9.remove_Tapped), new TappedEventHandler(this.StackPanel_Tapped));
          break;
        case 5:
          UIElement uiElement10 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement10.add_Tapped), new Action<EventRegistrationToken>(uiElement10.remove_Tapped), new TappedEventHandler(this.MenuItemUC_Tapped_News));
          break;
        case 6:
          UIElement uiElement11 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement11.add_Tapped), new Action<EventRegistrationToken>(uiElement11.remove_Tapped), new TappedEventHandler(this.MenuItemUC_Tapped_3));
          break;
        case 7:
          UIElement uiElement12 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement12.add_Tapped), new Action<EventRegistrationToken>(uiElement12.remove_Tapped), new TappedEventHandler(this.MenuItemUC_Tapped_Messages));
          break;
        case 8:
          UIElement uiElement13 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement13.add_Tapped), new Action<EventRegistrationToken>(uiElement13.remove_Tapped), new TappedEventHandler(this.MenuItemUC_Tapped_2));
          break;
        case 9:
          UIElement uiElement14 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement14.add_Tapped), new Action<EventRegistrationToken>(uiElement14.remove_Tapped), new TappedEventHandler(this.MenuItemUC_Tapped_1));
          break;
        case 10:
          UIElement uiElement15 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement15.add_Tapped), new Action<EventRegistrationToken>(uiElement15.remove_Tapped), new TappedEventHandler(this.MenuItemUC_Tapped_4));
          break;
        case 11:
          UIElement uiElement16 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement16.add_Tapped), new Action<EventRegistrationToken>(uiElement16.remove_Tapped), new TappedEventHandler(this.MenuItemUC_Tapped_5));
          break;
        case 12:
          UIElement uiElement17 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement17.add_Tapped), new Action<EventRegistrationToken>(uiElement17.remove_Tapped), new TappedEventHandler(this.MenuItemUC_Tapped_7));
          break;
        case 13:
          UIElement uiElement18 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement18.add_Tapped), new Action<EventRegistrationToken>(uiElement18.remove_Tapped), new TappedEventHandler(this.MenuItemUC_Tapped_8));
          break;
        case 14:
          UIElement uiElement19 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement19.add_Tapped), new Action<EventRegistrationToken>(uiElement19.remove_Tapped), new TappedEventHandler(this.MenuItemUC_Tapped));
          break;
        case 15:
          UIElement uiElement20 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(uiElement20.add_GotFocus), new Action<EventRegistrationToken>(uiElement20.remove_GotFocus), new RoutedEventHandler(this.TextBox_GotFocus));
          UIElement uiElement21 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(uiElement21.add_LostFocus), new Action<EventRegistrationToken>(uiElement21.remove_LostFocus), new RoutedEventHandler(this.TextBox_LostFocus));
          TextBox textBox = (TextBox) target;
          WindowsRuntimeMarshal.AddEventHandler<TextChangedEventHandler>(new Func<TextChangedEventHandler, EventRegistrationToken>(textBox.add_TextChanged), new Action<EventRegistrationToken>(textBox.remove_TextChanged), new TextChangedEventHandler(this.TextBox_TextChanged));
          break;
        case 16:
          UIElement uiElement22 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement22.add_Tapped), new Action<EventRegistrationToken>(uiElement22.remove_Tapped), new TappedEventHandler(this.borderSandwich_Tapped));
          break;
        case 17:
          UIElement uiElement23 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement23.add_Tapped), new Action<EventRegistrationToken>(uiElement23.remove_Tapped), new TappedEventHandler(this.Canvas_Tapped));
          break;
      }
      this._contentLoaded = true;
    }
  }
}
