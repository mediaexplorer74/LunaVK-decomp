// Decompiled with JetBrains decompiler
// Type: App1uwp.HideHeaderHelper
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

#nullable disable
namespace App1uwp
{
  public class HideHeaderHelper
  {
    private readonly FrameworkElement _ucHeader;
    private readonly FrameworkElement _sub;
    private readonly ScrollViewer _viewportControl;
    private readonly TranslateTransform _translateHeader;
    private readonly TranslateTransform _translateSub;
    private double _previousScrollPosition;
    private double temp = 300.0;
    private double temp2 = 1.0;

    public HideHeaderHelper(ScrollViewer viewportControl)
    {
      this._viewportControl = viewportControl;
      ScrollViewer viewportControl1 = this._viewportControl;
      WindowsRuntimeMarshal.AddEventHandler<EventHandler<ScrollViewerViewChangedEventArgs>>(new Func<EventHandler<ScrollViewerViewChangedEventArgs>, EventRegistrationToken>(viewportControl1.add_ViewChanged), new Action<EventRegistrationToken>(viewportControl1.remove_ViewChanged), new EventHandler<ScrollViewerViewChangedEventArgs>(this._viewportControl_ViewChanged2));
      ((UIElement) this.Head).put_Opacity(0.0);
    }

    private Grid Head => (Window.Current.Content as CustomFrame).HeaderWithMenu.BackBackground;

    private void _viewportControl_ViewChanged2(object sender, ScrollViewerViewChangedEventArgs e)
    {
      double verticalOffset = this._viewportControl.VerticalOffset;
      double num1 = verticalOffset - this._previousScrollPosition;
      if (verticalOffset > this.temp)
      {
        double num2 = num1 / 100.0 * this.temp2;
        if (((UIElement) this.Head).Opacity + num2 > 1.0)
          ((UIElement) this.Head).put_Opacity(1.0);
        else if (((UIElement) this.Head).Opacity + num2 <= 0.0)
        {
          ((UIElement) this.Head).put_Opacity(0.0);
        }
        else
        {
          Grid head = this.Head;
          ((UIElement) head).put_Opacity(((UIElement) head).Opacity + num2);
        }
      }
      else if (num1 < 0.0)
      {
        double num3 = num1 / 100.0 * this.temp2;
        Grid head = this.Head;
        ((UIElement) head).put_Opacity(((UIElement) head).Opacity + num3);
      }
      this._previousScrollPosition = verticalOffset;
    }

    private void _viewportControl_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
    {
      double num1 = -this._viewportControl.VerticalOffset;
      double num2 = this._previousScrollPosition - num1;
      if (num2 > 0.0)
      {
        if (-(this._translateHeader.Y - num2) >= this._ucHeader.ActualHeight)
        {
          this._translateHeader.put_Y(-this._ucHeader.ActualHeight);
        }
        else
        {
          TranslateTransform translateHeader = this._translateHeader;
          translateHeader.put_Y(translateHeader.Y - num2);
        }
      }
      else if (this._translateHeader.Y - num2 >= 0.0)
      {
        this._translateHeader.put_Y(0.0);
      }
      else
      {
        TranslateTransform translateHeader = this._translateHeader;
        translateHeader.put_Y(translateHeader.Y - num2);
      }
      this._previousScrollPosition = num1;
      if (this._translateSub == null)
        return;
      this._translateSub.put_Y(this._translateHeader.Y);
    }

    public void Update()
    {
      this._previousScrollPosition = 0.0;
      this._viewportControl_ViewChanged((object) null, (ScrollViewerViewChangedEventArgs) null);
    }
  }
}
