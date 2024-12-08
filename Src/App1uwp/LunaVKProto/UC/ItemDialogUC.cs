// Decompiled with JetBrains decompiler
// Type: App1uwp.UC.ItemDialogUC
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
using App1uwp.Network.DataObjects;
using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;

#nullable disable
namespace App1uwp.UC
{
  public sealed class ItemDialogUC : UserControl, IComponentConnector
  {
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Border _brd;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public ItemDialogUC()
    {
      this.InitializeComponent();
      this._brd.LetsRound();
    }

    private void ItemDialogUC_Tapped(object sender, TappedRoutedEventArgs e)
    {
      (Window.Current.Content as Frame).Navigate(typeof (ConversationPage), (object) (VKDialog) ((FrameworkElement) (sender as ItemDialogUC)).DataContext);
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///UC/ItemDialogUC.xaml"), (ComponentResourceLocation) 0);
      this._brd = (Border) ((FrameworkElement) this).FindName("_brd");
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void Connect(int connectionId, object target) => this._contentLoaded = true;
  }
}
