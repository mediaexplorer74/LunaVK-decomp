
// Type: XamlAnimatedGif.Decoding.GifGraphicControlExtension
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
  internal class GifGraphicControlExtension : GifExtension
  {
    internal const int ExtensionLabel = 249;

    public int BlockSize { get; private set; }

    public GifFrameDisposalMethod DisposalMethod { get; private set; }

    public bool UserInput { get; private set; }

    public bool HasTransparency { get; private set; }

    public int Delay { get; private set; }

    public int TransparencyIndex { get; private set; }

    private GifGraphicControlExtension()
    {
    }

    internal override GifBlockKind Kind => GifBlockKind.Control;

    internal static async Task<GifGraphicControlExtension> ReadAsync(Stream stream)
    {
      GifGraphicControlExtension ext = new GifGraphicControlExtension();
      await ext.ReadInternalAsync(stream).ConfigureAwait(false);
      return ext;
    }

    private async Task ReadInternalAsync(Stream stream)
    {
      byte[] bytes = new byte[6];
      await stream.ReadAllAsync(bytes, 0, bytes.Length).ConfigureAwait(false);
      this.BlockSize = (int) bytes[0];
      if (this.BlockSize != 4)
        throw GifHelpers.InvalidBlockSizeException("Graphic Control Extension", 4, this.BlockSize);
      byte packedFields = bytes[1];
      this.DisposalMethod = (GifFrameDisposalMethod) (((int) packedFields & 28) >> 2);
      this.UserInput = ((int) packedFields & 2) != 0;
      this.HasTransparency = ((int) packedFields & 1) != 0;
      this.Delay = (int) BitConverter.ToUInt16(bytes, 2) * 10;
      this.TransparencyIndex = (int) bytes[4];
    }
  }
}
