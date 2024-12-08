// Decompiled with JetBrains decompiler
// Type: App1uwp.Library.BackgroundTaskUtils
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using System;
using System.Collections.Generic;
using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.Storage;
using Windows.UI.Notifications;

#nullable disable
namespace App1uwp.Library
{
  public class BackgroundTaskUtils
  {
    public const string SampleBackgroundTaskEntryPoint = "NotificationBackgroundComponent.NotificationBackgroundTask";
    public const string SampleBackgroundTaskName = "NotificationBackgroundTask";
    public static string SampleBackgroundTaskProgress = "";
    public static bool SampleBackgroundTaskRegistered = false;

    public static BackgroundTaskRegistration RegisterBackgroundTask(
      string taskEntryPoint,
      string name,
      IBackgroundTrigger trigger,
      IBackgroundCondition condition)
    {
      BackgroundTaskBuilder backgroundTaskBuilder = new BackgroundTaskBuilder();
      backgroundTaskBuilder.put_Name(name);
      backgroundTaskBuilder.put_TaskEntryPoint(taskEntryPoint);
      backgroundTaskBuilder.SetTrigger(trigger);
      if (condition != null)
        backgroundTaskBuilder.AddCondition(condition);
      BackgroundTaskRegistration taskRegistration = backgroundTaskBuilder.Register();
      BackgroundTaskUtils.UpdateBackgroundTaskStatus(name, true);
      BackgroundTaskUtils.UpdateBadge();
      ((IDictionary<string, object>) ApplicationData.Current.LocalSettings.Values).Remove(name);
      return taskRegistration;
    }

    private static void UpdateBadge()
    {
      XmlDocument templateContent = BadgeUpdateManager.GetTemplateContent((BadgeTemplateType) 0);
      ((XmlElement) templateContent.SelectSingleNode("/badge")).SetAttribute("value", "1");
      BadgeNotification badgeNotification = new BadgeNotification(templateContent);
      BadgeUpdateManager.CreateBadgeUpdaterForApplication().Update(badgeNotification);
    }

    public static void UnregisterBackgroundTasks(string name)
    {
      foreach (KeyValuePair<Guid, IBackgroundTaskRegistration> allTask in (IEnumerable<KeyValuePair<Guid, IBackgroundTaskRegistration>>) BackgroundTaskRegistration.AllTasks)
      {
        if (allTask.Value.Name == name)
          allTask.Value.Unregister(true);
      }
      BackgroundTaskUtils.UpdateBackgroundTaskStatus(name, false);
    }

    public static void UpdateBackgroundTaskStatus(string name, bool registered)
    {
      switch (name)
      {
        case "NotificationBackgroundTask":
          BackgroundTaskUtils.SampleBackgroundTaskRegistered = registered;
          break;
      }
    }

    public static string GetBackgroundTaskStatus(string name)
    {
      bool flag = false;
      switch (name)
      {
        case "NotificationBackgroundTask":
          flag = BackgroundTaskUtils.SampleBackgroundTaskRegistered;
          break;
      }
      string backgroundTaskStatus = flag ? "Registered" : "Unregistered";
      ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
      if (((IDictionary<string, object>) localSettings.Values).ContainsKey(name))
        backgroundTaskStatus = backgroundTaskStatus + " - " + ((IDictionary<string, object>) localSettings.Values)[name].ToString();
      return backgroundTaskStatus;
    }
  }
}
