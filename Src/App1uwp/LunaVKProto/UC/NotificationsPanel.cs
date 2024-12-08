// Decompiled with JetBrains decompiler
// Type: App1uwp.UC.NotificationsPanel
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Utils;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

#nullable disable
namespace App1uwp.UC
{
  public sealed class NotificationsPanel : UserControl, IComponentConnector
  {
    private bool initialized;
    private bool in_manipulation;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private StackPanel main_content;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Border GripperBarHorizonta;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public NotificationsPanel()
    {
      this.InitializeComponent();
      (((UIElement) this).RenderTransform as TranslateTransform).put_Y(-(((FrameworkElement) this.main_content).Margin.Top + ((FrameworkElement) this.GripperBarHorizonta).Height));
      NotificationsPanel notificationsPanel1 = this;
      WindowsRuntimeMarshal.AddEventHandler<SizeChangedEventHandler>(new Func<SizeChangedEventHandler, EventRegistrationToken>(((FrameworkElement) notificationsPanel1).add_SizeChanged), new Action<EventRegistrationToken>(((FrameworkElement) notificationsPanel1).remove_SizeChanged), new SizeChangedEventHandler(this.NotificationsPanel_SizeChanged));
      NotificationsPanel notificationsPanel2 = this;
      WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(((FrameworkElement) notificationsPanel2).add_Loaded), new Action<EventRegistrationToken>(((FrameworkElement) notificationsPanel2).remove_Loaded), new RoutedEventHandler(this.NotificationsPanel_Loaded));
    }

    private void NotificationsPanel_Loaded(object sender, RoutedEventArgs e)
    {
      this.initialized = true;
    }

    private void NotificationsPanel_SizeChanged(object sender, SizeChangedEventArgs e)
    {
      if (!this.initialized)
        return;
      bool flag1 = true;
      if (e.NewSize.Height > ((FrameworkElement) this.GripperBarHorizonta).Height + ((FrameworkElement) this.main_content).Margin.Top)
      {
        TranslateTransform renderTransform = ((UIElement) this).RenderTransform as TranslateTransform;
        double y = renderTransform.Y;
        double to = flag1 ? 0.0 : -e.NewSize.Height;
        ExponentialEase easing = new ExponentialEase();
        easing.put_Exponent(6.0);
        ((DependencyObject) renderTransform).Animate(y, to, "Y", 250, easing: (EasingFunctionBase) easing);
      }
      else
      {
        bool flag2 = false;
        TranslateTransform renderTransform = ((UIElement) this).RenderTransform as TranslateTransform;
        double y = renderTransform.Y;
        double to = flag2 ? 0.0 : -100.0;
        ExponentialEase easing = new ExponentialEase();
        easing.put_Exponent(6.0);
        ((DependencyObject) renderTransform).Animate(y, to, "Y", 250, easing: (EasingFunctionBase) easing, completed: (Action) (() => { }));
      }
    }

    public void AddAndShowNotification(
      string image_src,
      string title,
      string content,
      Action tapCallback)
    {
      AppNotification2 appNotification2_1 = (AppNotification2) ((IEnumerable<UIElement>) ((Panel) this.main_content).Children).FirstOrDefault<UIElement>((Func<UIElement, bool>) (element => (element as AppNotification2).Title == title));
      if (appNotification2_1 == null)
      {
        AppNotification2 appNotification2_2 = new AppNotification2(image_src, title, content, tapCallback);
        appNotification2_2.TimeToDelete = (Action<UserControl>) (element =>
        {
          if (this.in_manipulation)
            return;
          ((ICollection<UIElement>) ((Panel) this.main_content).Children).Remove((UIElement) element);
        });
        if (((ICollection<UIElement>) ((Panel) this.main_content).Children).Count >= 3)
          ((IList<UIElement>) ((Panel) this.main_content).Children).RemoveAt(0);
        ((ICollection<UIElement>) ((Panel) this.main_content).Children).Add((UIElement) appNotification2_2);
      }
      else
        appNotification2_1.AddContent(content);
    }

    private void GripperBarHorizonta_ManipulationStarted(
      object sender,
      ManipulationStartedRoutedEventArgs e)
    {
      this.in_manipulation = true;
    }

    private void GripperBarHorizonta_ManipulationDelta(
      object sender,
      ManipulationDeltaRoutedEventArgs e)
    {
      if (!this.in_manipulation)
        this.in_manipulation = true;
      TranslateTransform renderTransform = ((UIElement) this).RenderTransform as TranslateTransform;
      if (renderTransform.Y + e.Delta.Translation.Y > 0.0)
      {
        renderTransform.put_Y(0.0);
      }
      else
      {
        TranslateTransform translateTransform = renderTransform;
        translateTransform.put_Y(translateTransform.Y + e.Delta.Translation.Y);
      }
    }

    private void GripperBarHorizonta_ManipulationCompleted(
      object sender,
      ManipulationCompletedRoutedEventArgs e)
    {
      TranslateTransform renderTransform = ((UIElement) this).RenderTransform as TranslateTransform;
      double y = renderTransform.Y;
      ExponentialEase easing = new ExponentialEase();
      easing.put_Exponent(6.0);
      if (renderTransform.Y < -(((FrameworkElement) this).ActualHeight / 2.0))
      {
        double to = -200.0;
        ((ICollection<UIElement>) ((Panel) this.main_content).Children).Clear();
        ((DependencyObject) renderTransform).Animate(y, to, "Y", 250, easing: (EasingFunctionBase) easing, completed: (Action) (() => this.in_manipulation = false));
      }
      else
      {
        double to = 0.0;
        ((DependencyObject) renderTransform).Animate(y, to, "Y", 250, easing: (EasingFunctionBase) easing, completed: (Action) (() => this.in_manipulation = false));
      }
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///UC/NotificationsPanel.xaml"), (ComponentResourceLocation) 0);
      this.main_content = (StackPanel) ((FrameworkElement) this).FindName("main_content");
      this.GripperBarHorizonta = (Border) ((FrameworkElement) this).FindName("GripperBarHorizonta");
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void Connect(int connectionId, object target)
    {
      if (connectionId == 1)
      {
        UIElement uiElement1 = (UIElement) target;
        WindowsRuntimeMarshal.AddEventHandler<ManipulationStartedEventHandler>(new Func<ManipulationStartedEventHandler, EventRegistrationToken>(uiElement1.add_ManipulationStarted), new Action<EventRegistrationToken>(uiElement1.remove_ManipulationStarted), new ManipulationStartedEventHandler(this.GripperBarHorizonta_ManipulationStarted));
        UIElement uiElement2 = (UIElement) target;
        WindowsRuntimeMarshal.AddEventHandler<ManipulationDeltaEventHandler>(new Func<ManipulationDeltaEventHandler, EventRegistrationToken>(uiElement2.add_ManipulationDelta), new Action<EventRegistrationToken>(uiElement2.remove_ManipulationDelta), new ManipulationDeltaEventHandler(this.GripperBarHorizonta_ManipulationDelta));
        UIElement uiElement3 = (UIElement) target;
        WindowsRuntimeMarshal.AddEventHandler<ManipulationCompletedEventHandler>(new Func<ManipulationCompletedEventHandler, EventRegistrationToken>(uiElement3.add_ManipulationCompleted), new Action<EventRegistrationToken>(uiElement3.remove_ManipulationCompleted), new ManipulationCompletedEventHandler(this.GripperBarHorizonta_ManipulationCompleted));
      }
      this._contentLoaded = true;
    }
  }
}
