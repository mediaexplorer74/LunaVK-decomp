// Decompiled with JetBrains decompiler
// Type: App1uwp.PageBase
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
using App1uwp.Library;
using App1uwp.Network.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Phone.UI.Input;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

#nullable disable
namespace App1uwp
{
  public class PageBase : Page
  {
    private bool _isLoadedAtLeastOnce;
    public object NavigationParameter;

    private CustomFrame CFrame
    {
      get
      {
        return Window.Current.Content is CustomFrame ? Window.Current.Content as CustomFrame : (CustomFrame) null;
      }
    }

    public PageBase()
    {
      PageBase pageBase1 = this;
      WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(((FrameworkElement) pageBase1).add_Loaded), new Action<EventRegistrationToken>(((FrameworkElement) pageBase1).remove_Loaded), new RoutedEventHandler(this.PageBase_Loaded));
      PageBase pageBase2 = this;
      WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(((FrameworkElement) pageBase2).add_Unloaded), new Action<EventRegistrationToken>(((FrameworkElement) pageBase2).remove_Unloaded), new RoutedEventHandler(this.PageBase_Unloaded));
      if (this.CFrame == null || this.CFrame.SupressTransition)
        return;
      this.SetUpPageAnimation();
    }

    protected void SetUpPageAnimation()
    {
      TransitionCollection transitionCollection = new TransitionCollection();
      PaneThemeTransition paneThemeTransition1 = new PaneThemeTransition();
      paneThemeTransition1.put_Edge((EdgeTransitionLocation) 2);
      PaneThemeTransition paneThemeTransition2 = paneThemeTransition1;
      ((ICollection<Transition>) transitionCollection).Add((Transition) paneThemeTransition2);
      ((UIElement) this).put_Transitions(transitionCollection);
    }

    private void PageBase_Loaded(object sender, RoutedEventArgs e)
    {
      this.CFrame.SupressTransition = false;
      if (!this._isLoadedAtLeastOnce)
        this._isLoadedAtLeastOnce = true;
      if (((UserControl) this).Content is Grid)
      {
        Grid content = ((UserControl) this).Content as Grid;
        ((UIElement) content).put_ManipulationMode((ManipulationModes) 65537);
        WindowsRuntimeMarshal.AddEventHandler<ManipulationDeltaEventHandler>(new Func<ManipulationDeltaEventHandler, EventRegistrationToken>(((UIElement) content).add_ManipulationDelta), new Action<EventRegistrationToken>(((UIElement) content).remove_ManipulationDelta), new ManipulationDeltaEventHandler(this.CFrame.HeaderWithMenu.Rectangle_ManipulationDelta));
        WindowsRuntimeMarshal.AddEventHandler<ManipulationStartedEventHandler>(new Func<ManipulationStartedEventHandler, EventRegistrationToken>(((UIElement) content).add_ManipulationStarted), new Action<EventRegistrationToken>(((UIElement) content).remove_ManipulationStarted), new ManipulationStartedEventHandler(this.CFrame.HeaderWithMenu._menuCallout_ManipulationStarted));
        WindowsRuntimeMarshal.AddEventHandler<ManipulationCompletedEventHandler>(new Func<ManipulationCompletedEventHandler, EventRegistrationToken>(((UIElement) content).add_ManipulationCompleted), new Action<EventRegistrationToken>(((UIElement) content).remove_ManipulationCompleted), new ManipulationCompletedEventHandler(this.CFrame.HeaderWithMenu.Rectangle_ManipulationCompleted));
      }
      Window current = Window.Current;
      WindowsRuntimeMarshal.AddEventHandler<WindowVisibilityChangedEventHandler>(new Func<WindowVisibilityChangedEventHandler, EventRegistrationToken>(current.add_VisibilityChanged), new Action<EventRegistrationToken>(current.remove_VisibilityChanged), new WindowVisibilityChangedEventHandler(this.Current_VisibilityChanged));
    }

    private void Current_VisibilityChanged(object sender, VisibilityChangedEventArgs e)
    {
      this.HandleOnVisibilityChanged(e.Visible);
    }

    private void PageBase_Unloaded(object sender, RoutedEventArgs e)
    {
      if (((UserControl) this).Content is Grid)
      {
        Grid content = ((UserControl) this).Content as Grid;
        WindowsRuntimeMarshal.RemoveEventHandler<ManipulationDeltaEventHandler>(new Action<EventRegistrationToken>(((UIElement) content).remove_ManipulationDelta), new ManipulationDeltaEventHandler(this.CFrame.HeaderWithMenu.Rectangle_ManipulationDelta));
        WindowsRuntimeMarshal.RemoveEventHandler<ManipulationStartedEventHandler>(new Action<EventRegistrationToken>(((UIElement) content).remove_ManipulationStarted), new ManipulationStartedEventHandler(this.CFrame.HeaderWithMenu._menuCallout_ManipulationStarted));
        WindowsRuntimeMarshal.RemoveEventHandler<ManipulationCompletedEventHandler>(new Action<EventRegistrationToken>(((UIElement) content).remove_ManipulationCompleted), new ManipulationCompletedEventHandler(this.CFrame.HeaderWithMenu.Rectangle_ManipulationCompleted));
      }
      WindowsRuntimeMarshal.RemoveEventHandler<WindowVisibilityChangedEventHandler>(new Action<EventRegistrationToken>(Window.Current.remove_VisibilityChanged), new WindowVisibilityChangedEventHandler(this.Current_VisibilityChanged));
    }

    protected virtual void OnNavigatedTo(NavigationEventArgs e)
    {
      this.NavigationParameter = e.Parameter;
      MenuViewModel.Instance.UpdateSelectedItem();
      base.OnNavigatedTo(e);
      this.HandleOnNavigatedTo(e);
      WindowsRuntimeMarshal.AddEventHandler<EventHandler<BackPressedEventArgs>>(new Func<EventHandler<BackPressedEventArgs>, EventRegistrationToken>(HardwareButtons.add_BackPressed), new Action<EventRegistrationToken>(HardwareButtons.remove_BackPressed), new EventHandler<BackPressedEventArgs>(this.HardwareButtons_BackPressed));
    }

    private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
    {
      if (e.Handled)
        return;
      if (this.CFrame.HeaderWithMenu._isMenuOpen)
      {
        this.CFrame.HeaderWithMenu.OpenCloseMenu(new bool?(false));
        e.put_Handled(true);
      }
      else if (((ICollection<UIElement>) ((Panel) (Window.Current.Content as CustomFrame).OverlayGrid).Children).Count > 0)
      {
        e.put_Handled(true);
      }
      else
      {
        CancelEventArgs e1 = new CancelEventArgs();
        this.HandleOnBackKeyPress(e1);
        if (e1.Cancel)
        {
          e.put_Handled(true);
        }
        else
        {
          bool flag = NavigatorImpl.GoBack();
          e.put_Handled(flag);
        }
      }
    }

    protected virtual void OnNavigatingFrom(NavigatingCancelEventArgs e)
    {
      base.OnNavigatingFrom(e);
      this.HandleOnNavigatingFrom(e);
      WindowsRuntimeMarshal.RemoveEventHandler<EventHandler<BackPressedEventArgs>>(new Action<EventRegistrationToken>(HardwareButtons.remove_BackPressed), new EventHandler<BackPressedEventArgs>(this.HardwareButtons_BackPressed));
    }

    protected virtual void HandleOnNavigatedTo(NavigationEventArgs e)
    {
    }

    protected virtual void HandleOnNavigatingFrom(NavigatingCancelEventArgs e)
    {
    }

    protected virtual void HandleOnBackKeyPress(CancelEventArgs e)
    {
    }

    protected virtual void HandleOnVisibilityChanged(bool is_visible)
    {
    }
  }
}
