
// Type: XamlAnimatedGif.Decoding.GifImageData
// Assembly: XamlAnimatedGif, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 02CD40CB-7FCE-4EFE-9D7D-B0095CDCA3E7
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\XamlAnimatedGif.dll

using System.IO;
using System.Threading.Tasks;

#nullable disable
namespace XamlAnimatedGif.Decoding
{
  internal class GifImageData
  {
    public byte LzwMinimumCodeSize { get; set; }

    public long CompressedDataStartOffset { get; set; }

    private GifImageData()
    {
    }

    internal static async Task<GifImageData> ReadAsync(Stream stream)
    {
      GifImageData imgData = new GifImageData();
      await imgData.ReadInternalAsync(stream).ConfigureAwait(false);
      return imgData;
    }

    private async Task ReadInternalAsync(Stream stream)
    {
      this.LzwMinimumCodeSize = (byte) stream.ReadByte();
      this.CompressedDataStartOffset = stream.Position;
      await GifHelpers.ConsumeDataBlocksAsync(stream).ConfigureAwait(false);
    }
  }
}
