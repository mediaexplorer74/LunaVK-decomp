// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.DataObjects.VKComment
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using System.Collections.Generic;

#nullable disable
namespace App1uwp.Network.DataObjects
{
  public class VKComment
  {
    public long id { get; set; }

    public long from_id { get; set; }

    public long post_id { get; set; }

    public long owner_id { get; set; }

    public int date { get; set; }

    public string text { get; set; }

    public VKLikes likes { get; set; }

    public long reply_to_user { get; set; }

    public List<VKAttachment> attachments { get; set; }

    public long cid { get; set; }

    public long uid { get; set; }

    public string _replyToUserDat { get; set; }

    public VKBaseDataForGroupOrUser User { get; set; }
  }
}
