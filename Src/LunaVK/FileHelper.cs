// Decompiled with JetBrains decompiler
// Type: App1uwp.FileHelper
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

#nullable disable
namespace App1uwp
{
  public static class FileHelper
  {
    public static async Task<bool> WriteTextToFile<T>(string fileName, T obj)
    {
      try
      {
        StorageFile file = await FileHelper.CreateLocalFile(fileName);
        string textToWrite = FileHelper.SerializeToJson<T>(obj);
        await FileIO.WriteTextAsync((IStorageFile) file, textToWrite);
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    public static async Task<T> ReadTextFromFile<T>(string fileName)
    {
      try
      {
        StorageFile file = await FileHelper.GetLocalFileFromName(fileName);
        if (file != null)
        {
          string temp = await FileIO.ReadTextAsync((IStorageFile) file);
          T ret = FileHelper.DeserializeFromJson<T>(temp);
          return ret;
        }
      }
      catch (Exception ex)
      {
      }
      return default (T);
    }

    public static async Task ReadTextFromFile<T>(string fileName, Action<T> callback)
    {
      try
      {
        StorageFile file = await FileHelper.GetLocalFileFromName(fileName);
        if (file == null)
          return;
        string temp = await FileIO.ReadTextAsync((IStorageFile) file);
        T ret = FileHelper.DeserializeFromJson<T>(temp);
        if (callback == null)
          return;
        callback(ret);
      }
      catch (Exception ex)
      {
      }
    }

    public static async void Remove(string fileName)
    {
      try
      {
        StorageFile file = await FileHelper.GetLocalFileFromName(fileName);
        if (file == null)
          return;
        await file.DeleteAsync();
      }
      catch (Exception ex)
      {
      }
    }

    public static async Task<StorageFile> GetLocalFileFromName(string fileName)
    {
      StorageFile ret = (StorageFile) null;
      try
      {
        ret = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
      }
      catch
      {
      }
      return ret;
    }

    public static async Task<StorageFile> CreateLocalFile(string fileName)
    {
      try
      {
        return await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, (CreationCollisionOption) 1);
      }
      catch (Exception ex)
      {
        return (StorageFile) null;
      }
    }

    public static string SerializeToJson<T>(T obj)
    {
      using (StringWriter stringWriter = new StringWriter())
      {
        new JsonSerializer().Serialize((TextWriter) stringWriter, (object) obj);
        return stringWriter.GetStringBuilder().ToString();
      }
    }

    public static T DeserializeFromJson<T>(string json)
    {
      using (StringReader reader1 = new StringReader(json))
      {
        using (JsonTextReader reader2 = new JsonTextReader((TextReader) reader1))
          return new JsonSerializer().Deserialize<T>((JsonReader) reader2);
      }
    }
  }
}
