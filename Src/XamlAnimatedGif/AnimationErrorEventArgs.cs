
// Type: XamlAnimatedGif.AnimationErrorEventArgs
// Assembly: XamlAnimatedGif, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 02CD40CB-7FCE-4EFE-9D7D-B0095CDCA3E7
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\XamlAnimatedGif.dll

using System;

#nullable disable
namespace XamlAnimatedGif
{
  public class AnimationErrorEventArgs : EventArgs
  {
    public AnimationErrorEventArgs(object source, Exception exception, AnimationErrorKind kind)
    {
      this.Source = source;
      this.Exception = exception;
      this.Kind = kind;
    }

    public Exception Exception { get; private set; }

    public AnimationErrorKind Kind { get; private set; }

    public object Source { get; private set; }
  }
}
