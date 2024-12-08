// Decompiled with JetBrains decompiler
// Type: App1uwp.Framework.CustomFrame
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Library;
using App1uwp.UC;
using App1uwp.Utils;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Navigation;

#nullable disable
namespace App1uwp.Framework
{
  public sealed class CustomFrame : Frame
  {
    private NotificationsPanel _notificationsPanel;
    private HeaderWithMenuUC _headerWithMenuUC;
    private Grid _overlay;
    public bool SupressTransition = true;
    public bool _shouldResetStack;
    public bool _shouldDeleteLastPageFromStack;
    private CustomFrame.MenuStates curMenuState;
    public static readonly DependencyProperty HeaderProperty = DependencyProperty.RegisterAttached("HeaderContent", typeof (FrameworkElement), typeof (CustomFrame), new PropertyMetadata((object) null, new PropertyChangedCallback(CustomFrame.OnHeaderChanged)));
    public ApplicationViewOrientation CurrentOrientation;
    public EventHandler<ApplicationViewOrientation> OrientationChanged;

    public CustomFrame() => ((Control) this).put_DefaultStyleKey((object) typeof (CustomFrame));

    public event EventHandler<CustomFrame.MenuStates> MenuStateChanged;

    public static FrameworkElement GetHeaderContent(DependencyObject obj)
    {
      return (FrameworkElement) obj.GetValue(CustomFrame.HeaderProperty);
    }

    public static void SetHeaderContent(DependencyObject obj, FrameworkElement value)
    {
      obj.SetValue(CustomFrame.HeaderProperty, (object) value);
    }

    public CommandBar CommandBar { get; private set; }

    public NotificationsPanel NotificationsPanel => this._notificationsPanel;

    public HeaderWithMenuUC HeaderWithMenu => this._headerWithMenuUC;

    public CustomFrame.MenuStates MenuState => this.curMenuState;

    public Grid OverlayGrid => this._overlay;

    public bool IsDevicePhone { get; private set; }

    private static void OnHeaderChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
      FrameworkElement newValue = (FrameworkElement) e.NewValue;
      obj.GetFirstAncestorOfType<CustomFrame>()?.UpdateHeaderContent(newValue);
    }

    private static void OnOptionsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
      FrameworkElement newValue = (FrameworkElement) e.NewValue;
      obj.GetFirstAncestorOfType<CustomFrame>();
    }

    public async void ShowStatusBar(bool status)
    {
      StatusBar bar = StatusBar.GetForCurrentView();
      if (status)
        await bar.ShowAsync();
      else
        await bar.HideAsync();
    }

    protected virtual void OnApplyTemplate()
    {
      ((FrameworkElement) this).OnApplyTemplate();
      this._notificationsPanel = ((Control) this).GetTemplateChild("NotificationsPanel") as NotificationsPanel;
      this._headerWithMenuUC = ((Control) this).GetTemplateChild("HeaderWithMenu") as HeaderWithMenuUC;
      this._overlay = ((Control) this).GetTemplateChild("Overlay") as Grid;
      this.CommandBar = ((Control) this).GetTemplateChild("CommandBarPanel") as CommandBar;
      if (((ContentControl) this).Content != null)
      {
        this.CommandBar.SecondaryCommands.Clear();
        this.CommandBar.PrimaryCommands.Clear();
        this.UpdateHeaderContent(CustomFrame.GetHeaderContent(((ContentControl) this).Content as DependencyObject));
      }
      CustomFrame customFrame = this;
      WindowsRuntimeMarshal.AddEventHandler<NavigatedEventHandler>(new Func<NavigatedEventHandler, EventRegistrationToken>(((Frame) customFrame).add_Navigated), new Action<EventRegistrationToken>(((Frame) customFrame).remove_Navigated), (NavigatedEventHandler) ((s, e) =>
      {
        this.CommandBar.SecondaryCommands.Clear();
        this.CommandBar.PrimaryCommands.Clear();
        this.UpdateHeaderContent(CustomFrame.GetHeaderContent(((ContentControl) this).Content as DependencyObject));
        if (this._shouldResetStack)
        {
          NavigatorImpl.Instance.ClearBackStack();
          this._shouldResetStack = false;
        }
        if (this._shouldDeleteLastPageFromStack)
        {
          (Window.Current.Content as Frame).BackStack.RemoveAt((Window.Current.Content as Frame).BackStackDepth - 1);
          this._shouldDeleteLastPageFromStack = false;
        }
        this.HeaderWithMenu.OptionsMenu.Clear();
        ((ICollection<UIElement>) ((Panel) this.HeaderWithMenu.SubContent).Children).Clear();
        this.HeaderWithMenu.HideSandwitchButton = false;
      }));
      Frame frame = (Frame) this;
      WindowsRuntimeMarshal.AddEventHandler<SizeChangedEventHandler>(new Func<SizeChangedEventHandler, EventRegistrationToken>(((FrameworkElement) frame).add_SizeChanged), new Action<EventRegistrationToken>(((FrameworkElement) frame).remove_SizeChanged), new SizeChangedEventHandler(this.CustomFrame_SizeChanged));
      this.IsDevicePhone = new EasClientDeviceInformation().OperatingSystem.Contains("Phone");
    }

    private void CustomFrame_SizeChanged(object sender, SizeChangedEventArgs e)
    {
      if (e.NewSize.Width < 505.0)
        this.PublishMenuState(CustomFrame.MenuStates.StateCollapsed);
      else if (e.NewSize.Width > 505.0 && e.NewSize.Width < Constants.MAX_CONTENT_WIDTH + Constants.MENU_WIDE_WIDTH)
        this.PublishMenuState(CustomFrame.MenuStates.StateNarrow);
      else if (e.NewSize.Width > Constants.MAX_CONTENT_WIDTH + Constants.MENU_WIDE_WIDTH)
        this.PublishMenuState(CustomFrame.MenuStates.StateWide);
      ApplicationView forCurrentView = ApplicationView.GetForCurrentView();
      if (this.OrientationChanged != null)
        this.OrientationChanged(sender, forCurrentView.Orientation);
      this.CurrentOrientation = forCurrentView.Orientation;
      if (this.CurrentOrientation == (ApplicationViewOrientation)1)
        this.ShowStatusBar(true);
      else
        this.ShowStatusBar(false);
    }

    private void PublishMenuState(CustomFrame.MenuStates newState)
    {
      if (this.curMenuState == newState)
        return;
      if (this.MenuStateChanged != null)
        this.MenuStateChanged((object) this, newState);
      this.curMenuState = newState;
    }

    private void UpdateHeaderContent(FrameworkElement bar)
    {
      ((ICollection<UIElement>) ((Panel) this.HeaderWithMenu.MainContent).Children).Clear();
      if (bar == null)
        return;
      FrameworkElement content = ((ContentControl) this).Content as FrameworkElement;
      Binding binding1 = new Binding();
      binding1.Mode = ((BindingMode) 3);
      binding1.Path = (new PropertyPath("DataContext"));
      binding1.Source = ((object) content);
      Binding binding2 = binding1;
      bar.SetBinding(FrameworkElement.DataContextProperty, (BindingBase) binding2);
      ((ICollection<UIElement>) ((Panel) this.HeaderWithMenu.MainContent).Children).Add((UIElement) bar);
    }

    public enum MenuStates : byte
    {
      StateWide,
      StateNarrow,
      StateCollapsed,
    }
  }
}
