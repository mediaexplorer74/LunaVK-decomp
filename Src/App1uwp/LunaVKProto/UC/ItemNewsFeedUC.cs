// Decompiled with JetBrains decompiler
// Type: App1uwp.UC.ItemNewsFeedUC
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Library;
using App1uwp.Network;
using App1uwp.Network.DataObjects;
using App1uwp.Network.Enums;
using App1uwp.UC.Attachment;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;

#nullable disable
namespace App1uwp.UC
{
  public sealed class ItemNewsFeedUC : UserControl, IComponentConnector
  {
    public static readonly DependencyProperty PostProperty = DependencyProperty.Register(nameof (Post), typeof (object), typeof (ItemNewsFeedUC), new PropertyMetadata((object) null, new PropertyChangedCallback(ItemNewsFeedUC.OnPostChanged)));
    private PopUP pop;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private StackPanel MainContent;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Grid footer;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private StackPanel _brdComments;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Border action;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public ItemNewsFeedUC() => this.InitializeComponent();

    public object Post
    {
      get => ((DependencyObject) this).GetValue(ItemNewsFeedUC.PostProperty);
      set => ((DependencyObject) this).SetValue(ItemNewsFeedUC.PostProperty, value);
    }

    private static void OnPostChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
      ((ItemNewsFeedUC) obj).ProcessPost();
    }

    private void ProcessPost()
    {
      ((ICollection<UIElement>) ((Panel) this.MainContent).Children).Clear();
      if (this.Post == null || !(this.Post is VKNewsfeedPost))
        return;
      this.AsNewsfeedPost();
    }

    public ItemNewsFeedUC(bool is_copy_post)
      : this()
    {
      if (!is_copy_post)
        return;
      ((UIElement) this.footer).put_Visibility((Visibility) 1);
      ((UIElement) this.action).put_Visibility((Visibility) 1);
    }

    private VKNewsfeedPost NewsfeedPostVM => this.Post as VKNewsfeedPost;

    private void AsNewsfeedPost()
    {
      if (this.NewsfeedPostVM.type == VKNewsfeedFilters.video && this.NewsfeedPostVM.video != null)
      {
        foreach (VKVideoBase a in this.NewsfeedPostVM.video.items)
          ((ICollection<UIElement>) ((Panel) this.MainContent).Children).Add((UIElement) new AttachVideoUC(a));
      }
      if (!string.IsNullOrEmpty(this.NewsfeedPostVM.text))
      {
        ScrollableTextBlock scrollableTextBlock = new ScrollableTextBlock();
        scrollableTextBlock.Foreground = (Brush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "TextColorContent"];
        scrollableTextBlock.Text = this.NewsfeedPostVM.text;
        ((FrameworkElement) scrollableTextBlock).put_Margin(new Thickness(15.0, 0.0, 15.0, 0.0));
        ((FrameworkElement) scrollableTextBlock).put_Margin(new Thickness(10.0, 0.0, 10.0, 15.0));
        ((ICollection<UIElement>) ((Panel) this.MainContent).Children).Add((UIElement) scrollableTextBlock);
      }
      if (this.NewsfeedPostVM.photos != null)
        ((ICollection<UIElement>) ((Panel) this.MainContent).Children).Add((UIElement) new AttachPhotosUC(this.NewsfeedPostVM.photos.items, ((FrameworkElement) (((ContentControl) (Window.Current.Content as Frame)).Content as PageBase)).ActualWidth));
      if (this.NewsfeedPostVM.attachments != null)
        ((ICollection<UIElement>) ((Panel) this.MainContent).Children).Add((UIElement) new AttachmentPresenter(this.NewsfeedPostVM.attachments));
      if (this.NewsfeedPostVM.copy_history == null || this.NewsfeedPostVM.copy_history.Count <= 0)
        return;
      foreach (VKNewsfeedPost vkNewsfeedPost in this.NewsfeedPostVM.copy_history)
      {
        ItemNewsFeedUC itemNewsFeedUc = new ItemNewsFeedUC(true);
        itemNewsFeedUc.Post = (object) vkNewsfeedPost;
        ((FrameworkElement) itemNewsFeedUc).put_DataContext((object) vkNewsfeedPost);
        ((ICollection<UIElement>) ((Panel) this.MainContent).Children).Add((UIElement) itemNewsFeedUc);
      }
    }

    private async void Like_Tapped(object sender, TappedRoutedEventArgs e)
    {
      ((UIElement) (sender as FrameworkElement)).put_IsHitTestVisible(false);
      VKResponse<ItemNewsFeedUC.TempLikes> temp = await RequestsDispatcher.GetResponse<ItemNewsFeedUC.TempLikes>(this.NewsfeedPostVM.likes.user_likes ? "likes.delete" : "likes.add", new Dictionary<string, string>()
      {
        ["owner_id"] = this.NewsfeedPostVM.source_id.ToString(),
        ["item_id"] = this.NewsfeedPostVM.post_id.ToString(),
        ["type"] = this.NewsfeedPostVM.post_type.ToString()
      });
      ((UIElement) (sender as FrameworkElement)).put_IsHitTestVisible(true);
      if (temp == null)
        return;
      this.NewsfeedPostVM.likes.count = temp.response.likes;
      this.NewsfeedPostVM.likes.user_likes = !this.NewsfeedPostVM.likes.user_likes;
      this.NewsfeedPostVM.UpdateUI();
    }

    private void StackPanel_Tapped_1(object sender, TappedRoutedEventArgs e)
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
      NavigatorImpl.Instance.NavigateToConversations((object) new OutboundWallPostAttachment((VKBaseDataForPostOrNews) this.NewsfeedPostVM));
    }

    private void _headerTapped(object sender, TappedRoutedEventArgs e)
    {
      NavigatorImpl.Instance.NavigateToProfilePage((long) (int) ((sender as FrameworkElement).DataContext as VKBaseDataForPostOrNews).OwnerId);
    }

    private void StackPanel_Tapped(object sender, TappedRoutedEventArgs e)
    {
    }

    private void action_Tapped(object sender, TappedRoutedEventArgs e) => e.put_Handled(true);

    private void Comments_Tapped(object sender, TappedRoutedEventArgs e)
    {
      switch (((VKNewsfeedPost) (sender as FrameworkElement).DataContext).type)
      {
        case VKNewsfeedFilters.video:
          e.put_Handled(true);
          break;
      }
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///UC/ItemNewsFeedUC.xaml"), (ComponentResourceLocation) 0);
      this.MainContent = (StackPanel) ((FrameworkElement) this).FindName("MainContent");
      this.footer = (Grid) ((FrameworkElement) this).FindName("footer");
      this._brdComments = (StackPanel) ((FrameworkElement) this).FindName("_brdComments");
      this.action = (Border) ((FrameworkElement) this).FindName("action");
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          UIElement uiElement1 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement1.add_Tapped), new Action<EventRegistrationToken>(uiElement1.remove_Tapped), new TappedEventHandler(this._headerTapped));
          break;
        case 2:
          UIElement uiElement2 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement2.add_Tapped), new Action<EventRegistrationToken>(uiElement2.remove_Tapped), new TappedEventHandler(this.Like_Tapped));
          break;
        case 3:
          UIElement uiElement3 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement3.add_Tapped), new Action<EventRegistrationToken>(uiElement3.remove_Tapped), new TappedEventHandler(this.Comments_Tapped));
          break;
        case 4:
          UIElement uiElement4 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement4.add_Tapped), new Action<EventRegistrationToken>(uiElement4.remove_Tapped), new TappedEventHandler(this.StackPanel_Tapped_1));
          break;
        case 5:
          UIElement uiElement5 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement5.add_Tapped), new Action<EventRegistrationToken>(uiElement5.remove_Tapped), new TappedEventHandler(this.action_Tapped));
          break;
      }
      this._contentLoaded = true;
    }

    private class TempLikes
    {
      public int likes { get; set; }
    }
  }
}
