// Decompiled with JetBrains decompiler
// Type: XamlAnimatedGif.Animator
// Assembly: XamlAnimatedGif, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 02CD40CB-7FCE-4EFE-9D7D-B0095CDCA3E7
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\XamlAnimatedGif.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using XamlAnimatedGif.Decoding;
using XamlAnimatedGif.Decompression;
using XamlAnimatedGif.Extensions;

#nullable disable
namespace XamlAnimatedGif
{
  [Bindable]
  public abstract class Animator : DependencyObject, IDisposable
  {
    private readonly Stream _sourceStream;
    private readonly Uri _sourceUri;
    private readonly bool _isSourceStreamOwner;
    private readonly GifDataStream _metadata;
    private readonly Dictionary<int, Animator.GifPalette> _palettes;
    private readonly WriteableBitmap _bitmap;
    private readonly int _stride;
    private readonly byte[] _previousBackBuffer;
    private readonly byte[] _indexStreamBuffer;
    private readonly TimingManager _timingManager;
    private bool _isStarted;
    private CancellationTokenSource _cancellationTokenSource;
    private int _frameIndex;
    private int _previousFrameIndex;
    private GifFrame _previousFrame;
    private volatile bool _disposing;
    private bool _disposed;

    internal Animator(
      Stream sourceStream,
      Uri sourceUri,
      GifDataStream metadata,
      RepeatBehavior repeatBehavior)
    {
      this._sourceStream = sourceStream;
      this._sourceUri = sourceUri;
      this._isSourceStreamOwner = sourceUri != (Uri) null;
      this._metadata = metadata;
      this._palettes = Animator.CreatePalettes(metadata);
      this._bitmap = Animator.CreateBitmap(metadata);
      GifLogicalScreenDescriptor screenDescriptor = metadata.Header.LogicalScreenDescriptor;
      this._stride = 4 * ((screenDescriptor.Width * 32 + 31) / 32);
      this._previousBackBuffer = new byte[screenDescriptor.Height * this._stride];
      this._indexStreamBuffer = Animator.CreateIndexStreamBuffer(metadata, this._sourceStream);
      this._timingManager = this.CreateTimingManager(metadata, repeatBehavior);
    }

    internal static async Task<TAnimator> CreateAsyncCore<TAnimator>(
      Uri sourceUri,
      IProgress<int> progress,
      Func<Stream, GifDataStream, TAnimator> create)
      where TAnimator : Animator
    {
      UriLoader loader = new UriLoader();
      Stream stream = await loader.GetStreamFromUriAsync(sourceUri, progress);
      TAnimator asyncCore;
      try
      {
        asyncCore = await Animator.CreateAsyncCore<TAnimator>(stream, (Func<GifDataStream, TAnimator>) (metadata => create(stream, metadata)));
      }
      catch
      {
        stream.Dispose();
        throw;
      }
      return asyncCore;
    }

    internal static async Task<TAnimator> CreateAsyncCore<TAnimator>(
      Stream sourceStream,
      Func<GifDataStream, TAnimator> create)
      where TAnimator : Animator
    {
      if (!sourceStream.CanSeek)
        throw new ArgumentException("The stream is not seekable");
      sourceStream.Seek(0L, SeekOrigin.Begin);
      GifDataStream metadata = await GifDataStream.ReadAsync(sourceStream);
      return create(metadata);
    }

    public int FrameCount => this._metadata.Frames.Count;

    public async void Play()
    {
      try
      {
        if (this._timingManager.IsComplete)
        {
          this._timingManager.Reset();
          this._isStarted = false;
        }
        if (!this._isStarted)
        {
          this._cancellationTokenSource.Dispose();
          this._cancellationTokenSource = new CancellationTokenSource();
          this._isStarted = true;
          if (this._timingManager.IsPaused)
            this._timingManager.Resume();
          await this.RunAsync(this._cancellationTokenSource.Token);
        }
        else
        {
          if (!this._timingManager.IsPaused)
            return;
          this._timingManager.Resume();
        }
      }
      catch (OperationCanceledException ex)
      {
      }
      catch (Exception ex)
      {
        if (this._disposing)
          return;
        this.OnError(ex, AnimationErrorKind.Rendering);
      }
    }

    private async Task RunAsync(CancellationToken cancellationToken)
    {
      while (true)
      {
        cancellationToken.ThrowIfCancellationRequested();
        Task<bool> timing = this._timingManager.NextAsync(cancellationToken);
        Task rendering = this.RenderFrameAsync(this.CurrentFrameIndex, cancellationToken);
        await Task.WhenAll((Task) timing, rendering);
        if (timing.Result)
          this.CurrentFrameIndex = (this.CurrentFrameIndex + 1) % this.FrameCount;
        else
          break;
      }
    }

    public void Pause() => this._timingManager.Pause();

    public bool IsPaused => this._timingManager.IsPaused;

    public bool IsComplete => this._isStarted && this._timingManager.IsComplete;

    public event EventHandler CurrentFrameChanged;

    protected virtual void OnCurrentFrameChanged()
    {
      this.CurrentFrameChanged((object) this, EventArgs.Empty);
    }

    public event EventHandler AnimationCompleted;

    protected virtual void OnAnimationCompleted()
    {
      this.AnimationCompleted((object) this, EventArgs.Empty);
    }

    public event EventHandler<AnimationErrorEventArgs> Error;

    protected virtual void OnError(Exception ex, AnimationErrorKind kind)
    {
      this.Error((object) this, new AnimationErrorEventArgs(this.ErrorSource, ex, kind));
    }

    public int CurrentFrameIndex
    {
      get => this._frameIndex;
      internal set
      {
        this._frameIndex = value;
        this.OnCurrentFrameChanged();
      }
    }

    private TimingManager CreateTimingManager(GifDataStream metadata, RepeatBehavior repeatBehavior)
    {
      TimingManager timingManager = new TimingManager(this.GetActualRepeatBehavior(metadata, repeatBehavior));
      foreach (GifFrame frame in (IEnumerable<GifFrame>) metadata.Frames)
        timingManager.Add(Animator.GetFrameDelay(frame));
      timingManager.Completed += new EventHandler(this.TimingManagerCompleted);
      return timingManager;
    }

    private RepeatBehavior GetActualRepeatBehavior(
      GifDataStream metadata,
      RepeatBehavior repeatBehavior)
    {
      return !(repeatBehavior == new RepeatBehavior()) ? repeatBehavior : Animator.GetRepeatBehaviorFromGif(metadata);
    }

    protected abstract RepeatBehavior GetSpecifiedRepeatBehavior();

    private void TimingManagerCompleted(object sender, EventArgs e) => this.OnAnimationCompleted();

    private static WriteableBitmap CreateBitmap(GifDataStream metadata)
    {
      GifLogicalScreenDescriptor screenDescriptor = metadata.Header.LogicalScreenDescriptor;
      return new WriteableBitmap(screenDescriptor.Width, screenDescriptor.Height);
    }

    private static Dictionary<int, Animator.GifPalette> CreatePalettes(GifDataStream metadata)
    {
      Dictionary<int, Animator.GifPalette> palettes = new Dictionary<int, Animator.GifPalette>();
      Color[] colorArray = (Color[]) null;
      if (metadata.Header.LogicalScreenDescriptor.HasGlobalColorTable)
        colorArray = ((IEnumerable<GifColor>) metadata.GlobalColorTable).Select<GifColor, Color>((Func<GifColor, Color>) (gc => Color.FromArgb(byte.MaxValue, gc.R, gc.G, gc.B))).ToArray<Color>();
      for (int index = 0; index < metadata.Frames.Count; ++index)
      {
        GifFrame frame = metadata.Frames[index];
        Color[] colors = colorArray;
        if (frame.Descriptor.HasLocalColorTable)
          colors = ((IEnumerable<GifColor>) frame.LocalColorTable).Select<GifColor, Color>((Func<GifColor, Color>) (gc => Color.FromArgb(byte.MaxValue, gc.R, gc.G, gc.B))).ToArray<Color>();
        int? transparencyIndex = new int?();
        GifGraphicControlExtension graphicControl = frame.GraphicControl;
        if (graphicControl != null && graphicControl.HasTransparency)
          transparencyIndex = new int?(graphicControl.TransparencyIndex);
        palettes[index] = new Animator.GifPalette(transparencyIndex, colors);
      }
      return palettes;
    }

    private static byte[] CreateIndexStreamBuffer(GifDataStream metadata, Stream stream)
    {
      long val2 = stream.Length - metadata.Frames.Last<GifFrame>().ImageData.CompressedDataStartOffset;
      long num = val2;
      if (metadata.Frames.Count > 1)
        num = Math.Max(metadata.Frames.Zip<GifFrame, GifFrame, long>(metadata.Frames.Skip<GifFrame>(1), (Func<GifFrame, GifFrame, long>) ((f1, f2) => f2.ImageData.CompressedDataStartOffset - f1.ImageData.CompressedDataStartOffset)).Max(), val2);
      return new byte[num + 4L];
    }

    private async Task RenderFrameAsync(int frameIndex, CancellationToken cancellationToken)
    {
      if (frameIndex < 0)
        return;
      GifFrame frame = this._metadata.Frames[frameIndex];
      GifImageDescriptor desc = frame.Descriptor;
      Int32Rect rect = this.GetFixedUpFrameRect(desc);
      using (Stream indexStream = await this.GetIndexStreamAsync(frame, cancellationToken))
      {
        using (this._bitmap.LockInScope())
        {
          if (frameIndex < this._previousFrameIndex)
            this.ClearArea((IGifRect) this._metadata.Header.LogicalScreenDescriptor);
          else
            this.DisposePreviousFrame(frame);
          int length = 4 * rect.Width;
          byte[] buffer = new byte[desc.Width];
          byte[] numArray = new byte[length];
          Animator.GifPalette palette = this._palettes[frameIndex];
          int num1 = palette.TransparencyIndex ?? -1;
          foreach (int num2 in desc.Interlace ? Animator.InterlacedRows(rect.Height) : Animator.NormalRows(rect.Height))
          {
            indexStream.ReadAll(buffer, 0, desc.Width);
            int offset = (desc.Top + num2) * this._stride + desc.Left * 4;
            if (num1 >= 0)
              Animator.CopyFromBitmap(numArray, this._bitmap, offset, length);
            for (int index = 0; index < rect.Width; ++index)
            {
              byte i = buffer[index];
              int startIndex = 4 * index;
              if ((int) i != num1)
                Animator.WriteColor(numArray, palette[(int) i], startIndex);
            }
            Animator.CopyToBitmap(numArray, this._bitmap, offset, length);
          }
        }
        this._bitmap.Invalidate();
        this._previousFrame = frame;
        this._previousFrameIndex = frameIndex;
      }
    }

    private static IEnumerable<int> NormalRows(int height) => Enumerable.Range(0, height);

    private static IEnumerable<int> InterlacedRows(int height)
    {
      \u003C\u003Ef__AnonymousType0<int, int>[] passes = new \u003C\u003Ef__AnonymousType0<int, int>[4]
      {
        new{ Start = 0, Step = 8 },
        new{ Start = 4, Step = 8 },
        new{ Start = 2, Step = 4 },
        new{ Start = 1, Step = 2 }
      };
      foreach (var data in passes)
      {
        for (int y = data.Start; y < height; y += data.Step)
          yield return y;
      }
    }

    private static void CopyToBitmap(
      byte[] buffer,
      WriteableBitmap bitmap,
      int offset,
      int length)
    {
      buffer.CopyTo(0, bitmap.PixelBuffer, (uint) offset, length);
    }

    private static void CopyFromBitmap(
      byte[] buffer,
      WriteableBitmap bitmap,
      int offset,
      int length)
    {
      bitmap.PixelBuffer.CopyTo((uint) offset, buffer, 0, length);
    }

    private static void WriteColor(byte[] lineBuffer, Color color, int startIndex)
    {
      lineBuffer[startIndex] = color.B;
      lineBuffer[startIndex + 1] = color.G;
      lineBuffer[startIndex + 2] = color.R;
      lineBuffer[startIndex + 3] = color.A;
    }

    private void DisposePreviousFrame(GifFrame currentFrame)
    {
      GifGraphicControlExtension graphicControl1 = this._previousFrame.GraphicControl;
      if (graphicControl1 != null)
      {
        switch (graphicControl1.DisposalMethod)
        {
          case GifFrameDisposalMethod.RestoreBackground:
            this.ClearArea(this.GetFixedUpFrameRect(this._previousFrame.Descriptor));
            break;
          case GifFrameDisposalMethod.RestorePrevious:
            Animator.CopyToBitmap(this._previousBackBuffer, this._bitmap, 0, this._previousBackBuffer.Length);
            break;
        }
      }
      GifGraphicControlExtension graphicControl2 = currentFrame.GraphicControl;
      if (graphicControl2 == null || graphicControl2.DisposalMethod != GifFrameDisposalMethod.RestorePrevious)
        return;
      Animator.CopyFromBitmap(this._previousBackBuffer, this._bitmap, 0, this._previousBackBuffer.Length);
    }

    private void ClearArea(IGifRect rect)
    {
      this.ClearArea(new Int32Rect(rect.Left, rect.Top, rect.Width, rect.Height));
    }

    private void ClearArea(Int32Rect rect)
    {
      int length = 4 * rect.Width;
      byte[] buffer = new byte[length];
      for (int index = 0; index < rect.Height; ++index)
      {
        int offset = (rect.Y + index) * this._stride + 4 * rect.X;
        Animator.CopyToBitmap(buffer, this._bitmap, offset, length);
      }
    }

    private async Task<Stream> GetIndexStreamAsync(
      GifFrame frame,
      CancellationToken cancellationToken)
    {
      GifImageData data = frame.ImageData;
      cancellationToken.ThrowIfCancellationRequested();
      this._sourceStream.Seek(data.CompressedDataStartOffset, SeekOrigin.Begin);
      using (MemoryStream ms = new MemoryStream(this._indexStreamBuffer))
        await GifHelpers.CopyDataBlocksToStreamAsync(this._sourceStream, (Stream) ms, cancellationToken).ConfigureAwait(false);
      LzwDecompressStream lzwStream = new LzwDecompressStream(this._indexStreamBuffer, (int) data.LzwMinimumCodeSize);
      return (Stream) lzwStream;
    }

    internal BitmapSource Bitmap => (BitmapSource) this._bitmap;

    private static TimeSpan GetFrameDelay(GifFrame frame)
    {
      GifGraphicControlExtension graphicControl = frame.GraphicControl;
      return graphicControl != null && graphicControl.Delay != 0 ? TimeSpan.FromMilliseconds((double) graphicControl.Delay) : TimeSpan.FromMilliseconds(100.0);
    }

    private static RepeatBehavior GetRepeatBehaviorFromGif(GifDataStream metadata)
    {
      return metadata.RepeatCount == (ushort) 0 ? RepeatBehavior.Forever : new RepeatBehavior((double) metadata.RepeatCount);
    }

    private Int32Rect GetFixedUpFrameRect(GifImageDescriptor desc)
    {
      int width = Math.Min(desc.Width, ((BitmapSource) this._bitmap).PixelWidth - desc.Left);
      int height = Math.Min(desc.Height, ((BitmapSource) this._bitmap).PixelHeight - desc.Top);
      return new Int32Rect(desc.Left, desc.Top, width, height);
    }

    ~Animator()
    {
      try
      {
        this.Dispose(false);
      }
      finally
      {
        // ISSUE: explicit finalizer call
        // ISSUE: explicit non-virtual call
        __nonvirtual (((object) this).Finalize());
      }
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (this._disposed)
        return;
      this._disposing = true;
      if (this._timingManager != null)
        this._timingManager.Completed -= new EventHandler(this.TimingManagerCompleted);
      this._cancellationTokenSource.Cancel();
      if (this._isSourceStreamOwner)
      {
        try
        {
          this._sourceStream.Dispose();
        }
        catch
        {
        }
      }
      this._disposed = true;
    }

    public virtual string ToString()
    {
      return "GIF: " + (!(this._sourceUri != (Uri) null) ? this._sourceStream.ToString() : this._sourceUri.ToString());
    }

    internal async Task ShowFirstFrameAsync()
    {
      try
      {
        await this.RenderFrameAsync(0, CancellationToken.None);
        this.CurrentFrameIndex = 0;
        this._timingManager.Pause();
      }
      catch (Exception ex)
      {
        this.OnError(ex, AnimationErrorKind.Rendering);
      }
    }

    public async void Rewind()
    {
      this.CurrentFrameIndex = 0;
      bool isStopped = this._timingManager.IsPaused || this._timingManager.IsComplete;
      this._timingManager.Reset();
      if (!isStopped)
        return;
      this._timingManager.Pause();
      this._isStarted = false;
      try
      {
        await this.RenderFrameAsync(0, CancellationToken.None);
      }
      catch (Exception ex)
      {
        this.OnError(ex, AnimationErrorKind.Rendering);
      }
    }

    protected abstract object ErrorSource { get; }

    internal void OnRepeatBehaviorChanged()
    {
      if (this._timingManager == null)
        return;
      RepeatBehavior actualRepeatBehavior = this.GetActualRepeatBehavior(this._metadata, this.GetSpecifiedRepeatBehavior());
      if (this._timingManager.RepeatBehavior == actualRepeatBehavior)
        return;
      this._timingManager.RepeatBehavior = actualRepeatBehavior;
      this.Rewind();
    }

    private class GifPalette
    {
      private readonly Color[] _colors;

      public GifPalette(int? transparencyIndex, Color[] colors)
      {
        this.TransparencyIndex = transparencyIndex;
        this._colors = colors;
      }

      public int? TransparencyIndex { get; private set; }

      public Color this[int i] => this._colors[i];
    }
  }
}
