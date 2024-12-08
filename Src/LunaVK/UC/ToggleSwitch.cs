// Decompiled with JetBrains decompiler
// Type: App1uwp.UC.ToggleSwitch
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Shapes;

#nullable disable
namespace App1uwp.UC
{
  public class ToggleSwitch : UserControl, IComponentConnector
  {
    public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.RegisterAttached(nameof (IsChecked), typeof (bool), typeof (ToggleSwitch), new PropertyMetadata((object) null, new PropertyChangedCallback(ToggleSwitch.IsChecked_OnChanged)));
    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof (Title), typeof (string), typeof (ToggleSwitch), new PropertyMetadata((object) null, new PropertyChangedCallback(ToggleSwitch.Title_OnChanged)));
    public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(nameof (Description), typeof (string), typeof (ToggleSwitch), new PropertyMetadata((object) null, new PropertyChangedCallback(ToggleSwitch.Description_OnChanged)));
    public static readonly DependencyProperty StateTextOnProperty = DependencyProperty.Register(nameof (StateTextOn), typeof (string), typeof (ToggleSwitch), new PropertyMetadata((object) null, new PropertyChangedCallback(ToggleSwitch.StateTextOn_OnChanged)));
    public static readonly DependencyProperty StateTextOffProperty = DependencyProperty.Register(nameof (StateTextOff), typeof (string), typeof (ToggleSwitch), new PropertyMetadata((object) null, new PropertyChangedCallback(ToggleSwitch.StateTextOff_OnChanged)));
    public static readonly DependencyProperty BorderColorProperty = DependencyProperty.Register(nameof (BorderColor), typeof (Brush), typeof (ToggleSwitch), new PropertyMetadata((object) null, new PropertyChangedCallback(ToggleSwitch.BorderColor_OnChanged)));
    private Storyboard _storyboard;
    private double total_width;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Storyboard AnimateChecked;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private DoubleAnimation AnimateCheckedAnim;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private DoubleAnimation AnimateOp;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBlock textBlockTitle;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBlock textBlockDescription;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Rectangle OuterBorder;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Rectangle SwitchBackground;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Grid SwitchKnobBounds;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Grid TextState;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBlock TextStateOn;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBlock TextStateOff;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Ellipse SwitchKnobOff;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Ellipse SwitchKnobOn;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private CompositeTransform ForGroundCellXPos;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public event EventHandler<RoutedEventArgs> Checked;

    public ToggleSwitch()
    {
      this.InitializeComponent();
      this.total_width = ((FrameworkElement) this.SwitchBackground).Width - ((FrameworkElement) this.SwitchKnobBounds).Width;
      this._storyboard = new Storyboard();
      DoubleAnimation doubleAnimation = new DoubleAnimation();
      ((ICollection<Timeline>) this._storyboard.Children).Add((Timeline) doubleAnimation);
      Storyboard.SetTarget((Timeline) doubleAnimation, (DependencyObject) ((UIElement) this.SwitchKnobBounds).RenderTransform);
      Storyboard.SetTargetProperty((Timeline) doubleAnimation, "TranslateX");
      ((Timeline) doubleAnimation).put_Duration(new Duration(TimeSpan.FromSeconds(0.2)));
      Storyboard animateChecked = this.AnimateChecked;
      WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(((Timeline) animateChecked).add_Completed), new Action<EventRegistrationToken>(((Timeline) animateChecked).remove_Completed), new EventHandler<object>(this.AnimateChecked_Completed));
      this.StateTextOn = LocalizedStrings.GetString("On");
      this.StateTextOff = LocalizedStrings.GetString("Off");
    }

    private void AnimateChecked_Completed(object sender, object e)
    {
      this.IsChecked = ((UIElement) this.SwitchBackground).Opacity == 1.0;
    }

    public bool IsChecked
    {
      get => (bool) ((DependencyObject) this).GetValue(ToggleSwitch.IsCheckedProperty);
      set => ((DependencyObject) this).SetValue(ToggleSwitch.IsCheckedProperty, (object) value);
    }

    public bool IsStateTextVisible
    {
      get => ((UIElement) this.TextState).Visibility == 0;
      set => ((UIElement) this.TextState).put_Visibility(value ? (Visibility) 0 : (Visibility) 1);
    }

    public string Title
    {
      get => (string) ((DependencyObject) this).GetValue(ToggleSwitch.TitleProperty);
      set => ((DependencyObject) this).SetValue(ToggleSwitch.TitleProperty, (object) value);
    }

    public string Description
    {
      get => (string) ((DependencyObject) this).GetValue(ToggleSwitch.DescriptionProperty);
      set => ((DependencyObject) this).SetValue(ToggleSwitch.DescriptionProperty, (object) value);
    }

    public string StateTextOn
    {
      get => (string) ((DependencyObject) this).GetValue(ToggleSwitch.StateTextOnProperty);
      set => ((DependencyObject) this).SetValue(ToggleSwitch.StateTextOnProperty, (object) value);
    }

    public string StateTextOff
    {
      get => (string) ((DependencyObject) this).GetValue(ToggleSwitch.StateTextOffProperty);
      set => ((DependencyObject) this).SetValue(ToggleSwitch.StateTextOffProperty, (object) value);
    }

    public Brush BorderColor
    {
      get => (Brush) ((DependencyObject) this).GetValue(ToggleSwitch.BorderColorProperty);
      set => ((DependencyObject) this).SetValue(ToggleSwitch.BorderColorProperty, (object) value);
    }

    private static void IsChecked_OnChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      ToggleSwitch toggleSwitch = (ToggleSwitch) d;
      toggleSwitch.FireCheckedEvent();
      if ((bool) e.NewValue)
      {
        toggleSwitch.MoveLeft(toggleSwitch.total_width);
        toggleSwitch.UpdateOpacity(1.0);
        ((UIElement) toggleSwitch.TextStateOn).put_Visibility((Visibility) 0);
        ((UIElement) toggleSwitch.TextStateOff).put_Visibility((Visibility) 1);
      }
      else
      {
        toggleSwitch.MoveLeft(0.0);
        toggleSwitch.UpdateOpacity(0.0);
        ((UIElement) toggleSwitch.TextStateOn).put_Visibility((Visibility) 1);
        ((UIElement) toggleSwitch.TextStateOff).put_Visibility((Visibility) 0);
      }
    }

    private static void Title_OnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ToggleSwitch toggleSwitch = (ToggleSwitch) d;
      toggleSwitch.textBlockTitle.put_Text(toggleSwitch.Title);
      ((UIElement) toggleSwitch.textBlockTitle).put_Visibility(string.IsNullOrEmpty(toggleSwitch.Title) ? (Visibility) 1 : (Visibility) 0);
    }

    private static void Description_OnChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      ToggleSwitch toggleSwitch = (ToggleSwitch) d;
      toggleSwitch.textBlockDescription.put_Text(toggleSwitch.Description);
      if (string.IsNullOrEmpty(toggleSwitch.Description))
      {
        ((UIElement) toggleSwitch.textBlockDescription).put_Visibility((Visibility) 1);
        toggleSwitch.textBlockTitle.put_FontWeight(FontWeights.Normal);
      }
      else
      {
        ((UIElement) toggleSwitch.textBlockDescription).put_Visibility((Visibility) 0);
        toggleSwitch.textBlockTitle.put_FontWeight(FontWeights.Medium);
      }
    }

    private static void StateTextOn_OnChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      ToggleSwitch toggleSwitch = (ToggleSwitch) d;
      toggleSwitch.TextStateOn.put_Text(toggleSwitch.StateTextOn);
    }

    private static void StateTextOff_OnChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      ToggleSwitch toggleSwitch = (ToggleSwitch) d;
      toggleSwitch.TextStateOff.put_Text(toggleSwitch.StateTextOff);
    }

    private static void BorderColor_OnChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      ToggleSwitch toggleSwitch = d as ToggleSwitch;
      Brush newValue = e.NewValue as Brush;
      Rectangle switchBackground = toggleSwitch.SwitchBackground;
      Rectangle outerBorder = toggleSwitch.OuterBorder;
      Brush brush1;
      ((Shape) toggleSwitch.SwitchKnobOff).put_Fill(brush1 = newValue);
      Brush brush2;
      Brush brush3 = brush2 = brush1;
      ((Shape) outerBorder).put_Stroke(brush2);
      Brush brush4 = brush3;
      ((Shape) switchBackground).put_Fill(brush4);
    }

    private void FireCheckedEvent()
    {
      if (this.Checked == null)
        return;
      this.Checked((object) this, new RoutedEventArgs());
    }

    private void ForeGroundCellGrid_ManipulationDelta(
      object sender,
      ManipulationDeltaRoutedEventArgs e)
    {
      double left = Canvas.GetLeft(this.Content) + e.Delta.Translation.X;
      if (left > this.total_width || left <= 0.0)
        return;
      this.UpdateOpacity(left / this.total_width);
      this.MoveLeft(left);
      if (!e.IsInertial)
        return;
      e.Complete();
    }

    private void UpdateOpacity(double newop)
    {
      Ellipse switchKnobOn = this.SwitchKnobOn;
      double num1;
      ((UIElement) this.SwitchBackground).put_Opacity(num1 = Math.Abs(newop));
      double num2 = num1;
      ((UIElement) switchKnobOn).put_Opacity(num2);
    }

    public void MoveLeft(double left, bool fast = false)
    {
      double left1 = Canvas.GetLeft(this.Content);
      Storyboard storyboard = this._storyboard;
      DoubleAnimation child = (DoubleAnimation) ((IList<Timeline>) storyboard.Children)[0];
      child.put_From(new double?(left1));
      storyboard.SkipToFill();
      child.put_To(new double?(left));
      this._storyboard.Begin();
      Canvas.SetLeft(this.Content, left);
    }

    private void ForeGroundCellGrid_ManipulationCompleted(
      object sender,
      ManipulationCompletedRoutedEventArgs e)
    {
      DoubleAnimation animateOp1 = this.AnimateOp;
      double? nullable1;
      this.AnimateCheckedAnim.put_From(nullable1 = new double?(((UIElement) this.SwitchBackground).Opacity));
      double? nullable2 = nullable1;
      animateOp1.put_From(nullable2);
      if (e.Cumulative.Translation.X > this.total_width / 2.0)
      {
        this.MoveLeft(this.total_width);
        DoubleAnimation animateOp2 = this.AnimateOp;
        double? nullable3;
        this.AnimateCheckedAnim.put_To(nullable3 = new double?(1.0));
        double? nullable4 = nullable3;
        animateOp2.put_To(nullable4);
      }
      else
      {
        this.MoveLeft(0.0);
        DoubleAnimation animateOp3 = this.AnimateOp;
        double? nullable5;
        this.AnimateCheckedAnim.put_To(nullable5 = new double?(0.0));
        double? nullable6 = nullable5;
        animateOp3.put_To(nullable6);
      }
      this.AnimateChecked.Begin();
    }

    private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
    {
      this.IsChecked = !this.IsChecked;
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///UC/ToggleSwitch.xaml"), (ComponentResourceLocation) 0);
      this.AnimateChecked = (Storyboard) ((FrameworkElement) this).FindName("AnimateChecked");
      this.AnimateCheckedAnim = (DoubleAnimation) ((FrameworkElement) this).FindName("AnimateCheckedAnim");
      this.AnimateOp = (DoubleAnimation) ((FrameworkElement) this).FindName("AnimateOp");
      this.textBlockTitle = (TextBlock) ((FrameworkElement) this).FindName("textBlockTitle");
      this.textBlockDescription = (TextBlock) ((FrameworkElement) this).FindName("textBlockDescription");
      this.OuterBorder = (Rectangle) ((FrameworkElement) this).FindName("OuterBorder");
      this.SwitchBackground = (Rectangle) ((FrameworkElement) this).FindName("SwitchBackground");
      this.SwitchKnobBounds = (Grid) ((FrameworkElement) this).FindName("SwitchKnobBounds");
      this.TextState = (Grid) ((FrameworkElement) this).FindName("TextState");
      this.TextStateOn = (TextBlock) ((FrameworkElement) this).FindName("TextStateOn");
      this.TextStateOff = (TextBlock) ((FrameworkElement) this).FindName("TextStateOff");
      this.SwitchKnobOff = (Ellipse) ((FrameworkElement) this).FindName("SwitchKnobOff");
      this.SwitchKnobOn = (Ellipse) ((FrameworkElement) this).FindName("SwitchKnobOn");
      this.ForGroundCellXPos = (CompositeTransform) ((FrameworkElement) this).FindName("ForGroundCellXPos");
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          UIElement uiElement1 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement1.add_Tapped), new Action<EventRegistrationToken>(uiElement1.remove_Tapped), new TappedEventHandler(this.Grid_Tapped));
          break;
        case 2:
          UIElement uiElement2 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<ManipulationDeltaEventHandler>(new Func<ManipulationDeltaEventHandler, EventRegistrationToken>(uiElement2.add_ManipulationDelta), new Action<EventRegistrationToken>(uiElement2.remove_ManipulationDelta), new ManipulationDeltaEventHandler(this.ForeGroundCellGrid_ManipulationDelta));
          UIElement uiElement3 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<ManipulationCompletedEventHandler>(new Func<ManipulationCompletedEventHandler, EventRegistrationToken>(uiElement3.add_ManipulationCompleted), new Action<EventRegistrationToken>(uiElement3.remove_ManipulationCompleted), new ManipulationCompletedEventHandler(this.ForeGroundCellGrid_ManipulationCompleted));
          break;
      }
      this._contentLoaded = true;
    }
  }
}
