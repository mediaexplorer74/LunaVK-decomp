// Decompiled with JetBrains decompiler
// Type: App1uwp.Framework.CommandBar
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

#nullable disable
namespace App1uwp.Framework
{
  public class CommandBar : ContentControl
  {
    public static readonly DependencyProperty PrimaryCommandsProperty = DependencyProperty.Register(nameof (PrimaryCommands), typeof (ObservableCollection<CommandBarButton>), typeof (CommandBar), new PropertyMetadata((object) new ObservableCollection<CommandBarButton>()));
    public static readonly DependencyProperty SecondaryCommandsProperty = DependencyProperty.Register(nameof (SecondaryCommands), typeof (ObservableCollection<CommandBarButton>), typeof (CommandBar), new PropertyMetadata((object) new ObservableCollection<CommandBarButton>()));
    private bool _isMenuOpened;
    private bool _isMenuHiden;
    protected FrameworkElement menuButtonBorder;
    protected ItemsControl buttonsControl;
    protected FrameworkElement _Rect;

    public CommandBar()
    {
      ((Control) this).put_DefaultStyleKey((object) typeof (CommandBar));
      this.PrimaryCommands.CollectionChanged += new NotifyCollectionChangedEventHandler(this.PrimaryCommands_CollectionChanged);
      this.SecondaryCommands.CollectionChanged += new NotifyCollectionChangedEventHandler(this.SecondaryCommands_CollectionChanged);
      this.PrimaryCommands.Clear();
      this.SecondaryCommands.Clear();
    }

    public ObservableCollection<CommandBarButton> PrimaryCommands
    {
      get
      {
        return (ObservableCollection<CommandBarButton>) ((DependencyObject) this).GetValue(CommandBar.PrimaryCommandsProperty);
      }
      set => ((DependencyObject) this).SetValue(CommandBar.PrimaryCommandsProperty, (object) value);
    }

    public ObservableCollection<CommandBarButton> SecondaryCommands
    {
      get
      {
        return (ObservableCollection<CommandBarButton>) ((DependencyObject) this).GetValue(CommandBar.SecondaryCommandsProperty);
      }
      set
      {
        ((DependencyObject) this).SetValue(CommandBar.SecondaryCommandsProperty, (object) value);
      }
    }

    public bool IsMenuOpened
    {
      get => this._isMenuOpened;
      private set
      {
        this._isMenuOpened = value;
        if (this.PrimaryCommands.Count == 0 && this.SecondaryCommands.Count == 0)
          VisualStateManager.GoToState((Control) this, "Empty", true);
        else
          VisualStateManager.GoToState((Control) this, value ? "MenuOpened" : (this.PrimaryCommands.Count != 0 || this.SecondaryCommands.Count <= 0 ? "Normal" : "Minimized"), true);
      }
    }

    public bool IsMenuHiden
    {
      get => this._isMenuHiden;
      private set
      {
        this._isMenuHiden = value;
        if (value)
          VisualStateManager.GoToState((Control) this, "Empty", true);
        else
          this.IsMenuOpened = this.IsMenuOpened;
      }
    }

    protected virtual void OnApplyTemplate()
    {
      ((FrameworkElement) this).put_Width(((FrameworkElement) Window.Current.Content).ActualWidth);
      ((FrameworkElement) this).OnApplyTemplate();
      this.menuButtonBorder = ((Control) this).GetTemplateChild("MenuButtonBorder") as FrameworkElement;
      this.buttonsControl = ((Control) this).GetTemplateChild("ButtonsControl") as ItemsControl;
      if (!Settings.Instance.CmdBarDivider)
      {
        this._Rect = ((Control) this).GetTemplateChild("_Rect") as FrameworkElement;
        ((UIElement) this._Rect).put_Visibility((Visibility) 1);
      }
      FrameworkElement menuButtonBorder1 = this.menuButtonBorder;
      WindowsRuntimeMarshal.AddEventHandler<PointerEventHandler>(new Func<PointerEventHandler, EventRegistrationToken>(((UIElement) menuButtonBorder1).add_PointerEntered), new Action<EventRegistrationToken>(((UIElement) menuButtonBorder1).remove_PointerEntered), (PointerEventHandler) ((param0, param1) => VisualStateManager.GoToState((Control) this, "PressedButton", true)));
      FrameworkElement menuButtonBorder2 = this.menuButtonBorder;
      WindowsRuntimeMarshal.AddEventHandler<PointerEventHandler>(new Func<PointerEventHandler, EventRegistrationToken>(((UIElement) menuButtonBorder2).add_PointerExited), new Action<EventRegistrationToken>(((UIElement) menuButtonBorder2).remove_PointerExited), (PointerEventHandler) ((param0, param1) => VisualStateManager.GoToState((Control) this, "NormalButton", true)));
      FrameworkElement content = (FrameworkElement) Window.Current.Content;
      WindowsRuntimeMarshal.AddEventHandler<SizeChangedEventHandler>(new Func<SizeChangedEventHandler, EventRegistrationToken>(content.add_SizeChanged), new Action<EventRegistrationToken>(content.remove_SizeChanged), (SizeChangedEventHandler) ((s, e) => ((FrameworkElement) this).put_Width(e.NewSize.Width)));
      FrameworkElement menuButtonBorder3 = this.menuButtonBorder;
      WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(((UIElement) menuButtonBorder3).add_Tapped), new Action<EventRegistrationToken>(((UIElement) menuButtonBorder3).remove_Tapped), (TappedEventHandler) ((s, e) =>
      {
        this.IsMenuOpened = !this.IsMenuOpened;
        e.put_Handled(true);
      }));
      VisualStateManager.GoToState((Control) this, "Empty", false);
    }

    private void temp4_Tapped(object sender, TappedRoutedEventArgs e) => e.put_Handled(true);

    private void SecondaryCommands_CollectionChanged(
      object sender,
      NotifyCollectionChangedEventArgs e)
    {
      if (e.NewItems == null || this.PrimaryCommands.Count != 0)
        return;
      VisualStateManager.GoToState((Control) this, "Minimized", false);
    }

    private void PrimaryCommands_CollectionChanged(
      object sender,
      NotifyCollectionChangedEventArgs e)
    {
      if (e.NewItems == null)
        VisualStateManager.GoToState((Control) this, "Empty", false);
      else
        VisualStateManager.GoToState((Control) this, "Normal", true);
    }
  }
}
