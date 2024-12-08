// Decompiled with JetBrains decompiler
// Type: App1uwp.PostCommentsPage
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
using App1uwp.Network.DataObjects;
using App1uwp.Network.ViewModels;
using App1uwp.VirtualUC;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Navigation;

#nullable disable
namespace App1uwp
{
  public sealed class PostCommentsPage : PageBase, IComponentConnector
  {
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private ExtendedListView2 MainScroll;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private ItemsBasePostUC _post;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public PostCommentsPage()
    {
      this.InitializeComponent();
      PostCommentsPage postCommentsPage = this;
      WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(((FrameworkElement) postCommentsPage).add_Loaded), new Action<EventRegistrationToken>(((FrameworkElement) postCommentsPage).remove_Loaded), (RoutedEventHandler) ((s, e) => (Window.Current.Content as CustomFrame).HeaderWithMenu.SetTitle("Запись на стене")));
    }

    protected override void HandleOnNavigatedTo(NavigationEventArgs e)
    {
      IDictionary<string, object> parameter = e.Parameter as IDictionary<string, object>;
      ulong postId = (ulong) parameter["PostId"];
      long ownerId = (long) parameter["PostOwnerId"];
      VKBaseDataForPostOrNews postData = (VKBaseDataForPostOrNews) null;
      if (parameter.Keys.Contains("WallPost"))
        postData = (VKBaseDataForPostOrNews) parameter["WallPost"];
      ((FrameworkElement) this).put_DataContext((object) new PostCommentsViewModel(postId, ownerId, postData));
      if (postData != null)
        this._post.DataPost = postData as VKWallPost;
      (((FrameworkElement) this).DataContext as PostCommentsViewModel).LoadData(true, new Action<bool>(this.CallBck));
    }

    private void CallBck(bool result)
    {
      if (!result)
        return;
      ((FrameworkElement) this._post).put_DataContext((object) (((FrameworkElement) this).DataContext as PostCommentsViewModel).WallPostData);
      this._post.DataPost = (((FrameworkElement) this).DataContext as PostCommentsViewModel).WallPostData as VKWallPost;
      VKComment vkComment = (((FrameworkElement) this).DataContext as PostCommentsViewModel).Items.LastOrDefault<VKComment>();
      if (vkComment == null)
        return;
      ((ListViewBase) this.MainScroll.GetListView).ScrollIntoView((object) vkComment);
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///PostCommentsPage.xaml"), (ComponentResourceLocation) 0);
      this.MainScroll = (ExtendedListView2) ((FrameworkElement) this).FindName("MainScroll");
      this._post = (ItemsBasePostUC) ((FrameworkElement) this).FindName("_post");
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void Connect(int connectionId, object target) => this._contentLoaded = true;
  }
}
