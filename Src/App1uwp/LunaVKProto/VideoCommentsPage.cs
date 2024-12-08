// Decompiled with JetBrains decompiler
// Type: App1uwp.VideoCommentsPage
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
using App1uwp.Library;
using App1uwp.Network;
using App1uwp.Network.DataObjects;
using App1uwp.UC;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

#nullable disable
namespace App1uwp
{
  public sealed class VideoCommentsPage : PageBase, IComponentConnector
  {
    private VKVideoBase data;
    private WebView web;
    private long ownerId;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private ExtendedListView2 MainScroll;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Grid ContentGrid;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private ScrollableTextBlock VideoDescription;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBlock VideoTitle;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Image ImageUri;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBlock Title;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBlock SubscribersCountStr;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public VideoCommentsPage()
    {
      this.InitializeComponent();
      VideoCommentsPage videoCommentsPage = this;
      WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(((FrameworkElement) videoCommentsPage).add_Loaded), new Action<EventRegistrationToken>(((FrameworkElement) videoCommentsPage).remove_Loaded), (RoutedEventHandler) ((s, e) => (Window.Current.Content as CustomFrame).HeaderWithMenu.SetTitle(LocalizedStrings.GetString("Video_Title"))));
    }

    protected override void HandleOnNavigatedTo(NavigationEventArgs e)
    {
      IDictionary<string, object> parameter = e.Parameter as IDictionary<string, object>;
      this.ownerId = (long) parameter["OwnerId"];
      long num = (long) (ulong) parameter["VideoId"];
      if (parameter.Keys.Contains("AccessKey"))
      {
        string str = (string) parameter["AccessKey"];
      }
      if (parameter.Keys.Contains("VideoContext"))
        this.data = (VKVideoBase) parameter["VideoContext"];
      this.RefreshHeader();
      this.VideoTitle.put_Text(this.data.title);
      this.VideoDescription.Text = this.data.description;
      this.GetVideoLinks();
    }

    private async void RefreshHeader()
    {
      VKBaseDataForGroupOrUser owner = UsersService.Instance.GetCachedUser(this.ownerId);
      if (owner == null)
      {
        await UsersService.Instance.GetUsers(new List<int>()
        {
          (int) this.ownerId
        });
        owner = UsersService.Instance.GetCachedUser(this.ownerId);
      }
      this.ImageUri.put_Source((ImageSource) new BitmapImage(new Uri(owner.photo_50)));
      this.Title.put_Text(owner.Title);
      if (!(owner is VKGroup))
        return;
      this.SubscribersCountStr.put_Text((owner as VKGroup).members_count.ToString());
    }

    private async void GetVideoLinks()
    {
      VKResponse<VKCountedItemsObject<VKVideoBase>> o = await RequestsDispatcher.GetResponse<VKCountedItemsObject<VKVideoBase>>("video.get", new Dictionary<string, string>()
      {
        ["videos"] = string.Format("{0}_{1}", (object) this.data.owner_id, (object) this.data.id)
      });
      if (o == null || o.response.count <= 0U)
        return;
      this.data.files = o.response.items[0].files;
      this.data.player = o.response.items[0].player;
      if (string.IsNullOrEmpty(this.data.player))
        return;
      this.web = new WebView();
      ((ICollection<UIElement>) ((Panel) this.ContentGrid).Children).Add((UIElement) this.web);
      this.web.Navigate(new Uri(this.data.player));
    }

    protected override void HandleOnNavigatingFrom(NavigatingCancelEventArgs e)
    {
      if (this.web == null)
        return;
      this.web.NavigateToString("");
      ((ICollection<UIElement>) ((Panel) this.ContentGrid).Children).Remove((UIElement) this.web);
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///VideoCommentsPage.xaml"), (ComponentResourceLocation) 0);
      this.MainScroll = (ExtendedListView2) ((FrameworkElement) this).FindName("MainScroll");
      this.ContentGrid = (Grid) ((FrameworkElement) this).FindName("ContentGrid");
      this.VideoDescription = (ScrollableTextBlock) ((FrameworkElement) this).FindName("VideoDescription");
      this.VideoTitle = (TextBlock) ((FrameworkElement) this).FindName("VideoTitle");
      this.ImageUri = (Image) ((FrameworkElement) this).FindName("ImageUri");
      this.Title = (TextBlock) ((FrameworkElement) this).FindName("Title");
      this.SubscribersCountStr = (TextBlock) ((FrameworkElement) this).FindName("SubscribersCountStr");
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void Connect(int connectionId, object target) => this._contentLoaded = true;
  }
}
