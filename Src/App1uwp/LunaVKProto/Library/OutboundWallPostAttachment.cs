// Decompiled with JetBrains decompiler
// Type: App1uwp.Library.OutboundWallPostAttachment
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.DataObjects;
using System;

#nullable disable
namespace App1uwp.Library
{
  public class OutboundWallPostAttachment : IOutboundAttachment
  {
    public VKBaseDataForPostOrNews _wallPost;

    public OutboundWallPostAttachment(VKBaseDataForPostOrNews post) => this._wallPost = post;

    public string IconSource => "\uE8F3";

    public string Title => "Запись на стене";

    public string Subtitle => this._wallPost != null ? this._wallPost.text : (string) null;

    public override string ToString()
    {
      return string.Format("{0}{1}_{2}", (object) "wall", (object) this._wallPost.OwnerId, (object) this._wallPost.PostId);
    }

    public void Upload(Action completionCallback, Action<double> progressCallback = null)
    {
      completionCallback();
    }

    public bool IsUploadAttachment => false;
  }
}
