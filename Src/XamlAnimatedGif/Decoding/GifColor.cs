
// Type: XamlAnimatedGif.Decoding.GifColor
// Assembly: XamlAnimatedGif, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 02CD40CB-7FCE-4EFE-9D7D-B0095CDCA3E7
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\XamlAnimatedGif.dll

#nullable disable
namespace XamlAnimatedGif.Decoding
{
  internal struct GifColor
  {
    public byte R;
    public byte G;
    public byte B;

    internal GifColor(byte r, byte g, byte b)
    {
      this.R = r;
      this.G = g;
      this.B = b;
    }

    public override string ToString()
    {
      return string.Format("#{0:x2}{1:x2}{2:x2}", (object) this.R, (object) this.G, (object) this.B);
    }
  }
}
