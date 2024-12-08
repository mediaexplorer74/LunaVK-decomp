// Decompiled with JetBrains decompiler
// Type: App1uwp.App
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.App1uwp_WindowsPhone_XamlTypeInfo;
using App1uwp.Framework;
using App1uwp.Library;
using App1uwp.Network.DataObjects;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;

#nullable disable
namespace App1uwp
{
  public sealed class App : Application, IComponentConnector, IXamlMetadataProvider
  {
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;
    private XamlTypeInfoProvider _provider;

    public App()
    {
      this.InitializeComponent();
      App app1 = this;
      WindowsRuntimeMarshal.AddEventHandler<SuspendingEventHandler>(new Func<SuspendingEventHandler, EventRegistrationToken>(((Application) app1).add_Suspending), new Action<EventRegistrationToken>(((Application) app1).remove_Suspending), new SuspendingEventHandler(this.OnSuspending));
      App app2 = this;
      WindowsRuntimeMarshal.AddEventHandler<UnhandledExceptionEventHandler>(new Func<UnhandledExceptionEventHandler, EventRegistrationToken>(((Application) app2).add_UnhandledException), new Action<EventRegistrationToken>(((Application) app2).remove_UnhandledException), new UnhandledExceptionEventHandler(this.App_UnhandledException));
    }

    private void App_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
      e.put_Handled(true);
      MessageDialog messageDialog = new MessageDialog(e.Message + "\n" + e.Exception.StackTrace);
      messageDialog.put_Title("Unhandled error");
      IList<IUICommand> commands = messageDialog.Commands;
      UICommand uiCommand1 = new UICommand();
      uiCommand1.put_Label("Ok");
      uiCommand1.put_Id((object) 0);
      UICommand uiCommand2 = uiCommand1;
      commands.Add((IUICommand) uiCommand2);
      messageDialog.ShowAsync();
    }

    private void Current_VisibilityChanged(object sender, VisibilityChangedEventArgs e)
    {
      PushNotifications.Instance.HidePush(e.Visible);
    }

    protected virtual async void OnLaunched(LaunchActivatedEventArgs e)
    {
      if (Settings.Instance == null)
      {
        Settings.Instance = new Settings();
        await Settings.Instance.Load();
        this.ApplyTheme();
        this.ApplyScale();
      }
      ApplicationView a = ApplicationView.GetForCurrentView();
      a.SetDesiredBoundsMode((ApplicationViewBoundsMode) 1);
      StatusBar s = StatusBar.GetForCurrentView();
      s.put_BackgroundColor(new Color?(Colors.Transparent));
      if (!(Window.Current.Content is CustomFrame rootFrame))
      {
        IList<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
        ResourceDictionary resourceDictionary1 = new ResourceDictionary();
        resourceDictionary1.put_Source(new Uri("ms-appx:///Themes/Controls.xaml"));
        ResourceDictionary resourceDictionary2 = resourceDictionary1;
        mergedDictionaries.Add(resourceDictionary2);
        rootFrame = new CustomFrame();
        ApplicationExecutionState previousExecutionState = e.PreviousExecutionState;
        Window.Current.put_Content((UIElement) rootFrame);
        MessengerStateManager.Instance.Initialize();
        Window current = Window.Current;
        WindowsRuntimeMarshal.AddEventHandler<WindowVisibilityChangedEventHandler>(new Func<WindowVisibilityChangedEventHandler, EventRegistrationToken>(current.add_VisibilityChanged), new Action<EventRegistrationToken>(current.remove_VisibilityChanged), new WindowVisibilityChangedEventHandler(this.Current_VisibilityChanged));
        ApplicationView forCurrentView = ApplicationView.GetForCurrentView();
        // ISSUE: method pointer
        WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<ApplicationView, object>>(new Func<TypedEventHandler<ApplicationView, object>, EventRegistrationToken>(forCurrentView.add_VisibleBoundsChanged), new Action<EventRegistrationToken>(forCurrentView.remove_VisibleBoundsChanged), new TypedEventHandler<ApplicationView, object>((object) this, __methodptr(App_VisibleBoundsChanged)));
      }
      if (((ContentControl) rootFrame).Content == null)
      {
        if (Settings.Instance.IsAuthorized)
        {
          PushNotifications.Instance.UpdateDeviceRegistration();
          if (!this.NavigateFromArgs(e.Arguments))
            NavigatorImpl.Instance.NavigateToNewsFeed();
        }
        else
          rootFrame.Navigate(typeof (LoginPage));
        Window.Current.Activate();
      }
      else
        this.NavigateFromArgs(e.Arguments);
    }

    private void App_VisibleBoundsChanged(ApplicationView sender, object args)
    {
      ApplicationView applicationView = sender;
      CustomFrame content = Window.Current.Content as CustomFrame;
      if (applicationView.VisibleBounds.Bottom < ((FrameworkElement) content).ActualHeight - 10.0)
      {
        ((FrameworkElement) content).put_Margin(new Thickness(0.0, 0.0, 0.0, ((FrameworkElement) content).ActualHeight - applicationView.VisibleBounds.Bottom));
      }
      else
      {
        if (((FrameworkElement) content).Margin.Bottom <= 0.0)
          return;
        ((FrameworkElement) content).put_Margin(new Thickness(0.0));
      }
    }

    private bool NavigateFromArgs(string args)
    {
      if (string.IsNullOrEmpty(args))
        return false;
      Dictionary<string, string> queryString = this.ParseQueryString(args);
      if (queryString.ContainsKey("msg_id") && queryString.ContainsKey("uid"))
      {
        VKDialog vkDialog = new VKDialog()
        {
          message = new VKMessage()
        };
        vkDialog.message.id = int.Parse(queryString["msg_id"]);
        vkDialog.message.user_id = int.Parse(queryString["uid"]);
        if (queryString.ContainsKey("push_id"))
        {
          string[] strArray = queryString["push_id"].Split('_');
          if (strArray[0] == "chat")
          {
            int num = int.Parse(strArray[1]) - 2000000000;
            vkDialog.message.chat_id = num;
            vkDialog.message.user_id = 0;
          }
        }
        NavigatorImpl.Instance.NavigateToConversation(vkDialog.message.user_id, vkDialog.message.chat_id);
        return true;
      }
      if (queryString.ContainsKey("place"))
      {
        string str = queryString["place"];
        if (str.Substring(0, 4) == "wall")
        {
          string[] strArray = str.Remove(0, 4).Split('_');
          long ownerId = long.Parse(strArray[0]);
          NavigatorImpl.Instance.NavigateToWallPostComments(ulong.Parse(strArray[1]), ownerId);
          return true;
        }
      }
      return false;
    }

    protected virtual void OnActivated(IActivatedEventArgs args) => base.OnActivated(args);

    private void ApplyScale()
    {
      int uiScale = Settings.Instance.UIScale;
      if (uiScale == 100)
        return;
      this.DoScale("Double20", (double) uiScale);
      this.DoScale("Double40", (double) uiScale);
      this.DoScale("Double50", (double) uiScale);
      this.DoScale("Double55", (double) uiScale);
      this.DoScale("Double64", (double) uiScale);
      this.DoScale("Double72", (double) uiScale);
      this.DoScale("Double96", (double) uiScale);
      this.DoScale("FontSizeSmall", (double) uiScale);
      this.DoScale("FontSizeContent", (double) uiScale);
      this.DoScale("FontSizeLarge", (double) uiScale);
      this.DoScale("FontSizeExtraLarge", (double) uiScale);
    }

    private void ToLightTheme()
    {
      Color resource = (Color) ((IDictionary<object, object>) Application.Current.Resources)[(object) ("AccentColor" + (object) Settings.Instance.AccentColor)];
      ((IDictionary<object, object>) Application.Current.Resources)[(object) "AccentBrushHigh"] = (object) new SolidColorBrush(resource);
      ((IDictionary<object, object>) Application.Current.Resources)[(object) "AccentBrushMediumHigh"] = (object) new SolidColorBrush(this.ChangeColorBrightness(resource, 0.2f));
      ((IDictionary<object, object>) Application.Current.Resources)[(object) "AccentBrushMedium"] = (object) new SolidColorBrush(this.ChangeColorBrightness(resource, 0.4f));
      ((IDictionary<object, object>) Application.Current.Resources)[(object) "AccentBrushMediumLow"] = (object) new SolidColorBrush(this.ChangeColorBrightness(resource, 0.6f));
      ((IDictionary<object, object>) Application.Current.Resources)[(object) "AccentBrushLow"] = (object) new SolidColorBrush(this.ChangeColorBrightness(resource, 0.8f));
    }

    private void ToDarkTheme()
    {
      ((IDictionary<object, object>) Application.Current.Resources)[(object) "BaseBackgroundBrush"] = (object) new SolidColorBrush(Colors.Black);
      ((IDictionary<object, object>) Application.Current.Resources)[(object) "TextColorTitle"] = (object) new SolidColorBrush(Colors.White);
      ((IDictionary<object, object>) Application.Current.Resources)[(object) "TextColorContent"] = (object) new SolidColorBrush(Colors.White);
      ((IDictionary<object, object>) Application.Current.Resources)[(object) "ItemBackgroundBrush"] = (object) new SolidColorBrush(Color.FromArgb(byte.MaxValue, (byte) 30, (byte) 30, (byte) 30));
      Color resource1 = (Color) ((IDictionary<object, object>) Application.Current.Resources)[(object) "PhoneAccentColor"];
      ((IDictionary<object, object>) Application.Current.Resources)[(object) "ShadowColor"] = (object) Color.FromArgb(byte.MaxValue, resource1.R, resource1.G, resource1.B);
      Color resource2 = (Color) ((IDictionary<object, object>) Application.Current.Resources)[(object) ("AccentColor" + (object) Settings.Instance.AccentColor)];
      ((IDictionary<object, object>) Application.Current.Resources)[(object) "AccentBrushHigh"] = (object) new SolidColorBrush(this.ChangeColorBrightness(resource2, -0.3f));
      ((IDictionary<object, object>) Application.Current.Resources)[(object) "AccentBrushMediumHigh"] = (object) new SolidColorBrush(this.ChangeColorBrightness(resource2, -0.2f));
      ((IDictionary<object, object>) Application.Current.Resources)[(object) "AccentBrushMedium"] = (object) new SolidColorBrush(this.ChangeColorBrightness(resource2, -0.3f));
      ((IDictionary<object, object>) Application.Current.Resources)[(object) "AccentBrushMediumLow"] = (object) new SolidColorBrush(this.ChangeColorBrightness(resource2, -0.4f));
      ((IDictionary<object, object>) Application.Current.Resources)[(object) "AccentBrushLow"] = (object) new SolidColorBrush(this.ChangeColorBrightness(resource2, -0.5f));
      ((IDictionary<object, object>) Application.Current.Resources)[(object) "PhoneAccentColor"] = (object) resource2;
    }

    private Color ChangeColorBrightness(Color color, float correction)
    {
      float r1 = (float) color.R;
      float g1 = (float) color.G;
      float b1 = (float) color.B;
      float r2;
      float g2;
      float b2;
      if ((double) correction < 0.0)
      {
        correction = 1f + correction;
        r2 = r1 * correction;
        g2 = g1 * correction;
        b2 = b1 * correction;
      }
      else
      {
        r2 = ((float) byte.MaxValue - r1) * correction + r1;
        g2 = ((float) byte.MaxValue - g1) * correction + g1;
        b2 = ((float) byte.MaxValue - b1) * correction + b1;
      }
      return Color.FromArgb(color.A, (byte) r2, (byte) g2, (byte) b2);
    }

    private void DoScale(string field, double scale)
    {
      ((IDictionary<object, object>) Application.Current.Resources)[(object) field] = (object) ((double) ((IDictionary<object, object>) Application.Current.Resources)[(object) field] * scale / 100.0);
    }

    private void ApplyTheme()
    {
      switch (Settings.Instance.BackgroundType)
      {
        case Threelen.Off:
          this.ToLightTheme();
          break;
        case Threelen.On:
          this.ToDarkTheme();
          break;
        case Threelen.Third:
          if (this.RequestedTheme == 1)
          {
            this.ToDarkTheme();
            break;
          }
          this.ToLightTheme();
          break;
      }
    }

    private Dictionary<string, string> ParseQueryString(string uriString)
    {
      MatchCollection matchCollection = new Regex("(?<name>[^&=]+)=(?<value>[^&=]+)").Matches(uriString);
      Dictionary<string, string> queryString = new Dictionary<string, string>();
      for (int i = 0; i < matchCollection.Count; ++i)
      {
        Match match = matchCollection[i];
        queryString[match.Groups["name"].Value] = match.Groups["value"].Value;
      }
      return queryString;
    }

    private void OnSuspending(object sender, SuspendingEventArgs e)
    {
      e.SuspendingOperation.GetDeferral().Complete();
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void Connect(int connectionId, object target) => this._contentLoaded = true;

    public IXamlType GetXamlType(Type type)
    {
      if (this._provider == null)
        this._provider = new XamlTypeInfoProvider();
      return this._provider.GetXamlTypeByType(type);
    }

    public IXamlType GetXamlType(string fullName)
    {
      if (this._provider == null)
        this._provider = new XamlTypeInfoProvider();
      return this._provider.GetXamlTypeByName(fullName);
    }

    public XmlnsDefinition[] GetXmlnsDefinitions() => new XmlnsDefinition[0];
  }
}
