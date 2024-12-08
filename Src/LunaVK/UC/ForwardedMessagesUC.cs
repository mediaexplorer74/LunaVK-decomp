// Decompiled with JetBrains decompiler
// Type: App1uwp.UC.ForwardedMessagesUC
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
using App1uwp.Library;
using App1uwp.Network.DataObjects;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

#nullable disable
namespace App1uwp.UC
{
  public sealed class ForwardedMessagesUC : UserControl, IComponentConnector
  {
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Border brd;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Grid headerGrid;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private StackPanel MainContent;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Border _brd;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBlock text;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private ImageBrush img;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public ForwardedMessagesUC()
    {
      this.InitializeComponent();
      this._brd.LetsRound();
    }

    public ForwardedMessagesUC(VKWallPost post, double width)
      : this()
    {
      ScrollableTextBlock scrollableTextBlock = new ScrollableTextBlock();
      scrollableTextBlock.Foreground = (Brush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "TextColorContent"];
      scrollableTextBlock.Text = post.text;
      scrollableTextBlock.FontSize = 20.0;
      ((FrameworkElement) scrollableTextBlock).put_Margin(new Thickness(10.0, 0.0, 10.0, 0.0));
      this.Do(post);
      ((ICollection<UIElement>) ((Panel) this.MainContent).Children).Add((UIElement) scrollableTextBlock);
      if (post.attachments == null)
        return;
      AttachmentPresenter attachmentPresenter = new AttachmentPresenter(post.attachments, width - 20.0);
      ((FrameworkElement) attachmentPresenter).put_Margin(new Thickness(10.0, 0.0, 10.0, 0.0));
      ((ICollection<UIElement>) ((Panel) this.MainContent).Children).Add((UIElement) attachmentPresenter);
    }

    public ForwardedMessagesUC(VKMessage msg, double width)
      : this()
    {
      ((FrameworkElement) this).put_DataContext((object) msg);
      ScrollableTextBlock scrollableTextBlock = new ScrollableTextBlock();
      scrollableTextBlock.Foreground = (Brush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "TextColorContent"];
      scrollableTextBlock.Text = msg.body;
      scrollableTextBlock.FontSize = 20.0;
      ((FrameworkElement) scrollableTextBlock).put_Margin(new Thickness(10.0, 0.0, 10.0, 0.0));
      VKBaseDataForGroupOrUser cachedUser = UsersService.Instance.GetCachedUser((long) msg.user_id);
      string uriString = cachedUser.photo_50;
      if (string.IsNullOrEmpty(uriString))
        uriString = cachedUser.photo_100;
      if (string.IsNullOrEmpty(uriString))
        uriString = cachedUser.photo_200;
      this.img.put_ImageSource((ImageSource) new BitmapImage(new Uri(uriString)));
      this.text.put_Text(cachedUser.Title);
      ((ICollection<UIElement>) ((Panel) this.MainContent).Children).Add((UIElement) scrollableTextBlock);
      if (msg.attachments != null)
      {
        AttachmentPresenter attachmentPresenter = new AttachmentPresenter(msg.attachments, width - 20.0);
        ((FrameworkElement) attachmentPresenter).put_Margin(new Thickness(10.0, 0.0, 10.0, 0.0));
        ((ICollection<UIElement>) ((Panel) this.MainContent).Children).Add((UIElement) attachmentPresenter);
      }
      if (msg.fwd_messages == null)
        return;
      foreach (VKMessage fwdMessage in msg.fwd_messages)
      {
        ForwardedMessagesUC forwardedMessagesUc = new ForwardedMessagesUC(fwdMessage, width);
        Thickness margin = ((FrameworkElement) forwardedMessagesUc).Margin with
        {
          Left = 10.0
        };
        ((FrameworkElement) forwardedMessagesUc).put_Margin(margin);
        ((ICollection<UIElement>) ((Panel) this.MainContent).Children).Add((UIElement) forwardedMessagesUc);
      }
    }

    private async void Do(VKWallPost post)
    {
      int owner = 0;
      owner = post.from_id == 0 ? post.owner_id : post.from_id;
      Grid headerGrid = this.headerGrid;
      WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(((UIElement) headerGrid).add_Tapped), new Action<EventRegistrationToken>(((UIElement) headerGrid).remove_Tapped), (TappedEventHandler) ((s, e) =>
      {
        NavigatorImpl.Instance.NavigateToProfilePage((long) owner);
        e.put_Handled(true);
      }));
      await UsersService.Instance.GetUsers(new List<int>()
      {
        owner
      });
      VKBaseDataForGroupOrUser u = UsersService.Instance.GetCachedUser((long) owner);
      this.img.put_ImageSource((ImageSource) new BitmapImage(new Uri(u.photo_200)));
      this.text.put_Text(u.Title);
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///UC/ForwardedMessagesUC.xaml"), (ComponentResourceLocation) 0);
      this.brd = (Border) ((FrameworkElement) this).FindName("brd");
      this.headerGrid = (Grid) ((FrameworkElement) this).FindName("headerGrid");
      this.MainContent = (StackPanel) ((FrameworkElement) this).FindName("MainContent");
      this._brd = (Border) ((FrameworkElement) this).FindName("_brd");
      this.text = (TextBlock) ((FrameworkElement) this).FindName("text");
      this.img = (ImageBrush) ((FrameworkElement) this).FindName("img");
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void Connect(int connectionId, object target) => this._contentLoaded = true;
  }
}
