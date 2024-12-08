// Decompiled with JetBrains decompiler
// Type: App1uwp.FontTest
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;

#nullable disable
namespace App1uwp
{
  public sealed class FontTest : Page, IComponentConnector
  {
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private StackPanel St;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public FontTest()
    {
      this.InitializeComponent();
      FontTest fontTest = this;
      WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(((FrameworkElement) fontTest).add_Loaded), new Action<EventRegistrationToken>(((FrameworkElement) fontTest).remove_Loaded), new RoutedEventHandler(this.FontTest_Loaded));
    }

    private void FontTest_Loaded(object sender, RoutedEventArgs e)
    {
      int num = 57345;
      for (int index = 0; index < 100; ++index)
      {
        FontIcon fontIcon = new FontIcon();
        fontIcon.put_FontFamily(new FontFamily("Segoe MDL2 Assets"));
        fontIcon.put_FontSize(40.0);
        (num + index).ToString();
        ((ICollection<UIElement>) ((Panel) this.St).Children).Add((UIElement) fontIcon);
      }
    }

    private string getUnicodeString(string input)
    {
      byte[] bytes = Encoding.Unicode.GetBytes(input);
      if (bytes.Length == 0)
        return "";
      string unicodeString = "\\u";
      for (int index = bytes.Length - 1; index >= 0; --index)
        unicodeString += string.Format("{0:X}", (object) bytes[index]);
      return unicodeString;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///FontTest.xaml"), (ComponentResourceLocation) 0);
      this.St = (StackPanel) ((FrameworkElement) this).FindName("St");
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void Connect(int connectionId, object target) => this._contentLoaded = true;
  }
}
