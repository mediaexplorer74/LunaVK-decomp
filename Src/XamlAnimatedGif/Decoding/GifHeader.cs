
// Type: XamlAnimatedGif.Decoding.GifHeader
// Assembly: XamlAnimatedGif, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 02CD40CB-7FCE-4EFE-9D7D-B0095CDCA3E7
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\XamlAnimatedGif.dll

using System.IO;
using System.Threading.Tasks;

#nullable disable
namespace XamlAnimatedGif.Decoding
{
  internal class GifHeader : GifBlock
  {
    public string Signature { get; private set; }

    public string Version { get; private set; }

    public GifLogicalScreenDescriptor LogicalScreenDescriptor { get; private set; }

    private GifHeader()
    {
    }

    internal override GifBlockKind Kind => GifBlockKind.Other;

    internal static async Task<GifHeader> ReadAsync(Stream stream)
    {
      GifHeader header = new GifHeader();
      await header.ReadInternalAsync(stream).ConfigureAwait(false);
      return header;
    }

    private async Task ReadInternalAsync(Stream stream)
    {
      this.Signature = await GifHelpers.ReadStringAsync(stream, 3).ConfigureAwait(false);
      if (this.Signature != "GIF")
        throw GifHelpers.InvalidSignatureException(this.Signature);
      this.Version = await GifHelpers.ReadStringAsync(stream, 3).ConfigureAwait(false);
      if (this.Version != "87a" && this.Version != "89a")
        throw GifHelpers.UnsupportedVersionException(this.Version);
      this.LogicalScreenDescriptor = await GifLogicalScreenDescriptor.ReadAsync(stream).ConfigureAwait(false);
    }
  }
}
