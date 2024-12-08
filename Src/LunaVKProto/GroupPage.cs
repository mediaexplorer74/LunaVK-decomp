// Decompiled with JetBrains decompiler
// Type: App1uwp.GroupPage
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
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.ApplicationModel.DataTransfer;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

#nullable disable
namespace App1uwp
{
  public sealed class GroupPage : PageBase, IComponentConnector
  {
    private double _previousScrollPosition;
    private HideHeaderHelper _hideHelper;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private ExtendedListView2 MainScroll;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private StackPanel _main;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private LoadingUC loading;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TranslateTransform transformCover;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public GroupPage()
    {
      this.InitializeComponent();
      this.MainScroll.InsideScrollViewerLoaded += new Action(this.InsideScrollViewerLoaded);
      ((UIElement) (Window.Current.Content as CustomFrame).HeaderWithMenu.BackBackground).put_Opacity(0.0);
    }

    private void InsideScrollViewerLoaded()
    {
      ScrollViewer insideScrollViewer = this.MainScroll.GetInsideScrollViewer;
      WindowsRuntimeMarshal.AddEventHandler<EventHandler<ScrollViewerViewChangedEventArgs>>(new Func<EventHandler<ScrollViewerViewChangedEventArgs>, EventRegistrationToken>(insideScrollViewer.add_ViewChanged), new Action<EventRegistrationToken>(insideScrollViewer.remove_ViewChanged), new EventHandler<ScrollViewerViewChangedEventArgs>(this.GetInsideScrollViewer_ViewChanged));
    }

    private GroupViewModel VM => ((FrameworkElement) this).DataContext as GroupViewModel;

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
      if (num1 == 0.0)
        this.transformCover.put_Y(0.0);
      this._previousScrollPosition = num1;
    }

    private CustomFrame CFrame => Window.Current.Content as CustomFrame;

    protected override void HandleOnNavigatingFrom(NavigatingCancelEventArgs e)
    {
      ((UIElement) (Window.Current.Content as CustomFrame).HeaderWithMenu.BackBackground).put_Opacity(1.0);
    }

    protected override void HandleOnNavigatedTo(NavigationEventArgs e)
    {
      IDictionary<string, long> parameter = e.Parameter as IDictionary<string, long>;
      base.HandleOnNavigatedTo(e);
      ((FrameworkElement) this).put_DataContext((object) new GroupViewModel(parameter["Id"]));
      this.VM.LoadingStatusUpdated += new Action<ProfileLoadingStatus>(this.HandleLoadingStatusUpdated);
      this.VM.LoadData(true);
    }

    private void CreateAppBar()
    {
      this.CFrame.CommandBar.PrimaryCommands.Clear();
      this.CFrame.CommandBar.SecondaryCommands.Clear();
      if (this.VM.GroupData.can_post)
        this.CFrame.CommandBar.PrimaryCommands.Add(new CommandBarButton()
        {
          Icon = "\uE929",
          Label = "добавить новость"
        });
      if (this.VM.GroupData.can_message)
        this.CFrame.CommandBar.PrimaryCommands.Add(new CommandBarButton()
        {
          Icon = "\uE715",
          Label = "сообщение",
          Command = (ICommand) new DelegateCommand((Action<object>) (args => NavigatorImpl.Instance.NavigateToConversation(-this.VM.GroupData.id)))
        });
      if (this.VM.GroupData.is_admin)
        this.CFrame.CommandBar.PrimaryCommands.Add(new CommandBarButton()
        {
          Icon = "\uE713",
          Label = "управление",
          Command = (ICommand) new DelegateCommand((Action<object>) (args => NavigatorImpl.Instance.NavigateToCommunityManagement(-this.VM.Id)))
        });
      this.CFrame.CommandBar.SecondaryCommands.Add(new CommandBarButton()
      {
        Label = this.VM.GroupData.is_subscribed ? "не уведомлять о новых записях" : "уведомлять о новых записях",
        Icon = "\uEA8F",
        Command = (ICommand) new DelegateCommand((Action<object>) (args => this.VM.SubscribeUnsubscribe((Action<bool>) (ret =>
        {
          this.VM.GroupData.is_subscribed = !this.VM.GroupData.is_subscribed;
          this.CreateAppBar();
        }))))
      });
      this.CFrame.CommandBar.SecondaryCommands.Add(new CommandBarButton()
      {
        Label = "копировать ссылку",
        Icon = "\uE8C8",
        Command = (ICommand) new DelegateCommand((Action<object>) (args => new DataPackage().SetWebLink(new Uri("https://vk.com/club" + (object) this.VM.GroupData.id))))
      });
      this.CFrame.CommandBar.SecondaryCommands.Add(new CommandBarButton()
      {
        Label = "открыть в браузере",
        Icon = "\uE774",
        Command = (ICommand) new DelegateCommand((Action<object>) (args => Launcher.LaunchUriAsync(new Uri("https://vk.com/club" + (object) this.VM.GroupData.id))))
      });
      if (this.VM.CanFaveUnfave)
        this.CFrame.CommandBar.SecondaryCommands.Add(new CommandBarButton()
        {
          Label = this.VM.GroupData.is_favorite ? "удалить из закладок" : "добавить в закладки",
          Icon = "\uE734",
          Command = (ICommand) new DelegateCommand((Action<object>) (args => this.VM.FaveUnfave((Action<bool>) (ret =>
          {
            this.VM.GroupData.is_favorite = !this.VM.GroupData.is_favorite;
            this.CreateAppBar();
          }))))
        });
      this.CFrame.CommandBar.SecondaryCommands.Add(new CommandBarButton()
      {
        Label = this.VM.GroupData.is_member ? "покинуть группу" : "вступить в группу",
        Icon = "\uE945",
        Command = (ICommand) new DelegateCommand((Action<object>) (args => this.VM.JoinLeave((Action<bool>) (ret =>
        {
          this.VM.GroupData.is_member = !this.VM.GroupData.is_member;
          this.CreateAppBar();
        }))))
      });
    }

    private void HandleLoadingStatusUpdated(ProfileLoadingStatus status)
    {
      if (status == ProfileLoadingStatus.Loaded)
      {
        ((UIElement) this._main).put_Visibility((Visibility) 0);
        this.CreateAppBar();
        if (this.VM.GroupData.cover.enabled && this._hideHelper == null)
        {
          this._hideHelper = new HideHeaderHelper(this.MainScroll.GetInsideScrollViewer);
        }
        else
        {
          this.MainScroll.UseHeaderOffset = true;
          ((UIElement) (Window.Current.Content as CustomFrame).HeaderWithMenu.BackBackground).put_Opacity(1.0);
        }
      }
      VisualStateManager.GoToState((Control) this.loading, status.ToString(), false);
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///GroupPage.xaml"), (ComponentResourceLocation) 0);
      this.MainScroll = (ExtendedListView2) ((FrameworkElement) this).FindName("MainScroll");
      this._main = (StackPanel) ((FrameworkElement) this).FindName("_main");
      this.loading = (LoadingUC) ((FrameworkElement) this).FindName("loading");
      this.transformCover = (TranslateTransform) ((FrameworkElement) this).FindName("transformCover");
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void Connect(int connectionId, object target) => this._contentLoaded = true;
  }
}
