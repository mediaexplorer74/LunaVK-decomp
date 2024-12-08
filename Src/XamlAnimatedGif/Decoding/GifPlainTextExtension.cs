
// Type: XamlAnimatedGif.Decoding.GifPlainTextExtension
// Assembly: XamlAnimatedGif, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 02CD40CB-7FCE-4EFE-9D7D-B0095CDCA3E7
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\XamlAnimatedGif.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using XamlAnimatedGif.Extensions;

#nullable disable
namespace XamlAnimatedGif.Decoding
{
  internal class GifPlainTextExtension : GifExtension
  {
    internal const int ExtensionLabel = 1;

    public int BlockSize { get; private set; }

    public int Left { get; private set; }

    public int Top { get; private set; }

    public int Width { get; private set; }

    public int Height { get; private set; }

    public int CellWidth { get; private set; }

    public int CellHeight { get; private set; }

    public int ForegroundColorIndex { get; private set; }

    public int BackgroundColorIndex { get; private set; }

    public string Text { get; private set; }

    public IList<GifExtension> Extensions { get; private set; }

    private GifPlainTextExtension()
    {
    }

    internal override GifBlockKind Kind => GifBlockKind.GraphicRendering;

    internal static async Task<GifPlainTextExtension> ReadAsync(
      Stream stream,
      IEnumerable<GifExtension> controlExtensions)
    {
      GifPlainTextExtension plainText = new GifPlainTextExtension();
      await plainText.ReadInternalAsync(stream, controlExtensions).ConfigureAwait(false);
      return plainText;
    }

    private async Task ReadInternalAsync(Stream stream, IEnumerable<GifExtension> controlExtensions)
    {
      byte[] bytes = new byte[13];
      await stream.ReadAllAsync(bytes, 0, bytes.Length).ConfigureAwait(false);
      this.BlockSize = (int) bytes[0];
      if (this.BlockSize != 12)
        throw GifHelpers.InvalidBlockSizeException("Plain Text Extension", 12, this.BlockSize);
      this.Left = (int) BitConverter.ToUInt16(bytes, 1);
      this.Top = (int) BitConverter.ToUInt16(bytes, 3);
      this.Width = (int) BitConverter.ToUInt16(bytes, 5);
      this.Height = (int) BitConverter.ToUInt16(bytes, 7);
      this.CellWidth = (int) bytes[9];
      this.CellHeight = (int) bytes[10];
      this.ForegroundColorIndex = (int) bytes[11];
      this.BackgroundColorIndex = (int) bytes[12];
      byte[] dataBytes = await GifHelpers.ReadDataBlocksAsync(stream).ConfigureAwait(false);
      this.Text = GifHelpers.GetString(dataBytes);
      this.Extensions = (IList<GifExtension>) controlExtensions.ToList<GifExtension>().AsReadOnly<GifExtension>();
    }
  }
}
