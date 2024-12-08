// Decompiled with JetBrains decompiler
// Type: App1uwp.VirtualUC.ItemMessageUC
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Library;
using App1uwp.Network.Converters;
using App1uwp.Network.DataObjects;
using App1uwp.Network.DataVM;
using App1uwp.Network.Enums;
using App1uwp.Network.ViewModels;
using App1uwp.UC;
using App1uwp.Utils;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

#nullable disable
namespace App1uwp.VirtualUC
{
  public class ItemMessageUC : Grid
  {
    public static readonly DependencyProperty DataProperty = DependencyProperty.Register(nameof (Data), typeof (object), typeof (ItemMessageUC), new PropertyMetadata((object) null, new PropertyChangedCallback(ItemMessageUC.OnDataChanged)));

    public object Data
    {
      get => ((DependencyObject) this).GetValue(ItemMessageUC.DataProperty);
      set => ((DependencyObject) this).SetValue(ItemMessageUC.DataProperty, value);
    }

    private static void OnDataChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
      ((ItemMessageUC) obj).ProcessData();
    }

    private void ProcessData()
    {
      ((ICollection<UIElement>) ((Panel) this).Children).Clear();
      if (this.Data == null)
        return;
      VKMessageVM data = this.Data as VKMessageVM;
      ((Panel) this).put_Background((Brush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "TextColorContent"]);
      if (data.action != VKChatMessageActionType.None)
      {
        VKProfileBase cachedUser1 = (VKProfileBase) UsersService.Instance.GetCachedUser((long) data.user_id);
        VKProfileBase cachedUser2 = (VKProfileBase) UsersService.Instance.GetCachedUser((long) data.action_mid);
        string text = DialogsViewModel.GenerateText((VKMessage) data, cachedUser1, cachedUser2, true);
        ScrollableTextBlock scrollableTextBlock = new ScrollableTextBlock();
        scrollableTextBlock.FullOnly = true;
        scrollableTextBlock.Foreground = (Brush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "TextColorContent"];
        scrollableTextBlock.Text = text;
        scrollableTextBlock.FontSize = (double) ((IDictionary<object, object>) Application.Current.Resources)[(object) "FontSizeContent"];
        ((FrameworkElement) scrollableTextBlock).put_HorizontalAlignment((HorizontalAlignment) 1);
        ((ICollection<UIElement>) ((Panel) this).Children).Add((UIElement) scrollableTextBlock);
      }
      else
      {
        StackPanel stackPanel1 = new StackPanel();
        if (data.chat_id > 0 && data.@out == VKMessageType.Received)
        {
          ColumnDefinition columnDefinition1 = new ColumnDefinition();
          columnDefinition1.put_Width(new GridLength(60.0));
          ColumnDefinition columnDefinition2 = columnDefinition1;
          ColumnDefinition columnDefinition3 = new ColumnDefinition();
          ((ICollection<ColumnDefinition>) this.ColumnDefinitions).Add(columnDefinition2);
          ((ICollection<ColumnDefinition>) this.ColumnDefinitions).Add(columnDefinition3);
          Border border1 = new Border();
          ((FrameworkElement) border1).put_Width(50.0);
          ((FrameworkElement) border1).put_Height(50.0);
          ((FrameworkElement) border1).put_VerticalAlignment((VerticalAlignment) 0);
          border1.put_CornerRadius(new CornerRadius(10.0));
          Border border2 = border1;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(((UIElement) border2).add_Tapped), new Action<EventRegistrationToken>(((UIElement) border2).remove_Tapped), new TappedEventHandler(this.Image_Tapped));
          ImageBrush imageBrush1 = new ImageBrush();
          imageBrush1.put_ImageSource((ImageSource) new BitmapImage(new Uri(data.UserThumb)));
          ImageBrush imageBrush2 = imageBrush1;
          border2.put_Background((Brush) imageBrush2);
          ((ICollection<UIElement>) ((Panel) this).Children).Add((UIElement) border2);
          Grid.SetColumn((FrameworkElement) border2, 0);
          Grid.SetColumn((FrameworkElement) stackPanel1, 1);
        }
        if (data.@out == VKMessageType.Received)
        {
          ((FrameworkElement) this).put_HorizontalAlignment((HorizontalAlignment) 0);
          Path path = new Path();
          path.put_Data(this.CreateTriangleGeometry(new Point(0.0, 0.0), new Point(0.0, 10.0), new Point(20.0, 10.0)));
          Binding binding1 = new Binding();
          binding1.put_Mode((BindingMode) 1);
          binding1.put_Path(new PropertyPath("BGBrush"));
          binding1.put_Source(this.Data);
          Binding binding2 = binding1;
          ((FrameworkElement) path).SetBinding(Shape.FillProperty, (BindingBase) binding2);
          ((FrameworkElement) path).put_Margin(new Thickness(5.0, 0.0, 0.0, 0.0));
          ((ICollection<UIElement>) ((Panel) stackPanel1).Children).Add((UIElement) path);
          ((FrameworkElement) this).put_Margin(new Thickness(10.0, 10.0, 80.0, 10.0));
        }
        Border border = new Border();
        border.put_CornerRadius(new CornerRadius(5.0));
        Binding binding3 = new Binding();
        binding3.put_Mode((BindingMode) 1);
        binding3.put_Path(new PropertyPath("BGBrush"));
        binding3.put_Source(this.Data);
        Binding binding4 = binding3;
        ((FrameworkElement) border).SetBinding(Border.BackgroundProperty, (BindingBase) binding4);
        StackPanel stackPanel2 = new StackPanel();
        ((FrameworkElement) stackPanel2).put_Margin(new Thickness(10.0));
        border.put_Child((UIElement) stackPanel2);
        if (!string.IsNullOrEmpty(data.body))
        {
          ScrollableTextBlock scrollableTextBlock = new ScrollableTextBlock();
          scrollableTextBlock.FullOnly = true;
          scrollableTextBlock.Foreground = (Brush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "TextColorContent"];
          scrollableTextBlock.Text = data.body;
          scrollableTextBlock.FontSize = (double) ((IDictionary<object, object>) Application.Current.Resources)[(object) "FontSizeContent"];
          ((UIElement) scrollableTextBlock).put_IsHitTestVisible(false);
          ((ICollection<UIElement>) ((Panel) stackPanel2).Children).Add((UIElement) scrollableTextBlock);
        }
        double num1 = ((FrameworkElement) (Window.Current.Content as Frame)).ActualWidth - ((FrameworkElement) this).Margin.Right - ((FrameworkElement) this).Margin.Left - 40.0;
        if (data.fwd_messages != null)
        {
          foreach (VKMessage fwdMessage in data.fwd_messages)
          {
            double num2 = 0.0;
            if (data.UserThumbVisibility == null)
              num2 = 64.0;
            ForwardedMessagesUC forwardedMessagesUc = new ForwardedMessagesUC(fwdMessage, num1 - num2);
            ((ICollection<UIElement>) ((Panel) stackPanel2).Children).Add((UIElement) forwardedMessagesUc);
          }
        }
        if (data.chat_id > 0 && data.@out == VKMessageType.Received)
          num1 -= 50.0;
        if (data.attachments != null)
        {
          AttachmentPresenter attachmentPresenter = new AttachmentPresenter(data.attachments, num1);
          ((ICollection<UIElement>) ((Panel) stackPanel2).Children).Add((UIElement) attachmentPresenter);
        }
        if (data.OutboundMessageVM != null && data.OutboundMessageVM.Attachments != null && data.OutboundMessageVM.Attachments.Count > 0)
        {
          AttachmentPresenter attachmentPresenter = new AttachmentPresenter(data.OutboundMessageVM.Attachments, num1, data.@out == VKMessageType.Sent);
          ((ICollection<UIElement>) ((Panel) stackPanel2).Children).Add((UIElement) attachmentPresenter);
        }
        StackPanel stackPanel3 = new StackPanel();
        stackPanel3.put_Orientation((Orientation) 1);
        ((FrameworkElement) stackPanel3).put_HorizontalAlignment((HorizontalAlignment) 2);
        ((UIElement) stackPanel3).put_Opacity(0.7);
        StackPanel stackPanel4 = stackPanel3;
        TextBlock textBlock1 = new TextBlock();
        textBlock1.put_Text("(ред.)");
        textBlock1.put_FontSize((double) ((IDictionary<object, object>) Application.Current.Resources)[(object) "FontSizeSmall"]);
        ((UIElement) textBlock1).put_Opacity(0.6);
        textBlock1.put_Foreground((Brush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "TextColorContent"]);
        TextBlock textBlock2 = textBlock1;
        Binding binding5 = new Binding();
        binding5.put_Mode((BindingMode) 1);
        binding5.put_Path(new PropertyPath("EditedVisibility"));
        binding5.put_Source(this.Data);
        Binding binding6 = binding5;
        ((FrameworkElement) textBlock2).SetBinding(UIElement.VisibilityProperty, (BindingBase) binding6);
        ((ICollection<UIElement>) ((Panel) stackPanel4).Children).Add((UIElement) textBlock2);
        IconUC iconUc1 = new IconUC();
        iconUc1.put_Glyph("\uE735");
        iconUc1.put_FontSize(18.0);
        ((FrameworkElement) iconUc1).put_Margin(new Thickness(0.0, 0.0, 5.0, 0.0));
        ((IconElement) iconUc1).put_Foreground((Brush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "AccentBrushHigh"]);
        IconUC iconUc2 = iconUc1;
        Binding binding7 = new Binding();
        binding7.put_Mode((BindingMode) 1);
        binding7.put_Path(new PropertyPath("important"));
        binding7.put_Source(this.Data);
        Binding binding8 = binding7;
        binding8.put_Converter((IValueConverter) new BoolToVisibilityConverter());
        ((FrameworkElement) iconUc2).SetBinding(UIElement.VisibilityProperty, (BindingBase) binding8);
        ((ICollection<UIElement>) ((Panel) stackPanel4).Children).Add((UIElement) iconUc2);
        TextBlock textBlock3 = new TextBlock();
        textBlock3.put_Foreground((Brush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "TextColorContent"]);
        textBlock3.put_Text(UIStringFormatterHelper.FormatDateTimeForUI(data.date));
        textBlock3.put_FontSize((double) ((IDictionary<object, object>) Application.Current.Resources)[(object) "FontSizeSmall"]);
        TextBlock textBlock4 = textBlock3;
        ((ICollection<UIElement>) ((Panel) stackPanel4).Children).Add((UIElement) textBlock4);
        IconUC iconUc3 = new IconUC();
        iconUc3.put_Glyph("\uE73E");
        iconUc3.put_FontSize(18.0);
        ((FrameworkElement) iconUc3).put_Margin(new Thickness(5.0, 0.0, 0.0, 0.0));
        iconUc3.put_FontWeight(FontWeights.ExtraBlack);
        IconUC iconUc4 = iconUc3;
        Binding binding9 = new Binding();
        binding9.put_Mode((BindingMode) 1);
        binding9.put_Path(new PropertyPath("MsgStateBrush"));
        binding9.put_Source(this.Data);
        Binding binding10 = binding9;
        ((FrameworkElement) iconUc4).SetBinding(IconElement.ForegroundProperty, (BindingBase) binding10);
        Binding binding11 = new Binding();
        binding11.put_Mode((BindingMode) 1);
        binding11.put_Path(new PropertyPath("MsgState"));
        binding11.put_Source(this.Data);
        Binding binding12 = binding11;
        ((FrameworkElement) iconUc4).SetBinding(FontIcon.GlyphProperty, (BindingBase) binding12);
        ((ICollection<UIElement>) ((Panel) stackPanel4).Children).Add((UIElement) iconUc4);
        ((ICollection<UIElement>) ((Panel) stackPanel2).Children).Add((UIElement) stackPanel4);
        ((ICollection<UIElement>) ((Panel) stackPanel1).Children).Add((UIElement) border);
        if (data.@out == VKMessageType.Sent)
        {
          ((FrameworkElement) this).put_HorizontalAlignment((HorizontalAlignment) 2);
          Path path = new Path();
          path.put_Data(this.CreateTriangleGeometry(new Point(20.0, 0.0), new Point(20.0, 10.0), new Point(0.0, 0.0)));
          Binding binding13 = new Binding();
          binding13.put_Mode((BindingMode) 1);
          binding13.put_Path(new PropertyPath("BGBrush"));
          binding13.put_Source(this.Data);
          Binding binding14 = binding13;
          ((FrameworkElement) path).SetBinding(Shape.FillProperty, (BindingBase) binding14);
          ((FrameworkElement) path).put_Margin(new Thickness(0.0, 0.0, 5.0, 0.0));
          ((FrameworkElement) path).put_HorizontalAlignment((HorizontalAlignment) 2);
          ((ICollection<UIElement>) ((Panel) stackPanel1).Children).Add((UIElement) path);
          ((FrameworkElement) this).put_Margin(new Thickness(80.0, 10.0, 10.0, 10.0));
        }
        ((ICollection<UIElement>) ((Panel) this).Children).Add((UIElement) stackPanel1);
      }
    }

    private void Image_Tapped(object sender, TappedRoutedEventArgs e)
    {
      NavigatorImpl.Instance.NavigateToProfilePage((long) (this.Data as VKMessageVM).user_id);
    }

    private Geometry CreateTriangleGeometry(Point p1, Point p2, Point p3)
    {
      PathFigure pathFigure1 = new PathFigure();
      pathFigure1.put_StartPoint(p1);
      PathFigure pathFigure2 = pathFigure1;
      LineSegment lineSegment1 = new LineSegment();
      lineSegment1.put_Point(p2);
      LineSegment lineSegment2 = lineSegment1;
      LineSegment lineSegment3 = new LineSegment();
      lineSegment3.put_Point(p3);
      LineSegment lineSegment4 = lineSegment3;
      ((ICollection<PathSegment>) pathFigure2.Segments).Add((PathSegment) lineSegment2);
      ((ICollection<PathSegment>) pathFigure2.Segments).Add((PathSegment) lineSegment4);
      PathGeometry pathGeometry = new PathGeometry();
      pathGeometry.put_Figures(new PathFigureCollection());
      PathGeometry triangleGeometry = pathGeometry;
      ((ICollection<PathFigure>) triangleGeometry.Figures).Add(pathFigure2);
      return (Geometry) triangleGeometry;
    }
  }
}
