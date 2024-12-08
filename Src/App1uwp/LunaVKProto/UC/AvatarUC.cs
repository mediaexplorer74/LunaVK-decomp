// Decompiled with JetBrains decompiler
// Type: App1uwp.UC.AvatarUC
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;

#nullable disable
namespace App1uwp.UC
{
  public sealed class AvatarUC : UserControl, IComponentConnector
  {
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Border _brd;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Border _brd2;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Border _brd3;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Border _brd4;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public AvatarUC()
    {
      this.InitializeComponent();
      AvatarUC avatarUc = this;
      WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(((FrameworkElement) avatarUc).add_Loaded), new Action<EventRegistrationToken>(((FrameworkElement) avatarUc).remove_Loaded), new RoutedEventHandler(this.AvatarUC_Loaded));
    }

    private void AvatarUC_Loaded(object sender, RoutedEventArgs e)
    {
      ((FrameworkElement) this._brd).put_Width(((FrameworkElement) this).Width);
      ((FrameworkElement) this._brd2).put_Width(((FrameworkElement) this).Width);
      ((FrameworkElement) this._brd3).put_Width(((FrameworkElement) this).Width);
      ((FrameworkElement) this._brd4).put_Width(((FrameworkElement) this).Width);
      this._brd.LetsRound();
      this._brd2.LetsRound();
      this._brd3.LetsRound();
      this._brd4.LetsRound();
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///UC/AvatarUC.xaml"), (ComponentResourceLocation) 0);
      this._brd = (Border) ((FrameworkElement) this).FindName("_brd");
      this._brd2 = (Border) ((FrameworkElement) this).FindName("_brd2");
      this._brd3 = (Border) ((FrameworkElement) this).FindName("_brd3");
      this._brd4 = (Border) ((FrameworkElement) this).FindName("_brd4");
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void Connect(int connectionId, object target) => this._contentLoaded = true;
  }
}
