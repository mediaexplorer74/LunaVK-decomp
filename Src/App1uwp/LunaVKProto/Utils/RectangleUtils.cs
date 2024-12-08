// Decompiled with JetBrains decompiler
// Type: App1uwp.Utils.RectangleUtils
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using System;
using Windows.Foundation;
using Windows.UI.Xaml.Media;

#nullable disable
namespace App1uwp.Utils
{
  public static class RectangleUtils
  {
    public static Rect ResizeToFit(Rect parentRect, Size childSize)
    {
      Rect fit = RectangleUtils.ResizeToFit(RectangleUtils.GetSize(parentRect), childSize);
      double num1 = fit.X + parentRect.X;
      fit.X = num1;
      double num2 = fit.Y + parentRect.Y;
      fit.Y = num2;
      return fit;
    }

    public static Rect ResizeToFit(Size parentSize, Size childSize)
    {
      if (parentSize.Height == 0.0 || parentSize.Width == 0.0 || childSize.Width == 0.0 || childSize.Height == 0.0)
        return new Rect();
      double num1 = parentSize.Width / parentSize.Height;
      double num2 = childSize.Width / childSize.Height;
      Rect fit = new Rect();
      double num3 = num2;
      if (num1 < num3)
      {
        double num4 = parentSize.Width / childSize.Width;
        fit.Width = parentSize.Width;
        fit.Height = childSize.Height * num4;
        fit.Y = (parentSize.Height - fit.Height) / 2.0;
      }
      else
      {
        double num5 = parentSize.Height / childSize.Height;
        fit.Height = parentSize.Height;
        fit.Width = childSize.Width * num5;
        fit.X = (parentSize.Width - fit.Width) / 2.0;
      }
      return fit;
    }

    public static Size GetSize(Rect rect) => new Size(rect.Width, rect.Height);

    public static Rect AlignRects(Rect parentRect, Rect childRect, bool fill)
    {
      bool flag1 = Math.Abs(childRect.Bottom - parentRect.Bottom) < Math.Abs(childRect.Top - parentRect.Top);
      bool flag2 = Math.Abs(childRect.Left - parentRect.Left) < Math.Abs(childRect.Right - parentRect.Right);
      double num1 = Math.Max(parentRect.Width / childRect.Width, parentRect.Height / childRect.Height);
      if (num1 > 1.0 & fill)
      {
        double num2 = childRect.Width * num1;
        childRect.Width = num2;
        double num3 = childRect.Height * num1;
        childRect.Height = num3;
      }
      double num4 = 0.0;
      double num5 = 0.0;
      if (childRect.Top > parentRect.Top ^ childRect.Bottom < parentRect.Bottom)
        num5 = !flag1 ? parentRect.Top - childRect.Top : parentRect.Bottom - childRect.Bottom;
      if (childRect.Left > parentRect.Left ^ childRect.Right < parentRect.Right)
        num4 = !flag2 ? parentRect.Right - childRect.Right : parentRect.Left - childRect.Left;
      double num6 = childRect.X + num4;
      childRect.X = num6;
      double num7 = childRect.Y + num5;
      childRect.Y = num7;
      if (!fill)
      {
        if (childRect.Height < parentRect.Height && (childRect.Top > parentRect.Top || childRect.Bottom < parentRect.Bottom))
          childRect.Y = parentRect.Y + (parentRect.Height - childRect.Height) / 2.0;
        if (childRect.Width < parentRect.Width && (childRect.Left > parentRect.Left || childRect.Right < parentRect.Right))
          childRect.X = parentRect.Y + (parentRect.Width - childRect.Width) / 2.0;
      }
      return childRect;
    }

    public static CompositeTransform TransformRect(
      Rect source,
      Rect target,
      bool inSourceCenterCoord = false)
    {
      if (source.Width == 0.0 || source.Height == 0.0 || target.Width == 0.0 || target.Height == 0.0)
        return new CompositeTransform();
      if (inSourceCenterCoord)
        return RectangleUtils.TransformRect(new Rect(source.X - source.Width / 2.0, source.Y - source.Height / 2.0, source.Width, source.Height), new Rect(target.X - source.Width / 2.0, target.Y - source.Height / 2.0, target.Width, target.Height));
      CompositeTransform compositeTransform = new CompositeTransform();
      compositeTransform.put_ScaleX(target.Width / source.Width);
      compositeTransform.put_ScaleY(target.Height / source.Height);
      compositeTransform.put_TranslateX(target.X - source.X * compositeTransform.ScaleX);
      compositeTransform.put_TranslateY(target.Y - source.Y * compositeTransform.ScaleY);
      return compositeTransform;
    }

    public static Rect ResizeToFill(Rect parentRect, Size childSize)
    {
      Rect fill = RectangleUtils.ResizeToFill(RectangleUtils.GetSize(parentRect), childSize);
      double num1 = fill.X + parentRect.X;
      fill.X = num1;
      double num2 = fill.Y + parentRect.Y;
      fill.Y = num2;
      return fill;
    }

    public static Rect ResizeToFill(Size parentSize, Size childSize)
    {
      if (parentSize.Height == 0.0 || parentSize.Width == 0.0 || childSize.Width == 0.0 || childSize.Height == 0.0)
        return new Rect();
      double num1 = parentSize.Width / parentSize.Height;
      double num2 = childSize.Width / childSize.Height;
      Rect fill = new Rect();
      double num3 = num2;
      if (num1 < num3)
      {
        double num4 = parentSize.Height / childSize.Height;
        fill.Height = parentSize.Height;
        fill.Width = childSize.Width * num4;
        fill.X = (parentSize.Width - fill.Width) / 2.0;
      }
      else
      {
        double num5 = parentSize.Width / childSize.Width;
        fill.Width = parentSize.Width;
        fill.Height = childSize.Height * num5;
        fill.Y = (parentSize.Height - fill.Height) / 2.0;
      }
      return fill;
    }

    public static Rect Rotate90(Rect source)
    {
      return new Rect(source.X + (source.Width - source.Height) / 2.0, source.Y + (source.Height - source.Width) / 2.0, source.Height, source.Width);
    }
  }
}
