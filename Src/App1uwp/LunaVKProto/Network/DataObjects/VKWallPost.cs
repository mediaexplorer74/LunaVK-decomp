// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.DataObjects.VKWallPost
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.json;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

#nullable disable
namespace App1uwp.Network.DataObjects
{
  public class VKWallPost : VKBaseDataForPostOrNews, INotifyPropertyChanged
  {
    public uint id { get; set; }

    public int owner_id { get; set; }

    public int from_id { get; set; }

    public int created_by { get; set; }

    public int reply_owner_id { get; set; }

    public int reply_post_id { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool friends_only { get; set; }

    public VKPostSource post_source { get; set; }

    public int signer_id { get; set; }

    public List<VKWallPost> copy_history { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool can_pin { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool is_pinned { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool market_as_ads { get; set; }

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

    public void UpdateUI()
    {
      this.NotifyPropertyChanged("likes");
      this.NotifyPropertyChanged("LikesBrush");
    }

    public override long OwnerId => (long) this.owner_id;

    public override long PostId => (long) this.id;
  }
}
