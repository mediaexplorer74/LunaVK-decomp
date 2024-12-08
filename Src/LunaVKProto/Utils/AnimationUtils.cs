// Decompiled with JetBrains decompiler
// Type: App1uwp.Utils.AnimationUtils
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;

#nullable disable
namespace App1uwp.Utils
{
  public static class AnimationUtils
  {
    public static void Animate(
      double to,
      DependencyObject target,
      string propertyName,
      double durationSeconds)
    {
      Storyboard storyboard = new Storyboard();
      DoubleAnimation doubleAnimation = new DoubleAnimation();
      doubleAnimation.put_To(new double?(to));
      ((Timeline) doubleAnimation).put_AutoReverse(false);
      ((Timeline) doubleAnimation).put_Duration((Duration) TimeSpan.FromSeconds(durationSeconds));
      doubleAnimation.put_EasingFunction((EasingFunctionBase) new CubicEase());
      Storyboard.SetTargetProperty((Timeline) doubleAnimation, propertyName);
      Storyboard.SetTarget((Timeline) doubleAnimation, target);
      ((ICollection<Timeline>) storyboard.Children).Add((Timeline) doubleAnimation);
      storyboard.Begin();
    }

    public static Storyboard Animate(
      this DependencyObject target,
      double from,
      double to,
      string propertyPath,
      int duration,
      int startTime = 0,
      EasingFunctionBase easing = null,
      Action completed = null,
      bool autoReverse = false)
    {
      DoubleAnimation doubleAnimation = new DoubleAnimation();
      doubleAnimation.put_To(new double?(to));
      doubleAnimation.put_From(new double?(from));
      ((Timeline) doubleAnimation).put_AutoReverse(autoReverse);
      doubleAnimation.put_EasingFunction(easing);
      ((Timeline) doubleAnimation).put_Duration((Duration) TimeSpan.FromMilliseconds((double) duration));
      Storyboard.SetTarget((Timeline) doubleAnimation, target);
      Storyboard.SetTargetProperty((Timeline) doubleAnimation, propertyPath);
      Storyboard storyboard = new Storyboard();
      ((Timeline) storyboard).put_BeginTime(new TimeSpan?(TimeSpan.FromMilliseconds((double) startTime)));
      if (completed != null)
        WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(((Timeline) storyboard).add_Completed), new Action<EventRegistrationToken>(((Timeline) storyboard).remove_Completed), (EventHandler<object>) ((s, e) => completed()));
      ((ICollection<Timeline>) storyboard.Children).Add((Timeline) doubleAnimation);
      storyboard.Begin();
      return storyboard;
    }

    public static Storyboard AnimateSeveral(
      List<AnimationUtils.AnimationInfo> animInfoList,
      int? startTime = null,
      Action completed = null)
    {
      List<DoubleAnimation> doubleAnimationList = new List<DoubleAnimation>();
      foreach (AnimationUtils.AnimationInfo animInfo in animInfoList)
      {
        DoubleAnimation doubleAnimation = new DoubleAnimation();
        doubleAnimation.put_To(new double?(animInfo.to));
        doubleAnimation.put_From(new double?(animInfo.from));
        doubleAnimation.put_EasingFunction(animInfo.easing);
        ((Timeline) doubleAnimation).put_Duration((Duration) TimeSpan.FromMilliseconds((double) animInfo.duration));
        Storyboard.SetTarget((Timeline) doubleAnimation, animInfo.target);
        Storyboard.SetTargetProperty((Timeline) doubleAnimation, animInfo.propertyPath);
        doubleAnimationList.Add(doubleAnimation);
      }
      Storyboard storyboard = new Storyboard();
      if (startTime.HasValue)
        ((Timeline) storyboard).put_BeginTime(new TimeSpan?(TimeSpan.FromMilliseconds((double) startTime.Value)));
      else
        ((Timeline) storyboard).put_BeginTime(new TimeSpan?());
      if (completed != null)
        WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(((Timeline) storyboard).add_Completed), new Action<EventRegistrationToken>(((Timeline) storyboard).remove_Completed), (EventHandler<object>) ((s, e) => completed()));
      foreach (Timeline timeline in doubleAnimationList)
        ((ICollection<Timeline>) storyboard.Children).Add(timeline);
      storyboard.Begin();
      return storyboard;
    }

    public class AnimationInfo
    {
      public DependencyObject target { get; set; }

      public double from { get; set; }

      public double to { get; set; }

      public string propertyPath { get; set; }

      public int duration { get; set; }

      public EasingFunctionBase easing { get; set; }
    }
  }
}
