
// Type: XamlAnimatedGif.BrushAnimator
// Assembly: XamlAnimatedGif, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 02CD40CB-7FCE-4EFE-9D7D-B0095CDCA3E7
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\XamlAnimatedGif.dll

using System;
using System.IO;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using XamlAnimatedGif.Decoding;

#nullable disable
namespace XamlAnimatedGif
{
  public class BrushAnimator : Animator
  {
    private RepeatBehavior _repeatBehavior;

    private BrushAnimator(
      Stream sourceStream,
      Uri sourceUri,
      GifDataStream metadata,
      RepeatBehavior repeatBehavior)
      : base(sourceStream, sourceUri, metadata, repeatBehavior)
    {
      ImageBrush imageBrush = new ImageBrush();
      imageBrush.ImageSource = (ImageSource) this.Bitmap;
      this.Brush = imageBrush;
      this.RepeatBehavior = this._repeatBehavior;
    }

    protected override RepeatBehavior GetSpecifiedRepeatBehavior() => this.RepeatBehavior;

    protected override object ErrorSource => (object) this.Brush;

    public ImageBrush Brush { get; private set; }

    public RepeatBehavior RepeatBehavior
    {
      get => this._repeatBehavior;
      set
      {
        this._repeatBehavior = value;
        this.OnRepeatBehaviorChanged();
      }
    }

    public static Task<BrushAnimator> CreateAsync(
      Uri sourceUri,
      RepeatBehavior repeatBehavior,
      IProgress<int> progress = null)
    {
      return Animator.CreateAsyncCore<BrushAnimator>(sourceUri, progress, (Func<Stream, GifDataStream, BrushAnimator>) ((stream, metadata) => new BrushAnimator(stream, sourceUri, metadata, repeatBehavior)));
    }

    public static Task<BrushAnimator> CreateAsync(
      Stream sourceStream,
      RepeatBehavior repeatBehavior)
    {
      return Animator.CreateAsyncCore<BrushAnimator>(sourceStream, (Func<GifDataStream, BrushAnimator>) (metadata => new BrushAnimator(sourceStream, (Uri) null, metadata, repeatBehavior)));
    }
  }
}
