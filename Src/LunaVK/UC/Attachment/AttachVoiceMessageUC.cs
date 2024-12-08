// Decompiled with JetBrains decompiler
// Type: App1uwp.UC.Attachment.AttachVoiceMessageUC
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.DataObjects;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

#nullable disable
namespace App1uwp.UC.Attachment
{
  public sealed class AttachVoiceMessageUC : UserControl, IComponentConnector
  {
    private int max;
    private double wave_width;
    private DispatcherTimer _timerPlayback;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBlock _textBlockDuration;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Grid GridWawes;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private MediaElement media;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private StackPanel HorizontalTrack;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private StackPanel HorizontalFill;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public AttachVoiceMessageUC()
    {
      this.InitializeComponent();
      AttachVoiceMessageUC attachVoiceMessageUc = this;
      WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(((FrameworkElement) attachVoiceMessageUc).add_Loaded), new Action<EventRegistrationToken>(((FrameworkElement) attachVoiceMessageUc).remove_Loaded), new RoutedEventHandler(this.AttachVoiceMessageUC_Loaded));
      MediaElement media1 = this.media;
      WindowsRuntimeMarshal.AddEventHandler<ExceptionRoutedEventHandler>(new Func<ExceptionRoutedEventHandler, EventRegistrationToken>(media1.add_MediaFailed), new Action<EventRegistrationToken>(media1.remove_MediaFailed), new ExceptionRoutedEventHandler(this.media_MediaFailed));
      MediaElement media2 = this.media;
      WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(media2.add_MediaOpened), new Action<EventRegistrationToken>(media2.remove_MediaOpened), new RoutedEventHandler(this.media_MediaOpened));
      MediaElement media3 = this.media;
      WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(media3.add_MediaEnded), new Action<EventRegistrationToken>(media3.remove_MediaEnded), new RoutedEventHandler(this.media_MediaEnded));
      this._timerPlayback = new DispatcherTimer();
      this._timerPlayback.put_Interval(TimeSpan.FromMilliseconds(10.0));
      DispatcherTimer timerPlayback = this._timerPlayback;
      WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(timerPlayback.add_Tick), new Action<EventRegistrationToken>(timerPlayback.remove_Tick), new EventHandler<object>(this._timerPlayback_Tick));
    }

    private void media_MediaEnded(object sender, RoutedEventArgs e) => this._timerPlayback.Stop();

    private void _timerPlayback_Tick(object sender, object e) => this.UpdatePlayerPosition();

    private void media_MediaOpened(object sender, RoutedEventArgs e) => this._timerPlayback.Start();

    private void media_MediaFailed(object sender, ExceptionRoutedEventArgs e)
    {
    }

    private void SetDurationString(TimeSpan timeSpan)
    {
      this._textBlockDuration.put_Text(timeSpan.ToString(timeSpan.Hours > 0 ? "h\\:m\\:ss" : "m\\:ss"));
    }

    private void UpdatePlayerPosition()
    {
      try
      {
        TimeSpan position = this.media.Position;
        if (position.TotalMilliseconds <= 0.0)
          return;
        this.SetDurationString(position);
      }
      catch
      {
      }
    }

    private void AttachVoiceMessageUC_Loaded(object sender, RoutedEventArgs e)
    {
      this.ProcessWaveform();
    }

    private void ProcessWaveform()
    {
      DocPreview.DocPreviewVoiceMessage dataContext = ((FrameworkElement) this).DataContext as DocPreview.DocPreviewVoiceMessage;
      this.wave_width = ((FrameworkElement) this).ActualWidth / (double) dataContext.waveform.Count;
      foreach (int waveformItem1 in dataContext.waveform)
      {
        FrameworkElement waveformItem2 = this.GetWaveformItem(waveformItem1);
        FrameworkElement waveformItem3 = this.GetWaveformItem(waveformItem1);
        ((ICollection<UIElement>) ((Panel) this.HorizontalTrack).Children).Add((UIElement) waveformItem2);
        ((ICollection<UIElement>) ((Panel) this.HorizontalFill).Children).Add((UIElement) waveformItem3);
      }
    }

    public AttachVoiceMessageUC(DocPreview.DocPreviewVoiceMessage a)
      : this()
    {
      ((FrameworkElement) this).put_DataContext((object) a);
      this.max = a.waveform.Max();
    }

    private FrameworkElement GetWaveformItem(int waveformItem)
    {
      double top = (((FrameworkElement) this.GridWawes).Height - (double) waveformItem) / 2.0;
      Rectangle waveformItem1 = new Rectangle();
      Thickness thickness = new Thickness(0.0, top, 0.0, 0.0);
      ((FrameworkElement) waveformItem1).put_VerticalAlignment((VerticalAlignment) 0);
      ((FrameworkElement) waveformItem1).put_Width(this.wave_width);
      ((FrameworkElement) waveformItem1).put_Height(Math.Max(1.0, (double) waveformItem));
      Rectangle rectangle = waveformItem1;
      double num1;
      waveformItem1.put_RadiusY(num1 = ((FrameworkElement) waveformItem1).Width / 2.0);
      double num2 = num1;
      rectangle.put_RadiusX(num2);
      ((FrameworkElement) waveformItem1).put_Margin(thickness);
      ((Shape) waveformItem1).put_Fill((Brush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "AccentBrushHigh"]);
      return (FrameworkElement) waveformItem1;
    }

    private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
    {
      this.media.put_Source(new Uri((((FrameworkElement) this).DataContext as DocPreview.DocPreviewVoiceMessage).link_mp3));
      this.media.Play();
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///UC/Attachment/AttachVoiceMessageUC.xaml"), (ComponentResourceLocation) 0);
      this._textBlockDuration = (TextBlock) ((FrameworkElement) this).FindName("_textBlockDuration");
      this.GridWawes = (Grid) ((FrameworkElement) this).FindName("GridWawes");
      this.media = (MediaElement) ((FrameworkElement) this).FindName("media");
      this.HorizontalTrack = (StackPanel) ((FrameworkElement) this).FindName("HorizontalTrack");
      this.HorizontalFill = (StackPanel) ((FrameworkElement) this).FindName("HorizontalFill");
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void Connect(int connectionId, object target)
    {
      if (connectionId == 1)
      {
        UIElement uiElement = (UIElement) target;
        WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement.add_Tapped), new Action<EventRegistrationToken>(uiElement.remove_Tapped), new TappedEventHandler(this.Grid_Tapped));
      }
      this._contentLoaded = true;
    }
  }
}
