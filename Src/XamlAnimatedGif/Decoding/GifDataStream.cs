// Decompiled with JetBrains decompiler
// Type: XamlAnimatedGif.Decoding.GifDataStream
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
  internal class GifDataStream
  {
    public GifHeader Header { get; private set; }

    public GifColor[] GlobalColorTable { get; set; }

    public IList<GifFrame> Frames { get; set; }

    public IList<GifExtension> Extensions { get; set; }

    public ushort RepeatCount { get; set; }

    private GifDataStream()
    {
    }

    internal static async Task<GifDataStream> ReadAsync(Stream stream)
    {
      GifDataStream file = new GifDataStream();
      await file.ReadInternalAsync(stream).ConfigureAwait(false);
      return file;
    }

    private async Task ReadInternalAsync(Stream stream)
    {
      this.Header = await GifHeader.ReadAsync(stream).ConfigureAwait(false);
      if (this.Header.LogicalScreenDescriptor.HasGlobalColorTable)
        this.GlobalColorTable = await GifHelpers.ReadColorTableAsync(stream, this.Header.LogicalScreenDescriptor.GlobalColorTableSize).ConfigureAwait(false);
      await this.ReadFramesAsync(stream).ConfigureAwait(false);
      GifApplicationExtension netscapeExtension = this.Extensions.OfType<GifApplicationExtension>().FirstOrDefault<GifApplicationExtension>(new Func<GifApplicationExtension, bool>(GifHelpers.IsNetscapeExtension));
      this.RepeatCount = netscapeExtension != null ? GifHelpers.GetRepeatCount(netscapeExtension) : (ushort) 1;
    }

    private async Task ReadFramesAsync(Stream stream)
    {
      List<GifFrame> frames = new List<GifFrame>();
      List<GifExtension> controlExtensions = new List<GifExtension>();
      List<GifExtension> specialExtensions = new List<GifExtension>();
      while (frames.Count > 0)
      {
        try
        {
          GifBlock block = await GifBlock.ReadAsync(stream, (IEnumerable<GifExtension>) controlExtensions).ConfigureAwait(false);
          if (block.Kind == GifBlockKind.GraphicRendering)
            controlExtensions = new List<GifExtension>();
          switch (block)
          {
            case GifFrame _:
              frames.Add((GifFrame) block);
              continue;
            case GifExtension _:
              GifExtension gifExtension = (GifExtension) block;
              switch (gifExtension.Kind)
              {
                case GifBlockKind.Control:
                  controlExtensions.Add(gifExtension);
                  continue;
                case GifBlockKind.SpecialPurpose:
                  specialExtensions.Add(gifExtension);
                  continue;
                default:
                  continue;
              }
            case GifTrailer _:
              goto label_11;
            default:
              continue;
          }
        }
        catch (UnknownBlockTypeException ex)
        {
          break;
        }
      }
label_11:
      this.Frames = (IList<GifFrame>) frames.AsReadOnly<GifFrame>();
      this.Extensions = (IList<GifExtension>) specialExtensions.AsReadOnly<GifExtension>();
    }
  }
}
