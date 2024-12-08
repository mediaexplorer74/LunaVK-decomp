// Decompiled with JetBrains decompiler
// Type: App1uwp.UC.LoadingUC
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;

#nullable disable
namespace App1uwp.UC
{
  public sealed class LoadingUC : UserControl, IComponentConnector
  {
    public static readonly DependencyProperty TryAgainCommandProperty = DependencyProperty.Register(nameof (TryAgainCommand), typeof (Action), typeof (LoadingUC), new PropertyMetadata((object) null));
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Grid root;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private VisualStateGroup Common;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private VisualState Loading;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private VisualState Reloading;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private VisualState Blocked;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private VisualState Private;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private VisualState LoadingFailed;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private VisualState Loaded;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private StackPanel stackPanelNotLoaded;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private ProgressRing progressRing;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBlock textBlockLoadingStatus;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBlock textError;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Button buttonTryAgain;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public Action TryAgainCommand
    {
      get => (Action) ((DependencyObject) this).GetValue(LoadingUC.TryAgainCommandProperty);
      set => ((DependencyObject) this).SetValue(LoadingUC.TryAgainCommandProperty, (object) value);
    }

    public LoadingUC() => this.InitializeComponent();

    private void Grid_Loaded(object sender, RoutedEventArgs e)
    {
      ((FrameworkElement) (sender as Grid)).put_Height(((FrameworkElement) (Window.Current.Content as Frame)).ActualHeight / 2.0);
    }

    private void buttonTryAgain_Click(object sender, RoutedEventArgs e)
    {
      if (this.TryAgainCommand == null)
        return;
      this.TryAgainCommand();
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///UC/LoadingUC.xaml"), (ComponentResourceLocation) 0);
      this.root = (Grid) ((FrameworkElement) this).FindName("root");
      this.Common = (VisualStateGroup) ((FrameworkElement) this).FindName("Common");
      this.Loading = (VisualState) ((FrameworkElement) this).FindName("Loading");
      this.Reloading = (VisualState) ((FrameworkElement) this).FindName("Reloading");
      this.Blocked = (VisualState) ((FrameworkElement) this).FindName("Blocked");
      this.Private = (VisualState) ((FrameworkElement) this).FindName("Private");
      this.LoadingFailed = (VisualState) ((FrameworkElement) this).FindName("LoadingFailed");
      this.Loaded = (VisualState) ((FrameworkElement) this).FindName("Loaded");
      this.stackPanelNotLoaded = (StackPanel) ((FrameworkElement) this).FindName("stackPanelNotLoaded");
      this.progressRing = (ProgressRing) ((FrameworkElement) this).FindName("progressRing");
      this.textBlockLoadingStatus = (TextBlock) ((FrameworkElement) this).FindName("textBlockLoadingStatus");
      this.textError = (TextBlock) ((FrameworkElement) this).FindName("textError");
      this.buttonTryAgain = (Button) ((FrameworkElement) this).FindName("buttonTryAgain");
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          FrameworkElement frameworkElement = (FrameworkElement) target;
          WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(frameworkElement.add_Loaded), new Action<EventRegistrationToken>(frameworkElement.remove_Loaded), new RoutedEventHandler(this.Grid_Loaded));
          break;
        case 2:
          ButtonBase buttonBase = (ButtonBase) target;
          WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.buttonTryAgain_Click));
          break;
      }
      this._contentLoaded = true;
    }
  }
}
