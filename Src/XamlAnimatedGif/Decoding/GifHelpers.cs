// Decompiled with JetBrains decompiler
// Type: XamlAnimatedGif.Decoding.GifHelpers
// Assembly: XamlAnimatedGif, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 02CD40CB-7FCE-4EFE-9D7D-B0095CDCA3E7
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\XamlAnimatedGif.dll

using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XamlAnimatedGif.Extensions;

#nullable disable
namespace XamlAnimatedGif.Decoding
{
  internal static class GifHelpers
  {
    public static async Task<string> ReadStringAsync(Stream stream, int length)
    {
      byte[] bytes = new byte[length];
      await stream.ReadAllAsync(bytes, 0, length).ConfigureAwait(false);
      return GifHelpers.GetString(bytes);
    }

    public static async Task ConsumeDataBlocksAsync(
      Stream sourceStream,
      CancellationToken cancellationToken = default (CancellationToken))
    {
      await GifHelpers.CopyDataBlocksToStreamAsync(sourceStream, Stream.Null, cancellationToken);
    }

    public static async Task<byte[]> ReadDataBlocksAsync(
      Stream stream,
      CancellationToken cancellationToken = default (CancellationToken))
    {
      byte[] array;
      using (MemoryStream ms = new MemoryStream())
      {
        await GifHelpers.CopyDataBlocksToStreamAsync(stream, (Stream) ms, cancellationToken);
        array = ms.ToArray();
      }
      return array;
    }

    public static async Task CopyDataBlocksToStreamAsync(
      Stream sourceStream,
      Stream targetStream,
      CancellationToken cancellationToken = default (CancellationToken))
    {
      byte[] buffer = new byte[(int) byte.MaxValue];
      while (true)
      {
        int num = await sourceStream.ReadByteAsync(cancellationToken);
        int len;
        if ((len = num) > 0)
        {
          await sourceStream.ReadAllAsync(buffer, 0, len, cancellationToken).ConfigureAwait(false);
          await targetStream.WriteAsync(buffer, 0, len, cancellationToken);
        }
        else
          break;
      }
    }

    public static async Task<GifColor[]> ReadColorTableAsync(Stream stream, int size)
    {
      int length = 3 * size;
      byte[] bytes = new byte[length];
      await stream.ReadAllAsync(bytes, 0, length).ConfigureAwait(false);
      GifColor[] colorTable = new GifColor[size];
      for (int index = 0; index < size; ++index)
      {
        byte r = bytes[3 * index];
        byte g = bytes[3 * index + 1];
        byte b = bytes[3 * index + 2];
        colorTable[index] = new GifColor(r, g, b);
      }
      return colorTable;
    }

    public static bool IsNetscapeExtension(GifApplicationExtension ext)
    {
      return ext.ApplicationIdentifier == "NETSCAPE" && GifHelpers.GetString(ext.AuthenticationCode) == "2.0";
    }

    public static ushort GetRepeatCount(GifApplicationExtension ext)
    {
      return ext.Data.Length >= 3 ? BitConverter.ToUInt16(ext.Data, 1) : (ushort) 1;
    }

    public static Exception UnknownBlockTypeException(int blockId)
    {
      return (Exception) new XamlAnimatedGif.Decoding.UnknownBlockTypeException("Unknown block type: 0x" + blockId.ToString("x2"));
    }

    public static Exception UnknownExtensionTypeException(int extensionLabel)
    {
      return (Exception) new XamlAnimatedGif.Decoding.UnknownExtensionTypeException("Unknown extension type: 0x" + extensionLabel.ToString("x2"));
    }

    public static Exception InvalidBlockSizeException(
      string blockName,
      int expectedBlockSize,
      int actualBlockSize)
    {
      return (Exception) new XamlAnimatedGif.Decoding.InvalidBlockSizeException(string.Format("Invalid block size for {0}. Expected {1}, but was {2}", (object) blockName, (object) expectedBlockSize, (object) actualBlockSize));
    }

    public static Exception InvalidSignatureException(string signature)
    {
      return (Exception) new XamlAnimatedGif.Decoding.InvalidSignatureException("Invalid file signature: " + signature);
    }

    public static Exception UnsupportedVersionException(string version)
    {
      return (Exception) new UnsupportedGifVersionException("Unsupported version: " + version);
    }

    public static string GetString(byte[] bytes) => GifHelpers.GetString(bytes, 0, bytes.Length);

    public static string GetString(byte[] bytes, int index, int count)
    {
      return Encoding.UTF8.GetString(bytes, index, count);
    }
  }
}
