﻿// Decompiled with JetBrains decompiler
// Type: XamlAnimatedGif.Decoding.UnknownExtensionTypeException
// Assembly: XamlAnimatedGif, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 02CD40CB-7FCE-4EFE-9D7D-B0095CDCA3E7
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\XamlAnimatedGif.dll

using System;

#nullable disable
namespace XamlAnimatedGif.Decoding
{
  public class UnknownExtensionTypeException : GifDecoderException
  {
    internal UnknownExtensionTypeException(string message)
      : base(message)
    {
    }

    internal UnknownExtensionTypeException(string message, Exception inner)
      : base(message, inner)
    {
    }
  }
}
