// Decompiled with JetBrains decompiler
// Type: App1uwp.UserPage
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
using App1uwp.Library;
using App1uwp.Network.DataObjects;
using App1uwp.Network.Enums;
using App1uwp.Network.ViewModels;
using App1uwp.UC;
using App1uwp.Utils;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

#nullable disable
namespace App1uwp
{
  public sealed class UserPage : PageBase, IComponentConnector
  {
    private double _previousScrollPosition;
    private HideHeaderHelper _hideHelper;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private ExtendedListView2 MainScroll;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private StackPanel _main;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private ItemsControl _itemsControl;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private LoadingUC loading;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TranslateTransform transformCover;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public UserPage()
    {
      this.InitializeComponent();
      this.MainScroll.InsideScrollViewerLoaded += new Action(this.InsideScrollViewerLoaded);
      ((UIElement) (Window.Current.Content as CustomFrame).HeaderWithMenu.BackBackground).put_Opacity(0.0);
    }

    private void UserPage_SizeChanged(object sender, SizeChangedEventArgs e)
    {
      this.VM.UpdateProfilePhoto(e.NewSize.Width, 1.3);
    }

    private void InsideScrollViewerLoaded()
    {
      ScrollViewer insideScrollViewer = this.MainScroll.GetInsideScrollViewer;
      WindowsRuntimeMarshal.AddEventHandler<EventHandler<ScrollViewerViewChangedEventArgs>>(new Func<EventHandler<ScrollViewerViewChangedEventArgs>, EventRegistrationToken>(insideScrollViewer.add_ViewChanged), new Action<EventRegistrationToken>(insideScrollViewer.remove_ViewChanged), new EventHandler<ScrollViewerViewChangedEventArgs>(this.GetInsideScrollViewer_ViewChanged));
    }

    public ProfileViewModel VM => ((FrameworkElement) this).DataContext as ProfileViewModel;

    private void GetInsideScrollViewer_ViewChanged(
      object sender,
      ScrollViewerViewChangedEventArgs e)
    {
      double num1 = -(sender as ScrollViewer).VerticalOffset;
      double num2 = this._previousScrollPosition - num1;
      if (num2 - this.transformCover.Y < -140.0)
      {
        this.transformCover.put_Y(-140.0);
      }
      else
      {
        TranslateTransform transformCover = this.transformCover;
        transformCover.put_Y(transformCover.Y - num2 / 2.0);
      }
      this._previousScrollPosition = num1;
    }

    protected override void HandleOnNavigatingFrom(NavigatingCancelEventArgs e)
    {
      ((UIElement) (Window.Current.Content as CustomFrame).HeaderWithMenu.BackBackground).put_Opacity(1.0);
    }

    protected override void HandleOnNavigatedTo(NavigationEventArgs e)
    {
      IDictionary<string, long> parameter = e.Parameter as IDictionary<string, long>;
      base.HandleOnNavigatedTo(e);
      ((FrameworkElement) this).put_DataContext((object) new ProfileViewModel(parameter["Id"]));
      this.VM.LoadingStatusUpdated += new Action<ProfileLoadingStatus>(this.HandleLoadingStatusUpdated);
      this.VM.LoadData(true);
    }

    private void _header_Tapped(object sender, TappedRoutedEventArgs e)
    {
      this.MainScroll.GetInsideScrollViewer.ChangeView(new double?(0.0), new double?(0.0), new float?(1f));
    }

    private void CreateAppBar()
    {
      this.CFrame.CommandBar.PrimaryCommands.Clear();
      this.CFrame.CommandBar.SecondaryCommands.Clear();
      if (this.VM.UserData.can_write_private_message && (long) this.VM.UserData.id != Settings.Instance.auth.UserId)
        this.CFrame.CommandBar.PrimaryCommands.Add(new CommandBarButton()
        {
          Icon = "\uE715",
          Label = "сообщение",
          Command = (ICommand) new DelegateCommand((Action<object>) (args => NavigatorImpl.Instance.NavigateToConversation(this.VM.UserData.id)))
        });
      this.CFrame.CommandBar.SecondaryCommands.Add(new CommandBarButton()
      {
        Label = "открыть в браузере",
        Icon = "\uE774",
        Command = (ICommand) new DelegateCommand((Action<object>) (args => Launcher.LaunchUriAsync(new Uri("https://m.vk.com/id" + (object) this.VM.UserData.id))))
      });
      if (this.VM.CanFaveUnfave)
        this.CFrame.CommandBar.SecondaryCommands.Add(new CommandBarButton()
        {
          Label = this.VM.UserData.is_favorite ? "удалить из закладок" : "добавить в закладки",
          Icon = "\uE734",
          Command = (ICommand) new DelegateCommand((Action<object>) (args => { }))
        });
      if (!this.VM.CanSendGift)
        return;
      this.CFrame.CommandBar.PrimaryCommands.Add(new CommandBarButton()
      {
        Label = "отправить подарок",
        Icon = "\uE88C",
        Command = (ICommand) new DelegateCommand((Action<object>) (args => { }))
      });
    }

    private CustomFrame CFrame => Window.Current.Content as CustomFrame;

    private void HandleLoadingStatusUpdated(ProfileLoadingStatus status)
    {
      if (status == ProfileLoadingStatus.Loaded)
      {
        ((UIElement) this._main).put_Visibility((Visibility) 0);
        this.CreateAppBar();
        if (this._hideHelper == null)
        {
          this._hideHelper = new HideHeaderHelper(this.MainScroll.GetInsideScrollViewer);
        }
        else
        {
          this.MainScroll.UseHeaderOffset = true;
          ((UIElement) (Window.Current.Content as CustomFrame).HeaderWithMenu.BackBackground).put_Opacity(1.0);
        }
        UserPage userPage = this;
        WindowsRuntimeMarshal.AddEventHandler<SizeChangedEventHandler>(new Func<SizeChangedEventHandler, EventRegistrationToken>(((FrameworkElement) userPage).add_SizeChanged), new Action<EventRegistrationToken>(((FrameworkElement) userPage).remove_SizeChanged), new SizeChangedEventHandler(this.UserPage_SizeChanged));
      }
      VisualStateManager.GoToState((Control) this.loading, status.ToString(), false);
    }

    private Image GetImageFunc(int index)
    {
      return (((DependencyObject) (this._itemsControl.ContainerFromIndex(index) as ContentPresenter)).GetVisualChildren().ToList<DependencyObject>()[0] as Border).Child as Image;
    }

    private void Border_Tapped(object sender, TappedRoutedEventArgs e)
    {
      int ind = this.VM.Photos.IndexOf((sender as FrameworkElement).DataContext as VKPhoto);
      ImageViewerDecoratorUC viewerDecoratorUc = new ImageViewerDecoratorUC();
      viewerDecoratorUc.Initialize(this.VM.Photos.ToList<VKPhoto>(), (Func<int, Image>) (i => this.GetImageFunc(i)));
      viewerDecoratorUc.Show(ind);
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///UserPage.xaml"), (ComponentResourceLocation) 0);
      this.MainScroll = (ExtendedListView2) ((FrameworkElement) this).FindName("MainScroll");
      this._main = (StackPanel) ((FrameworkElement) this).FindName("_main");
      this._itemsControl = (ItemsControl) ((FrameworkElement) this).FindName("_itemsControl");
      this.loading = (LoadingUC) ((FrameworkElement) this).FindName("loading");
      this.transformCover = (TranslateTransform) ((FrameworkElement) this).FindName("transformCover");
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void Connect(int connectionId, object target)
    {
      if (connectionId == 1)
      {
        UIElement uiElement = (UIElement) target;
        WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement.add_Tapped), new Action<EventRegistrationToken>(uiElement.remove_Tapped), new TappedEventHandler(this.Border_Tapped));
      }
      this._contentLoaded = true;
    }
  }
}
