// Decompiled with JetBrains decompiler
// Type: App1uwp.UC.ImageViewerDecoratorUC
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
using App1uwp.Network.DataObjects;
using App1uwp.Utils;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Phone.UI.Input;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

#nullable disable
namespace App1uwp.UC
{
  public sealed class ImageViewerDecoratorUC : UserControl, IComponentConnector
  {
    private readonly int DURATION_BOUNCING = 170;
    private readonly double MIN_SCALE = 0.5;
    private readonly double MAX_SCALE = 4.0;
    private readonly EasingFunctionBase ANIMATION_EASING;
    private List<Image> _images;
    private bool _isInPinch;
    private double HIDE_AFTER_VERT_SWIPE_THRESHOLD = 100.0;
    private readonly int ANIMATION_DURATION_MS = 250;
    private double HIDE_AFTER_VERT_SWIPE_VELOCITY_THRESHOLD = 100.0;
    private Func<int, Image> _getImageFunc;
    private int _currentInd;
    private readonly double MOVE_TO_NEXT_VELOCITY_THRESHOLD = 100.0;
    private double MARGIN_BETWEEN_IMAGES = 12.0;
    private List<VKPhoto> _photos;
    private int DURATION_MOVE_TO_NEXT = 200;
    private ImageAnimator _imageAnimator;
    public readonly int ANIMATION_INOUT_DURATION_MS = 300;
    public readonly EasingFunctionBase ANIMATION_EASING_IN_OUT;
    private PopUpService _flyout;
    private bool IsInVerticalSwipe;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Rectangle _blackRectangle;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Canvas imageViewer;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Grid gridTop;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Grid gridBottom;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBlock textBoxText;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBlock ShareCountStr;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBlock CommentsCountStr;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBlock LikesCountStr;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBlock textBlockCounter;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public ImageViewerDecoratorUC()
    {
      this.InitializeComponent();
      this._images = new List<Image>();
      this._images.Add(new Image());
      this._images.Add(new Image());
      this._images.Add(new Image());
      ((ICollection<UIElement>) ((Panel) this.imageViewer).Children).Add((UIElement) this._images[0]);
      ((ICollection<UIElement>) ((Panel) this.imageViewer).Children).Add((UIElement) this._images[1]);
      ((ICollection<UIElement>) ((Panel) this.imageViewer).Children).Add((UIElement) this._images[2]);
      UserControl userControl1 = (UserControl) this;
      WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(((FrameworkElement) userControl1).add_Loaded), new Action<EventRegistrationToken>(((FrameworkElement) userControl1).remove_Loaded), new RoutedEventHandler(this.ImageViewerDecoratorUC_Loaded));
      CubicEase cubicEase = new CubicEase();
      ((EasingFunctionBase) cubicEase).put_EasingMode((EasingMode) 0);
      this.ANIMATION_EASING = (EasingFunctionBase) cubicEase;
      QuadraticEase quadraticEase = new QuadraticEase();
      ((EasingFunctionBase) quadraticEase).put_EasingMode((EasingMode) 2);
      this.ANIMATION_EASING_IN_OUT = (EasingFunctionBase) quadraticEase;
      this._imageAnimator = new ImageAnimator(this.ANIMATION_INOUT_DURATION_MS, this.ANIMATION_EASING_IN_OUT);
      ((UIElement) this._blackRectangle).put_Opacity(0.0);
      (((UIElement) this.gridTop).RenderTransform as CompositeTransform).put_TranslateY(-((FrameworkElement) this.gridTop).Height);
      (((UIElement) this.gridBottom).RenderTransform as CompositeTransform).put_TranslateY(((FrameworkElement) this.gridBottom).Height);
      UserControl userControl2 = (UserControl) this;
      WindowsRuntimeMarshal.AddEventHandler<SizeChangedEventHandler>(new Func<SizeChangedEventHandler, EventRegistrationToken>(((FrameworkElement) userControl2).add_SizeChanged), new Action<EventRegistrationToken>(((FrameworkElement) userControl2).remove_SizeChanged), new SizeChangedEventHandler(this.ImageViewerDecoratorUC_SizeChanged));
    }

    private void ImageViewerDecoratorUC_SizeChanged(object sender, SizeChangedEventArgs e)
    {
      this.ArrangeImages();
      this.UpdateImagesSources();
    }

    private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
    {
      e.put_Handled(true);
      this.Hide();
    }

    public Image CurrentImage => this._images[1];

    private CompositeTransform CurrentImageTransform
    {
      get => ((UIElement) this.CurrentImage).RenderTransform as CompositeTransform;
    }

    private double CurrentImageScale => this.CurrentImageTransform.ScaleX;

    private void ImageViewerDecoratorUC_Loaded(object sender, RoutedEventArgs e)
    {
      this.ArrangeImages();
      Image originalImage = this.OriginalImage;
      if (originalImage != null)
        this.CurrentImage.put_Source(originalImage.Source);
      this._imageAnimator.AnimateIn(this.GetImageSizeSafelyBy(), this.OriginalImage, this.CurrentImage, (Action) (() =>
      {
        ((UIElement) this).put_IsHitTestVisible(true);
        this.UpdateImagesSources();
      }));
      ((DependencyObject) this._blackRectangle).Animate(((UIElement) this._blackRectangle).Opacity, 1.0, "Opacity", this.ANIMATION_INOUT_DURATION_MS, easing: this.ANIMATION_EASING);
    }

    private void imageViewer_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
    {
      double actualWidth = ((FrameworkElement) this.CurrentImage).ActualWidth;
      double scaleX = this.CurrentImageTransform.ScaleX;
      double actualHeight = ((FrameworkElement) this.CurrentImage).ActualHeight;
      double scaleY = this.CurrentImageTransform.ScaleY;
      e.put_Handled(true);
      if ((double) e.Delta.Scale == 1.0)
      {
        this.HandleDragDelta(e.Delta.Translation.X, e.Delta.Translation.Y);
      }
      else
      {
        if (!this._isInPinch)
          this._isInPinch = true;
        this.HandlePinch(e.Position, e.Delta.Scale);
      }
    }

    private double Clamp(double val, double min, double max)
    {
      if (val <= min)
        return min;
      return val >= max ? max : val;
    }

    private double TranslateInterval(double x, double a, double b, double toA, double toB)
    {
      return (toB * (x - a) + toA * (b - x)) / (b - a);
    }

    private void HandlePinch(Point pos, float scale)
    {
      CompositeTransform renderTransform = ((UIElement) this.CurrentImage).RenderTransform as CompositeTransform;
      CompositeTransform compositeTransform1 = renderTransform;
      compositeTransform1.put_ScaleX(compositeTransform1.ScaleX * (double) scale);
      CompositeTransform compositeTransform2 = renderTransform;
      compositeTransform2.put_ScaleY(compositeTransform2.ScaleY * (double) scale);
      if (renderTransform.ScaleX < this.MIN_SCALE)
      {
        CompositeTransform compositeTransform3 = renderTransform;
        double minScale;
        renderTransform.put_ScaleY(minScale = this.MIN_SCALE);
        double num = minScale;
        compositeTransform3.put_ScaleX(num);
      }
      else if (renderTransform.ScaleX > this.MAX_SCALE)
      {
        CompositeTransform compositeTransform4 = renderTransform;
        double maxScale;
        renderTransform.put_ScaleY(maxScale = this.MAX_SCALE);
        double num = maxScale;
        compositeTransform4.put_ScaleX(num);
      }
      else
      {
        double x = pos.X;
        double y = pos.Y;
        double num1 = x - x * (double) scale;
        double num2 = y - y * (double) scale;
        double num3 = num1 * renderTransform.ScaleX;
        double num4 = num2 * renderTransform.ScaleX;
        CompositeTransform compositeTransform5 = renderTransform;
        compositeTransform5.put_TranslateX(compositeTransform5.TranslateX + num3);
        CompositeTransform compositeTransform6 = renderTransform;
        compositeTransform6.put_TranslateY(compositeTransform6.TranslateY + num4);
      }
    }

    private void HandleDragDelta(double hDelta, double vDelta)
    {
      double num1 = hDelta;
      double num2 = vDelta;
      if (this.CurrentImageScale == 1.0)
      {
        CompositeTransform renderTransform1 = ((UIElement) this.CurrentImage).RenderTransform as CompositeTransform;
        if (renderTransform1.TranslateX == 0.0 && (this.IsInVerticalSwipe || num1 == 0.0 && num2 != 0.0 || Math.Abs(num2) / Math.Abs(num1) > 1.2))
        {
          this.IsInVerticalSwipe = true;
          CompositeTransform compositeTransform = renderTransform1;
          compositeTransform.put_TranslateY(compositeTransform.TranslateY + num2);
        }
        else
        {
          if (this._currentInd == 0 && num1 > 0.0 && renderTransform1.TranslateX > 0.0 || this._currentInd == this._photos.Count - 1 && num1 < 0.0 && renderTransform1.TranslateX < 0.0)
            num1 /= 3.0;
          foreach (UIElement image in this._images)
          {
            CompositeTransform renderTransform2 = image.RenderTransform as CompositeTransform;
            renderTransform2.put_TranslateX(renderTransform2.TranslateX + num1);
          }
        }
      }
      else
      {
        CompositeTransform currentImageTransform = this.CurrentImageTransform;
        currentImageTransform.put_TranslateX(currentImageTransform.TranslateX + num1);
        currentImageTransform.put_TranslateY(currentImageTransform.TranslateY + num2);
      }
    }

    private void imageViewer_ManipulationCompleted(
      object sender,
      ManipulationCompletedRoutedEventArgs e)
    {
      e.put_Handled(true);
      if (this._isInPinch)
      {
        this.HandlePinchCompleted();
        this._isInPinch = false;
      }
      else
        this.HandleDragCompleted(e.Cumulative.Translation.X, e.Cumulative.Translation.Y);
    }

    public Size GetImageSizeSafelyBy()
    {
      return new Size(((FrameworkElement) this.CurrentImage).ActualWidth, ((FrameworkElement) this.CurrentImage).ActualHeight);
    }

    public Size GetImageSizeSafelyBy(int ind)
    {
      return new Size((double) this._photos[ind].width, (double) this._photos[ind].height);
    }

    public Rect CurrentImageFitRectOriginal
    {
      get
      {
        return RectangleUtils.ResizeToFit(new Rect(new Point(), new Size(((FrameworkElement) this).ActualWidth, ((FrameworkElement) this).ActualHeight)), this.GetImageSizeSafelyBy());
      }
    }

    private void HandleDragCompleted(double hVelocity, double vVelocity)
    {
      double num1 = hVelocity;
      double num2 = vVelocity;
      if (this.CurrentImageScale != 1.0)
        return;
      if (this.IsInVerticalSwipe)
      {
        CompositeTransform currentImageTransform = this.CurrentImageTransform;
        if (Math.Abs(currentImageTransform.TranslateY) < this.HIDE_AFTER_VERT_SWIPE_THRESHOLD && num2 < this.HIDE_AFTER_VERT_SWIPE_VELOCITY_THRESHOLD)
          ((DependencyObject) currentImageTransform).Animate(currentImageTransform.TranslateY, 0.0, "TranslateY", this.ANIMATION_DURATION_MS, easing: this.ANIMATION_EASING);
        else
          this.Hide();
        this.IsInVerticalSwipe = false;
      }
      else
      {
        bool? moveNext = new bool?();
        double translateX = (((UIElement) this._images[1]).RenderTransform as CompositeTransform).TranslateX;
        double num3 = num1;
        if ((num3 < -this.MOVE_TO_NEXT_VELOCITY_THRESHOLD && translateX < 0.0 || translateX <= -((FrameworkElement) this).ActualWidth / 2.0) && this._currentInd < this._photos.Count - 1)
          moveNext = new bool?(true);
        else if ((num3 > this.MOVE_TO_NEXT_VELOCITY_THRESHOLD && translateX > 0.0 || translateX >= ((FrameworkElement) this).ActualWidth / 2.0) && this._currentInd > 0)
          moveNext = new bool?(false);
        double num4 = 0.0;
        bool? nullable1 = moveNext;
        bool flag1 = true;
        if ((nullable1.GetValueOrDefault() == flag1 ? (nullable1.HasValue ? 1 : 0) : 0) != 0)
        {
          num4 = -((FrameworkElement) this).ActualWidth - this.MARGIN_BETWEEN_IMAGES;
        }
        else
        {
          bool? nullable2 = moveNext;
          bool flag2 = false;
          if ((nullable2.GetValueOrDefault() == flag2 ? (nullable2.HasValue ? 1 : 0) : 0) != 0)
            num4 = ((FrameworkElement) this).ActualWidth + this.MARGIN_BETWEEN_IMAGES;
        }
        double delta = num4 - translateX;
        if (moveNext.HasValue && moveNext.Value)
        {
          this.AnimateTwoImagesOnDragComplete(this._images[1], this._images[2], delta, (Action) (() =>
          {
            this.MoveToNextOrPrevious(moveNext.Value);
            this.ArrangeImages();
          }), moveNext.HasValue);
          this.ChangeCurrentInd(moveNext.Value);
        }
        else if (moveNext.HasValue && !moveNext.Value)
        {
          this.AnimateTwoImagesOnDragComplete(this._images[0], this._images[1], delta, (Action) (() =>
          {
            this.MoveToNextOrPrevious(moveNext.Value);
            this.ArrangeImages();
          }), moveNext.HasValue);
          this.ChangeCurrentInd(moveNext.Value);
        }
        else
        {
          if (delta == 0.0)
            return;
          this.AnimateImageOnDragComplete(this._images[0], delta, (Action) null, moveNext.HasValue);
          this.AnimateImageOnDragComplete(this._images[1], delta, (Action) null, moveNext.HasValue);
          this.AnimateImageOnDragComplete(this._images[2], delta, new Action(this.ArrangeImages), moveNext.HasValue);
        }
      }
    }

    private void HandlePinchCompleted()
    {
      if ((((UIElement) this.CurrentImage).RenderTransform as CompositeTransform).ScaleX < 1.0)
        this.AnimateImage();
      else
        this.EnsureBoundaries();
    }

    private void MoveToNextOrPrevious(bool next)
    {
      if (next)
      {
        this.Swap(this._images, 0, 1);
        this.Swap(this._images, 1, 2);
      }
      else
      {
        this.Swap(this._images, 1, 2);
        this.Swap(this._images, 0, 1);
      }
      this.UpdateImagesSources(movedForvard: new bool?(next));
    }

    private void Swap(List<Image> images, int ind1, int ind2)
    {
      Image image = images[ind1];
      images[ind1] = images[ind2];
      images[ind2] = image;
    }

    public Rect CurrentImageFitRectTransformed
    {
      get
      {
        return ((GeneralTransform) this.CurrentImageTransform).TransformBounds(this.CurrentImageFitRectOriginal);
      }
    }

    private void EnsureBoundaries()
    {
      Rect fitRectTransformed = this.CurrentImageFitRectTransformed;
      Rect target = RectangleUtils.AlignRects(new Rect(new Point(), new Size(((FrameworkElement) this).ActualWidth, ((FrameworkElement) this).ActualHeight)), fitRectTransformed, false);
      if (target == fitRectTransformed)
        return;
      CompositeTransform compositeTransform = RectangleUtils.TransformRect(this.CurrentImageFitRectOriginal, target);
      this.AnimateImage(compositeTransform.ScaleX, compositeTransform.ScaleY, compositeTransform.TranslateX, compositeTransform.TranslateY);
    }

    private void imageViewer_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
      e.put_Handled(true);
      if (this.CurrentImageScale == 1.0)
      {
        Point position = e.GetPosition((UIElement) this.CurrentImage);
        Rect imageFitRectOriginal = this.CurrentImageFitRectOriginal;
        CompositeTransform compositeTransform1 = new CompositeTransform();
        CompositeTransform compositeTransform2 = compositeTransform1;
        double num1;
        compositeTransform1.put_ScaleY(num1 = 2.0);
        double num2 = num1;
        compositeTransform2.put_ScaleX(num2);
        compositeTransform1.put_TranslateX(-position.X);
        compositeTransform1.put_TranslateY(-position.Y);
        Rect target = RectangleUtils.AlignRects(new Rect(new Point(), new Size(((FrameworkElement) this).ActualWidth, ((FrameworkElement) this).ActualHeight)), ((GeneralTransform) compositeTransform1).TransformBounds(imageFitRectOriginal), false);
        CompositeTransform compositeTransform3 = RectangleUtils.TransformRect(imageFitRectOriginal, target, true);
        this.AnimateImage(compositeTransform3.ScaleX, compositeTransform3.ScaleY, compositeTransform3.TranslateX, compositeTransform3.TranslateY);
      }
      else
        this.AnimateImage();
    }

    public void AnimateImage(
      double toScaleX = 1.0,
      double toScaleY = 1.0,
      double toTranslateX = 0.0,
      double toTranslateY = 0.0,
      Action completionCallback = null)
    {
      CompositeTransform renderTransform = ((UIElement) this.CurrentImage).RenderTransform as CompositeTransform;
      ((DependencyObject) renderTransform).Animate(renderTransform.ScaleX, toScaleX, "ScaleX", this.DURATION_BOUNCING, easing: this.ANIMATION_EASING);
      ((DependencyObject) renderTransform).Animate(renderTransform.ScaleY, toScaleY, "ScaleY", this.DURATION_BOUNCING, easing: this.ANIMATION_EASING);
      ((DependencyObject) renderTransform).Animate(renderTransform.TranslateX, toTranslateX, "TranslateX", this.DURATION_BOUNCING, easing: this.ANIMATION_EASING);
      ((DependencyObject) renderTransform).Animate(renderTransform.TranslateY, toTranslateY, "TranslateY", this.DURATION_BOUNCING, easing: this.ANIMATION_EASING, completed: completionCallback);
    }

    private void SetImageSource(Image image, string source)
    {
      if (source == null)
        image.put_Source((ImageSource) null);
      else
        image.put_Source((ImageSource) new BitmapImage(new Uri(source)));
    }

    private void ArrangeImages()
    {
      Image image1 = this._images[0];
      CompositeTransform compositeTransform1 = new CompositeTransform();
      compositeTransform1.put_TranslateX(-((FrameworkElement) this).ActualWidth);
      CompositeTransform compositeTransform2 = compositeTransform1;
      ((UIElement) image1).put_RenderTransform((Transform) compositeTransform2);
      ((UIElement) this._images[1]).put_RenderTransform((Transform) new CompositeTransform());
      Image image2 = this._images[2];
      CompositeTransform compositeTransform3 = new CompositeTransform();
      compositeTransform3.put_TranslateX(((FrameworkElement) this).ActualWidth);
      CompositeTransform compositeTransform4 = compositeTransform3;
      ((UIElement) image2).put_RenderTransform((Transform) compositeTransform4);
      ((FrameworkElement) this._images[0]).put_Width(((FrameworkElement) this).ActualWidth);
      ((FrameworkElement) this._images[1]).put_Width(((FrameworkElement) this).ActualWidth);
      ((FrameworkElement) this._images[2]).put_Width(((FrameworkElement) this).ActualWidth);
      ((FrameworkElement) this._images[0]).put_Height(((FrameworkElement) this).ActualHeight);
      ((FrameworkElement) this._images[1]).put_Height(((FrameworkElement) this).ActualHeight);
      ((FrameworkElement) this._images[2]).put_Height(((FrameworkElement) this).ActualHeight);
    }

    private Image OriginalImage
    {
      get => this._getImageFunc == null ? (Image) null : this._getImageFunc(this._currentInd);
    }

    public void Hide(Action callback = null, bool leavingPageImmediately = false)
    {
      ApplicationView.GetForCurrentView().put_SuppressSystemOverlays(false);
      bool? clockwiseRotation = new bool?();
      Image thumbImage = this.OriginalImage;
      ((UIElement) thumbImage).put_Opacity(0.0);
      if (!leavingPageImmediately)
      {
        this._imageAnimator.AnimateOut(this.GetImageSizeSafelyBy(), thumbImage, this.CurrentImage, clockwiseRotation, (Action) (() =>
        {
          ((UIElement) thumbImage).put_Opacity(1.0);
          if (this._flyout == null)
            return;
          this._flyout.Hide();
        }));
        ((DependencyObject) this._blackRectangle).Animate(((UIElement) this._blackRectangle).Opacity, 0.0, "Opacity", this.ANIMATION_INOUT_DURATION_MS);
        ((DependencyObject) this.textBoxText).Animate(((UIElement) this.textBoxText).Opacity, 0.0, "Opacity", this.ANIMATION_INOUT_DURATION_MS);
        CompositeTransform renderTransform1 = ((UIElement) this.gridTop).RenderTransform as CompositeTransform;
        ((DependencyObject) renderTransform1).Animate(renderTransform1.TranslateY, -((FrameworkElement) this.gridTop).Height, "TranslateY", this.ANIMATION_DURATION_MS, easing: this.ANIMATION_EASING);
        CompositeTransform renderTransform2 = ((UIElement) this.gridBottom).RenderTransform as CompositeTransform;
        ((DependencyObject) renderTransform2).Animate(renderTransform2.TranslateY, ((FrameworkElement) this.gridBottom).Height, "TranslateY", this.ANIMATION_DURATION_MS, easing: this.ANIMATION_EASING);
      }
      else
      {
        if (callback != null)
          callback();
        if (this._flyout != null)
          this._flyout.Hide();
      }
      WindowsRuntimeMarshal.RemoveEventHandler<EventHandler<BackPressedEventArgs>>(new Action<EventRegistrationToken>(HardwareButtons.remove_BackPressed), new EventHandler<BackPressedEventArgs>(this.HardwareButtons_BackPressed));
    }

    public void Initialize(List<VKPhoto> photos, Func<int, Image> getImageFunc)
    {
      this._photos = photos;
      this._getImageFunc = getImageFunc;
      this.UpdateImagesSources();
    }

    private void UpdateImagesSources(bool keepCurrentAsIs = false, bool? movedForvard = null)
    {
      if (!keepCurrentAsIs && !movedForvard.HasValue)
        this.SetImageSource(this._images[1], this.GetImageSource(this._currentInd));
      int num = !movedForvard.HasValue ? 1 : (!movedForvard.HasValue ? 0 : (movedForvard.Value ? 1 : 0));
      if ((!movedForvard.HasValue ? 1 : (!movedForvard.HasValue ? 0 : (!movedForvard.Value ? 1 : 0))) != 0)
      {
        string imageSource = this.GetImageSource(this._currentInd - 1);
        this.SetImageSource(this._images[0], (string) null);
        this.SetImageSource(this._images[0], imageSource);
      }
      if (num == 0)
        return;
      string imageSource1 = this.GetImageSource(this._currentInd + 1);
      this.SetImageSource(this._images[2], (string) null);
      this.SetImageSource(this._images[2], imageSource1);
    }

    private string GetImageSource(int ind)
    {
      if (ind < 0 || ind >= this._photos.Count)
        return (string) null;
      if (!string.IsNullOrEmpty(this._photos[ind].photo_2560))
        return this._photos[ind].photo_2560;
      if (!string.IsNullOrEmpty(this._photos[ind].photo_1280))
        return this._photos[ind].photo_1280;
      return !string.IsNullOrEmpty(this._photos[ind].photo_807) ? this._photos[ind].photo_807 : this._photos[ind].photo_604;
    }

    private void ChangeCurrentInd(bool next)
    {
      this._currentInd = !next ? this._currentInd - 1 : this._currentInd + 1;
      this.Update();
    }

    private void AnimateTwoImagesOnDragComplete(
      Image image1,
      Image image2,
      double delta,
      Action completedCallback,
      bool movingToNextOrPrevious)
    {
      int num = movingToNextOrPrevious ? this.DURATION_MOVE_TO_NEXT : this.DURATION_BOUNCING;
      List<AnimationUtils.AnimationInfo> animInfoList = new List<AnimationUtils.AnimationInfo>();
      CompositeTransform renderTransform1 = ((UIElement) image1).RenderTransform as CompositeTransform;
      CompositeTransform renderTransform2 = ((UIElement) image2).RenderTransform as CompositeTransform;
      animInfoList.Add(new AnimationUtils.AnimationInfo()
      {
        from = renderTransform1.TranslateX,
        to = renderTransform1.TranslateX + delta,
        propertyPath = "TranslateX",
        duration = num,
        target = (DependencyObject) renderTransform1,
        easing = this.ANIMATION_EASING
      });
      animInfoList.Add(new AnimationUtils.AnimationInfo()
      {
        from = renderTransform2.TranslateX,
        to = renderTransform2.TranslateX + delta,
        propertyPath = "TranslateX",
        duration = num,
        target = (DependencyObject) renderTransform2,
        easing = this.ANIMATION_EASING
      });
      int? startTime = new int?(0);
      Action completed = (Action) (() => completedCallback());
      AnimationUtils.AnimateSeveral(animInfoList, startTime, completed);
    }

    private void AnimateImageOnDragComplete(
      Image image,
      double delta,
      Action completedCallback,
      bool movingToNextOrPrevious)
    {
      int duration = movingToNextOrPrevious ? this.DURATION_MOVE_TO_NEXT : this.DURATION_BOUNCING;
      CompositeTransform renderTransform = ((UIElement) image).RenderTransform as CompositeTransform;
      ((DependencyObject) renderTransform).Animate(renderTransform.TranslateX, renderTransform.TranslateX + delta, "TranslateX", duration, easing: this.ANIMATION_EASING, completed: completedCallback);
    }

    public void Show(int ind)
    {
      ApplicationView.GetForCurrentView().put_SuppressSystemOverlays(true);
      this._flyout = new PopUpService();
      this._flyout.AnimationTypeChild = PopUpService.AnimationTypes.None;
      this._flyout.BackgroundBrush = (Brush) null;
      this._flyout.Child = (FrameworkElement) this;
      this._currentInd = ind;
      this.SetImageSource(this.CurrentImage, (string) null);
      Image thumbImage = this.OriginalImage;
      ((UIElement) thumbImage).put_Opacity(0.0);
      this._flyout.Opened += (EventHandler) ((sender, args) =>
      {
        ((UIElement) thumbImage).put_Opacity(1.0);
        CompositeTransform renderTransform1 = ((UIElement) this.gridTop).RenderTransform as CompositeTransform;
        ((DependencyObject) renderTransform1).Animate(renderTransform1.TranslateY, 0.0, "TranslateY", this.ANIMATION_DURATION_MS, easing: this.ANIMATION_EASING);
        CompositeTransform renderTransform2 = ((UIElement) this.gridBottom).RenderTransform as CompositeTransform;
        ((DependencyObject) renderTransform2).Animate(renderTransform2.TranslateY, 0.0, "TranslateY", this.ANIMATION_DURATION_MS, easing: this.ANIMATION_EASING);
      });
      this._flyout.Show();
      this.Update();
      WindowsRuntimeMarshal.AddEventHandler<EventHandler<BackPressedEventArgs>>(new Func<EventHandler<BackPressedEventArgs>, EventRegistrationToken>(HardwareButtons.add_BackPressed), new Action<EventRegistrationToken>(HardwareButtons.remove_BackPressed), new EventHandler<BackPressedEventArgs>(this.HardwareButtons_BackPressed));
    }

    private void Update()
    {
      if (this._photos.Count > 0)
      {
        this.textBlockCounter.put_Text(string.Format(LocalizedStrings.GetString("ImageViewer_PhotoCounterFrm"), (object) (this._currentInd + 1), (object) this._photos.Count));
        ((UIElement) this.textBlockCounter).put_Visibility((Visibility) 0);
      }
      else
      {
        this.textBlockCounter.put_Text("");
        ((UIElement) this.textBlockCounter).put_Visibility((Visibility) 1);
      }
      this.textBoxText.put_Text(this._photos[this._currentInd].text);
      TextBlock shareCountStr = this.ShareCountStr;
      TextBlock commentsCountStr = this.CommentsCountStr;
      Visibility visibility1;
      ((UIElement) this.LikesCountStr).put_Visibility((Visibility) (int) (visibility1 = (Visibility) 1));
      Visibility visibility2;
      Visibility visibility3 = visibility2 = visibility1;
      ((UIElement) commentsCountStr).put_Visibility(visibility2);
      Visibility visibility4 = visibility3;
      ((UIElement) shareCountStr).put_Visibility(visibility4);
    }

    private void imageViewer_Tapped(object sender, TappedRoutedEventArgs e)
    {
      CompositeTransform renderTransform1 = ((UIElement) this.gridTop).RenderTransform as CompositeTransform;
      CompositeTransform renderTransform2 = ((UIElement) this.gridBottom).RenderTransform as CompositeTransform;
      if (renderTransform1.TranslateY == 0.0)
      {
        ((DependencyObject) renderTransform1).Animate(renderTransform1.TranslateY, -((FrameworkElement) this.gridTop).Height, "TranslateY", this.ANIMATION_DURATION_MS, easing: this.ANIMATION_EASING);
        ((DependencyObject) renderTransform2).Animate(renderTransform2.TranslateY, ((FrameworkElement) this.gridBottom).Height, "TranslateY", this.ANIMATION_DURATION_MS, easing: this.ANIMATION_EASING);
        ((DependencyObject) this.textBoxText).Animate(0.8, 0.0, "Opacity", this.ANIMATION_INOUT_DURATION_MS);
      }
      else
      {
        ((DependencyObject) renderTransform1).Animate(renderTransform1.TranslateY, 0.0, "TranslateY", this.ANIMATION_DURATION_MS, easing: this.ANIMATION_EASING);
        ((DependencyObject) renderTransform2).Animate(renderTransform2.TranslateY, 0.0, "TranslateY", this.ANIMATION_DURATION_MS, easing: this.ANIMATION_EASING);
        ((DependencyObject) this.textBoxText).Animate(0.0, 0.8, "Opacity", this.ANIMATION_INOUT_DURATION_MS);
      }
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///UC/ImageViewerDecoratorUC.xaml"), (ComponentResourceLocation) 0);
      this._blackRectangle = (Rectangle) ((FrameworkElement) this).FindName("_blackRectangle");
      this.imageViewer = (Canvas) ((FrameworkElement) this).FindName("imageViewer");
      this.gridTop = (Grid) ((FrameworkElement) this).FindName("gridTop");
      this.gridBottom = (Grid) ((FrameworkElement) this).FindName("gridBottom");
      this.textBoxText = (TextBlock) ((FrameworkElement) this).FindName("textBoxText");
      this.ShareCountStr = (TextBlock) ((FrameworkElement) this).FindName("ShareCountStr");
      this.CommentsCountStr = (TextBlock) ((FrameworkElement) this).FindName("CommentsCountStr");
      this.LikesCountStr = (TextBlock) ((FrameworkElement) this).FindName("LikesCountStr");
      this.textBlockCounter = (TextBlock) ((FrameworkElement) this).FindName("textBlockCounter");
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void Connect(int connectionId, object target)
    {
      if (connectionId == 1)
      {
        UIElement uiElement1 = (UIElement) target;
        WindowsRuntimeMarshal.AddEventHandler<ManipulationDeltaEventHandler>(new Func<ManipulationDeltaEventHandler, EventRegistrationToken>(uiElement1.add_ManipulationDelta), new Action<EventRegistrationToken>(uiElement1.remove_ManipulationDelta), new ManipulationDeltaEventHandler(this.imageViewer_ManipulationDelta));
        UIElement uiElement2 = (UIElement) target;
        WindowsRuntimeMarshal.AddEventHandler<ManipulationCompletedEventHandler>(new Func<ManipulationCompletedEventHandler, EventRegistrationToken>(uiElement2.add_ManipulationCompleted), new Action<EventRegistrationToken>(uiElement2.remove_ManipulationCompleted), new ManipulationCompletedEventHandler(this.imageViewer_ManipulationCompleted));
        UIElement uiElement3 = (UIElement) target;
        WindowsRuntimeMarshal.AddEventHandler<DoubleTappedEventHandler>(new Func<DoubleTappedEventHandler, EventRegistrationToken>(uiElement3.add_DoubleTapped), new Action<EventRegistrationToken>(uiElement3.remove_DoubleTapped), new DoubleTappedEventHandler(this.imageViewer_DoubleTapped));
        UIElement uiElement4 = (UIElement) target;
        WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement4.add_Tapped), new Action<EventRegistrationToken>(uiElement4.remove_Tapped), new TappedEventHandler(this.imageViewer_Tapped));
      }
      this._contentLoaded = true;
    }
  }
}
