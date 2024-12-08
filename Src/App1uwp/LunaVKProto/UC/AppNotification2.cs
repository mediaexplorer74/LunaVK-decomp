// Decompiled with JetBrains decompiler
// Type: App1uwp.UC.AppNotification2
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

#nullable disable
namespace App1uwp.UC
{
  public sealed class AppNotification2 : UserControl, IComponentConnector
  {
    private DispatcherTimer timer;
    private Action _tapCallback;
    public Action<UserControl> TimeToDelete;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Grid main_grid;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Image img;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBlock title;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private StackPanel content;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public AppNotification2()
    {
      DispatcherTimer dispatcherTimer = new DispatcherTimer();
      dispatcherTimer.put_Interval(TimeSpan.FromSeconds(5.0));
      this.timer = dispatcherTimer;
      // ISSUE: explicit constructor call
      base.\u002Ector();
      this.InitializeComponent();
    }

    public string Title => !this.timer.IsEnabled ? "" : this.title.Text;

    public AppNotification2(string image_src, string title, string content, Action tapCallback)
      : this()
    {
      this._tapCallback = tapCallback;
      this.img.put_Source((ImageSource) new BitmapImage(new Uri(image_src)));
      this.title.put_Text(title);
      this.AddContent(content);
      DispatcherTimer timer = this.timer;
      WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(timer.add_Tick), new Action<EventRegistrationToken>(timer.remove_Tick), new EventHandler<object>(this.timer_Tick));
      this.timer.Start();
    }

    private void timer_Tick(object sender, object e)
    {
      if (((ICollection<UIElement>) ((Panel) this.content).Children).Count > 0)
      {
        if (((ICollection<UIElement>) ((Panel) this.content).Children).Count == 1)
        {
          if (this.TimeToDelete == null)
            return;
          this.TimeToDelete((UserControl) this);
        }
        else
          ((IList<UIElement>) ((Panel) this.content).Children).RemoveAt(0);
      }
      else
        this.timer.Stop();
    }

    public void AddContent(string content)
    {
      if (((ICollection<UIElement>) ((Panel) this.content).Children).Count >= 3)
        ((IList<UIElement>) ((Panel) this.content).Children).RemoveAt(0);
      TextBlock textBlock = new TextBlock();
      textBlock.put_FontSize((double) ((IDictionary<object, object>) Application.Current.Resources)[(object) "FontSizeContent"]);
      textBlock.put_Text(content);
      textBlock.put_Foreground((Brush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "TextColorContent"]);
      ((FrameworkElement) textBlock).put_Margin(new Thickness(0.0, 0.0, 0.0, 10.0));
      ((ICollection<UIElement>) ((Panel) this.content).Children).Add((UIElement) textBlock);
      if (this.timer.IsEnabled)
        return;
      this.timer.Start();
    }

    public void Hide()
    {
      if (this.TimeToDelete == null)
        return;
      this.TimeToDelete((UserControl) this);
    }

    private void main_grid_Tapped(object sender, TappedRoutedEventArgs e)
    {
      if (this._tapCallback != null)
        this._tapCallback();
      if (this.TimeToDelete == null)
        return;
      this.TimeToDelete((UserControl) this);
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///UC/AppNotification2.xaml"), (ComponentResourceLocation) 0);
      this.main_grid = (Grid) ((FrameworkElement) this).FindName("main_grid");
      this.img = (Image) ((FrameworkElement) this).FindName("img");
      this.title = (TextBlock) ((FrameworkElement) this).FindName("title");
      this.content = (StackPanel) ((FrameworkElement) this).FindName("content");
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void Connect(int connectionId, object target)
    {
      if (connectionId == 1)
      {
        UIElement uiElement = (UIElement) target;
        WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement.add_Tapped), new Action<EventRegistrationToken>(uiElement.remove_Tapped), new TappedEventHandler(this.main_grid_Tapped));
      }
      this._contentLoaded = true;
    }
  }
}
