
// Type: XamlAnimatedGif.Extensions.StreamExtensions
// Assembly: XamlAnimatedGif, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 02CD40CB-7FCE-4EFE-9D7D-B0095CDCA3E7
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\XamlAnimatedGif.dll

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace XamlAnimatedGif.Extensions
{
  internal static class StreamExtensions
  {
    public static async Task ReadAllAsync(
      this Stream stream,
      byte[] buffer,
      int offset,
      int count,
      CancellationToken cancellationToken = default (CancellationToken))
    {
      int n;
      for (int totalRead = 0; totalRead < count; totalRead += n)
      {
        n = await stream.ReadAsync(buffer, offset + totalRead, count - totalRead, cancellationToken);
        if (n == 0)
          throw new EndOfStreamException();
      }
    }

    public static void ReadAll(this Stream stream, byte[] buffer, int offset, int count)
    {
      int num;
      for (int index = 0; index < count; index += num)
      {
        num = stream.Read(buffer, offset + index, count - index);
        if (num == 0)
          throw new EndOfStreamException();
      }
    }

    public static async Task<int> ReadByteAsync(
      this Stream stream,
      CancellationToken cancellationToken = default (CancellationToken))
    {
      byte[] buffer = new byte[1];
      int n = await stream.ReadAsync(buffer, 0, 1, cancellationToken);
      return n != 0 ? (int) buffer[0] : -1;
    }

    public static Stream AsBuffered(this Stream stream) => stream;

    public static async Task CopyToAsync(
      this Stream source,
      Stream destination,
      IProgress<long> progress,
      int bufferSize = 81920,
      CancellationToken cancellationToken = default (CancellationToken))
    {
      byte[] buffer = new byte[bufferSize];
      long bytesCopied = 0;
      while (true)
      {
        int num = await source.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false);
        int bytesRead;
        if ((bytesRead = num) != 0)
        {
          await destination.WriteAsync(buffer, 0, bytesRead, cancellationToken).ConfigureAwait(false);
          bytesCopied += (long) bytesRead;
          progress.Report(bytesCopied);
        }
        else
          break;
      }
    }
  }
}
