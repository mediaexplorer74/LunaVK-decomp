// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.ViewModels.ConversationAvatarViewModel
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.Enums;
using Windows.Foundation;

#nullable disable
namespace App1uwp.Network.ViewModels
{
  public class ConversationAvatarViewModel : ViewModelBase
  {
    private int _usersCount;
    public bool Online;
    public VKPlatform platform;
    public string online_app;

    public double img_height_width { get; set; }

    public ConversationAvatarViewModel(double hw) => this.img_height_width = hw;

    public string UIChatImage1Url { get; set; }

    public string UIChatImage2Url { get; set; }

    public string UIChatImage3Url { get; set; }

    public string UIChatImage4Url { get; set; }

    public Rect Image1Rect { get; set; }

    public Rect Image2Rect { get; set; }

    public Rect Image3Rect { get; set; }

    public Rect Image4Rect { get; set; }

    public double Image1Margin { get; set; }

    public double Image2Margin { get; set; }

    public double Image3Margin { get; set; }

    public double Image4Margin { get; set; }

    public double Image1MarginY { get; set; }

    public double Image2MarginY { get; set; }

    public double Image3MarginY { get; set; }

    public double Image4MarginY { get; set; }

    public int UsersCount
    {
      get => this._usersCount;
      set
      {
        if (this._usersCount == value)
          return;
        this._usersCount = value;
        switch (value)
        {
          case 2:
            double num1 = this.img_height_width / 4.0;
            this.Image1Rect = new Rect(0.0, 0.0, this.img_height_width / 2.0, this.img_height_width);
            this.Image1Margin = -num1;
            this.Image2Rect = new Rect(this.img_height_width / 2.0, 0.0, this.img_height_width / 2.0, this.img_height_width);
            this.Image2Margin = num1;
            break;
          case 3:
            double num2 = this.img_height_width / 4.0;
            this.Image1Rect = new Rect(0.0, 0.0, this.img_height_width / 2.0, this.img_height_width / 2.0);
            this.Image1Margin = -num2;
            this.Image1MarginY = -num2;
            this.Image2Rect = new Rect(this.img_height_width / 2.0, 0.0, this.img_height_width / 2.0, this.img_height_width / 2.0);
            this.Image2Margin = num2;
            this.Image2MarginY = -num2;
            this.Image3Rect = new Rect(0.0, this.img_height_width / 2.0, this.img_height_width, this.img_height_width / 2.0);
            this.Image3Margin = 0.0;
            this.Image3MarginY = num2;
            break;
          case 4:
            double num3 = this.img_height_width / 4.0;
            this.Image1Rect = new Rect(0.0, 0.0, this.img_height_width / 2.0, this.img_height_width / 2.0);
            this.Image1Margin = -num3;
            this.Image1MarginY = -num3;
            this.Image2Rect = new Rect(this.img_height_width / 2.0, 0.0, this.img_height_width / 2.0, this.img_height_width / 2.0);
            this.Image2Margin = num3;
            this.Image2MarginY = -num3;
            this.Image3Rect = new Rect(0.0, this.img_height_width / 2.0, this.img_height_width, this.img_height_width / 2.0);
            this.Image3Margin = num3;
            this.Image3MarginY = num3;
            this.Image4Rect = new Rect(0.0, this.img_height_width / 2.0, this.img_height_width / 2.0, this.img_height_width / 2.0);
            this.Image4Margin = -num3;
            this.Image4MarginY = 0.0;
            break;
          default:
            this.Image1Rect = new Rect(0.0, 0.0, this.img_height_width, this.img_height_width);
            break;
        }
      }
    }

    public string PlatformIcon
    {
      get
      {
        if (!this.Online)
          return "";
        switch (this.platform)
        {
          case VKPlatform.Mobile:
            return "\uEE64";
          case VKPlatform.iPhone:
          case VKPlatform.iPad:
            return "\uEE77";
          case VKPlatform.Android:
            return "\uEE79";
          case VKPlatform.WindowsPhone:
          case VKPlatform.Windows:
            return "\uEE63";
          case VKPlatform.Web:
            return "\uF137";
          default:
            return "\uF137";
        }
      }
    }
  }
}
