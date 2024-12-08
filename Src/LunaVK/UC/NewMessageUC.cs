// Decompiled with JetBrains decompiler
// Type: App1uwp.UC.NewMessageUC
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Emoji;
using App1uwp.Framework;
using App1uwp.Library;
using App1uwp.Network.DataObjects;
using App1uwp.Network.Enums;
using App1uwp.Network.ViewModels;
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
using Windows.UI.Xaml.Shapes;

#nullable disable
namespace App1uwp.UC
{
  public sealed class NewMessageUC : UserControl, IComponentConnector
  {
    private bool IsFocused;
    private bool _panelInitialized;
    private bool _isEmojiOpened;
    private double _height = 347.0;
    private double _height_keyboard;
    private double scale = 0.5;
    private double m = 0.05;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Grid root;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private ListView mentionPicker;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Ellipse ellipseVolume;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Ellipse ellipseCancel;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private SwipeThroughControl panelControl;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private ScaleTransform scaleCancel;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private ScaleTransform scaleVolume;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Grid _borderAttach;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private ScrollViewer scroll;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Border _borderSend;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Border _borderVoice;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private IconUC _icon;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Ellipse ellipseHasStickersUpdates;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBox textBoxPost;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public NewMessageUC()
    {
      this.InitializeComponent();
      if ((Window.Current.Content as CustomFrame).IsDevicePhone)
      {
        TextBox textBoxPost1 = this.textBoxPost;
        WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(((UIElement) textBoxPost1).add_GotFocus), new Action<EventRegistrationToken>(((UIElement) textBoxPost1).remove_GotFocus), new RoutedEventHandler(this.TextBoxPost_GotFocus));
        TextBox textBoxPost2 = this.textBoxPost;
        WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(((UIElement) textBoxPost2).add_LostFocus), new Action<EventRegistrationToken>(((UIElement) textBoxPost2).remove_LostFocus), new RoutedEventHandler(this.TextBoxPost_LostFocus));
      }
      Border borderSend = this._borderSend;
      WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(((UIElement) borderSend).add_Tapped), new Action<EventRegistrationToken>(((UIElement) borderSend).remove_Tapped), new TappedEventHandler(this._borderSend_Tapped));
      InputPane forCurrentView1 = InputPane.GetForCurrentView();
      // ISSUE: method pointer
      WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<InputPane, InputPaneVisibilityEventArgs>>(new Func<TypedEventHandler<InputPane, InputPaneVisibilityEventArgs>, EventRegistrationToken>(forCurrentView1.add_Showing), new Action<EventRegistrationToken>(forCurrentView1.remove_Showing), new TypedEventHandler<InputPane, InputPaneVisibilityEventArgs>((object) this, __methodptr(Keyboard_Showing)));
      InputPane forCurrentView2 = InputPane.GetForCurrentView();
      // ISSUE: method pointer
      WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<InputPane, InputPaneVisibilityEventArgs>>(new Func<TypedEventHandler<InputPane, InputPaneVisibilityEventArgs>, EventRegistrationToken>(forCurrentView2.add_Hiding), new Action<EventRegistrationToken>(forCurrentView2.remove_Hiding), new TypedEventHandler<InputPane, InputPaneVisibilityEventArgs>((object) this, __methodptr(Keyboard_Hiding)));
      if (Window.Current.Content is Frame)
      {
        Frame content = Window.Current.Content as Frame;
        WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(((FrameworkElement) content).add_Loaded), new Action<EventRegistrationToken>(((FrameworkElement) content).remove_Loaded), new RoutedEventHandler(this.NewMessageUC_Loaded));
      }
      bool flag = false;
      switch (Settings.Instance.BackgroundType)
      {
        case Threelen.On:
          flag = true;
          break;
        case Threelen.Third:
          if (((FrameworkElement) this).RequestedTheme == 2)
          {
            flag = true;
            break;
          }
          break;
      }
      if (!flag)
        return;
      ((Panel) this.root).put_Background((Brush) new SolidColorBrush(Colors.Black));
    }

    private void NewMessageUC_Loaded(object sender, RoutedEventArgs e)
    {
      ((FrameworkElement) this.scroll).put_MaxHeight(((FrameworkElement) (Window.Current.Content as Frame)).ActualHeight / 3.0);
      WindowsRuntimeMarshal.RemoveEventHandler<RoutedEventHandler>(new Action<EventRegistrationToken>(((FrameworkElement) (Window.Current.Content as Frame)).remove_Loaded), new RoutedEventHandler(this.NewMessageUC_Loaded));
    }

    public bool HidePanel()
    {
      if (!this._isEmojiOpened)
        return false;
      this._isEmojiOpened = false;
      this.UpdateVisibilityState();
      return true;
    }

    private void Keyboard_Hiding(InputPane sender, InputPaneVisibilityEventArgs args)
    {
      if (!this.IsFocused)
        return;
      this.IsFocused = false;
      this.UpdateVisibilityState();
      this.IsFocused = true;
    }

    private void Keyboard_Showing(InputPane sender, InputPaneVisibilityEventArgs args)
    {
      if (this._height_keyboard < sender.OccludedRect.Height)
        this._height_keyboard = sender.OccludedRect.Height;
      args.put_EnsuredFocusedElementInView(true);
      this._height = this._height_keyboard;
      this.UpdateVisibilityState();
    }

    private void Smiles_Tapped(object sender, TappedRoutedEventArgs e)
    {
      this._isEmojiOpened = !this._isEmojiOpened;
      this.InitPanel();
      this.UpdateVisibilityState();
    }

    private void UpdateVisibilityState()
    {
      bool flag = this.IsFocused || this._isEmojiOpened;
      ((FrameworkElement) this.panelControl).put_Height(flag ? this._height : 0.0);
      if (!string.IsNullOrEmpty(this.textBoxPost.Text))
        flag = true;
      if (flag)
      {
        ((UIElement) this._borderSend).put_Visibility((Visibility) 0);
        ((UIElement) this._borderVoice).put_Visibility((Visibility) 1);
      }
      else
      {
        ((UIElement) this._borderSend).put_Visibility((Visibility) 1);
        ((UIElement) this._borderVoice).put_Visibility((Visibility) 0);
      }
    }

    private void TextBoxPost_LostFocus(object sender, RoutedEventArgs e)
    {
      this.IsFocused = false;
      this.UpdateVisibilityState();
    }

    private void TextBoxPost_GotFocus(object sender, RoutedEventArgs e)
    {
      this.IsFocused = true;
      this._isEmojiOpened = false;
      this.UpdateVisibilityState();
    }

    public SwipeThroughControl PanelControl => this.panelControl;

    private void _borderSend_Tapped(object sender, TappedRoutedEventArgs e)
    {
      this.ForceFocusIfNeeded();
    }

    public void ForceFocusIfNeeded()
    {
      if (!this.IsFocused)
        return;
      ((Control) this.textBoxPost).Focus((FocusState) 2);
    }

    public TextBox TextBoxNewComment => this.textBoxPost;

    public void ActivateSendButton(bool status)
    {
      SolidColorBrush resource1 = (SolidColorBrush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "AccentBrushHigh"];
      SolidColorBrush resource2 = (SolidColorBrush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "TextColorContent"];
      this._borderSend.put_Background(status ? (Brush) resource1 : (Brush) new SolidColorBrush(Colors.Transparent));
      ((IconElement) this._icon).put_Foreground(status ? (Brush) new SolidColorBrush(Colors.White) : (Brush) resource2);
      if (status)
      {
        ((UIElement) this._borderSend).put_Visibility((Visibility) 0);
        ((UIElement) this._borderVoice).put_Visibility((Visibility) 1);
      }
      else
      {
        if (this.IsFocused)
          return;
        ((UIElement) this._borderSend).put_Visibility((Visibility) 1);
        ((UIElement) this._borderVoice).put_Visibility((Visibility) 0);
      }
    }

    public Border BorderSend => this._borderSend;

    public Grid BorderAttach => this._borderAttach;

    private async void InitPanel()
    {
      if (this._panelInitialized)
        return;
      List<StockItem> temp = await StickersSettings.Instance.GetStickers(new List<StoreProductFilter>()
      {
        StoreProductFilter.Active
      });
      this.panelControl.Items = new ObservableCollection<object>((IEnumerable<object>) temp);
      this._panelInitialized = true;
    }

    private void Delete_Tapped(object sender, TappedRoutedEventArgs e)
    {
      (((FrameworkElement) this).DataContext as DialogHistoryViewModel).Attachments.Remove((sender as FrameworkElement).DataContext as IOutboundAttachment);
    }

    private void _borderVoice_ManipulationCompleted(
      object sender,
      ManipulationCompletedRoutedEventArgs e)
    {
    }

    private void _borderVoice_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
    {
    }

    private void _borderVoice_PointerEntered(object sender, PointerRoutedEventArgs e)
    {
    }

    private void _borderVoice_PointerReleased(object sender, PointerRoutedEventArgs e)
    {
    }

    public ListView MentionPicker => this.mentionPicker;

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///UC/NewMessageUC.xaml"), (ComponentResourceLocation) 0);
      this.root = (Grid) ((FrameworkElement) this).FindName("root");
      this.mentionPicker = (ListView) ((FrameworkElement) this).FindName("mentionPicker");
      this.ellipseVolume = (Ellipse) ((FrameworkElement) this).FindName("ellipseVolume");
      this.ellipseCancel = (Ellipse) ((FrameworkElement) this).FindName("ellipseCancel");
      this.panelControl = (SwipeThroughControl) ((FrameworkElement) this).FindName("panelControl");
      this.scaleCancel = (ScaleTransform) ((FrameworkElement) this).FindName("scaleCancel");
      this.scaleVolume = (ScaleTransform) ((FrameworkElement) this).FindName("scaleVolume");
      this._borderAttach = (Grid) ((FrameworkElement) this).FindName("_borderAttach");
      this.scroll = (ScrollViewer) ((FrameworkElement) this).FindName("scroll");
      this._borderSend = (Border) ((FrameworkElement) this).FindName("_borderSend");
      this._borderVoice = (Border) ((FrameworkElement) this).FindName("_borderVoice");
      this._icon = (IconUC) ((FrameworkElement) this).FindName("_icon");
      this.ellipseHasStickersUpdates = (Ellipse) ((FrameworkElement) this).FindName("ellipseHasStickersUpdates");
      this.textBoxPost = (TextBox) ((FrameworkElement) this).FindName("textBoxPost");
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          UIElement uiElement1 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement1.add_Tapped), new Action<EventRegistrationToken>(uiElement1.remove_Tapped), new TappedEventHandler(this.Smiles_Tapped));
          break;
        case 2:
          UIElement uiElement2 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<ManipulationCompletedEventHandler>(new Func<ManipulationCompletedEventHandler, EventRegistrationToken>(uiElement2.add_ManipulationCompleted), new Action<EventRegistrationToken>(uiElement2.remove_ManipulationCompleted), new ManipulationCompletedEventHandler(this._borderVoice_ManipulationCompleted));
          UIElement uiElement3 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<ManipulationDeltaEventHandler>(new Func<ManipulationDeltaEventHandler, EventRegistrationToken>(uiElement3.add_ManipulationDelta), new Action<EventRegistrationToken>(uiElement3.remove_ManipulationDelta), new ManipulationDeltaEventHandler(this._borderVoice_ManipulationDelta));
          UIElement uiElement4 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<PointerEventHandler>(new Func<PointerEventHandler, EventRegistrationToken>(uiElement4.add_PointerEntered), new Action<EventRegistrationToken>(uiElement4.remove_PointerEntered), new PointerEventHandler(this._borderVoice_PointerEntered));
          UIElement uiElement5 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<PointerEventHandler>(new Func<PointerEventHandler, EventRegistrationToken>(uiElement5.add_PointerReleased), new Action<EventRegistrationToken>(uiElement5.remove_PointerReleased), new PointerEventHandler(this._borderVoice_PointerReleased));
          break;
        case 3:
          UIElement uiElement6 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement6.add_Tapped), new Action<EventRegistrationToken>(uiElement6.remove_Tapped), new TappedEventHandler(this.Delete_Tapped));
          break;
        case 4:
          UIElement uiElement7 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement7.add_Tapped), new Action<EventRegistrationToken>(uiElement7.remove_Tapped), new TappedEventHandler(this.Delete_Tapped));
          break;
        case 5:
          UIElement uiElement8 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement8.add_Tapped), new Action<EventRegistrationToken>(uiElement8.remove_Tapped), new TappedEventHandler(this.Delete_Tapped));
          break;
      }
      this._contentLoaded = true;
    }
  }
}
