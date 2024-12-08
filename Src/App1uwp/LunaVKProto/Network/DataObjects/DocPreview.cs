// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.DataObjects.DocPreview
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using System.Collections.Generic;

#nullable disable
namespace App1uwp.Network.DataObjects
{
  public class DocPreview
  {
    public DocPreview.DocPreviewPhoto photo { get; set; }

    public DocPreview.DocPreviewVideo video { get; set; }

    public DocPreview.DocPreviewGraffiti graffiti { get; set; }

    public DocPreview.DocPreviewVoiceMessage audio_msg { get; set; }

    public sealed class DocPreviewPhoto
    {
      public List<DocPreview.DocPreviewPhoto.DocPreviewPhotoSize> sizes { get; set; }

      public sealed class DocPreviewPhotoSize
      {
        public string src { get; set; }

        public int width { get; set; }

        public int height { get; set; }

        public string type { get; set; }
      }
    }

    public class DocPreviewVideo
    {
      public string src { get; set; }

      public int width { get; set; }

      public int height { get; set; }

      public long file_size { get; set; }
    }

    public class DocPreviewGraffiti
    {
      public string src { get; set; }

      public int width { get; set; }

      public int height { get; set; }
    }

    public class DocPreviewVoiceMessage
    {
      public int duration { get; set; }

      public List<int> waveform { get; set; }

      public string link_ogg { get; set; }

      public string link_mp3 { get; set; }
    }
  }
}
