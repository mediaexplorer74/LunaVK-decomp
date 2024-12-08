// Decompiled with JetBrains decompiler
// Type: XamlAnimatedGif.Extensions.BitArrayExtensions
// Assembly: XamlAnimatedGif, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 02CD40CB-7FCE-4EFE-9D7D-B0095CDCA3E7
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\XamlAnimatedGif.dll

using System.Collections;

#nullable disable
namespace XamlAnimatedGif.Extensions
{
  internal static class BitArrayExtensions
  {
    public static short ToInt16(this BitArray bitArray)
    {
      short int16 = 0;
      for (int index = bitArray.Length - 1; index >= 0; --index)
        int16 = (short) (((int) int16 << 1) + (bitArray[index] ? 1 : 0));
      return int16;
    }
  }
}
