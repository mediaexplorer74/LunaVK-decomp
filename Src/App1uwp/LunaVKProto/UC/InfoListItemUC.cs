// Decompiled with JetBrains decompiler
// Type: App1uwp.UC.InfoListItemUC
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Utils;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Markup;

#nullable disable
namespace App1uwp.UC
{
  public sealed class InfoListItemUC : UserControl, IComponentConnector
  {
    public static readonly DependencyProperty IconUrlProperty = DependencyProperty.Register(nameof (IconUrl), typeof (string), typeof (InfoListItemUC), new PropertyMetadata((object) null, new PropertyChangedCallback(InfoListItemUC.OnIconUrlChanged)));
    public static readonly DependencyProperty InlinesProperty = DependencyProperty.Register(nameof (Inlines), typeof (List<Inline>), typeof (InfoListItemUC), new PropertyMetadata((object) null, new PropertyChangedCallback(InfoListItemUC.OnInlinesChanged)));
    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof (Text), typeof (string), typeof (InfoListItemUC), new PropertyMetadata((object) null, new PropertyChangedCallback(InfoListItemUC.OnTextChanged)));
    public static readonly DependencyProperty Preview1UrlProperty = DependencyProperty.Register(nameof (Preview1Url), typeof (string), typeof (InfoListItemUC), new PropertyMetadata((object) null, new PropertyChangedCallback(InfoListItemUC.OnPreview1UrlChanged)));
    public static readonly DependencyProperty Preview2UrlProperty = DependencyProperty.Register(nameof (Preview2Url), typeof (string), typeof (InfoListItemUC), new PropertyMetadata((object) null, new PropertyChangedCallback(InfoListItemUC.OnPreview2UrlChanged)));
    public static readonly DependencyProperty Preview3UrlProperty = DependencyProperty.Register(nameof (Preview3Url), typeof (string), typeof (InfoListItemUC), new PropertyMetadata((object) null, new PropertyChangedCallback(InfoListItemUC.OnPreview3UrlChanged)));
    public static readonly DependencyProperty IsTiltEnabledProperty = DependencyProperty.Register(nameof (IsTiltEnabled), typeof (bool), typeof (InfoListItemUC), new PropertyMetadata((object) null, new PropertyChangedCallback(InfoListItemUC.OnIsTiltEnabledChanged)));
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Grid gridRoot;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private IconUC borderIcon;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private ScrollableTextBlock textBlockContent;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Grid gridPreview1;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Grid gridPreview2;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Grid gridPreview3;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Image imagePreview3;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Image imagePreview2;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Image imagePreview1;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public string IconUrl
    {
      get => (string) ((DependencyObject) this).GetValue(InfoListItemUC.IconUrlProperty);
      set => ((DependencyObject) this).SetValue(InfoListItemUC.IconUrlProperty, (object) value);
    }

    public List<Inline> Inlines
    {
      get => (List<Inline>) ((DependencyObject) this).GetValue(InfoListItemUC.InlinesProperty);
      set => ((DependencyObject) this).SetValue(InfoListItemUC.InlinesProperty, (object) value);
    }

    public string Text
    {
      get => (string) ((DependencyObject) this).GetValue(InfoListItemUC.TextProperty);
      set => ((DependencyObject) this).SetValue(InfoListItemUC.TextProperty, (object) value);
    }

    public string Preview1Url
    {
      get => (string) ((DependencyObject) this).GetValue(InfoListItemUC.Preview1UrlProperty);
      set => ((DependencyObject) this).SetValue(InfoListItemUC.Preview1UrlProperty, (object) value);
    }

    public string Preview2Url
    {
      get => (string) ((DependencyObject) this).GetValue(InfoListItemUC.Preview2UrlProperty);
      set => ((DependencyObject) this).SetValue(InfoListItemUC.Preview2UrlProperty, (object) value);
    }

    public string Preview3Url
    {
      get => (string) ((DependencyObject) this).GetValue(InfoListItemUC.Preview3UrlProperty);
      set => ((DependencyObject) this).SetValue(InfoListItemUC.Preview3UrlProperty, (object) value);
    }

    public bool IsTiltEnabled
    {
      get => (bool) ((DependencyObject) this).GetValue(InfoListItemUC.IsTiltEnabledProperty);
      set
      {
        ((DependencyObject) this).SetValue(InfoListItemUC.IsTiltEnabledProperty, (object) value);
      }
    }

    public InfoListItemUC()
    {
      this.InitializeComponent();
      this.textBlockContent.Text = "";
      ((UIElement) this.gridPreview1).put_Visibility((Visibility) 1);
      ((UIElement) this.gridPreview2).put_Visibility((Visibility) 1);
      ((UIElement) this.gridPreview3).put_Visibility((Visibility) 1);
    }

    private static void OnIconUrlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ((InfoListItemUC) d).UpdateIcon(e.NewValue as string);
    }

    private void UpdateIcon(string url) => this.borderIcon.put_Glyph(url);

    private static void OnInlinesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ((InfoListItemUC) d).UpdateInlines(e.NewValue as List<Inline>);
    }

    private void UpdateInlines(List<Inline> inlines) => this.textBlockContent.Text = "";

    private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ((InfoListItemUC) d).UpdateText(e.NewValue as string);
    }

    private void UpdateText(string text)
    {
      text = text != null ? text.Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ").Replace("  ", " ") : "";
      TextBlock textBlock = new TextBlock();
      double num1 = 306.0;
      ((FrameworkElement) textBlock).put_Width(num1);
      int num2 = 2;
      textBlock.put_TextWrapping((TextWrapping) num2);
      int num3 = 1;
      textBlock.put_LineStackingStrategy((LineStackingStrategy) num3);
      double num4 = 26.0;
      textBlock.put_LineHeight(num4);
      string str = text;
      textBlock.put_Text(str);
      this.textBlockContent.Text = UIStringFormatterHelper.SubstituteMentionsWithNames(text);
      double actualHeight = ((FrameworkElement) textBlock).ActualHeight;
    }

    private static void OnPreview1UrlChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      InfoListItemUC infoListItemUc = (InfoListItemUC) d;
      InfoListItemUC.UpdatePreviewUrl((UIElement) infoListItemUc.gridPreview1, infoListItemUc.imagePreview1, e.NewValue as string);
    }

    private static void OnPreview2UrlChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      InfoListItemUC infoListItemUc = (InfoListItemUC) d;
      InfoListItemUC.UpdatePreviewUrl((UIElement) infoListItemUc.gridPreview2, infoListItemUc.imagePreview2, e.NewValue as string);
    }

    private static void OnPreview3UrlChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      InfoListItemUC infoListItemUc = (InfoListItemUC) d;
      InfoListItemUC.UpdatePreviewUrl((UIElement) infoListItemUc.gridPreview3, infoListItemUc.imagePreview3, e.NewValue as string);
    }

    private static void UpdatePreviewUrl(UIElement gridPreview, Image imagePreview, string url)
    {
      gridPreview.put_Visibility(!string.IsNullOrEmpty(url) ? (Visibility) 0 : (Visibility) 1);
    }

    private static void OnIsTiltEnabledChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
    }

    private void TextBlockContent_OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
      TextBlock textBlock = (TextBlock) sender;
      double height = e.NewSize.Height;
      if (height <= textBlock.LineHeight || height <= ((FrameworkElement) textBlock).MaxHeight)
        return;
      ((FrameworkElement) this.textBlockContent).put_MaxHeight(((FrameworkElement) textBlock).MaxHeight);
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///UC/InfoListItemUC.xaml"), (ComponentResourceLocation) 0);
      this.gridRoot = (Grid) ((FrameworkElement) this).FindName("gridRoot");
      this.borderIcon = (IconUC) ((FrameworkElement) this).FindName("borderIcon");
      this.textBlockContent = (ScrollableTextBlock) ((FrameworkElement) this).FindName("textBlockContent");
      this.gridPreview1 = (Grid) ((FrameworkElement) this).FindName("gridPreview1");
      this.gridPreview2 = (Grid) ((FrameworkElement) this).FindName("gridPreview2");
      this.gridPreview3 = (Grid) ((FrameworkElement) this).FindName("gridPreview3");
      this.imagePreview3 = (Image) ((FrameworkElement) this).FindName("imagePreview3");
      this.imagePreview2 = (Image) ((FrameworkElement) this).FindName("imagePreview2");
      this.imagePreview1 = (Image) ((FrameworkElement) this).FindName("imagePreview1");
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void Connect(int connectionId, object target) => this._contentLoaded = true;
  }
}
