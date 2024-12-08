// Decompiled with JetBrains decompiler
// Type: App1uwp.UC.MenuItemUC
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
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
  public sealed class MenuItemUC : UserControl, IComponentConnector
  {
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Grid root;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private VisualStateGroup WindowStates;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private VisualStateGroup ButtonStates;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private VisualState NormalButton;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private VisualState PressedButton;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private VisualState NarrowState;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private VisualState WideState;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Border brd;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private IconUC fIcon;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBlock tTitle;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBlock MiniCounter;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public MenuItemUC()
    {
      this.InitializeComponent();
      if (Settings.Instance != null && !Settings.Instance.MenuDivider)
        ((FrameworkElement) this.root).put_Margin(new Thickness());
      MenuItemUC menuItemUc1 = this;
      WindowsRuntimeMarshal.AddEventHandler<SizeChangedEventHandler>(new Func<SizeChangedEventHandler, EventRegistrationToken>(((FrameworkElement) menuItemUc1).add_SizeChanged), new Action<EventRegistrationToken>(((FrameworkElement) menuItemUc1).remove_SizeChanged), new SizeChangedEventHandler(this.MenuItemUC_SizeChanged));
      if (Window.Current.Content is CustomFrame)
        (Window.Current.Content as CustomFrame).MenuStateChanged += new EventHandler<CustomFrame.MenuStates>(this.MenuStateChanged);
      MenuItemUC menuItemUc2 = this;
      WindowsRuntimeMarshal.AddEventHandler<PointerEventHandler>(new Func<PointerEventHandler, EventRegistrationToken>(((UIElement) menuItemUc2).add_PointerEntered), new Action<EventRegistrationToken>(((UIElement) menuItemUc2).remove_PointerEntered), new PointerEventHandler(this.MenuItemUC_PointerEntered));
      MenuItemUC menuItemUc3 = this;
      WindowsRuntimeMarshal.AddEventHandler<PointerEventHandler>(new Func<PointerEventHandler, EventRegistrationToken>(((UIElement) menuItemUc3).add_PointerExited), new Action<EventRegistrationToken>(((UIElement) menuItemUc3).remove_PointerExited), new PointerEventHandler(this.MenuItemUC_PointerExited));
    }

    private void MenuItemUC_PointerExited(object sender, PointerRoutedEventArgs e)
    {
      VisualStateManager.GoToState((Control) this, "NormalButton", true);
    }

    private void MenuItemUC_PointerEntered(object sender, PointerRoutedEventArgs e)
    {
      VisualStateManager.GoToState((Control) this, "PressedButton", true);
    }

    private void MenuItemUC_SizeChanged(object sender, SizeChangedEventArgs e)
    {
    }

    private void MenuStateChanged(object sender, CustomFrame.MenuStates e)
    {
      if (e == CustomFrame.MenuStates.StateWide)
      {
        ((UIElement) this.brd).put_Visibility((Visibility) 0);
        ((UIElement) this.MiniCounter).put_Visibility((Visibility) 1);
      }
      else if (e == CustomFrame.MenuStates.StateNarrow)
      {
        ((UIElement) this.brd).put_Visibility((Visibility) 1);
        ((UIElement) this.MiniCounter).put_Visibility((Visibility) 0);
      }
      else
      {
        ((UIElement) this.brd).put_Visibility((Visibility) 0);
        ((UIElement) this.MiniCounter).put_Visibility((Visibility) 1);
      }
    }

    public string Icon
    {
      get => this.fIcon.Glyph;
      set => this.fIcon.put_Glyph(value);
    }

    public string Title
    {
      get => this.tTitle.Text;
      set => this.tTitle.put_Text(value);
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///UC/MenuItemUC.xaml"), (ComponentResourceLocation) 0);
      this.root = (Grid) ((FrameworkElement) this).FindName("root");
      this.WindowStates = (VisualStateGroup) ((FrameworkElement) this).FindName("WindowStates");
      this.ButtonStates = (VisualStateGroup) ((FrameworkElement) this).FindName("ButtonStates");
      this.NormalButton = (VisualState) ((FrameworkElement) this).FindName("NormalButton");
      this.PressedButton = (VisualState) ((FrameworkElement) this).FindName("PressedButton");
      this.NarrowState = (VisualState) ((FrameworkElement) this).FindName("NarrowState");
      this.WideState = (VisualState) ((FrameworkElement) this).FindName("WideState");
      this.brd = (Border) ((FrameworkElement) this).FindName("brd");
      this.fIcon = (IconUC) ((FrameworkElement) this).FindName("fIcon");
      this.tTitle = (TextBlock) ((FrameworkElement) this).FindName("tTitle");
      this.MiniCounter = (TextBlock) ((FrameworkElement) this).FindName("MiniCounter");
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void Connect(int connectionId, object target) => this._contentLoaded = true;
  }
}
