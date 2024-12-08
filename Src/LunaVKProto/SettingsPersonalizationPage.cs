// Decompiled with JetBrains decompiler
// Type: App1uwp.SettingsPersonalizationPage
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

#nullable disable
namespace App1uwp
{
  public sealed class SettingsPersonalizationPage : PageBase, IComponentConnector
  {
    private PopUpService dialogService;
    private GridView gv;
    private StackPanel colorStack;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Rectangle Offset;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Rectangle _rectAccent;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBlock _roundAvatar;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBlock _scaleInterface;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public SettingsPersonalizationPage()
    {
      this.InitializeComponent();
      SettingsPersonalizationPage personalizationPage = this;
      WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(((FrameworkElement) personalizationPage).add_Loaded), new Action<EventRegistrationToken>(((FrameworkElement) personalizationPage).remove_Loaded), (RoutedEventHandler) ((s, e) =>
      {
        (Window.Current.Content as CustomFrame).HeaderWithMenu.SetTitle("Персонализация");
        ((FrameworkElement) this.Offset).put_Height((Window.Current.Content as CustomFrame).HeaderWithMenu.HeaderHeight);
        this.CFrame.CommandBar.PrimaryCommands.Add(new CommandBarButton()
        {
          Icon = "\uE74E",
          Label = "сохранить",
          Command = (ICommand) new DelegateCommand((Action<object>) (a => Settings.Instance.Save()))
        });
      }));
      ((FrameworkElement) this).put_DataContext((object) Settings.Instance);
    }

    private CustomFrame CFrame => Window.Current.Content as CustomFrame;

    private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
    {
      if (this._scaleInterface == null)
        return;
      this._scaleInterface.put_Text("Масштаб интерфейса (" + (object) e.NewValue + "%)");
    }

    private void Slider_ValueChanged2(object sender, RangeBaseValueChangedEventArgs e)
    {
      if (this._roundAvatar == null)
        return;
      this._roundAvatar.put_Text("Скругление аватарок (" + (object) e.NewValue + "%)");
    }

    private void Rectangle_Tapped(object sender, TappedRoutedEventArgs e)
    {
      if (this.dialogService == null)
        this.dialogService = new PopUpService();
      if (this.gv != null)
        ((ListViewBase) this.gv).SelectedItems.Clear();
      if (this.colorStack == null)
      {
        this.colorStack = new StackPanel();
        ((FrameworkElement) this.colorStack).put_VerticalAlignment((VerticalAlignment) 1);
        ((Panel) this.colorStack).put_Background((Brush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "ItemBackgroundBrush"]);
        this.gv = new GridView();
        ((FrameworkElement) this.gv).put_Margin(new Thickness(0.0, 10.0, 0.0, 10.0));
        ((ListViewBase) this.gv).put_SelectionMode((ListViewSelectionMode) 2);
        GridView gv = this.gv;
        WindowsRuntimeMarshal.AddEventHandler<SizeChangedEventHandler>(new Func<SizeChangedEventHandler, EventRegistrationToken>(((FrameworkElement) gv).add_SizeChanged), new Action<EventRegistrationToken>(((FrameworkElement) gv).remove_SizeChanged), new SizeChangedEventHandler(this.gv_SizeChanged));
        for (int index = 0; index < 8; ++index)
        {
          GridViewItem gridViewItem = new GridViewItem();
          ((Control) gridViewItem).put_Background((Brush) new SolidColorBrush((Color) ((IDictionary<object, object>) Application.Current.Resources)[(object) ("AccentColor" + (object) index)]));
          ((SelectorItem) gridViewItem).put_IsSelected(index == (int) Settings.Instance.AccentColor);
          ((FrameworkElement) gridViewItem).put_Tag((object) index);
          ((ICollection<object>) ((ItemsControl) this.gv).Items).Add((object) gridViewItem);
        }
        ((ICollection<UIElement>) ((Panel) this.colorStack).Children).Add((UIElement) this.gv);
      }
      ((Selector) this.gv).put_SelectedIndex((int) Settings.Instance.AccentColor);
      GridView gv1 = this.gv;
      WindowsRuntimeMarshal.AddEventHandler<SelectionChangedEventHandler>(new Func<SelectionChangedEventHandler, EventRegistrationToken>(((Selector) gv1).add_SelectionChanged), new Action<EventRegistrationToken>(((Selector) gv1).remove_SelectionChanged), new SelectionChangedEventHandler(this.GV_SelectionChanged));
      this.dialogService.Child = (FrameworkElement) this.colorStack;
      this.dialogService.Show();
    }

    private void gv_SizeChanged(object sender, SizeChangedEventArgs e)
    {
      ItemsWrapGrid itemsPanelRoot = (ItemsWrapGrid) ((ItemsControl) (sender as GridView)).ItemsPanelRoot;
      ItemsWrapGrid itemsWrapGrid1 = itemsPanelRoot;
      ItemsWrapGrid itemsWrapGrid2 = itemsPanelRoot;
      Size newSize = e.NewSize;
      double num1;
      double num2 = num1 = newSize.Width / 4.0;
      itemsWrapGrid2.put_ItemWidth(num1);
      double num3 = num2;
      itemsWrapGrid1.put_ItemHeight(num3);
    }

    private void GV_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      GridView gridView = sender as GridView;
      WindowsRuntimeMarshal.RemoveEventHandler<SelectionChangedEventHandler>(new Action<EventRegistrationToken>(((Selector) gridView).remove_SelectionChanged), new SelectionChangedEventHandler(this.GV_SelectionChanged));
      if (((Selector) gridView).SelectedIndex == -1)
        return;
      foreach (object selectedItem in (IEnumerable<object>) ((ListViewBase) gridView).SelectedItems)
      {
        GridViewItem gridViewItem = selectedItem as GridViewItem;
        if ((int) ((FrameworkElement) gridViewItem).Tag != (int) Settings.Instance.AccentColor)
        {
          Settings.Instance.AccentColor = (short) (int) ((FrameworkElement) gridViewItem).Tag;
          ((Shape) this._rectAccent).put_Fill((Brush) new SolidColorBrush((Color) ((IDictionary<object, object>) Application.Current.Resources)[(object) ("AccentColor" + (object) Settings.Instance.AccentColor)]));
          break;
        }
      }
      this.dialogService.Hide();
    }

    private void Border_Tapped(object sender, TappedRoutedEventArgs e)
    {
      Settings.Instance.AccentColor = (short) 0;
      ((Shape) this._rectAccent).put_Fill((Brush) new SolidColorBrush(Color.FromArgb(byte.MaxValue, (byte) 72, (byte) 119, (byte) 203)));
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///SettingsPersonalizationPage.xaml"), (ComponentResourceLocation) 0);
      this.Offset = (Rectangle) ((FrameworkElement) this).FindName("Offset");
      this._rectAccent = (Rectangle) ((FrameworkElement) this).FindName("_rectAccent");
      this._roundAvatar = (TextBlock) ((FrameworkElement) this).FindName("_roundAvatar");
      this._scaleInterface = (TextBlock) ((FrameworkElement) this).FindName("_scaleInterface");
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          UIElement uiElement1 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement1.add_Tapped), new Action<EventRegistrationToken>(uiElement1.remove_Tapped), new TappedEventHandler(this.Rectangle_Tapped));
          break;
        case 2:
          UIElement uiElement2 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement2.add_Tapped), new Action<EventRegistrationToken>(uiElement2.remove_Tapped), new TappedEventHandler(this.Border_Tapped));
          break;
        case 3:
          RangeBase rangeBase1 = (RangeBase) target;
          WindowsRuntimeMarshal.AddEventHandler<RangeBaseValueChangedEventHandler>(new Func<RangeBaseValueChangedEventHandler, EventRegistrationToken>(rangeBase1.add_ValueChanged), new Action<EventRegistrationToken>(rangeBase1.remove_ValueChanged), new RangeBaseValueChangedEventHandler(this.Slider_ValueChanged));
          break;
        case 4:
          RangeBase rangeBase2 = (RangeBase) target;
          WindowsRuntimeMarshal.AddEventHandler<RangeBaseValueChangedEventHandler>(new Func<RangeBaseValueChangedEventHandler, EventRegistrationToken>(rangeBase2.add_ValueChanged), new Action<EventRegistrationToken>(rangeBase2.remove_ValueChanged), new RangeBaseValueChangedEventHandler(this.Slider_ValueChanged2));
          break;
      }
      this._contentLoaded = true;
    }
  }
}
