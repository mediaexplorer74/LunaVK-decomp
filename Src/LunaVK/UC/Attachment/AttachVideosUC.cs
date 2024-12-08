// Decompiled with JetBrains decompiler
// Type: App1uwp.UC.Attachment.AttachVideosUC
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
using App1uwp.Network.DataObjects;
using App1uwp.Utils;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

#nullable disable
namespace App1uwp.UC.Attachment
{
  public class AttachVideosUC : Grid
  {
    private List<VKVideoBase> _list;

    public AttachVideosUC(List<VKVideoBase> list, double screen_width = 0.0)
    {
      this._list = new List<VKVideoBase>();
      this._list.AddRange((IEnumerable<VKVideoBase>) list);
      if (screen_width == 0.0)
        screen_width = Math.Min(((FrameworkElement) (Window.Current.Content as Frame)).ActualWidth, Constants.MAX_CONTENT_WIDTH);
      List<Size> childrenRects = new List<Size>();
      for (int index = 0; index < list.Count; ++index)
      {
        Size size = new Size((double) list[index].width, (double) list[index].height);
        childrenRects.Add(size);
      }
      List<Rect> layout = RectangleLayoutHelper.CreateLayout(screen_width, 550.0, childrenRects, 4.0);
      for (int index = 0; index < list.Count; ++index)
      {
        Rect rect = layout[index];
        Thickness thickness = new Thickness(rect.Left, rect.Top, 0.0, 0.0);
        AttachVideoUC attachVideoUc = new AttachVideoUC(list[index]);
        ((FrameworkElement) attachVideoUc).put_Width(rect.Width);
        ((FrameworkElement) attachVideoUc).put_Height(rect.Height);
        ((FrameworkElement) attachVideoUc).put_Margin(thickness);
        ((ICollection<UIElement>) ((Panel) this).Children).Add((UIElement) attachVideoUc);
      }
      Frame content = Window.Current.Content as Frame;
      WindowsRuntimeMarshal.AddEventHandler<SizeChangedEventHandler>(new Func<SizeChangedEventHandler, EventRegistrationToken>(((FrameworkElement) content).add_SizeChanged), new Action<EventRegistrationToken>(((FrameworkElement) content).remove_SizeChanged), new SizeChangedEventHandler(this.Window_SizeChanged));
    }

    private string GetProperSrc(VKVideoBase video, double width, double height)
    {
      double num = Math.Max(width, height);
      if (num <= 130.0 && !string.IsNullOrEmpty(video.photo_130))
        return video.photo_130;
      if (num <= 320.0 && !string.IsNullOrEmpty(video.photo_320))
        return video.photo_320;
      return !string.IsNullOrEmpty(video.photo_800) ? video.photo_800 : video.photo_640;
    }

    private void img_Tapped(object sender, TappedRoutedEventArgs e)
    {
    }

    private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
    {
      double num = 0.0;
      if ((Window.Current.Content as CustomFrame).MenuState == CustomFrame.MenuStates.StateWide)
        num = Constants.MENU_WIDE_WIDTH;
      else if ((Window.Current.Content as CustomFrame).MenuState == CustomFrame.MenuStates.StateNarrow)
        num = Constants.MENU_NARROW_WIDTH;
      this.Update(Math.Min(e.NewSize.Width - num, Constants.MAX_CONTENT_WIDTH));
    }

    public void Update(double screen_width)
    {
      List<Size> childrenRects = new List<Size>();
      for (int index = 0; index < this._list.Count; ++index)
      {
        Size size = new Size((double) this._list[index].width, (double) this._list[index].height);
        childrenRects.Add(size);
      }
      List<Rect> layout = RectangleLayoutHelper.CreateLayout(screen_width, 550.0, childrenRects, 4.0);
      for (int index = 0; index < this._list.Count; ++index)
      {
        Rect rect = layout[index];
        Thickness thickness = new Thickness(rect.Left, rect.Top, 0.0, 0.0);
        ((FrameworkElement) (((IList<UIElement>) ((Panel) this).Children)[index] as Image)).put_Margin(thickness);
      }
    }
  }
}
