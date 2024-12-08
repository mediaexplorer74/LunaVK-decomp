// Decompiled with JetBrains decompiler
// Type: App1uwp.TestAudioRecord
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

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
namespace App1uwp
{
  public sealed class TestAudioRecord : Page, IComponentConnector
  {
    private double _frameSize = 20.0;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public TestAudioRecord()
    {
      this.InitializeComponent();
      TestAudioRecord testAudioRecord = this;
      WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(((FrameworkElement) testAudioRecord).add_Loaded), new Action<EventRegistrationToken>(((FrameworkElement) testAudioRecord).remove_Loaded), new RoutedEventHandler(this.TestAudioRecord_Loaded));
    }

    private void TestAudioRecord_Loaded(object sender, RoutedEventArgs e)
    {
    }

    private int GetFrameSize() => (int) (48000.0 * this._frameSize / 1000.0);

    private void Button_Click(object sender, RoutedEventArgs e)
    {
    }

    private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
    {
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///TestAudioRecord.xaml"), (ComponentResourceLocation) 0);
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          UIElement uiElement = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement.add_Tapped), new Action<EventRegistrationToken>(uiElement.remove_Tapped), new TappedEventHandler(this.Grid_Tapped));
          break;
        case 2:
          ButtonBase buttonBase = (ButtonBase) target;
          WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.Button_Click));
          break;
      }
      this._contentLoaded = true;
    }
  }
}
