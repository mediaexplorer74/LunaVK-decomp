// Decompiled with JetBrains decompiler
// Type: XamlAnimatedGif.Decoding.GifApplicationExtension
// Assembly: XamlAnimatedGif, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 02CD40CB-7FCE-4EFE-9D7D-B0095CDCA3E7
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\XamlAnimatedGif.dll

using System;
using System.IO;
using System.Threading.Tasks;
using XamlAnimatedGif.Extensions;

#nullable disable
namespace XamlAnimatedGif.Decoding
{
  internal class GifApplicationExtension : GifExtension
  {
    internal const int ExtensionLabel = 255;

    public int BlockSize { get; private set; }

    public string ApplicationIdentifier { get; private set; }

    public byte[] AuthenticationCode { get; private set; }

    public byte[] Data { get; private set; }

    private GifApplicationExtension()
    {
    }

    internal override GifBlockKind Kind => GifBlockKind.SpecialPurpose;

    internal static async Task<GifApplicationExtension> ReadAsync(Stream stream)
    {
      GifApplicationExtension ext = new GifApplicationExtension();
      await ext.ReadInternalAsync(stream).ConfigureAwait(false);
      return ext;
    }

    private async Task ReadInternalAsync(Stream stream)
    {
      byte[] bytes = new byte[12];
      await stream.ReadAllAsync(bytes, 0, bytes.Length).ConfigureAwait(false);
      this.BlockSize = (int) bytes[0];
      if (this.BlockSize != 11)
        throw GifHelpers.InvalidBlockSizeException("Application Extension", 11, this.BlockSize);
      this.ApplicationIdentifier = GifHelpers.GetString(bytes, 1, 8);
      byte[] authCode = new byte[3];
      Array.Copy((Array) bytes, 9, (Array) authCode, 0, 3);
      this.AuthenticationCode = authCode;
      this.Data = await GifHelpers.ReadDataBlocksAsync(stream).ConfigureAwait(false);
    }
  }
}
