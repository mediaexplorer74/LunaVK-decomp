
// Type: XamlAnimatedGif.Decoding.GifFrame
// Assembly: XamlAnimatedGif, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 02CD40CB-7FCE-4EFE-9D7D-B0095CDCA3E7
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\XamlAnimatedGif.dll

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using XamlAnimatedGif.Extensions;

#nullable disable
namespace XamlAnimatedGif.Decoding
{
  internal class GifFrame : GifBlock
  {
    internal const int ImageSeparator = 44;

    public GifImageDescriptor Descriptor { get; private set; }

    public GifColor[] LocalColorTable { get; private set; }

    public IList<GifExtension> Extensions { get; private set; }

    public GifImageData ImageData { get; private set; }

    public GifGraphicControlExtension GraphicControl { get; set; }

    private GifFrame()
    {
    }

    internal override GifBlockKind Kind => GifBlockKind.GraphicRendering;

    internal static async Task<GifFrame> ReadAsync(
      Stream stream,
      IEnumerable<GifExtension> controlExtensions)
    {
      GifFrame frame = new GifFrame();
      await frame.ReadInternalAsync(stream, controlExtensions).ConfigureAwait(false);
      return frame;
    }

    private async Task ReadInternalAsync(Stream stream, IEnumerable<GifExtension> controlExtensions)
    {
      this.Descriptor = await GifImageDescriptor.ReadAsync(stream).ConfigureAwait(false);
      if (this.Descriptor.HasLocalColorTable)
        this.LocalColorTable = await GifHelpers.ReadColorTableAsync(stream, this.Descriptor.LocalColorTableSize).ConfigureAwait(false);
      this.ImageData = await GifImageData.ReadAsync(stream).ConfigureAwait(false);
      this.Extensions = (IList<GifExtension>) controlExtensions.ToList<GifExtension>().AsReadOnly<GifExtension>();
      this.GraphicControl = this.Extensions.OfType<GifGraphicControlExtension>().FirstOrDefault<GifGraphicControlExtension>();
    }
  }
}
