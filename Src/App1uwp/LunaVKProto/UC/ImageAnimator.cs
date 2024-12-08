// Decompiled with JetBrains decompiler
// Type: App1uwp.UC.ImageAnimator
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Utils;
using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

#nullable disable
namespace App1uwp.UC
{
  public class ImageAnimator
  {
    private EasingFunctionBase _easingFunction;
    private int _animationDurationMs;

    public ImageAnimator(int animationDurationMs, EasingFunctionBase easingFunction)
    {
      this._animationDurationMs = animationDurationMs;
      this._easingFunction = easingFunction;
    }

    public void AnimateIn(
      Size imageSize,
      Image imageOriginal,
      Image imageFit,
      Action completionCallback = null,
      int startTime = 0)
    {
      if (imageOriginal == null)
      {
        completionCallback();
      }
      else
      {
        Size size = new Size(((FrameworkElement) imageFit).Width, ((FrameworkElement) imageFit).Height);
        Rect fill = RectangleUtils.ResizeToFill(RectangleUtils.ResizeToFill(new Size(((FrameworkElement) imageOriginal).ActualWidth, ((FrameworkElement) imageOriginal).ActualHeight), imageSize), size);
        Rect target1 = ((UIElement) imageOriginal).TransformToVisual((UIElement) imageFit).TransformBounds(fill);
        ((UIElement) imageFit).put_RenderTransform((Transform) RectangleUtils.TransformRect(new Rect(new Point(), size), target1));
        Rect source = ((UIElement) imageOriginal).TransformToVisual((UIElement) imageFit).TransformBounds(new Rect(0.0, 0.0, ((FrameworkElement) imageOriginal).ActualWidth, ((FrameworkElement) imageOriginal).ActualHeight));
        CompositeTransform target2 = new CompositeTransform();
        RectangleGeometry rectangleGeometry = new RectangleGeometry();
        Rect rect = source;
        rectangleGeometry.put_Rect(rect);
        ((Geometry) rectangleGeometry).put_Transform((Transform) target2);
        ((UIElement) imageFit).put_Clip(rectangleGeometry);
        CompositeTransform compositeTransform = RectangleUtils.TransformRect(source, new Rect(new Point(), size));
        ((DependencyObject) target2).Animate(0.0, compositeTransform.TranslateY, "TranslateY", this._animationDurationMs, startTime, this._easingFunction);
        ((DependencyObject) target2).Animate(0.0, compositeTransform.TranslateX, "TranslateX", this._animationDurationMs, startTime, this._easingFunction);
        ((DependencyObject) target2).Animate(1.0, compositeTransform.ScaleX, "ScaleX", this._animationDurationMs, startTime, this._easingFunction);
        ((DependencyObject) target2).Animate(1.0, compositeTransform.ScaleY, "ScaleY", this._animationDurationMs, startTime, this._easingFunction);
        CompositeTransform renderTransform = ((UIElement) imageFit).RenderTransform as CompositeTransform;
        ((DependencyObject) renderTransform).Animate(renderTransform.TranslateX, 0.0, "TranslateX", this._animationDurationMs, startTime, this._easingFunction, completionCallback);
        ((DependencyObject) renderTransform).Animate(renderTransform.TranslateY, 0.0, "TranslateY", this._animationDurationMs, startTime, this._easingFunction);
        ((DependencyObject) renderTransform).Animate(renderTransform.ScaleX, 1.0, "ScaleX", this._animationDurationMs, startTime, this._easingFunction);
        ((DependencyObject) renderTransform).Animate(renderTransform.ScaleY, 1.0, "ScaleY", this._animationDurationMs, startTime, this._easingFunction);
      }
    }

    public void AnimateOut(
      Size imageSize,
      Image imageOriginal,
      Image imageFit,
      bool? clockwiseRotation,
      Action completionCallback = null)
    {
      CompositeTransform renderTransform = ((UIElement) imageFit).RenderTransform as CompositeTransform;
      if (imageOriginal == null || renderTransform.ScaleX != 1.0)
      {
        this.AnimateFlyout(completionCallback, renderTransform);
      }
      else
      {
        Size size = new Size(((FrameworkElement) imageFit).ActualWidth, ((FrameworkElement) imageFit).ActualHeight);
        Rect fill = RectangleUtils.ResizeToFill(RectangleUtils.ResizeToFill(new Size(((FrameworkElement) imageOriginal).ActualWidth, ((FrameworkElement) imageOriginal).ActualHeight), imageSize), size);
        Rect rect = ((UIElement) imageOriginal).TransformToVisual((UIElement) imageFit).TransformBounds(fill);
        if (clockwiseRotation.HasValue)
          rect = RectangleUtils.Rotate90(rect);
        CompositeTransform compositeTransform1 = RectangleUtils.TransformRect(new Rect(new Point(), size), rect, true);
        renderTransform.put_CenterX(((FrameworkElement) imageFit).Width / 2.0);
        renderTransform.put_CenterY(((FrameworkElement) imageFit).Height / 2.0);
        double num = ((FrameworkElement) imageFit).Width / fill.Width;
        Rect target1 = new Rect(-fill.X * num, -fill.Y * num, ((FrameworkElement) imageOriginal).ActualWidth * num, ((FrameworkElement) imageOriginal).ActualHeight * num);
        if (target1.Width < 10.0 || target1.Height < 10.0)
        {
          this.AnimateFlyout(completionCallback, renderTransform);
        }
        else
        {
          RectangleGeometry rectangleGeometry = new RectangleGeometry();
          Rect source = new Rect(0.0, 0.0, ((FrameworkElement) imageFit).Width, ((FrameworkElement) imageFit).Height);
          rectangleGeometry.put_Rect(source);
          ((UIElement) imageFit).put_Clip(rectangleGeometry);
          CompositeTransform target2 = new CompositeTransform();
          ((Geometry) rectangleGeometry).put_Transform((Transform) target2);
          CompositeTransform compositeTransform2 = RectangleUtils.TransformRect(source, target1);
          ((DependencyObject) target2).Animate(0.0, compositeTransform2.TranslateY, "TranslateY", this._animationDurationMs, easing: this._easingFunction);
          ((DependencyObject) target2).Animate(0.0, compositeTransform2.TranslateX, "TranslateX", this._animationDurationMs, easing: this._easingFunction);
          ((DependencyObject) target2).Animate(1.0, compositeTransform2.ScaleX, "ScaleX", this._animationDurationMs, easing: this._easingFunction);
          ((DependencyObject) target2).Animate(1.0, compositeTransform2.ScaleY, "ScaleY", this._animationDurationMs, easing: this._easingFunction);
          if (clockwiseRotation.HasValue)
            ((DependencyObject) renderTransform).Animate(renderTransform.Rotation, clockwiseRotation.Value ? renderTransform.Rotation + 90.0 : renderTransform.Rotation - 90.0, "Rotation", this._animationDurationMs, easing: this._easingFunction);
          ((DependencyObject) renderTransform).Animate(renderTransform.TranslateX, renderTransform.TranslateX + compositeTransform1.TranslateX, "TranslateX", this._animationDurationMs, easing: this._easingFunction);
          ((DependencyObject) renderTransform).Animate(renderTransform.TranslateY, renderTransform.TranslateY + compositeTransform1.TranslateY, "TranslateY", this._animationDurationMs, easing: this._easingFunction);
          ((DependencyObject) renderTransform).Animate(renderTransform.ScaleX, compositeTransform1.ScaleX, "ScaleX", this._animationDurationMs, easing: this._easingFunction);
          ((DependencyObject) renderTransform).Animate(renderTransform.ScaleY, compositeTransform1.ScaleY, "ScaleY", this._animationDurationMs, easing: this._easingFunction, completed: completionCallback);
        }
      }
    }

    private void AnimateFlyout(Action completionCallback, CompositeTransform imageFitTransform)
    {
      CompositeTransform target = imageFitTransform;
      double translateY = imageFitTransform.TranslateY;
      double to = 1000.0;
      int animationDurationMs = this._animationDurationMs;
      ExponentialEase easing = new ExponentialEase();
      double num1 = 6.0;
      easing.put_Exponent(num1);
      int num2 = 1;
      ((EasingFunctionBase) easing).put_EasingMode((EasingMode) num2);
      Action completed = completionCallback;
      ((DependencyObject) target).Animate(translateY, to, "TranslateY", animationDurationMs, easing: (EasingFunctionBase) easing, completed: completed);
    }
  }
}
