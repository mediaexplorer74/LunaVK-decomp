// Decompiled with JetBrains decompiler
// Type: App1uwp.Framework.ProfileImageLoader
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

#nullable disable
namespace App1uwp.Framework
{
  public static class ProfileImageLoader
  {
    public static Dictionary<string, string> _downloadedDictionary = new Dictionary<string, string>();
    private static readonly Task _task = new Task(new Action(ProfileImageLoader.WorkerThreadProc));
    private static readonly object _syncBlock = new object();
    private static ProfileImageLoader.PendingRequest _currentRequest;
    private static readonly Queue<ProfileImageLoader.PendingRequest> _pendingRequests = new Queue<ProfileImageLoader.PendingRequest>();

    static ProfileImageLoader() => ProfileImageLoader._task.Start();

    private static void WorkerThreadProc()
    {
      while (true)
      {
        lock (ProfileImageLoader._syncBlock)
        {
          if (ProfileImageLoader._pendingRequests.Count == 0 || ProfileImageLoader._currentRequest != null)
          {
            Monitor.Wait(ProfileImageLoader._syncBlock, 300);
          }
          else
          {
            ProfileImageLoader._currentRequest = ProfileImageLoader._pendingRequests.Dequeue();
            ProfileImageLoader.DownloadFileAndCache(ProfileImageLoader._currentRequest.Uri);
          }
        }
      }
    }

    private static async void DownloadFileAndCache(string uri)
    {
      string ret = (string) null;
      if (ProfileImageLoader._downloadedDictionary.ContainsKey(uri))
      {
        ret = ProfileImageLoader._downloadedDictionary[uri];
      }
      else
      {
        HttpClient http = new HttpClient();
        byte[] image_from_web_as_bytes = await http.GetByteArrayAsync(uri);
        ret = await CacheManager.TrySaveRawCachedData(image_from_web_as_bytes, uri.GetHashCode().ToString() + ".jpg");
      }
      if (ret == null)
        return;
      Execute.ExecuteOnUIThread((Action) (() =>
      {
        BitmapImage bitmapImage = new BitmapImage();
        bitmapImage.put_CreateOptions((BitmapCreateOptions) 8);
        bitmapImage.put_UriSource(new Uri(ret));
        ProfileImageLoader._currentRequest.Image.put_Source((ImageSource) bitmapImage);
        ProfileImageLoader._currentRequest = (ProfileImageLoader.PendingRequest) null;
        if (ProfileImageLoader._downloadedDictionary.ContainsKey(uri))
          return;
        ProfileImageLoader._downloadedDictionary.Add(uri, ret);
      }));
    }

    public static void SetUriSource(Image image, string value)
    {
      if (image == null)
        throw new ArgumentNullException("obj");
      if (value == null)
        return;
      if (ProfileImageLoader._downloadedDictionary.ContainsKey(value))
      {
        BitmapImage bitmapImage = new BitmapImage();
        bitmapImage.put_CreateOptions((BitmapCreateOptions) 8);
        bitmapImage.put_UriSource(new Uri(ProfileImageLoader._downloadedDictionary[value]));
        image.put_Source((ImageSource) bitmapImage);
      }
      else
        ProfileImageLoader.AddPendingRequest(image, value);
    }

    private static void AddPendingRequest(Image image, string uri)
    {
      lock (ProfileImageLoader._syncBlock)
      {
        ProfileImageLoader._pendingRequests.Enqueue(new ProfileImageLoader.PendingRequest(image, uri));
        Monitor.Pulse(ProfileImageLoader._syncBlock);
      }
    }

    private class PendingRequest
    {
      public Image Image { get; private set; }

      public string Uri { get; private set; }

      public PendingRequest(Image image, string uri)
      {
        this.Image = image;
        this.Uri = uri;
      }
    }

    public class SerializedData : IBinarySerializable
    {
      public List<string> DownloadedUris { get; set; }

      public SerializedData() => this.DownloadedUris = new List<string>();

      public void Write(BinaryWriter writer) => writer.WriteList(this.DownloadedUris);

      public void Read(BinaryReader reader) => this.DownloadedUris = reader.ReadList();
    }
  }
}
