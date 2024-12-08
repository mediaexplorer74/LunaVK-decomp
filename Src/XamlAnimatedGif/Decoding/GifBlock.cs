
// Type: XamlAnimatedGif.Decoding.GifBlock
// Assembly: XamlAnimatedGif, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 02CD40CB-7FCE-4EFE-9D7D-B0095CDCA3E7
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\XamlAnimatedGif.dll

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using XamlAnimatedGif.Extensions;

#nullable disable
namespace XamlAnimatedGif.Decoding
{
  internal abstract class GifBlock
  {
    internal static async Task<GifBlock> ReadAsync(
      Stream stream,
      IEnumerable<GifExtension> controlExtensions)
    {
      int blockId = await stream.ReadByteAsync().ConfigureAwait(false);
      if (blockId < 0)
        throw new EndOfStreamException();
      switch (blockId)
      {
        case 33:
          return (GifBlock) await GifExtension.ReadAsync(stream, controlExtensions).ConfigureAwait(false);
        case 44:
          return (GifBlock) await GifFrame.ReadAsync(stream, controlExtensions).ConfigureAwait(false);
        case 59:
          return (GifBlock) await GifTrailer.ReadAsync().ConfigureAwait(false);
        default:
          throw GifHelpers.UnknownBlockTypeException(blockId);
      }
    }

    internal abstract GifBlockKind Kind { get; }
  }
}
