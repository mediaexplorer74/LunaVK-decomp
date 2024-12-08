// Decompiled with JetBrains decompiler
// Type: App1uwp.TestRectangleLayout
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.DataObjects;
using App1uwp.UC.Attachment;
using App1uwp.Utils;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;

#nullable disable
namespace App1uwp
{
  public sealed class TestRectangleLayout : Page, IComponentConnector
  {
    private int num = 6;
    private double h;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Border edge;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBlock sl_w;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBlock sl_h;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBlock sl_value;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Grid MainGrid;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public TestRectangleLayout() => this.InitializeComponent();

    private void TestRectangleLayout_Loaded(object sender, RoutedEventArgs e)
    {
      if (this.h <= 0.0)
        this.h = 400.0;
      ((FrameworkElement) this.edge).put_Margin(new Thickness(0.0, this.h, 0.0, 0.0));
      List<Size> childrenRects = new List<Size>();
      for (int index = 0; index < this.num; ++index)
      {
        Size size = new Size(90.0, 210.0);
        childrenRects.Add(size);
      }
      List<Rect> layout = RectangleLayoutHelper.CreateLayout(((FrameworkElement) this.MainGrid).Width, childrenRects.Count != 1 ? (childrenRects.Count < 2 || childrenRects.Count > 4 ? 580.0 : 430.0) : 280.0, childrenRects, 4.0);
      for (int index = 0; index < layout.Count; ++index)
      {
        Rect rect = layout[index];
        Thickness thickness = new Thickness(rect.Left, rect.Top, 0.0, 0.0);
        Border border = new Border();
        ((FrameworkElement) border).put_HorizontalAlignment((HorizontalAlignment) 0);
        ((FrameworkElement) border).put_VerticalAlignment((VerticalAlignment) 0);
        ((FrameworkElement) border).put_Width(rect.Width);
        ((FrameworkElement) border).put_Height(rect.Height);
        ((FrameworkElement) border).put_Margin(thickness);
        border.put_Background((Brush) new SolidColorBrush(Colors.Yellow));
        border.put_BorderBrush((Brush) new SolidColorBrush(Colors.Red));
        border.put_BorderThickness(new Thickness(2.0));
        TextBlock textBlock1 = new TextBlock();
        textBlock1.put_FontSize(16.0);
        textBlock1.put_Foreground((Brush) new SolidColorBrush(Colors.Black));
        textBlock1.put_Text(string.Format("{0}\nw:{1}h:{2}\n", (object) index.ToString(), (object) (int) rect.Width, (object) (int) rect.Height));
        TextBlock textBlock2 = textBlock1;
        textBlock2.put_Text(textBlock2.Text + string.Format("t:{0}b:{1}\n", (object) (int) rect.Top, (object) (int) rect.Bottom));
        TextBlock textBlock3 = textBlock1;
        textBlock3.put_Text(textBlock3.Text + string.Format("l:{0}r:{1}\n", (object) (int) rect.Left, (object) (int) rect.Right));
        TextBlock textBlock4 = textBlock1;
        textBlock4.put_Text(textBlock4.Text + string.Format("x:{0}y:{1}", (object) (int) rect.X, (object) (int) rect.Y));
        border.put_Child((UIElement) textBlock1);
        ((ICollection<UIElement>) ((Panel) this.MainGrid).Children).Add((UIElement) border);
      }
    }

    private void DO()
    {
      List<VKPhoto> list = new List<VKPhoto>();
      for (int index = 0; index < this.num; ++index)
        list.Add(new VKPhoto()
        {
          width = 719,
          height = 917,
          photo_604 = "https://pp.userapi.com/c7004/v7004186/34d11/gWAADvKHkxc.jpg"
        });
      if (list.Count == 0)
        return;
      AttachPhotosUC attachPhotosUc = new AttachPhotosUC(list, ((FrameworkElement) this.MainGrid).Width);
      ((FrameworkElement) attachPhotosUc).put_VerticalAlignment((VerticalAlignment) 0);
      ((ICollection<UIElement>) ((Panel) this.MainGrid).Children).Add((UIElement) attachPhotosUc);
    }

    private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
    {
      this.num = (int) e.NewValue;
      this.sl_value.put_Text(this.num.ToString());
      ((ICollection<UIElement>) ((Panel) this.MainGrid).Children).Clear();
      this.DO();
    }

    private void Slider_ValueChanged_1(object sender, RangeBaseValueChangedEventArgs e)
    {
      this.h -= e.NewValue;
      this.sl_h.put_Text(this.h.ToString());
      ((ICollection<UIElement>) ((Panel) this.MainGrid).Children).Clear();
      this.DO();
    }

    private void Slider_ValueChanged_2(object sender, RangeBaseValueChangedEventArgs e)
    {
      Grid mainGrid = this.MainGrid;
      ((FrameworkElement) mainGrid).put_Width(((FrameworkElement) mainGrid).Width - e.NewValue);
      this.sl_w.put_Text(((FrameworkElement) this.MainGrid).Width.ToString());
      ((ICollection<UIElement>) ((Panel) this.MainGrid).Children).Clear();
      this.DO();
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///TestRectangleLayout.xaml"), (ComponentResourceLocation) 0);
      this.edge = (Border) ((FrameworkElement) this).FindName("edge");
      this.sl_w = (TextBlock) ((FrameworkElement) this).FindName("sl_w");
      this.sl_h = (TextBlock) ((FrameworkElement) this).FindName("sl_h");
      this.sl_value = (TextBlock) ((FrameworkElement) this).FindName("sl_value");
      this.MainGrid = (Grid) ((FrameworkElement) this).FindName("MainGrid");
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          RangeBase rangeBase1 = (RangeBase) target;
          WindowsRuntimeMarshal.AddEventHandler<RangeBaseValueChangedEventHandler>(new Func<RangeBaseValueChangedEventHandler, EventRegistrationToken>(rangeBase1.add_ValueChanged), new Action<EventRegistrationToken>(rangeBase1.remove_ValueChanged), new RangeBaseValueChangedEventHandler(this.Slider_ValueChanged_2));
          break;
        case 2:
          RangeBase rangeBase2 = (RangeBase) target;
          WindowsRuntimeMarshal.AddEventHandler<RangeBaseValueChangedEventHandler>(new Func<RangeBaseValueChangedEventHandler, EventRegistrationToken>(rangeBase2.add_ValueChanged), new Action<EventRegistrationToken>(rangeBase2.remove_ValueChanged), new RangeBaseValueChangedEventHandler(this.Slider_ValueChanged_1));
          break;
        case 3:
          RangeBase rangeBase3 = (RangeBase) target;
          WindowsRuntimeMarshal.AddEventHandler<RangeBaseValueChangedEventHandler>(new Func<RangeBaseValueChangedEventHandler, EventRegistrationToken>(rangeBase3.add_ValueChanged), new Action<EventRegistrationToken>(rangeBase3.remove_ValueChanged), new RangeBaseValueChangedEventHandler(this.Slider_ValueChanged));
          break;
      }
      this._contentLoaded = true;
    }
  }
}
