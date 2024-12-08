// Decompiled with JetBrains decompiler
// Type: App1uwp.UC.PullToRefreshUC
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

#nullable disable
namespace App1uwp.UC
{
  public sealed class PullToRefreshUC : UserControl, IComponentConnector
  {
    private StatusBar bar;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Rectangle rectProgress;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBlock textBlockTip;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public PullToRefreshUC()
    {
      this.InitializeComponent();
      PullToRefreshUC pullToRefreshUc = this;
      WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(((FrameworkElement) pullToRefreshUc).add_Loaded), new Action<EventRegistrationToken>(((FrameworkElement) pullToRefreshUc).remove_Loaded), new RoutedEventHandler(this.PullToRefreshUC_Loaded));
    }

    private void PullToRefreshUC_Loaded(object sender, RoutedEventArgs e)
    {
      this.bar = StatusBar.GetForCurrentView();
      ((FrameworkElement) this.rectProgress).put_Width(((FrameworkElement) this).ActualWidth);
      (((UIElement) this.rectProgress).RenderTransform as ScaleTransform).put_CenterX(((FrameworkElement) this.rectProgress).Width / 2.0);
      this.Update(0.0);
    }

    public void TrackListBox(ExtendedListView2 lv)
    {
      lv.OnPullPercentageChanged += new Action<double>(this.OnPullPercentageChanged);
      this.Update(0.0);
    }

    private void OnPullPercentageChanged(double value) => this.Update(value);

    private void Update(double p)
    {
      double num = 0.01 * p;
      (((UIElement) this.rectProgress).RenderTransform as ScaleTransform).put_ScaleX(num);
      ((UIElement) this.textBlockTip).put_Opacity(num);
      if (p < 2.0)
        this.bar.ShowAsync();
      else
        this.bar.HideAsync();
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///UC/PullToRefreshUC.xaml"), (ComponentResourceLocation) 0);
      this.rectProgress = (Rectangle) ((FrameworkElement) this).FindName("rectProgress");
      this.textBlockTip = (TextBlock) ((FrameworkElement) this).FindName("textBlockTip");
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void Connect(int connectionId, object target) => this._contentLoaded = true;
  }
}
