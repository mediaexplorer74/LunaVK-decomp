// Decompiled with JetBrains decompiler
// Type: App1uwp.Framework.PopUpService
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

#nullable disable
namespace App1uwp.Framework
{
  public class PopUpService
  {
    private PopUpService.AnimationTypes _animationTypeOverlay = PopUpService.AnimationTypes.Fade;
    public PopUpService.AnimationTypes AnimationTypeChild;
    private Grid PopupContainer;
    private Grid BackGroundGrid;
    public Brush BackgroundBrush = (Brush) new SolidColorBrush(Color.FromArgb((byte) 102, (byte) 0, (byte) 0, (byte) 0));

    public FrameworkElement Child { get; set; }

    public double VerticalOffset { get; set; }

    public double HorizontalOffset { get; set; }

    public bool IsOpen { get; private set; }

    public event EventHandler Closed;

    public event EventHandler Opened;

    public void Show()
    {
      this.IsOpen = true;
      this.InitializePopup();
    }

    private CustomFrame Frame => Window.Current.Content as CustomFrame;

    private Grid OverlayGrid => this.Frame.OverlayGrid;

    private void InitializePopup()
    {
      this.PopupContainer = new Grid();
      this.BackGroundGrid = new Grid();
      if (this.BackgroundBrush != null)
        ((Panel) this.BackGroundGrid).put_Background(this.BackgroundBrush);
      Grid backGroundGrid = this.BackGroundGrid;
      WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(((UIElement) backGroundGrid).add_Tapped), new Action<EventRegistrationToken>(((UIElement) backGroundGrid).remove_Tapped), new TappedEventHandler(this.OverlayGrid_Tapped));
      this.Child.put_Margin(new Thickness(this.HorizontalOffset, this.VerticalOffset, 0.0, 0.0));
      ((ICollection<UIElement>) ((Panel) this.PopupContainer).Children).Add((UIElement) this.Child);
      ((UIElement) this.PopupContainer).put_Opacity(0.0);
      ((UIElement) this.BackGroundGrid).put_Opacity(0.0);
      ((ICollection<UIElement>) ((Panel) this.OverlayGrid).Children).Add((UIElement) this.BackGroundGrid);
      ((ICollection<UIElement>) ((Panel) this.OverlayGrid).Children).Add((UIElement) this.PopupContainer);
      this.RunShowStoryboard((FrameworkElement) this.PopupContainer, this.AnimationTypeChild);
      this.RunShowStoryboard((FrameworkElement) this.BackGroundGrid, this._animationTypeOverlay, (Action) (() =>
      {
        if (this.Opened == null)
          return;
        this.Opened((object) this, (EventArgs) null);
      }));
    }

    private void OverlayGrid_Tapped(object sender, TappedRoutedEventArgs e)
    {
      if (sender != ((RoutedEventArgs) e).OriginalSource)
        return;
      this.Hide();
    }

    public void Hide()
    {
      this.RunHideStoryboard((FrameworkElement) this.PopupContainer, this.AnimationTypeChild);
      this.RunHideStoryboard((FrameworkElement) this.BackGroundGrid, this._animationTypeOverlay, new Action(this.HideStoryboardCompleted));
    }

    private void RunShowStoryboard(
      FrameworkElement element,
      PopUpService.AnimationTypes animation,
      Action completionCallback = null)
    {
      if (element == null)
      {
        if (completionCallback == null)
          return;
        completionCallback();
      }
      else
      {
        Storyboard storyboard = (Storyboard) null;
        switch (animation)
        {
          case PopUpService.AnimationTypes.Slide:
            storyboard = XamlReader.Load("\r\n        <Storyboard  xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">\r\n            <DoubleAnimationUsingKeyFrames Storyboard.TargetProperty=\"(UIElement.RenderTransform).(TranslateTransform.Y)\">\r\n                <EasingDoubleKeyFrame KeyTime=\"0\" Value=\"-150\"/>\r\n                <EasingDoubleKeyFrame KeyTime=\"0:0:0.35\" Value=\"0\">\r\n                    <EasingDoubleKeyFrame.EasingFunction>\r\n                        <ExponentialEase EasingMode=\"EaseOut\" Exponent=\"6\"/>\r\n                    </EasingDoubleKeyFrame.EasingFunction>\r\n                </EasingDoubleKeyFrame>\r\n            </DoubleAnimationUsingKeyFrames>\r\n            <DoubleAnimation Storyboard.TargetProperty=\"(UIElement.Opacity)\" From=\"0\" To=\"1\" Duration=\"0:0:0.350\">\r\n                <DoubleAnimation.EasingFunction>\r\n                    <ExponentialEase EasingMode=\"EaseOut\" Exponent=\"6\"/>\r\n                </DoubleAnimation.EasingFunction>\r\n            </DoubleAnimation>\r\n        </Storyboard>") as Storyboard;
            ((UIElement) element).put_RenderTransform((Transform) new TranslateTransform());
            break;
          case PopUpService.AnimationTypes.SlideInversed:
            storyboard = XamlReader.Load("\r\n        <Storyboard  xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">\r\n            <DoubleAnimationUsingKeyFrames Storyboard.TargetProperty=\"(UIElement.RenderTransform).(TranslateTransform.Y)\">\r\n                <SplineDoubleKeyFrame KeyTime=\"0\" Value=\"800\"/>\r\n                <SplineDoubleKeyFrame KeyTime=\"0:0:0.35\" Value=\"0\">\r\n                    <SplineDoubleKeyFrame.KeySpline>\r\n                        <KeySpline>\r\n                            <KeySpline.ControlPoint1>\r\n                                <Point X=\"0.10000000149011612\" Y=\"0.89999997615811421\" />\r\n                            </KeySpline.ControlPoint1>\r\n                            <KeySpline.ControlPoint2>\r\n                                <Point X=\"0.20000000298023224\" Y=\"1\" />\r\n                            </KeySpline.ControlPoint2>\r\n                        </KeySpline>\r\n                    </SplineDoubleKeyFrame.KeySpline>\r\n                </SplineDoubleKeyFrame>\r\n            </DoubleAnimationUsingKeyFrames>\r\n            <DoubleAnimation Storyboard.TargetProperty=\"(UIElement.Opacity)\" From=\"0\" To=\"1\" Duration=\"0\" />\r\n        </Storyboard>") as Storyboard;
            ((UIElement) element).put_RenderTransform((Transform) new TranslateTransform());
            break;
          case PopUpService.AnimationTypes.SlideHorizontal:
            storyboard = XamlReader.Load("\r\n        <Storyboard  xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">\r\n            <DoubleAnimationUsingKeyFrames Storyboard.TargetProperty=\"(UIElement.RenderTransform).(TranslateTransform.X)\" >\r\n                    <EasingDoubleKeyFrame KeyTime=\"0\" Value=\"-150\"/>\r\n                    <EasingDoubleKeyFrame KeyTime=\"0:0:0.35\" Value=\"0\">\r\n                        <EasingDoubleKeyFrame.EasingFunction>\r\n                            <ExponentialEase EasingMode=\"EaseOut\" Exponent=\"6\"/>\r\n                        </EasingDoubleKeyFrame.EasingFunction>\r\n                    </EasingDoubleKeyFrame>\r\n                </DoubleAnimationUsingKeyFrames>\r\n            <DoubleAnimation Storyboard.TargetProperty=\"(UIElement.Opacity)\" From=\"0\" To=\"1\" Duration=\"0:0:0.350\" >\r\n                <DoubleAnimation.EasingFunction>\r\n                    <ExponentialEase EasingMode=\"EaseOut\" Exponent=\"6\"/>\r\n                </DoubleAnimation.EasingFunction>\r\n            </DoubleAnimation>\r\n        </Storyboard>") as Storyboard;
            ((UIElement) element).put_RenderTransform((Transform) new TranslateTransform());
            break;
          case PopUpService.AnimationTypes.Swivel:
          case PopUpService.AnimationTypes.SwivelHorizontal:
            storyboard = XamlReader.Load("<Storyboard xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">\r\n            <DoubleAnimationUsingKeyFrames Storyboard.TargetProperty=\"(UIElement.Projection).(PlaneProjection.RotationX)\">\r\n                <EasingDoubleKeyFrame KeyTime=\"0:0:0.0\" Value=\"-45\"/>\r\n                <EasingDoubleKeyFrame KeyTime=\"0:0:0.35\" Value=\"0\">\r\n                    <EasingDoubleKeyFrame.EasingFunction>\r\n                        <ExponentialEase EasingMode=\"EaseOut\" Exponent=\"6\"/>\r\n                    </EasingDoubleKeyFrame.EasingFunction>\r\n                </EasingDoubleKeyFrame>\r\n            </DoubleAnimationUsingKeyFrames>\r\n            <DoubleAnimationUsingKeyFrames Storyboard.TargetProperty=\"(UIElement.Opacity)\">\r\n                <DiscreteDoubleKeyFrame KeyTime=\"0\" Value=\"1\" />\r\n            </DoubleAnimationUsingKeyFrames>\r\n        </Storyboard>") as Storyboard;
            FrameworkElement frameworkElement = element;
            PlaneProjection planeProjection = new PlaneProjection();
            double num1 = -45.0;
            planeProjection.put_RotationX(num1);
            double num2 = element.ActualHeight / 2.0;
            planeProjection.put_CenterOfRotationX(num2);
            ((UIElement) frameworkElement).put_Projection((Projection) planeProjection);
            break;
          case PopUpService.AnimationTypes.Fade:
            storyboard = XamlReader.Load("<Storyboard xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">\r\n            <DoubleAnimation \r\n\t\t\t\tDuration=\"0:0:0.267\" \r\n\t\t\t\tStoryboard.TargetProperty=\"(UIElement.Opacity)\" \r\n                To=\"1\"/>\r\n        </Storyboard>") as Storyboard;
            break;
        }
        if (storyboard != null)
        {
          ((UIElement) element).put_Opacity(0.0);
          WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(((Timeline) storyboard).add_Completed), new Action<EventRegistrationToken>(((Timeline) storyboard).remove_Completed), (EventHandler<object>) ((s, e) =>
          {
            if (completionCallback == null)
              return;
            completionCallback();
          }));
          foreach (Timeline child in (IEnumerable<Timeline>) storyboard.Children)
            Storyboard.SetTarget(child, (DependencyObject) element);
          storyboard.Begin();
        }
        else
        {
          ((UIElement) element).put_Opacity(1.0);
          if (completionCallback == null)
            return;
          completionCallback();
        }
      }
    }

    private void RunHideStoryboard(
      FrameworkElement element,
      PopUpService.AnimationTypes animation,
      Action completionCallback = null)
    {
      if (element == null)
        return;
      Storyboard storyboard = (Storyboard) null;
      switch (animation)
      {
        case PopUpService.AnimationTypes.Slide:
          storyboard = XamlReader.Load("\r\n        <Storyboard  xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">\r\n            <DoubleAnimationUsingKeyFrames Storyboard.TargetProperty=\"(UIElement.RenderTransform).(TranslateTransform.Y)\">\r\n                <EasingDoubleKeyFrame KeyTime=\"0\" Value=\"0\"/>\r\n                <EasingDoubleKeyFrame KeyTime=\"0:0:0.25\" Value=\"-150\">\r\n                    <EasingDoubleKeyFrame.EasingFunction>\r\n                        <ExponentialEase EasingMode=\"EaseIn\" Exponent=\"6\"/>\r\n                    </EasingDoubleKeyFrame.EasingFunction>\r\n                </EasingDoubleKeyFrame>\r\n            </DoubleAnimationUsingKeyFrames>\r\n            <DoubleAnimation Storyboard.TargetProperty=\"(UIElement.Opacity)\" From=\"1\" To=\"0\" Duration=\"0:0:0.25\">\r\n                <DoubleAnimation.EasingFunction>\r\n                    <ExponentialEase EasingMode=\"EaseIn\" Exponent=\"6\"/>\r\n                </DoubleAnimation.EasingFunction>\r\n            </DoubleAnimation>\r\n        </Storyboard>") as Storyboard;
          break;
        case PopUpService.AnimationTypes.SlideInversed:
          storyboard = XamlReader.Load("\r\n        <Storyboard  xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">\r\n            <DoubleAnimationUsingKeyFrames Storyboard.TargetProperty=\"(UIElement.RenderTransform).(TranslateTransform.Y)\">\r\n                <EasingDoubleKeyFrame KeyTime=\"0\" Value=\"0\"/>\r\n                <EasingDoubleKeyFrame KeyTime=\"0:0:0.35\" Value=\"800\">\r\n                    <EasingDoubleKeyFrame.EasingFunction>\r\n                        <ExponentialEase EasingMode=\"EaseIn\" Exponent=\"6\"/>\r\n                    </EasingDoubleKeyFrame.EasingFunction>\r\n                </EasingDoubleKeyFrame>\r\n            </DoubleAnimationUsingKeyFrames>\r\n        </Storyboard>") as Storyboard;
          break;
        case PopUpService.AnimationTypes.SlideHorizontal:
          storyboard = XamlReader.Load("\r\n        <Storyboard  xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">\r\n            <DoubleAnimationUsingKeyFrames Storyboard.TargetProperty=\"(UIElement.RenderTransform).(TranslateTransform.X)\">\r\n                <EasingDoubleKeyFrame KeyTime=\"0\" Value=\"0\"/>\r\n                <EasingDoubleKeyFrame KeyTime=\"0:0:0.25\" Value=\"150\">\r\n                    <EasingDoubleKeyFrame.EasingFunction>\r\n                        <ExponentialEase EasingMode=\"EaseIn\" Exponent=\"6\"/>\r\n                    </EasingDoubleKeyFrame.EasingFunction>\r\n                </EasingDoubleKeyFrame>\r\n            </DoubleAnimationUsingKeyFrames>\r\n            <DoubleAnimation Storyboard.TargetProperty=\"(UIElement.Opacity)\" From=\"1\" To=\"0\" Duration=\"0:0:0.25\">\r\n                <DoubleAnimation.EasingFunction>\r\n                    <ExponentialEase EasingMode=\"EaseIn\" Exponent=\"6\"/>\r\n                </DoubleAnimation.EasingFunction>\r\n            </DoubleAnimation>\r\n        </Storyboard>") as Storyboard;
          break;
        case PopUpService.AnimationTypes.Swivel:
        case PopUpService.AnimationTypes.SwivelHorizontal:
          storyboard = XamlReader.Load("<Storyboard xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">\r\n            <DoubleAnimationUsingKeyFrames Storyboard.TargetProperty=\"(UIElement.Projection).(PlaneProjection.RotationX)\">\r\n                <EasingDoubleKeyFrame KeyTime=\"0\" Value=\"0\"/>\r\n                <EasingDoubleKeyFrame KeyTime=\"0:0:0.25\" Value=\"45\">\r\n                    <EasingDoubleKeyFrame.EasingFunction>\r\n                        <ExponentialEase EasingMode=\"EaseIn\" Exponent=\"6\"/>\r\n                    </EasingDoubleKeyFrame.EasingFunction>\r\n                </EasingDoubleKeyFrame>\r\n            </DoubleAnimationUsingKeyFrames>\r\n            <DoubleAnimationUsingKeyFrames Storyboard.TargetProperty=\"(UIElement.Opacity)\">\r\n                <DiscreteDoubleKeyFrame KeyTime=\"0\" Value=\"1\" />\r\n                <DiscreteDoubleKeyFrame KeyTime=\"0:0:0.267\" Value=\"0\" />\r\n            </DoubleAnimationUsingKeyFrames>\r\n        </Storyboard>") as Storyboard;
          FrameworkElement frameworkElement = element;
          PlaneProjection planeProjection = new PlaneProjection();
          planeProjection.put_RotationX(0.0);
          planeProjection.put_CenterOfRotationX(element.ActualHeight / 2.0);
          ((UIElement) frameworkElement).put_Projection((Projection) planeProjection);
          break;
        case PopUpService.AnimationTypes.Fade:
          storyboard = XamlReader.Load("<Storyboard xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">\r\n            <DoubleAnimation \r\n\t\t\t\tDuration=\"0:0:0.267\"\r\n\t\t\t\tStoryboard.TargetProperty=\"(UIElement.Opacity)\" \r\n                To=\"0\"/>\r\n        </Storyboard>") as Storyboard;
          break;
      }
      try
      {
        if (storyboard != null)
        {
          WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(((Timeline) storyboard).add_Completed), new Action<EventRegistrationToken>(((Timeline) storyboard).remove_Completed), (EventHandler<object>) ((s, e) =>
          {
            if (completionCallback == null)
              return;
            completionCallback();
          }));
          foreach (Timeline child in (IEnumerable<Timeline>) storyboard.Children)
            Storyboard.SetTarget(child, (DependencyObject) element);
          storyboard.Begin();
        }
        else
        {
          if (completionCallback == null)
            return;
          completionCallback();
        }
      }
      catch
      {
        if (completionCallback == null)
          return;
        completionCallback();
      }
    }

    private void HideStoryboardCompleted()
    {
      this.IsOpen = false;
      ((ICollection<UIElement>) ((Panel) this.PopupContainer).Children).Remove((UIElement) this.Child);
      ((ICollection<UIElement>) ((Panel) this.OverlayGrid).Children).Remove((UIElement) this.BackGroundGrid);
      ((ICollection<UIElement>) ((Panel) this.OverlayGrid).Children).Remove((UIElement) this.PopupContainer);
      WindowsRuntimeMarshal.RemoveEventHandler<TappedEventHandler>(new Action<EventRegistrationToken>(((UIElement) this.OverlayGrid).remove_Tapped), new TappedEventHandler(this.OverlayGrid_Tapped));
      if (this.Closed == null)
        return;
      this.Closed((object) this, (EventArgs) null);
    }

    public enum AnimationTypes
    {
      Slide,
      SlideInversed,
      SlideHorizontal,
      Swivel,
      SwivelHorizontal,
      Fade,
      None,
    }
  }
}
