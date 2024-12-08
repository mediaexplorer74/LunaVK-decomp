// Decompiled with JetBrains decompiler
// Type: App1uwp.Library.DocumentsService
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network;
using App1uwp.Network.DataObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Streams;

#nullable disable
namespace App1uwp.Library
{
  public class DocumentsService
  {
    private static DocumentsService _instance;

    public static DocumentsService Instance
    {
      get
      {
        if (DocumentsService._instance == null)
          DocumentsService._instance = new DocumentsService();
        return DocumentsService._instance;
      }
    }

    public async Task<string> GetDocumentUploadServer(long optionalGroupId, string type)
    {
      Dictionary<string, string> parameters = new Dictionary<string, string>();
      if (optionalGroupId != 0L)
        parameters["group_id"] = optionalGroupId.ToString();
      VKResponse<DocumentsService.UploadServerAddress> temp = await RequestsDispatcher.GetResponse<DocumentsService.UploadServerAddress>("docs.getUploadServer", parameters);
      return temp != null ? temp.response.upload_url : (string) null;
    }

    public async Task<string> GetPhotoUploadServerAlbum(long aid, long optionalGroupId = 0)
    {
      Dictionary<string, string> parameters = new Dictionary<string, string>();
      parameters["album_id"] = aid.ToString();
      if (optionalGroupId != 0L)
        parameters["group_id"] = optionalGroupId.ToString();
      VKResponse<DocumentsService.UploadServerAddress> temp = await RequestsDispatcher.GetResponse<DocumentsService.UploadServerAddress>("photos.getUploadServer", parameters);
      return temp != null ? temp.response.upload_url : (string) null;
    }

    public async Task<string> GetPhotoMessagesUploadServer()
    {
      Dictionary<string, string> parameters = new Dictionary<string, string>();
      VKResponse<DocumentsService.UploadServerAddress> temp = await RequestsDispatcher.GetResponse<DocumentsService.UploadServerAddress>("photos.getMessagesUploadServer", parameters);
      return temp != null ? temp.response.upload_url : (string) null;
    }

    public async void GetPhotoMessagesUploadServerCallback(Action<string> callback)
    {
      Dictionary<string, string> parameters = new Dictionary<string, string>();
      VKResponse<DocumentsService.UploadServerAddress> temp = await RequestsDispatcher.GetResponse<DocumentsService.UploadServerAddress>("photos.getMessagesUploadServer", parameters);
      if (temp == null)
        callback((string) null);
      else
        callback(temp.response.upload_url);
    }

    public async Task<UploadPhotoResponseData> UploadPhotoToDialog(StorageFile file)
    {
      string u = await DocumentsService.Instance.GetPhotoMessagesUploadServer();
      if (u == null)
        return (UploadPhotoResponseData) null;
      byte[] fileBytes = (byte[]) null;
      using (IRandomAccessStreamWithContentType stream = await file.OpenReadAsync())
      {
        fileBytes = new byte[((IRandomAccessStream) stream).Size];
        using (DataReader reader = new DataReader((IInputStream) stream))
        {
          int num = (int) await (IAsyncOperation<uint>) reader.LoadAsync((uint) ((IRandomAccessStream) stream).Size);
          reader.ReadBytes(fileBytes);
        }
      }
      return await RequestsDispatcher.Upload<UploadPhotoResponseData>(u, fileBytes, "photo", file.ContentType, file.Name);
    }

    public async void ReadFully(StorageFile file, Action<byte[]> callback)
    {
      byte[] fileBytes = (byte[]) null;
      using (IRandomAccessStreamWithContentType stream = await file.OpenReadAsync())
      {
        fileBytes = new byte[((IRandomAccessStream) stream).Size];
        using (DataReader reader = new DataReader((IInputStream) stream))
        {
          int num = (int) await (IAsyncOperation<uint>) reader.LoadAsync((uint) ((IRandomAccessStream) stream).Size);
          reader.ReadBytes(fileBytes);
        }
      }
      callback(fileBytes);
    }

    public void UploadPhoto(
      byte[] photoData,
      Action<VKPhoto, ResultCode> callback,
      Action<double> progressCallback = null)
    {
      DocumentsService.Instance.GetPhotoMessagesUploadServerCallback((Action<string>) (u =>
      {
        if (u == null)
          callback(new VKPhoto(), ResultCode.UnknownError);
        else
          RequestsDispatcher.Upload<UploadPhotoResponseData>(u, photoData, "photo", "image", (Action<UploadPhotoResponseData>) (resp => this.SavePhoto(resp, callback)), "MyImage.jpg", progressCallback);
      }));
    }

    public async Task<VKPhoto> SavePhoto(UploadPhotoResponseData uploadData)
    {
      VKResponse<List<VKPhoto>> temp = await RequestsDispatcher.GetResponse<List<VKPhoto>>("photos.saveMessagesPhoto", new Dictionary<string, string>()
      {
        ["server"] = uploadData.server,
        ["photo"] = uploadData.photo,
        ["hash"] = uploadData.hash
      });
      return temp.response[0];
    }

    public async void SavePhoto(
      UploadPhotoResponseData uploadData,
      Action<VKPhoto, ResultCode> callback)
    {
      VKResponse<List<VKPhoto>> temp = await RequestsDispatcher.GetResponse<List<VKPhoto>>("photos.saveMessagesPhoto", new Dictionary<string, string>()
      {
        ["server"] = uploadData.server,
        ["photo"] = uploadData.photo,
        ["hash"] = uploadData.hash
      });
      callback(temp.response[0], ResultCode.Succeeded);
    }

    private class UploadServerAddress
    {
      public string upload_url { get; set; }
    }
  }
}
