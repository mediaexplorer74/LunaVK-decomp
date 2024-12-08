// Decompiled with JetBrains decompiler
// Type: XamlAnimatedGif.Decoding.GifImageDescriptor
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
  internal class GifImageDescriptor : IGifRect
  {
    public int Left { get; private set; }

    public int Top { get; private set; }

    public int Width { get; private set; }

    public int Height { get; private set; }

    public bool HasLocalColorTable { get; private set; }

    public bool Interlace { get; private set; }

    public bool IsLocalColorTableSorted { get; private set; }

    public int LocalColorTableSize { get; private set; }

    private GifImageDescriptor()
    {
    }

    internal static async Task<GifImageDescriptor> ReadAsync(Stream stream)
    {
      GifImageDescriptor descriptor = new GifImageDescriptor();
      await descriptor.ReadInternalAsync(stream).ConfigureAwait(false);
      return descriptor;
    }

    private async Task ReadInternalAsync(Stream stream)
    {
      byte[] bytes = new byte[9];
      await stream.ReadAllAsync(bytes, 0, bytes.Length).ConfigureAwait(false);
      this.Left = (int) BitConverter.ToUInt16(bytes, 0);
      this.Top = (int) BitConverter.ToUInt16(bytes, 2);
      this.Width = (int) BitConverter.ToUInt16(bytes, 4);
      this.Height = (int) BitConverter.ToUInt16(bytes, 6);
      byte packedFields = bytes[8];
      this.HasLocalColorTable = ((int) packedFields & 128) != 0;
      this.Interlace = ((int) packedFields & 64) != 0;
      this.IsLocalColorTableSorted = ((int) packedFields & 32) != 0;
      this.LocalColorTableSize = 1 << ((int) packedFields & 7) + 1;
    }
  }
}
