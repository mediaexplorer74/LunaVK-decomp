
// Type: BackgroundAudio.AudioHeader
// Assembly: BackgroundAudio, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: BDE4D73A-D23E-4D0C-B342-DEA789080D59
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\BackgroundAudio.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation;
using Windows.Foundation.Metadata;

#nullable disable
namespace BackgroundAudio
{
 
  [Version(16777216)]
  [CompilerGenerated]
  [Activatable(16777216)]
  public sealed class AudioHeader : IAudioHeaderClass, IStringable
  {
    [MethodImpl(default)]
    public extern AudioHeader();

    public extern string artist { [MethodImpl(default)] get; [MethodImpl(default)] [param: In] set; }

    public extern string cover { [MethodImpl(default)] get; [MethodImpl(default)] [param: In] set; }

    public extern int duration { [MethodImpl(default)] get; [MethodImpl(default)] [param: In] set; }

    public extern string title { [MethodImpl(default)] get; [MethodImpl(default)] [param: In] set; }

    public extern string url { [MethodImpl(default)] get; [MethodImpl(default)] [param: In] set; }

    [MethodImpl(default)]
    extern string IStringable.ToString();
  }
}
