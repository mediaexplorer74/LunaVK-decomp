// Decompiled with JetBrains decompiler
// Type: App1uwp.UC.ItemNotificationUC
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Library;
using App1uwp.Network;
using App1uwp.Network.DataObjects;
using App1uwp.Network.Enums;
using App1uwp.Utils;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

#nullable disable
namespace App1uwp.UC
{
  public sealed class ItemNotificationUC : UserControl, IComponentConnector
  {
    public static readonly DependencyProperty PostProperty = DependencyProperty.Register(nameof (Post), typeof (object), typeof (ItemNotificationUC), new PropertyMetadata((object) null, new PropertyChangedCallback(ItemNotificationUC.OnPostChanged)));
    private VKBaseDataForGroupOrUser user;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBlock from;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBlock action;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private ScrollableTextBlock comment;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBlock date;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Image img_from;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private IconUC icon;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public object Post
    {
      get => ((DependencyObject) this).GetValue(ItemNotificationUC.PostProperty);
      set => ((DependencyObject) this).SetValue(ItemNotificationUC.PostProperty, value);
    }

    private static void OnPostChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
      ((ItemNotificationUC) obj).ProcessPost();
    }

    private VKNotification Notyfication => this.Post as VKNotification;

    private void ProcessPost()
    {
      if (this.Post == null || !(this.Post is VKNotification))
        return;
      this.icon.put_Glyph(this.SetIcon());
      this.GetThumb();
      this.comment.Text = this.GetHighlightedText();
      this.date.put_Text(UIStringFormatterHelper.FormatDateTimeForUI(this.Notyfication.date));
    }

    private void GetThumb()
    {
      string uriString = "";
      if (this.Notyfication.ParsedFeedback is List<FeedbackUser>)
      {
        List<FeedbackUser> parsedFeedback = this.Notyfication.ParsedFeedback as List<FeedbackUser>;
        this.user = UsersService.Instance.GetCachedUser(parsedFeedback[0].from_id);
        if (this.user == null)
          this.user = UsersService.Instance.GetCachedUser(parsedFeedback[0].owner_id);
      }
      else if (this.Notyfication.ParsedFeedback is VKComment)
        this.user = UsersService.Instance.GetCachedUser((this.Notyfication.ParsedFeedback as VKComment).from_id);
      else if (this.Notyfication.ParsedFeedback is VKWallPost)
      {
        this.user = UsersService.Instance.GetCachedUser((long) (this.Notyfication.ParsedFeedback as VKWallPost).from_id);
      }
      else
      {
        List<FeedbackCopyInfo> parsedFeedback1 = this.Notyfication.ParsedFeedback as List<FeedbackCopyInfo>;
      }
      if (this.user != null)
      {
        uriString = this.user.photo_100;
        this.from.put_Text(this.user.Title);
      }
      if (!string.IsNullOrEmpty(uriString))
        this.img_from.put_Source((ImageSource) new BitmapImage(new Uri(uriString)));
      this.action.put_Text(this.GetLocalizableText());
    }

    public ItemNotificationUC() => this.InitializeComponent();

    private VKUserSex GetGender()
    {
      if (this.Notyfication.ParsedFeedback is VKCountedItemsObject<FeedbackUser>)
      {
        if ((this.Notyfication.ParsedFeedback as VKCountedItemsObject<FeedbackUser>).count > 1U)
          return VKUserSex.Unknown;
      }
      else if (this.Notyfication.ParsedFeedback is VKCountedItemsObject<FeedbackCopyInfo> && (this.Notyfication.ParsedFeedback as VKCountedItemsObject<FeedbackCopyInfo>).count > 1U)
        return VKUserSex.Unknown;
      return this.user == null || this.user is VKGroup ? VKUserSex.Unknown : ((VKProfileBase) this.user).sex;
    }

    private string GetLocalizableText()
    {
      VKUserSex gender = this.GetGender();
      string key = "";
      switch (this.Notyfication.type)
      {
        case VKNotification.NotificationType.follow:
          switch (gender)
          {
            case VKUserSex.Unknown:
              key = "Notification_FollowPlural";
              break;
            case VKUserSex.Female:
              key = "Notification_FollowFemale";
              break;
            case VKUserSex.Male:
              key = "Notification_FollowMale";
              break;
          }
          break;
        case VKNotification.NotificationType.friend_accepted:
          switch (gender)
          {
            case VKUserSex.Unknown:
              key = "Notification_FriendAcceptedPlural";
              break;
            case VKUserSex.Female:
              key = "Notification_FriendAcceptedFemale";
              break;
            case VKUserSex.Male:
              key = "Notification_FriendAcceptedMale";
              break;
          }
          break;
        case VKNotification.NotificationType.mention_comments:
          switch (gender)
          {
            case VKUserSex.Female:
              key = "Notification_MentionCommentsFemale";
              break;
            case VKUserSex.Male:
              key = "Notification_MentionCommentsMale";
              break;
          }
          break;
        case VKNotification.NotificationType.wall_publish:
          return "опубликована ваша новость";
        case VKNotification.NotificationType.comment_post:
          key = "Notification_CommentPost";
          break;
        case VKNotification.NotificationType.comment_photo:
          switch (gender)
          {
            case VKUserSex.Female:
              key = "Notification_CommentPhotoFemale";
              break;
            case VKUserSex.Male:
              key = "Notification_CommentPhotoMale";
              break;
          }
          break;
        case VKNotification.NotificationType.comment_video:
          switch (gender)
          {
            case VKUserSex.Female:
              key = "Notification_CommentVideoFemale";
              break;
            case VKUserSex.Male:
              key = "Notification_CommentVideoMale";
              break;
          }
          break;
        case VKNotification.NotificationType.reply_comment:
        case VKNotification.NotificationType.reply_comment_photo:
        case VKNotification.NotificationType.reply_comment_video:
        case VKNotification.NotificationType.reply_comment_market:
        case VKNotification.NotificationType.reply_topic:
          key = "Notification_ReplyCommentOrTopic";
          break;
        case VKNotification.NotificationType.like_post:
          switch (gender)
          {
            case VKUserSex.Unknown:
              key = "Notification_LikePostPlural";
              break;
            case VKUserSex.Female:
              key = "Notification_LikePostFemale";
              break;
            case VKUserSex.Male:
              key = "Notification_LikePostMale";
              break;
          }
          break;
        case VKNotification.NotificationType.like_comment:
        case VKNotification.NotificationType.like_comment_photo:
        case VKNotification.NotificationType.like_comment_video:
        case VKNotification.NotificationType.like_comment_topic:
          switch (gender)
          {
            case VKUserSex.Unknown:
              key = "Notification_LikeCommentPlural";
              break;
            case VKUserSex.Female:
              key = "Notification_LikeCommentFemale";
              break;
            case VKUserSex.Male:
              key = "Notification_LikeCommentMale";
              break;
          }
          break;
        case VKNotification.NotificationType.like_photo:
          switch (gender)
          {
            case VKUserSex.Unknown:
              key = "Notification_LikePhotoPlural";
              break;
            case VKUserSex.Female:
              key = "Notification_LikePhotoFemale";
              break;
            case VKUserSex.Male:
              key = "Notification_LikePhotoMale";
              break;
          }
          break;
        case VKNotification.NotificationType.like_video:
          switch (gender)
          {
            case VKUserSex.Unknown:
              key = "Notification_LikeVideoPlural";
              break;
            case VKUserSex.Female:
              key = "Notification_LikeVideoFemale";
              break;
            case VKUserSex.Male:
              key = "Notification_LikeVideoMale";
              break;
          }
          break;
        case VKNotification.NotificationType.copy_post:
          switch (gender)
          {
            case VKUserSex.Unknown:
              key = "Notification_CopyPostPlural";
              break;
            case VKUserSex.Female:
              key = "Notification_CopyPostFemale";
              break;
            case VKUserSex.Male:
              key = "Notification_CopyPostMale";
              break;
          }
          break;
        case VKNotification.NotificationType.copy_photo:
          switch (gender)
          {
            case VKUserSex.Unknown:
              key = "Notification_CopyPhotoPlural";
              break;
            case VKUserSex.Female:
              key = "Notification_CopyPhotoFemale";
              break;
            case VKUserSex.Male:
              key = "Notification_CopyPhotoMale";
              break;
          }
          break;
        case VKNotification.NotificationType.copy_video:
          switch (gender)
          {
            case VKUserSex.Unknown:
              key = "Notification_CopyVideoPlural";
              break;
            case VKUserSex.Female:
              key = "Notification_CopyVideoFemale";
              break;
            case VKUserSex.Male:
              key = "Notification_CopyVideoMale";
              break;
          }
          break;
        case VKNotification.NotificationType.mention_comment_photo:
          key = "Notification_MentionInPhotoComment";
          break;
        case VKNotification.NotificationType.mention_comment_video:
          key = "Notification_MentionInVideoComment";
          break;
      }
      return string.IsNullOrEmpty(key) ? "" : LocalizedStrings.GetString(key);
    }

    private string GetHighlightedText()
    {
      if (this.Notyfication.ParsedFeedback is VKComment parsedFeedback)
        return parsedFeedback.text;
      if (this.Notyfication.ParsedParent is VKWallPost parsedParent1)
        return !string.IsNullOrEmpty(parsedParent1.text) ? parsedParent1.text : "";
      if (this.Notyfication.ParsedParent is VKComment parsedParent2)
        return parsedParent2.text;
      return this.Notyfication.ParsedParent is VKTopic parsedParent3 ? parsedParent3.title : "";
    }

    private bool IsRepost(VKWallPost wallPost) => false;

    private string SetIcon()
    {
      switch (this.Notyfication.type)
      {
        case VKNotification.NotificationType.follow:
          ((IconElement) this.icon).put_Foreground((Brush) new SolidColorBrush(Colors.Blue));
          return "\uE948";
        case VKNotification.NotificationType.friend_accepted:
          ((IconElement) this.icon).put_Foreground((Brush) new SolidColorBrush(Colors.Green));
          return "\uE948";
        case VKNotification.NotificationType.wall:
        case VKNotification.NotificationType.wall_publish:
          ((IconElement) this.icon).put_Foreground((Brush) new SolidColorBrush(Colors.Orange));
          return "\uE874";
        case VKNotification.NotificationType.comment_post:
        case VKNotification.NotificationType.comment_photo:
        case VKNotification.NotificationType.comment_video:
          return "\uED63";
        case VKNotification.NotificationType.reply_comment:
        case VKNotification.NotificationType.reply_comment_photo:
        case VKNotification.NotificationType.reply_comment_video:
        case VKNotification.NotificationType.reply_comment_market:
        case VKNotification.NotificationType.reply_topic:
          ((IconElement) this.icon).put_Foreground((Brush) new SolidColorBrush(Colors.Green));
          return "\uE8BD";
        case VKNotification.NotificationType.like_post:
        case VKNotification.NotificationType.like_comment:
        case VKNotification.NotificationType.like_photo:
        case VKNotification.NotificationType.like_video:
        case VKNotification.NotificationType.like_comment_photo:
        case VKNotification.NotificationType.like_comment_video:
        case VKNotification.NotificationType.like_comment_topic:
          ((IconElement) this.icon).put_Foreground((Brush) new SolidColorBrush(Colors.Red));
          return "\uEB52";
        default:
          return "";
      }
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///UC/ItemNotificationUC.xaml"), (ComponentResourceLocation) 0);
      this.from = (TextBlock) ((FrameworkElement) this).FindName("from");
      this.action = (TextBlock) ((FrameworkElement) this).FindName("action");
      this.comment = (ScrollableTextBlock) ((FrameworkElement) this).FindName("comment");
      this.date = (TextBlock) ((FrameworkElement) this).FindName("date");
      this.img_from = (Image) ((FrameworkElement) this).FindName("img_from");
      this.icon = (IconUC) ((FrameworkElement) this).FindName("icon");
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void Connect(int connectionId, object target) => this._contentLoaded = true;
  }
}
