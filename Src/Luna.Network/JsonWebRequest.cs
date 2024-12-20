﻿
// Type: Luna.Network.JsonWebRequest
// Assembly: Luna.Network, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: DCF3176C-D8EF-420C-B6C7-0188B16D444E
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\Luna.Network.winmd

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;

#nullable disable
namespace Luna.Network
{
  [Version(16777216)]
  [CompilerGenerated]
  [Activatable(16777216)]
  [Static(typeof (IJsonWebRequestStatic), 16777216)]
  public sealed class JsonWebRequest : IJsonWebRequestClass, IStringable
  {
    [MethodImpl(default)]
    public extern JsonWebRequest();

    [MethodImpl(default)]
    public static extern void Download(
      [In] string uri,
      [In] string destinationFile,
      [In] EventHandler<bool> resultCallback,
      [In] EventHandler<double> progressCallback);

    [MethodImpl(default)]
    public static extern IAsyncOperation<string> SendHTTPRequestAsync(
      [In] string requestURL,
      [In] IMapView1<string, string> parameters);

    [MethodImpl(default)]
    extern string IStringable.ToString();
  }
}
