
// Type: XamlAnimatedGif.AnimationBehavior
// Assembly: XamlAnimatedGif, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 02CD40CB-7FCE-4EFE-9D7D-B0095CDCA3E7
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\XamlAnimatedGif.dll

using System;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using XamlAnimatedGif.Decoding;
using XamlAnimatedGif.Extensions;

#nullable disable
namespace XamlAnimatedGif
{
  public static class AnimationBehavior
  {
    public static readonly DependencyProperty SourceUriProperty = DependencyProperty.RegisterAttached("SourceUri", typeof (Uri), typeof (AnimationBehavior), new PropertyMetadata((object) null, new PropertyChangedCallback(AnimationBehavior.SourceChanged)));
    public static readonly DependencyProperty SourceStreamProperty = DependencyProperty.RegisterAttached("SourceStream", typeof (Stream), typeof (AnimationBehavior), new PropertyMetadata((object) null, new PropertyChangedCallback(AnimationBehavior.SourceChanged)));
    public static readonly DependencyProperty RepeatBehaviorProperty = DependencyProperty.RegisterAttached("RepeatBehavior", typeof (RepeatBehavior), typeof (AnimationBehavior), new PropertyMetadata((object) new RepeatBehavior(), new PropertyChangedCallback(AnimationBehavior.RepeatBehaviorChanged)));
    public static readonly DependencyProperty AutoStartProperty = DependencyProperty.RegisterAttached("AutoStart", typeof (bool), typeof (AnimationBehavior), new PropertyMetadata((object) true));
    public static readonly DependencyProperty AnimateInDesignModeProperty = DependencyProperty.RegisterAttached("AnimateInDesignMode", typeof (bool), typeof (AnimationBehavior), new PropertyMetadata((object) false, new PropertyChangedCallback(AnimationBehavior.AnimateInDesignModeChanged)));
    public static readonly DependencyProperty AnimatorProperty = DependencyProperty.RegisterAttached("Animator", typeof (Animator), typeof (AnimationBehavior), new PropertyMetadata((object) null));
    private static readonly DependencyProperty SeqNumProperty = DependencyProperty.RegisterAttached("SeqNum", typeof (int), typeof (AnimationBehavior), new PropertyMetadata((object) 0));

    public static Uri GetSourceUri(Image image)
    {
      return (Uri) ((DependencyObject) image).GetValue(AnimationBehavior.SourceUriProperty);
    }

    public static void SetSourceUri(Image image, Uri value)
    {
      ((DependencyObject) image).SetValue(AnimationBehavior.SourceUriProperty, (object) value);
    }

    public static Stream GetSourceStream(DependencyObject obj)
    {
      return (Stream) obj.GetValue(AnimationBehavior.SourceStreamProperty);
    }

    public static void SetSourceStream(DependencyObject obj, Stream value)
    {
      obj.SetValue(AnimationBehavior.SourceStreamProperty, (object) value);
    }

    public static RepeatBehavior GetRepeatBehavior(DependencyObject obj)
    {
      return (RepeatBehavior) obj.GetValue(AnimationBehavior.RepeatBehaviorProperty);
    }

    public static void SetRepeatBehavior(DependencyObject obj, RepeatBehavior value)
    {
      obj.SetValue(AnimationBehavior.RepeatBehaviorProperty, (object) value);
    }

    public static bool GetAutoStart(DependencyObject obj)
    {
      return (bool) obj.GetValue(AnimationBehavior.AutoStartProperty);
    }

    public static void SetAutoStart(DependencyObject obj, bool value)
    {
      obj.SetValue(AnimationBehavior.AutoStartProperty, (object) value);
    }

    public static bool GetAnimateInDesignMode(DependencyObject obj)
    {
      return (bool) obj.GetValue(AnimationBehavior.AnimateInDesignModeProperty);
    }

    public static void SetAnimateInDesignMode(DependencyObject obj, bool value)
    {
      obj.SetValue(AnimationBehavior.AnimateInDesignModeProperty, (object) value);
    }

    public static Animator GetAnimator(DependencyObject obj)
    {
      return (Animator) obj.GetValue(AnimationBehavior.AnimatorProperty);
    }

    private static void SetAnimator(DependencyObject obj, Animator value)
    {
      obj.SetValue(AnimationBehavior.AnimatorProperty, (object) value);
    }

    public static event EventHandler<AnimationErrorEventArgs> Error;

    internal static void OnError(Image image, Exception exception, AnimationErrorKind kind)
    {
      AnimationBehavior.Error((object) image, new AnimationErrorEventArgs((object) image, exception, kind));
    }

    private static void AnimatorError(object sender, AnimationErrorEventArgs e)
    {
      AnimationBehavior.Error(sender, e);
    }

    public static event EventHandler<DownloadProgressEventArgs> DownloadProgress;

    internal static void OnDownloadProgress(Image image, int downloadPercentage)
    {
      AnimationBehavior.DownloadProgress((object) image, new DownloadProgressEventArgs(downloadPercentage));
    }

    public static event EventHandler Loaded;

    private static void OnLoaded(Image sender)
    {
      AnimationBehavior.Loaded((object) sender, EventArgs.Empty);
    }

    private static int GetSeqNum(DependencyObject obj)
    {
      return (int) obj.GetValue(AnimationBehavior.SeqNumProperty);
    }

    private static void SetSeqNum(DependencyObject obj, int value)
    {
      obj.SetValue(AnimationBehavior.SeqNumProperty, (object) value);
    }

    private static void SourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
    {
      if (!(o is Image image))
        return;
      AnimationBehavior.InitAnimation(image);
    }

    private static void RepeatBehaviorChanged(
      DependencyObject o,
      DependencyPropertyChangedEventArgs e)
    {
      AnimationBehavior.GetAnimator(o).OnRepeatBehaviorChanged();
    }

    private static void AnimateInDesignModeChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is Image image))
        return;
      AnimationBehavior.InitAnimation(image);
    }

    private static bool CheckDesignMode(Image image, Uri sourceUri, Stream sourceStream)
    {
      if (AnimationBehavior.IsInDesignMode((DependencyObject) image))
      {
        if (!AnimationBehavior.GetAnimateInDesignMode((DependencyObject) image))
        {
          try
          {
            if (sourceStream != null)
              AnimationBehavior.SetStaticImage(image, sourceStream);
            else if (sourceUri != (Uri) null)
            {
              BitmapImage bitmapImage = new BitmapImage();
              bitmapImage.UriSource = sourceUri;
              image.Source = (ImageSource) bitmapImage;
            }
          }
          catch
          {
            image.Source = (ImageSource) null;
          }
          return false;
        }
      }
      return true;
    }

    //TODO via ChatGPT
    private static void InitAnimation(Image image)
    {
        if (AnimationBehavior.IsLoaded((FrameworkElement)image))
        {
            //WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>
            //    (new Func<RoutedEventHandler, EventRegistrationToken>(((FrameworkElement)image).add_Unloaded),
            //    new Action<EventRegistrationToken>(((FrameworkElement)image).remove_Unloaded),
            //    new RoutedEventHandler(AnimationBehavior.Image_Unloaded));

            int seqNum = AnimationBehavior.GetSeqNum((DependencyObject)image) + 1;
            AnimationBehavior.SetSeqNum((DependencyObject)image, seqNum);
            image.Source = (ImageSource)null;
            AnimationBehavior.ClearAnimatorCore(image);
            try
            {
                Stream sourceStream = AnimationBehavior.GetSourceStream((DependencyObject)image);
                if (sourceStream != null)
                {
                    AnimationBehavior.InitAnimationAsync(image, sourceStream.AsBuffered(),
                        AnimationBehavior.GetRepeatBehavior((DependencyObject)image), seqNum);
                }
                else
                {
                    Uri absoluteUri = AnimationBehavior.GetAbsoluteUri(image);
                    if (!(absoluteUri != (Uri)null))
                        return;
                    AnimationBehavior.InitAnimationAsync(image, absoluteUri,
                        AnimationBehavior.GetRepeatBehavior((DependencyObject)image), seqNum);
                }
            }
            catch (Exception ex)
            {
                AnimationBehavior.OnError(image, ex, AnimationErrorKind.Loading);
            }
        }
        else
        { 
            //WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>
            //        (new Func<RoutedEventHandler, EventRegistrationToken>(((FrameworkElement)image).add_Loaded),
            //        new Action<EventRegistrationToken>(((FrameworkElement)image).remove_Loaded),
            //        new RoutedEventHandler(AnimationBehavior.Image_Loaded));
        }
    }

    private static void Image_Loaded(object sender, RoutedEventArgs e)
    {
      Image image = (Image) sender;
      
       //TODO
       //WindowsRuntimeMarshal.RemoveEventHandler<RoutedEventHandler>(
       //   new Action<EventRegistrationToken>(((FrameworkElement) image).remove_Loaded),
       //   new RoutedEventHandler(AnimationBehavior.Image_Loaded));

      AnimationBehavior.InitAnimation(image);
    }

    private static void Image_Unloaded(object sender, RoutedEventArgs e)
    {
      Image image = (Image) sender;

          //TODO via ChatGPT
          //WindowsRuntimeMarshal.RemoveEventHandler<RoutedEventHandler>(
          //new Action<EventRegistrationToken>(((FrameworkElement) image).remove_Unloaded), 
          //new RoutedEventHandler(AnimationBehavior.Image_Unloaded));

          //TODO via ChatGPT
          //WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(
          //new Func<RoutedEventHandler, EventRegistrationToken>(((FrameworkElement) image).add_Loaded), 
          //new Action<EventRegistrationToken>(((FrameworkElement) image).remove_Loaded), 
          //new RoutedEventHandler(AnimationBehavior.Image_Loaded));

      AnimationBehavior.ClearAnimatorCore(image);
    }

    private static bool IsLoaded(FrameworkElement element)
    {
      return VisualTreeHelper.GetParent((DependencyObject) element) != null;
    }

    private static Uri GetAbsoluteUri(Image image)
    {
      Uri relativeUri = AnimationBehavior.GetSourceUri(image);
      if (relativeUri == (Uri) null)
        return (Uri) null;
      if (!relativeUri.IsAbsoluteUri)
      {
        Uri baseUri = ((FrameworkElement) image).BaseUri;
        relativeUri = baseUri != (Uri) null 
                    ? new Uri(baseUri, relativeUri) 
                    : throw new InvalidOperationException("Relative URI can't be resolved");
      }
      return relativeUri;
    }

    private static async void InitAnimationAsync(
      Image image,
      Uri sourceUri,
      RepeatBehavior repeatBehavior,
      int seqNum)
    {
      if (!AnimationBehavior.CheckDesignMode(image, sourceUri, (Stream) null))
        return;
      try
      {
        Progress<int> progress = new Progress<int>((Action<int>) (percentage => AnimationBehavior.OnDownloadProgress(image, percentage)));
        ImageAnimator animator = await ImageAnimator.CreateAsync(sourceUri, repeatBehavior, (IProgress<int>) progress, image);
        if (AnimationBehavior.GetSeqNum((DependencyObject) image) != seqNum)
        {
          animator.Dispose();
        }
        else
        {
          await AnimationBehavior.SetAnimatorCoreAsync(image, (Animator) animator);
          AnimationBehavior.OnLoaded(image);
        }
      }
      catch (InvalidSignatureException ex)
      {
        AnimationBehavior.SetStaticImageAsync(image, sourceUri);
        AnimationBehavior.OnLoaded(image);
      }
      catch (Exception ex)
      {
        AnimationBehavior.OnError(image, ex, AnimationErrorKind.Loading);
      }
    }

    private static async void InitAnimationAsync(
      Image image,
      Stream stream,
      RepeatBehavior repeatBehavior,
      int seqNum)
    {
      if (!AnimationBehavior.CheckDesignMode(image, (Uri) null, stream))
        return;
      try
      {
        ImageAnimator animator = await ImageAnimator.CreateAsync(stream, repeatBehavior, image);
        await AnimationBehavior.SetAnimatorCoreAsync(image, (Animator) animator);
        if (AnimationBehavior.GetSeqNum((DependencyObject) image) != seqNum)
          animator.Dispose();
        else
          AnimationBehavior.OnLoaded(image);
      }
      catch (InvalidSignatureException ex)
      {
        AnimationBehavior.SetStaticImage(image, stream);
        AnimationBehavior.OnLoaded(image);
      }
      catch (Exception ex)
      {
        AnimationBehavior.OnError(image, ex, AnimationErrorKind.Loading);
      }
    }

    private static async Task SetAnimatorCoreAsync(Image image, Animator animator)
    {
      AnimationBehavior.SetAnimator((DependencyObject) image, animator);
      animator.Error += new EventHandler<AnimationErrorEventArgs>(AnimationBehavior.AnimatorError);
      image.Source = (ImageSource) animator.Bitmap;
      if (AnimationBehavior.GetAutoStart((DependencyObject) image))
        animator.Play();
      else
        await animator.ShowFirstFrameAsync();
    }

    private static void ClearAnimatorCore(Image image)
    {
      Animator animator = AnimationBehavior.GetAnimator((DependencyObject) image);
      if (animator == null)
        return;
      animator.Error -= new EventHandler<AnimationErrorEventArgs>(AnimationBehavior.AnimatorError);
      animator.Dispose();
      AnimationBehavior.SetAnimator((DependencyObject) image, (Animator) null);
    }

    private static bool IsInDesignMode(DependencyObject obj) => DesignMode.DesignModeEnabled;

    private static async Task SetStaticImageAsync(Image image, Uri sourceUri)
    {
      try
      {
        UriLoader loader = new UriLoader();
        Progress<int> progress = new Progress<int>((Action<int>) 
            (percentage => AnimationBehavior.OnDownloadProgress(image, percentage)));

        Stream stream = await loader.GetStreamFromUriAsync(sourceUri, (IProgress<int>) progress);
        AnimationBehavior.SetStaticImageCore(image, stream);
      }
      catch (Exception ex)
      {
        AnimationBehavior.OnError(image, ex, AnimationErrorKind.Loading);
      }
    }

    private static void SetStaticImage(Image image, Stream stream)
    {
      try
      {
        AnimationBehavior.SetStaticImageCore(image, stream);
      }
      catch (Exception ex)
      {
        AnimationBehavior.OnError(image, ex, AnimationErrorKind.Loading);
      }
    }

    private static void SetStaticImageCore(Image image, Stream stream)
    {
      stream.Seek(0L, SeekOrigin.Begin);
      BitmapImage bitmapImage = new BitmapImage();
      ((BitmapSource) bitmapImage).SetSource(stream.AsRandomAccessStream());
      image.Source = (ImageSource) bitmapImage;
    }
  }
}
