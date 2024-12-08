// Decompiled with JetBrains decompiler
// Type: App1uwp.Library.OutboundForwardedMessages
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.DataObjects;
using App1uwp.Utils;
using System;
using System.Collections.Generic;

#nullable disable
namespace App1uwp.Library
{
  public class OutboundForwardedMessages : IOutboundAttachment
  {
    public List<VKMessage> Messages { get; private set; }

    public OutboundForwardedMessages(List<VKMessage> messages) => this.Messages = messages;

    public bool IsUploadAttachment => false;

    public string Title => this.Messages.Count.ToString();

    public string Subtitle
    {
      get
      {
        return UIStringFormatterHelper.FormatNumberOfSomething(this.Messages.Count, LocalizedStrings.GetString("OneMessageFrm"), LocalizedStrings.GetString("TwoFourMessagesFrm"), LocalizedStrings.GetString("FiveMessagesFrm"), false);
      }
    }

    public double Width => 133.0;

    public double Height => 100.0;

    public void Upload(Action completionCallback, Action<double> progressCallback = null)
    {
      completionCallback();
    }
  }
}
