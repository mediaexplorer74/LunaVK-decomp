// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.LongPollServerService
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Framework;
using App1uwp.Library;
using App1uwp.Library.Events;
using App1uwp.Network.DataObjects;
using App1uwp.Network.DataVM;
using App1uwp.Network.Enums;
using App1uwp.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;

#nullable disable
namespace App1uwp.Network
{
  public class LongPollServerService
  {
    private CountersChanged.OwnCounters _counters = new CountersChanged.OwnCounters();
    private readonly string _getUpdatesURIFrm = "https://{0}?act=a_check&key={1}&ts={2}&wait=25&mode=66&version=1";
    private LongPollServerResponse _lastResp;
    private static LongPollServerService _instance;
    private CancellationTokenSource ct = new CancellationTokenSource();
    private bool IamWaiting;

    public static LongPollServerService Instance
    {
      get
      {
        if (LongPollServerService._instance == null)
          LongPollServerService._instance = new LongPollServerService();
        return LongPollServerService._instance;
      }
    }

    public LongPollServerService()
    {
      Window current = Window.Current;
      WindowsRuntimeMarshal.AddEventHandler<WindowVisibilityChangedEventHandler>(new Func<WindowVisibilityChangedEventHandler, EventRegistrationToken>(current.add_VisibilityChanged), new Action<EventRegistrationToken>(current.remove_VisibilityChanged), new WindowVisibilityChangedEventHandler(this.Current_VisibilityChanged));
    }

    private void Current_VisibilityChanged(object sender, VisibilityChangedEventArgs e)
    {
      if (e.Visible)
      {
        this.ct = new CancellationTokenSource();
        this.IamWaiting = false;
        if (!Settings.Instance.IsAuthorized)
          return;
        this.Restart();
      }
      else
        this.Stop();
    }

    public async void GetLongPollServer()
    {
      if (this._lastResp != null)
      {
        this.RunRequestsLoop(Settings.Instance.auth.AccessToken, this._lastResp);
      }
      else
      {
        VKResponse<LongPollServerResponseExtended> temp = await RequestsDispatcher.Execute<LongPollServerResponseExtended>("var c=API.getCounters();c.temp=1;return {\"c\":c,s:API.messages.getLongPollServer({use_ssl:1}),time:API.utils.getServerTime()};");
        if (temp == null)
        {
          this.Restart();
        }
        else
        {
          if (temp.error.error_code != VKErrors.None)
            return;
          this._counters = temp.response.c;
          EventAggregator.Instance.PublishEvent((object) new CountersChanged()
          {
            Counts = {
              messages = temp.response.c.messages,
              friends = temp.response.c.friends,
              groups = temp.response.c.groups
            }
          });
          this.RunRequestsLoop(Settings.Instance.auth.AccessToken, temp.response.s);
          this._lastResp = temp.response.s;
          Settings.Instance.ServerMinusLocalTimeDelta = temp.response.time - App1uwp.Utils.Extensions.DateTimeToUnixTimestamp(DateTime.UtcNow, false);
        }
      }
    }

    private async void RunRequestsLoop(string token, LongPollServerResponse requestsSettings)
    {
      this.IamWaiting = true;
      UpdatesResponse res = await this.GetUpdates(requestsSettings.server, requestsSettings.key, requestsSettings.ts);
      this.IamWaiting = false;
      if (res == null)
      {
        if (this.ct.IsCancellationRequested)
          return;
        this.Restart();
      }
      else
      {
        requestsSettings.ts = res.ts;
        this.ProcessReceivedData(res.Updates);
        this.RunRequestsLoop(token, requestsSettings);
      }
    }

    private async void Restart(bool fast = false)
    {
      bool b = NetworkInterface.GetIsNetworkAvailable();
      await Task.Delay(fast ? 500 : (b ? 2000 : 5000));
      this.GetLongPollServer();
    }

    public void SetUnreadMessages(int c)
    {
      this._counters.messages = c;
      EventAggregator.Instance.PublishEvent((object) new CountersChanged(this._counters));
    }

    private void ProcessReceivedData(
      List<UpdatesResponse.LongPollServerUpdateData> updates)
    {
      foreach (UpdatesResponse.LongPollServerUpdateData update1 in updates)
      {
        UpdatesResponse.LongPollServerUpdateData update = update1;
        switch (update.UpdateType)
        {
          case LongPollServerUpdateType.UserBecameOnline:
            EventAggregator.Instance.PublishEvent((object) new UserBecameOnlineEvent(update.user_id, true));
            continue;
          case LongPollServerUpdateType.UserBecameOffline:
            EventAggregator.Instance.PublishEvent((object) new UserBecameOnlineEvent(update.user_id, false));
            continue;
          case LongPollServerUpdateType.MessageHasBeenRead:
            EventAggregator.Instance.PublishEvent((object) new MessageHasBeenReadEvent(update.message_id, update.user_id, update.chat_id, update.isChat));
            continue;
          case LongPollServerUpdateType.MessageHasBeenAdded:
            VKMessageVM msg = new VKMessageVM();
            msg.user_id = update.user_id;
            msg.date = UIStringFormatterHelper.UnixTimeStampToDateTime((double) update.timestamp);
            msg.body = update.text;
            msg.@out = update.@out ? VKMessageType.Sent : VKMessageType.Received;
            msg.id = (int) update.message_id;
            msg.read_state = false;
            msg.chat_id = (int) update.chat_id;
            if (update.hasAttachOrForward)
            {
              Dictionary<string, string> parameters = new Dictionary<string, string>();
              parameters["message_ids"] = update.message_id.ToString();
              parameters["fields"] = "online,online_mobile,photo_max,sex,photo_50,photo_100";
              parameters["extended"] = "1";
              Task.Run((Func<Task>) (async () =>
              {
                VKResponse<VKGetMessagesHistoryObject> res = await RequestsDispatcher.GetResponse<VKGetMessagesHistoryObject>("messages.getById", parameters);
                UsersService.Instance.SetCachedUsers((IEnumerable<VKBaseDataForGroupOrUser>) res.response.groups);
                UsersService.Instance.SetCachedUsers((IEnumerable<VKBaseDataForGroupOrUser>) res.response.profiles);
                msg = res.response.items[0];
                EventAggregator.Instance.PublishEvent((object) msg);
              }));
              continue;
            }
            EventAggregator.Instance.PublishEvent((object) msg);
            continue;
          case LongPollServerUpdateType.ChatParamsWereChanged:
            RequestsDispatcher.ExecuteCallback<ChatInfo>("var chat = API.messages.getChat({\"chat_id\":" + (object) update.chat_id + "});var chat_participants = API.messages.getChatUsers({\"chat_id\":" + (object) update.chat_id + ",\"fields\":\"first_name,last_name,first_name_acc,last_name_acc,online,online_mobile,photo_max,photo_200,sex\"});return {\"chat\": chat,\"chat_participants\": chat_participants};", callback: (Action<ChatInfo>) (info => EventAggregator.Instance.PublishEvent((object) new ChatParamsWereChangedEvent(update.chat_id)
            {
              title = info.chat.title,
              _associatedUsers = info.chat.users
            })));
            continue;
          case LongPollServerUpdateType.UserIsTyping:
          case LongPollServerUpdateType.UserIsTypingInChat:
            long chatId = update.chat_id;
            long userId = (long) update.user_id;
            int updateType = (int) update.UpdateType;
            EventAggregator.Instance.PublishEvent((object) new UserIsTyping(userId, chatId));
            continue;
          case LongPollServerUpdateType.NewCounter:
            this.SetUnreadMessages(update.Counter);
            continue;
          default:
            continue;
        }
      }
    }

    public void Stop()
    {
      this.ct.Cancel();
      this.IamWaiting = false;
    }

    public async Task<UpdatesResponse> GetUpdates(string serverName, string key, long ts)
    {
      string temp = await RequestsDispatcher.DoWebRequestString(string.Format(this._getUpdatesURIFrm, (object) serverName, (object) key, (object) ts), ct: this.ct.Token);
      if (string.IsNullOrEmpty(temp))
        return (UpdatesResponse) null;
      UpdatesResponse ResultData = new UpdatesResponse();
      LongPollServerService.RootObjectGetUpdates objectGetUpdates = JsonConvert.DeserializeObject<LongPollServerService.RootObjectGetUpdates>(temp);
      ResultData.ts = (long) objectGetUpdates.ts;
      ResultData.Updates = this.ReadUpdatesResponseFromRaw(objectGetUpdates.updates, new Func<List<object>, UpdatesResponse.LongPollServerUpdateData>(this.GetUpdateDataForNewMessageLongPollData));
      return ResultData;
    }

    private List<UpdatesResponse.LongPollServerUpdateData> ReadUpdatesResponseFromRaw(
      List<List<object>> rawUpdates,
      Func<List<object>, UpdatesResponse.LongPollServerUpdateData> getUpdatesForNewMessageFunc)
    {
      List<UpdatesResponse.LongPollServerUpdateData> serverUpdateDataList = new List<UpdatesResponse.LongPollServerUpdateData>();
      if (rawUpdates != null)
      {
        foreach (List<object> rawUpdate in rawUpdates)
        {
          if (rawUpdate != null)
          {
            LongPollServerUpdateType serverUpdateType = (LongPollServerUpdateType) int.Parse(rawUpdate[0].ToString());
            switch (serverUpdateType)
            {
              case LongPollServerUpdateType.ProcessAddFlags:
                if ((int.Parse(rawUpdate[2].ToString()) & 128) == 128)
                {
                  UpdatesResponse.LongPollServerUpdateData serverUpdateData = this.ReadUserOrChatIds(rawUpdate);
                  if (serverUpdateData != null)
                  {
                    serverUpdateData.UpdateType = LongPollServerUpdateType.MessageHasBeenDeleted;
                    serverUpdateDataList.Add(serverUpdateData);
                    continue;
                  }
                  continue;
                }
                continue;
              case LongPollServerUpdateType.ClearFlags:
                int num1 = int.Parse(rawUpdate[2].ToString());
                if ((num1 & 1) == 1)
                {
                  long num2 = long.Parse(rawUpdate[1].ToString());
                  if (rawUpdate.Count >= 4)
                  {
                    int num3 = int.Parse(rawUpdate[3].ToString());
                    if (num3 >= 2000000000)
                      serverUpdateDataList.Add(new UpdatesResponse.LongPollServerUpdateData()
                      {
                        message_id = num2,
                        chat_id = (long) (num3 - 2000000000),
                        isChat = true,
                        UpdateType = LongPollServerUpdateType.MessageHasBeenRead
                      });
                    else
                      serverUpdateDataList.Add(new UpdatesResponse.LongPollServerUpdateData()
                      {
                        message_id = num2,
                        user_id = num3,
                        UpdateType = LongPollServerUpdateType.MessageHasBeenRead
                      });
                  }
                }
                if ((num1 & 128) == 128)
                {
                  UpdatesResponse.LongPollServerUpdateData serverUpdateData = this.ReadUserOrChatIds(rawUpdate);
                  if (serverUpdateData != null)
                  {
                    serverUpdateData.UpdateType = LongPollServerUpdateType.MessageHasBeenRestored;
                    serverUpdateDataList.Add(serverUpdateData);
                    continue;
                  }
                  continue;
                }
                continue;
              case LongPollServerUpdateType.MessageAdd:
                UpdatesResponse.LongPollServerUpdateData serverUpdateData1 = getUpdatesForNewMessageFunc(rawUpdate);
                if (serverUpdateData1 != null)
                {
                  serverUpdateDataList.Add(serverUpdateData1);
                  continue;
                }
                continue;
              case LongPollServerUpdateType.IncomingMessagesRead:
                int num4 = int.Parse(rawUpdate[1].ToString());
                UpdatesResponse.LongPollServerUpdateData serverUpdateData2 = new UpdatesResponse.LongPollServerUpdateData()
                {
                  UpdateType = serverUpdateType
                };
                if (num4 >= 2000000000)
                  serverUpdateData2.chat_id = (long) (num4 - 2000000000);
                else
                  serverUpdateData2.user_id = num4;
                serverUpdateData2.message_id = long.Parse(rawUpdate[2].ToString());
                serverUpdateDataList.Add(serverUpdateData2);
                continue;
              case LongPollServerUpdateType.UserBecameOnline:
              case LongPollServerUpdateType.UserBecameOffline:
                int num5 = 7;
                if (rawUpdate.Count > 2)
                  num5 = int.Parse(rawUpdate[2].ToString()) % 256;
                int num6 = -int.Parse(rawUpdate[1].ToString());
                serverUpdateDataList.Add(new UpdatesResponse.LongPollServerUpdateData()
                {
                  UpdateType = serverUpdateType,
                  user_id = num6,
                  Platform = num5
                });
                continue;
              case LongPollServerUpdateType.ChatParamsChanged:
                long num7 = long.Parse(rawUpdate[1].ToString());
                serverUpdateDataList.Add(new UpdatesResponse.LongPollServerUpdateData()
                {
                  UpdateType = LongPollServerUpdateType.ChatParamsWereChanged,
                  isChat = true,
                  chat_id = num7
                });
                continue;
              case LongPollServerUpdateType.UserIsTyping:
                int num8 = int.Parse(rawUpdate[1].ToString());
                serverUpdateDataList.Add(new UpdatesResponse.LongPollServerUpdateData()
                {
                  UpdateType = serverUpdateType,
                  user_id = num8
                });
                continue;
              case LongPollServerUpdateType.UserIsTypingInChat:
                int num9 = int.Parse(rawUpdate[1].ToString());
                long num10 = long.Parse(rawUpdate[2].ToString());
                serverUpdateDataList.Add(new UpdatesResponse.LongPollServerUpdateData()
                {
                  UpdateType = serverUpdateType,
                  user_id = num9,
                  chat_id = num10,
                  isChat = true
                });
                continue;
              case LongPollServerUpdateType.NewCounter:
                int result = 0;
                if (rawUpdate.Count > 1 && int.TryParse(rawUpdate[1].ToString(), out result))
                {
                  serverUpdateDataList.Add(new UpdatesResponse.LongPollServerUpdateData()
                  {
                    UpdateType = serverUpdateType,
                    Counter = result
                  });
                  continue;
                }
                continue;
              default:
                continue;
            }
          }
        }
      }
      return serverUpdateDataList;
    }

    private UpdatesResponse.LongPollServerUpdateData GetUpdateDataForNewMessageLongPollData(
      List<object> updateDataRaw)
    {
      UpdatesResponse.LongPollServerUpdateData messageLongPollData = new UpdatesResponse.LongPollServerUpdateData();
      messageLongPollData.UpdateType = LongPollServerUpdateType.MessageHasBeenAdded;
      long num1 = long.Parse(updateDataRaw[1].ToString());
      bool flag1 = (long.Parse(updateDataRaw[2].ToString()) & 2L) == 2L;
      int num2 = int.Parse(updateDataRaw[3].ToString());
      long num3 = long.Parse(updateDataRaw[4].ToString());
      string str = updateDataRaw[6].ToString();
      bool flag2 = false;
      bool flag3 = false;
      long num4 = 0;
      if (updateDataRaw.Count > 7)
      {
        foreach (KeyValuePair<string, JToken> keyValuePair in updateDataRaw[7] as JObject)
        {
          if (keyValuePair.Key == "from")
          {
            num4 = (long) num2 - 2000000000L;
            num2 = int.Parse(keyValuePair.Value.ToString());
            flag3 = true;
          }
          else
            flag2 = true;
        }
      }
      messageLongPollData.message_id = num1;
      messageLongPollData.@out = flag1;
      messageLongPollData.user_id = num2;
      messageLongPollData.chat_id = num4;
      messageLongPollData.timestamp = num3;
      messageLongPollData.text = str;
      messageLongPollData.isChat = flag3;
      messageLongPollData.hasAttachOrForward = flag2;
      return messageLongPollData;
    }

    private UpdatesResponse.LongPollServerUpdateData ReadUserOrChatIds(List<object> updateDataRaw)
    {
      if (updateDataRaw == null || updateDataRaw.Count < 4)
        return (UpdatesResponse.LongPollServerUpdateData) null;
      long num1 = long.Parse(updateDataRaw[1].ToString());
      bool flag = false;
      int num2 = 0;
      long num3 = 0;
      if (updateDataRaw.Count >= 4)
      {
        int num4 = int.Parse(updateDataRaw[3].ToString());
        if ((long) num4 - 2000000000L >= 0L)
        {
          flag = true;
          num3 = (long) num4 - 2000000000L;
        }
        else
        {
          flag = false;
          num2 = num4;
        }
      }
      return new UpdatesResponse.LongPollServerUpdateData()
      {
        user_id = num2,
        chat_id = num3,
        isChat = flag,
        message_id = num1
      };
    }

    public event LongPollServerService.UpdatesReceivedEventHandler ReceivedUpdates;

    public class RootObjectGetUpdates
    {
      public int ts { get; set; }

      public List<List<object>> updates { get; set; }
    }

    public delegate void UpdatesReceivedEventHandler(
      List<UpdatesResponse.LongPollServerUpdateData> updates);
  }
}
