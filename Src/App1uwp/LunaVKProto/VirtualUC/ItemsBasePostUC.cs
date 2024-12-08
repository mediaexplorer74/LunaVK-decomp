// Decompiled with JetBrains decompiler
// Type: App1uwp.VirtualUC.ItemsBasePostUC
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Library;
using App1uwp.Network;
using App1uwp.Network.DataObjects;
using App1uwp.Network.Enums;
using App1uwp.UC;
using App1uwp.UC.Attachment;
using App1uwp.Utils;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

#nullable disable
namespace App1uwp.VirtualUC
{
  public class ItemsBasePostUC : StackPanel
  {
    private PopUP pop;
    public bool IsCopyPost;
    public static readonly DependencyProperty DataProperty = DependencyProperty.Register(nameof (DataNews), typeof (VKBaseDataForPostOrNews), typeof (ItemsBasePostUC), new PropertyMetadata((object) null, new PropertyChangedCallback(ItemsBasePostUC.OnDataChanged)));
    public static readonly DependencyProperty DataPostProperty = DependencyProperty.Register(nameof (DataPost), typeof (VKBaseDataForPostOrNews), typeof (ItemsBasePostUC), new PropertyMetadata((object) null, new PropertyChangedCallback(ItemsBasePostUC.OnDataChanged)));

    public VKNewsfeedPost DataNews
    {
      get => (VKNewsfeedPost) ((DependencyObject) this).GetValue(ItemsBasePostUC.DataProperty);
      set => ((DependencyObject) this).SetValue(ItemsBasePostUC.DataProperty, (object) value);
    }

    public VKWallPost DataPost
    {
      get => (VKWallPost) ((DependencyObject) this).GetValue(ItemsBasePostUC.DataPostProperty);
      set => ((DependencyObject) this).SetValue(ItemsBasePostUC.DataPostProperty, (object) value);
    }

    private VKBaseDataForPostOrNews Data
    {
      get
      {
        if (this.DataNews != null)
          return (VKBaseDataForPostOrNews) this.DataNews;
        return this.DataPost != null ? (VKBaseDataForPostOrNews) this.DataPost : (VKBaseDataForPostOrNews) null;
      }
    }

    private static void OnDataChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
      (obj as ItemsBasePostUC).ProcessData();
    }

    private void ProcessData()
    {
      ((ICollection<UIElement>) ((Panel) this).Children).Clear();
      if (this.Data == null)
        return;
      ((FrameworkElement) this).put_Margin(new Thickness(0.0, 0.0, 0.0, 10.0));
      ((Panel) this).put_Background((Brush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "ItemBackgroundBrush"]);
      bool flag = true;
      Grid grid1 = new Grid();
      ColumnDefinitionCollection columnDefinitions1 = grid1.ColumnDefinitions;
      ColumnDefinition columnDefinition1 = new ColumnDefinition();
      columnDefinition1.put_Width(new GridLength(75.0));
      ColumnDefinition columnDefinition2 = columnDefinition1;
      ((ICollection<ColumnDefinition>) columnDefinitions1).Add(columnDefinition2);
      ((ICollection<ColumnDefinition>) grid1.ColumnDefinitions).Add(new ColumnDefinition());
      ColumnDefinitionCollection columnDefinitions2 = grid1.ColumnDefinitions;
      ColumnDefinition columnDefinition3 = new ColumnDefinition();
      columnDefinition3.put_Width(new GridLength(40.0));
      ColumnDefinition columnDefinition4 = columnDefinition3;
      ((ICollection<ColumnDefinition>) columnDefinitions2).Add(columnDefinition4);
      ((ICollection<RowDefinition>) grid1.RowDefinitions).Add(new RowDefinition());
      ((ICollection<RowDefinition>) grid1.RowDefinitions).Add(new RowDefinition());
      ((FrameworkElement) grid1).put_Margin(new Thickness(0.0, 10.0, 0.0, 0.0));
      ((Panel) grid1).put_Background((Brush) new SolidColorBrush(Colors.Transparent));
      WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(((UIElement) grid1).add_Tapped), new Action<EventRegistrationToken>(((UIElement) grid1).remove_Tapped), new TappedEventHandler(this._headerTapped));
      Image image1 = new Image();
      Image image2 = image1;
      double resource;
      ((FrameworkElement) image1).put_Width(resource = (double) ((IDictionary<object, object>) Application.Current.Resources)[(object) "Double50"]);
      double num = resource;
      ((FrameworkElement) image2).put_Height(num);
      image1.put_Source((ImageSource) new BitmapImage(new Uri(this.Data.Owner.photo_100)));
      Grid.SetRowSpan((FrameworkElement) image1, 2);
      ((FrameworkElement) image1).put_HorizontalAlignment((HorizontalAlignment) 1);
      StackPanel stackPanel1 = new StackPanel();
      stackPanel1.put_Orientation((Orientation) 1);
      Grid.SetColumn((FrameworkElement) stackPanel1, 1);
      if (this.IsCopyPost)
      {
        IconUC iconUc = new IconUC();
        iconUc.put_Glyph("\uEE35");
        iconUc.put_FontSize((double) ((IDictionary<object, object>) Application.Current.Resources)[(object) "FontSizeContent"]);
        ((FrameworkElement) iconUc).put_VerticalAlignment((VerticalAlignment) 1);
        ((IconElement) iconUc).put_Foreground((Brush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "AccentBrushHigh"]);
        ((FrameworkElement) iconUc).put_Margin(new Thickness(0.0, 0.0, 10.0, 0.0));
        ((ICollection<UIElement>) ((Panel) stackPanel1).Children).Add((UIElement) iconUc);
      }
      TextBlock textBlock1 = new TextBlock();
      textBlock1.put_Text(this.Data.Owner.Title);
      ((FrameworkElement) textBlock1).put_VerticalAlignment((VerticalAlignment) 1);
      textBlock1.put_FontSize((double) ((IDictionary<object, object>) Application.Current.Resources)[(object) "FontSizeContent"]);
      textBlock1.put_Foreground((Brush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "AccentBrushHigh"]);
      ((ICollection<UIElement>) ((Panel) stackPanel1).Children).Add((UIElement) textBlock1);
      if (this.Data is VKWallPost && this.DataPost.is_pinned)
      {
        IconUC iconUc = new IconUC();
        iconUc.put_Glyph("\uE840");
        iconUc.put_FontSize((double) ((IDictionary<object, object>) Application.Current.Resources)[(object) "FontSizeContent"]);
        ((FrameworkElement) iconUc).put_VerticalAlignment((VerticalAlignment) 1);
        ((IconElement) iconUc).put_Foreground((Brush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "AccentBrushHigh"]);
        ((FrameworkElement) iconUc).put_Margin(new Thickness(10.0, 0.0, 0.0, 0.0));
        ((ICollection<UIElement>) ((Panel) stackPanel1).Children).Add((UIElement) iconUc);
      }
      TextBlock textBlock2 = new TextBlock();
      textBlock2.put_Text(UIStringFormatterHelper.FormatDateTimeForUI(this.Data.date));
      Grid.SetColumn((FrameworkElement) textBlock2, 1);
      Grid.SetRow((FrameworkElement) textBlock2, 1);
      ((FrameworkElement) textBlock2).put_VerticalAlignment((VerticalAlignment) 1);
      textBlock2.put_FontSize((double) ((IDictionary<object, object>) Application.Current.Resources)[(object) "FontSizeContent"]);
      textBlock2.put_Foreground((Brush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "TextColorContent"]);
      ((ICollection<UIElement>) ((Panel) grid1).Children).Add((UIElement) image1);
      ((ICollection<UIElement>) ((Panel) grid1).Children).Add((UIElement) stackPanel1);
      ((ICollection<UIElement>) ((Panel) grid1).Children).Add((UIElement) textBlock2);
      if (!this.IsCopyPost)
      {
        Border border = new Border();
        Grid.SetColumn((FrameworkElement) border, 2);
        Grid.SetRowSpan((FrameworkElement) border, 2);
        border.put_Background((Brush) new SolidColorBrush(Colors.Transparent));
        WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(((UIElement) border).add_Tapped), new Action<EventRegistrationToken>(((UIElement) border).remove_Tapped), new TappedEventHandler(this.action_Tapped));
        IconUC iconUc = new IconUC();
        iconUc.put_Glyph("\uE972");
        iconUc.put_FontSize((double) ((IDictionary<object, object>) Application.Current.Resources)[(object) "FontSizeContent"]);
        ((FrameworkElement) iconUc).put_VerticalAlignment((VerticalAlignment) 1);
        ((IconElement) iconUc).put_Foreground((Brush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "TextColorContent"]);
        border.put_Child((UIElement) iconUc);
        ((ICollection<UIElement>) ((Panel) grid1).Children).Add((UIElement) border);
      }
      ((ICollection<UIElement>) ((Panel) this).Children).Add((UIElement) grid1);
      StackPanel stackPanel2 = new StackPanel();
      ((FrameworkElement) stackPanel2).put_Margin(new Thickness(0.0, 10.0, 0.0, 0.0));
      if (!string.IsNullOrEmpty(this.Data.text))
      {
        ScrollableTextBlock scrollableTextBlock = new ScrollableTextBlock();
        scrollableTextBlock.Foreground = (Brush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "TextColorContent"];
        scrollableTextBlock.Text = this.Data.text;
        ((FrameworkElement) scrollableTextBlock).put_Margin(new Thickness(15.0, 0.0, 15.0, 0.0));
        ((FrameworkElement) scrollableTextBlock).put_Margin(new Thickness(10.0, 0.0, 10.0, 15.0));
        ((ICollection<UIElement>) ((Panel) stackPanel2).Children).Add((UIElement) scrollableTextBlock);
      }
      if (this.Data is VKNewsfeedPost && this.DataNews.photos != null)
      {
        AttachPhotosUC attachPhotosUc = new AttachPhotosUC(this.DataNews.photos.items, ((FrameworkElement) (((ContentControl) (Window.Current.Content as Frame)).Content as Page)).ActualWidth);
        ((ICollection<UIElement>) ((Panel) stackPanel2).Children).Add((UIElement) attachPhotosUc);
      }
      if (this.Data.attachments != null)
      {
        AttachmentPresenter attachmentPresenter = new AttachmentPresenter(this.Data.attachments);
        ((ICollection<UIElement>) ((Panel) stackPanel2).Children).Add((UIElement) attachmentPresenter);
      }
      if (this.Data is VKNewsfeedPost)
      {
        if (this.DataNews.copy_history != null && this.DataNews.copy_history.Count > 0)
        {
          foreach (VKNewsfeedPost vkNewsfeedPost in this.DataNews.copy_history)
          {
            ItemsBasePostUC itemsBasePostUc = new ItemsBasePostUC();
            itemsBasePostUc.IsCopyPost = true;
            itemsBasePostUc.DataNews = vkNewsfeedPost;
            ((FrameworkElement) itemsBasePostUc).put_DataContext((object) vkNewsfeedPost);
            ((ICollection<UIElement>) ((Panel) stackPanel2).Children).Add((UIElement) itemsBasePostUc);
          }
        }
        if (this.DataNews.type == VKNewsfeedFilters.video && this.DataNews.video != null)
        {
          AttachVideosUC attachVideosUc = new AttachVideosUC(this.DataNews.video.items);
          ((ICollection<UIElement>) ((Panel) stackPanel2).Children).Add((UIElement) attachVideosUc);
        }
      }
      else if (this.Data is VKWallPost && this.DataPost.copy_history != null && this.DataPost.copy_history.Count > 0)
      {
        foreach (VKWallPost vkWallPost in this.DataPost.copy_history)
        {
          ItemsBasePostUC itemsBasePostUc = new ItemsBasePostUC();
          itemsBasePostUc.IsCopyPost = true;
          itemsBasePostUc.DataPost = vkWallPost;
          ((FrameworkElement) itemsBasePostUc).put_DataContext((object) vkWallPost);
          ((ICollection<UIElement>) ((Panel) stackPanel2).Children).Add((UIElement) itemsBasePostUc);
        }
      }
      ((ICollection<UIElement>) ((Panel) this).Children).Add((UIElement) stackPanel2);
      if (!flag)
        return;
      Grid grid2 = new Grid();
      ((FrameworkElement) grid2).put_Margin(new Thickness(0.0, 10.0, 0.0, 10.0));
      StackPanel stackPanel3 = new StackPanel();
      stackPanel3.put_Orientation((Orientation) 1);
      if (this.Data.likes != null && (this.Data.likes.can_like || this.Data.likes.user_likes))
      {
        StackPanel stackPanel4 = new StackPanel();
        ((Panel) stackPanel4).put_Background((Brush) new SolidColorBrush(Colors.Transparent));
        stackPanel4.put_Orientation((Orientation) 1);
        IconUC like_icon = new IconUC();
        like_icon.put_Glyph("\uE006");
        like_icon.put_FontSize((double) ((IDictionary<object, object>) Application.Current.Resources)[(object) "FontSizeContent"]);
        ((FrameworkElement) like_icon).put_VerticalAlignment((VerticalAlignment) 1);
        ((IconElement) like_icon).put_Foreground((Brush) this.GetIconCounterForeground());
        ((FrameworkElement) like_icon).put_Margin(new Thickness(20.0, 0.0, 0.0, 0.0));
        ((ICollection<UIElement>) ((Panel) stackPanel4).Children).Add((UIElement) like_icon);
        TextBlock like_text = new TextBlock();
        ((FrameworkElement) like_text).put_Margin(new Thickness(10.0, 0.0, 0.0, 0.0));
        ((FrameworkElement) like_text).put_VerticalAlignment((VerticalAlignment) 1);
        like_text.put_FontSize((double) ((IDictionary<object, object>) Application.Current.Resources)[(object) "FontSizeContent"]);
        like_text.put_Foreground((Brush) this.GetIconCounterForeground());
        ((UIElement) like_text).put_Visibility((Visibility) 1);
        ((ICollection<UIElement>) ((Panel) stackPanel4).Children).Add((UIElement) like_text);
        if (this.Data.likes.count > 0)
        {
          like_text.put_Text(this.Data.likes.count.ToString());
          ((UIElement) like_text).put_Visibility((Visibility) 0);
        }
        WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(((UIElement) stackPanel4).add_Tapped), new Action<EventRegistrationToken>(((UIElement) stackPanel4).remove_Tapped), (TappedEventHandler) (async (sender, e) =>
        {
          ((UIElement) (sender as FrameworkElement)).put_IsHitTestVisible(false);
          VKResponse<ItemsBasePostUC.TempLikes> temp = await RequestsDispatcher.GetResponse<ItemsBasePostUC.TempLikes>(this.Data.likes.user_likes ? "likes.delete" : "likes.add", new Dictionary<string, string>()
          {
            ["owner_id"] = this.Data.OwnerId.ToString(),
            ["item_id"] = this.Data.PostId.ToString(),
            ["type"] = this.Data.post_type.ToString()
          });
          ((UIElement) (sender as FrameworkElement)).put_IsHitTestVisible(true);
          if (temp == null)
            return;
          this.Data.likes.count = temp.response.likes;
          this.Data.likes.user_likes = !this.Data.likes.user_likes;
          like_text.put_Text(this.Data.likes.count.ToString());
          like_text.put_Foreground((Brush) this.GetIconCounterForeground());
          ((IconElement) like_icon).put_Foreground((Brush) this.GetIconCounterForeground());
          if (this.Data.likes.count > 0)
            ((UIElement) like_text).put_Visibility((Visibility) 0);
          else
            ((UIElement) like_text).put_Visibility((Visibility) 1);
        }));
        ((ICollection<UIElement>) ((Panel) stackPanel3).Children).Add((UIElement) stackPanel4);
      }
      if (this.Data.comments != null && this.Data.comments.can_post)
      {
        StackPanel stackPanel5 = new StackPanel();
        ((Panel) stackPanel5).put_Background((Brush) new SolidColorBrush(Colors.Transparent));
        stackPanel5.put_Orientation((Orientation) 1);
        WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(((UIElement) stackPanel5).add_Tapped), new Action<EventRegistrationToken>(((UIElement) stackPanel5).remove_Tapped), new TappedEventHandler(this.Comments_Tapped));
        IconUC iconUc = new IconUC();
        iconUc.put_Glyph("\uE8BD");
        iconUc.put_FontSize((double) ((IDictionary<object, object>) Application.Current.Resources)[(object) "FontSizeContent"]);
        ((FrameworkElement) iconUc).put_VerticalAlignment((VerticalAlignment) 1);
        ((IconElement) iconUc).put_Foreground((Brush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "TextColorSubContent"]);
        ((FrameworkElement) iconUc).put_Margin(new Thickness(25.0, 0.0, 0.0, 0.0));
        ((ICollection<UIElement>) ((Panel) stackPanel5).Children).Add((UIElement) iconUc);
        if (this.Data.comments.count > 0)
        {
          TextBlock textBlock3 = new TextBlock();
          textBlock3.put_Text(this.Data.comments.count.ToString());
          ((FrameworkElement) textBlock3).put_Margin(new Thickness(10.0, 0.0, 0.0, 0.0));
          ((FrameworkElement) textBlock3).put_VerticalAlignment((VerticalAlignment) 1);
          textBlock3.put_FontSize((double) ((IDictionary<object, object>) Application.Current.Resources)[(object) "FontSizeContent"]);
          textBlock3.put_Foreground((Brush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "TextColorSubContent"]);
          ((ICollection<UIElement>) ((Panel) stackPanel5).Children).Add((UIElement) textBlock3);
        }
        ((ICollection<UIElement>) ((Panel) stackPanel3).Children).Add((UIElement) stackPanel5);
      }
      if (this.Data.likes != null && this.Data.likes.can_publish)
      {
        StackPanel stackPanel6 = new StackPanel();
        ((Panel) stackPanel6).put_Background((Brush) new SolidColorBrush(Colors.Transparent));
        stackPanel6.put_Orientation((Orientation) 1);
        WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(((UIElement) stackPanel6).add_Tapped), new Action<EventRegistrationToken>(((UIElement) stackPanel6).remove_Tapped), new TappedEventHandler(this.Publish_Tapped));
        IconUC iconUc = new IconUC();
        iconUc.put_Glyph("\uE789");
        iconUc.put_FontSize((double) ((IDictionary<object, object>) Application.Current.Resources)[(object) "FontSizeContent"]);
        ((FrameworkElement) iconUc).put_VerticalAlignment((VerticalAlignment) 1);
        ((IconElement) iconUc).put_Foreground((Brush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "TextColorSubContent"]);
        ((FrameworkElement) iconUc).put_Margin(new Thickness(25.0, 0.0, 0.0, 0.0));
        ((ICollection<UIElement>) ((Panel) stackPanel6).Children).Add((UIElement) iconUc);
        if (this.Data.reposts.count > 0)
        {
          TextBlock textBlock4 = new TextBlock();
          textBlock4.put_Text(this.Data.reposts.count.ToString());
          ((FrameworkElement) textBlock4).put_Margin(new Thickness(10.0, 0.0, 10.0, 0.0));
          ((FrameworkElement) textBlock4).put_VerticalAlignment((VerticalAlignment) 1);
          textBlock4.put_FontSize((double) ((IDictionary<object, object>) Application.Current.Resources)[(object) "FontSizeContent"]);
          textBlock4.put_Foreground((Brush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "TextColorSubContent"]);
          ((ICollection<UIElement>) ((Panel) stackPanel6).Children).Add((UIElement) textBlock4);
        }
        ((ICollection<UIElement>) ((Panel) stackPanel3).Children).Add((UIElement) stackPanel6);
      }
      ((ICollection<UIElement>) ((Panel) grid2).Children).Add((UIElement) stackPanel3);
      if (this.Data.views != null)
      {
        StackPanel stackPanel7 = new StackPanel();
        stackPanel7.put_Orientation((Orientation) 1);
        ((FrameworkElement) stackPanel7).put_HorizontalAlignment((HorizontalAlignment) 2);
        IconUC iconUc = new IconUC();
        iconUc.put_Glyph("\uE7B3");
        iconUc.put_FontSize((double) ((IDictionary<object, object>) Application.Current.Resources)[(object) "FontSizeContent"]);
        ((FrameworkElement) iconUc).put_VerticalAlignment((VerticalAlignment) 1);
        ((IconElement) iconUc).put_Foreground((Brush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "TextColorSubContent"]);
        TextBlock textBlock5 = new TextBlock();
        textBlock5.put_Text(this.Data.views.count.ToString());
        ((FrameworkElement) textBlock5).put_Margin(new Thickness(10.0, 0.0, 20.0, 0.0));
        ((FrameworkElement) textBlock5).put_VerticalAlignment((VerticalAlignment) 1);
        textBlock5.put_FontSize((double) ((IDictionary<object, object>) Application.Current.Resources)[(object) "FontSizeContent"]);
        textBlock5.put_Foreground((Brush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "TextColorSubContent"]);
        ((ICollection<UIElement>) ((Panel) stackPanel7).Children).Add((UIElement) iconUc);
        ((ICollection<UIElement>) ((Panel) stackPanel7).Children).Add((UIElement) textBlock5);
        ((ICollection<UIElement>) ((Panel) grid2).Children).Add((UIElement) stackPanel7);
      }
      ((ICollection<UIElement>) ((Panel) this).Children).Add((UIElement) grid2);
    }

    private SolidColorBrush GetIconCounterForeground()
    {
      return ((IDictionary<object, object>) Application.Current.Resources)[this.Data.likes.user_likes ? (object) "AccentBrushHigh" : (object) "TextColorSubContent"] as SolidColorBrush;
    }

    private void _headerTapped(object sender, TappedRoutedEventArgs e)
    {
      object dataContext = (sender as FrameworkElement).DataContext;
      NavigatorImpl.Instance.NavigateToProfilePage(this.DataPost == null ? (long) (int) this.DataNews.OwnerId : (long) this.DataPost.from_id);
    }

    private void action_Tapped(object sender, TappedRoutedEventArgs e) => e.put_Handled(true);

    private void Publish_Tapped(object sender, TappedRoutedEventArgs e)
    {
      Point position = e.GetPosition((UIElement) null);
      if (this.pop == null)
      {
        this.pop = new PopUP();
        this.pop.ItemTapped += new EventHandler<int>(this._picker_ItemTapped);
      }
      this.pop.ClearItems();
      this.pop.AddItem(0, "Написать на странице", "\uEDC6", false);
      this.pop.AddItem(1, "Опубликовать в сообществе", "\uEC26", false);
      this.pop.AddItem(2, "Отправить сообщением", "\uE715");
      this.pop.Show(position);
      e.put_Handled(true);
    }

    private void _picker_ItemTapped(object argument, int i)
    {
      if (i != 2)
        return;
      NavigatorImpl.Instance.NavigateToConversations((object) new OutboundWallPostAttachment(this.Data));
    }

    private void Comments_Tapped(object sender, TappedRoutedEventArgs e)
    {
      VKBaseDataForPostOrNews dataContext = (sender as FrameworkElement).DataContext as VKBaseDataForPostOrNews;
      switch (dataContext.post_type)
      {
        case VKNewsfeedPostType.post:
          NavigatorImpl.Instance.NavigateToWallPostComments((ulong) dataContext.PostId, dataContext.OwnerId, postData: dataContext);
          break;
        case VKNewsfeedPostType.video:
          e.put_Handled(true);
          break;
      }
    }

    private class TempLikes
    {
      public int likes { get; set; }
    }
  }
}
