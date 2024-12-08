// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.DataObjects.VKPoll
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.json;
using App1uwp.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

#nullable disable
namespace App1uwp.Network.DataObjects
{
  public class VKPoll
  {
    public long owner_id { get; set; }

    public long id { get; set; }

    [JsonConverter(typeof (UnixtimeToDateTimeConverter))]
    public DateTime created { get; set; }

    public string question { get; set; }

    public int votes { get; set; }

    public long answer_id { get; set; }

    public List<VKPollAnswers> answers { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool anonymous { get; set; }

    private string VotedCountStr
    {
      get
      {
        return UIStringFormatterHelper.FormatNumberOfSomething(this.votes, LocalizedStrings.GetString("Poll_OneVoteFrm"), LocalizedStrings.GetString("Poll_TwoFourVotesFrm"), LocalizedStrings.GetString("Poll_FiveVotesFrm"));
      }
    }

    private string PollTypeStr
    {
      get => LocalizedStrings.GetString(this.anonymous ? "Poll_AnonymousPoll" : "Poll_PublicPoll");
    }

    public string Description
    {
      get => string.Format("{0} · {1}", (object) this.PollTypeStr, (object) this.VotedCountStr);
    }
  }
}
