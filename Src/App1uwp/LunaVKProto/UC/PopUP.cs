// Decompiled with JetBrains decompiler
// Type: App1uwp.UC.PopUP
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
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
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

#nullable disable
namespace App1uwp.UC
{
  public sealed class PopUP : UserControl, IComponentConnector
  {
    private List<string> Titles;
    private PopUpService dialogService;
    public bool Showing;
    public double _MaxWidth;
    public object Argument;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private StackPanel main;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public event EventHandler<int> ItemTapped;

    public PopUP()
    {
      this.InitializeComponent();
      this.Titles = new List<string>();
    }

    public void AddItem(int id, string content, string icon = "", bool active = true)
    {
      this.Titles.Add(content);
      double resource = (double) ((IDictionary<object, object>) Application.Current.Resources)[(object) "FontSizeContent"];
      StackPanel stackPanel = new StackPanel();
      stackPanel.put_Orientation((Orientation) 1);
      ((FrameworkElement) stackPanel).put_Margin(new Thickness(0.0, resource / 3.0, 0.0, resource / 3.0));
      ((FrameworkElement) stackPanel).put_Height(30.0);
      ((FrameworkElement) stackPanel).put_Tag((object) id);
      WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(((UIElement) stackPanel).add_Tapped), new Action<EventRegistrationToken>(((UIElement) stackPanel).remove_Tapped), new TappedEventHandler(this.sp_Tapped));
      ((Panel) stackPanel).put_Background((Brush) new SolidColorBrush(Colors.Transparent));
      TextBlock textBlock = new TextBlock();
      textBlock.put_Text(content);
      ((FrameworkElement) textBlock).put_Margin(new Thickness(10.0, 0.0, 10.0, 0.0));
      ((FrameworkElement) textBlock).put_VerticalAlignment((VerticalAlignment) 1);
      textBlock.put_Foreground((Brush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "TextColorContent"]);
      textBlock.put_FontSize(resource);
      ((UIElement) textBlock).Measure(new Size(1000.0, 1000.0));
      double num = 0.0;
      if (!string.IsNullOrEmpty(icon))
      {
        IconUC iconUc = new IconUC();
        iconUc.put_Glyph(icon);
        ((FrameworkElement) iconUc).put_VerticalAlignment((VerticalAlignment) 1);
        ((IconElement) iconUc).put_Foreground((Brush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "TextColorContent"]);
        iconUc.put_FontSize(textBlock.FontSize);
        ((UIElement) iconUc).Measure(new Size(1000.0, 1000.0));
        ((FrameworkElement) iconUc).put_Margin(new Thickness(10.0, 0.0, 0.0, 0.0));
        ((ICollection<UIElement>) ((Panel) stackPanel).Children).Add((UIElement) iconUc);
        num = ((UIElement) iconUc).DesiredSize.Width + ((FrameworkElement) iconUc).Margin.Left;
      }
      this._MaxWidth = Math.Max(this._MaxWidth, ((UIElement) textBlock).DesiredSize.Width + num + ((FrameworkElement) textBlock).Margin.Left + ((FrameworkElement) textBlock).Margin.Right + 2.0);
      ((ICollection<UIElement>) ((Panel) stackPanel).Children).Add((UIElement) textBlock);
      if (!active)
        ((UIElement) stackPanel).put_Opacity(0.5);
      ((ICollection<UIElement>) ((Panel) this.main).Children).Add((UIElement) stackPanel);
    }

    public void ClearItems()
    {
      ((ICollection<UIElement>) ((Panel) this.main).Children).Clear();
      this._MaxWidth = 0.0;
    }

    public string GetTitle(int index) => this.Titles[index];

    private void sp_Tapped(object sender, TappedRoutedEventArgs e)
    {
      e.put_Handled(true);
      if (this.ItemTapped == null)
        return;
      FrameworkElement frameworkElement = sender as FrameworkElement;
      this.ItemTapped(this.Argument ?? (object) this, (int) frameworkElement.Tag);
      this.dialogService.Hide();
    }

    public void AddSpace()
    {
      Rectangle rectangle = new Rectangle();
      ((FrameworkElement) rectangle).put_Height(1.0);
      ((Shape) rectangle).put_StrokeThickness(1.0);
      ((Shape) rectangle).put_Stroke((Brush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "AccentBrushHigh"]);
      ((FrameworkElement) rectangle).put_Margin(new Thickness(0.0, 5.0, 0.0, 5.0));
      ((UIElement) rectangle).put_Opacity(0.5);
      ((ICollection<UIElement>) ((Panel) this.main).Children).Add((UIElement) rectangle);
    }

    public void Show(Point position)
    {
      if (this.Showing)
        return;
      ((FrameworkElement) this).put_VerticalAlignment((VerticalAlignment) 0);
      ((FrameworkElement) this).put_HorizontalAlignment((HorizontalAlignment) 0);
      double num1 = 0.0;
      foreach (FrameworkElement child in (IEnumerable<UIElement>) ((Panel) this.main).Children)
      {
        num1 += child.Height;
        num1 += child.Margin.Top;
        num1 += child.Margin.Bottom;
      }
      if (num1 + position.Y + 20.0 > ((FrameworkElement) (((ContentControl) (Window.Current.Content as Frame)).Content as PageBase)).ActualHeight)
        position.Y -= num1;
      if (position.X + this._MaxWidth > ((FrameworkElement) (((ContentControl) (Window.Current.Content as Frame)).Content as PageBase)).ActualWidth)
      {
        double num2 = position.X + this._MaxWidth - ((FrameworkElement) (((ContentControl) (Window.Current.Content as Frame)).Content as PageBase)).ActualWidth;
        position.X -= num2;
      }
      if (this.dialogService == null)
        this.dialogService = new PopUpService();
      this.dialogService.VerticalOffset = position.Y;
      this.dialogService.HorizontalOffset = position.X;
      this.dialogService.Child = (FrameworkElement) this;
      this.dialogService.Closed += new EventHandler(this.dialogService_Closed);
      this.dialogService.Show();
      this.Showing = true;
    }

    private void dialogService_Closed(object sender, EventArgs e) => this.Showing = false;

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///UC/PopUP.xaml"), (ComponentResourceLocation) 0);
      this.main = (StackPanel) ((FrameworkElement) this).FindName("main");
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void Connect(int connectionId, object target) => this._contentLoaded = true;
  }
}
