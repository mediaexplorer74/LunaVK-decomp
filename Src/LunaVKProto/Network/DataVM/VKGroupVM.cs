// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.DataVM.VKGroupVM
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.DataObjects;
using App1uwp.Utils;

#nullable disable
namespace App1uwp.Network.DataVM
{
  public class VKGroupVM : VKGroup
  {
    public string UIMembersCount
    {
      get
      {
        return UIStringFormatterHelper.FormatNumberOfSomething(this.members_count, LocalizedStrings.GetString("OneMemberFrm"), LocalizedStrings.GetString("TwoFourMembersFrm"), LocalizedStrings.GetString("FiveMembersFrm"));
      }
    }

    public string Inviter { get; set; }
  }
}
