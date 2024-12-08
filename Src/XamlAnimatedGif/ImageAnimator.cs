
// Type: XamlAnimatedGif.ImageAnimator
// Assembly: XamlAnimatedGif, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 02CD40CB-7FCE-4EFE-9D7D-B0095CDCA3E7
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\XamlAnimatedGif.dll

using System;
using System.IO;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using XamlAnimatedGif.Decoding;

#nullable disable
namespace XamlAnimatedGif
{
  internal class ImageAnimator : Animator
  {
    private readonly Image _image;

    public ImageAnimator(
      Stream sourceStream,
      Uri sourceUri,
      GifDataStream metadata,
      RepeatBehavior repeatBehavior,
      Image image)
      : base(sourceStream, sourceUri, metadata, repeatBehavior)
    {
      this._image = image;
      this.OnRepeatBehaviorChanged();
    }

    protected override RepeatBehavior GetSpecifiedRepeatBehavior()
    {
      return AnimationBehavior.GetRepeatBehavior((DependencyObject) this._image);
    }

    protected override object ErrorSource => (object) this._image;

    public static Task<ImageAnimator> CreateAsync(
      Uri sourceUri,
      RepeatBehavior repeatBehavior,
      IProgress<int> progress,
      Image image)
    {
      return Animator.CreateAsyncCore<ImageAnimator>(sourceUri, progress, (Func<Stream, GifDataStream, ImageAnimator>) ((stream, metadata) => new ImageAnimator(stream, sourceUri, metadata, repeatBehavior, image)));
    }

    public static Task<ImageAnimator> CreateAsync(
      Stream sourceStream,
      RepeatBehavior repeatBehavior,
      Image image)
    {
      return Animator.CreateAsyncCore<ImageAnimator>(sourceStream, (Func<GifDataStream, ImageAnimator>) (metadata => new ImageAnimator(sourceStream, (Uri) null, metadata, repeatBehavior, image)));
    }
  }
}
