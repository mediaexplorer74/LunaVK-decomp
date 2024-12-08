// Decompiled with JetBrains decompiler
// Type: App1uwp.DialogsPage
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
using App1uwp.Library;
using App1uwp.Network.ViewModels;
using App1uwp.UC;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Navigation;

#nullable disable
namespace App1uwp
{
  public sealed class DialogsPage : PageBase, IComponentConnector
  {
    private bool _isInitialized;
    private PopUP popForItem;
    private PopUP popForFilter;
    private string NeedTitle = "";
    private TextBlock t;
    private static ConversationsUC _conversationsUCInstance;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Grid ContentPanel;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public static ConversationsUC ConversationsUCInstance
    {
      get
      {
        if (DialogsPage._conversationsUCInstance == null)
          DialogsPage._conversationsUCInstance = new ConversationsUC();
        return DialogsPage._conversationsUCInstance;
      }
      set => DialogsPage._conversationsUCInstance = value;
    }

    public DialogsPage()
    {
      this.InitializeComponent();
      DialogsPage dialogsPage = this;
      WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(((FrameworkElement) dialogsPage).add_Loaded), new Action<EventRegistrationToken>(((FrameworkElement) dialogsPage).remove_Loaded), new RoutedEventHandler(this.DialogsPage_Loaded));
    }

    private void _header_OnFilterTap(object sender, TappedRoutedEventArgs e)
    {
      Point position = e.GetPosition((UIElement) null);
      if (this.popForFilter == null)
      {
        this.popForFilter = new PopUP();
        this.popForFilter.ItemTapped += new EventHandler<int>(this._filterPopUp);
      }
      this.popForFilter.ClearItems();
      this.popForFilter.AddItem(0, "Все");
      this.popForFilter.AddItem(1, "Важные", active: false);
      this.popForFilter.AddItem(2, "Непрочитанные");
      this.popForFilter.Show(position);
      e.put_Handled(true);
    }

    private void _filterPopUp(object sender, int i)
    {
      this.t.put_Text(this.popForFilter.GetTitle(i));
      DialogsViewModel.Instance.SetDialogsSource(i);
    }

    private CustomFrame CFrame => Window.Current.Content as CustomFrame;

    private void DialogsPage_Loaded(object sender, RoutedEventArgs e)
    {
      (Window.Current.Content as CustomFrame).HeaderWithMenu.SetTitle(this.NeedTitle);
      StackPanel stackPanel = new StackPanel();
      stackPanel.put_Orientation((Orientation) 1);
      this.t = new TextBlock();
      this.t.put_Text("Все");
      this.t.put_FontSize((double) ((IDictionary<object, object>) Application.Current.Resources)[(object) "FontSizeContent"]);
      IconUC iconUc = new IconUC();
      iconUc.put_Glyph("\uE70D");
      iconUc.put_FontSize((double) ((IDictionary<object, object>) Application.Current.Resources)[(object) "FontSizeContent"]);
      ((FrameworkElement) iconUc).put_Margin(new Thickness(10.0, 0.0, 10.0, 0.0));
      ((ICollection<UIElement>) ((Panel) stackPanel).Children).Add((UIElement) this.t);
      ((ICollection<UIElement>) ((Panel) stackPanel).Children).Add((UIElement) iconUc);
      ((ICollection<UIElement>) ((Panel) this.CFrame.HeaderWithMenu.SubContent).Children).Add((UIElement) stackPanel);
      Grid subContent = this.CFrame.HeaderWithMenu.SubContent;
      WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(((UIElement) subContent).add_Tapped), new Action<EventRegistrationToken>(((UIElement) subContent).remove_Tapped), new TappedEventHandler(this._header_OnFilterTap));
      Grid headerGrid = this.CFrame.HeaderWithMenu.HeaderGrid;
      WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(((UIElement) headerGrid).add_Tapped), new Action<EventRegistrationToken>(((UIElement) headerGrid).remove_Tapped), new TappedEventHandler(this._header_Tapped));
      CommandBarButton commandBarButton1 = new CommandBarButton();
      commandBarButton1.Icon = "\uEE56";
      commandBarButton1.Label = "написать";
      CommandBarButton commandBarButton2 = new CommandBarButton();
      commandBarButton2.Icon = "\uE71E";
      commandBarButton2.Label = "искать";
      this.CFrame.CommandBar.PrimaryCommands.Add(commandBarButton1);
      this.CFrame.CommandBar.PrimaryCommands.Add(commandBarButton2);
    }

    private void Search() => this.CFrame.HeaderWithMenu.ActivateSearch(true);

    protected override void HandleOnNavigatedTo(NavigationEventArgs e)
    {
      DialogsPage.ConversationsUCInstance.param = (object) (e.Parameter as IOutboundAttachment);
      if (DialogsPage.ConversationsUCInstance.param != null)
      {
        (Window.Current.Content as CustomFrame).HeaderWithMenu.HideSandwitchButton = true;
        this.NeedTitle = LocalizedStrings.GetString("ChooseConversation");
        DialogsPage.ConversationsUCInstance.InSelectionMode = true;
      }
      else
      {
        this.NeedTitle = LocalizedStrings.GetString("Menu_Messages/Title");
        DialogsPage.ConversationsUCInstance.InSelectionMode = false;
      }
      base.HandleOnNavigatedTo(e);
      if (!this._isInitialized)
      {
        ((ICollection<UIElement>) ((Panel) this.ContentPanel).Children).Add((UIElement) DialogsPage.ConversationsUCInstance);
        this._isInitialized = true;
      }
      DialogsPage.ConversationsUCInstance.PrepareForViewIfNeeded();
    }

    protected override void HandleOnNavigatingFrom(NavigatingCancelEventArgs e)
    {
      ((ICollection<UIElement>) ((Panel) this.ContentPanel).Children).Remove((UIElement) DialogsPage.ConversationsUCInstance);
    }

    private void _header_Tapped(object sender, TappedRoutedEventArgs e)
    {
      DialogsPage.ConversationsUCInstance.Scroll.GetInsideScrollViewer.ChangeView(new double?(0.0), new double?(0.0), new float?(1f));
    }

    private void ItemDialogUC_Holding(object sender, HoldingRoutedEventArgs e)
    {
      Point position = e.GetPosition((UIElement) null);
      if (this.popForItem == null)
      {
        this.popForItem = new PopUP();
        this.popForItem.AddItem(0, "Delete", "\uE74D", false);
        this.popForItem.AddItem(1, "Disable notification", "\uE74F", false);
        this.popForItem.ItemTapped += new EventHandler<int>(this._itemPopUp);
      }
      this.popForItem.Show(position);
      e.put_Handled(true);
    }

    private void _itemPopUp(object sender, int i)
    {
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///DialogsPage.xaml"), (ComponentResourceLocation) 0);
      this.ContentPanel = (Grid) ((FrameworkElement) this).FindName("ContentPanel");
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void Connect(int connectionId, object target) => this._contentLoaded = true;
  }
}
