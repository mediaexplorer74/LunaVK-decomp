// Decompiled with JetBrains decompiler
// Type: App1uwp.Framework.ImageExtensions
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

#nullable disable
namespace App1uwp.Framework
{
  public static class ImageExtensions
  {
    public static readonly DependencyProperty CacheUriProperty = DependencyProperty.RegisterAttached("CacheUri", typeof (Uri), typeof (ImageExtensions), new PropertyMetadata((object) null, new PropertyChangedCallback(ImageExtensions.OnCacheUriChanged)));
    public static readonly DependencyProperty CacheUriImageBrush = DependencyProperty.RegisterAttached(nameof (CacheUriImageBrush), typeof (Uri), typeof (ImageExtensions), new PropertyMetadata((object) null, new PropertyChangedCallback(ImageExtensions.OnImageBrushMultiResSourceChanged)));

    public static Uri GetCacheUri(DependencyObject d)
    {
      return (Uri) d.GetValue(ImageExtensions.CacheUriProperty);
    }

    public static void SetCacheUri(DependencyObject d, Uri value)
    {
      d.SetValue(ImageExtensions.CacheUriProperty, (object) value);
    }

    public static Uri GetCacheUriImageBrush(ImageBrush obj)
    {
      return (Uri) ((DependencyObject) obj).GetValue(ImageExtensions.CacheUriImageBrush);
    }

    public static void SetCacheUriImageBrush(ImageBrush obj, Uri value)
    {
      ((DependencyObject) obj).SetValue(ImageExtensions.CacheUriImageBrush, (object) value);
    }

    private static void OnCacheUriChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      Uri uri = (Uri) d.GetValue(ImageExtensions.CacheUriProperty);
      Image image = (Image) d;
      if (uri != (Uri) null)
      {
        try
        {
          ImageExtensions.ImageFromCache3(uri.AbsoluteUri, (Action<string>) (cacheUri => image.put_Source((ImageSource) new BitmapImage(new Uri(cacheUri)))));
        }
        catch (Exception ex)
        {
        }
      }
      else
        image.put_Source((ImageSource) null);
    }

    private static void OnImageBrushMultiResSourceChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      ImageBrush imageBrush = (ImageBrush) d;
      string path = e.NewValue.ToString();
      if (path != null)
      {
        try
        {
          ImageExtensions.ImageFromCache3(path, (Action<string>) (cacheUri => imageBrush.put_ImageSource((ImageSource) new BitmapImage(new Uri(cacheUri)))));
        }
        catch (Exception ex)
        {
        }
      }
      else
        imageBrush.put_ImageSource((ImageSource) null);
    }

    public static async void ImageFromCache3(string path, Action<string> callback)
    {
      string cacheUri = await ImageExtensions.ImageFromCache2(path);
      if (callback == null || cacheUri == null)
        return;
      callback(cacheUri);
    }

    public static async Task<string> ImageFromCache2(string path)
    {
      int ru = path.IndexOf(".com") + 5;
      string new_path = path.Substring(ru).Replace("/", "\\");
      StorageFolder localFolder = ApplicationData.Current.LocalFolder;
      try
      {
        Stream p = await ((IStorageFolder) localFolder).OpenStreamForReadAsync(new_path);
        p.Dispose();
        return localFolder.Path + "\\" + new_path;
      }
      catch (FileNotFoundException ex)
      {
      }
      catch (Exception ex)
      {
      }
      try
      {
        StorageFile storageFile = await localFolder.CreateFileAsync(new_path, (CreationCollisionOption) 3);
        Uri Website = new Uri(path);
        HttpClient http = new HttpClient();
        byte[] image_from_web_as_bytes = await http.GetByteArrayAsync(Website);
        ImageExtensions.MakeFolders(localFolder, path.Substring(ru));
        Stream outputStream = await ((IStorageFile) storageFile).OpenStreamForWriteAsync();
        outputStream.Write(image_from_web_as_bytes, 0, image_from_web_as_bytes.Length);
        outputStream.Position = 0L;
        outputStream.Dispose();
        return localFolder.Path + "\\" + new_path;
      }
      catch
      {
      }
      return (string) null;
    }

    private static async void MakeFolders(StorageFolder localFolder, string path)
    {
      int slash = path.IndexOf("/");
      if (slash <= 0)
        return;
      string new_path = path.Substring(0, slash);
      StorageFolder opened_folder = await localFolder.CreateFolderAsync(new_path, (CreationCollisionOption) 3);
      string very_new_path = path.Remove(0, new_path.Length + 1);
      ImageExtensions.MakeFolders(opened_folder, very_new_path);
    }
  }
}
