// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.DataObjects.VKNewsfeedPost
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.Enums;
using App1uwp.Network.json;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

#nullable disable
namespace App1uwp.Network.DataObjects
{
  public class VKNewsfeedPost : VKBaseDataForPostOrNews, INotifyPropertyChanged
  {
    [JsonConverter(typeof (StringEnumConverter))]
    public VKNewsfeedFilters type { get; set; }

    public long source_id { get; set; }

    public ulong post_id { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool final_post { get; set; }

    public long copy_owner_id { get; set; }

    public long copy_post_id { get; set; }

    public List<VKNewsfeedPost> copy_history { get; set; }

    [JsonConverter(typeof (UnixtimeToDateTimeConverter))]
    public DateTime copy_post_date { get; set; }

    public VKCountedItemsObject<VKPhoto> photos { get; set; }

    public VKCountedItemsObject<VKPhoto> photo_tags { get; set; }

    public List<VKNewsfeedPost.VKNotes> notes { get; set; }

    public VKCountedItemsObject<VKNewsfeedPost.VKFriends> friends { get; set; }

    public VKCountedItemsObject<VKVideoBase> video { get; set; }

    public int owner_id { get; set; }

    public void UpdateUI()
    {
      this.NotifyPropertyChanged("likes");
      this.NotifyPropertyChanged("LikesBrush");
    }

    public SolidColorBrush LikesBrush
    {
      get
      {
        return (SolidColorBrush) ((IDictionary<object, object>) Application.Current.Resources)[this.likes == null || !this.likes.user_likes ? (object) "TextColorSubContent" : (object) "AccentBrushHigh"];
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void NotifyPropertyChanged(string propertyName = null)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }

    public override long OwnerId => this.source_id;

    public override long PostId => (long) this.post_id;

    public class VKFriends
    {
      public long uid { get; set; }
    }

    public class VKNotes
    {
      public long id { get; set; }

      public long owner_id { get; set; }

      public string title { get; set; }

      public long comments { get; set; }
    }
  }
}
