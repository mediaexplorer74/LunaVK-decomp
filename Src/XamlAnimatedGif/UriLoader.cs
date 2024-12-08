
// Type: XamlAnimatedGif.UriLoader
// Assembly: XamlAnimatedGif, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 02CD40CB-7FCE-4EFE-9D7D-B0095CDCA3E7
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\XamlAnimatedGif.dll

using System;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources.Core;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Web.Http;

#nullable disable
namespace XamlAnimatedGif
{
  internal class UriLoader
  {
    private static async Task DeleteTempFileAsync(string fileName)
    {
      try
      {
        StorageFile file = await ApplicationData.Current.TemporaryFolder.GetFileAsync(fileName);
        await file.DeleteAsync();
      }
      catch (FileNotFoundException ex)
      {
      }
    }

    private static async Task<Stream> CreateTempFileStreamAsync(string fileName)
    {
      IStorageFile file = (IStorageFile) await ApplicationData.Current.TemporaryFolder.CreateFileAsync(fileName, (CreationCollisionOption) 1);
      return await file.OpenStreamForWriteAsync();
    }

    private static async Task<Stream> GetStreamFromUriCoreAsync(Uri uri)
    {
      switch (uri.Scheme)
      {
        case "ms-appx":
        case "ms-appdata":
          StorageFile file1 = await StorageFile.GetFileFromApplicationUriAsync(uri);
          return await ((IStorageFile) file1).OpenStreamForReadAsync();
        case "ms-resource":
          ResourceManager rm = ResourceManager.Current;
          ResourceContext context = ResourceContext.GetForCurrentView();
          ResourceCandidate candidate = rm.MainResourceMap.GetValue(uri.LocalPath, context);
          if (candidate == null || !candidate.IsMatch)
            throw new Exception("Resource not found");
          StorageFile file2 = await candidate.GetValueAsFileAsync();
          return await ((IStorageFile) file2).OpenStreamForReadAsync();
        case "file":
          StorageFile file3 = await StorageFile.GetFileFromPathAsync(uri.LocalPath);
          return await ((IStorageFile) file3).OpenStreamForReadAsync();
        default:
          throw new NotSupportedException("Only ms-appx:, ms-appdata:, ms-resource:, http:, https: and file: URIs are supported");
      }
    }

    private static string GetCacheFileName(Uri uri)
    {
      return CryptographicBuffer.EncodeToHexString(HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Sha1).HashData(CryptographicBuffer.CreateFromByteArray(Encoding.UTF8.GetBytes(uri.AbsoluteUri))));
    }

    private static async Task<Stream> OpenTempFileStreamAsync(string fileName)
    {
      IStorageFile file;
      try
      {
        file = (IStorageFile) await ApplicationData.Current.TemporaryFolder.GetFileAsync(fileName);
      }
      catch (FileNotFoundException ex)
      {
        return (Stream) null;
      }
      return await file.OpenStreamForReadAsync();
    }

    public Task<Stream> GetStreamFromUriAsync(Uri uri, IProgress<int> progress)
    {
      return uri.IsAbsoluteUri && (uri.Scheme == "http" || uri.Scheme == "https") ? UriLoader.GetNetworkStreamAsync(uri, progress) : UriLoader.GetStreamFromUriCoreAsync(uri);
    }

    private static async Task<Stream> GetNetworkStreamAsync(Uri uri, IProgress<int> progress)
    {
      string cacheFileName = UriLoader.GetCacheFileName(uri);
      Stream cacheStream = await UriLoader.OpenTempFileStreamAsync(cacheFileName);
      progress.Report(100);
      return await UriLoader.OpenTempFileStreamAsync(cacheFileName);
    }

    public event EventHandler<UriLoader.DownloadProgressChangedArgs> DownloadProgressChanged;

    private async Task DownloadToCacheFileAsync(
      Uri uri,
      string fileName,
      CancellationToken cancellationToken)
    {
      Exception obj = (Exception) null;
      int num = 0;
      try
      {
        HttpClient httpClient = new HttpClient();
        try
        {
          IBuffer source = await httpClient.GetBufferAsync(uri).AsTask<IBuffer, HttpProgress>(cancellationToken, (IProgress<HttpProgress>) new Progress<HttpProgress>((Action<HttpProgress>) (progress =>
          {
            ulong? totalBytesToReceive = progress.TotalBytesToReceive;
            if (!totalBytesToReceive.HasValue)
              return;
            double percentage = Math.Round((double) progress.BytesReceived * 100.0 / (double) totalBytesToReceive.Value, 2);
            EventHandler<UriLoader.DownloadProgressChangedArgs> downloadProgressChanged = this.DownloadProgressChanged;
            if (downloadProgressChanged == null)
              return;
            downloadProgressChanged((object) this, new UriLoader.DownloadProgressChangedArgs(uri, percentage));
          })));
          Stream stream = source.AsStream();
          try
          {
            Stream stream2 = await UriLoader.CreateTempFileStreamAsync(fileName);
            try
            {
              await stream.CopyToAsync(stream2);
            }
            finally
            {
              stream2?.Dispose();
            }
            stream2 = (Stream) null;
          }
          finally
          {
            stream?.Dispose();
          }
          stream = (Stream) null;
        }
        finally
        {
          httpClient?.Dispose();
        }
        httpClient = (HttpClient) null;
      }
      catch (Exception ex)
      {
        num = 1;
        obj = ex;
      }
      if (num == 1)
      {
        await UriLoader.DeleteTempFileAsync(fileName);
        ExceptionDispatchInfo.Capture(obj ?? throw obj).Throw();
      }
      obj = (Exception) null;
    }

    public class DownloadProgressChangedArgs
    {
      public Uri Uri { get; private set; }

      public double Percentage { get; private set; }

      public DownloadProgressChangedArgs(Uri uri, double percentage)
      {
        this.Uri = uri;
        this.Percentage = percentage;
      }
    }
  }
}
