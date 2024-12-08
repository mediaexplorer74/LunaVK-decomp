
// Type: XamlAnimatedGif.Decoding.GifTrailer
// Assembly: XamlAnimatedGif, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 02CD40CB-7FCE-4EFE-9D7D-B0095CDCA3E7
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\XamlAnimatedGif.dll

using System.Threading.Tasks;

#nullable disable
namespace XamlAnimatedGif.Decoding
{
  internal class GifTrailer : GifBlock
  {
    internal const int TrailerByte = 59;

    private GifTrailer()
    {
    }

    internal override GifBlockKind Kind => GifBlockKind.Other;

    internal static Task<GifTrailer> ReadAsync() => Task.FromResult<GifTrailer>(new GifTrailer());
  }
}
