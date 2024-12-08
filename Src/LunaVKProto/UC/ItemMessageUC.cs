// Decompiled with JetBrains decompiler
// Type: App1uwp.UC.ItemMessageUC
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
using App1uwp.Library;
using App1uwp.Network.DataObjects;
using App1uwp.Network.DataVM;
using App1uwp.Network.Enums;
using App1uwp.Network.ViewModels;
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
using Windows.UI.Xaml.Shapes;

#nullable disable
namespace App1uwp.UC
{
  public sealed class ItemMessageUC : UserControl, IComponentConnector
  {
    public static readonly DependencyProperty DataProperty = DependencyProperty.Register(nameof (Data), typeof (object), typeof (ItemMessageUC), new PropertyMetadata((object) null, new PropertyChangedCallback(ItemMessageUC.OnDataChanged)));
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Grid mainGrid;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Border _brd;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Path Path_In;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Path Path_Out;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private StackPanel MainContent;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private StackPanel Footer;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBlock textDate;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public object Data
    {
      get => ((DependencyObject) this).GetValue(ItemMessageUC.DataProperty);
      set => ((DependencyObject) this).SetValue(ItemMessageUC.DataProperty, value);
    }

    private static void OnDataChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
      ((ItemMessageUC) obj).ProcessData();
    }

    public ItemMessageUC()
    {
      this.InitializeComponent();
      this._brd.LetsRound();
    }

    private VKMessageVM DataVM => this.Data as VKMessageVM;

    private void ProcessData()
    {
      ((ICollection<UIElement>) ((Panel) this.MainContent).Children).Clear();
      if (this.Data == null)
        return;
      VKMessageVM data = this.Data as VKMessageVM;
      if (data.action != VKChatMessageActionType.None)
      {
        VKProfileBase cachedUser1 = (VKProfileBase) UsersService.Instance.GetCachedUser((long) data.user_id);
        VKProfileBase cachedUser2 = (VKProfileBase) UsersService.Instance.GetCachedUser((long) data.action_mid);
        string text = DialogsViewModel.GenerateText((VKMessage) data, cachedUser1, cachedUser2, true);
        ((UIElement) this.Footer).put_Visibility((Visibility) 1);
        ((UIElement) this.Path_In).put_Visibility((Visibility) 1);
        ((UIElement) this.Path_Out).put_Visibility((Visibility) 1);
        data.@out = VKMessageType.Sent;
        ScrollableTextBlock scrollableTextBlock = new ScrollableTextBlock();
        scrollableTextBlock.FullOnly = true;
        scrollableTextBlock.Foreground = (Brush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "TextColorContent"];
        scrollableTextBlock.Text = text;
        scrollableTextBlock.FontSize = (double) ((IDictionary<object, object>) Application.Current.Resources)[(object) "FontSizeContent"];
        ((FrameworkElement) scrollableTextBlock).put_HorizontalAlignment((HorizontalAlignment) 1);
        ((ICollection<UIElement>) ((Panel) this.MainContent).Children).Add((UIElement) scrollableTextBlock);
      }
      else
      {
        Thickness thickness = new Thickness(10.0);
        if (data.@out == VKMessageType.Received)
        {
          ((FrameworkElement) this.mainGrid).put_HorizontalAlignment((HorizontalAlignment) 0);
          ((UIElement) this.Path_In).put_Visibility((Visibility) 0);
          ((UIElement) this.Path_Out).put_Visibility((Visibility) 1);
          thickness.Right = 80.0;
        }
        else
        {
          ((FrameworkElement) this.mainGrid).put_HorizontalAlignment((HorizontalAlignment) 2);
          ((UIElement) this.Path_In).put_Visibility((Visibility) 1);
          ((UIElement) this.Path_Out).put_Visibility((Visibility) 0);
          thickness.Left = 80.0;
        }
        ((FrameworkElement) this).put_Margin(thickness);
        if (!string.IsNullOrEmpty(data.body))
        {
          ScrollableTextBlock scrollableTextBlock = new ScrollableTextBlock();
          scrollableTextBlock.FullOnly = true;
          scrollableTextBlock.Foreground = (Brush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "TextColorContent"];
          scrollableTextBlock.Text = data.body;
          scrollableTextBlock.FontSize = (double) ((IDictionary<object, object>) Application.Current.Resources)[(object) "FontSizeContent"];
          ((UIElement) scrollableTextBlock).put_IsHitTestVisible(false);
          ((ICollection<UIElement>) ((Panel) this.MainContent).Children).Add((UIElement) scrollableTextBlock);
        }
        double num1 = ((FrameworkElement) (Window.Current.Content as Frame)).ActualWidth - ((FrameworkElement) this).Margin.Right - ((FrameworkElement) this).Margin.Left - 40.0;
        if (this.DataVM.fwd_messages != null)
        {
          foreach (VKMessage fwdMessage in this.DataVM.fwd_messages)
          {
            double num2 = 0.0;
            if (data.UserThumbVisibility == null)
              num2 = 64.0;
            ((ICollection<UIElement>) ((Panel) this.MainContent).Children).Add((UIElement) new ForwardedMessagesUC(fwdMessage, num1 - num2));
          }
        }
        if (data.chat_id > 0 && data.@out == VKMessageType.Received)
          num1 -= 50.0;
        if (this.DataVM.attachments != null)
          ((ICollection<UIElement>) ((Panel) this.MainContent).Children).Add((UIElement) new AttachmentPresenter(this.DataVM.attachments, num1));
        if (this.DataVM.OutboundMessageVM == null || this.DataVM.OutboundMessageVM.Attachments == null || this.DataVM.OutboundMessageVM.Attachments.Count <= 0)
          return;
        ((ICollection<UIElement>) ((Panel) this.MainContent).Children).Add((UIElement) new AttachmentPresenter(this.DataVM.OutboundMessageVM.Attachments, num1, data.@out == VKMessageType.Sent));
      }
    }

    private void Image_Tapped(object sender, TappedRoutedEventArgs e)
    {
      NavigatorImpl.Instance.NavigateToProfilePage((long) (this.Data as VKMessageVM).user_id);
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///UC/ItemMessageUC.xaml"), (ComponentResourceLocation) 0);
      this.mainGrid = (Grid) ((FrameworkElement) this).FindName("mainGrid");
      this._brd = (Border) ((FrameworkElement) this).FindName("_brd");
      this.Path_In = (Path) ((FrameworkElement) this).FindName("Path_In");
      this.Path_Out = (Path) ((FrameworkElement) this).FindName("Path_Out");
      this.MainContent = (StackPanel) ((FrameworkElement) this).FindName("MainContent");
      this.Footer = (StackPanel) ((FrameworkElement) this).FindName("Footer");
      this.textDate = (TextBlock) ((FrameworkElement) this).FindName("textDate");
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void Connect(int connectionId, object target)
    {
      if (connectionId == 1)
      {
        UIElement uiElement = (UIElement) target;
        WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement.add_Tapped), new Action<EventRegistrationToken>(uiElement.remove_Tapped), new TappedEventHandler(this.Image_Tapped));
      }
      this._contentLoaded = true;
    }
  }
}
