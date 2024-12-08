// Decompiled with JetBrains decompiler
// Type: App1uwp.NewsPage
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
using App1uwp.Library;
using App1uwp.Network.Enums;
using App1uwp.Network.ViewModels;
using App1uwp.UC;
using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Navigation;

#nullable disable
namespace App1uwp
{
  public sealed class NewsPage : PageBase, IComponentConnector
  {
    private static double _scrollPosition;
    private PopUP pop;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private ExtendedListView2 MainScroll;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private LoadingUC loading;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private StackPanel stackPanel;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBlock textBlockTitle;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Border borderMenuOpenIcon;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    private CustomFrame СFrame => Window.Current.Content as CustomFrame;

    public NewsPage()
    {
      this.InitializeComponent();
      ExtendedListView2 mainScroll = this.MainScroll;
      WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(((FrameworkElement) mainScroll).add_Loaded), new Action<EventRegistrationToken>(((FrameworkElement) mainScroll).remove_Loaded), new RoutedEventHandler(this.MainScroll_Loaded));
      this.MainScroll.InsideScrollViewerLoaded += new Action(this.InsideScrollViewerLoaded);
      NewsPage newsPage1 = this;
      WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(((FrameworkElement) newsPage1).add_Loaded), new Action<EventRegistrationToken>(((FrameworkElement) newsPage1).remove_Loaded), new RoutedEventHandler(this.NewsPage_Loaded));
      NewsPage newsPage2 = this;
      WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(((FrameworkElement) newsPage2).add_Unloaded), new Action<EventRegistrationToken>(((FrameworkElement) newsPage2).remove_Unloaded), new RoutedEventHandler(this.NewsPage_Unloaded));
    }

    private void NewsPage_Unloaded(object sender, RoutedEventArgs e)
    {
    }

    private void NewsPage_Loaded(object sender, RoutedEventArgs e)
    {
    }

    private void MainScroll_Loaded(object sender, RoutedEventArgs e)
    {
      this.СFrame.HeaderWithMenu.PullToRefresh.TrackListBox(this.MainScroll);
      this.СFrame.HeaderWithMenu.OptionsMenu.Add(new OptionsMenuItem()
      {
        Icon = "\uEDC6"
      });
      this.СFrame.HeaderWithMenu.OptionsMenu.Add(new OptionsMenuItem()
      {
        Icon = "\uE721"
      });
      Grid headerGrid = this.СFrame.HeaderWithMenu.HeaderGrid;
      WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(((UIElement) headerGrid).add_Tapped), new Action<EventRegistrationToken>(((UIElement) headerGrid).remove_Tapped), new TappedEventHandler(this._header_Tapped));
    }

    private void InsideScrollViewerLoaded()
    {
      if (NewsPage._scrollPosition <= 0.0)
        return;
      this.MainScroll.GetInsideScrollViewer.ScrollToVerticalOffset(NewsPage._scrollPosition);
    }

    private void OpenNewsSourcePicker(object sender, TappedRoutedEventArgs e)
    {
      Point position = e.GetPosition((UIElement) null);
      if (this.pop == null)
      {
        this.pop = new PopUP();
        this.pop.AddItem(0, LocalizedStrings.GetString("Menu_News/Title"), "\uE8A1");
        this.pop.AddItem(1, LocalizedStrings.GetString("NewsFeed_Recommended/Title"), "\uE789");
        this.pop.AddItem(2, LocalizedStrings.GetString("NewsFeed_Photos/Title"), "\uEB9F");
        this.pop.AddItem(3, LocalizedStrings.GetString("NewsFeed_Videos/Title"), "\uE714");
        this.pop.AddItem(4, LocalizedStrings.GetString("NewsFeed_Friends/Title"), "\uE77B");
        this.pop.ItemTapped += new EventHandler<int>(this._picker_ItemTapped);
      }
      this.pop.Show(position);
    }

    private void _picker_ItemTapped(object sender, int i)
    {
      (((FrameworkElement) this).DataContext as NewsViewModel).Title = this.pop.GetTitle(i);
      (((FrameworkElement) this).DataContext as NewsViewModel).SetNewsSource(i);
    }

    private void _header_Tapped(object sender, TappedRoutedEventArgs e)
    {
      this.MainScroll.GetInsideScrollViewer.ChangeView(new double?(0.0), new double?(0.0), new float?(1f));
    }

    protected override void HandleOnNavigatingFrom(NavigatingCancelEventArgs e)
    {
      base.HandleOnNavigatingFrom(e);
      NewsPage._scrollPosition = this.MainScroll.GetInsideScrollViewer.VerticalOffset;
    }

    protected override void HandleOnNavigatedTo(NavigationEventArgs e)
    {
      ((FrameworkElement) this).put_DataContext((object) NewsViewModel.Instance);
      NewsViewModel.Instance.LoadingStatusUpdated += new Action<ProfileLoadingStatus>(this.HandleLoadingStatusUpdated);
      if (NewsViewModel.Instance.Items.Count != 0)
        return;
      NewsViewModel.Instance.LoadData(true);
    }

    private void HandleLoadingStatusUpdated(ProfileLoadingStatus status)
    {
      VisualStateManager.GoToState((Control) this.loading, status.ToString(), false);
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///NewsPage.xaml"), (ComponentResourceLocation) 0);
      this.MainScroll = (ExtendedListView2) ((FrameworkElement) this).FindName("MainScroll");
      this.loading = (LoadingUC) ((FrameworkElement) this).FindName("loading");
      this.stackPanel = (StackPanel) ((FrameworkElement) this).FindName("stackPanel");
      this.textBlockTitle = (TextBlock) ((FrameworkElement) this).FindName("textBlockTitle");
      this.borderMenuOpenIcon = (Border) ((FrameworkElement) this).FindName("borderMenuOpenIcon");
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void Connect(int connectionId, object target)
    {
      if (connectionId == 1)
      {
        UIElement uiElement = (UIElement) target;
        WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement.add_Tapped), new Action<EventRegistrationToken>(uiElement.remove_Tapped), new TappedEventHandler(this.OpenNewsSourcePicker));
      }
      this._contentLoaded = true;
    }
  }
}
