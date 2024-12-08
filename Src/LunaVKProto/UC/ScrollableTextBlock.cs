// Decompiled with JetBrains decompiler
// Type: App1uwp.UC.ScrollableTextBlock
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Utils;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

#nullable disable
namespace App1uwp.UC
{
  public class ScrollableTextBlock : StackPanel
  {
    private static readonly Regex linksRegex = new Regex("(\\[.*?\\|.*?\\])|(https?://\\S+)|(#\\S+)");
    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof (Text), typeof (string), typeof (ScrollableTextBlock), new PropertyMetadata((object) "", new PropertyChangedCallback(ScrollableTextBlock.OnTextPropertyChanged)));
    public static readonly DependencyProperty BrushProperty = DependencyProperty.Register(nameof (Foreground), typeof (Brush), typeof (ScrollableTextBlock), (PropertyMetadata) null);
    public static readonly DependencyProperty FullProperty = DependencyProperty.Register(nameof (FullOnly), typeof (bool), typeof (ScrollableTextBlock), new PropertyMetadata((object) null));
    public double FontSize = (double) ((IDictionary<object, object>) Application.Current.Resources)[(object) "FontSizeContent"];

    public string Text
    {
      get => (string) ((DependencyObject) this).GetValue(ScrollableTextBlock.TextProperty);
      set => ((DependencyObject) this).SetValue(ScrollableTextBlock.TextProperty, (object) value);
    }

    public bool FullOnly
    {
      get => (bool) ((DependencyObject) this).GetValue(ScrollableTextBlock.FullProperty);
      set => ((DependencyObject) this).SetValue(ScrollableTextBlock.FullProperty, (object) value);
    }

    public Brush Foreground
    {
      get => (Brush) ((DependencyObject) this).GetValue(ScrollableTextBlock.BrushProperty);
      set => ((DependencyObject) this).SetValue(ScrollableTextBlock.BrushProperty, (object) value);
    }

    private static void OnTextPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      ((ScrollableTextBlock) d).ParseText((string) e.NewValue, false);
    }

    private void ParseText(string value, bool show_full)
    {
      if (this.FullOnly)
        show_full = true;
      bool flag = false;
      if (value == null)
        value = "";
      ((ICollection<UIElement>) ((Panel) this).Children).Clear();
      if (!show_full)
        value = UIStringFormatterHelper.CutTextGently(value, 300);
      if (value != this.Text)
      {
        value += "...";
        flag = true;
      }
      RichTextBlock richTextBlock = new RichTextBlock();
      richTextBlock.put_FontSize(this.FontSize);
      richTextBlock.put_Foreground(this.Foreground);
      Paragraph paragraph = new Paragraph();
      foreach (string str1 in ScrollableTextBlock.linksRegex.Split(value))
      {
        TypedEventHandler<Hyperlink, HyperlinkClickEventArgs> typedEventHandler = (TypedEventHandler<Hyperlink, HyperlinkClickEventArgs>) null;
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        ScrollableTextBlock.\u003C\u003Ec__DisplayClass7 cDisplayClass7 = new ScrollableTextBlock.\u003C\u003Ec__DisplayClass7();
        // ISSUE: reference to a compiler-generated field
        cDisplayClass7.block = str1;
        // ISSUE: reference to a compiler-generated field
        if (!string.IsNullOrEmpty(cDisplayClass7.block))
        {
          // ISSUE: reference to a compiler-generated field
          if (cDisplayClass7.block.StartsWith("http", StringComparison.OrdinalIgnoreCase))
          {
            Uri result = (Uri) null;
            // ISSUE: reference to a compiler-generated field
            if (Uri.TryCreate(cDisplayClass7.block, UriKind.Absolute, out result))
            {
              Hyperlink hyperlink = new Hyperlink();
              Func<TypedEventHandler<Hyperlink, HyperlinkClickEventArgs>, EventRegistrationToken> addMethod = new Func<TypedEventHandler<Hyperlink, HyperlinkClickEventArgs>, EventRegistrationToken>(hyperlink.add_Click);
              Action<EventRegistrationToken> removeMethod = new Action<EventRegistrationToken>(hyperlink.remove_Click);
              if (typedEventHandler == null)
              {
                // ISSUE: method pointer
                typedEventHandler = new TypedEventHandler<Hyperlink, HyperlinkClickEventArgs>((object) cDisplayClass7, __methodptr(\u003CParseText\u003Eb__5));
              }
              TypedEventHandler<Hyperlink, HyperlinkClickEventArgs> handler = typedEventHandler;
              WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<Hyperlink, HyperlinkClickEventArgs>>(addMethod, removeMethod, handler);
              ((TextElement) hyperlink).put_Foreground((Brush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "AccentBrushHigh"]);
              InlineCollection inlines = ((Span) hyperlink).Inlines;
              Run run1 = new Run();
              // ISSUE: reference to a compiler-generated field
              run1.put_Text(cDisplayClass7.block);
              Run run2 = run1;
              ((ICollection<Inline>) inlines).Add((Inline) run2);
              ((ICollection<Inline>) paragraph.Inlines).Add((Inline) hyperlink);
            }
            else
            {
              InlineCollection inlines = paragraph.Inlines;
              Run run3 = new Run();
              // ISSUE: reference to a compiler-generated field
              run3.put_Text(cDisplayClass7.block);
              Run run4 = run3;
              ((ICollection<Inline>) inlines).Add((Inline) run4);
            }
          }
          else
          {
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            if (cDisplayClass7.block.StartsWith("[", StringComparison.OrdinalIgnoreCase) && cDisplayClass7.block.EndsWith("]", StringComparison.OrdinalIgnoreCase))
            {
              // ISSUE: reference to a compiler-generated field
              string str2 = cDisplayClass7.block.Replace("[", "").Replace("]", "");
              InlineCollection inlines = paragraph.Inlines;
              Run run5 = new Run();
              run5.put_Text(str2.Split('|')[1]);
              ((TextElement) run5).put_Foreground((Brush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "AccentBrushHigh"]);
              Run run6 = run5;
              ((ICollection<Inline>) inlines).Add((Inline) run6);
            }
            else
            {
              // ISSUE: reference to a compiler-generated field
              if (cDisplayClass7.block.StartsWith("#", StringComparison.OrdinalIgnoreCase))
              {
                InlineCollection inlines = paragraph.Inlines;
                Run run7 = new Run();
                // ISSUE: reference to a compiler-generated field
                run7.put_Text(cDisplayClass7.block);
                ((TextElement) run7).put_Foreground((Brush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "AccentBrushHigh"]);
                Run run8 = run7;
                ((ICollection<Inline>) inlines).Add((Inline) run8);
              }
              else
              {
                InlineCollection inlines = paragraph.Inlines;
                Run run9 = new Run();
                // ISSUE: reference to a compiler-generated field
                run9.put_Text(cDisplayClass7.block);
                Run run10 = run9;
                ((ICollection<Inline>) inlines).Add((Inline) run10);
              }
            }
          }
        }
      }
      ((ICollection<Block>) richTextBlock.Blocks).Add((Block) paragraph);
      ((ICollection<UIElement>) ((Panel) this).Children).Add((UIElement) richTextBlock);
      if (show_full || !flag)
        return;
      Border border = new Border();
      string str = string.Format("{0}...", (object) "Показать полностью");
      TextBlock textBlock = new TextBlock();
      textBlock.put_FontWeight(FontWeights.Bold);
      textBlock.put_Text(str);
      textBlock.put_Foreground((Brush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "AccentBrushHigh"]);
      textBlock.put_FontSize(this.FontSize);
      border.put_Child((UIElement) textBlock);
      WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(((UIElement) border).add_Tapped), new Action<EventRegistrationToken>(((UIElement) border).remove_Tapped), new TappedEventHandler(this.TextBlockReadFull_OnTap));
      ((ICollection<UIElement>) ((Panel) this).Children).Add((UIElement) border);
    }

    private void TextBlockReadFull_OnTap(object sender, TappedRoutedEventArgs e)
    {
      this.ParseText(this.Text, true);
    }
  }
}
