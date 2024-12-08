// Decompiled with JetBrains decompiler
// Type: App1uwp.Framework.CacheManager
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

#nullable disable
namespace App1uwp.Framework
{
  public static class CacheManager
  {
    private static string _cacheFolderName = "CachedDataV4";
    private static string _stateFolderName = "CachedData";

    public static async void EnsureCacheFolderExists()
    {
      StorageFolder localFolder = ApplicationData.Current.LocalFolder;
      StorageFolder folderAsync1 = await localFolder.CreateFolderAsync(CacheManager._cacheFolderName, (CreationCollisionOption) 3);
      StorageFolder folderAsync2 = await localFolder.CreateFolderAsync(CacheManager._stateFolderName, (CreationCollisionOption) 3);
    }

    public static async void EraseAll()
    {
      StorageFolder localFolder = ApplicationData.Current.LocalFolder;
      await localFolder.DeleteAsync();
    }

    public static string GetFilePath(
      string fileId,
      CacheManager.DataType dataType = CacheManager.DataType.CachedData,
      string pathSeparator = "/")
    {
      return CacheManager.GetFolderNameForDataType(dataType) + pathSeparator + fileId;
    }

    public static string GetFullFilePath(string fileId, CacheManager.DataType dataType = CacheManager.DataType.CachedData)
    {
      return Path.Combine(ApplicationData.Current.LocalFolder.Path, CacheManager.GetFilePath(fileId, dataType, "\\"));
    }

    public static string TrySerializeToString(IBinarySerializable obj)
    {
      try
      {
        using (MemoryStream memoryStream = new MemoryStream())
        {
          BinaryWriter writer = new BinaryWriter((Stream) memoryStream);
          obj.Write(writer);
          memoryStream.Position = 0L;
          return CacheManager.AsciiToString(new BinaryReader((Stream) memoryStream).ReadBytes((int) memoryStream.Length));
        }
      }
      catch (Exception ex)
      {
      }
      return "";
    }

    public static void TryDeserializeFromString(IBinarySerializable obj, string serStr)
    {
      try
      {
        using (MemoryStream input = new MemoryStream(CacheManager.StringToAscii(serStr)))
        {
          BinaryReader reader = new BinaryReader((Stream) input);
          obj.Read(reader);
        }
      }
      catch (Exception ex)
      {
      }
    }

    public static byte[] StringToAscii(string s)
    {
      byte[] ascii = new byte[s.Length];
      for (int index = 0; index < s.Length; ++index)
      {
        char ch = s[index];
        ascii[index] = ch > '\u007F' ? (byte) 63 : (byte) ch;
      }
      return ascii;
    }

    public static string AsciiToString(byte[] bytes)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (byte num in bytes)
        stringBuilder = stringBuilder.Append((char) num);
      return stringBuilder.ToString();
    }

    public static async Task<bool> TryDeserialize(
      IBinarySerializable obj,
      string fileId,
      CacheManager.DataType dataType = CacheManager.DataType.CachedData)
    {
      try
      {
        StorageFolder localFolder = ApplicationData.Current.LocalFolder;
        string filePath = CacheManager.GetFilePath(fileId, dataType);
        Stream storageFileStream = await ((IStorageFolder) localFolder).OpenStreamForReadAsync(filePath);
        BinaryReader reader = new BinaryReader(storageFileStream);
        obj.Read(reader);
      }
      catch
      {
        return false;
      }
      return true;
    }

    public static async Task<bool> TryDeleteAsync(string fileId)
    {
      bool result;
      try
      {
        await (await (await ApplicationData.Current.LocalFolder.GetFolderAsync(CacheManager.GetFolderNameForDataType(CacheManager.DataType.CachedData))).GetFileAsync(fileId)).DeleteAsync();
      }
      catch
      {
        result = false;
        return result;
      }
      result = true;
      return result;
    }

    public static string GetFolderNameForDataType(CacheManager.DataType dataType)
    {
      if (dataType == CacheManager.DataType.CachedData)
        return CacheManager._cacheFolderName;
      if (dataType == CacheManager.DataType.StateData)
        return CacheManager._stateFolderName;
      throw new Exception("Unknown data type");
    }

    public static async Task<bool> TrySerializeAsync(
      IBinarySerializable obj,
      string fileId,
      bool trim = false,
      CacheManager.DataType dataType = CacheManager.DataType.CachedData)
    {
      bool result;
      try
      {
        Stream arg_FC_0 = await ((IStorageFolder) await ApplicationData.Current.LocalFolder.GetFolderAsync(CacheManager.GetFolderNameForDataType(dataType))).OpenStreamForWriteAsync(fileId, (CreationCollisionOption) 1);
        BinaryWriter writer = new BinaryWriter(arg_FC_0);
        if (trim && obj is IBinarySerializableWithTrimSupport)
          (obj as IBinarySerializableWithTrimSupport).WriteTrimmed(writer);
        else
          obj.Write(writer);
        arg_FC_0.Dispose();
        result = true;
      }
      catch
      {
        result = false;
      }
      return result;
    }

    public static async Task<string> TrySaveRawCachedData(byte[] bytes, string fileId)
    {
      try
      {
        StorageFolder localFolder = ApplicationData.Current.LocalFolder;
        string new_path = CacheManager.GetFilePath(fileId, pathSeparator: "\\");
        await CacheManager.MakeFolders(localFolder, new_path);
        StorageFile storageFile = await localFolder.CreateFileAsync(new_path, (CreationCollisionOption) 1);
        Stream outputStream = await ((IStorageFile) storageFile).OpenStreamForWriteAsync();
        outputStream.Write(bytes, 0, bytes.Length);
        outputStream.Position = 0L;
        outputStream.Dispose();
        return localFolder.Path + "\\" + new_path;
      }
      catch (Exception ex)
      {
      }
      return (string) null;
    }

    private static async Task MakeFolders(StorageFolder localFolder, string path)
    {
      int slash = path.IndexOf("/");
      if (slash <= 0)
        return;
      string new_path = path.Substring(0, slash);
      StorageFolder opened_folder = await localFolder.CreateFolderAsync(new_path, (CreationCollisionOption) 3);
      string very_new_path = path.Remove(0, new_path.Length + 1);
      await CacheManager.MakeFolders(opened_folder, very_new_path);
    }

    public static async Task<bool> MakeFile(string file)
    {
      StorageFolder localFolder = ApplicationData.Current.LocalFolder;
      await CacheManager.MakeFolders(localFolder, file);
      StorageFile fileAsync = await localFolder.CreateFileAsync(file, (CreationCollisionOption) 1);
      return true;
    }

    public static async Task<bool> TrySerialize(
      IBinarySerializable obj,
      string fileId,
      bool trim = false,
      CacheManager.DataType dataType = CacheManager.DataType.CachedData)
    {
      try
      {
        StorageFolder localFolder = ApplicationData.Current.LocalFolder;
        string new_path = CacheManager.GetFilePath(fileId);
        StorageFile storageFile = await localFolder.CreateFileAsync(new_path, (CreationCollisionOption) 1);
        Stream storageFileStream = await ((IStorageFile) storageFile).OpenStreamForWriteAsync();
        BinaryWriter writer = new BinaryWriter(storageFileStream);
        if (trim && obj is IBinarySerializableWithTrimSupport)
          (obj as IBinarySerializableWithTrimSupport).WriteTrimmed(writer);
        else
          obj.Write(writer);
        return true;
      }
      catch (Exception ex)
      {
      }
      return false;
    }

    public static async Task<Stream> GetStreamForWrite(string fileId)
    {
      StorageFolder localFolder = ApplicationData.Current.LocalFolder;
      string new_path = CacheManager.GetFilePath(fileId);
      StorageFile storageFile = await localFolder.CreateFileAsync(new_path, (CreationCollisionOption) 1);
      Stream storageFileStream = await ((IStorageFile) storageFile).OpenStreamForWriteAsync();
      return storageFileStream;
    }

    public static async Task<Stream> GetStreamForRead(string fileId)
    {
      StorageFolder localFolder = ApplicationData.Current.LocalFolder;
      string new_path = fileId;
      Stream storageFileStream = await ((IStorageFolder) localFolder).OpenStreamForReadAsync(new_path);
      return storageFileStream;
    }

    public static async Task<StorageFile> GetFileAsync(string fileId)
    {
      StorageFile result;
      try
      {
        result = await StorageFile.GetFileFromPathAsync(CacheManager.GetFullFilePath(fileId));
        return result;
      }
      catch
      {
      }
      result = (StorageFile) null;
      return result;
    }

    public static async Task<bool> TryDelete(string fileId, CacheManager.DataType dataType = CacheManager.DataType.CachedData)
    {
      try
      {
        StorageFolder localFolder = ApplicationData.Current.LocalFolder;
        string filePath = CacheManager.GetFilePath(fileId, dataType);
        StorageFile f = await localFolder.GetFileAsync(filePath);
        if (f == null)
          return false;
        await f.DeleteAsync();
        return true;
      }
      catch (Exception ex)
      {
      }
      return false;
    }

    public enum DataType
    {
      CachedData,
      StateData,
    }
  }
}
