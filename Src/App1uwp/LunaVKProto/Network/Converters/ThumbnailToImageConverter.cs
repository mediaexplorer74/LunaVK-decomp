// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.Converters.ThumbnailToImageConverter
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using System;
using Windows.Storage.FileProperties;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

#nullable disable
namespace App1uwp.Network.Converters
{
  public class ThumbnailToImageConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, string language)
    {
      BitmapImage bitmapImage = (BitmapImage) null;
      if (value != null)
      {
        if (value.GetType() != typeof (StorageItemThumbnail))
          throw new ArgumentException("Expected a thumbnail");
        if (targetType != typeof (ImageSource))
          throw new ArgumentException("What are you trying to convert to here?");
        bitmapImage = new BitmapImage();
        ((BitmapSource) bitmapImage).SetSource((IRandomAccessStream) value);
      }
      return (object) bitmapImage;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
      throw new NotImplementedException();
    }
  }
}
