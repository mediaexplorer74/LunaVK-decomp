// Decompiled with JetBrains decompiler
// Type: App1uwp.Library.ISupportLoadMore
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using System.Threading.Tasks;

#nullable disable
namespace App1uwp.Library
{
  public interface ISupportLoadMore
  {
    Task LoadData(bool reload = false);

    bool HasMoreItems { get; }
  }
}
