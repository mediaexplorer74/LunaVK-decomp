// Decompiled with JetBrains decompiler
// Type: App1uwp.Framework.ImageLoader
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Utils;
using System;
using System.IO;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

#nullable disable
namespace App1uwp.Framework
{
  public class ImageLoader
  {
    public static readonly DependencyProperty UriSourceProperty = DependencyProperty.RegisterAttached("UriSource", typeof (string), typeof (ImageLoader), new PropertyMetadata((object) null, new PropertyChangedCallback(ImageLoader.OnUriSourceChanged)));
    public static readonly DependencyProperty StreamSourceProperty = DependencyProperty.RegisterAttached("StreamSource", typeof (Stream), typeof (ImageLoader), new PropertyMetadata((object) new PropertyChangedCallback(ImageLoader.OnStreamSourceChanged)));
    public static readonly DependencyProperty ImageBrushSourceProperty = DependencyProperty.RegisterAttached("ImageBrushSource", typeof (string), typeof (ImageLoader), new PropertyMetadata((object) new PropertyChangedCallback(ImageLoader.OnImageBrushSourceChanged)));
    public static readonly DependencyProperty ImageBrushMultiResSourceProperty = DependencyProperty.RegisterAttached("ImageBrushMultiResSource", typeof (string), typeof (ImageLoader), new PropertyMetadata((object) new PropertyChangedCallback(ImageLoader.OnImageBrushMultiResSourceChanged)));

    public static string GetUriSource(Image obj)
    {
      return obj != null ? (string) ((DependencyObject) obj).GetValue(ImageLoader.UriSourceProperty) : throw new ArgumentNullException(nameof (obj));
    }

    public static void SetUriSource(Image obj, string value)
    {
      if (obj == null)
        throw new ArgumentNullException(nameof (obj));
      ((DependencyObject) obj).SetValue(ImageLoader.UriSourceProperty, (object) value);
    }

    public static string GetStreamSource(Image obj)
    {
      return obj != null ? (string) ((DependencyObject) obj).GetValue(ImageLoader.StreamSourceProperty) : throw new ArgumentNullException(nameof (obj));
    }

    public static void SetStreamSource(Image obj, Stream value)
    {
      if (obj == null)
        throw new ArgumentNullException(nameof (obj));
      ((DependencyObject) obj).SetValue(ImageLoader.StreamSourceProperty, (object) value);
    }

    public static string GetImageBrushSource(ImageBrush obj)
    {
      return obj != null ? (string) ((DependencyObject) obj).GetValue(ImageLoader.ImageBrushSourceProperty) : throw new ArgumentException(nameof (obj));
    }

    public static void SetImageBrushSource(ImageBrush obj, string value)
    {
      if (obj == null)
        throw new ArgumentException(nameof (obj));
      ((DependencyObject) obj).SetValue(ImageLoader.ImageBrushSourceProperty, (object) value);
    }

    public static string GetImageBrushMultiResSource(ImageBrush obj)
    {
      return obj != null ? (string) ((DependencyObject) obj).GetValue(ImageLoader.ImageBrushMultiResSourceProperty) : throw new ArgumentException(nameof (obj));
    }

    public static void SetImageBrushMultiResSource(ImageBrush obj, string value)
    {
      if (obj == null)
        throw new ArgumentException(nameof (obj));
      ((DependencyObject) obj).SetValue(ImageLoader.ImageBrushMultiResSourceProperty, (object) value);
    }

    private static void OnImageBrushSourceChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      ImageLoader.ProcessImageBrush((ImageBrush) d, e.NewValue);
    }

    private static void OnImageBrushMultiResSourceChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      ImageBrush ib = (ImageBrush) d;
      if (e.NewValue == null)
      {
        ImageLoader.ProcessImageBrush(ib, (object) null);
      }
      else
      {
        string newSource = e.NewValue.ToString();
        int num = DesignMode.DesignModeEnabled ? 1 : 0;
        ImageLoader.ProcessImageBrush(ib, (object) newSource);
      }
    }

    private static void ProcessImageBrush(ImageBrush ib, object newSource)
    {
      if (newSource == null)
      {
        ib.put_ImageSource((ImageSource) null);
      }
      else
      {
        Uri uri = new Uri((string) newSource, UriKind.RelativeOrAbsolute);
        BitmapImage bitmapImage = new BitmapImage();
        bitmapImage.put_CreateOptions((BitmapCreateOptions) 18);
        bitmapImage.put_UriSource(uri);
        ib.put_ImageSource((ImageSource) bitmapImage);
      }
    }

    private static void OnStreamSourceChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      Image image = (Image) d;
      if (!(e.NewValue is Stream newValue))
      {
        image.put_Source((ImageSource) null);
      }
      else
      {
        BitmapImage bitmapImage = new BitmapImage();
        ((BitmapSource) bitmapImage).SetSource(newValue.AsRandomAccessStream());
        image.put_Source((ImageSource) bitmapImage);
      }
    }

    private static void OnUriSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
    {
      ImageLoader.HandleUriChangeLowProfile((Image) o, (string) e.NewValue);
    }

    public static void SetSourceForImage(Image image, string uriStr, bool animateOpacity = false)
    {
      ImageLoader.SetUriSource(image, uriStr);
    }

    private static void HandleUriChangeLowProfile(Image image, string uriStr)
    {
      uriStr.ConvertToUri();
      ProfileImageLoader.SetUriSource(image, uriStr);
    }
  }
}
