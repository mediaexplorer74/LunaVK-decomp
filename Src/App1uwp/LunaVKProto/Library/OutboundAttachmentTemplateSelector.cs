// Decompiled with JetBrains decompiler
// Type: App1uwp.Library.OutboundAttachmentTemplateSelector
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace App1uwp.Library
{
  public class OutboundAttachmentTemplateSelector : ContentControl
  {
    protected virtual void OnContentChanged(object oldContent, object newContent)
    {
      base.OnContentChanged(oldContent, newContent);
      this.put_ContentTemplate(this.SelectTemplate(newContent, (DependencyObject) this));
    }

    private DataTemplate SelectTemplate(object item, DependencyObject container)
    {
      switch (item)
      {
        case OutboundPhotoAttachment _:
          return this.PhotoTemplate;
        case OutboundForwardedMessages _:
          return this.ForwardedMessageTemplate;
        default:
          return this.GenericIconTemplate;
      }
    }

    public DataTemplate PhotoTemplate { get; set; }

    public DataTemplate GeoTemplate { get; set; }

    public DataTemplate VideoTemplate { get; set; }

    public DataTemplate AudioTemplate { get; set; }

    public DataTemplate DocumentTemplate { get; set; }

    public DataTemplate GenericThumbTemplate { get; set; }

    public DataTemplate AddAttachmentTemplate { get; set; }

    public DataTemplate WallPostTemplate { get; set; }

    public DataTemplate GenericIconTemplate { get; set; }

    public DataTemplate ForwardedMessageTemplate { get; set; }
  }
}
