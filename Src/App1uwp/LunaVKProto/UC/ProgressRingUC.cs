// Decompiled with JetBrains decompiler
// Type: App1uwp.UC.ProgressRingUC
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;

#nullable disable
namespace App1uwp.UC
{
  public sealed class ProgressRingUC : UserControl, IComponentConnector
  {
    public static readonly DependencyProperty ProgressProperty = DependencyProperty.Register(nameof (Progress), typeof (double), typeof (ProgressRingUC), new PropertyMetadata((object) 0.0, new PropertyChangedCallback(ProgressRingUC.Progress_OnChanged)));
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private ArcSegment arcProgress;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public double Progress
    {
      get => (double) ((DependencyObject) this).GetValue(ProgressRingUC.ProgressProperty);
      set => ((DependencyObject) this).SetValue(ProgressRingUC.ProgressProperty, (object) value);
    }

    public ProgressRingUC() => this.InitializeComponent();

    private static void Progress_OnChanged(
      DependencyObject obj,
      DependencyPropertyChangedEventArgs e)
    {
      double num1 = (double) e.NewValue;
      if (num1 < 0.0)
        num1 = 0.0;
      else if (num1 > 100.0)
        num1 = 100.0;
      ProgressRingUC progressRingUc = (ProgressRingUC) obj;
      double num2 = num1 / 100.0;
      progressRingUc.arcProgress.put_Point(new Point(26.0 * Math.Sin(6.2831853071795862 * num2), 26.0 * (1.0 - Math.Cos(6.2831853071795862 * num2))));
      progressRingUc.arcProgress.put_IsLargeArc(num1 >= 50.0);
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///UC/ProgressRingUC.xaml"), (ComponentResourceLocation) 0);
      this.arcProgress = (ArcSegment) ((FrameworkElement) this).FindName("arcProgress");
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void Connect(int connectionId, object target) => this._contentLoaded = true;
  }
}
