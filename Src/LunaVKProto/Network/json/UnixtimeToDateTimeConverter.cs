// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.json.UnixtimeToDateTimeConverter
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using Newtonsoft.Json;
using System;

#nullable disable
namespace App1uwp.Network.json
{
  public class UnixtimeToDateTimeConverter : JsonConverter
  {
    public override bool CanConvert(Type objectType) => true;

    public override object ReadJson(
      JsonReader reader,
      Type objectType,
      object existingValue,
      JsonSerializer serializer)
    {
      long unixTime = !(reader.Value is string) ? (long) reader.Value : long.Parse(reader.Value.ToString());
      return unixTime == 0L ? (object) null : (object) UnixtimeToDateTimeConverter.GetDateTimeFromUnixTime(unixTime);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
      writer.WriteValue(UnixtimeToDateTimeConverter.GetUnixTimeFromDateTime((DateTime) value));
    }

    public static DateTime GetDateTimeFromUnixTime(long unixTime, bool includeTimeDiff = true)
    {
      DateTime timeFromUnixTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
      if (includeTimeDiff)
      {
        int minusLocalTimeDelta = Settings.Instance.ServerMinusLocalTimeDelta;
        unixTime -= (long) minusLocalTimeDelta;
      }
      timeFromUnixTime = timeFromUnixTime.AddSeconds((double) unixTime).ToLocalTime();
      return timeFromUnixTime;
    }

    public static long GetUnixTimeFromDateTime(DateTime dateTime)
    {
      return (long) (dateTime - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
    }
  }
}
