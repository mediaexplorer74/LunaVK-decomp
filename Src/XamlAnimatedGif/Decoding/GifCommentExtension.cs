
// Type: XamlAnimatedGif.Decoding.GifCommentExtension
// Assembly: XamlAnimatedGif, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 02CD40CB-7FCE-4EFE-9D7D-B0095CDCA3E7
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\XamlAnimatedGif.dll

using System.IO;
using System.Threading.Tasks;

#nullable disable
namespace XamlAnimatedGif.Decoding
{
  internal class GifCommentExtension : GifExtension
  {
    internal const int ExtensionLabel = 254;

    public string Text { get; private set; }

    private GifCommentExtension()
    {
    }

    internal override GifBlockKind Kind => GifBlockKind.SpecialPurpose;

    internal static async Task<GifCommentExtension> ReadAsync(Stream stream)
    {
      GifCommentExtension comment = new GifCommentExtension();
      await comment.ReadInternalAsync(stream).ConfigureAwait(false);
      return comment;
    }

    private async Task ReadInternalAsync(Stream stream)
    {
      byte[] bytes = await GifHelpers.ReadDataBlocksAsync(stream).ConfigureAwait(false);
      if (bytes == null)
        return;
      this.Text = GifHelpers.GetString(bytes);
    }
  }
}
