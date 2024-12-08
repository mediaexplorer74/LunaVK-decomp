// Decompiled with JetBrains decompiler
// Type: App1uwp.Utils.ThumbAttachment
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

#nullable disable
namespace App1uwp.Utils
{
  public class ThumbAttachment
  {
    public double Width { get; set; }

    public double Height { get; set; }

    public double CalcWidth { get; set; }

    public double CalcHeight { get; set; }

    public bool LastColumn { get; set; }

    public bool LastRow { get; set; }

    internal double getRatio() => this.Width / this.Height;

    internal void SetViewSize(double width, double height, bool lastColumn, bool lastRow)
    {
      this.CalcWidth = width;
      this.CalcHeight = height;
      this.LastColumn = lastColumn;
      this.LastRow = lastRow;
    }
  }
}
