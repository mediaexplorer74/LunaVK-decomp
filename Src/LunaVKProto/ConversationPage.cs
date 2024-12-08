// Decompiled with JetBrains decompiler
// Type: App1uwp.ConversationPage
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
using App1uwp.Library;
using App1uwp.Library.Events;
using App1uwp.Network.DataObjects;
using App1uwp.Network.DataVM;
using App1uwp.Network.Enums;
using App1uwp.Network.ViewModels;
using App1uwp.UC;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

#nullable disable
namespace App1uwp
{
  public sealed class ConversationPage : 
    PageBase,
    ISubscriber<VKMessageVM>,
    ISubscriber<MessageHasBeenReadEvent>,
    ISubscriber<UserBecameOnlineEvent>,
    ISubscriber<UserIsTyping>,
    IComponentConnector
  {
    private DispatcherTimer stopAnimationTimer = new DispatcherTimer();
    private PopUP popAttach;
    private PopUP popMsg;
    private PopUP popMenu;
    private bool InSelectionMode;
    private List<VKMessageVM> selectedMsgs = new List<VKMessageVM>();
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Grid MainGrid;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private ExtendedListView2 MainScroll;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private NewMessageUC ucNewMessage;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Rectangle Offset;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Border typingBorder;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBlock typingText;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBlock textBlockSubtitleVertical;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBlock textBlockTitle;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public ConversationPage()
    {
      this.InitializeComponent();
      TextBox textBoxNewComment = this.ucNewMessage.TextBoxNewComment;
      WindowsRuntimeMarshal.AddEventHandler<TextChangedEventHandler>(new Func<TextChangedEventHandler, EventRegistrationToken>(textBoxNewComment.add_TextChanged), new Action<EventRegistrationToken>(textBoxNewComment.remove_TextChanged), new TextChangedEventHandler(this.TextBoxNewComment_TextChanged));
      Border borderSend = this.ucNewMessage.BorderSend;
      WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(((UIElement) borderSend).add_Tapped), new Action<EventRegistrationToken>(((UIElement) borderSend).remove_Tapped), new TappedEventHandler(this.BorderSend_Tapped));
      Grid borderAttach = this.ucNewMessage.BorderAttach;
      WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(((UIElement) borderAttach).add_Tapped), new Action<EventRegistrationToken>(((UIElement) borderAttach).remove_Tapped), new TappedEventHandler(this.BorderAttach_Tapped));
      this.ucNewMessage.PanelControl.StickerTapped += new EventHandler<int>(this.PanelControl_StickerTapped);
      EventAggregator.Instance.SubsribeEvent((object) this);
      this.stopAnimationTimer.put_Interval(TimeSpan.FromSeconds(5.0));
      DispatcherTimer stopAnimationTimer = this.stopAnimationTimer;
      WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(stopAnimationTimer.add_Tick), new Action<EventRegistrationToken>(stopAnimationTimer.remove_Tick), (EventHandler<object>) ((o, e) =>
      {
        ((UIElement) this.typingBorder).put_Visibility((Visibility) 1);
        this.stopAnimationTimer.Stop();
      }));
      ListView mentionPicker = this.ucNewMessage.MentionPicker;
      WindowsRuntimeMarshal.AddEventHandler<SelectionChangedEventHandler>(new Func<SelectionChangedEventHandler, EventRegistrationToken>(((Selector) mentionPicker).add_SelectionChanged), new Action<EventRegistrationToken>(((Selector) mentionPicker).remove_SelectionChanged), new SelectionChangedEventHandler(this.MentionPicker_SelectionChanged));
      this.ChatMembers = new ObservableCollection<VKBaseDataForGroupOrUser>();
    }

    private void MentionPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      this.ucNewMessage.ForceFocusIfNeeded();
      ListView listView = sender as ListView;
      if (((Selector) listView).SelectedItem == null)
        return;
      VKBaseDataForGroupOrUser selectedItem = ((Selector) listView).SelectedItem as VKBaseDataForGroupOrUser;
      string text = this.ucNewMessage.TextBoxNewComment.Text;
      int startIndex = text.LastIndexOf("@");
      string str = string.Format("[id{0}|{1}]", (object) selectedItem.id, (object) selectedItem.Title);
      this.ucNewMessage.TextBoxNewComment.put_Text(text.Remove(startIndex).Insert(startIndex, str));
      this.ucNewMessage.TextBoxNewComment.put_SelectionStart(startIndex + str.Length);
    }

    protected override void HandleOnBackKeyPress(CancelEventArgs e)
    {
      bool flag = this.ucNewMessage.HidePanel();
      e.Cancel = flag;
    }

    private void PanelControl_StickerTapped(object sender, int sticker_id)
    {
      this.ConversationVM.SendMessage(sticker_id);
    }

    private void Attachments_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      this.ucNewMessage.ActivateSendButton(this.ConversationVM.Attachments.Count > 0 || !string.IsNullOrEmpty(this.ucNewMessage.TextBoxNewComment.Text));
    }

    private void BorderAttach_Tapped(object sender, TappedRoutedEventArgs e)
    {
      Point position = e.GetPosition((UIElement) null);
      if (this.popAttach == null)
      {
        this.popAttach = new PopUP();
        this.popAttach.ItemTapped += new EventHandler<int>(this._popCallback);
      }
      this.popAttach.ClearItems();
      this.popAttach.AddItem(0, "Photo", "\uEB9F");
      this.popAttach.AddItem(1, "Camera", "\uE722", false);
      this.popAttach.AddItem(2, "Location", "\uE707", false);
      this.popAttach.AddItem(3, "Graffiti", "\uEDC6", false);
      this.popAttach.AddItem(4, "Money", "\uE88B", false);
      this.popAttach.AddItem(5, "Gift", "\uE88C", false);
      this.popAttach.AddItem(6, "File from account", "\uE8E5", false);
      this.popAttach.Show(position);
      e.put_Handled(true);
    }

    private async void _popCallback(object sender, int i)
    {
      if (i != 0)
        return;
      FileOpenPicker fileOpenPicker = new FileOpenPicker();
      fileOpenPicker.FileTypeFilter.Add(".jpg");
      fileOpenPicker.FileTypeFilter.Add(".bmp");
      fileOpenPicker.FileTypeFilter.Add(".png");
      fileOpenPicker.FileTypeFilter.Add(".mp4");
      fileOpenPicker.FileTypeFilter.Add(".mov");
      fileOpenPicker.FileTypeFilter.Add(".wmv");
      fileOpenPicker.FileTypeFilter.Add(".3gp");
      fileOpenPicker.FileTypeFilter.Add(".3g2");
      fileOpenPicker.put_SuggestedStartLocation((PickerLocationId) 6);
      IReadOnlyList<StorageFile> files = await fileOpenPicker.PickMultipleFilesAsync();
      if (files == null || files.Count <= 0)
        return;
      foreach (StorageFile f in (IEnumerable<StorageFile>) files)
      {
        StorageItemThumbnail thumbnailAsync = await f.GetThumbnailAsync((ThumbnailMode) 0);
        OutboundPhotoAttachment a = new OutboundPhotoAttachment();
        a.sf = f;
        BitmapImage bimg = new BitmapImage();
        using (FileRandomAccessStream stream = (FileRandomAccessStream) await f.OpenAsync((FileAccessMode) 0))
          ((BitmapSource) bimg).SetSource((IRandomAccessStream) stream);
        a.ImgWidth = (double) ((BitmapSource) bimg).PixelWidth;
        a.ImgHeight = (double) ((BitmapSource) bimg).PixelHeight;
        a.LocalUrl2 = bimg;
        this.ConversationVM.Attachments.Add((IOutboundAttachment) a);
      }
    }

    private async void CalBack(AttachmentPickerType t)
    {
      if (t != AttachmentPickerType.PhotoOrVideoFromPhone)
        return;
      FileOpenPicker fileOpenPicker = new FileOpenPicker();
      fileOpenPicker.FileTypeFilter.Add(".jpg");
      fileOpenPicker.FileTypeFilter.Add(".bmp");
      fileOpenPicker.FileTypeFilter.Add(".png");
      fileOpenPicker.FileTypeFilter.Add(".mp4");
      fileOpenPicker.FileTypeFilter.Add(".mov");
      fileOpenPicker.FileTypeFilter.Add(".wmv");
      fileOpenPicker.FileTypeFilter.Add(".3gp");
      fileOpenPicker.FileTypeFilter.Add(".3g2");
      fileOpenPicker.put_SuggestedStartLocation((PickerLocationId) 6);
      IReadOnlyList<StorageFile> files = await fileOpenPicker.PickMultipleFilesAsync();
      if (files == null || files.Count <= 0)
        return;
      foreach (StorageFile f in (IEnumerable<StorageFile>) files)
      {
        StorageItemThumbnail thumbnailAsync = await f.GetThumbnailAsync((ThumbnailMode) 0);
        OutboundPhotoAttachment a = new OutboundPhotoAttachment();
        a.sf = f;
        BitmapImage bimg = new BitmapImage();
        using (FileRandomAccessStream stream = (FileRandomAccessStream) await f.OpenAsync((FileAccessMode) 0))
          ((BitmapSource) bimg).SetSource((IRandomAccessStream) stream);
        a.ImgWidth = (double) ((BitmapSource) bimg).PixelWidth;
        a.ImgHeight = (double) ((BitmapSource) bimg).PixelHeight;
        a.LocalUrl2 = bimg;
        this.ConversationVM.Attachments.Add((IOutboundAttachment) a);
      }
    }

    public DialogHistoryViewModel ConversationVM
    {
      get => ((FrameworkElement) this).DataContext as DialogHistoryViewModel;
    }

    public ObservableCollection<VKBaseDataForGroupOrUser> ChatMembers { get; set; }

    private void TextBoxNewComment_TextChanged(object sender, TextChangedEventArgs e)
    {
      this.ConversationVM.UserIsTyping();
      TextBox textBox = sender as TextBox;
      this.ucNewMessage.ActivateSendButton(this.ConversationVM.Attachments.Count > 0 || !string.IsNullOrEmpty(textBox.Text));
      this.UpdateMentionPicker();
    }

    private void UpdateMentionPicker()
    {
      if (this.ConversationVM._chatId == 0 || this.ConversationVM.IsKickedFromChat)
        return;
      if (this.ucNewMessage.TextBoxNewComment.Text.EndsWith("@"))
      {
        if (((ItemsControl) this.ucNewMessage.MentionPicker).ItemsSource == null)
          ((ItemsControl) this.ucNewMessage.MentionPicker).put_ItemsSource((object) this.ChatMembers);
        this.ChatMembers.Clear();
        foreach (VKBaseDataForGroupOrUser chatMember in this.ConversationVM.ChatMembers)
          this.ChatMembers.Add(chatMember);
      }
      else
        this.ChatMembers.Clear();
    }

    private void BorderSend_Tapped(object sender, TappedRoutedEventArgs e)
    {
      string text = this.ucNewMessage.TextBoxNewComment.Text;
      this.ucNewMessage.TextBoxNewComment.put_Text("");
      this.ConversationVM.SendMessage(text);
      this.ConversationVM.Attachments.Clear();
    }

    protected override void HandleOnNavigatedTo(NavigationEventArgs e)
    {
      PagesParams parameter = e.Parameter as PagesParams;
      ((FrameworkElement) this).put_DataContext((object) new DialogHistoryViewModel(parameter.user_id, parameter.chat_id));
      base.HandleOnNavigatedTo(e);
      this.ConversationVM.LoadData(true);
      this.ConversationVM.SetReadStatusIfNeeded();
      this.ConversationVM.Attachments.CollectionChanged += new NotifyCollectionChangedEventHandler(this.Attachments_CollectionChanged);
      if (parameter.attachment == null)
        return;
      this.ConversationVM.Attachments.Add(parameter.attachment as IOutboundAttachment);
    }

    protected override void HandleOnNavigatingFrom(NavigatingCancelEventArgs e)
    {
      EventAggregator.Instance.UnSubsribeEvent((object) this);
    }

    public async void OnEventHandler(VKMessageVM m)
    {
      if (this.ConversationVM._chatId != m.chat_id || this.ConversationVM._chatId == 0 && this.ConversationVM._userId != m.user_id)
        return;
      if (m.@out == VKMessageType.Sent)
      {
        if (this.ConversationVM.Items.FirstOrDefault<VKMessageVM>((Func<VKMessageVM, bool>) (msg0 => msg0.id == m.id)) != null)
          return;
        this.ConversationVM.Items.Insert(0, m);
      }
      else
      {
        VKBaseDataForGroupOrUser u = UsersService.Instance.GetCachedUser((long) m.user_id);
        if (u == null)
        {
          await UsersService.Instance.GetUsers(new List<int>()
          {
            m.user_id
          });
          u = UsersService.Instance.GetCachedUser((long) m.user_id);
        }
        this.ConversationVM.Items.Insert(0, m);
        this.ConversationVM.SetReadStatusIfNeeded();
        ((UIElement) this.typingBorder).put_Visibility((Visibility) 1);
      }
    }

    public void OnEventHandler(MessageHasBeenReadEvent ev)
    {
      if (ev.is_chat)
      {
        if ((long) this.ConversationVM._chatId != ev.chat_id)
          return;
      }
      else if (this.ConversationVM._userId != ev.user_id)
        return;
      VKMessageVM vkMessageVm = this.ConversationVM.Items.FirstOrDefault<VKMessageVM>((Func<VKMessageVM, bool>) (m => (long) m.id == ev.msg_id));
      if (vkMessageVm == null)
        return;
      vkMessageVm.read_state = true;
      vkMessageVm.RefreshUIProperties();
    }

    public void OnEventHandler(UserBecameOnlineEvent ev)
    {
      if (this.ConversationVM._userId != ev.user_id || this.ConversationVM._chatId != 0)
        return;
      this.ConversationVM.UpdateUISubtitle((VKProfileBase) UsersService.Instance.GetCachedUser((long) this.ConversationVM._userId));
    }

    public void OnEventHandler(UserIsTyping e)
    {
      if ((long) this.ConversationVM._userId != e._userId || (long) this.ConversationVM._chatId != e._chatId)
        return;
      string str = "";
      VKBaseDataForGroupOrUser cachedUser = UsersService.Instance.GetCachedUser(e._userId);
      if (cachedUser != null)
        str = cachedUser.Title + " " + LocalizedStrings.GetString("IsTyping");
      this.typingText.put_Text(str);
      ((UIElement) this.typingBorder).put_Visibility((Visibility) 0);
      this.stopAnimationTimer.Start();
    }

    private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
    {
      if (this.ConversationVM._chatId != 0)
        return;
      NavigatorImpl.Instance.NavigateToProfilePage((long) this.ConversationVM._userId);
    }

    private void ItemMessageUC_Holding(object sender, HoldingRoutedEventArgs e)
    {
      VKMessageVM dataContext = (sender as FrameworkElement).DataContext as VKMessageVM;
      Point position = e.GetPosition((UIElement) null);
      if (this.popMsg == null)
      {
        this.popMsg = new PopUP();
        this.popMsg.ItemTapped += new EventHandler<int>(this._picker_ItemTapped);
      }
      this.popMsg.ClearItems();
      this.popMsg.AddItem(0, "Выбрать", "\uE762");
      if (dataContext.@out == VKMessageType.Received)
        this.popMsg.AddItem(1, "Ответить", "\uE8CA");
      this.popMsg.AddItem(2, "Переслать", "\uE72A");
      this.popMsg.AddItem(3, dataContext.important ? "Неважное" : "Важное", "\uE734");
      this.popMsg.AddItem(4, "Удалить", "\uE74D");
      if (dataContext.@out == VKMessageType.Sent)
        this.popMsg.AddItem(5, "Редактировать", "\uE70F", false);
      this.popMsg.Argument = (object) dataContext;
      this.popMsg.Show(position);
      e.put_Handled(true);
    }

    private void _picker_ItemTapped(object argument, int i)
    {
      VKMessageVM msg = argument as VKMessageVM;
      switch (i)
      {
        case 0:
          this.InSelectionMode = true;
          msg.IsSelected = true;
          this.selectedMsgs.Add(msg);
          this.BuildAppBar();
          break;
        case 1:
          this.ConversationVM.AddForwardedMessagesToOutboundMessage((IList<VKMessage>) new List<VKMessage>()
          {
            (VKMessage) msg
          });
          break;
        case 2:
          NavigatorImpl.Instance.NavigateToConversations((object) new OutboundForwardedMessages(new List<VKMessage>()
          {
            (VKMessage) msg
          }));
          break;
        case 3:
          this.ConversationVM.MarkAsImportant(msg, !msg.important);
          break;
        case 4:
          this.ConversationVM.DeleteMessages(new List<VKMessageVM>()
          {
            msg
          });
          break;
      }
    }

    private void Grid_Tapped_1(object sender, TappedRoutedEventArgs e)
    {
      e.put_Handled(true);
      Point position = e.GetPosition((UIElement) null);
      if (this.popMenu == null)
        this.popMenu = new PopUP();
      this.popMenu.ClearItems();
      this.popMenu.AddItem(0, "Показать вложения", "\uE723", false);
      this.popMenu.AddItem(1, "Добавить собеседника", "\uE8FA", false);
      this.popMenu.AddItem(2, "Удалить диалог", "\uE74D", false);
      if (this.ConversationVM._chatId > 0)
      {
        this.popMenu.AddItem(3, "Покинуть беседу", "\uE89B", false);
        this.popMenu.AddItem(4, "Отключить уведомления", "\uE74F", false);
      }
      this.popMenu.Show(position);
    }

    private void Grid_Tapped_2(object sender, TappedRoutedEventArgs e)
    {
      e.put_Handled(true);
      if (!(Window.Current.Content as Frame).CanGoBack)
        return;
      (Window.Current.Content as Frame).GoBack();
    }

    private void ItemMessageUC_Tapped(object sender, TappedRoutedEventArgs e)
    {
      if (!this.InSelectionMode)
        return;
      VKMessageVM dataContext = (sender as FrameworkElement).DataContext as VKMessageVM;
      dataContext.IsSelected = !dataContext.IsSelected;
      if (dataContext.IsSelected)
        this.selectedMsgs.Add(dataContext);
      else
        this.selectedMsgs.Remove(dataContext);
      if (this.selectedMsgs.Count == 0)
      {
        ((UIElement) this.ucNewMessage).put_Visibility((Visibility) 0);
        this.CFrame.CommandBar.PrimaryCommands.Clear();
        this.InSelectionMode = false;
      }
      e.put_Handled(true);
    }

    private void BuildAppBar()
    {
      if (this.CFrame.CommandBar.PrimaryCommands.Count > 0)
        return;
      CommandBarButton commandBarButton1 = new CommandBarButton()
      {
        Icon = "\uE89C",
        Label = "переслать",
        Command = (ICommand) new DelegateCommand((Action<object>) (a => this._appBarButtonForward_Click()))
      };
      CommandBarButton commandBarButton2 = new CommandBarButton()
      {
        Icon = "\uE74D",
        Label = "удалить",
        Command = (ICommand) new DelegateCommand((Action<object>) (a => this._appBarButtonDelete_Click()))
      };
      CommandBarButton commandBarButton3 = new CommandBarButton()
      {
        Icon = "\uE711",
        Label = "отмена",
        Command = (ICommand) new DelegateCommand((Action<object>) (a => this._appBarButtonCancel_Click()))
      };
      this.CFrame.CommandBar.PrimaryCommands.Add(commandBarButton1);
      this.CFrame.CommandBar.PrimaryCommands.Add(commandBarButton2);
      this.CFrame.CommandBar.PrimaryCommands.Add(commandBarButton3);
      ((UIElement) this.ucNewMessage).put_Visibility((Visibility) 1);
    }

    private void _appBarButtonCancel_Click()
    {
      foreach (VKMessageVM selectedMsg in this.selectedMsgs)
        selectedMsg.IsSelected = false;
      this.selectedMsgs.Clear();
      ((UIElement) this.ucNewMessage).put_Visibility((Visibility) 0);
      this.CFrame.CommandBar.PrimaryCommands.Clear();
      this.InSelectionMode = false;
    }

    private void _appBarButtonDelete_Click()
    {
      this.ConversationVM.DeleteMessages(this.selectedMsgs, (Action<bool>) (res => this.selectedMsgs.Clear()));
      this.InSelectionMode = false;
      ((UIElement) this.ucNewMessage).put_Visibility((Visibility) 0);
      this.CFrame.CommandBar.PrimaryCommands.Clear();
    }

    private void _appBarButtonForward_Click()
    {
      NavigatorImpl.Instance.NavigateToConversations((object) new OutboundForwardedMessages(((IEnumerable<VKMessage>) this.selectedMsgs).ToList<VKMessage>()));
    }

    private CustomFrame CFrame => Window.Current.Content as CustomFrame;

    private void ucNewMessage_SizeChanged(object sender, SizeChangedEventArgs e)
    {
      ((FrameworkElement) this.Offset).put_Height(e.NewSize.Height);
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///ConversationPage.xaml"), (ComponentResourceLocation) 0);
      this.MainGrid = (Grid) ((FrameworkElement) this).FindName("MainGrid");
      this.MainScroll = (ExtendedListView2) ((FrameworkElement) this).FindName("MainScroll");
      this.ucNewMessage = (NewMessageUC) ((FrameworkElement) this).FindName("ucNewMessage");
      this.Offset = (Rectangle) ((FrameworkElement) this).FindName("Offset");
      this.typingBorder = (Border) ((FrameworkElement) this).FindName("typingBorder");
      this.typingText = (TextBlock) ((FrameworkElement) this).FindName("typingText");
      this.textBlockSubtitleVertical = (TextBlock) ((FrameworkElement) this).FindName("textBlockSubtitleVertical");
      this.textBlockTitle = (TextBlock) ((FrameworkElement) this).FindName("textBlockTitle");
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          FrameworkElement frameworkElement = (FrameworkElement) target;
          WindowsRuntimeMarshal.AddEventHandler<SizeChangedEventHandler>(new Func<SizeChangedEventHandler, EventRegistrationToken>(frameworkElement.add_SizeChanged), new Action<EventRegistrationToken>(frameworkElement.remove_SizeChanged), new SizeChangedEventHandler(this.ucNewMessage_SizeChanged));
          break;
        case 2:
          UIElement uiElement1 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<HoldingEventHandler>(new Func<HoldingEventHandler, EventRegistrationToken>(uiElement1.add_Holding), new Action<EventRegistrationToken>(uiElement1.remove_Holding), new HoldingEventHandler(this.ItemMessageUC_Holding));
          UIElement uiElement2 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement2.add_Tapped), new Action<EventRegistrationToken>(uiElement2.remove_Tapped), new TappedEventHandler(this.ItemMessageUC_Tapped));
          break;
        case 3:
          UIElement uiElement3 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement3.add_Tapped), new Action<EventRegistrationToken>(uiElement3.remove_Tapped), new TappedEventHandler(this.Grid_Tapped));
          break;
        case 4:
          UIElement uiElement4 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uiElement4.add_Tapped), new Action<EventRegistrationToken>(uiElement4.remove_Tapped), new TappedEventHandler(this.Grid_Tapped_1));
          break;
      }
      this._contentLoaded = true;
    }
  }
}
