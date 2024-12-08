// Decompiled with JetBrains decompiler
// Type: App1uwp.Library.PushNotifications
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
using App1uwp.Network;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Networking.PushNotifications;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.System.Profile;
using Windows.UI.Notifications;

#nullable disable
namespace App1uwp.Library
{
  public class PushNotifications
  {
    private bool _is_attached;
    private bool _is_push_hidden;
    private PushNotificationChannel channel;
    private static App1uwp.Library.PushNotifications _instance;

    public static App1uwp.Library.PushNotifications Instance
    {
      get
      {
        if (App1uwp.Library.PushNotifications._instance == null)
          App1uwp.Library.PushNotifications._instance = new App1uwp.Library.PushNotifications();
        return App1uwp.Library.PushNotifications._instance;
      }
    }

    public string GetHardwareID()
    {
      IBuffer id = HardwareIdentification.GetPackageSpecificToken((IBuffer) null).Id;
      DataReader dataReader = DataReader.FromBuffer(id);
      byte[] inArray = new byte[(IntPtr) id.Length];
      dataReader.ReadBytes(inArray);
      return Convert.ToBase64String(inArray).Replace("=", "");
    }

    public long GetPeerId(long userOrChatId, bool isChat)
    {
      return !isChat ? userOrChatId : userOrChatId + 2000000000L;
    }

    public async Task<bool> SetSilenceMode(int nrOfSeconds, long chatId = 0, long uid = 0)
    {
      string device_id = this.GetHardwareID();
      Dictionary<string, string> parameters = new Dictionary<string, string>();
      string str = nrOfSeconds.ToString();
      parameters.Add("device_id", device_id);
      parameters.Add("time", str);
      if (chatId != 0L)
        parameters["peer_id"] = this.GetPeerId(chatId, true).ToString();
      if (uid != 0L)
        parameters["peer_id"] = this.GetPeerId(uid, false).ToString();
      VKResponse<int> e = await RequestsDispatcher.GetResponse<int>("account.setSilenceMode", parameters);
      return e != null && e.error.error_code == VKErrors.None && e.response == 1;
    }

    public async void UpdateDeviceRegistration(Action<bool> calback = null)
    {
      try
      {
        if (this.channel == null)
          this.channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
      }
      catch
      {
        return;
      }
      Dictionary<string, string> parameters = new Dictionary<string, string>();
      parameters["device_id"] = this.GetHardwareID();
      if (Settings.Instance.PushNotificationsEnabled)
      {
        if (!this._is_attached)
        {
          PushNotificationChannel channel = this.channel;
          // ISSUE: method pointer
          WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<PushNotificationChannel, PushNotificationReceivedEventArgs>>(new Func<TypedEventHandler<PushNotificationChannel, PushNotificationReceivedEventArgs>, EventRegistrationToken>(channel.add_PushNotificationReceived), new Action<EventRegistrationToken>(channel.remove_PushNotificationReceived), new TypedEventHandler<PushNotificationChannel, PushNotificationReceivedEventArgs>((object) this, __methodptr(channel_PushNotificationReceived)));
          this._is_attached = true;
        }
        if (calback != null)
          calback(true);
        if (this.channel.Uri == Settings.Instance.LastPushNotificationsUri)
          return;
        Settings.Instance.LastPushNotificationsUri = this.channel.Uri;
        Settings.Instance.Save();
        parameters["token"] = this.channel.Uri;
        parameters["settings"] = Settings.Instance.PushSettings.ToString();
        VKResponse<int> e = await RequestsDispatcher.GetResponse<int>("account.registerDevice", parameters);
        if (e != null && e.response == 1 && !this._is_attached)
        {
          PushNotificationChannel channel = this.channel;
          // ISSUE: method pointer
          WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<PushNotificationChannel, PushNotificationReceivedEventArgs>>(new Func<TypedEventHandler<PushNotificationChannel, PushNotificationReceivedEventArgs>, EventRegistrationToken>(channel.add_PushNotificationReceived), new Action<EventRegistrationToken>(channel.remove_PushNotificationReceived), new TypedEventHandler<PushNotificationChannel, PushNotificationReceivedEventArgs>((object) this, __methodptr(channel_PushNotificationReceived)));
          this._is_attached = true;
        }
        if (calback == null)
          return;
        calback(e.response == 1);
      }
      else
      {
        VKResponse<int> e = await RequestsDispatcher.GetResponse<int>("account.unregisterDevice", parameters);
        if (e != null && e.response == 1)
        {
          if (this._is_attached)
          {
            // ISSUE: method pointer
            WindowsRuntimeMarshal.RemoveEventHandler<TypedEventHandler<PushNotificationChannel, PushNotificationReceivedEventArgs>>(new Action<EventRegistrationToken>(this.channel.remove_PushNotificationReceived), new TypedEventHandler<PushNotificationChannel, PushNotificationReceivedEventArgs>((object) this, __methodptr(channel_PushNotificationReceived)));
            this._is_attached = false;
          }
          this.channel.Close();
          this.channel = (PushNotificationChannel) null;
        }
        if (calback == null)
          return;
        calback(e.response == 1);
      }
    }

    public async void SetPushSettings(
      string key,
      string value,
      Action<ResultCode> callback = null,
      string key2 = "",
      string value2 = "")
    {
      string deviceId = this.GetHardwareID();
      string format = "API.account.setPushSettings({{\"device_id\":\"{0}\", \"key\":\"{1}\", \"value\":\"{2}\"}});";
      string code = string.Format(format, (object) deviceId, (object) key, (object) value);
      if (!string.IsNullOrWhiteSpace(key2) && !string.IsNullOrWhiteSpace(value2))
        code = code + Environment.NewLine + string.Format(format, (object) deviceId, (object) key2, (object) value2);
      VKResponse<App1uwp.Library.PushNotifications.ResponseWithId> vkResponse = await RequestsDispatcher.Execute<App1uwp.Library.PushNotifications.ResponseWithId>(code);
    }

    public void HidePush(bool hide) => this._is_push_hidden = hide;

    private void channel_PushNotificationReceived(
      PushNotificationChannel sender,
      PushNotificationReceivedEventArgs args)
    {
      args.put_Cancel(this._is_push_hidden);
      if (args.NotificationType != null || args.Cancel)
        return;
      args.put_Cancel(true);
      XmlDocument content = args.ToastNotification.Content;
      ((IReadOnlyList<IXmlNode>) content.GetElementsByTagName("audio"))[0].Attributes.RemoveNamedItem("silent");
      XmlElement documentElement = content.DocumentElement;
      XmlElement element1 = content.CreateElement("actions");
      documentElement.AppendChild((IXmlNode) element1);
      XmlElement element2 = content.CreateElement("action");
      element2.SetAttribute("content", "Send");
      element2.SetAttribute("arguments", "action={0}");
      element2.SetAttribute("hint-inputId", "textBox");
      element2.SetAttribute("activationType", "background");
      element2.SetAttribute("imageUri", "Assets/Reply.png");
      XmlElement element3 = content.CreateElement("input");
      element3.SetAttribute("type", "text");
      element3.SetAttribute("id", "textBox");
      element3.SetAttribute("placeHolderContent", "Сообщение...");
      element1.AppendChild((IXmlNode) element3);
      element1.AppendChild((IXmlNode) element2);
      ToastNotification toastNotification = new ToastNotification(content);
      ToastNotificationManager.CreateToastNotifier().Show(toastNotification);
    }

    private void t_Activated(ToastNotification sender, object args)
    {
    }

    private void DUmp(XmlDocument doc)
    {
      Execute.ExecuteOnUIThread((Action) (async () =>
      {
        FileSavePicker savePicker = new FileSavePicker();
        savePicker.put_SuggestedStartLocation((PickerLocationId) 0);
        savePicker.FileTypeChoices.Add("Plain Text", (IList<string>) new List<string>()
        {
          ".txt"
        });
        savePicker.put_SuggestedFileName("New Document");
        StorageFile file = await savePicker.PickSaveFileAsync();
        await doc.SaveToFileAsync((IStorageFile) file);
      }));
    }

    private void AnalizeNode(IXmlNode node)
    {
      foreach (IXmlNode attribute in (IEnumerable<IXmlNode>) node.Attributes)
        ;
      foreach (IXmlNode childNode in (IEnumerable<IXmlNode>) node.ChildNodes)
        this.AnalizeNode(childNode);
    }

    public class ResponseWithId
    {
      public long response { get; set; }
    }
  }
}
