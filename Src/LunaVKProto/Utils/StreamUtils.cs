// Decompiled with JetBrains decompiler
// Type: App1uwp.Utils.StreamUtils
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using System;
using System.IO;

#nullable disable
namespace App1uwp.Utils
{
  public static class StreamUtils
  {
    public static void CopyStream(
      Stream input,
      Stream output,
      Action<double> progressCallback = null,
      long inputLength = 0)
    {
      if (inputLength == 0L)
      {
        try
        {
          inputLength = input.Length;
        }
        catch (Exception ex)
        {
        }
      }
      byte[] buffer = new byte[32768];
      int num = 0;
      int count;
      while ((count = input.Read(buffer, 0, buffer.Length)) > 0)
      {
        if (!output.CanWrite)
          throw new Exception("failed to copy stream");
        output.Write(buffer, 0, count);
        num += count;
        if (progressCallback != null && inputLength > 0L)
          progressCallback((double) num * 100.0 / (double) inputLength);
      }
    }

    private static int Read(this byte[] input, byte[] output, int offset, int output_size)
    {
      int index1 = 0;
      for (int index2 = offset; index2 < input.Length && index1 < output_size; ++index2)
      {
        output[index1] = input[index2];
        ++index1;
      }
      return index1;
    }

    public static void CopyStream(byte[] input, Stream output, Action<double> progressCallback = null)
    {
      int length = input.Length;
      byte[] numArray = new byte[32768];
      int num = 0;
      int offset = 0;
      int count;
      while ((count = input.Read(numArray, offset, numArray.Length)) > 0)
      {
        if (!output.CanWrite)
          throw new Exception("failed to copy stream");
        output.Write(numArray, 0, count);
        num += count;
        offset += count;
        if (progressCallback != null && length > 0)
          progressCallback((double) num * 100.0 / (double) length);
      }
    }

    public static MemoryStream ReadFully(Stream input)
    {
      byte[] buffer = new byte[16384];
      MemoryStream memoryStream = new MemoryStream();
      int count;
      while ((count = input.Read(buffer, 0, buffer.Length)) > 0)
        memoryStream.Write(buffer, 0, count);
      memoryStream.Position = 0L;
      return memoryStream;
    }
  }
}
