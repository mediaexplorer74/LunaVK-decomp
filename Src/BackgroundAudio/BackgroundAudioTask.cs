// Decompiled with JetBrains decompiler
// Type: BackgroundAudio.BackgroundAudioTask
// Assembly: BackgroundAudio, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: BDE4D73A-D23E-4D0C-B342-DEA789080D59
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\BackgroundAudio.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.ApplicationModel.Background;
using Windows.Foundation;
using Windows.Foundation.Metadata;

#nullable disable
namespace BackgroundAudio
{
  [MarshalingBehavior]
  [Threading]
  [Version(16777216)]
  [CompilerGenerated]
  [Activatable(16777216)]
  public sealed class BackgroundAudioTask : IBackgroundTask, IStringable
  {
    [MethodImpl]
    public extern BackgroundAudioTask();

    [MethodImpl]
    public extern void Run([In] IBackgroundTaskInstance taskInstance);

    [MethodImpl]
    extern string IStringable.ToString();
  }
}
