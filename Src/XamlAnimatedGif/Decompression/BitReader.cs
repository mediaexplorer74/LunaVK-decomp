
// Type: XamlAnimatedGif.Decompression.BitReader
// Assembly: XamlAnimatedGif, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 02CD40CB-7FCE-4EFE-9D7D-B0095CDCA3E7
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\XamlAnimatedGif.dll

#nullable disable
namespace XamlAnimatedGif.Decompression
{
  internal class BitReader
  {
    private readonly byte[] _buffer;
    private int _bytePosition = -1;
    private int _bitPosition;
    private int _currentValue = -1;

    public BitReader(byte[] buffer) => this._buffer = buffer;

    public int ReadBits(int bitCount)
    {
      if (this._bytePosition == -1)
      {
        this._bytePosition = 0;
        this._bitPosition = 0;
        this._currentValue = this.ReadInt32();
      }
      else if (bitCount > 32 - this._bitPosition)
      {
        this._bytePosition += this._bitPosition >> 3;
        this._bitPosition &= 7;
        this._currentValue = this.ReadInt32() >> this._bitPosition;
      }
      int num = this._currentValue & (1 << bitCount) - 1;
      this._currentValue >>= bitCount;
      this._bitPosition += bitCount;
      return num;
    }

    private int ReadInt32()
    {
      int num = 0;
      for (int index = 0; index < 4 && this._bytePosition + index < this._buffer.Length; ++index)
        num |= (int) this._buffer[this._bytePosition + index] << (index << 3);
      return num;
    }
  }
}
