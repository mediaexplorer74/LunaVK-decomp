// Decompiled with JetBrains decompiler
// Type: App1uwp.App1uwp_WindowsPhone_XamlTypeInfo.XamlTypeInfoProvider
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
using App1uwp.Library;
using App1uwp.Network.Converters;
using App1uwp.Network.DataObjects;
using App1uwp.Network.Enums;
using App1uwp.Network.ViewModels;
using App1uwp.UC;
using App1uwp.UC.Attachment;
using App1uwp.VirtualUC;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;

#nullable disable
namespace App1uwp.App1uwp_WindowsPhone_XamlTypeInfo
{
  [DebuggerNonUserCode]
  [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")]
  internal class XamlTypeInfoProvider
  {
    private Dictionary<string, IXamlType> _xamlTypeCacheByName = new Dictionary<string, IXamlType>();
    private Dictionary<Type, IXamlType> _xamlTypeCacheByType = new Dictionary<Type, IXamlType>();
    private Dictionary<string, IXamlMember> _xamlMembers = new Dictionary<string, IXamlMember>();
    private string[] _typeNameTable;
    private Type[] _typeTable;

    public IXamlType GetXamlTypeByType(Type type)
    {
      IXamlType xamlType;
      if (this._xamlTypeCacheByType.TryGetValue(type, out xamlType))
        return xamlType;
      int typeIndex = this.LookupTypeIndexByType(type);
      if (typeIndex != -1)
        xamlType = this.CreateXamlType(typeIndex);
      if (xamlType != null)
      {
        this._xamlTypeCacheByName.Add(xamlType.FullName, xamlType);
        this._xamlTypeCacheByType.Add(xamlType.UnderlyingType, xamlType);
      }
      return xamlType;
    }

    public IXamlType GetXamlTypeByName(string typeName)
    {
      if (string.IsNullOrEmpty(typeName))
        return (IXamlType) null;
      IXamlType xamlType;
      if (this._xamlTypeCacheByName.TryGetValue(typeName, out xamlType))
        return xamlType;
      int typeIndex = this.LookupTypeIndexByName(typeName);
      if (typeIndex != -1)
        xamlType = this.CreateXamlType(typeIndex);
      if (xamlType != null)
      {
        this._xamlTypeCacheByName.Add(xamlType.FullName, xamlType);
        this._xamlTypeCacheByType.Add(xamlType.UnderlyingType, xamlType);
      }
      return xamlType;
    }

    public IXamlMember GetMemberByLongName(string longMemberName)
    {
      if (string.IsNullOrEmpty(longMemberName))
        return (IXamlMember) null;
      IXamlMember memberByLongName;
      if (this._xamlMembers.TryGetValue(longMemberName, out memberByLongName))
        return memberByLongName;
      IXamlMember xamlMember = this.CreateXamlMember(longMemberName);
      if (xamlMember != null)
        this._xamlMembers.Add(longMemberName, xamlMember);
      return xamlMember;
    }

    private void InitTypeTables()
    {
      this._typeNameTable = new string[161];
      this._typeNameTable[0] = "Windows.UI.Color";
      this._typeNameTable[1] = "System.ValueType";
      this._typeNameTable[2] = "Object";
      this._typeNameTable[3] = "Byte";
      this._typeNameTable[4] = "App1uwp.Framework.CustomFrame";
      this._typeNameTable[5] = "Windows.UI.Xaml.Controls.Frame";
      this._typeNameTable[6] = "Windows.UI.Xaml.Controls.ContentControl";
      this._typeNameTable[7] = "App1uwp.Framework.CommandBar";
      this._typeNameTable[8] = "App1uwp.UC.NotificationsPanel";
      this._typeNameTable[9] = "Windows.UI.Xaml.Controls.UserControl";
      this._typeNameTable[10] = "App1uwp.UC.HeaderWithMenuUC";
      this._typeNameTable[11] = "App1uwp.Framework.CustomFrame.MenuStates";
      this._typeNameTable[12] = "System.Enum";
      this._typeNameTable[13] = "Windows.UI.Xaml.Controls.Grid";
      this._typeNameTable[14] = "Boolean";
      this._typeNameTable[15] = "Windows.UI.Xaml.FrameworkElement";
      this._typeNameTable[16] = "Windows.UI.Xaml.DependencyObject";
      this._typeNameTable[17] = "App1uwp.Framework.ExtendedListView2";
      this._typeNameTable[18] = "Windows.UI.Xaml.Controls.ItemsControl";
      this._typeNameTable[19] = "Windows.UI.Xaml.Controls.ItemsPresenter";
      this._typeNameTable[20] = "Windows.UI.Xaml.Controls.ScrollViewer";
      this._typeNameTable[21] = "Windows.UI.Xaml.Controls.ListView";
      this._typeNameTable[22] = "System.Action`1<Double>";
      this._typeNameTable[23] = "System.MulticastDelegate";
      this._typeNameTable[24] = "System.Delegate";
      this._typeNameTable[25] = "System.Collections.ObjectModel.ObservableCollection`1<App1uwp.Framework.CommandBarButton>";
      this._typeNameTable[26] = "System.Collections.ObjectModel.Collection`1<App1uwp.Framework.CommandBarButton>";
      this._typeNameTable[27] = "App1uwp.Framework.CommandBarButton";
      this._typeNameTable[28] = "String";
      this._typeNameTable[29] = "System.Windows.Input.ICommand";
      this._typeNameTable[30] = "System.Collections.ObjectModel.ObservableCollection`1<App1uwp.Library.OptionsMenuItem>";
      this._typeNameTable[31] = "System.Collections.ObjectModel.Collection`1<App1uwp.Library.OptionsMenuItem>";
      this._typeNameTable[32] = "App1uwp.Library.OptionsMenuItem";
      this._typeNameTable[33] = "App1uwp.Network.ViewModels.ViewModelBase";
      this._typeNameTable[34] = "Double";
      this._typeNameTable[35] = "App1uwp.UC.PullToRefreshUC";
      this._typeNameTable[36] = "App1uwp.PageBase";
      this._typeNameTable[37] = "Windows.UI.Xaml.Controls.Page";
      this._typeNameTable[38] = "App1uwp.UC.IconUC";
      this._typeNameTable[39] = "Windows.UI.Xaml.Controls.FontIcon";
      this._typeNameTable[40] = "App1uwp.AboutPage";
      this._typeNameTable[41] = "App1uwp.Network.Converters.BoolToVisibilityConverter";
      this._typeNameTable[42] = "App1uwp.UC.NewMessageUC";
      this._typeNameTable[43] = "App1uwp.UC.SwipeThroughControl";
      this._typeNameTable[44] = "Windows.UI.Xaml.Controls.TextBox";
      this._typeNameTable[45] = "Windows.UI.Xaml.Controls.Border";
      this._typeNameTable[46] = "App1uwp.UC.ItemMessageUC";
      this._typeNameTable[47] = "App1uwp.UC.AvatarUC";
      this._typeNameTable[48] = "App1uwp.ConversationPage";
      this._typeNameTable[49] = "App1uwp.Network.ViewModels.DialogHistoryViewModel";
      this._typeNameTable[50] = "System.Collections.ObjectModel.ObservableCollection`1<App1uwp.Network.DataObjects.VKBaseDataForGroupOrUser>";
      this._typeNameTable[51] = "System.Collections.ObjectModel.Collection`1<App1uwp.Network.DataObjects.VKBaseDataForGroupOrUser>";
      this._typeNameTable[52] = "App1uwp.Network.DataObjects.VKBaseDataForGroupOrUser";
      this._typeNameTable[53] = "Int32";
      this._typeNameTable[54] = "App1uwp.Network.Enums.VKIsDeactivated";
      this._typeNameTable[55] = "App1uwp.Network.DataObjects.VKCounters";
      this._typeNameTable[56] = "App1uwp.Framework.ExtendedGridView";
      this._typeNameTable[57] = "Windows.UI.Xaml.Controls.GridView";
      this._typeNameTable[58] = "App1uwp.FavoritesPage";
      this._typeNameTable[59] = "App1uwp.FontTest";
      this._typeNameTable[60] = "App1uwp.UC.ItemFriendUC";
      this._typeNameTable[61] = "App1uwp.FriendsPage";
      this._typeNameTable[62] = "App1uwp.Network.ViewModels.FriendsViewModel";
      this._typeNameTable[63] = "App1uwp.UC.SettingsSectionUC";
      this._typeNameTable[64] = "App1uwp.GroupManagementPage";
      this._typeNameTable[65] = "App1uwp.UC.InfoListItemUC";
      this._typeNameTable[66] = "System.Collections.Generic.List`1<Windows.UI.Xaml.Documents.Inline>";
      this._typeNameTable[67] = "Windows.UI.Xaml.Documents.Inline";
      this._typeNameTable[68] = "App1uwp.VirtualUC.ItemsBasePostUC";
      this._typeNameTable[69] = "Windows.UI.Xaml.Controls.StackPanel";
      this._typeNameTable[70] = "Windows.UI.Xaml.Controls.Panel";
      this._typeNameTable[71] = "App1uwp.Network.DataObjects.VKWallPost";
      this._typeNameTable[72] = "App1uwp.Network.DataObjects.VKBaseDataForPostOrNews";
      this._typeNameTable[73] = "App1uwp.Network.DataObjects.VKNewsfeedPost";
      this._typeNameTable[74] = "App1uwp.UC.LoadingUC";
      this._typeNameTable[75] = "System.Action";
      this._typeNameTable[76] = "App1uwp.GroupPage";
      this._typeNameTable[77] = "App1uwp.UC.ItemGroupInvitationUC";
      this._typeNameTable[78] = "App1uwp.UC.ItemGroupUC";
      this._typeNameTable[79] = "App1uwp.GroupsPage";
      this._typeNameTable[80] = "App1uwp.Network.ViewModels.GroupsViewModel";
      this._typeNameTable[81] = "App1uwp.LoginPage";
      this._typeNameTable[82] = "App1uwp.DialogsPage";
      this._typeNameTable[83] = "App1uwp.MarketPage";
      this._typeNameTable[84] = "App1uwp.Network.ViewModels.MarketViewModel";
      this._typeNameTable[85] = "App1uwp.UC.AudioTrackUC";
      this._typeNameTable[86] = "App1uwp.MusicPage";
      this._typeNameTable[87] = "App1uwp.Network.ViewModels.MusicViewModel";
      this._typeNameTable[88] = "App1uwp.NewsPage";
      this._typeNameTable[89] = "App1uwp.UC.ItemNotificationUC";
      this._typeNameTable[90] = "App1uwp.NotificationsPage";
      this._typeNameTable[91] = "App1uwp.Network.ViewModels.NotificationsViewModel";
      this._typeNameTable[92] = "App1uwp.UC.ItemComment";
      this._typeNameTable[93] = "App1uwp.PostCommentsPage";
      this._typeNameTable[94] = "App1uwp.Network.Converters.ThreelenToIntConverter";
      this._typeNameTable[95] = "App1uwp.SettingsPersonalizationPage";
      this._typeNameTable[96] = "App1uwp.UC.PhotoAlbumUC";
      this._typeNameTable[97] = "App1uwp.PhotoAlbumPage";
      this._typeNameTable[98] = "App1uwp.Network.ViewModels.PhotoAlbumViewModel";
      this._typeNameTable[99] = "App1uwp.PhotosPage";
      this._typeNameTable[100] = "App1uwp.Network.ViewModels.PhotosViewModel";
      this._typeNameTable[101] = "App1uwp.TestAddRemove";
      this._typeNameTable[102] = "System.Collections.ObjectModel.ObservableCollection`1<App1uwp.TestAddRemove.Temp>";
      this._typeNameTable[103] = "System.Collections.ObjectModel.Collection`1<App1uwp.TestAddRemove.Temp>";
      this._typeNameTable[104] = "App1uwp.TestAddRemove.Temp";
      this._typeNameTable[105] = "App1uwp.UC.MentionPickerUC";
      this._typeNameTable[106] = "App1uwp.UC.MiniPlayerUC";
      this._typeNameTable[107] = "App1uwp.UserPage";
      this._typeNameTable[108] = "App1uwp.Network.ViewModels.ProfileViewModel";
      this._typeNameTable[109] = "App1uwp.UC.ToggleSwitch";
      this._typeNameTable[110] = "Windows.UI.Xaml.Media.Brush";
      this._typeNameTable[111] = "App1uwp.SettingsGeneralPage";
      this._typeNameTable[112] = "App1uwp.SettingsNotificationsPage";
      this._typeNameTable[113] = "App1uwp.SettingsPage";
      this._typeNameTable[114] = "App1uwp.UC.StickersPackInfoUC";
      this._typeNameTable[115] = "App1uwp.StickersStorePage";
      this._typeNameTable[116] = "App1uwp.TestAudioRecord";
      this._typeNameTable[117] = "App1uwp.TestNotificationsPanel";
      this._typeNameTable[118] = "App1uwp.TestRectangleLayout";
      this._typeNameTable[119] = "App1uwp.UC.AppNotification2";
      this._typeNameTable[120] = "App1uwp.Network.Converters.TimeSpanToUIStringConverter";
      this._typeNameTable[121] = "App1uwp.UC.Attachment.AttachAudioUC";
      this._typeNameTable[122] = "App1uwp.UC.Attachment.AttachDocumentUC";
      this._typeNameTable[123] = "App1uwp.UC.Attachment.AttachGiftUC";
      this._typeNameTable[124] = "App1uwp.UC.Attachment.AttachLinkUC";
      this._typeNameTable[125] = "App1uwp.UC.Attachment.AttachPollUC";
      this._typeNameTable[126] = "App1uwp.UC.Attachment.AttachVideoUC";
      this._typeNameTable[(int) sbyte.MaxValue] = "App1uwp.UC.Attachment.AttachVoiceMessageUC";
      this._typeNameTable[128] = "App1uwp.Network.Converters.ThumbnailToImageConverter";
      this._typeNameTable[129] = "App1uwp.Network.Converters.LongToUISizeConverter";
      this._typeNameTable[130] = "App1uwp.UC.Attachment.OutboundMessageAttachment";
      this._typeNameTable[131] = "App1uwp.UC.ItemDialogUC";
      this._typeNameTable[132] = "App1uwp.UC.ConversationsUC";
      this._typeNameTable[133] = "App1uwp.Network.ViewModels.DialogsViewModel";
      this._typeNameTable[134] = "App1uwp.UC.ForwardedMessagesUC";
      this._typeNameTable[135] = "App1uwp.UC.ProgressRingUC";
      this._typeNameTable[136] = "App1uwp.UC.GifViewerUC";
      this._typeNameTable[137] = "App1uwp.UC.ItemSearchUC";
      this._typeNameTable[138] = "App1uwp.UC.MenuItemUC";
      this._typeNameTable[139] = "App1uwp.UC.ImageViewerDecoratorUC";
      this._typeNameTable[140] = "Windows.UI.Xaml.Controls.Image";
      this._typeNameTable[141] = "Windows.Foundation.Rect";
      this._typeNameTable[142] = "App1uwp.UC.ScrollableTextBlock";
      this._typeNameTable[143] = "App1uwp.Network.Converters.ForUIShortTimeConverter";
      this._typeNameTable[144] = "App1uwp.Network.Converters.UIStringTimeConverter";
      this._typeNameTable[145] = "App1uwp.Network.Converters.RelativeTimeConverter";
      this._typeNameTable[146] = "App1uwp.UC.ItemNewsFeedUC";
      this._typeNameTable[147] = "App1uwp.UC.ItemVideoCatalog";
      this._typeNameTable[148] = "System.Collections.ObjectModel.ObservableCollection`1<Object>";
      this._typeNameTable[149] = "System.Collections.ObjectModel.Collection`1<Object>";
      this._typeNameTable[150] = "App1uwp.Library.OutboundAttachmentTemplateSelector";
      this._typeNameTable[151] = "Windows.UI.Xaml.DataTemplate";
      this._typeNameTable[152] = "App1uwp.UC.NewsSourceListItemUC";
      this._typeNameTable[153] = "App1uwp.UC.PopUP";
      this._typeNameTable[154] = "App1uwp.Network.Converters.StickerUrlConverter";
      this._typeNameTable[155] = "App1uwp.Framework.ImageExtensions";
      this._typeNameTable[156] = "System.Uri";
      this._typeNameTable[157] = "Windows.UI.Xaml.Media.ImageBrush";
      this._typeNameTable[158] = "App1uwp.VideoCatalogPage";
      this._typeNameTable[159] = "App1uwp.VideoCommentsPage";
      this._typeNameTable[160] = "App1uwp.VideoPage";
      this._typeTable = new Type[161];
      this._typeTable[0] = typeof (Color);
      this._typeTable[1] = typeof (ValueType);
      this._typeTable[2] = typeof (object);
      this._typeTable[3] = typeof (byte);
      this._typeTable[4] = typeof (CustomFrame);
      this._typeTable[5] = typeof (Frame);
      this._typeTable[6] = typeof (ContentControl);
      this._typeTable[7] = typeof (CommandBar);
      this._typeTable[8] = typeof (NotificationsPanel);
      this._typeTable[9] = typeof (UserControl);
      this._typeTable[10] = typeof (HeaderWithMenuUC);
      this._typeTable[11] = typeof (CustomFrame.MenuStates);
      this._typeTable[12] = typeof (Enum);
      this._typeTable[13] = typeof (Grid);
      this._typeTable[14] = typeof (bool);
      this._typeTable[15] = typeof (FrameworkElement);
      this._typeTable[16] = typeof (DependencyObject);
      this._typeTable[17] = typeof (ExtendedListView2);
      this._typeTable[18] = typeof (ItemsControl);
      this._typeTable[19] = typeof (ItemsPresenter);
      this._typeTable[20] = typeof (ScrollViewer);
      this._typeTable[21] = typeof (ListView);
      this._typeTable[22] = typeof (Action<double>);
      this._typeTable[23] = typeof (MulticastDelegate);
      this._typeTable[24] = typeof (Delegate);
      this._typeTable[25] = typeof (ObservableCollection<CommandBarButton>);
      this._typeTable[26] = typeof (Collection<CommandBarButton>);
      this._typeTable[27] = typeof (CommandBarButton);
      this._typeTable[28] = typeof (string);
      this._typeTable[29] = typeof (ICommand);
      this._typeTable[30] = typeof (ObservableCollection<OptionsMenuItem>);
      this._typeTable[31] = typeof (Collection<OptionsMenuItem>);
      this._typeTable[32] = typeof (OptionsMenuItem);
      this._typeTable[33] = typeof (ViewModelBase);
      this._typeTable[34] = typeof (double);
      this._typeTable[35] = typeof (PullToRefreshUC);
      this._typeTable[36] = typeof (PageBase);
      this._typeTable[37] = typeof (Page);
      this._typeTable[38] = typeof (IconUC);
      this._typeTable[39] = typeof (FontIcon);
      this._typeTable[40] = typeof (AboutPage);
      this._typeTable[41] = typeof (BoolToVisibilityConverter);
      this._typeTable[42] = typeof (NewMessageUC);
      this._typeTable[43] = typeof (SwipeThroughControl);
      this._typeTable[44] = typeof (TextBox);
      this._typeTable[45] = typeof (Border);
      this._typeTable[46] = typeof (App1uwp.UC.ItemMessageUC);
      this._typeTable[47] = typeof (AvatarUC);
      this._typeTable[48] = typeof (ConversationPage);
      this._typeTable[49] = typeof (DialogHistoryViewModel);
      this._typeTable[50] = typeof (ObservableCollection<VKBaseDataForGroupOrUser>);
      this._typeTable[51] = typeof (Collection<VKBaseDataForGroupOrUser>);
      this._typeTable[52] = typeof (VKBaseDataForGroupOrUser);
      this._typeTable[53] = typeof (int);
      this._typeTable[54] = typeof (VKIsDeactivated);
      this._typeTable[55] = typeof (VKCounters);
      this._typeTable[56] = typeof (ExtendedGridView);
      this._typeTable[57] = typeof (GridView);
      this._typeTable[58] = typeof (FavoritesPage);
      this._typeTable[59] = typeof (FontTest);
      this._typeTable[60] = typeof (ItemFriendUC);
      this._typeTable[61] = typeof (FriendsPage);
      this._typeTable[62] = typeof (FriendsViewModel);
      this._typeTable[63] = typeof (SettingsSectionUC);
      this._typeTable[64] = typeof (GroupManagementPage);
      this._typeTable[65] = typeof (InfoListItemUC);
      this._typeTable[66] = typeof (List<Inline>);
      this._typeTable[67] = typeof (Inline);
      this._typeTable[68] = typeof (ItemsBasePostUC);
      this._typeTable[69] = typeof (StackPanel);
      this._typeTable[70] = typeof (Panel);
      this._typeTable[71] = typeof (VKWallPost);
      this._typeTable[72] = typeof (VKBaseDataForPostOrNews);
      this._typeTable[73] = typeof (VKNewsfeedPost);
      this._typeTable[74] = typeof (LoadingUC);
      this._typeTable[75] = typeof (Action);
      this._typeTable[76] = typeof (GroupPage);
      this._typeTable[77] = typeof (ItemGroupInvitationUC);
      this._typeTable[78] = typeof (ItemGroupUC);
      this._typeTable[79] = typeof (GroupsPage);
      this._typeTable[80] = typeof (GroupsViewModel);
      this._typeTable[81] = typeof (LoginPage);
      this._typeTable[82] = typeof (DialogsPage);
      this._typeTable[83] = typeof (MarketPage);
      this._typeTable[84] = typeof (MarketViewModel);
      this._typeTable[85] = typeof (AudioTrackUC);
      this._typeTable[86] = typeof (MusicPage);
      this._typeTable[87] = typeof (MusicViewModel);
      this._typeTable[88] = typeof (NewsPage);
      this._typeTable[89] = typeof (ItemNotificationUC);
      this._typeTable[90] = typeof (NotificationsPage);
      this._typeTable[91] = typeof (NotificationsViewModel);
      this._typeTable[92] = typeof (ItemComment);
      this._typeTable[93] = typeof (PostCommentsPage);
      this._typeTable[94] = typeof (ThreelenToIntConverter);
      this._typeTable[95] = typeof (SettingsPersonalizationPage);
      this._typeTable[96] = typeof (PhotoAlbumUC);
      this._typeTable[97] = typeof (PhotoAlbumPage);
      this._typeTable[98] = typeof (PhotoAlbumViewModel);
      this._typeTable[99] = typeof (PhotosPage);
      this._typeTable[100] = typeof (PhotosViewModel);
      this._typeTable[101] = typeof (TestAddRemove);
      this._typeTable[102] = typeof (ObservableCollection<TestAddRemove.Temp>);
      this._typeTable[103] = typeof (Collection<TestAddRemove.Temp>);
      this._typeTable[104] = typeof (TestAddRemove.Temp);
      this._typeTable[105] = typeof (MentionPickerUC);
      this._typeTable[106] = typeof (MiniPlayerUC);
      this._typeTable[107] = typeof (UserPage);
      this._typeTable[108] = typeof (ProfileViewModel);
      this._typeTable[109] = typeof (ToggleSwitch);
      this._typeTable[110] = typeof (Brush);
      this._typeTable[111] = typeof (SettingsGeneralPage);
      this._typeTable[112] = typeof (SettingsNotificationsPage);
      this._typeTable[113] = typeof (SettingsPage);
      this._typeTable[114] = typeof (StickersPackInfoUC);
      this._typeTable[115] = typeof (StickersStorePage);
      this._typeTable[116] = typeof (TestAudioRecord);
      this._typeTable[117] = typeof (TestNotificationsPanel);
      this._typeTable[118] = typeof (TestRectangleLayout);
      this._typeTable[119] = typeof (AppNotification2);
      this._typeTable[120] = typeof (TimeSpanToUIStringConverter);
      this._typeTable[121] = typeof (AttachAudioUC);
      this._typeTable[122] = typeof (AttachDocumentUC);
      this._typeTable[123] = typeof (AttachGiftUC);
      this._typeTable[124] = typeof (AttachLinkUC);
      this._typeTable[125] = typeof (AttachPollUC);
      this._typeTable[126] = typeof (AttachVideoUC);
      this._typeTable[(int) sbyte.MaxValue] = typeof (AttachVoiceMessageUC);
      this._typeTable[128] = typeof (ThumbnailToImageConverter);
      this._typeTable[129] = typeof (LongToUISizeConverter);
      this._typeTable[130] = typeof (OutboundMessageAttachment);
      this._typeTable[131] = typeof (ItemDialogUC);
      this._typeTable[132] = typeof (ConversationsUC);
      this._typeTable[133] = typeof (DialogsViewModel);
      this._typeTable[134] = typeof (ForwardedMessagesUC);
      this._typeTable[135] = typeof (ProgressRingUC);
      this._typeTable[136] = typeof (GifViewerUC);
      this._typeTable[137] = typeof (ItemSearchUC);
      this._typeTable[138] = typeof (MenuItemUC);
      this._typeTable[139] = typeof (ImageViewerDecoratorUC);
      this._typeTable[140] = typeof (Image);
      this._typeTable[141] = typeof (Rect);
      this._typeTable[142] = typeof (ScrollableTextBlock);
      this._typeTable[143] = typeof (ForUIShortTimeConverter);
      this._typeTable[144] = typeof (UIStringTimeConverter);
      this._typeTable[145] = typeof (RelativeTimeConverter);
      this._typeTable[146] = typeof (ItemNewsFeedUC);
      this._typeTable[147] = typeof (ItemVideoCatalog);
      this._typeTable[148] = typeof (ObservableCollection<object>);
      this._typeTable[149] = typeof (Collection<object>);
      this._typeTable[150] = typeof (OutboundAttachmentTemplateSelector);
      this._typeTable[151] = typeof (DataTemplate);
      this._typeTable[152] = typeof (NewsSourceListItemUC);
      this._typeTable[153] = typeof (PopUP);
      this._typeTable[154] = typeof (StickerUrlConverter);
      this._typeTable[155] = typeof (ImageExtensions);
      this._typeTable[156] = typeof (Uri);
      this._typeTable[157] = typeof (ImageBrush);
      this._typeTable[158] = typeof (VideoCatalogPage);
      this._typeTable[159] = typeof (VideoCommentsPage);
      this._typeTable[160] = typeof (VideoPage);
    }

    private int LookupTypeIndexByName(string typeName)
    {
      if (this._typeNameTable == null)
        this.InitTypeTables();
      for (int index = 0; index < this._typeNameTable.Length; ++index)
      {
        if (string.CompareOrdinal(this._typeNameTable[index], typeName) == 0)
          return index;
      }
      return -1;
    }

    private int LookupTypeIndexByType(Type type)
    {
      if (this._typeTable == null)
        this.InitTypeTables();
      for (int index = 0; index < this._typeTable.Length; ++index)
      {
        if (type == this._typeTable[index])
          return index;
      }
      return -1;
    }

    private object Activate_4_CustomFrame() => (object) new CustomFrame();

    private object Activate_7_CommandBar() => (object) new CommandBar();

    private object Activate_8_NotificationsPanel() => (object) new NotificationsPanel();

    private object Activate_10_HeaderWithMenuUC() => (object) new HeaderWithMenuUC();

    private object Activate_17_ExtendedListView2() => (object) new ExtendedListView2();

    private object Activate_25_ObservableCollection()
    {
      return (object) new ObservableCollection<CommandBarButton>();
    }

    private object Activate_26_Collection() => (object) new Collection<CommandBarButton>();

    private object Activate_27_CommandBarButton() => (object) new CommandBarButton();

    private object Activate_30_ObservableCollection()
    {
      return (object) new ObservableCollection<OptionsMenuItem>();
    }

    private object Activate_31_Collection() => (object) new Collection<OptionsMenuItem>();

    private object Activate_32_OptionsMenuItem() => (object) new OptionsMenuItem();

    private object Activate_33_ViewModelBase() => (object) new ViewModelBase();

    private object Activate_35_PullToRefreshUC() => (object) new PullToRefreshUC();

    private object Activate_36_PageBase() => (object) new PageBase();

    private object Activate_38_IconUC() => (object) new IconUC();

    private object Activate_40_AboutPage() => (object) new AboutPage();

    private object Activate_41_BoolToVisibilityConverter()
    {
      return (object) new BoolToVisibilityConverter();
    }

    private object Activate_42_NewMessageUC() => (object) new NewMessageUC();

    private object Activate_43_SwipeThroughControl() => (object) new SwipeThroughControl();

    private object Activate_46_ItemMessageUC() => (object) new App1uwp.UC.ItemMessageUC();

    private object Activate_47_AvatarUC() => (object) new AvatarUC();

    private object Activate_48_ConversationPage() => (object) new ConversationPage();

    private object Activate_50_ObservableCollection()
    {
      return (object) new ObservableCollection<VKBaseDataForGroupOrUser>();
    }

    private object Activate_51_Collection() => (object) new Collection<VKBaseDataForGroupOrUser>();

    private object Activate_52_VKBaseDataForGroupOrUser()
    {
      return (object) new VKBaseDataForGroupOrUser();
    }

    private object Activate_55_VKCounters() => (object) new VKCounters();

    private object Activate_56_ExtendedGridView() => (object) new ExtendedGridView();

    private object Activate_58_FavoritesPage() => (object) new FavoritesPage();

    private object Activate_59_FontTest() => (object) new FontTest();

    private object Activate_60_ItemFriendUC() => (object) new ItemFriendUC();

    private object Activate_61_FriendsPage() => (object) new FriendsPage();

    private object Activate_62_FriendsViewModel() => (object) new FriendsViewModel();

    private object Activate_63_SettingsSectionUC() => (object) new SettingsSectionUC();

    private object Activate_64_GroupManagementPage() => (object) new GroupManagementPage();

    private object Activate_65_InfoListItemUC() => (object) new InfoListItemUC();

    private object Activate_66_List() => (object) new List<Inline>();

    private object Activate_68_ItemsBasePostUC() => (object) new ItemsBasePostUC();

    private object Activate_71_VKWallPost() => (object) new VKWallPost();

    private object Activate_72_VKBaseDataForPostOrNews() => (object) new VKBaseDataForPostOrNews();

    private object Activate_73_VKNewsfeedPost() => (object) new VKNewsfeedPost();

    private object Activate_74_LoadingUC() => (object) new LoadingUC();

    private object Activate_76_GroupPage() => (object) new GroupPage();

    private object Activate_77_ItemGroupInvitationUC() => (object) new ItemGroupInvitationUC();

    private object Activate_78_ItemGroupUC() => (object) new ItemGroupUC();

    private object Activate_79_GroupsPage() => (object) new GroupsPage();

    private object Activate_80_GroupsViewModel() => (object) new GroupsViewModel();

    private object Activate_81_LoginPage() => (object) new LoginPage();

    private object Activate_82_DialogsPage() => (object) new DialogsPage();

    private object Activate_83_MarketPage() => (object) new MarketPage();

    private object Activate_84_MarketViewModel() => (object) new MarketViewModel();

    private object Activate_85_AudioTrackUC() => (object) new AudioTrackUC();

    private object Activate_86_MusicPage() => (object) new MusicPage();

    private object Activate_88_NewsPage() => (object) new NewsPage();

    private object Activate_89_ItemNotificationUC() => (object) new ItemNotificationUC();

    private object Activate_90_NotificationsPage() => (object) new NotificationsPage();

    private object Activate_91_NotificationsViewModel() => (object) new NotificationsViewModel();

    private object Activate_92_ItemComment() => (object) new ItemComment();

    private object Activate_93_PostCommentsPage() => (object) new PostCommentsPage();

    private object Activate_94_ThreelenToIntConverter() => (object) new ThreelenToIntConverter();

    private object Activate_95_SettingsPersonalizationPage()
    {
      return (object) new SettingsPersonalizationPage();
    }

    private object Activate_96_PhotoAlbumUC() => (object) new PhotoAlbumUC();

    private object Activate_97_PhotoAlbumPage() => (object) new PhotoAlbumPage();

    private object Activate_98_PhotoAlbumViewModel() => (object) new PhotoAlbumViewModel();

    private object Activate_99_PhotosPage() => (object) new PhotosPage();

    private object Activate_101_TestAddRemove() => (object) new TestAddRemove();

    private object Activate_102_ObservableCollection()
    {
      return (object) new ObservableCollection<TestAddRemove.Temp>();
    }

    private object Activate_103_Collection() => (object) new Collection<TestAddRemove.Temp>();

    private object Activate_105_MentionPickerUC() => (object) new MentionPickerUC();

    private object Activate_106_MiniPlayerUC() => (object) new MiniPlayerUC();

    private object Activate_107_UserPage() => (object) new UserPage();

    private object Activate_109_ToggleSwitch() => (object) new ToggleSwitch();

    private object Activate_111_SettingsGeneralPage() => (object) new SettingsGeneralPage();

    private object Activate_112_SettingsNotificationsPage()
    {
      return (object) new SettingsNotificationsPage();
    }

    private object Activate_113_SettingsPage() => (object) new SettingsPage();

    private object Activate_114_StickersPackInfoUC() => (object) new StickersPackInfoUC();

    private object Activate_115_StickersStorePage() => (object) new StickersStorePage();

    private object Activate_116_TestAudioRecord() => (object) new TestAudioRecord();

    private object Activate_117_TestNotificationsPanel() => (object) new TestNotificationsPanel();

    private object Activate_118_TestRectangleLayout() => (object) new TestRectangleLayout();

    private object Activate_119_AppNotification2() => (object) new AppNotification2();

    private object Activate_120_TimeSpanToUIStringConverter()
    {
      return (object) new TimeSpanToUIStringConverter();
    }

    private object Activate_121_AttachAudioUC() => (object) new AttachAudioUC();

    private object Activate_122_AttachDocumentUC() => (object) new AttachDocumentUC();

    private object Activate_123_AttachGiftUC() => (object) new AttachGiftUC();

    private object Activate_124_AttachLinkUC() => (object) new AttachLinkUC();

    private object Activate_125_AttachPollUC() => (object) new AttachPollUC();

    private object Activate_126_AttachVideoUC() => (object) new AttachVideoUC();

    private object Activate_127_AttachVoiceMessageUC() => (object) new AttachVoiceMessageUC();

    private object Activate_128_ThumbnailToImageConverter()
    {
      return (object) new ThumbnailToImageConverter();
    }

    private object Activate_129_LongToUISizeConverter() => (object) new LongToUISizeConverter();

    private object Activate_130_OutboundMessageAttachment()
    {
      return (object) new OutboundMessageAttachment();
    }

    private object Activate_131_ItemDialogUC() => (object) new ItemDialogUC();

    private object Activate_132_ConversationsUC() => (object) new ConversationsUC();

    private object Activate_133_DialogsViewModel() => (object) new DialogsViewModel();

    private object Activate_134_ForwardedMessagesUC() => (object) new ForwardedMessagesUC();

    private object Activate_135_ProgressRingUC() => (object) new ProgressRingUC();

    private object Activate_136_GifViewerUC() => (object) new GifViewerUC();

    private object Activate_137_ItemSearchUC() => (object) new ItemSearchUC();

    private object Activate_138_MenuItemUC() => (object) new MenuItemUC();

    private object Activate_139_ImageViewerDecoratorUC() => (object) new ImageViewerDecoratorUC();

    private object Activate_142_ScrollableTextBlock() => (object) new ScrollableTextBlock();

    private object Activate_143_ForUIShortTimeConverter() => (object) new ForUIShortTimeConverter();

    private object Activate_144_UIStringTimeConverter() => (object) new UIStringTimeConverter();

    private object Activate_145_RelativeTimeConverter() => (object) new RelativeTimeConverter();

    private object Activate_146_ItemNewsFeedUC() => (object) new ItemNewsFeedUC();

    private object Activate_147_ItemVideoCatalog() => (object) new ItemVideoCatalog();

    private object Activate_148_ObservableCollection()
    {
      return (object) new ObservableCollection<object>();
    }

    private object Activate_149_Collection() => (object) new Collection<object>();

    private object Activate_150_OutboundAttachmentTemplateSelector()
    {
      return (object) new OutboundAttachmentTemplateSelector();
    }

    private object Activate_152_NewsSourceListItemUC() => (object) new NewsSourceListItemUC();

    private object Activate_153_PopUP() => (object) new PopUP();

    private object Activate_154_StickerUrlConverter() => (object) new StickerUrlConverter();

    private object Activate_158_VideoCatalogPage() => (object) new VideoCatalogPage();

    private object Activate_159_VideoCommentsPage() => (object) new VideoCommentsPage();

    private object Activate_160_VideoPage() => (object) new VideoPage();

    private void VectorAdd_25_ObservableCollection(object instance, object item)
    {
      ((ICollection<CommandBarButton>) instance).Add((CommandBarButton) item);
    }

    private void VectorAdd_26_Collection(object instance, object item)
    {
      ((ICollection<CommandBarButton>) instance).Add((CommandBarButton) item);
    }

    private void VectorAdd_30_ObservableCollection(object instance, object item)
    {
      ((ICollection<OptionsMenuItem>) instance).Add((OptionsMenuItem) item);
    }

    private void VectorAdd_31_Collection(object instance, object item)
    {
      ((ICollection<OptionsMenuItem>) instance).Add((OptionsMenuItem) item);
    }

    private void VectorAdd_50_ObservableCollection(object instance, object item)
    {
      ((ICollection<VKBaseDataForGroupOrUser>) instance).Add((VKBaseDataForGroupOrUser) item);
    }

    private void VectorAdd_51_Collection(object instance, object item)
    {
      ((ICollection<VKBaseDataForGroupOrUser>) instance).Add((VKBaseDataForGroupOrUser) item);
    }

    private void VectorAdd_66_List(object instance, object item)
    {
      ((ICollection<Inline>) instance).Add((Inline) item);
    }

    private void VectorAdd_102_ObservableCollection(object instance, object item)
    {
      ((ICollection<TestAddRemove.Temp>) instance).Add((TestAddRemove.Temp) item);
    }

    private void VectorAdd_103_Collection(object instance, object item)
    {
      ((ICollection<TestAddRemove.Temp>) instance).Add((TestAddRemove.Temp) item);
    }

    private void VectorAdd_148_ObservableCollection(object instance, object item)
    {
      ((ICollection<object>) instance).Add(item);
    }

    private void VectorAdd_149_Collection(object instance, object item)
    {
      ((ICollection<object>) instance).Add(item);
    }

    private IXamlType CreateXamlType(int typeIndex)
    {
      XamlSystemBaseType xamlType = (XamlSystemBaseType) null;
      string fullName = this._typeNameTable[typeIndex];
      Type type = this._typeTable[typeIndex];
      switch (typeIndex)
      {
        case 0:
          XamlUserType xamlUserType1 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("System.ValueType"));
          xamlUserType1.AddMemberName("A");
          xamlUserType1.AddMemberName("B");
          xamlUserType1.AddMemberName("G");
          xamlUserType1.AddMemberName("R");
          xamlType = (XamlSystemBaseType) xamlUserType1;
          break;
        case 1:
          xamlType = (XamlSystemBaseType) new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
          break;
        case 2:
          xamlType = new XamlSystemBaseType(fullName, type);
          break;
        case 3:
          XamlUserType xamlUserType2 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("System.ValueType"));
          xamlUserType2.SetIsReturnTypeStub();
          xamlType = (XamlSystemBaseType) xamlUserType2;
          break;
        case 4:
          XamlUserType xamlUserType3 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Frame"));
          xamlUserType3.Activator = new Activator(this.Activate_4_CustomFrame);
          xamlUserType3.AddMemberName("CommandBar");
          xamlUserType3.AddMemberName("NotificationsPanel");
          xamlUserType3.AddMemberName("HeaderWithMenu");
          xamlUserType3.AddMemberName("MenuState");
          xamlUserType3.AddMemberName("OverlayGrid");
          xamlUserType3.AddMemberName("IsDevicePhone");
          xamlUserType3.AddMemberName("HeaderContent");
          xamlUserType3.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType3;
          break;
        case 5:
          xamlType = new XamlSystemBaseType(fullName, type);
          break;
        case 6:
          xamlType = new XamlSystemBaseType(fullName, type);
          break;
        case 7:
          XamlUserType xamlUserType4 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.ContentControl"));
          xamlUserType4.Activator = new Activator(this.Activate_7_CommandBar);
          xamlUserType4.AddMemberName("PrimaryCommands");
          xamlUserType4.AddMemberName("SecondaryCommands");
          xamlUserType4.AddMemberName("IsMenuOpened");
          xamlUserType4.AddMemberName("IsMenuHiden");
          xamlUserType4.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType4;
          break;
        case 8:
          XamlUserType xamlUserType5 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
          xamlUserType5.Activator = new Activator(this.Activate_8_NotificationsPanel);
          xamlUserType5.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType5;
          break;
        case 9:
          xamlType = new XamlSystemBaseType(fullName, type);
          break;
        case 10:
          XamlUserType xamlUserType6 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
          xamlUserType6.Activator = new Activator(this.Activate_10_HeaderWithMenuUC);
          xamlUserType6.AddMemberName("OptionsMenu");
          xamlUserType6.AddMemberName("MainContent");
          xamlUserType6.AddMemberName("SubContent");
          xamlUserType6.AddMemberName("HeaderHeight");
          xamlUserType6.AddMemberName("BackBackground");
          xamlUserType6.AddMemberName("HideSandwitchButton");
          xamlUserType6.AddMemberName("PullToRefresh");
          xamlUserType6.AddMemberName("HeaderGrid");
          xamlUserType6.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType6;
          break;
        case 11:
          XamlUserType xamlUserType7 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("System.Enum"));
          xamlUserType7.AddEnumValue("StateWide", (object) CustomFrame.MenuStates.StateWide);
          xamlUserType7.AddEnumValue("StateNarrow", (object) CustomFrame.MenuStates.StateNarrow);
          xamlUserType7.AddEnumValue("StateCollapsed", (object) CustomFrame.MenuStates.StateCollapsed);
          xamlUserType7.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType7;
          break;
        case 12:
          xamlType = (XamlSystemBaseType) new XamlUserType(this, fullName, type, this.GetXamlTypeByName("System.ValueType"));
          break;
        case 13:
          xamlType = new XamlSystemBaseType(fullName, type);
          break;
        case 14:
          xamlType = new XamlSystemBaseType(fullName, type);
          break;
        case 15:
          xamlType = new XamlSystemBaseType(fullName, type);
          break;
        case 16:
          xamlType = new XamlSystemBaseType(fullName, type);
          break;
        case 17:
          XamlUserType xamlUserType8 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.ItemsControl"));
          xamlUserType8.Activator = new Activator(this.Activate_17_ExtendedListView2);
          xamlUserType8.AddMemberName("ContentItemsPresenter");
          xamlUserType8.AddMemberName("IsFlat");
          xamlUserType8.AddMemberName("ReversPull");
          xamlUserType8.AddMemberName("IsPullEnabled");
          xamlUserType8.AddMemberName("UseHeaderOffset");
          xamlUserType8.AddMemberName("UseFooterOffset");
          xamlUserType8.AddMemberName("Header");
          xamlUserType8.AddMemberName("Footer");
          xamlUserType8.AddMemberName("GetInsideScrollViewer");
          xamlUserType8.AddMemberName("GetListView");
          xamlUserType8.AddMemberName("OnPullPercentageChanged");
          xamlUserType8.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType8;
          break;
        case 18:
          xamlType = new XamlSystemBaseType(fullName, type);
          break;
        case 19:
          xamlType = new XamlSystemBaseType(fullName, type);
          break;
        case 20:
          xamlType = new XamlSystemBaseType(fullName, type);
          break;
        case 21:
          xamlType = new XamlSystemBaseType(fullName, type);
          break;
        case 22:
          XamlUserType xamlUserType9 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("System.MulticastDelegate"));
          xamlUserType9.SetIsReturnTypeStub();
          xamlType = (XamlSystemBaseType) xamlUserType9;
          break;
        case 23:
          xamlType = (XamlSystemBaseType) new XamlUserType(this, fullName, type, this.GetXamlTypeByName("System.Delegate"));
          break;
        case 24:
          xamlType = (XamlSystemBaseType) new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
          break;
        case 25:
          XamlUserType xamlUserType10 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("System.Collections.ObjectModel.Collection`1<App1uwp.Framework.CommandBarButton>"));
          xamlUserType10.CollectionAdd = new AddToCollection(this.VectorAdd_25_ObservableCollection);
          xamlUserType10.SetIsReturnTypeStub();
          xamlType = (XamlSystemBaseType) xamlUserType10;
          break;
        case 26:
          xamlType = (XamlSystemBaseType) new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"))
          {
            Activator = new Activator(this.Activate_26_Collection),
            CollectionAdd = new AddToCollection(this.VectorAdd_26_Collection)
          };
          break;
        case 27:
          XamlUserType xamlUserType11 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
          xamlUserType11.Activator = new Activator(this.Activate_27_CommandBarButton);
          xamlUserType11.AddMemberName("Label");
          xamlUserType11.AddMemberName("Icon");
          xamlUserType11.AddMemberName("Command");
          xamlUserType11.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType11;
          break;
        case 28:
          xamlType = new XamlSystemBaseType(fullName, type);
          break;
        case 29:
          XamlUserType xamlUserType12 = new XamlUserType(this, fullName, type, (IXamlType) null);
          xamlUserType12.SetIsReturnTypeStub();
          xamlType = (XamlSystemBaseType) xamlUserType12;
          break;
        case 30:
          XamlUserType xamlUserType13 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("System.Collections.ObjectModel.Collection`1<App1uwp.Library.OptionsMenuItem>"));
          xamlUserType13.CollectionAdd = new AddToCollection(this.VectorAdd_30_ObservableCollection);
          xamlUserType13.SetIsReturnTypeStub();
          xamlType = (XamlSystemBaseType) xamlUserType13;
          break;
        case 31:
          xamlType = (XamlSystemBaseType) new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"))
          {
            Activator = new Activator(this.Activate_31_Collection),
            CollectionAdd = new AddToCollection(this.VectorAdd_31_Collection)
          };
          break;
        case 32:
          XamlUserType xamlUserType14 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("App1uwp.Network.ViewModels.ViewModelBase"));
          xamlUserType14.Activator = new Activator(this.Activate_32_OptionsMenuItem);
          xamlUserType14.AddMemberName("Icon");
          xamlUserType14.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType14;
          break;
        case 33:
          XamlUserType xamlUserType15 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
          xamlUserType15.Activator = new Activator(this.Activate_33_ViewModelBase);
          xamlUserType15.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType15;
          break;
        case 34:
          xamlType = new XamlSystemBaseType(fullName, type);
          break;
        case 35:
          XamlUserType xamlUserType16 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
          xamlUserType16.Activator = new Activator(this.Activate_35_PullToRefreshUC);
          xamlUserType16.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType16;
          break;
        case 36:
          XamlUserType xamlUserType17 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
          xamlUserType17.Activator = new Activator(this.Activate_36_PageBase);
          xamlUserType17.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType17;
          break;
        case 37:
          xamlType = new XamlSystemBaseType(fullName, type);
          break;
        case 38:
          XamlUserType xamlUserType18 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.FontIcon"));
          xamlUserType18.Activator = new Activator(this.Activate_38_IconUC);
          xamlUserType18.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType18;
          break;
        case 39:
          xamlType = new XamlSystemBaseType(fullName, type);
          break;
        case 40:
          XamlUserType xamlUserType19 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("App1uwp.PageBase"));
          xamlUserType19.Activator = new Activator(this.Activate_40_AboutPage);
          xamlUserType19.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType19;
          break;
        case 41:
          XamlUserType xamlUserType20 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
          xamlUserType20.Activator = new Activator(this.Activate_41_BoolToVisibilityConverter);
          xamlUserType20.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType20;
          break;
        case 42:
          XamlUserType xamlUserType21 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
          xamlUserType21.Activator = new Activator(this.Activate_42_NewMessageUC);
          xamlUserType21.AddMemberName("PanelControl");
          xamlUserType21.AddMemberName("TextBoxNewComment");
          xamlUserType21.AddMemberName("BorderSend");
          xamlUserType21.AddMemberName("BorderAttach");
          xamlUserType21.AddMemberName("MentionPicker");
          xamlUserType21.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType21;
          break;
        case 43:
          XamlUserType xamlUserType22 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
          xamlUserType22.Activator = new Activator(this.Activate_43_SwipeThroughControl);
          xamlUserType22.AddMemberName("Items");
          xamlUserType22.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType22;
          break;
        case 44:
          xamlType = new XamlSystemBaseType(fullName, type);
          break;
        case 45:
          xamlType = new XamlSystemBaseType(fullName, type);
          break;
        case 46:
          XamlUserType xamlUserType23 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
          xamlUserType23.Activator = new Activator(this.Activate_46_ItemMessageUC);
          xamlUserType23.AddMemberName("Data");
          xamlUserType23.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType23;
          break;
        case 47:
          XamlUserType xamlUserType24 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
          xamlUserType24.Activator = new Activator(this.Activate_47_AvatarUC);
          xamlUserType24.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType24;
          break;
        case 48:
          XamlUserType xamlUserType25 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("App1uwp.PageBase"));
          xamlUserType25.Activator = new Activator(this.Activate_48_ConversationPage);
          xamlUserType25.AddMemberName("ConversationVM");
          xamlUserType25.AddMemberName("ChatMembers");
          xamlUserType25.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType25;
          break;
        case 49:
          XamlUserType xamlUserType26 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("App1uwp.Network.ViewModels.ViewModelBase"));
          xamlUserType26.SetIsReturnTypeStub();
          xamlUserType26.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType26;
          break;
        case 50:
          XamlUserType xamlUserType27 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("System.Collections.ObjectModel.Collection`1<App1uwp.Network.DataObjects.VKBaseDataForGroupOrUser>"));
          xamlUserType27.CollectionAdd = new AddToCollection(this.VectorAdd_50_ObservableCollection);
          xamlUserType27.SetIsReturnTypeStub();
          xamlType = (XamlSystemBaseType) xamlUserType27;
          break;
        case 51:
          xamlType = (XamlSystemBaseType) new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"))
          {
            Activator = new Activator(this.Activate_51_Collection),
            CollectionAdd = new AddToCollection(this.VectorAdd_51_Collection)
          };
          break;
        case 52:
          XamlUserType xamlUserType28 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
          xamlUserType28.Activator = new Activator(this.Activate_52_VKBaseDataForGroupOrUser);
          xamlUserType28.AddMemberName("id");
          xamlUserType28.AddMemberName("deactivated");
          xamlUserType28.AddMemberName("can_post");
          xamlUserType28.AddMemberName("can_see_all_posts");
          xamlUserType28.AddMemberName("counters");
          xamlUserType28.AddMemberName("is_favorite");
          xamlUserType28.AddMemberName("is_hidden_from_feed");
          xamlUserType28.AddMemberName("photo_50");
          xamlUserType28.AddMemberName("photo_100");
          xamlUserType28.AddMemberName("photo_200");
          xamlUserType28.AddMemberName("verified");
          xamlUserType28.AddMemberName("Title");
          xamlUserType28.AddMemberName("domain");
          xamlUserType28.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType28;
          break;
        case 53:
          xamlType = new XamlSystemBaseType(fullName, type);
          break;
        case 54:
          XamlUserType xamlUserType29 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("System.Enum"));
          xamlUserType29.AddEnumValue("None", (object) VKIsDeactivated.None);
          xamlUserType29.AddEnumValue("Deleted", (object) VKIsDeactivated.Deleted);
          xamlUserType29.AddEnumValue("Banned", (object) VKIsDeactivated.Banned);
          xamlUserType29.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType29;
          break;
        case 55:
          XamlUserType xamlUserType30 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
          xamlUserType30.SetIsReturnTypeStub();
          xamlUserType30.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType30;
          break;
        case 56:
          XamlUserType xamlUserType31 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.GridView"));
          xamlUserType31.Activator = new Activator(this.Activate_56_ExtendedGridView);
          xamlUserType31.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType31;
          break;
        case 57:
          xamlType = new XamlSystemBaseType(fullName, type);
          break;
        case 58:
          XamlUserType xamlUserType32 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("App1uwp.PageBase"));
          xamlUserType32.Activator = new Activator(this.Activate_58_FavoritesPage);
          xamlUserType32.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType32;
          break;
        case 59:
          XamlUserType xamlUserType33 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
          xamlUserType33.Activator = new Activator(this.Activate_59_FontTest);
          xamlUserType33.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType33;
          break;
        case 60:
          XamlUserType xamlUserType34 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
          xamlUserType34.Activator = new Activator(this.Activate_60_ItemFriendUC);
          xamlUserType34.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType34;
          break;
        case 61:
          XamlUserType xamlUserType35 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("App1uwp.PageBase"));
          xamlUserType35.Activator = new Activator(this.Activate_61_FriendsPage);
          xamlUserType35.AddMemberName("VM");
          xamlUserType35.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType35;
          break;
        case 62:
          XamlUserType xamlUserType36 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("App1uwp.Network.ViewModels.ViewModelBase"));
          xamlUserType36.SetIsReturnTypeStub();
          xamlUserType36.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType36;
          break;
        case 63:
          XamlUserType xamlUserType37 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
          xamlUserType37.Activator = new Activator(this.Activate_63_SettingsSectionUC);
          xamlUserType37.AddMemberName("Icon");
          xamlUserType37.AddMemberName("Title");
          xamlUserType37.AddMemberName("SubTitle");
          xamlUserType37.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType37;
          break;
        case 64:
          XamlUserType xamlUserType38 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("App1uwp.PageBase"));
          xamlUserType38.Activator = new Activator(this.Activate_64_GroupManagementPage);
          xamlUserType38.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType38;
          break;
        case 65:
          XamlUserType xamlUserType39 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
          xamlUserType39.Activator = new Activator(this.Activate_65_InfoListItemUC);
          xamlUserType39.AddMemberName("IconUrl");
          xamlUserType39.AddMemberName("Text");
          xamlUserType39.AddMemberName("Preview1Url");
          xamlUserType39.AddMemberName("Preview2Url");
          xamlUserType39.AddMemberName("Preview3Url");
          xamlUserType39.AddMemberName("IsTiltEnabled");
          xamlUserType39.AddMemberName("Inlines");
          xamlUserType39.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType39;
          break;
        case 66:
          XamlUserType xamlUserType40 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
          xamlUserType40.CollectionAdd = new AddToCollection(this.VectorAdd_66_List);
          xamlUserType40.SetIsReturnTypeStub();
          xamlType = (XamlSystemBaseType) xamlUserType40;
          break;
        case 67:
          xamlType = new XamlSystemBaseType(fullName, type);
          break;
        case 68:
          XamlUserType xamlUserType41 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.StackPanel"));
          xamlUserType41.Activator = new Activator(this.Activate_68_ItemsBasePostUC);
          xamlUserType41.AddMemberName("DataPost");
          xamlUserType41.AddMemberName("DataNews");
          xamlUserType41.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType41;
          break;
        case 69:
          xamlType = new XamlSystemBaseType(fullName, type);
          break;
        case 70:
          xamlType = new XamlSystemBaseType(fullName, type);
          break;
        case 71:
          XamlUserType xamlUserType42 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("App1uwp.Network.DataObjects.VKBaseDataForPostOrNews"));
          xamlUserType42.SetIsReturnTypeStub();
          xamlUserType42.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType42;
          break;
        case 72:
          XamlUserType xamlUserType43 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
          xamlUserType43.Activator = new Activator(this.Activate_72_VKBaseDataForPostOrNews);
          xamlUserType43.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType43;
          break;
        case 73:
          XamlUserType xamlUserType44 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("App1uwp.Network.DataObjects.VKBaseDataForPostOrNews"));
          xamlUserType44.SetIsReturnTypeStub();
          xamlUserType44.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType44;
          break;
        case 74:
          XamlUserType xamlUserType45 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
          xamlUserType45.Activator = new Activator(this.Activate_74_LoadingUC);
          xamlUserType45.AddMemberName("TryAgainCommand");
          xamlUserType45.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType45;
          break;
        case 75:
          XamlUserType xamlUserType46 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("System.MulticastDelegate"));
          xamlUserType46.SetIsReturnTypeStub();
          xamlType = (XamlSystemBaseType) xamlUserType46;
          break;
        case 76:
          XamlUserType xamlUserType47 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("App1uwp.PageBase"));
          xamlUserType47.Activator = new Activator(this.Activate_76_GroupPage);
          xamlUserType47.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType47;
          break;
        case 77:
          XamlUserType xamlUserType48 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
          xamlUserType48.Activator = new Activator(this.Activate_77_ItemGroupInvitationUC);
          xamlUserType48.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType48;
          break;
        case 78:
          XamlUserType xamlUserType49 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
          xamlUserType49.Activator = new Activator(this.Activate_78_ItemGroupUC);
          xamlUserType49.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType49;
          break;
        case 79:
          XamlUserType xamlUserType50 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("App1uwp.PageBase"));
          xamlUserType50.Activator = new Activator(this.Activate_79_GroupsPage);
          xamlUserType50.AddMemberName("ConversationVM");
          xamlUserType50.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType50;
          break;
        case 80:
          XamlUserType xamlUserType51 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("App1uwp.Network.ViewModels.ViewModelBase"));
          xamlUserType51.SetIsReturnTypeStub();
          xamlUserType51.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType51;
          break;
        case 81:
          XamlUserType xamlUserType52 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
          xamlUserType52.Activator = new Activator(this.Activate_81_LoginPage);
          xamlUserType52.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType52;
          break;
        case 82:
          XamlUserType xamlUserType53 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("App1uwp.PageBase"));
          xamlUserType53.Activator = new Activator(this.Activate_82_DialogsPage);
          xamlUserType53.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType53;
          break;
        case 83:
          XamlUserType xamlUserType54 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("App1uwp.PageBase"));
          xamlUserType54.Activator = new Activator(this.Activate_83_MarketPage);
          xamlUserType54.AddMemberName("VM");
          xamlUserType54.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType54;
          break;
        case 84:
          XamlUserType xamlUserType55 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
          xamlUserType55.SetIsReturnTypeStub();
          xamlUserType55.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType55;
          break;
        case 85:
          XamlUserType xamlUserType56 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
          xamlUserType56.Activator = new Activator(this.Activate_85_AudioTrackUC);
          xamlUserType56.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType56;
          break;
        case 86:
          XamlUserType xamlUserType57 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("App1uwp.PageBase"));
          xamlUserType57.Activator = new Activator(this.Activate_86_MusicPage);
          xamlUserType57.AddMemberName("VM");
          xamlUserType57.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType57;
          break;
        case 87:
          XamlUserType xamlUserType58 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
          xamlUserType58.SetIsReturnTypeStub();
          xamlUserType58.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType58;
          break;
        case 88:
          XamlUserType xamlUserType59 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("App1uwp.PageBase"));
          xamlUserType59.Activator = new Activator(this.Activate_88_NewsPage);
          xamlUserType59.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType59;
          break;
        case 89:
          XamlUserType xamlUserType60 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
          xamlUserType60.Activator = new Activator(this.Activate_89_ItemNotificationUC);
          xamlUserType60.AddMemberName("Post");
          xamlUserType60.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType60;
          break;
        case 90:
          XamlUserType xamlUserType61 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("App1uwp.PageBase"));
          xamlUserType61.Activator = new Activator(this.Activate_90_NotificationsPage);
          xamlUserType61.AddMemberName("ConversationVM");
          xamlUserType61.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType61;
          break;
        case 91:
          XamlUserType xamlUserType62 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
          xamlUserType62.SetIsReturnTypeStub();
          xamlUserType62.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType62;
          break;
        case 92:
          XamlUserType xamlUserType63 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
          xamlUserType63.Activator = new Activator(this.Activate_92_ItemComment);
          xamlUserType63.AddMemberName("Data");
          xamlUserType63.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType63;
          break;
        case 93:
          XamlUserType xamlUserType64 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("App1uwp.PageBase"));
          xamlUserType64.Activator = new Activator(this.Activate_93_PostCommentsPage);
          xamlUserType64.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType64;
          break;
        case 94:
          XamlUserType xamlUserType65 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
          xamlUserType65.Activator = new Activator(this.Activate_94_ThreelenToIntConverter);
          xamlUserType65.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType65;
          break;
        case 95:
          XamlUserType xamlUserType66 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("App1uwp.PageBase"));
          xamlUserType66.Activator = new Activator(this.Activate_95_SettingsPersonalizationPage);
          xamlUserType66.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType66;
          break;
        case 96:
          XamlUserType xamlUserType67 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
          xamlUserType67.Activator = new Activator(this.Activate_96_PhotoAlbumUC);
          xamlUserType67.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType67;
          break;
        case 97:
          XamlUserType xamlUserType68 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("App1uwp.PageBase"));
          xamlUserType68.Activator = new Activator(this.Activate_97_PhotoAlbumPage);
          xamlUserType68.AddMemberName("AlbumsVM");
          xamlUserType68.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType68;
          break;
        case 98:
          XamlUserType xamlUserType69 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
          xamlUserType69.SetIsReturnTypeStub();
          xamlUserType69.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType69;
          break;
        case 99:
          XamlUserType xamlUserType70 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("App1uwp.PageBase"));
          xamlUserType70.Activator = new Activator(this.Activate_99_PhotosPage);
          xamlUserType70.AddMemberName("PhotosVM");
          xamlUserType70.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType70;
          break;
        case 100:
          XamlUserType xamlUserType71 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
          xamlUserType71.SetIsReturnTypeStub();
          xamlUserType71.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType71;
          break;
        case 101:
          XamlUserType xamlUserType72 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("App1uwp.PageBase"));
          xamlUserType72.Activator = new Activator(this.Activate_101_TestAddRemove);
          xamlUserType72.AddMemberName("Items");
          xamlUserType72.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType72;
          break;
        case 102:
          XamlUserType xamlUserType73 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("System.Collections.ObjectModel.Collection`1<App1uwp.TestAddRemove.Temp>"));
          xamlUserType73.CollectionAdd = new AddToCollection(this.VectorAdd_102_ObservableCollection);
          xamlUserType73.SetIsReturnTypeStub();
          xamlType = (XamlSystemBaseType) xamlUserType73;
          break;
        case 103:
          xamlType = (XamlSystemBaseType) new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"))
          {
            Activator = new Activator(this.Activate_103_Collection),
            CollectionAdd = new AddToCollection(this.VectorAdd_103_Collection)
          };
          break;
        case 104:
          XamlUserType xamlUserType74 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
          xamlUserType74.AddMemberName("id");
          xamlUserType74.AddMemberName("content");
          xamlUserType74.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType74;
          break;
        case 105:
          XamlUserType xamlUserType75 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
          xamlUserType75.Activator = new Activator(this.Activate_105_MentionPickerUC);
          xamlUserType75.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType75;
          break;
        case 106:
          XamlUserType xamlUserType76 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
          xamlUserType76.Activator = new Activator(this.Activate_106_MiniPlayerUC);
          xamlUserType76.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType76;
          break;
        case 107:
          XamlUserType xamlUserType77 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("App1uwp.PageBase"));
          xamlUserType77.Activator = new Activator(this.Activate_107_UserPage);
          xamlUserType77.AddMemberName("VM");
          xamlUserType77.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType77;
          break;
        case 108:
          XamlUserType xamlUserType78 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("App1uwp.Network.ViewModels.ViewModelBase"));
          xamlUserType78.SetIsReturnTypeStub();
          xamlUserType78.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType78;
          break;
        case 109:
          XamlUserType xamlUserType79 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
          xamlUserType79.Activator = new Activator(this.Activate_109_ToggleSwitch);
          xamlUserType79.AddMemberName("IsChecked");
          xamlUserType79.AddMemberName("Title");
          xamlUserType79.AddMemberName("IsStateTextVisible");
          xamlUserType79.AddMemberName("Description");
          xamlUserType79.AddMemberName("StateTextOn");
          xamlUserType79.AddMemberName("StateTextOff");
          xamlUserType79.AddMemberName("BorderColor");
          xamlUserType79.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType79;
          break;
        case 110:
          xamlType = new XamlSystemBaseType(fullName, type);
          break;
        case 111:
          XamlUserType xamlUserType80 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("App1uwp.PageBase"));
          xamlUserType80.Activator = new Activator(this.Activate_111_SettingsGeneralPage);
          xamlUserType80.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType80;
          break;
        case 112:
          XamlUserType xamlUserType81 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("App1uwp.PageBase"));
          xamlUserType81.Activator = new Activator(this.Activate_112_SettingsNotificationsPage);
          xamlUserType81.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType81;
          break;
        case 113:
          XamlUserType xamlUserType82 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("App1uwp.PageBase"));
          xamlUserType82.Activator = new Activator(this.Activate_113_SettingsPage);
          xamlUserType82.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType82;
          break;
        case 114:
          XamlUserType xamlUserType83 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
          xamlUserType83.Activator = new Activator(this.Activate_114_StickersPackInfoUC);
          xamlUserType83.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType83;
          break;
        case 115:
          XamlUserType xamlUserType84 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("App1uwp.PageBase"));
          xamlUserType84.Activator = new Activator(this.Activate_115_StickersStorePage);
          xamlUserType84.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType84;
          break;
        case 116:
          XamlUserType xamlUserType85 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
          xamlUserType85.Activator = new Activator(this.Activate_116_TestAudioRecord);
          xamlUserType85.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType85;
          break;
        case 117:
          XamlUserType xamlUserType86 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("App1uwp.PageBase"));
          xamlUserType86.Activator = new Activator(this.Activate_117_TestNotificationsPanel);
          xamlUserType86.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType86;
          break;
        case 118:
          XamlUserType xamlUserType87 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
          xamlUserType87.Activator = new Activator(this.Activate_118_TestRectangleLayout);
          xamlUserType87.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType87;
          break;
        case 119:
          XamlUserType xamlUserType88 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
          xamlUserType88.Activator = new Activator(this.Activate_119_AppNotification2);
          xamlUserType88.AddMemberName("Title");
          xamlUserType88.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType88;
          break;
        case 120:
          XamlUserType xamlUserType89 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
          xamlUserType89.Activator = new Activator(this.Activate_120_TimeSpanToUIStringConverter);
          xamlUserType89.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType89;
          break;
        case 121:
          XamlUserType xamlUserType90 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
          xamlUserType90.Activator = new Activator(this.Activate_121_AttachAudioUC);
          xamlUserType90.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType90;
          break;
        case 122:
          XamlUserType xamlUserType91 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
          xamlUserType91.Activator = new Activator(this.Activate_122_AttachDocumentUC);
          xamlUserType91.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType91;
          break;
        case 123:
          XamlUserType xamlUserType92 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
          xamlUserType92.Activator = new Activator(this.Activate_123_AttachGiftUC);
          xamlUserType92.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType92;
          break;
        case 124:
          XamlUserType xamlUserType93 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
          xamlUserType93.Activator = new Activator(this.Activate_124_AttachLinkUC);
          xamlUserType93.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType93;
          break;
        case 125:
          XamlUserType xamlUserType94 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
          xamlUserType94.Activator = new Activator(this.Activate_125_AttachPollUC);
          xamlUserType94.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType94;
          break;
        case 126:
          XamlUserType xamlUserType95 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
          xamlUserType95.Activator = new Activator(this.Activate_126_AttachVideoUC);
          xamlUserType95.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType95;
          break;
        case (int) sbyte.MaxValue:
          XamlUserType xamlUserType96 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
          xamlUserType96.Activator = new Activator(this.Activate_127_AttachVoiceMessageUC);
          xamlUserType96.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType96;
          break;
        case 128:
          XamlUserType xamlUserType97 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
          xamlUserType97.Activator = new Activator(this.Activate_128_ThumbnailToImageConverter);
          xamlUserType97.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType97;
          break;
        case 129:
          XamlUserType xamlUserType98 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
          xamlUserType98.Activator = new Activator(this.Activate_129_LongToUISizeConverter);
          xamlUserType98.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType98;
          break;
        case 130:
          XamlUserType xamlUserType99 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
          xamlUserType99.Activator = new Activator(this.Activate_130_OutboundMessageAttachment);
          xamlUserType99.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType99;
          break;
        case 131:
          XamlUserType xamlUserType100 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
          xamlUserType100.Activator = new Activator(this.Activate_131_ItemDialogUC);
          xamlUserType100.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType100;
          break;
        case 132:
          XamlUserType xamlUserType101 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
          xamlUserType101.Activator = new Activator(this.Activate_132_ConversationsUC);
          xamlUserType101.AddMemberName("ConversationsVM");
          xamlUserType101.AddMemberName("Scroll");
          xamlUserType101.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType101;
          break;
        case 133:
          XamlUserType xamlUserType102 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("App1uwp.Network.ViewModels.ViewModelBase"));
          xamlUserType102.SetIsReturnTypeStub();
          xamlUserType102.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType102;
          break;
        case 134:
          XamlUserType xamlUserType103 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
          xamlUserType103.Activator = new Activator(this.Activate_134_ForwardedMessagesUC);
          xamlUserType103.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType103;
          break;
        case 135:
          XamlUserType xamlUserType104 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
          xamlUserType104.Activator = new Activator(this.Activate_135_ProgressRingUC);
          xamlUserType104.AddMemberName("Progress");
          xamlUserType104.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType104;
          break;
        case 136:
          XamlUserType xamlUserType105 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
          xamlUserType105.Activator = new Activator(this.Activate_136_GifViewerUC);
          xamlUserType105.AddMemberName("UseOldGifPlayer");
          xamlUserType105.AddMemberName("DownloadProgress");
          xamlUserType105.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType105;
          break;
        case 137:
          XamlUserType xamlUserType106 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
          xamlUserType106.Activator = new Activator(this.Activate_137_ItemSearchUC);
          xamlUserType106.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType106;
          break;
        case 138:
          XamlUserType xamlUserType107 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
          xamlUserType107.Activator = new Activator(this.Activate_138_MenuItemUC);
          xamlUserType107.AddMemberName("Icon");
          xamlUserType107.AddMemberName("Title");
          xamlUserType107.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType107;
          break;
        case 139:
          XamlUserType xamlUserType108 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
          xamlUserType108.Activator = new Activator(this.Activate_139_ImageViewerDecoratorUC);
          xamlUserType108.AddMemberName("CurrentImage");
          xamlUserType108.AddMemberName("CurrentImageFitRectOriginal");
          xamlUserType108.AddMemberName("CurrentImageFitRectTransformed");
          xamlUserType108.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType108;
          break;
        case 140:
          xamlType = new XamlSystemBaseType(fullName, type);
          break;
        case 141:
          xamlType = new XamlSystemBaseType(fullName, type);
          break;
        case 142:
          XamlUserType xamlUserType109 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.StackPanel"));
          xamlUserType109.Activator = new Activator(this.Activate_142_ScrollableTextBlock);
          xamlUserType109.AddMemberName("Text");
          xamlUserType109.AddMemberName("Foreground");
          xamlUserType109.AddMemberName("FullOnly");
          xamlUserType109.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType109;
          break;
        case 143:
          XamlUserType xamlUserType110 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
          xamlUserType110.Activator = new Activator(this.Activate_143_ForUIShortTimeConverter);
          xamlUserType110.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType110;
          break;
        case 144:
          XamlUserType xamlUserType111 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
          xamlUserType111.Activator = new Activator(this.Activate_144_UIStringTimeConverter);
          xamlUserType111.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType111;
          break;
        case 145:
          XamlUserType xamlUserType112 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
          xamlUserType112.Activator = new Activator(this.Activate_145_RelativeTimeConverter);
          xamlUserType112.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType112;
          break;
        case 146:
          XamlUserType xamlUserType113 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
          xamlUserType113.Activator = new Activator(this.Activate_146_ItemNewsFeedUC);
          xamlUserType113.AddMemberName("Post");
          xamlUserType113.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType113;
          break;
        case 147:
          XamlUserType xamlUserType114 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
          xamlUserType114.Activator = new Activator(this.Activate_147_ItemVideoCatalog);
          xamlUserType114.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType114;
          break;
        case 148:
          XamlUserType xamlUserType115 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("System.Collections.ObjectModel.Collection`1<Object>"));
          xamlUserType115.CollectionAdd = new AddToCollection(this.VectorAdd_148_ObservableCollection);
          xamlUserType115.SetIsReturnTypeStub();
          xamlType = (XamlSystemBaseType) xamlUserType115;
          break;
        case 149:
          xamlType = (XamlSystemBaseType) new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"))
          {
            Activator = new Activator(this.Activate_149_Collection),
            CollectionAdd = new AddToCollection(this.VectorAdd_149_Collection)
          };
          break;
        case 150:
          XamlUserType xamlUserType116 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.ContentControl"));
          xamlUserType116.Activator = new Activator(this.Activate_150_OutboundAttachmentTemplateSelector);
          xamlUserType116.AddMemberName("PhotoTemplate");
          xamlUserType116.AddMemberName("ForwardedMessageTemplate");
          xamlUserType116.AddMemberName("GenericIconTemplate");
          xamlUserType116.AddMemberName("GeoTemplate");
          xamlUserType116.AddMemberName("VideoTemplate");
          xamlUserType116.AddMemberName("AudioTemplate");
          xamlUserType116.AddMemberName("DocumentTemplate");
          xamlUserType116.AddMemberName("GenericThumbTemplate");
          xamlUserType116.AddMemberName("AddAttachmentTemplate");
          xamlUserType116.AddMemberName("WallPostTemplate");
          xamlUserType116.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType116;
          break;
        case 151:
          xamlType = new XamlSystemBaseType(fullName, type);
          break;
        case 152:
          XamlUserType xamlUserType117 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
          xamlUserType117.Activator = new Activator(this.Activate_152_NewsSourceListItemUC);
          xamlUserType117.AddMemberName("Icon");
          xamlUserType117.AddMemberName("Title");
          xamlUserType117.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType117;
          break;
        case 153:
          XamlUserType xamlUserType118 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
          xamlUserType118.Activator = new Activator(this.Activate_153_PopUP);
          xamlUserType118.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType118;
          break;
        case 154:
          XamlUserType xamlUserType119 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
          xamlUserType119.Activator = new Activator(this.Activate_154_StickerUrlConverter);
          xamlUserType119.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType119;
          break;
        case 155:
          XamlUserType xamlUserType120 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
          xamlUserType120.AddMemberName("CacheUriImageBrush");
          xamlUserType120.AddMemberName("CacheUri");
          xamlUserType120.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType120;
          break;
        case 156:
          XamlUserType xamlUserType121 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
          xamlUserType121.SetIsReturnTypeStub();
          xamlType = (XamlSystemBaseType) xamlUserType121;
          break;
        case 157:
          xamlType = new XamlSystemBaseType(fullName, type);
          break;
        case 158:
          XamlUserType xamlUserType122 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("App1uwp.PageBase"));
          xamlUserType122.Activator = new Activator(this.Activate_158_VideoCatalogPage);
          xamlUserType122.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType122;
          break;
        case 159:
          XamlUserType xamlUserType123 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("App1uwp.PageBase"));
          xamlUserType123.Activator = new Activator(this.Activate_159_VideoCommentsPage);
          xamlUserType123.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType123;
          break;
        case 160:
          XamlUserType xamlUserType124 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("App1uwp.PageBase"));
          xamlUserType124.Activator = new Activator(this.Activate_160_VideoPage);
          xamlUserType124.SetIsLocalType();
          xamlType = (XamlSystemBaseType) xamlUserType124;
          break;
      }
      return (IXamlType) xamlType;
    }

    private object get_0_Color_A(object instance) => (object) ((Color) instance).A;

    private void set_0_Color_A(object instance, object Value)
    {
      ((Color) instance).A = (byte) Value;
    }

    private object get_1_Color_B(object instance) => (object) ((Color) instance).B;

    private void set_1_Color_B(object instance, object Value)
    {
      ((Color) instance).B = (byte) Value;
    }

    private object get_2_Color_G(object instance) => (object) ((Color) instance).G;

    private void set_2_Color_G(object instance, object Value)
    {
      ((Color) instance).G = (byte) Value;
    }

    private object get_3_Color_R(object instance) => (object) ((Color) instance).R;

    private void set_3_Color_R(object instance, object Value)
    {
      ((Color) instance).R = (byte) Value;
    }

    private object get_4_CustomFrame_CommandBar(object instance)
    {
      return (object) ((CustomFrame) instance).CommandBar;
    }

    private object get_5_CustomFrame_NotificationsPanel(object instance)
    {
      return (object) ((CustomFrame) instance).NotificationsPanel;
    }

    private object get_6_CustomFrame_HeaderWithMenu(object instance)
    {
      return (object) ((CustomFrame) instance).HeaderWithMenu;
    }

    private object get_7_CustomFrame_MenuState(object instance)
    {
      return (object) ((CustomFrame) instance).MenuState;
    }

    private object get_8_CustomFrame_OverlayGrid(object instance)
    {
      return (object) ((CustomFrame) instance).OverlayGrid;
    }

    private object get_9_CustomFrame_IsDevicePhone(object instance)
    {
      return (object) ((CustomFrame) instance).IsDevicePhone;
    }

    private object get_10_CustomFrame_HeaderContent(object instance)
    {
      return (object) CustomFrame.GetHeaderContent((DependencyObject) instance);
    }

    private void set_10_CustomFrame_HeaderContent(object instance, object Value)
    {
      CustomFrame.SetHeaderContent((DependencyObject) instance, (FrameworkElement) Value);
    }

    private object get_11_ExtendedListView2_ContentItemsPresenter(object instance)
    {
      return (object) ((ExtendedListView2) instance).ContentItemsPresenter;
    }

    private object get_12_ExtendedListView2_IsFlat(object instance)
    {
      return (object) ((ExtendedListView2) instance).IsFlat;
    }

    private void set_12_ExtendedListView2_IsFlat(object instance, object Value)
    {
      ((ExtendedListView2) instance).IsFlat = (bool) Value;
    }

    private object get_13_ExtendedListView2_ReversPull(object instance)
    {
      return (object) ((ExtendedListView2) instance).ReversPull;
    }

    private void set_13_ExtendedListView2_ReversPull(object instance, object Value)
    {
      ((ExtendedListView2) instance).ReversPull = (bool) Value;
    }

    private object get_14_ExtendedListView2_IsPullEnabled(object instance)
    {
      return (object) ((ExtendedListView2) instance).IsPullEnabled;
    }

    private void set_14_ExtendedListView2_IsPullEnabled(object instance, object Value)
    {
      ((ExtendedListView2) instance).IsPullEnabled = (bool) Value;
    }

    private object get_15_ExtendedListView2_UseHeaderOffset(object instance)
    {
      return (object) ((ExtendedListView2) instance).UseHeaderOffset;
    }

    private void set_15_ExtendedListView2_UseHeaderOffset(object instance, object Value)
    {
      ((ExtendedListView2) instance).UseHeaderOffset = (bool) Value;
    }

    private object get_16_ExtendedListView2_UseFooterOffset(object instance)
    {
      return (object) ((ExtendedListView2) instance).UseFooterOffset;
    }

    private void set_16_ExtendedListView2_UseFooterOffset(object instance, object Value)
    {
      ((ExtendedListView2) instance).UseFooterOffset = (bool) Value;
    }

    private object get_17_ExtendedListView2_Header(object instance)
    {
      return ((ExtendedListView2) instance).Header;
    }

    private void set_17_ExtendedListView2_Header(object instance, object Value)
    {
      ((ExtendedListView2) instance).Header = Value;
    }

    private object get_18_ExtendedListView2_Footer(object instance)
    {
      return ((ExtendedListView2) instance).Footer;
    }

    private void set_18_ExtendedListView2_Footer(object instance, object Value)
    {
      ((ExtendedListView2) instance).Footer = Value;
    }

    private object get_19_ExtendedListView2_GetInsideScrollViewer(object instance)
    {
      return (object) ((ExtendedListView2) instance).GetInsideScrollViewer;
    }

    private object get_20_ExtendedListView2_GetListView(object instance)
    {
      return (object) ((ExtendedListView2) instance).GetListView;
    }

    private object get_21_ExtendedListView2_OnPullPercentageChanged(object instance)
    {
      return (object) ((ExtendedListView2) instance).OnPullPercentageChanged;
    }

    private void set_21_ExtendedListView2_OnPullPercentageChanged(object instance, object Value)
    {
      ((ExtendedListView2) instance).OnPullPercentageChanged = (Action<double>) Value;
    }

    private object get_22_CommandBar_PrimaryCommands(object instance)
    {
      return (object) ((CommandBar) instance).PrimaryCommands;
    }

    private void set_22_CommandBar_PrimaryCommands(object instance, object Value)
    {
      ((CommandBar) instance).PrimaryCommands = (ObservableCollection<CommandBarButton>) Value;
    }

    private object get_23_CommandBarButton_Label(object instance)
    {
      return (object) ((CommandBarButton) instance).Label;
    }

    private void set_23_CommandBarButton_Label(object instance, object Value)
    {
      ((CommandBarButton) instance).Label = (string) Value;
    }

    private object get_24_CommandBarButton_Icon(object instance)
    {
      return (object) ((CommandBarButton) instance).Icon;
    }

    private void set_24_CommandBarButton_Icon(object instance, object Value)
    {
      ((CommandBarButton) instance).Icon = (string) Value;
    }

    private object get_25_CommandBarButton_Command(object instance)
    {
      return (object) ((CommandBarButton) instance).Command;
    }

    private void set_25_CommandBarButton_Command(object instance, object Value)
    {
      ((CommandBarButton) instance).Command = (ICommand) Value;
    }

    private object get_26_CommandBar_SecondaryCommands(object instance)
    {
      return (object) ((CommandBar) instance).SecondaryCommands;
    }

    private void set_26_CommandBar_SecondaryCommands(object instance, object Value)
    {
      ((CommandBar) instance).SecondaryCommands = (ObservableCollection<CommandBarButton>) Value;
    }

    private object get_27_CommandBar_IsMenuOpened(object instance)
    {
      return (object) ((CommandBar) instance).IsMenuOpened;
    }

    private object get_28_CommandBar_IsMenuHiden(object instance)
    {
      return (object) ((CommandBar) instance).IsMenuHiden;
    }

    private object get_29_HeaderWithMenuUC_OptionsMenu(object instance)
    {
      return (object) ((HeaderWithMenuUC) instance).OptionsMenu;
    }

    private void set_29_HeaderWithMenuUC_OptionsMenu(object instance, object Value)
    {
      ((HeaderWithMenuUC) instance).OptionsMenu = (ObservableCollection<OptionsMenuItem>) Value;
    }

    private object get_30_OptionsMenuItem_Icon(object instance)
    {
      return (object) ((OptionsMenuItem) instance).Icon;
    }

    private void set_30_OptionsMenuItem_Icon(object instance, object Value)
    {
      ((OptionsMenuItem) instance).Icon = (string) Value;
    }

    private object get_31_HeaderWithMenuUC_MainContent(object instance)
    {
      return (object) ((HeaderWithMenuUC) instance).MainContent;
    }

    private object get_32_HeaderWithMenuUC_SubContent(object instance)
    {
      return (object) ((HeaderWithMenuUC) instance).SubContent;
    }

    private object get_33_HeaderWithMenuUC_HeaderHeight(object instance)
    {
      return (object) ((HeaderWithMenuUC) instance).HeaderHeight;
    }

    private object get_34_HeaderWithMenuUC_BackBackground(object instance)
    {
      return (object) ((HeaderWithMenuUC) instance).BackBackground;
    }

    private object get_35_HeaderWithMenuUC_HideSandwitchButton(object instance)
    {
      return (object) ((HeaderWithMenuUC) instance).HideSandwitchButton;
    }

    private void set_35_HeaderWithMenuUC_HideSandwitchButton(object instance, object Value)
    {
      ((HeaderWithMenuUC) instance).HideSandwitchButton = (bool) Value;
    }

    private object get_36_HeaderWithMenuUC_PullToRefresh(object instance)
    {
      return (object) ((HeaderWithMenuUC) instance).PullToRefresh;
    }

    private object get_37_HeaderWithMenuUC_HeaderGrid(object instance)
    {
      return (object) ((HeaderWithMenuUC) instance).HeaderGrid;
    }

    private object get_38_NewMessageUC_PanelControl(object instance)
    {
      return (object) ((NewMessageUC) instance).PanelControl;
    }

    private object get_39_NewMessageUC_TextBoxNewComment(object instance)
    {
      return (object) ((NewMessageUC) instance).TextBoxNewComment;
    }

    private object get_40_NewMessageUC_BorderSend(object instance)
    {
      return (object) ((NewMessageUC) instance).BorderSend;
    }

    private object get_41_NewMessageUC_BorderAttach(object instance)
    {
      return (object) ((NewMessageUC) instance).BorderAttach;
    }

    private object get_42_NewMessageUC_MentionPicker(object instance)
    {
      return (object) ((NewMessageUC) instance).MentionPicker;
    }

    private object get_43_ItemMessageUC_Data(object instance) => ((App1uwp.UC.ItemMessageUC) instance).Data;

    private void set_43_ItemMessageUC_Data(object instance, object Value)
    {
      ((App1uwp.UC.ItemMessageUC) instance).Data = Value;
    }

    private object get_44_ConversationPage_ConversationVM(object instance)
    {
      return (object) ((ConversationPage) instance).ConversationVM;
    }

    private object get_45_ConversationPage_ChatMembers(object instance)
    {
      return (object) ((ConversationPage) instance).ChatMembers;
    }

    private void set_45_ConversationPage_ChatMembers(object instance, object Value)
    {
      ((ConversationPage) instance).ChatMembers = (ObservableCollection<VKBaseDataForGroupOrUser>) Value;
    }

    private object get_46_VKBaseDataForGroupOrUser_id(object instance)
    {
      return (object) ((VKBaseDataForGroupOrUser) instance).id;
    }

    private void set_46_VKBaseDataForGroupOrUser_id(object instance, object Value)
    {
      ((VKBaseDataForGroupOrUser) instance).id = (int) Value;
    }

    private object get_47_VKBaseDataForGroupOrUser_deactivated(object instance)
    {
      return (object) ((VKBaseDataForGroupOrUser) instance).deactivated;
    }

    private void set_47_VKBaseDataForGroupOrUser_deactivated(object instance, object Value)
    {
      ((VKBaseDataForGroupOrUser) instance).deactivated = (VKIsDeactivated) Value;
    }

    private object get_48_VKBaseDataForGroupOrUser_can_post(object instance)
    {
      return (object) ((VKBaseDataForGroupOrUser) instance).can_post;
    }

    private void set_48_VKBaseDataForGroupOrUser_can_post(object instance, object Value)
    {
      ((VKBaseDataForGroupOrUser) instance).can_post = (bool) Value;
    }

    private object get_49_VKBaseDataForGroupOrUser_can_see_all_posts(object instance)
    {
      return (object) ((VKBaseDataForGroupOrUser) instance).can_see_all_posts;
    }

    private void set_49_VKBaseDataForGroupOrUser_can_see_all_posts(object instance, object Value)
    {
      ((VKBaseDataForGroupOrUser) instance).can_see_all_posts = (bool) Value;
    }

    private object get_50_VKBaseDataForGroupOrUser_counters(object instance)
    {
      return (object) ((VKBaseDataForGroupOrUser) instance).counters;
    }

    private void set_50_VKBaseDataForGroupOrUser_counters(object instance, object Value)
    {
      ((VKBaseDataForGroupOrUser) instance).counters = (VKCounters) Value;
    }

    private object get_51_VKBaseDataForGroupOrUser_is_favorite(object instance)
    {
      return (object) ((VKBaseDataForGroupOrUser) instance).is_favorite;
    }

    private void set_51_VKBaseDataForGroupOrUser_is_favorite(object instance, object Value)
    {
      ((VKBaseDataForGroupOrUser) instance).is_favorite = (bool) Value;
    }

    private object get_52_VKBaseDataForGroupOrUser_is_hidden_from_feed(object instance)
    {
      return (object) ((VKBaseDataForGroupOrUser) instance).is_hidden_from_feed;
    }

    private void set_52_VKBaseDataForGroupOrUser_is_hidden_from_feed(object instance, object Value)
    {
      ((VKBaseDataForGroupOrUser) instance).is_hidden_from_feed = (bool) Value;
    }

    private object get_53_VKBaseDataForGroupOrUser_photo_50(object instance)
    {
      return (object) ((VKBaseDataForGroupOrUser) instance).photo_50;
    }

    private void set_53_VKBaseDataForGroupOrUser_photo_50(object instance, object Value)
    {
      ((VKBaseDataForGroupOrUser) instance).photo_50 = (string) Value;
    }

    private object get_54_VKBaseDataForGroupOrUser_photo_100(object instance)
    {
      return (object) ((VKBaseDataForGroupOrUser) instance).photo_100;
    }

    private void set_54_VKBaseDataForGroupOrUser_photo_100(object instance, object Value)
    {
      ((VKBaseDataForGroupOrUser) instance).photo_100 = (string) Value;
    }

    private object get_55_VKBaseDataForGroupOrUser_photo_200(object instance)
    {
      return (object) ((VKBaseDataForGroupOrUser) instance).photo_200;
    }

    private void set_55_VKBaseDataForGroupOrUser_photo_200(object instance, object Value)
    {
      ((VKBaseDataForGroupOrUser) instance).photo_200 = (string) Value;
    }

    private object get_56_VKBaseDataForGroupOrUser_verified(object instance)
    {
      return (object) ((VKBaseDataForGroupOrUser) instance).verified;
    }

    private void set_56_VKBaseDataForGroupOrUser_verified(object instance, object Value)
    {
      ((VKBaseDataForGroupOrUser) instance).verified = (bool) Value;
    }

    private object get_57_VKBaseDataForGroupOrUser_Title(object instance)
    {
      return (object) ((VKBaseDataForGroupOrUser) instance).Title;
    }

    private void set_57_VKBaseDataForGroupOrUser_Title(object instance, object Value)
    {
      ((VKBaseDataForGroupOrUser) instance).Title = (string) Value;
    }

    private object get_58_VKBaseDataForGroupOrUser_domain(object instance)
    {
      return (object) ((VKBaseDataForGroupOrUser) instance).domain;
    }

    private void set_58_VKBaseDataForGroupOrUser_domain(object instance, object Value)
    {
      ((VKBaseDataForGroupOrUser) instance).domain = (string) Value;
    }

    private object get_59_FriendsPage_VM(object instance) => (object) ((FriendsPage) instance).VM;

    private object get_60_SettingsSectionUC_Icon(object instance)
    {
      return (object) ((SettingsSectionUC) instance).Icon;
    }

    private void set_60_SettingsSectionUC_Icon(object instance, object Value)
    {
      ((SettingsSectionUC) instance).Icon = (string) Value;
    }

    private object get_61_SettingsSectionUC_Title(object instance)
    {
      return (object) ((SettingsSectionUC) instance).Title;
    }

    private void set_61_SettingsSectionUC_Title(object instance, object Value)
    {
      ((SettingsSectionUC) instance).Title = (string) Value;
    }

    private object get_62_SettingsSectionUC_SubTitle(object instance)
    {
      return (object) ((SettingsSectionUC) instance).SubTitle;
    }

    private void set_62_SettingsSectionUC_SubTitle(object instance, object Value)
    {
      ((SettingsSectionUC) instance).SubTitle = (string) Value;
    }

    private object get_63_InfoListItemUC_IconUrl(object instance)
    {
      return (object) ((InfoListItemUC) instance).IconUrl;
    }

    private void set_63_InfoListItemUC_IconUrl(object instance, object Value)
    {
      ((InfoListItemUC) instance).IconUrl = (string) Value;
    }

    private object get_64_InfoListItemUC_Text(object instance)
    {
      return (object) ((InfoListItemUC) instance).Text;
    }

    private void set_64_InfoListItemUC_Text(object instance, object Value)
    {
      ((InfoListItemUC) instance).Text = (string) Value;
    }

    private object get_65_InfoListItemUC_Preview1Url(object instance)
    {
      return (object) ((InfoListItemUC) instance).Preview1Url;
    }

    private void set_65_InfoListItemUC_Preview1Url(object instance, object Value)
    {
      ((InfoListItemUC) instance).Preview1Url = (string) Value;
    }

    private object get_66_InfoListItemUC_Preview2Url(object instance)
    {
      return (object) ((InfoListItemUC) instance).Preview2Url;
    }

    private void set_66_InfoListItemUC_Preview2Url(object instance, object Value)
    {
      ((InfoListItemUC) instance).Preview2Url = (string) Value;
    }

    private object get_67_InfoListItemUC_Preview3Url(object instance)
    {
      return (object) ((InfoListItemUC) instance).Preview3Url;
    }

    private void set_67_InfoListItemUC_Preview3Url(object instance, object Value)
    {
      ((InfoListItemUC) instance).Preview3Url = (string) Value;
    }

    private object get_68_InfoListItemUC_IsTiltEnabled(object instance)
    {
      return (object) ((InfoListItemUC) instance).IsTiltEnabled;
    }

    private void set_68_InfoListItemUC_IsTiltEnabled(object instance, object Value)
    {
      ((InfoListItemUC) instance).IsTiltEnabled = (bool) Value;
    }

    private object get_69_InfoListItemUC_Inlines(object instance)
    {
      return (object) ((InfoListItemUC) instance).Inlines;
    }

    private void set_69_InfoListItemUC_Inlines(object instance, object Value)
    {
      ((InfoListItemUC) instance).Inlines = (List<Inline>) Value;
    }

    private object get_70_ItemsBasePostUC_DataPost(object instance)
    {
      return (object) ((ItemsBasePostUC) instance).DataPost;
    }

    private void set_70_ItemsBasePostUC_DataPost(object instance, object Value)
    {
      ((ItemsBasePostUC) instance).DataPost = (VKWallPost) Value;
    }

    private object get_71_ItemsBasePostUC_DataNews(object instance)
    {
      return (object) ((ItemsBasePostUC) instance).DataNews;
    }

    private void set_71_ItemsBasePostUC_DataNews(object instance, object Value)
    {
      ((ItemsBasePostUC) instance).DataNews = (VKNewsfeedPost) Value;
    }

    private object get_72_LoadingUC_TryAgainCommand(object instance)
    {
      return (object) ((LoadingUC) instance).TryAgainCommand;
    }

    private void set_72_LoadingUC_TryAgainCommand(object instance, object Value)
    {
      ((LoadingUC) instance).TryAgainCommand = (Action) Value;
    }

    private object get_73_GroupsPage_ConversationVM(object instance)
    {
      return (object) ((GroupsPage) instance).ConversationVM;
    }

    private object get_74_MarketPage_VM(object instance) => (object) ((MarketPage) instance).VM;

    private object get_75_MusicPage_VM(object instance) => (object) ((MusicPage) instance).VM;

    private object get_76_ItemNotificationUC_Post(object instance)
    {
      return ((ItemNotificationUC) instance).Post;
    }

    private void set_76_ItemNotificationUC_Post(object instance, object Value)
    {
      ((ItemNotificationUC) instance).Post = Value;
    }

    private object get_77_NotificationsPage_ConversationVM(object instance)
    {
      return (object) ((NotificationsPage) instance).ConversationVM;
    }

    private object get_78_ItemComment_Data(object instance) => ((ItemComment) instance).Data;

    private void set_78_ItemComment_Data(object instance, object Value)
    {
      ((ItemComment) instance).Data = Value;
    }

    private object get_79_PhotoAlbumPage_AlbumsVM(object instance)
    {
      return (object) ((PhotoAlbumPage) instance).AlbumsVM;
    }

    private object get_80_PhotosPage_PhotosVM(object instance)
    {
      return (object) ((PhotosPage) instance).PhotosVM;
    }

    private object get_81_TestAddRemove_Items(object instance)
    {
      return (object) ((TestAddRemove) instance).Items;
    }

    private void set_81_TestAddRemove_Items(object instance, object Value)
    {
      ((TestAddRemove) instance).Items = (ObservableCollection<TestAddRemove.Temp>) Value;
    }

    private object get_82_Temp_id(object instance) => (object) ((TestAddRemove.Temp) instance).id;

    private void set_82_Temp_id(object instance, object Value)
    {
      ((TestAddRemove.Temp) instance).id = (int) Value;
    }

    private object get_83_Temp_content(object instance)
    {
      return (object) ((TestAddRemove.Temp) instance).content;
    }

    private void set_83_Temp_content(object instance, object Value)
    {
      ((TestAddRemove.Temp) instance).content = (string) Value;
    }

    private object get_84_UserPage_VM(object instance) => (object) ((UserPage) instance).VM;

    private object get_85_ToggleSwitch_IsChecked(object instance)
    {
      return (object) ((ToggleSwitch) instance).IsChecked;
    }

    private void set_85_ToggleSwitch_IsChecked(object instance, object Value)
    {
      ((ToggleSwitch) instance).IsChecked = (bool) Value;
    }

    private object get_86_ToggleSwitch_Title(object instance)
    {
      return (object) ((ToggleSwitch) instance).Title;
    }

    private void set_86_ToggleSwitch_Title(object instance, object Value)
    {
      ((ToggleSwitch) instance).Title = (string) Value;
    }

    private object get_87_ToggleSwitch_IsStateTextVisible(object instance)
    {
      return (object) ((ToggleSwitch) instance).IsStateTextVisible;
    }

    private void set_87_ToggleSwitch_IsStateTextVisible(object instance, object Value)
    {
      ((ToggleSwitch) instance).IsStateTextVisible = (bool) Value;
    }

    private object get_88_ToggleSwitch_Description(object instance)
    {
      return (object) ((ToggleSwitch) instance).Description;
    }

    private void set_88_ToggleSwitch_Description(object instance, object Value)
    {
      ((ToggleSwitch) instance).Description = (string) Value;
    }

    private object get_89_ToggleSwitch_StateTextOn(object instance)
    {
      return (object) ((ToggleSwitch) instance).StateTextOn;
    }

    private void set_89_ToggleSwitch_StateTextOn(object instance, object Value)
    {
      ((ToggleSwitch) instance).StateTextOn = (string) Value;
    }

    private object get_90_ToggleSwitch_StateTextOff(object instance)
    {
      return (object) ((ToggleSwitch) instance).StateTextOff;
    }

    private void set_90_ToggleSwitch_StateTextOff(object instance, object Value)
    {
      ((ToggleSwitch) instance).StateTextOff = (string) Value;
    }

    private object get_91_ToggleSwitch_BorderColor(object instance)
    {
      return (object) ((ToggleSwitch) instance).BorderColor;
    }

    private void set_91_ToggleSwitch_BorderColor(object instance, object Value)
    {
      ((ToggleSwitch) instance).BorderColor = (Brush) Value;
    }

    private object get_92_AppNotification2_Title(object instance)
    {
      return (object) ((AppNotification2) instance).Title;
    }

    private object get_93_ConversationsUC_ConversationsVM(object instance)
    {
      return (object) ((ConversationsUC) instance).ConversationsVM;
    }

    private object get_94_ConversationsUC_Scroll(object instance)
    {
      return (object) ((ConversationsUC) instance).Scroll;
    }

    private object get_95_ProgressRingUC_Progress(object instance)
    {
      return (object) ((ProgressRingUC) instance).Progress;
    }

    private void set_95_ProgressRingUC_Progress(object instance, object Value)
    {
      ((ProgressRingUC) instance).Progress = (double) Value;
    }

    private object get_96_GifViewerUC_UseOldGifPlayer(object instance)
    {
      return (object) ((GifViewerUC) instance).UseOldGifPlayer;
    }

    private object get_97_GifViewerUC_DownloadProgress(object instance)
    {
      return (object) ((GifViewerUC) instance).DownloadProgress;
    }

    private void set_97_GifViewerUC_DownloadProgress(object instance, object Value)
    {
      ((GifViewerUC) instance).DownloadProgress = (double) Value;
    }

    private object get_98_MenuItemUC_Icon(object instance) => (object) ((MenuItemUC) instance).Icon;

    private void set_98_MenuItemUC_Icon(object instance, object Value)
    {
      ((MenuItemUC) instance).Icon = (string) Value;
    }

    private object get_99_MenuItemUC_Title(object instance)
    {
      return (object) ((MenuItemUC) instance).Title;
    }

    private void set_99_MenuItemUC_Title(object instance, object Value)
    {
      ((MenuItemUC) instance).Title = (string) Value;
    }

    private object get_100_ImageViewerDecoratorUC_CurrentImage(object instance)
    {
      return (object) ((ImageViewerDecoratorUC) instance).CurrentImage;
    }

    private object get_101_ImageViewerDecoratorUC_CurrentImageFitRectOriginal(object instance)
    {
      return (object) ((ImageViewerDecoratorUC) instance).CurrentImageFitRectOriginal;
    }

    private object get_102_ImageViewerDecoratorUC_CurrentImageFitRectTransformed(object instance)
    {
      return (object) ((ImageViewerDecoratorUC) instance).CurrentImageFitRectTransformed;
    }

    private object get_103_ScrollableTextBlock_Text(object instance)
    {
      return (object) ((ScrollableTextBlock) instance).Text;
    }

    private void set_103_ScrollableTextBlock_Text(object instance, object Value)
    {
      ((ScrollableTextBlock) instance).Text = (string) Value;
    }

    private object get_104_ScrollableTextBlock_Foreground(object instance)
    {
      return (object) ((ScrollableTextBlock) instance).Foreground;
    }

    private void set_104_ScrollableTextBlock_Foreground(object instance, object Value)
    {
      ((ScrollableTextBlock) instance).Foreground = (Brush) Value;
    }

    private object get_105_ScrollableTextBlock_FullOnly(object instance)
    {
      return (object) ((ScrollableTextBlock) instance).FullOnly;
    }

    private void set_105_ScrollableTextBlock_FullOnly(object instance, object Value)
    {
      ((ScrollableTextBlock) instance).FullOnly = (bool) Value;
    }

    private object get_106_ItemNewsFeedUC_Post(object instance) => ((ItemNewsFeedUC) instance).Post;

    private void set_106_ItemNewsFeedUC_Post(object instance, object Value)
    {
      ((ItemNewsFeedUC) instance).Post = Value;
    }

    private object get_107_SwipeThroughControl_Items(object instance)
    {
      return (object) ((SwipeThroughControl) instance).Items;
    }

    private void set_107_SwipeThroughControl_Items(object instance, object Value)
    {
      ((SwipeThroughControl) instance).Items = (ObservableCollection<object>) Value;
    }

    private object get_108_OutboundAttachmentTemplateSelector_PhotoTemplate(object instance)
    {
      return (object) ((OutboundAttachmentTemplateSelector) instance).PhotoTemplate;
    }

    private void set_108_OutboundAttachmentTemplateSelector_PhotoTemplate(
      object instance,
      object Value)
    {
      ((OutboundAttachmentTemplateSelector) instance).PhotoTemplate = (DataTemplate) Value;
    }

    private object get_109_OutboundAttachmentTemplateSelector_ForwardedMessageTemplate(
      object instance)
    {
      return (object) ((OutboundAttachmentTemplateSelector) instance).ForwardedMessageTemplate;
    }

    private void set_109_OutboundAttachmentTemplateSelector_ForwardedMessageTemplate(
      object instance,
      object Value)
    {
      ((OutboundAttachmentTemplateSelector) instance).ForwardedMessageTemplate = (DataTemplate) Value;
    }

    private object get_110_OutboundAttachmentTemplateSelector_GenericIconTemplate(object instance)
    {
      return (object) ((OutboundAttachmentTemplateSelector) instance).GenericIconTemplate;
    }

    private void set_110_OutboundAttachmentTemplateSelector_GenericIconTemplate(
      object instance,
      object Value)
    {
      ((OutboundAttachmentTemplateSelector) instance).GenericIconTemplate = (DataTemplate) Value;
    }

    private object get_111_OutboundAttachmentTemplateSelector_GeoTemplate(object instance)
    {
      return (object) ((OutboundAttachmentTemplateSelector) instance).GeoTemplate;
    }

    private void set_111_OutboundAttachmentTemplateSelector_GeoTemplate(
      object instance,
      object Value)
    {
      ((OutboundAttachmentTemplateSelector) instance).GeoTemplate = (DataTemplate) Value;
    }

    private object get_112_OutboundAttachmentTemplateSelector_VideoTemplate(object instance)
    {
      return (object) ((OutboundAttachmentTemplateSelector) instance).VideoTemplate;
    }

    private void set_112_OutboundAttachmentTemplateSelector_VideoTemplate(
      object instance,
      object Value)
    {
      ((OutboundAttachmentTemplateSelector) instance).VideoTemplate = (DataTemplate) Value;
    }

    private object get_113_OutboundAttachmentTemplateSelector_AudioTemplate(object instance)
    {
      return (object) ((OutboundAttachmentTemplateSelector) instance).AudioTemplate;
    }

    private void set_113_OutboundAttachmentTemplateSelector_AudioTemplate(
      object instance,
      object Value)
    {
      ((OutboundAttachmentTemplateSelector) instance).AudioTemplate = (DataTemplate) Value;
    }

    private object get_114_OutboundAttachmentTemplateSelector_DocumentTemplate(object instance)
    {
      return (object) ((OutboundAttachmentTemplateSelector) instance).DocumentTemplate;
    }

    private void set_114_OutboundAttachmentTemplateSelector_DocumentTemplate(
      object instance,
      object Value)
    {
      ((OutboundAttachmentTemplateSelector) instance).DocumentTemplate = (DataTemplate) Value;
    }

    private object get_115_OutboundAttachmentTemplateSelector_GenericThumbTemplate(object instance)
    {
      return (object) ((OutboundAttachmentTemplateSelector) instance).GenericThumbTemplate;
    }

    private void set_115_OutboundAttachmentTemplateSelector_GenericThumbTemplate(
      object instance,
      object Value)
    {
      ((OutboundAttachmentTemplateSelector) instance).GenericThumbTemplate = (DataTemplate) Value;
    }

    private object get_116_OutboundAttachmentTemplateSelector_AddAttachmentTemplate(object instance)
    {
      return (object) ((OutboundAttachmentTemplateSelector) instance).AddAttachmentTemplate;
    }

    private void set_116_OutboundAttachmentTemplateSelector_AddAttachmentTemplate(
      object instance,
      object Value)
    {
      ((OutboundAttachmentTemplateSelector) instance).AddAttachmentTemplate = (DataTemplate) Value;
    }

    private object get_117_OutboundAttachmentTemplateSelector_WallPostTemplate(object instance)
    {
      return (object) ((OutboundAttachmentTemplateSelector) instance).WallPostTemplate;
    }

    private void set_117_OutboundAttachmentTemplateSelector_WallPostTemplate(
      object instance,
      object Value)
    {
      ((OutboundAttachmentTemplateSelector) instance).WallPostTemplate = (DataTemplate) Value;
    }

    private object get_118_NewsSourceListItemUC_Icon(object instance)
    {
      return (object) ((NewsSourceListItemUC) instance).Icon;
    }

    private void set_118_NewsSourceListItemUC_Icon(object instance, object Value)
    {
      ((NewsSourceListItemUC) instance).Icon = (string) Value;
    }

    private object get_119_NewsSourceListItemUC_Title(object instance)
    {
      return (object) ((NewsSourceListItemUC) instance).Title;
    }

    private void set_119_NewsSourceListItemUC_Title(object instance, object Value)
    {
      ((NewsSourceListItemUC) instance).Title = (string) Value;
    }

    private object get_120_ImageExtensions_CacheUriImageBrush(object instance)
    {
      return (object) ImageExtensions.GetCacheUriImageBrush((ImageBrush) instance);
    }

    private void set_120_ImageExtensions_CacheUriImageBrush(object instance, object Value)
    {
      ImageExtensions.SetCacheUriImageBrush((ImageBrush) instance, (Uri) Value);
    }

    private object get_121_ImageExtensions_CacheUri(object instance)
    {
      return (object) ImageExtensions.GetCacheUri((DependencyObject) instance);
    }

    private void set_121_ImageExtensions_CacheUri(object instance, object Value)
    {
      ImageExtensions.SetCacheUri((DependencyObject) instance, (Uri) Value);
    }

    private IXamlMember CreateXamlMember(string longMemberName)
    {
      XamlMember xamlMember = (XamlMember) null;
      switch (longMemberName)
      {
        case "Windows.UI.Color.A":
          XamlUserType xamlTypeByName1 = (XamlUserType) this.GetXamlTypeByName("Windows.UI.Color");
          xamlMember = new XamlMember(this, "A", "Byte");
          xamlMember.Getter = new Getter(this.get_0_Color_A);
          xamlMember.Setter = new Setter(this.set_0_Color_A);
          break;
        case "Windows.UI.Color.B":
          XamlUserType xamlTypeByName2 = (XamlUserType) this.GetXamlTypeByName("Windows.UI.Color");
          xamlMember = new XamlMember(this, "B", "Byte");
          xamlMember.Getter = new Getter(this.get_1_Color_B);
          xamlMember.Setter = new Setter(this.set_1_Color_B);
          break;
        case "Windows.UI.Color.G":
          XamlUserType xamlTypeByName3 = (XamlUserType) this.GetXamlTypeByName("Windows.UI.Color");
          xamlMember = new XamlMember(this, "G", "Byte");
          xamlMember.Getter = new Getter(this.get_2_Color_G);
          xamlMember.Setter = new Setter(this.set_2_Color_G);
          break;
        case "Windows.UI.Color.R":
          XamlUserType xamlTypeByName4 = (XamlUserType) this.GetXamlTypeByName("Windows.UI.Color");
          xamlMember = new XamlMember(this, "R", "Byte");
          xamlMember.Getter = new Getter(this.get_3_Color_R);
          xamlMember.Setter = new Setter(this.set_3_Color_R);
          break;
        case "App1uwp.Framework.CustomFrame.CommandBar":
          XamlUserType xamlTypeByName5 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Framework.CustomFrame");
          xamlMember = new XamlMember(this, "CommandBar", "App1uwp.Framework.CommandBar");
          xamlMember.Getter = new Getter(this.get_4_CustomFrame_CommandBar);
          xamlMember.SetIsReadOnly();
          break;
        case "App1uwp.Framework.CustomFrame.NotificationsPanel":
          XamlUserType xamlTypeByName6 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Framework.CustomFrame");
          xamlMember = new XamlMember(this, "NotificationsPanel", "App1uwp.UC.NotificationsPanel");
          xamlMember.Getter = new Getter(this.get_5_CustomFrame_NotificationsPanel);
          xamlMember.SetIsReadOnly();
          break;
        case "App1uwp.Framework.CustomFrame.HeaderWithMenu":
          XamlUserType xamlTypeByName7 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Framework.CustomFrame");
          xamlMember = new XamlMember(this, "HeaderWithMenu", "App1uwp.UC.HeaderWithMenuUC");
          xamlMember.Getter = new Getter(this.get_6_CustomFrame_HeaderWithMenu);
          xamlMember.SetIsReadOnly();
          break;
        case "App1uwp.Framework.CustomFrame.MenuState":
          XamlUserType xamlTypeByName8 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Framework.CustomFrame");
          xamlMember = new XamlMember(this, "MenuState", "App1uwp.Framework.CustomFrame.MenuStates");
          xamlMember.Getter = new Getter(this.get_7_CustomFrame_MenuState);
          xamlMember.SetIsReadOnly();
          break;
        case "App1uwp.Framework.CustomFrame.OverlayGrid":
          XamlUserType xamlTypeByName9 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Framework.CustomFrame");
          xamlMember = new XamlMember(this, "OverlayGrid", "Windows.UI.Xaml.Controls.Grid");
          xamlMember.Getter = new Getter(this.get_8_CustomFrame_OverlayGrid);
          xamlMember.SetIsReadOnly();
          break;
        case "App1uwp.Framework.CustomFrame.IsDevicePhone":
          XamlUserType xamlTypeByName10 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Framework.CustomFrame");
          xamlMember = new XamlMember(this, "IsDevicePhone", "Boolean");
          xamlMember.Getter = new Getter(this.get_9_CustomFrame_IsDevicePhone);
          xamlMember.SetIsReadOnly();
          break;
        case "App1uwp.Framework.CustomFrame.HeaderContent":
          XamlUserType xamlTypeByName11 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Framework.CustomFrame");
          xamlMember = new XamlMember(this, "HeaderContent", "Windows.UI.Xaml.FrameworkElement");
          xamlMember.SetTargetTypeName("Windows.UI.Xaml.DependencyObject");
          xamlMember.SetIsAttachable();
          xamlMember.Getter = new Getter(this.get_10_CustomFrame_HeaderContent);
          xamlMember.Setter = new Setter(this.set_10_CustomFrame_HeaderContent);
          break;
        case "App1uwp.Framework.ExtendedListView2.ContentItemsPresenter":
          XamlUserType xamlTypeByName12 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Framework.ExtendedListView2");
          xamlMember = new XamlMember(this, "ContentItemsPresenter", "Windows.UI.Xaml.Controls.ItemsPresenter");
          xamlMember.Getter = new Getter(this.get_11_ExtendedListView2_ContentItemsPresenter);
          xamlMember.SetIsReadOnly();
          break;
        case "App1uwp.Framework.ExtendedListView2.IsFlat":
          XamlUserType xamlTypeByName13 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Framework.ExtendedListView2");
          xamlMember = new XamlMember(this, "IsFlat", "Boolean");
          xamlMember.SetIsDependencyProperty();
          xamlMember.Getter = new Getter(this.get_12_ExtendedListView2_IsFlat);
          xamlMember.Setter = new Setter(this.set_12_ExtendedListView2_IsFlat);
          break;
        case "App1uwp.Framework.ExtendedListView2.ReversPull":
          XamlUserType xamlTypeByName14 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Framework.ExtendedListView2");
          xamlMember = new XamlMember(this, "ReversPull", "Boolean");
          xamlMember.SetIsDependencyProperty();
          xamlMember.Getter = new Getter(this.get_13_ExtendedListView2_ReversPull);
          xamlMember.Setter = new Setter(this.set_13_ExtendedListView2_ReversPull);
          break;
        case "App1uwp.Framework.ExtendedListView2.IsPullEnabled":
          XamlUserType xamlTypeByName15 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Framework.ExtendedListView2");
          xamlMember = new XamlMember(this, "IsPullEnabled", "Boolean");
          xamlMember.SetIsDependencyProperty();
          xamlMember.Getter = new Getter(this.get_14_ExtendedListView2_IsPullEnabled);
          xamlMember.Setter = new Setter(this.set_14_ExtendedListView2_IsPullEnabled);
          break;
        case "App1uwp.Framework.ExtendedListView2.UseHeaderOffset":
          XamlUserType xamlTypeByName16 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Framework.ExtendedListView2");
          xamlMember = new XamlMember(this, "UseHeaderOffset", "Boolean");
          xamlMember.SetIsDependencyProperty();
          xamlMember.Getter = new Getter(this.get_15_ExtendedListView2_UseHeaderOffset);
          xamlMember.Setter = new Setter(this.set_15_ExtendedListView2_UseHeaderOffset);
          break;
        case "App1uwp.Framework.ExtendedListView2.UseFooterOffset":
          XamlUserType xamlTypeByName17 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Framework.ExtendedListView2");
          xamlMember = new XamlMember(this, "UseFooterOffset", "Boolean");
          xamlMember.SetIsDependencyProperty();
          xamlMember.Getter = new Getter(this.get_16_ExtendedListView2_UseFooterOffset);
          xamlMember.Setter = new Setter(this.set_16_ExtendedListView2_UseFooterOffset);
          break;
        case "App1uwp.Framework.ExtendedListView2.Header":
          XamlUserType xamlTypeByName18 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Framework.ExtendedListView2");
          xamlMember = new XamlMember(this, "Header", "Object");
          xamlMember.SetIsDependencyProperty();
          xamlMember.Getter = new Getter(this.get_17_ExtendedListView2_Header);
          xamlMember.Setter = new Setter(this.set_17_ExtendedListView2_Header);
          break;
        case "App1uwp.Framework.ExtendedListView2.Footer":
          XamlUserType xamlTypeByName19 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Framework.ExtendedListView2");
          xamlMember = new XamlMember(this, "Footer", "Object");
          xamlMember.SetIsDependencyProperty();
          xamlMember.Getter = new Getter(this.get_18_ExtendedListView2_Footer);
          xamlMember.Setter = new Setter(this.set_18_ExtendedListView2_Footer);
          break;
        case "App1uwp.Framework.ExtendedListView2.GetInsideScrollViewer":
          XamlUserType xamlTypeByName20 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Framework.ExtendedListView2");
          xamlMember = new XamlMember(this, "GetInsideScrollViewer", "Windows.UI.Xaml.Controls.ScrollViewer");
          xamlMember.Getter = new Getter(this.get_19_ExtendedListView2_GetInsideScrollViewer);
          xamlMember.SetIsReadOnly();
          break;
        case "App1uwp.Framework.ExtendedListView2.GetListView":
          XamlUserType xamlTypeByName21 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Framework.ExtendedListView2");
          xamlMember = new XamlMember(this, "GetListView", "Windows.UI.Xaml.Controls.ListView");
          xamlMember.Getter = new Getter(this.get_20_ExtendedListView2_GetListView);
          xamlMember.SetIsReadOnly();
          break;
        case "App1uwp.Framework.ExtendedListView2.OnPullPercentageChanged":
          XamlUserType xamlTypeByName22 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Framework.ExtendedListView2");
          xamlMember = new XamlMember(this, "OnPullPercentageChanged", "System.Action`1<Double>");
          xamlMember.Getter = new Getter(this.get_21_ExtendedListView2_OnPullPercentageChanged);
          xamlMember.Setter = new Setter(this.set_21_ExtendedListView2_OnPullPercentageChanged);
          break;
        case "App1uwp.Framework.CommandBar.PrimaryCommands":
          XamlUserType xamlTypeByName23 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Framework.CommandBar");
          xamlMember = new XamlMember(this, "PrimaryCommands", "System.Collections.ObjectModel.ObservableCollection`1<App1uwp.Framework.CommandBarButton>");
          xamlMember.SetIsDependencyProperty();
          xamlMember.Getter = new Getter(this.get_22_CommandBar_PrimaryCommands);
          xamlMember.Setter = new Setter(this.set_22_CommandBar_PrimaryCommands);
          break;
        case "App1uwp.Framework.CommandBarButton.Label":
          XamlUserType xamlTypeByName24 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Framework.CommandBarButton");
          xamlMember = new XamlMember(this, "Label", "String");
          xamlMember.Getter = new Getter(this.get_23_CommandBarButton_Label);
          xamlMember.Setter = new Setter(this.set_23_CommandBarButton_Label);
          break;
        case "App1uwp.Framework.CommandBarButton.Icon":
          XamlUserType xamlTypeByName25 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Framework.CommandBarButton");
          xamlMember = new XamlMember(this, "Icon", "String");
          xamlMember.Getter = new Getter(this.get_24_CommandBarButton_Icon);
          xamlMember.Setter = new Setter(this.set_24_CommandBarButton_Icon);
          break;
        case "App1uwp.Framework.CommandBarButton.Command":
          XamlUserType xamlTypeByName26 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Framework.CommandBarButton");
          xamlMember = new XamlMember(this, "Command", "System.Windows.Input.ICommand");
          xamlMember.Getter = new Getter(this.get_25_CommandBarButton_Command);
          xamlMember.Setter = new Setter(this.set_25_CommandBarButton_Command);
          break;
        case "App1uwp.Framework.CommandBar.SecondaryCommands":
          XamlUserType xamlTypeByName27 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Framework.CommandBar");
          xamlMember = new XamlMember(this, "SecondaryCommands", "System.Collections.ObjectModel.ObservableCollection`1<App1uwp.Framework.CommandBarButton>");
          xamlMember.SetIsDependencyProperty();
          xamlMember.Getter = new Getter(this.get_26_CommandBar_SecondaryCommands);
          xamlMember.Setter = new Setter(this.set_26_CommandBar_SecondaryCommands);
          break;
        case "App1uwp.Framework.CommandBar.IsMenuOpened":
          XamlUserType xamlTypeByName28 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Framework.CommandBar");
          xamlMember = new XamlMember(this, "IsMenuOpened", "Boolean");
          xamlMember.Getter = new Getter(this.get_27_CommandBar_IsMenuOpened);
          xamlMember.SetIsReadOnly();
          break;
        case "App1uwp.Framework.CommandBar.IsMenuHiden":
          XamlUserType xamlTypeByName29 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Framework.CommandBar");
          xamlMember = new XamlMember(this, "IsMenuHiden", "Boolean");
          xamlMember.Getter = new Getter(this.get_28_CommandBar_IsMenuHiden);
          xamlMember.SetIsReadOnly();
          break;
        case "App1uwp.UC.HeaderWithMenuUC.OptionsMenu":
          XamlUserType xamlTypeByName30 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.HeaderWithMenuUC");
          xamlMember = new XamlMember(this, "OptionsMenu", "System.Collections.ObjectModel.ObservableCollection`1<App1uwp.Library.OptionsMenuItem>");
          xamlMember.Getter = new Getter(this.get_29_HeaderWithMenuUC_OptionsMenu);
          xamlMember.Setter = new Setter(this.set_29_HeaderWithMenuUC_OptionsMenu);
          break;
        case "App1uwp.Library.OptionsMenuItem.Icon":
          XamlUserType xamlTypeByName31 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Library.OptionsMenuItem");
          xamlMember = new XamlMember(this, "Icon", "String");
          xamlMember.Getter = new Getter(this.get_30_OptionsMenuItem_Icon);
          xamlMember.Setter = new Setter(this.set_30_OptionsMenuItem_Icon);
          break;
        case "App1uwp.UC.HeaderWithMenuUC.MainContent":
          XamlUserType xamlTypeByName32 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.HeaderWithMenuUC");
          xamlMember = new XamlMember(this, "MainContent", "Windows.UI.Xaml.Controls.Grid");
          xamlMember.Getter = new Getter(this.get_31_HeaderWithMenuUC_MainContent);
          xamlMember.SetIsReadOnly();
          break;
        case "App1uwp.UC.HeaderWithMenuUC.SubContent":
          XamlUserType xamlTypeByName33 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.HeaderWithMenuUC");
          xamlMember = new XamlMember(this, "SubContent", "Windows.UI.Xaml.Controls.Grid");
          xamlMember.Getter = new Getter(this.get_32_HeaderWithMenuUC_SubContent);
          xamlMember.SetIsReadOnly();
          break;
        case "App1uwp.UC.HeaderWithMenuUC.HeaderHeight":
          XamlUserType xamlTypeByName34 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.HeaderWithMenuUC");
          xamlMember = new XamlMember(this, "HeaderHeight", "Double");
          xamlMember.Getter = new Getter(this.get_33_HeaderWithMenuUC_HeaderHeight);
          xamlMember.SetIsReadOnly();
          break;
        case "App1uwp.UC.HeaderWithMenuUC.BackBackground":
          XamlUserType xamlTypeByName35 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.HeaderWithMenuUC");
          xamlMember = new XamlMember(this, "BackBackground", "Windows.UI.Xaml.Controls.Grid");
          xamlMember.Getter = new Getter(this.get_34_HeaderWithMenuUC_BackBackground);
          xamlMember.SetIsReadOnly();
          break;
        case "App1uwp.UC.HeaderWithMenuUC.HideSandwitchButton":
          XamlUserType xamlTypeByName36 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.HeaderWithMenuUC");
          xamlMember = new XamlMember(this, "HideSandwitchButton", "Boolean");
          xamlMember.Getter = new Getter(this.get_35_HeaderWithMenuUC_HideSandwitchButton);
          xamlMember.Setter = new Setter(this.set_35_HeaderWithMenuUC_HideSandwitchButton);
          break;
        case "App1uwp.UC.HeaderWithMenuUC.PullToRefresh":
          XamlUserType xamlTypeByName37 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.HeaderWithMenuUC");
          xamlMember = new XamlMember(this, "PullToRefresh", "App1uwp.UC.PullToRefreshUC");
          xamlMember.Getter = new Getter(this.get_36_HeaderWithMenuUC_PullToRefresh);
          xamlMember.SetIsReadOnly();
          break;
        case "App1uwp.UC.HeaderWithMenuUC.HeaderGrid":
          XamlUserType xamlTypeByName38 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.HeaderWithMenuUC");
          xamlMember = new XamlMember(this, "HeaderGrid", "Windows.UI.Xaml.Controls.Grid");
          xamlMember.Getter = new Getter(this.get_37_HeaderWithMenuUC_HeaderGrid);
          xamlMember.SetIsReadOnly();
          break;
        case "App1uwp.UC.NewMessageUC.PanelControl":
          XamlUserType xamlTypeByName39 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.NewMessageUC");
          xamlMember = new XamlMember(this, "PanelControl", "App1uwp.UC.SwipeThroughControl");
          xamlMember.Getter = new Getter(this.get_38_NewMessageUC_PanelControl);
          xamlMember.SetIsReadOnly();
          break;
        case "App1uwp.UC.NewMessageUC.TextBoxNewComment":
          XamlUserType xamlTypeByName40 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.NewMessageUC");
          xamlMember = new XamlMember(this, "TextBoxNewComment", "Windows.UI.Xaml.Controls.TextBox");
          xamlMember.Getter = new Getter(this.get_39_NewMessageUC_TextBoxNewComment);
          xamlMember.SetIsReadOnly();
          break;
        case "App1uwp.UC.NewMessageUC.BorderSend":
          XamlUserType xamlTypeByName41 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.NewMessageUC");
          xamlMember = new XamlMember(this, "BorderSend", "Windows.UI.Xaml.Controls.Border");
          xamlMember.Getter = new Getter(this.get_40_NewMessageUC_BorderSend);
          xamlMember.SetIsReadOnly();
          break;
        case "App1uwp.UC.NewMessageUC.BorderAttach":
          XamlUserType xamlTypeByName42 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.NewMessageUC");
          xamlMember = new XamlMember(this, "BorderAttach", "Windows.UI.Xaml.Controls.Grid");
          xamlMember.Getter = new Getter(this.get_41_NewMessageUC_BorderAttach);
          xamlMember.SetIsReadOnly();
          break;
        case "App1uwp.UC.NewMessageUC.MentionPicker":
          XamlUserType xamlTypeByName43 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.NewMessageUC");
          xamlMember = new XamlMember(this, "MentionPicker", "Windows.UI.Xaml.Controls.ListView");
          xamlMember.Getter = new Getter(this.get_42_NewMessageUC_MentionPicker);
          xamlMember.SetIsReadOnly();
          break;
        case "App1uwp.UC.ItemMessageUC.Data":
          XamlUserType xamlTypeByName44 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.ItemMessageUC");
          xamlMember = new XamlMember(this, "Data", "Object");
          xamlMember.SetIsDependencyProperty();
          xamlMember.Getter = new Getter(this.get_43_ItemMessageUC_Data);
          xamlMember.Setter = new Setter(this.set_43_ItemMessageUC_Data);
          break;
        case "App1uwp.ConversationPage.ConversationVM":
          XamlUserType xamlTypeByName45 = (XamlUserType) this.GetXamlTypeByName("App1uwp.ConversationPage");
          xamlMember = new XamlMember(this, "ConversationVM", "App1uwp.Network.ViewModels.DialogHistoryViewModel");
          xamlMember.Getter = new Getter(this.get_44_ConversationPage_ConversationVM);
          xamlMember.SetIsReadOnly();
          break;
        case "App1uwp.ConversationPage.ChatMembers":
          XamlUserType xamlTypeByName46 = (XamlUserType) this.GetXamlTypeByName("App1uwp.ConversationPage");
          xamlMember = new XamlMember(this, "ChatMembers", "System.Collections.ObjectModel.ObservableCollection`1<App1uwp.Network.DataObjects.VKBaseDataForGroupOrUser>");
          xamlMember.Getter = new Getter(this.get_45_ConversationPage_ChatMembers);
          xamlMember.Setter = new Setter(this.set_45_ConversationPage_ChatMembers);
          break;
        case "App1uwp.Network.DataObjects.VKBaseDataForGroupOrUser.id":
          XamlUserType xamlTypeByName47 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Network.DataObjects.VKBaseDataForGroupOrUser");
          xamlMember = new XamlMember(this, "id", "Int32");
          xamlMember.Getter = new Getter(this.get_46_VKBaseDataForGroupOrUser_id);
          xamlMember.Setter = new Setter(this.set_46_VKBaseDataForGroupOrUser_id);
          break;
        case "App1uwp.Network.DataObjects.VKBaseDataForGroupOrUser.deactivated":
          XamlUserType xamlTypeByName48 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Network.DataObjects.VKBaseDataForGroupOrUser");
          xamlMember = new XamlMember(this, "deactivated", "App1uwp.Network.Enums.VKIsDeactivated");
          xamlMember.Getter = new Getter(this.get_47_VKBaseDataForGroupOrUser_deactivated);
          xamlMember.Setter = new Setter(this.set_47_VKBaseDataForGroupOrUser_deactivated);
          break;
        case "App1uwp.Network.DataObjects.VKBaseDataForGroupOrUser.can_post":
          XamlUserType xamlTypeByName49 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Network.DataObjects.VKBaseDataForGroupOrUser");
          xamlMember = new XamlMember(this, "can_post", "Boolean");
          xamlMember.Getter = new Getter(this.get_48_VKBaseDataForGroupOrUser_can_post);
          xamlMember.Setter = new Setter(this.set_48_VKBaseDataForGroupOrUser_can_post);
          break;
        case "App1uwp.Network.DataObjects.VKBaseDataForGroupOrUser.can_see_all_posts":
          XamlUserType xamlTypeByName50 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Network.DataObjects.VKBaseDataForGroupOrUser");
          xamlMember = new XamlMember(this, "can_see_all_posts", "Boolean");
          xamlMember.Getter = new Getter(this.get_49_VKBaseDataForGroupOrUser_can_see_all_posts);
          xamlMember.Setter = new Setter(this.set_49_VKBaseDataForGroupOrUser_can_see_all_posts);
          break;
        case "App1uwp.Network.DataObjects.VKBaseDataForGroupOrUser.counters":
          XamlUserType xamlTypeByName51 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Network.DataObjects.VKBaseDataForGroupOrUser");
          xamlMember = new XamlMember(this, "counters", "App1uwp.Network.DataObjects.VKCounters");
          xamlMember.Getter = new Getter(this.get_50_VKBaseDataForGroupOrUser_counters);
          xamlMember.Setter = new Setter(this.set_50_VKBaseDataForGroupOrUser_counters);
          break;
        case "App1uwp.Network.DataObjects.VKBaseDataForGroupOrUser.is_favorite":
          XamlUserType xamlTypeByName52 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Network.DataObjects.VKBaseDataForGroupOrUser");
          xamlMember = new XamlMember(this, "is_favorite", "Boolean");
          xamlMember.Getter = new Getter(this.get_51_VKBaseDataForGroupOrUser_is_favorite);
          xamlMember.Setter = new Setter(this.set_51_VKBaseDataForGroupOrUser_is_favorite);
          break;
        case "App1uwp.Network.DataObjects.VKBaseDataForGroupOrUser.is_hidden_from_feed":
          XamlUserType xamlTypeByName53 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Network.DataObjects.VKBaseDataForGroupOrUser");
          xamlMember = new XamlMember(this, "is_hidden_from_feed", "Boolean");
          xamlMember.Getter = new Getter(this.get_52_VKBaseDataForGroupOrUser_is_hidden_from_feed);
          xamlMember.Setter = new Setter(this.set_52_VKBaseDataForGroupOrUser_is_hidden_from_feed);
          break;
        case "App1uwp.Network.DataObjects.VKBaseDataForGroupOrUser.photo_50":
          XamlUserType xamlTypeByName54 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Network.DataObjects.VKBaseDataForGroupOrUser");
          xamlMember = new XamlMember(this, "photo_50", "String");
          xamlMember.Getter = new Getter(this.get_53_VKBaseDataForGroupOrUser_photo_50);
          xamlMember.Setter = new Setter(this.set_53_VKBaseDataForGroupOrUser_photo_50);
          break;
        case "App1uwp.Network.DataObjects.VKBaseDataForGroupOrUser.photo_100":
          XamlUserType xamlTypeByName55 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Network.DataObjects.VKBaseDataForGroupOrUser");
          xamlMember = new XamlMember(this, "photo_100", "String");
          xamlMember.Getter = new Getter(this.get_54_VKBaseDataForGroupOrUser_photo_100);
          xamlMember.Setter = new Setter(this.set_54_VKBaseDataForGroupOrUser_photo_100);
          break;
        case "App1uwp.Network.DataObjects.VKBaseDataForGroupOrUser.photo_200":
          XamlUserType xamlTypeByName56 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Network.DataObjects.VKBaseDataForGroupOrUser");
          xamlMember = new XamlMember(this, "photo_200", "String");
          xamlMember.Getter = new Getter(this.get_55_VKBaseDataForGroupOrUser_photo_200);
          xamlMember.Setter = new Setter(this.set_55_VKBaseDataForGroupOrUser_photo_200);
          break;
        case "App1uwp.Network.DataObjects.VKBaseDataForGroupOrUser.verified":
          XamlUserType xamlTypeByName57 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Network.DataObjects.VKBaseDataForGroupOrUser");
          xamlMember = new XamlMember(this, "verified", "Boolean");
          xamlMember.Getter = new Getter(this.get_56_VKBaseDataForGroupOrUser_verified);
          xamlMember.Setter = new Setter(this.set_56_VKBaseDataForGroupOrUser_verified);
          break;
        case "App1uwp.Network.DataObjects.VKBaseDataForGroupOrUser.Title":
          XamlUserType xamlTypeByName58 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Network.DataObjects.VKBaseDataForGroupOrUser");
          xamlMember = new XamlMember(this, "Title", "String");
          xamlMember.Getter = new Getter(this.get_57_VKBaseDataForGroupOrUser_Title);
          xamlMember.Setter = new Setter(this.set_57_VKBaseDataForGroupOrUser_Title);
          break;
        case "App1uwp.Network.DataObjects.VKBaseDataForGroupOrUser.domain":
          XamlUserType xamlTypeByName59 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Network.DataObjects.VKBaseDataForGroupOrUser");
          xamlMember = new XamlMember(this, "domain", "String");
          xamlMember.Getter = new Getter(this.get_58_VKBaseDataForGroupOrUser_domain);
          xamlMember.Setter = new Setter(this.set_58_VKBaseDataForGroupOrUser_domain);
          break;
        case "App1uwp.FriendsPage.VM":
          XamlUserType xamlTypeByName60 = (XamlUserType) this.GetXamlTypeByName("App1uwp.FriendsPage");
          xamlMember = new XamlMember(this, "VM", "App1uwp.Network.ViewModels.FriendsViewModel");
          xamlMember.Getter = new Getter(this.get_59_FriendsPage_VM);
          xamlMember.SetIsReadOnly();
          break;
        case "App1uwp.UC.SettingsSectionUC.Icon":
          XamlUserType xamlTypeByName61 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.SettingsSectionUC");
          xamlMember = new XamlMember(this, "Icon", "String");
          xamlMember.Getter = new Getter(this.get_60_SettingsSectionUC_Icon);
          xamlMember.Setter = new Setter(this.set_60_SettingsSectionUC_Icon);
          break;
        case "App1uwp.UC.SettingsSectionUC.Title":
          XamlUserType xamlTypeByName62 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.SettingsSectionUC");
          xamlMember = new XamlMember(this, "Title", "String");
          xamlMember.Getter = new Getter(this.get_61_SettingsSectionUC_Title);
          xamlMember.Setter = new Setter(this.set_61_SettingsSectionUC_Title);
          break;
        case "App1uwp.UC.SettingsSectionUC.SubTitle":
          XamlUserType xamlTypeByName63 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.SettingsSectionUC");
          xamlMember = new XamlMember(this, "SubTitle", "String");
          xamlMember.Getter = new Getter(this.get_62_SettingsSectionUC_SubTitle);
          xamlMember.Setter = new Setter(this.set_62_SettingsSectionUC_SubTitle);
          break;
        case "App1uwp.UC.InfoListItemUC.IconUrl":
          XamlUserType xamlTypeByName64 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.InfoListItemUC");
          xamlMember = new XamlMember(this, "IconUrl", "String");
          xamlMember.SetIsDependencyProperty();
          xamlMember.Getter = new Getter(this.get_63_InfoListItemUC_IconUrl);
          xamlMember.Setter = new Setter(this.set_63_InfoListItemUC_IconUrl);
          break;
        case "App1uwp.UC.InfoListItemUC.Text":
          XamlUserType xamlTypeByName65 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.InfoListItemUC");
          xamlMember = new XamlMember(this, "Text", "String");
          xamlMember.SetIsDependencyProperty();
          xamlMember.Getter = new Getter(this.get_64_InfoListItemUC_Text);
          xamlMember.Setter = new Setter(this.set_64_InfoListItemUC_Text);
          break;
        case "App1uwp.UC.InfoListItemUC.Preview1Url":
          XamlUserType xamlTypeByName66 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.InfoListItemUC");
          xamlMember = new XamlMember(this, "Preview1Url", "String");
          xamlMember.SetIsDependencyProperty();
          xamlMember.Getter = new Getter(this.get_65_InfoListItemUC_Preview1Url);
          xamlMember.Setter = new Setter(this.set_65_InfoListItemUC_Preview1Url);
          break;
        case "App1uwp.UC.InfoListItemUC.Preview2Url":
          XamlUserType xamlTypeByName67 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.InfoListItemUC");
          xamlMember = new XamlMember(this, "Preview2Url", "String");
          xamlMember.SetIsDependencyProperty();
          xamlMember.Getter = new Getter(this.get_66_InfoListItemUC_Preview2Url);
          xamlMember.Setter = new Setter(this.set_66_InfoListItemUC_Preview2Url);
          break;
        case "App1uwp.UC.InfoListItemUC.Preview3Url":
          XamlUserType xamlTypeByName68 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.InfoListItemUC");
          xamlMember = new XamlMember(this, "Preview3Url", "String");
          xamlMember.SetIsDependencyProperty();
          xamlMember.Getter = new Getter(this.get_67_InfoListItemUC_Preview3Url);
          xamlMember.Setter = new Setter(this.set_67_InfoListItemUC_Preview3Url);
          break;
        case "App1uwp.UC.InfoListItemUC.IsTiltEnabled":
          XamlUserType xamlTypeByName69 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.InfoListItemUC");
          xamlMember = new XamlMember(this, "IsTiltEnabled", "Boolean");
          xamlMember.SetIsDependencyProperty();
          xamlMember.Getter = new Getter(this.get_68_InfoListItemUC_IsTiltEnabled);
          xamlMember.Setter = new Setter(this.set_68_InfoListItemUC_IsTiltEnabled);
          break;
        case "App1uwp.UC.InfoListItemUC.Inlines":
          XamlUserType xamlTypeByName70 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.InfoListItemUC");
          xamlMember = new XamlMember(this, "Inlines", "System.Collections.Generic.List`1<Windows.UI.Xaml.Documents.Inline>");
          xamlMember.SetIsDependencyProperty();
          xamlMember.Getter = new Getter(this.get_69_InfoListItemUC_Inlines);
          xamlMember.Setter = new Setter(this.set_69_InfoListItemUC_Inlines);
          break;
        case "App1uwp.VirtualUC.ItemsBasePostUC.DataPost":
          XamlUserType xamlTypeByName71 = (XamlUserType) this.GetXamlTypeByName("App1uwp.VirtualUC.ItemsBasePostUC");
          xamlMember = new XamlMember(this, "DataPost", "App1uwp.Network.DataObjects.VKWallPost");
          xamlMember.SetIsDependencyProperty();
          xamlMember.Getter = new Getter(this.get_70_ItemsBasePostUC_DataPost);
          xamlMember.Setter = new Setter(this.set_70_ItemsBasePostUC_DataPost);
          break;
        case "App1uwp.VirtualUC.ItemsBasePostUC.DataNews":
          XamlUserType xamlTypeByName72 = (XamlUserType) this.GetXamlTypeByName("App1uwp.VirtualUC.ItemsBasePostUC");
          xamlMember = new XamlMember(this, "DataNews", "App1uwp.Network.DataObjects.VKNewsfeedPost");
          xamlMember.Getter = new Getter(this.get_71_ItemsBasePostUC_DataNews);
          xamlMember.Setter = new Setter(this.set_71_ItemsBasePostUC_DataNews);
          break;
        case "App1uwp.UC.LoadingUC.TryAgainCommand":
          XamlUserType xamlTypeByName73 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.LoadingUC");
          xamlMember = new XamlMember(this, "TryAgainCommand", "System.Action");
          xamlMember.SetIsDependencyProperty();
          xamlMember.Getter = new Getter(this.get_72_LoadingUC_TryAgainCommand);
          xamlMember.Setter = new Setter(this.set_72_LoadingUC_TryAgainCommand);
          break;
        case "App1uwp.GroupsPage.ConversationVM":
          XamlUserType xamlTypeByName74 = (XamlUserType) this.GetXamlTypeByName("App1uwp.GroupsPage");
          xamlMember = new XamlMember(this, "ConversationVM", "App1uwp.Network.ViewModels.GroupsViewModel");
          xamlMember.Getter = new Getter(this.get_73_GroupsPage_ConversationVM);
          xamlMember.SetIsReadOnly();
          break;
        case "App1uwp.MarketPage.VM":
          XamlUserType xamlTypeByName75 = (XamlUserType) this.GetXamlTypeByName("App1uwp.MarketPage");
          xamlMember = new XamlMember(this, "VM", "App1uwp.Network.ViewModels.MarketViewModel");
          xamlMember.Getter = new Getter(this.get_74_MarketPage_VM);
          xamlMember.SetIsReadOnly();
          break;
        case "App1uwp.MusicPage.VM":
          XamlUserType xamlTypeByName76 = (XamlUserType) this.GetXamlTypeByName("App1uwp.MusicPage");
          xamlMember = new XamlMember(this, "VM", "App1uwp.Network.ViewModels.MusicViewModel");
          xamlMember.Getter = new Getter(this.get_75_MusicPage_VM);
          xamlMember.SetIsReadOnly();
          break;
        case "App1uwp.UC.ItemNotificationUC.Post":
          XamlUserType xamlTypeByName77 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.ItemNotificationUC");
          xamlMember = new XamlMember(this, "Post", "Object");
          xamlMember.SetIsDependencyProperty();
          xamlMember.Getter = new Getter(this.get_76_ItemNotificationUC_Post);
          xamlMember.Setter = new Setter(this.set_76_ItemNotificationUC_Post);
          break;
        case "App1uwp.NotificationsPage.ConversationVM":
          XamlUserType xamlTypeByName78 = (XamlUserType) this.GetXamlTypeByName("App1uwp.NotificationsPage");
          xamlMember = new XamlMember(this, "ConversationVM", "App1uwp.Network.ViewModels.NotificationsViewModel");
          xamlMember.Getter = new Getter(this.get_77_NotificationsPage_ConversationVM);
          xamlMember.SetIsReadOnly();
          break;
        case "App1uwp.UC.ItemComment.Data":
          XamlUserType xamlTypeByName79 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.ItemComment");
          xamlMember = new XamlMember(this, "Data", "Object");
          xamlMember.SetIsDependencyProperty();
          xamlMember.Getter = new Getter(this.get_78_ItemComment_Data);
          xamlMember.Setter = new Setter(this.set_78_ItemComment_Data);
          break;
        case "App1uwp.PhotoAlbumPage.AlbumsVM":
          XamlUserType xamlTypeByName80 = (XamlUserType) this.GetXamlTypeByName("App1uwp.PhotoAlbumPage");
          xamlMember = new XamlMember(this, "AlbumsVM", "App1uwp.Network.ViewModels.PhotoAlbumViewModel");
          xamlMember.Getter = new Getter(this.get_79_PhotoAlbumPage_AlbumsVM);
          xamlMember.SetIsReadOnly();
          break;
        case "App1uwp.PhotosPage.PhotosVM":
          XamlUserType xamlTypeByName81 = (XamlUserType) this.GetXamlTypeByName("App1uwp.PhotosPage");
          xamlMember = new XamlMember(this, "PhotosVM", "App1uwp.Network.ViewModels.PhotosViewModel");
          xamlMember.Getter = new Getter(this.get_80_PhotosPage_PhotosVM);
          xamlMember.SetIsReadOnly();
          break;
        case "App1uwp.TestAddRemove.Items":
          XamlUserType xamlTypeByName82 = (XamlUserType) this.GetXamlTypeByName("App1uwp.TestAddRemove");
          xamlMember = new XamlMember(this, "Items", "System.Collections.ObjectModel.ObservableCollection`1<App1uwp.TestAddRemove.Temp>");
          xamlMember.Getter = new Getter(this.get_81_TestAddRemove_Items);
          xamlMember.Setter = new Setter(this.set_81_TestAddRemove_Items);
          break;
        case "App1uwp.TestAddRemove.Temp.id":
          XamlUserType xamlTypeByName83 = (XamlUserType) this.GetXamlTypeByName("App1uwp.TestAddRemove.Temp");
          xamlMember = new XamlMember(this, "id", "Int32");
          xamlMember.Getter = new Getter(this.get_82_Temp_id);
          xamlMember.Setter = new Setter(this.set_82_Temp_id);
          break;
        case "App1uwp.TestAddRemove.Temp.content":
          XamlUserType xamlTypeByName84 = (XamlUserType) this.GetXamlTypeByName("App1uwp.TestAddRemove.Temp");
          xamlMember = new XamlMember(this, "content", "String");
          xamlMember.Getter = new Getter(this.get_83_Temp_content);
          xamlMember.Setter = new Setter(this.set_83_Temp_content);
          break;
        case "App1uwp.UserPage.VM":
          XamlUserType xamlTypeByName85 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UserPage");
          xamlMember = new XamlMember(this, "VM", "App1uwp.Network.ViewModels.ProfileViewModel");
          xamlMember.Getter = new Getter(this.get_84_UserPage_VM);
          xamlMember.SetIsReadOnly();
          break;
        case "App1uwp.UC.ToggleSwitch.IsChecked":
          XamlUserType xamlTypeByName86 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.ToggleSwitch");
          xamlMember = new XamlMember(this, "IsChecked", "Boolean");
          xamlMember.SetIsDependencyProperty();
          xamlMember.Getter = new Getter(this.get_85_ToggleSwitch_IsChecked);
          xamlMember.Setter = new Setter(this.set_85_ToggleSwitch_IsChecked);
          break;
        case "App1uwp.UC.ToggleSwitch.Title":
          XamlUserType xamlTypeByName87 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.ToggleSwitch");
          xamlMember = new XamlMember(this, "Title", "String");
          xamlMember.SetIsDependencyProperty();
          xamlMember.Getter = new Getter(this.get_86_ToggleSwitch_Title);
          xamlMember.Setter = new Setter(this.set_86_ToggleSwitch_Title);
          break;
        case "App1uwp.UC.ToggleSwitch.IsStateTextVisible":
          XamlUserType xamlTypeByName88 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.ToggleSwitch");
          xamlMember = new XamlMember(this, "IsStateTextVisible", "Boolean");
          xamlMember.Getter = new Getter(this.get_87_ToggleSwitch_IsStateTextVisible);
          xamlMember.Setter = new Setter(this.set_87_ToggleSwitch_IsStateTextVisible);
          break;
        case "App1uwp.UC.ToggleSwitch.Description":
          XamlUserType xamlTypeByName89 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.ToggleSwitch");
          xamlMember = new XamlMember(this, "Description", "String");
          xamlMember.SetIsDependencyProperty();
          xamlMember.Getter = new Getter(this.get_88_ToggleSwitch_Description);
          xamlMember.Setter = new Setter(this.set_88_ToggleSwitch_Description);
          break;
        case "App1uwp.UC.ToggleSwitch.StateTextOn":
          XamlUserType xamlTypeByName90 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.ToggleSwitch");
          xamlMember = new XamlMember(this, "StateTextOn", "String");
          xamlMember.SetIsDependencyProperty();
          xamlMember.Getter = new Getter(this.get_89_ToggleSwitch_StateTextOn);
          xamlMember.Setter = new Setter(this.set_89_ToggleSwitch_StateTextOn);
          break;
        case "App1uwp.UC.ToggleSwitch.StateTextOff":
          XamlUserType xamlTypeByName91 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.ToggleSwitch");
          xamlMember = new XamlMember(this, "StateTextOff", "String");
          xamlMember.SetIsDependencyProperty();
          xamlMember.Getter = new Getter(this.get_90_ToggleSwitch_StateTextOff);
          xamlMember.Setter = new Setter(this.set_90_ToggleSwitch_StateTextOff);
          break;
        case "App1uwp.UC.ToggleSwitch.BorderColor":
          XamlUserType xamlTypeByName92 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.ToggleSwitch");
          xamlMember = new XamlMember(this, "BorderColor", "Windows.UI.Xaml.Media.Brush");
          xamlMember.SetIsDependencyProperty();
          xamlMember.Getter = new Getter(this.get_91_ToggleSwitch_BorderColor);
          xamlMember.Setter = new Setter(this.set_91_ToggleSwitch_BorderColor);
          break;
        case "App1uwp.UC.AppNotification2.Title":
          XamlUserType xamlTypeByName93 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.AppNotification2");
          xamlMember = new XamlMember(this, "Title", "String");
          xamlMember.Getter = new Getter(this.get_92_AppNotification2_Title);
          xamlMember.SetIsReadOnly();
          break;
        case "App1uwp.UC.ConversationsUC.ConversationsVM":
          XamlUserType xamlTypeByName94 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.ConversationsUC");
          xamlMember = new XamlMember(this, "ConversationsVM", "App1uwp.Network.ViewModels.DialogsViewModel");
          xamlMember.Getter = new Getter(this.get_93_ConversationsUC_ConversationsVM);
          xamlMember.SetIsReadOnly();
          break;
        case "App1uwp.UC.ConversationsUC.Scroll":
          XamlUserType xamlTypeByName95 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.ConversationsUC");
          xamlMember = new XamlMember(this, "Scroll", "App1uwp.Framework.ExtendedListView2");
          xamlMember.Getter = new Getter(this.get_94_ConversationsUC_Scroll);
          xamlMember.SetIsReadOnly();
          break;
        case "App1uwp.UC.ProgressRingUC.Progress":
          XamlUserType xamlTypeByName96 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.ProgressRingUC");
          xamlMember = new XamlMember(this, "Progress", "Double");
          xamlMember.SetIsDependencyProperty();
          xamlMember.Getter = new Getter(this.get_95_ProgressRingUC_Progress);
          xamlMember.Setter = new Setter(this.set_95_ProgressRingUC_Progress);
          break;
        case "App1uwp.UC.GifViewerUC.UseOldGifPlayer":
          XamlUserType xamlTypeByName97 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.GifViewerUC");
          xamlMember = new XamlMember(this, "UseOldGifPlayer", "Boolean");
          xamlMember.Getter = new Getter(this.get_96_GifViewerUC_UseOldGifPlayer);
          xamlMember.SetIsReadOnly();
          break;
        case "App1uwp.UC.GifViewerUC.DownloadProgress":
          XamlUserType xamlTypeByName98 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.GifViewerUC");
          xamlMember = new XamlMember(this, "DownloadProgress", "Double");
          xamlMember.Getter = new Getter(this.get_97_GifViewerUC_DownloadProgress);
          xamlMember.Setter = new Setter(this.set_97_GifViewerUC_DownloadProgress);
          break;
        case "App1uwp.UC.MenuItemUC.Icon":
          XamlUserType xamlTypeByName99 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.MenuItemUC");
          xamlMember = new XamlMember(this, "Icon", "String");
          xamlMember.Getter = new Getter(this.get_98_MenuItemUC_Icon);
          xamlMember.Setter = new Setter(this.set_98_MenuItemUC_Icon);
          break;
        case "App1uwp.UC.MenuItemUC.Title":
          XamlUserType xamlTypeByName100 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.MenuItemUC");
          xamlMember = new XamlMember(this, "Title", "String");
          xamlMember.Getter = new Getter(this.get_99_MenuItemUC_Title);
          xamlMember.Setter = new Setter(this.set_99_MenuItemUC_Title);
          break;
        case "App1uwp.UC.ImageViewerDecoratorUC.CurrentImage":
          XamlUserType xamlTypeByName101 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.ImageViewerDecoratorUC");
          xamlMember = new XamlMember(this, "CurrentImage", "Windows.UI.Xaml.Controls.Image");
          xamlMember.Getter = new Getter(this.get_100_ImageViewerDecoratorUC_CurrentImage);
          xamlMember.SetIsReadOnly();
          break;
        case "App1uwp.UC.ImageViewerDecoratorUC.CurrentImageFitRectOriginal":
          XamlUserType xamlTypeByName102 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.ImageViewerDecoratorUC");
          xamlMember = new XamlMember(this, "CurrentImageFitRectOriginal", "Windows.Foundation.Rect");
          xamlMember.Getter = new Getter(this.get_101_ImageViewerDecoratorUC_CurrentImageFitRectOriginal);
          xamlMember.SetIsReadOnly();
          break;
        case "App1uwp.UC.ImageViewerDecoratorUC.CurrentImageFitRectTransformed":
          XamlUserType xamlTypeByName103 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.ImageViewerDecoratorUC");
          xamlMember = new XamlMember(this, "CurrentImageFitRectTransformed", "Windows.Foundation.Rect");
          xamlMember.Getter = new Getter(this.get_102_ImageViewerDecoratorUC_CurrentImageFitRectTransformed);
          xamlMember.SetIsReadOnly();
          break;
        case "App1uwp.UC.ScrollableTextBlock.Text":
          XamlUserType xamlTypeByName104 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.ScrollableTextBlock");
          xamlMember = new XamlMember(this, "Text", "String");
          xamlMember.SetIsDependencyProperty();
          xamlMember.Getter = new Getter(this.get_103_ScrollableTextBlock_Text);
          xamlMember.Setter = new Setter(this.set_103_ScrollableTextBlock_Text);
          break;
        case "App1uwp.UC.ScrollableTextBlock.Foreground":
          XamlUserType xamlTypeByName105 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.ScrollableTextBlock");
          xamlMember = new XamlMember(this, "Foreground", "Windows.UI.Xaml.Media.Brush");
          xamlMember.Getter = new Getter(this.get_104_ScrollableTextBlock_Foreground);
          xamlMember.Setter = new Setter(this.set_104_ScrollableTextBlock_Foreground);
          break;
        case "App1uwp.UC.ScrollableTextBlock.FullOnly":
          XamlUserType xamlTypeByName106 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.ScrollableTextBlock");
          xamlMember = new XamlMember(this, "FullOnly", "Boolean");
          xamlMember.Getter = new Getter(this.get_105_ScrollableTextBlock_FullOnly);
          xamlMember.Setter = new Setter(this.set_105_ScrollableTextBlock_FullOnly);
          break;
        case "App1uwp.UC.ItemNewsFeedUC.Post":
          XamlUserType xamlTypeByName107 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.ItemNewsFeedUC");
          xamlMember = new XamlMember(this, "Post", "Object");
          xamlMember.SetIsDependencyProperty();
          xamlMember.Getter = new Getter(this.get_106_ItemNewsFeedUC_Post);
          xamlMember.Setter = new Setter(this.set_106_ItemNewsFeedUC_Post);
          break;
        case "App1uwp.UC.SwipeThroughControl.Items":
          XamlUserType xamlTypeByName108 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.SwipeThroughControl");
          xamlMember = new XamlMember(this, "Items", "System.Collections.ObjectModel.ObservableCollection`1<Object>");
          xamlMember.Getter = new Getter(this.get_107_SwipeThroughControl_Items);
          xamlMember.Setter = new Setter(this.set_107_SwipeThroughControl_Items);
          break;
        case "App1uwp.Library.OutboundAttachmentTemplateSelector.PhotoTemplate":
          XamlUserType xamlTypeByName109 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Library.OutboundAttachmentTemplateSelector");
          xamlMember = new XamlMember(this, "PhotoTemplate", "Windows.UI.Xaml.DataTemplate");
          xamlMember.Getter = new Getter(this.get_108_OutboundAttachmentTemplateSelector_PhotoTemplate);
          xamlMember.Setter = new Setter(this.set_108_OutboundAttachmentTemplateSelector_PhotoTemplate);
          break;
        case "App1uwp.Library.OutboundAttachmentTemplateSelector.ForwardedMessageTemplate":
          XamlUserType xamlTypeByName110 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Library.OutboundAttachmentTemplateSelector");
          xamlMember = new XamlMember(this, "ForwardedMessageTemplate", "Windows.UI.Xaml.DataTemplate");
          xamlMember.Getter = new Getter(this.get_109_OutboundAttachmentTemplateSelector_ForwardedMessageTemplate);
          xamlMember.Setter = new Setter(this.set_109_OutboundAttachmentTemplateSelector_ForwardedMessageTemplate);
          break;
        case "App1uwp.Library.OutboundAttachmentTemplateSelector.GenericIconTemplate":
          XamlUserType xamlTypeByName111 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Library.OutboundAttachmentTemplateSelector");
          xamlMember = new XamlMember(this, "GenericIconTemplate", "Windows.UI.Xaml.DataTemplate");
          xamlMember.Getter = new Getter(this.get_110_OutboundAttachmentTemplateSelector_GenericIconTemplate);
          xamlMember.Setter = new Setter(this.set_110_OutboundAttachmentTemplateSelector_GenericIconTemplate);
          break;
        case "App1uwp.Library.OutboundAttachmentTemplateSelector.GeoTemplate":
          XamlUserType xamlTypeByName112 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Library.OutboundAttachmentTemplateSelector");
          xamlMember = new XamlMember(this, "GeoTemplate", "Windows.UI.Xaml.DataTemplate");
          xamlMember.Getter = new Getter(this.get_111_OutboundAttachmentTemplateSelector_GeoTemplate);
          xamlMember.Setter = new Setter(this.set_111_OutboundAttachmentTemplateSelector_GeoTemplate);
          break;
        case "App1uwp.Library.OutboundAttachmentTemplateSelector.VideoTemplate":
          XamlUserType xamlTypeByName113 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Library.OutboundAttachmentTemplateSelector");
          xamlMember = new XamlMember(this, "VideoTemplate", "Windows.UI.Xaml.DataTemplate");
          xamlMember.Getter = new Getter(this.get_112_OutboundAttachmentTemplateSelector_VideoTemplate);
          xamlMember.Setter = new Setter(this.set_112_OutboundAttachmentTemplateSelector_VideoTemplate);
          break;
        case "App1uwp.Library.OutboundAttachmentTemplateSelector.AudioTemplate":
          XamlUserType xamlTypeByName114 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Library.OutboundAttachmentTemplateSelector");
          xamlMember = new XamlMember(this, "AudioTemplate", "Windows.UI.Xaml.DataTemplate");
          xamlMember.Getter = new Getter(this.get_113_OutboundAttachmentTemplateSelector_AudioTemplate);
          xamlMember.Setter = new Setter(this.set_113_OutboundAttachmentTemplateSelector_AudioTemplate);
          break;
        case "App1uwp.Library.OutboundAttachmentTemplateSelector.DocumentTemplate":
          XamlUserType xamlTypeByName115 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Library.OutboundAttachmentTemplateSelector");
          xamlMember = new XamlMember(this, "DocumentTemplate", "Windows.UI.Xaml.DataTemplate");
          xamlMember.Getter = new Getter(this.get_114_OutboundAttachmentTemplateSelector_DocumentTemplate);
          xamlMember.Setter = new Setter(this.set_114_OutboundAttachmentTemplateSelector_DocumentTemplate);
          break;
        case "App1uwp.Library.OutboundAttachmentTemplateSelector.GenericThumbTemplate":
          XamlUserType xamlTypeByName116 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Library.OutboundAttachmentTemplateSelector");
          xamlMember = new XamlMember(this, "GenericThumbTemplate", "Windows.UI.Xaml.DataTemplate");
          xamlMember.Getter = new Getter(this.get_115_OutboundAttachmentTemplateSelector_GenericThumbTemplate);
          xamlMember.Setter = new Setter(this.set_115_OutboundAttachmentTemplateSelector_GenericThumbTemplate);
          break;
        case "App1uwp.Library.OutboundAttachmentTemplateSelector.AddAttachmentTemplate":
          XamlUserType xamlTypeByName117 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Library.OutboundAttachmentTemplateSelector");
          xamlMember = new XamlMember(this, "AddAttachmentTemplate", "Windows.UI.Xaml.DataTemplate");
          xamlMember.Getter = new Getter(this.get_116_OutboundAttachmentTemplateSelector_AddAttachmentTemplate);
          xamlMember.Setter = new Setter(this.set_116_OutboundAttachmentTemplateSelector_AddAttachmentTemplate);
          break;
        case "App1uwp.Library.OutboundAttachmentTemplateSelector.WallPostTemplate":
          XamlUserType xamlTypeByName118 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Library.OutboundAttachmentTemplateSelector");
          xamlMember = new XamlMember(this, "WallPostTemplate", "Windows.UI.Xaml.DataTemplate");
          xamlMember.Getter = new Getter(this.get_117_OutboundAttachmentTemplateSelector_WallPostTemplate);
          xamlMember.Setter = new Setter(this.set_117_OutboundAttachmentTemplateSelector_WallPostTemplate);
          break;
        case "App1uwp.UC.NewsSourceListItemUC.Icon":
          XamlUserType xamlTypeByName119 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.NewsSourceListItemUC");
          xamlMember = new XamlMember(this, "Icon", "String");
          xamlMember.Getter = new Getter(this.get_118_NewsSourceListItemUC_Icon);
          xamlMember.Setter = new Setter(this.set_118_NewsSourceListItemUC_Icon);
          break;
        case "App1uwp.UC.NewsSourceListItemUC.Title":
          XamlUserType xamlTypeByName120 = (XamlUserType) this.GetXamlTypeByName("App1uwp.UC.NewsSourceListItemUC");
          xamlMember = new XamlMember(this, "Title", "String");
          xamlMember.Getter = new Getter(this.get_119_NewsSourceListItemUC_Title);
          xamlMember.Setter = new Setter(this.set_119_NewsSourceListItemUC_Title);
          break;
        case "App1uwp.Framework.ImageExtensions.CacheUriImageBrush":
          XamlUserType xamlTypeByName121 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Framework.ImageExtensions");
          xamlMember = new XamlMember(this, "CacheUriImageBrush", "System.Uri");
          xamlMember.SetTargetTypeName("Windows.UI.Xaml.Media.ImageBrush");
          xamlMember.SetIsAttachable();
          xamlMember.Getter = new Getter(this.get_120_ImageExtensions_CacheUriImageBrush);
          xamlMember.Setter = new Setter(this.set_120_ImageExtensions_CacheUriImageBrush);
          break;
        case "App1uwp.Framework.ImageExtensions.CacheUri":
          XamlUserType xamlTypeByName122 = (XamlUserType) this.GetXamlTypeByName("App1uwp.Framework.ImageExtensions");
          xamlMember = new XamlMember(this, "CacheUri", "System.Uri");
          xamlMember.SetTargetTypeName("Windows.UI.Xaml.DependencyObject");
          xamlMember.SetIsAttachable();
          xamlMember.Getter = new Getter(this.get_121_ImageExtensions_CacheUri);
          xamlMember.Setter = new Setter(this.set_121_ImageExtensions_CacheUri);
          break;
      }
      return (IXamlMember) xamlMember;
    }
  }
}
