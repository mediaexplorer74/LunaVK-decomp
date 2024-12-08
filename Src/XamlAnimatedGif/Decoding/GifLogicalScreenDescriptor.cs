// Decompiled with JetBrains decompiler
// Type: XamlAnimatedGif.Decoding.GifLogicalScreenDescriptor
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
  internal class GifLogicalScreenDescriptor : IGifRect
  {
    public int Width { get; private set; }

    public int Height { get; private set; }

    public bool HasGlobalColorTable { get; private set; }

    public int ColorResolution { get; private set; }

    public bool IsGlobalColorTableSorted { get; private set; }

    public int GlobalColorTableSize { get; private set; }

    public int BackgroundColorIndex { get; private set; }

    public double PixelAspectRatio { get; private set; }

    internal static async Task<GifLogicalScreenDescriptor> ReadAsync(Stream stream)
    {
      GifLogicalScreenDescriptor descriptor = new GifLogicalScreenDescriptor();
      await descriptor.ReadInternalAsync(stream).ConfigureAwait(false);
      return descriptor;
    }

    private async Task ReadInternalAsync(Stream stream)
    {
      byte[] bytes = new byte[7];
      await stream.ReadAllAsync(bytes, 0, bytes.Length).ConfigureAwait(false);
      this.Width = (int) BitConverter.ToUInt16(bytes, 0);
      this.Height = (int) BitConverter.ToUInt16(bytes, 2);
      byte packedFields = bytes[4];
      this.HasGlobalColorTable = ((int) packedFields & 128) != 0;
      this.ColorResolution = (((int) packedFields & 112) >> 4) + 1;
      this.IsGlobalColorTableSorted = ((int) packedFields & 8) != 0;
      this.GlobalColorTableSize = 1 << ((int) packedFields & 7) + 1;
      this.BackgroundColorIndex = (int) bytes[5];
      this.PixelAspectRatio = bytes[6] == (byte) 0 ? 0.0 : (double) (15 + (int) bytes[6]) / 64.0;
    }

    int IGifRect.Left => 0;

    int IGifRect.Top => 0;
  }
}
