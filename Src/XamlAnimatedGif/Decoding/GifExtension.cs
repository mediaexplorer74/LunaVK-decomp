
// Type: XamlAnimatedGif.Decoding.GifExtension
// Assembly: XamlAnimatedGif, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 02CD40CB-7FCE-4EFE-9D7D-B0095CDCA3E7
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\XamlAnimatedGif.dll

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

#nullable disable
namespace XamlAnimatedGif.Decoding
{
  internal abstract class GifExtension : GifBlock
  {
    internal const int ExtensionIntroducer = 33;

    internal static async Task<GifExtension> ReadAsync(
      Stream stream,
      IEnumerable<GifExtension> controlExtensions)
    {
      int label = stream.ReadByte();
      if (label < 0)
        throw new EndOfStreamException();
      switch (label)
      {
        case 1:
          return (GifExtension) await GifPlainTextExtension.ReadAsync(stream, controlExtensions).ConfigureAwait(false);
        case 249:
          return (GifExtension) await GifGraphicControlExtension.ReadAsync(stream).ConfigureAwait(false);
        case 254:
          return (GifExtension) await GifCommentExtension.ReadAsync(stream).ConfigureAwait(false);
        case (int) byte.MaxValue:
          return (GifExtension) await GifApplicationExtension.ReadAsync(stream).ConfigureAwait(false);
        default:
          throw GifHelpers.UnknownExtensionTypeException(label);
      }
    }
  }
}
