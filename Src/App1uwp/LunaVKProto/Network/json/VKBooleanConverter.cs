// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.json.VKBooleanConverter
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using Newtonsoft.Json;
using System;

#nullable disable
namespace App1uwp.Network.json
{
  internal sealed class VKBooleanConverter : JsonConverter
  {
    public override bool CanConvert(Type objectType) => objectType == typeof (bool);

    public override object ReadJson(
      JsonReader reader,
      Type objectType,
      object existingValue,
      JsonSerializer serializer)
    {
      return (object) (reader.Value.ToString() == "1");
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
      writer.WriteValue((bool) value ? 1 : 0);
    }
  }
}
