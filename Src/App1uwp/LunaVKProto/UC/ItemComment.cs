// Decompiled with JetBrains decompiler
// Type: App1uwp.UC.ItemComment
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
using App1uwp.Network.DataObjects;
using App1uwp.Utils;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

#nullable disable
namespace App1uwp.UC
{
  public sealed class ItemComment : UserControl, IComponentConnector
  {
    public static readonly DependencyProperty DataProperty = DependencyProperty.Register(nameof (Data), typeof (object), typeof (ItemComment), new PropertyMetadata((object) null, new PropertyChangedCallback(ItemComment.OnDataChanged)));
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Border _brd;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBlock textBlockUserOrGroupName;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private StackPanel MainContent;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBlock textBlockDate;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBlock _textblockLikes;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private ImageBrush imageUserOrGroup;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public ItemComment()
    {
      this.InitializeComponent();
      this._brd.LetsRound();
    }

    public object Data
    {
      get => ((DependencyObject) this).GetValue(ItemComment.DataProperty);
      set => ((DependencyObject) this).SetValue(ItemComment.DataProperty, value);
    }

    private static void OnDataChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
      ((ItemComment) obj).ProcessData();
    }

    private VKComment DataVM => this.Data as VKComment;

    private void ProcessData()
    {
      ((ICollection<UIElement>) ((Panel) this.MainContent).Children).Clear();
      if (this.Data == null)
        return;
      this.GetNamePicAndSex(this.DataVM.from_id, out string _, out string _, out bool _);
      if (!string.IsNullOrEmpty(this.DataVM.text))
        ((ICollection<UIElement>) ((Panel) this.MainContent).Children).Add((UIElement) new ScrollableTextBlock()
        {
          FullOnly = true,
          Foreground = (Brush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "TextColorContent"],
          Text = this.DataVM.text,
          FontSize = (double) ((IDictionary<object, object>) Application.Current.Resources)[(object) "FontSizeContent"]
        });
      if (this.DataVM.attachments != null)
        ((ICollection<UIElement>) ((Panel) this.MainContent).Children).Add((UIElement) new AttachmentPresenter(this.DataVM.attachments, 100.0));
      this.textBlockDate.put_Text(UIStringFormatterHelper.FormatDateTimeForUI(this.DataVM.date));
      if (this.DataVM.likes.count > 0)
        this._textblockLikes.put_Text(this.DataVM.likes.count.ToString());
      else
        ((UIElement) this._textblockLikes).put_Visibility((Visibility) 1);
      if (this.DataVM.reply_to_user == 0L)
        this.textBlockUserOrGroupName.put_Text(this.DataVM.User.Title);
      else
        this.textBlockUserOrGroupName.put_Text(this.DataVM.User.Title + " ответил " + this.DataVM._replyToUserDat);
      this.imageUserOrGroup.put_ImageSource((ImageSource) new BitmapImage(new Uri(this.DataVM.User.photo_50)));
    }

    private void GetNamePicAndSex(
      long userOrGroupId,
      out string name,
      out string pic,
      out bool isMale)
    {
      name = "";
      pic = "";
      isMale = false;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///UC/ItemComment.xaml"), (ComponentResourceLocation) 0);
      this._brd = (Border) ((FrameworkElement) this).FindName("_brd");
      this.textBlockUserOrGroupName = (TextBlock) ((FrameworkElement) this).FindName("textBlockUserOrGroupName");
      this.MainContent = (StackPanel) ((FrameworkElement) this).FindName("MainContent");
      this.textBlockDate = (TextBlock) ((FrameworkElement) this).FindName("textBlockDate");
      this._textblockLikes = (TextBlock) ((FrameworkElement) this).FindName("_textblockLikes");
      this.imageUserOrGroup = (ImageBrush) ((FrameworkElement) this).FindName("imageUserOrGroup");
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void Connect(int connectionId, object target) => this._contentLoaded = true;
  }
}
