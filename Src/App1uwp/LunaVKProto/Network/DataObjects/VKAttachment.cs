// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.DataObjects.VKAttachment
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

#nullable disable
namespace App1uwp.Network.DataObjects
{
  public sealed class VKAttachment
  {
    public VKPhoto photo { get; set; }

    public VKVideoBase video { get; set; }

    public VKAudio audio { get; set; }

    public VKDocument doc { get; set; }

    public VKWallPost wall { get; set; }

    public VKComment wall_reply { get; set; }

    public VKSticker sticker { get; set; }

    public VKLink link { get; set; }

    public VKGift gift { get; set; }

    public VKPoll poll { get; set; }

    public VKNote note { get; set; }

    public VKMarket market { get; set; }

    public VKWiki page { get; set; }

    public VKGraffiti graffiti { get; set; }

    [JsonConverter(typeof (StringEnumConverter))]
    public VKAttachmentType type { get; set; }

    public override string ToString()
    {
      long num1 = 0;
      int num2 = 0;
      switch (this.type)
      {
        case VKAttachmentType.Photo:
          num1 = this.photo.owner_id;
          num2 = (int) this.photo.id;
          break;
        case VKAttachmentType.Video:
          num1 = this.video.owner_id;
          num2 = (int) this.video.id;
          break;
        case VKAttachmentType.Audio:
          num1 = (long) this.audio.owner_id;
          num2 = this.audio.id;
          break;
        case VKAttachmentType.Doc:
          num1 = this.doc.owner_id;
          num2 = (int) this.doc.id;
          break;
        case VKAttachmentType.Wall:
          num1 = (long) this.wall.owner_id;
          num2 = (int) this.wall.id;
          break;
      }
      return string.Format("{0}{1}_{2}", (object) this.type.ToString().ToLower(), (object) num1, (object) num2);
    }
  }
}
