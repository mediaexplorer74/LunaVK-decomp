// Decompiled with JetBrains decompiler
// Type: App1uwp.LoginPage
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
using App1uwp.Network;
using App1uwp.Network.DataObjects;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.Web.Http;
using Windows.Web.Http.Filters;

#nullable disable
namespace App1uwp
{
  public sealed class LoginPage : Page, IComponentConnector
  {
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private WebView browser;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public LoginPage()
    {
      this.InitializeComponent();
      LoginPage loginPage = this;
      WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(((FrameworkElement) loginPage).add_Loaded), new Action<EventRegistrationToken>(((FrameworkElement) loginPage).remove_Loaded), new RoutedEventHandler(this.LoginPage_Loaded));
    }

    private void LoginPage_Loaded(object sender, RoutedEventArgs e)
    {
      (Window.Current.Content as CustomFrame).HeaderWithMenu.SuppressMenu(true);
    }

    private void buttonLogin_Click(object sender, RoutedEventArgs e)
    {
    }

    private void browser_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
    {
      if (args.Uri.AbsoluteUri.Contains("access_token="))
      {
        ((UIElement) this.browser).put_Visibility((Visibility) 1);
        string[] array = ((IEnumerable<string>) args.Uri.Fragment.Substring(1).Split('&')).ToArray<string>();
        string str = array[0].Split('=')[1];
        long num = long.Parse(array[2].Split('=')[1]);
        (Window.Current.Content as CustomFrame).HeaderWithMenu.SuppressMenu(false);
        Settings.Instance.auth = new AutorizationData();
        Settings.Instance.auth.AccessToken = str;
        Settings.Instance.auth.UserId = num;
        Settings.Instance.Save();
        Settings.Instance.GetBaseData();
        LongPollServerService.Instance.GetLongPollServer();
        (Window.Current.Content as Frame).Navigate(typeof (NewsPage));
      }
      else
      {
        if (!args.Uri.AbsoluteUri.Contains("error="))
          return;
        ((UIElement) this.browser).put_Visibility((Visibility) 1);
      }
    }

    private void browser_NavigationCompleted(
      WebView sender,
      WebViewNavigationCompletedEventArgs args)
    {
    }

    private void browser_NavigationFailed(object sender, WebViewNavigationFailedEventArgs e)
    {
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      HttpCookieManager cookieManager = new HttpBaseProtocolFilter().CookieManager;
      foreach (HttpCookie cookie in (IEnumerable<HttpCookie>) cookieManager.GetCookies(new Uri("https://www.vk.com/")))
        cookieManager.DeleteCookie(cookie);
      ((UIElement) this.browser).put_Visibility((Visibility) 0);
      this.browser.Navigate(new Uri(string.Format("https://oauth.{0}/authorize?client_id={1}&scope={2}&redirect_uri={3}&display=mobile&v={4}&response_type=token", Settings.Instance.UseProxy ? (object) Settings.Instance.ProxyAdress : (object) Constants.HostURL, (object) Constants.ApplicationID, (object) Constants.Scope, (object) Constants.Redirect, (object) Constants.API_VERSION)));
    }

    private void TextBlock_Tapped(object sender, TappedRoutedEventArgs e)
    {
      Launcher.LaunchUriAsync(new Uri("https://m.vk.com/terms"));
    }

    private void Border_Tapped(object sender, TappedRoutedEventArgs e)
    {
      (Window.Current.Content as Frame).Navigate(typeof (SettingsGeneralPage));
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///LoginPage.xaml"), (ComponentResourceLocation) 0);
      this.browser = (WebView) ((FrameworkElement) this).FindName("browser");
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          UIElement uiElement1 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement1.add_Tapped), new Action<EventRegistrationToken>(uiElement1.remove_Tapped), new TappedEventHandler(this.Border_Tapped));
          break;
        case 2:
          ButtonBase buttonBase = (ButtonBase) target;
          WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.Button_Click));
          break;
        case 3:
          WebView webView1 = (WebView) target;
          // ISSUE: method pointer
          WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<WebView, WebViewNavigationStartingEventArgs>>(new Func<TypedEventHandler<WebView, WebViewNavigationStartingEventArgs>, EventRegistrationToken>(webView1.add_NavigationStarting), new Action<EventRegistrationToken>(webView1.remove_NavigationStarting), new TypedEventHandler<WebView, WebViewNavigationStartingEventArgs>((object) this, __methodptr(browser_NavigationStarting)));
          WebView webView2 = (WebView) target;
          // ISSUE: method pointer
          WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<WebView, WebViewNavigationCompletedEventArgs>>(new Func<TypedEventHandler<WebView, WebViewNavigationCompletedEventArgs>, EventRegistrationToken>(webView2.add_NavigationCompleted), new Action<EventRegistrationToken>(webView2.remove_NavigationCompleted), new TypedEventHandler<WebView, WebViewNavigationCompletedEventArgs>((object) this, __methodptr(browser_NavigationCompleted)));
          WebView webView3 = (WebView) target;
          WindowsRuntimeMarshal.AddEventHandler<WebViewNavigationFailedEventHandler>(new Func<WebViewNavigationFailedEventHandler, EventRegistrationToken>(webView3.add_NavigationFailed), new Action<EventRegistrationToken>(webView3.remove_NavigationFailed), new WebViewNavigationFailedEventHandler(this.browser_NavigationFailed));
          break;
        case 4:
          UIElement uiElement2 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement2.add_Tapped), new Action<EventRegistrationToken>(uiElement2.remove_Tapped), new TappedEventHandler(this.TextBlock_Tapped));
          break;
      }
      this._contentLoaded = true;
    }
  }
}
