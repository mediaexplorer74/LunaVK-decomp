
// Type: App1uwp.TestNotificationsPanel
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
using App1uwp.Library;
using App1uwp.Network.DataObjects;
using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;

#nullable disable
namespace App1uwp
{
    public sealed partial class TestNotificationsPanel : PageBase
    {
        private string[] _names;
        private DispatcherTimer[] _timers;
        private int ProfileImageWidth;
        private int ProfileImageHeight;
        private Thickness ProfileImageMargin;
      

        public TestNotificationsPanel()
        {
            this.InitializeComponent();
            this._names = new string[(int)((RangeBase)this.slUsers).Maximum];
            this._timers = new DispatcherTimer[(int)((RangeBase)this.slUsers).Maximum];
            for (int index = 0; index < (int)((RangeBase)this.slUsers).Maximum; ++index)
            {
                this._names[index] = "User #" + (object)(index + 1);
                this._timers[index] = new DispatcherTimer();
                DispatcherTimer timer = this._timers[index];
                WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(timer.add_Tick), new Action<EventRegistrationToken>(timer.remove_Tick), new EventHandler<object>(this.TestNotificationsPanel_Tick));
                this._timers[index].Interval = (TimeSpan.FromMilliseconds(((RangeBase)this.slInterv).Value * 1000.0 + (double)(index * 10)));
            }
            TestNotificationsPanel notificationsPanel1 = this;
            WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(((FrameworkElement)notificationsPanel1).add_Unloaded), new Action<EventRegistrationToken>(((FrameworkElement)notificationsPanel1).remove_Unloaded), new RoutedEventHandler(this.TestNotificationsPanel_Unloaded));
            TestNotificationsPanel notificationsPanel2 = this;
            WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(((FrameworkElement)notificationsPanel2).add_Loaded), new Action<EventRegistrationToken>(((FrameworkElement)notificationsPanel2).remove_Loaded), new RoutedEventHandler(this.TestNotificationsPanel_Loaded));
        }

        private void TestNotificationsPanel_Loaded(object sender, RoutedEventArgs e)
        {
            this.UpdateProfilePhoto(490.0, 1.3);
        }

        private void UpdateProfilePhoto(double width, double ratio)
        {
            double height1 = width / ratio;
            VKProfileBase.CropPhoto cropPhoto = new VKProfileBase.CropPhoto();
            cropPhoto.crop = new VKProfileBase.CropPhoto.CropRect()
            {
                x = 9.4,
                y = 5.4,
                x2 = 80.9,
                y2 = 77.5
            };
            cropPhoto.photo = new VKPhoto()
            {
                width = 1620,
                height = 2160
            };
            cropPhoto.rect = new VKProfileBase.CropPhoto.CropRect()
            {
                x = 14.5,
                y = 13.0,
                x2 = 93.5,
                y2 = 71.9
            };
            if (cropPhoto != null)
            {
                bool flag = true;
                VKPhoto photo = cropPhoto.photo;
                double num1 = photo.height > 0 ? (double)photo.width / (double)photo.height : 1.0;
                double width1 = width;
                double height2 = width1 / num1;
                if (num1 > ratio)
                {
                    height2 = height1;
                    width1 = height1 * num1;
                    flag = false;
                }
                this.ProfileImageWidth = (int)width1;
                this.ProfileImageHeight = (int)height2;

                Rect croppingRectangle1 = cropPhoto.crop.GetCroppingRectangle(width1, height2);
                Rect croppingRectangle2 = cropPhoto.rect.GetCroppingRectangle(croppingRectangle1.Width, 
                    croppingRectangle1.Height);

                croppingRectangle2.X += croppingRectangle1.X;
                croppingRectangle2.Y += croppingRectangle1.Y;
                double num2 = croppingRectangle2.X + croppingRectangle2.Width / 2.0;
                double num3 = croppingRectangle2.Y + croppingRectangle2.Height;
                if (flag)
                {
                    double num4 = croppingRectangle2.Height <= height1 ? 2.0 : 2.56;
                    double num5 = num3 - croppingRectangle2.Height - croppingRectangle2.Height / num4;
                    double val1 = height1 / 2.0 - num5;
                    if (croppingRectangle2.Height > height1 && num5 - croppingRectangle2.Height / 2.0 >= 0.0)
                        val1 = -croppingRectangle2.Y;
                    double top = Math.Min(0.0, Math.Max(val1, height1 - height2));
                    this.ProfileImageMargin = new Thickness(0.0, top, 0.0, 0.0);
                    this.ProfileImageClipRect.Rect = new Rect(0.0, -(top + 1.0), width, height1 + 1.0);
                }
                else
                {
                    double left = Math.Min(0.0, Math.Max(width / 2.0 - num2, width - width1));
                    this.ProfileImageMargin = new Thickness(left, 0.0, 0.0, 0.0);
                    this.ProfileImageClipRect.Rect = new Rect(-(left + 1.0), 0.0, width + 1.0, height1);
                }
            }
            else
            {
                this.ProfileImageWidth = (int)width;
                this.ProfileImageHeight = (int)height1;
            }
          this.BackGrid.Width = ((double)this.ProfileImageWidth);
          this.BackGrid.Height = (height1);
          this.image.Margin = (this.ProfileImageMargin);

            this.Temp.Text = (string.Format("ProfileImageWidth {0} ProfileImageHeight {1} requiredHeight {2}", 
                (object)this.ProfileImageWidth, (object)this.ProfileImageHeight, (object)height1));

            this.Temp2.Text = (string.Format("Rect.Left {0} Rect.Top {1} Rect.X {2} Rect.Y {3}", 
                (object)this.ProfileImageClipRect.Rect.Left,
                (object)this.ProfileImageClipRect.Rect.Top, 
                (object)this.ProfileImageClipRect.Rect.X,
                (object)this.ProfileImageClipRect.Rect.Y));
        }

        public Rect GetCroppingRectangle(
          double width,
          double height,
          double x,
          double y,
          double x2,
          double y2)
        {
            double x1 = x * width / 100.0;
            double num1 = x2 * width / 100.0;
            double y1 = y * height / 100.0;
            double num2 = y2 * height / 100.0;
            double width1 = num1 - x1;
            double height1 = num2 - y1;
            return new Rect((double)(int)x1, (double)(int)y1, (double)(int)width1, (double)(int)height1);
        }

        private void TestNotificationsPanel_Unloaded(object sender, RoutedEventArgs e)
        {
            for (int index = 0; index < (int)((RangeBase)this.slUsers).Maximum; ++index)
                this._timers[index].Stop();
        }

        private void TestNotificationsPanel_Tick(object sender, object e)
        {
            DispatcherTimer t = sender as DispatcherTimer;
            int timerIndex = this.GetTimerIndex(t);
            MessengerStateManager.Instance.HandleInAppNotification(this._names[timerIndex], this.GenerateText(), timerIndex, 0, "https://vk.com/images/camera_50.png");
            t.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (Window.Current.Content as CustomFrame).NotificationsPanel.AddAndShowNotification("https://pp.userapi.com/c840334/v840334195/26377/o3km3h8kJuo.jpg", "SberCat", "LOL", (Action)null);
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
        }

        private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            Slider slider = sender as Slider;
            for (int index = 0; index < (int)((RangeBase)this.slUsers).Maximum; ++index)
            {
                if (index > (int)((RangeBase)slider).Value)
                    this._timers[index].Stop();
                else if (!this._timers[index].IsEnabled)
                    this._timers[index].Start();
            }
        }

        private void Slider_ValueChanged2(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (this.slUsers == null)
                return;
            Slider slider = sender as Slider;
            for (int index = 0; index < (int)((RangeBase)this.slUsers).Maximum; ++index)
                this._timers[index].put_Interval(TimeSpan.FromMilliseconds(((RangeBase)slider).Value * 1000.0 + (double)(index * 10)));
        }

        private void Slider_ValueChanged3(object sender, RangeBaseValueChangedEventArgs e)
        {
        }

        private string GenerateText()
        {
            string text = "";
            for (int index = 0; (double)index < ((RangeBase)this.slLen).Value; ++index)
                text += index.ToString();
            return text;
        }

        private int GetTimerIndex(DispatcherTimer t)
        {
            int timerIndex = 0;
            DispatcherTimer[] timers = this._timers;
            for (int index = 0; index < timers.Length && timers[index] != t; ++index)
                ++timerIndex;
            return timerIndex;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //
        }

    }
}
