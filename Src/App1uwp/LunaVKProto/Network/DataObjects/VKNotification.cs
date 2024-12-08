// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.DataObjects.VKNotification
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

#nullable disable
namespace App1uwp.Network.DataObjects
{
  public class VKNotification
  {
    private object _parsedParent;
    private object _parsedFeedback;

    [JsonConverter(typeof (StringEnumConverter))]
    public VKNotification.NotificationType type { get; set; }

    public int date { get; set; }

    public object parent { get; set; }

    public object feedback { get; set; }

    public object reply { get; set; }

    public object ParsedParent
    {
      get
      {
        if (this._parsedParent != null)
          return this._parsedParent;
        if (this.parent == null)
          return (object) null;
        string str = this.parent.ToString();
        switch (this.type)
        {
          case VKNotification.NotificationType.mention_comments:
          case VKNotification.NotificationType.comment_post:
          case VKNotification.NotificationType.like_post:
          case VKNotification.NotificationType.copy_post:
            this._parsedParent = (object) JsonConvert.DeserializeObject<VKWallPost>(str);
            break;
          case VKNotification.NotificationType.comment_photo:
          case VKNotification.NotificationType.like_photo:
          case VKNotification.NotificationType.copy_photo:
          case VKNotification.NotificationType.mention_comment_photo:
            this._parsedParent = (object) JsonConvert.DeserializeObject<VKPhoto>(str);
            break;
          case VKNotification.NotificationType.comment_video:
          case VKNotification.NotificationType.like_video:
          case VKNotification.NotificationType.copy_video:
          case VKNotification.NotificationType.mention_comment_video:
            this._parsedParent = (object) JsonConvert.DeserializeObject<VKVideoBase>(str);
            break;
          case VKNotification.NotificationType.reply_comment:
          case VKNotification.NotificationType.reply_comment_photo:
          case VKNotification.NotificationType.reply_comment_video:
          case VKNotification.NotificationType.reply_comment_market:
          case VKNotification.NotificationType.like_comment:
          case VKNotification.NotificationType.like_comment_photo:
          case VKNotification.NotificationType.like_comment_video:
          case VKNotification.NotificationType.like_comment_topic:
            this._parsedParent = (object) JsonConvert.DeserializeObject<VKComment>(str);
            break;
          case VKNotification.NotificationType.reply_topic:
            VKTopic vkTopic = JsonConvert.DeserializeObject<VKTopic>(str);
            vkTopic.id = vkTopic.id;
            this._parsedParent = (object) vkTopic;
            break;
        }
        return this._parsedParent;
      }
    }

    public object ParsedFeedback
    {
      get
      {
        if (this._parsedFeedback != null)
          return this._parsedFeedback;
        string str = this.feedback.ToString();
        switch (this.type)
        {
          case VKNotification.NotificationType.follow:
          case VKNotification.NotificationType.friend_accepted:
          case VKNotification.NotificationType.like_post:
          case VKNotification.NotificationType.like_comment:
          case VKNotification.NotificationType.like_photo:
          case VKNotification.NotificationType.like_video:
          case VKNotification.NotificationType.like_comment_photo:
          case VKNotification.NotificationType.like_comment_video:
          case VKNotification.NotificationType.like_comment_topic:
            this._parsedFeedback = (object) JsonConvert.DeserializeObject<VKCountedItemsObject<FeedbackUser>>(str).items;
            break;
          case VKNotification.NotificationType.mention:
          case VKNotification.NotificationType.wall:
          case VKNotification.NotificationType.wall_publish:
            this._parsedFeedback = (object) JsonConvert.DeserializeObject<VKWallPost>(str);
            break;
          case VKNotification.NotificationType.mention_comments:
          case VKNotification.NotificationType.comment_post:
          case VKNotification.NotificationType.comment_photo:
          case VKNotification.NotificationType.comment_video:
          case VKNotification.NotificationType.reply_comment:
          case VKNotification.NotificationType.reply_comment_photo:
          case VKNotification.NotificationType.reply_comment_video:
          case VKNotification.NotificationType.reply_comment_market:
          case VKNotification.NotificationType.reply_topic:
          case VKNotification.NotificationType.mention_comment_photo:
          case VKNotification.NotificationType.mention_comment_video:
            this._parsedFeedback = (object) JsonConvert.DeserializeObject<VKComment>(str);
            break;
          case VKNotification.NotificationType.copy_post:
          case VKNotification.NotificationType.copy_photo:
          case VKNotification.NotificationType.copy_video:
            this._parsedFeedback = (object) JsonConvert.DeserializeObject<VKCountedItemsObject<FeedbackCopyInfo>>(str).items;
            break;
        }
        if (this._parsedFeedback != null)
          return this._parsedFeedback;
        return this.parent == null ? (object) "" : (object) this.parent.ToString();
      }
    }

    public enum NotificationType
    {
      follow,
      friend_accepted,
      mention,
      mention_comments,
      wall,
      wall_publish,
      comment_post,
      comment_photo,
      comment_video,
      reply_comment,
      reply_comment_photo,
      reply_comment_video,
      reply_comment_market,
      reply_topic,
      like_post,
      like_comment,
      like_photo,
      like_video,
      like_comment_photo,
      like_comment_video,
      like_comment_topic,
      copy_post,
      copy_photo,
      copy_video,
      mention_comment_photo,
      mention_comment_video,
    }
  }
}
