// Decompiled with JetBrains decompiler
// Type: XamlAnimatedGif.Decompression.LzwDecompressStream
// Assembly: XamlAnimatedGif, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 02CD40CB-7FCE-4EFE-9D7D-B0095CDCA3E7
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\XamlAnimatedGif.dll

using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

#nullable disable
namespace XamlAnimatedGif.Decompression
{
  internal class LzwDecompressStream : Stream
  {
    private const int MaxCodeLength = 12;
    private readonly BitReader _reader;
    private readonly LzwDecompressStream.CodeTable _codeTable;
    private int _prevCode;
    private byte[] _remainingBytes;
    private bool _endOfStream;

    public LzwDecompressStream(byte[] compressedBuffer, int minimumCodeLength)
    {
      this._reader = new BitReader(compressedBuffer);
      this._codeTable = new LzwDecompressStream.CodeTable(minimumCodeLength);
    }

    public override void Flush()
    {
    }

    public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();

    public override void SetLength(long value) => throw new NotSupportedException();

    public override int Read(byte[] buffer, int offset, int count)
    {
      if (this._endOfStream)
        return 0;
      int read = 0;
      this.FlushRemainingBytes(buffer, offset, count, ref read);
      while (read < count)
      {
        if (!this.ProcessCode(this._reader.ReadBits(this._codeTable.CodeLength), buffer, offset, count, ref read))
        {
          this._endOfStream = true;
          break;
        }
      }
      return read;
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
      throw new NotSupportedException();
    }

    public override bool CanRead => true;

    public override bool CanSeek => false;

    public override bool CanWrite => true;

    public override long Length => throw new NotSupportedException();

    public override long Position
    {
      get => throw new NotSupportedException();
      set => throw new NotSupportedException();
    }

    private void InitCodeTable()
    {
      this._codeTable.Reset();
      this._prevCode = -1;
    }

    private static byte[] CopySequenceToBuffer(
      byte[] sequence,
      byte[] buffer,
      int offset,
      int count,
      ref int read)
    {
      int num = Math.Min(sequence.Length, count - read);
      Buffer.BlockCopy((Array) sequence, 0, (Array) buffer, offset + read, num);
      read += num;
      byte[] dst = (byte[]) null;
      if (num < sequence.Length)
      {
        int count1 = sequence.Length - num;
        dst = new byte[count1];
        Buffer.BlockCopy((Array) sequence, num, (Array) dst, 0, count1);
      }
      return dst;
    }

    private void FlushRemainingBytes(byte[] buffer, int offset, int count, ref int read)
    {
      if (this._remainingBytes == null)
        return;
      this._remainingBytes = LzwDecompressStream.CopySequenceToBuffer(this._remainingBytes, buffer, offset, count, ref read);
    }

    [Conditional("DISABLED")]
    private static void ValidateReadArgs(byte[] buffer, int offset, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      if (offset < 0)
        throw new ArgumentOutOfRangeException(nameof (offset), "Offset can't be negative");
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), "Count can't be negative");
      if (offset + count > buffer.Length)
        throw new ArgumentException("Buffer is to small to receive the requested data");
    }

    private bool ProcessCode(int code, byte[] buffer, int offset, int count, ref int read)
    {
      if (code < this._codeTable.Count)
      {
        LzwDecompressStream.Sequence sequence = this._codeTable[code];
        if (sequence.IsStopCode)
          return false;
        if (sequence.IsClearCode)
        {
          this.InitCodeTable();
          return true;
        }
        this._remainingBytes = LzwDecompressStream.CopySequenceToBuffer(sequence.Bytes, buffer, offset, count, ref read);
        if (this._prevCode >= 0)
          this._codeTable.Add(this._codeTable[this._prevCode].Append(sequence.Bytes[0]));
      }
      else
      {
        LzwDecompressStream.Sequence sequence1 = this._codeTable[this._prevCode];
        LzwDecompressStream.Sequence sequence2 = sequence1.Append(sequence1.Bytes[0]);
        this._codeTable.Add(sequence2);
        this._remainingBytes = LzwDecompressStream.CopySequenceToBuffer(sequence2.Bytes, buffer, offset, count, ref read);
      }
      this._prevCode = code;
      return true;
    }

    private struct Sequence
    {
      public static LzwDecompressStream.Sequence ClearCode = new LzwDecompressStream.Sequence(true, false);
      public static LzwDecompressStream.Sequence StopCode = new LzwDecompressStream.Sequence(false, true);

      public Sequence(byte[] bytes)
        : this()
      {
        this.Bytes = bytes;
      }

      private Sequence(bool isClearCode, bool isStopCode)
        : this()
      {
        this.IsClearCode = isClearCode;
        this.IsStopCode = isStopCode;
      }

      public byte[] Bytes { get; private set; }

      public bool IsClearCode { get; private set; }

      public bool IsStopCode { get; private set; }

      public LzwDecompressStream.Sequence Append(byte b)
      {
        byte[] bytes = new byte[this.Bytes.Length + 1];
        this.Bytes.CopyTo((Array) bytes, 0);
        bytes[this.Bytes.Length] = b;
        return new LzwDecompressStream.Sequence(bytes);
      }
    }

    private class CodeTable
    {
      private readonly int _minimumCodeLength;
      private readonly LzwDecompressStream.Sequence[] _table;
      private int _count;
      private int _codeLength;

      public CodeTable(int minimumCodeLength)
      {
        this._minimumCodeLength = minimumCodeLength;
        this._codeLength = this._minimumCodeLength + 1;
        int num = 1 << minimumCodeLength;
        this._table = new LzwDecompressStream.Sequence[4096];
        for (int index = 0; index < num; ++index)
          this._table[this._count++] = new LzwDecompressStream.Sequence(new byte[1]
          {
            (byte) index
          });
        this.Add(LzwDecompressStream.Sequence.ClearCode);
        this.Add(LzwDecompressStream.Sequence.StopCode);
      }

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public void Reset()
      {
        this._count = (1 << this._minimumCodeLength) + 2;
        this._codeLength = this._minimumCodeLength + 1;
      }

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public void Add(LzwDecompressStream.Sequence sequence)
      {
        if (this._count >= this._table.Length)
          return;
        this._table[this._count++] = sequence;
        if ((this._count & this._count - 1) != 0 || this._codeLength >= 12)
          return;
        ++this._codeLength;
      }

      public LzwDecompressStream.Sequence this[int index]
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._table[index];
      }

      public int Count
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._count;
      }

      public int CodeLength
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._codeLength;
      }
    }
  }
}
