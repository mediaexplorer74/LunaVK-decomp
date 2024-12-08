// Decompiled with JetBrains decompiler
// Type: App1uwp.UC.Attachment.AttachDocumentUC
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.DataObjects;
using App1uwp.Network.Enums;
using App1uwp.Utils;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

#nullable disable
namespace App1uwp.UC.Attachment
{
  public sealed class AttachDocumentUC : UserControl, IComponentConnector
  {
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Grid Main;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public AttachDocumentUC() => this.InitializeComponent();

    public AttachDocumentUC(VKDocument doc)
      : this()
    {
      if (doc.type == VKDocumentType.IMAGE)
      {
        if (doc.preview == null || doc.preview.graffiti == null)
          return;
        Image image = new Image();
        image.put_Source((ImageSource) new BitmapImage(new Uri(doc.preview.graffiti.src)));
        ((FrameworkElement) image).put_Width((double) doc.preview.graffiti.width);
        ((FrameworkElement) image).put_Height((double) doc.preview.graffiti.height);
        ((ICollection<UIElement>) ((Panel) this.Main).Children).Add((UIElement) image);
      }
      else if (doc.type == VKDocumentType.UNKNOWN || doc.type == VKDocumentType.TEXT)
      {
        Grid grid = new Grid();
        ((FrameworkElement) grid).put_HorizontalAlignment((HorizontalAlignment) 0);
        ColumnDefinitionCollection columnDefinitions = grid.ColumnDefinitions;
        ColumnDefinition columnDefinition1 = new ColumnDefinition();
        columnDefinition1.put_Width(new GridLength(40.0));
        ColumnDefinition columnDefinition2 = columnDefinition1;
        ((ICollection<ColumnDefinition>) columnDefinitions).Add(columnDefinition2);
        ((ICollection<ColumnDefinition>) grid.ColumnDefinitions).Add(new ColumnDefinition());
        ((ICollection<RowDefinition>) grid.RowDefinitions).Add(new RowDefinition());
        ((ICollection<RowDefinition>) grid.RowDefinitions).Add(new RowDefinition());
        IconUC iconUc = new IconUC();
        iconUc.put_FontSize(35.0);
        ((FrameworkElement) iconUc).put_Margin(new Thickness(0.0, 0.0, 10.0, 0.0));
        iconUc.put_Glyph("\uE8FF");
        ((IconElement) iconUc).put_Foreground((Brush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "AccentBrushHigh"]);
        TextBlock textBlock1 = new TextBlock();
        textBlock1.put_Text(doc.title);
        textBlock1.put_Foreground((Brush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "AccentBrushHigh"]);
        textBlock1.put_FontSize((double) ((IDictionary<object, object>) Application.Current.Resources)[(object) "FontSizeContent"]);
        TextBlock textBlock2 = new TextBlock();
        textBlock2.put_Text(UIStringFormatterHelper.BytesForUI((double) doc.size));
        textBlock2.put_Foreground((Brush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "TextColorContent"]);
        textBlock2.put_FontSize((double) ((IDictionary<object, object>) Application.Current.Resources)[(object) "FontSizeSmall"]);
        Grid.SetRowSpan((FrameworkElement) iconUc, 2);
        Grid.SetColumn((FrameworkElement) textBlock1, 1);
        Grid.SetColumn((FrameworkElement) textBlock2, 1);
        Grid.SetRow((FrameworkElement) textBlock2, 1);
        ((ICollection<UIElement>) ((Panel) grid).Children).Add((UIElement) iconUc);
        ((ICollection<UIElement>) ((Panel) grid).Children).Add((UIElement) textBlock1);
        ((ICollection<UIElement>) ((Panel) grid).Children).Add((UIElement) textBlock2);
        ((ICollection<UIElement>) ((Panel) this.Main).Children).Add((UIElement) grid);
      }
      else if (doc.type == VKDocumentType.VIDEO)
        ((ICollection<UIElement>) ((Panel) this.Main).Children).Add((UIElement) new AttachVideoUC(new VKVideoBase()
        {
          title = doc.title
        }));
      else if (doc.type == VKDocumentType.AUDIO)
      {
        if (doc.preview != null)
          ((ICollection<UIElement>) ((Panel) this.Main).Children).Add((UIElement) new AttachVoiceMessageUC(doc.preview.audio_msg));
        else
          ((ICollection<UIElement>) ((Panel) this.Main).Children).Add((UIElement) new AttachAudioUC(new VKAudio()
          {
            title = doc.title,
            url = doc.url
          }));
      }
      else
      {
        if (doc.type != VKDocumentType.GIF || doc.preview.photo == null || doc.preview.photo.sizes.Count <= 2)
          return;
        Image image = new Image();
        image.put_Source((ImageSource) new BitmapImage(new Uri(doc.preview.photo.sizes[2].src)));
        image.put_Stretch((Stretch) 1);
        ((ICollection<UIElement>) ((Panel) this.Main).Children).Add((UIElement) image);
        ((ICollection<UIElement>) ((Panel) this.Main).Children).Add((UIElement) new GifViewerUC(doc.preview.photo.sizes[2].src, doc));
      }
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///UC/Attachment/AttachDocumentUC.xaml"), (ComponentResourceLocation) 0);
      this.Main = (Grid) ((FrameworkElement) this).FindName("Main");
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void Connect(int connectionId, object target) => this._contentLoaded = true;
  }
}
