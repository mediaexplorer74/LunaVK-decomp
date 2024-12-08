// Decompiled with JetBrains decompiler
// Type: App1uwp.UC.ConversationsUC
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
using App1uwp.Library;
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

#nullable disable
namespace App1uwp.UC
{
  public sealed class ConversationsUC : UserControl, IComponentConnector
  {
    private bool _isShown;
    public bool InSelectionMode;
    public object param;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private ExtendedListView2 MainScroll;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public ConversationsUC()
    {
      this.InitializeComponent();
      ((FrameworkElement) this).put_DataContext((object) DialogsViewModel.Instance);
      ExtendedListView2 mainScroll = this.MainScroll;
      WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(((FrameworkElement) mainScroll).add_Loaded), new Action<EventRegistrationToken>(((FrameworkElement) mainScroll).remove_Loaded), new RoutedEventHandler(this.MainScroll_Loaded));
      double actualWidth = ((FrameworkElement) (Window.Current.Content as Frame)).ActualWidth;
    }

    private void MainScroll_Loaded(object sender, RoutedEventArgs e)
    {
      (Window.Current.Content as CustomFrame).HeaderWithMenu.PullToRefresh.TrackListBox(this.MainScroll);
    }

    public DialogsViewModel ConversationsVM
    {
      get => ((FrameworkElement) this).DataContext as DialogsViewModel;
    }

    public void PrepareForViewIfNeeded()
    {
      if (this.ConversationsVM == null || this._isShown)
        return;
      DialogsViewModel.Instance.LoadData(true);
      this._isShown = true;
    }

    private void ItemDialogUC_Tapped(object sender, TappedRoutedEventArgs e)
    {
      VKDialog dataContext = (VKDialog) (sender as FrameworkElement).DataContext;
      if (this.InSelectionMode)
        NavigatorImpl.Instance.NavigateToConversation(dataContext.message.user_id, dataContext.message.chat_id, this.param);
      else
        NavigatorImpl.Instance.NavigateToConversation(dataContext.message.user_id, dataContext.message.chat_id);
    }

    private void ItemDialogUC_Holding(object sender, HoldingRoutedEventArgs e)
    {
    }

    public ExtendedListView2 Scroll => this.MainScroll;

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///UC/ConversationsUC.xaml"), (ComponentResourceLocation) 0);
      this.MainScroll = (ExtendedListView2) ((FrameworkElement) this).FindName("MainScroll");
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void Connect(int connectionId, object target)
    {
      if (connectionId == 1)
      {
        UIElement uiElement1 = (UIElement) target;
        WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement1.add_Tapped), new Action<EventRegistrationToken>(uiElement1.remove_Tapped), new TappedEventHandler(this.ItemDialogUC_Tapped));
        UIElement uiElement2 = (UIElement) target;
        WindowsRuntimeMarshal.AddEventHandler<HoldingEventHandler>(new Func<HoldingEventHandler, EventRegistrationToken>(uiElement2.add_Holding), new Action<EventRegistrationToken>(uiElement2.remove_Holding), new HoldingEventHandler(this.ItemDialogUC_Holding));
      }
      this._contentLoaded = true;
    }
  }
}
