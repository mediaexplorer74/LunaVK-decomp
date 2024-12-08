// Decompiled with JetBrains decompiler
// Type: App1uwp.UC.GifViewerUC
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
using App1uwp.Network.DataObjects;
using Luna.Network;
using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using XamlAnimatedGif;

#nullable disable
namespace App1uwp.UC
{
  public sealed class GifViewerUC : UserControl, IComponentConnector
  {
    private string _url;
    private long _size;
    private bool _isLoaded;
    private string _localFile;
    private DocPreview.DocPreviewVideo _data;
    private VKDocument _doc;
    private double _downloadProgress;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Image imageGif;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public GifViewerUC() => this.InitializeComponent();

    public GifViewerUC(string preview, VKDocument doc)
      : this()
    {
      this._doc = doc;
    }

    public bool UseOldGifPlayer => string.IsNullOrWhiteSpace(this._doc.preview?.video?.src);

    public void Play(string VideoUri)
    {
      string localFile = this._localFile;
      // ISSUE: reference to a compiler-generated method
      JsonWebRequest.Download(VideoUri, localFile, (EventHandler<bool>) ((sender, result) => Execute.ExecuteOnUIThread((Action) (async () =>
      {
        if (result)
        {
          Stream stream = await CacheManager.GetStreamForRead(this._localFile);
          AnimationBehavior.SetSourceStream((DependencyObject) this.imageGif, stream);
        }
        else
          CacheManager.TryDelete(this._localFile);
      }))), (EventHandler<double>) ((sender, progress) => this.DownloadProgress = progress));
    }

    public double DownloadProgress
    {
      get => this._downloadProgress;
      set => this._downloadProgress = value;
    }

    private void ProgressRingUC_Tapped(object sender, TappedRoutedEventArgs e)
    {
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///UC/GifViewerUC.xaml"), (ComponentResourceLocation) 0);
      this.imageGif = (Image) ((FrameworkElement) this).FindName("imageGif");
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void Connect(int connectionId, object target)
    {
      if (connectionId == 1)
      {
        UIElement uiElement = (UIElement) target;
        WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement.add_Tapped), new Action<EventRegistrationToken>(uiElement.remove_Tapped), new TappedEventHandler(this.ProgressRingUC_Tapped));
      }
      this._contentLoaded = true;
    }
  }
}
