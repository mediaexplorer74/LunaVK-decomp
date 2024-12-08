// Decompiled with JetBrains decompiler
// Type: App1uwp.UC.Attachment.AttachPhotosUC
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
using App1uwp.Library;
using App1uwp.Network.DataObjects;
using App1uwp.Utils;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

#nullable disable
namespace App1uwp.UC.Attachment
{
  public class AttachPhotosUC : Grid
  {
    private List<VKPhoto> _list;

    private Image GetImageFunc(int index)
    {
      return (((IList<UIElement>) ((Panel) this).Children)[index] as Border).Child as Image;
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
        Border child1 = ((IList<UIElement>) ((Panel) this).Children)[index] as Border;
        ((FrameworkElement) child1).put_Width(rect.Width);
        ((FrameworkElement) child1).put_Height(rect.Height);
        ((FrameworkElement) child1).put_Margin(thickness);
        Image child2 = child1.Child as Image;
        ((FrameworkElement) child2).put_Width(rect.Width);
        ((FrameworkElement) child2).put_Height(rect.Height);
      }
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

    public AttachPhotosUC(List<VKPhoto> list, double screen_width)
    {
      this._list = new List<VKPhoto>();
      this._list.AddRange((IEnumerable<VKPhoto>) list);
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
        Image image = new Image();
        ((FrameworkElement) image).put_Tag((object) index);
        WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(((UIElement) image).add_Tapped), new Action<EventRegistrationToken>(((UIElement) image).remove_Tapped), new TappedEventHandler(this.img_Tapped));
        image.put_Source((ImageSource) new BitmapImage(new Uri(this.GetProperSrc(list[index], rect.Width, rect.Height))));
        ((FrameworkElement) image).put_VerticalAlignment((VerticalAlignment) 0);
        ((FrameworkElement) image).put_Width(rect.Width);
        ((FrameworkElement) image).put_Height(rect.Height);
        image.put_Stretch((Stretch) 3);
        Border border = new Border();
        ((FrameworkElement) border).put_HorizontalAlignment((HorizontalAlignment) 0);
        ((FrameworkElement) border).put_VerticalAlignment((VerticalAlignment) 0);
        ((FrameworkElement) border).put_Width(rect.Width);
        ((FrameworkElement) border).put_Height(rect.Height);
        ((FrameworkElement) border).put_Margin(thickness);
        border.put_Child((UIElement) image);
        ((ICollection<UIElement>) ((Panel) this).Children).Add((UIElement) border);
      }
      Frame content = Window.Current.Content as Frame;
      WindowsRuntimeMarshal.AddEventHandler<SizeChangedEventHandler>(new Func<SizeChangedEventHandler, EventRegistrationToken>(((FrameworkElement) content).add_SizeChanged), new Action<EventRegistrationToken>(((FrameworkElement) content).remove_SizeChanged), new SizeChangedEventHandler(this.Window_SizeChanged));
    }

    private string GetProperSrc(VKPhoto photo, double width, double height)
    {
      double num = Math.Max(width, height);
      if (num <= 75.0 && !string.IsNullOrEmpty(photo.photo_75))
        return photo.photo_75;
      return num <= 130.0 && !string.IsNullOrEmpty(photo.photo_130) ? photo.photo_130 : photo.photo_604;
    }

    private void img_Tapped(object sender, TappedRoutedEventArgs e)
    {
      int tag = (int) ((FrameworkElement) (sender as Image)).Tag;
      ImageViewerDecoratorUC viewerDecoratorUc = new ImageViewerDecoratorUC();
      viewerDecoratorUc.Initialize(this._list, (Func<int, Image>) (i => this.GetImageFunc(i)));
      viewerDecoratorUc.Show(tag);
    }

    private async void Asy(
      List<OutboundPhotoAttachment> list,
      double screen_width,
      bool aligment_to_right)
    {
      if (screen_width == 0.0)
        screen_width = ((FrameworkElement) (Window.Current.Content as Frame)).ActualWidth;
      List<Size> childrenRects = new List<Size>();
      for (int index = 0; index < list.Count; ++index)
      {
        StorageFile sf = list[index].sf;
        Size size = new Size(list[index].ImgWidth, list[index].ImgHeight);
        childrenRects.Add(size);
      }
      List<Rect> layout = RectangleLayoutHelper.CreateLayout(screen_width, 500.0, childrenRects, 4.0);
      for (int index = 0; index < list.Count; ++index)
      {
        Rect rect = layout[index];
        Image image = new Image();
        Thickness thickness = new Thickness(aligment_to_right ? 0.0 : rect.Left, rect.Top, aligment_to_right ? rect.Left : 0.0, 0.0);
        ((FrameworkElement) image).put_Tag((object) index);
        image.put_Source((ImageSource) list[index].LocalUrl2);
        ((FrameworkElement) image).put_HorizontalAlignment(aligment_to_right ? (HorizontalAlignment) 2 : (HorizontalAlignment) 0);
        ((FrameworkElement) image).put_VerticalAlignment((VerticalAlignment) 0);
        ((FrameworkElement) image).put_Width(rect.Width);
        ((FrameworkElement) image).put_Height(rect.Height);
        ((FrameworkElement) image).put_Margin(thickness);
        image.put_Stretch((Stretch) 1);
        ((ICollection<UIElement>) ((Panel) this).Children).Add((UIElement) image);
      }
    }

    public AttachPhotosUC(
      List<OutboundPhotoAttachment> list,
      double screen_width,
      bool aligment_to_right)
    {
      ((ICollection<UIElement>) ((Panel) this).Children).Clear();
      this.Asy(list, screen_width, aligment_to_right);
    }
  }
}
