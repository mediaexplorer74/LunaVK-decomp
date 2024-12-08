// Decompiled with JetBrains decompiler
// Type: App1uwp.AboutPage
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
using App1uwp.Library;
using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Shapes;

#nullable disable
namespace App1uwp
{
    public sealed partial class AboutPage : PageBase
    {
        
        public AboutPage()
        {
            this.InitializeComponent();
            AboutPage aboutPage = this;
            WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(((FrameworkElement)aboutPage).add_Loaded), new Action<EventRegistrationToken>(((FrameworkElement)aboutPage).remove_Loaded), (RoutedEventHandler)((s, e) =>
            {
                (Window.Current.Content as CustomFrame).HeaderWithMenu.SetTitle("О программе");
                double num = 0.0;
                if (((UIElement)(Window.Current.Content as CustomFrame).HeaderWithMenu).Visibility == null)
                    num = (Window.Current.Content as CustomFrame).HeaderWithMenu.HeaderHeight;
                this.Offset.Height = num;
            }));
            PackageVersion version = Package.Current.Id.Version;

            this.ver.Text = (string.Format("Версия: {0}.{1}.{2}.{3}", (object)version.Major, 
                (object)version.Minor, (object)version.Build, (object)version.Revision));

            this.api.Text = (string.Format("Версия API: {0}", (object)Constants.API_VERSION));
        }

        private void Border_Tapped(object sender, TappedRoutedEventArgs e)
        {
            NavigatorImpl.Instance.NavigateToProfilePage(-154148777L);
        }

        private void Border_Tapped_1(object sender, TappedRoutedEventArgs e)
        {
            Launcher.LaunchUriAsync(new Uri("https://m.vk.com/privacy"));
        }

        private void Border_Tapped_2(object sender, TappedRoutedEventArgs e)
        {
            Launcher.LaunchUriAsync(new Uri("https://m.vk.com/terms"));
        }

        
    }
}
