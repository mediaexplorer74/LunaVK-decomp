// Decompiled with JetBrains decompiler
// Type: App1uwp.Utils.IBufferExtensions
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using System;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage.Streams;

#nullable disable
namespace App1uwp.Utils
{
  public static class IBufferExtensions
  {
    public static IBufferExtensions.PixelBufferInfo GetPixels(this IBuffer pixelBuffer)
    {
      return new IBufferExtensions.PixelBufferInfo(pixelBuffer);
    }

    public class PixelBufferInfo
    {
      private readonly Stream _pixelStream;
      public byte[] Bytes;

      public int this[int i]
      {
        get
        {
          return ColorExtensions.IntColorFromBytes(this.Bytes[i * 4 + 3], this.Bytes[i * 4 + 2], this.Bytes[i * 4 + 1], this.Bytes[i * 4]);
        }
        set
        {
          this.Bytes[i * 4 + 3] = (byte) (value >> 24 & (int) byte.MaxValue);
          this.Bytes[i * 4 + 2] = (byte) (value >> 16 & (int) byte.MaxValue);
          this.Bytes[i * 4 + 1] = (byte) (value >> 8 & (int) byte.MaxValue);
          this.Bytes[i * 4] = (byte) (value & (int) byte.MaxValue);
          this._pixelStream.Seek((long) (i * 4), SeekOrigin.Begin);
          this._pixelStream.Write(this.Bytes, i * 4, 4);
        }
      }

      public byte MaxDiff(int i, int color)
      {
        return Math.Max(Math.Max(Math.Max((byte) Math.Abs((int) this.Bytes[i * 4 + 3] - (color >> 24 & (int) byte.MaxValue)), (byte) Math.Abs((int) this.Bytes[i * 4 + 2] - (color >> 16 & (int) byte.MaxValue))), (byte) Math.Abs((int) this.Bytes[i * 4 + 1] - (color >> 8 & (int) byte.MaxValue))), (byte) Math.Abs((int) this.Bytes[i * 4] - (color & (int) byte.MaxValue)));
      }

      public PixelBufferInfo(IBuffer pixelBuffer)
      {
        this._pixelStream = pixelBuffer.AsStream();
        this.Bytes = new byte[this._pixelStream.Length];
        this._pixelStream.Seek(0L, SeekOrigin.Begin);
        this._pixelStream.Read(this.Bytes, 0, this.Bytes.Length);
      }

      public void UpdateFromBytes()
      {
        this._pixelStream.Seek(0L, SeekOrigin.Begin);
        this._pixelStream.Write(this.Bytes, 0, this.Bytes.Length);
      }
    }
  }
}
