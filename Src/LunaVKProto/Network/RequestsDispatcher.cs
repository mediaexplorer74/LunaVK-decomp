// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.RequestsDispatcher
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Utils;
using Luna.Network;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Globalization;
using Windows.UI.Popups;

#nullable disable
namespace App1uwp.Network
{
  public static class RequestsDispatcher
  {
    private static HttpClient httpClient;

    public static async Task<string> DoWebRequestString(
      string url,
      List<KeyValuePair<string, string>> post = null,
      CancellationToken ct = default (CancellationToken))
    {
      string ret = "";
      if (RequestsDispatcher.httpClient == null)
      {
        CookieContainer cookieContainer = new CookieContainer();
        RequestsDispatcher.httpClient = new HttpClient((HttpMessageHandler) new HttpClientHandler()
        {
          AllowAutoRedirect = false,
          CookieContainer = cookieContainer,
          AutomaticDecompression = (DecompressionMethods.Deflate | DecompressionMethods.GZip)
        });
        RequestsDispatcher.httpClient.DefaultRequestHeaders.Add("User-Agent", AppInfo.AppVersionForUserAgent);
      }
      try
      {
        if (post == null)
        {
          HttpResponseMessage response = await RequestsDispatcher.httpClient.GetAsync(url, ct);
          ret = await response.Content.ReadAsStringAsync();
        }
        else
        {
          HttpContent httpContent = (HttpContent) new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>) post);
          HttpResponseMessage response_post = await RequestsDispatcher.httpClient.PostAsync(url, httpContent, ct);
          ret = await response_post.Content.ReadAsStringAsync();
        }
      }
      catch
      {
      }
      return ret;
    }

    public static async Task<VKResponse<T>> Execute<T>(
      string code,
      Func<string, string> customDeserializationFunc = null)
      where T : class
    {
      CancellationToken ct = new CancellationToken();
      VKResponse<T> temp = await RequestsDispatcher.GetResponse<T>("execute", new Dictionary<string, string>()
      {
        [nameof (code)] = code
      }, customDeserializationFunc, ct);
      return temp;
    }

    public static async void ExecuteCallback<T>(
      string code,
      Func<string, string> customDeserializationFunc = null,
      Action<T> callback = null)
      where T : class
    {
      VKResponse<T> temp = await RequestsDispatcher.Execute<T>(code, customDeserializationFunc);
      if (callback == null || temp == null)
        return;
      callback(temp.response);
    }

    private static string GetLang()
    {
      string language = ApplicationLanguages.Languages[0];
      int length = language.IndexOf('-');
      return length == -1 ? language : language.Substring(0, length);
    }

    public static string FixArrayToObject(string jsonStr, string arrayName)
    {
      return jsonStr.Replace(arrayName + "\":[]", arrayName + "\":{}");
    }

    public static async Task<bool> Ping(string url)
    {
      string json = await RequestsDispatcher.GetAsync("https://" + url + "/ping.txt");
      return !string.IsNullOrEmpty(json);
    }

    public static async Task<VKResponse<T>> GetResponse<T>(
      string methodName,
      Dictionary<string, string> parameters,
      Func<string, string> customDeserializationFunc = null,
      CancellationToken ct = default (CancellationToken))
    {
      parameters["v"] = Constants.API_VERSION;
      parameters["access_token"] = Settings.Instance.auth.AccessToken;
      parameters["lang"] = RequestsDispatcher.GetLang();
      string query = string.Format("https://api.{0}/method/{1}", Settings.Instance.UseProxy ? (object) Settings.Instance.ProxyAdress : (object) Constants.HostURL, (object) methodName);
      // ISSUE: reference to a compiler-generated method
      string json = await JsonWebRequest.SendHTTPRequestAsync(query, (IReadOnlyDictionary<string, string>) parameters);
      VKResponse<T> response = (VKResponse<T>) null;
      if (string.IsNullOrEmpty(json))
      {
        response = new VKResponse<T>()
        {
          error = new VKError()
        };
        response.error.error_code = VKErrors.NoNetwork;
        return response;
      }
      try
      {
        if (customDeserializationFunc != null)
          json = customDeserializationFunc(json);
        response = JsonConvert.DeserializeObject<VKResponse<T>>(json);
      }
      catch (Exception ex)
      {
        MessageDialog messageDialog = new MessageDialog(ex.Message + json);
        messageDialog.put_Title("Error");
        IList<IUICommand> commands = messageDialog.Commands;
        UICommand uiCommand1 = new UICommand();
        uiCommand1.put_Label("Ok");
        uiCommand1.put_Id((object) 0);
        UICommand uiCommand2 = uiCommand1;
        commands.Add((IUICommand) uiCommand2);
        messageDialog.ShowAsync();
      }
      if (response.error.error_code != VKErrors.UnknownMethod)
      {
        int errorCode = (int) response.error.error_code;
      }
      return response;
    }

    private static async Task<string> GetAsync(string query, CancellationToken ct = default (CancellationToken))
    {
      string result = string.Empty;
      using (HttpClient client = new HttpClient())
      {
        try
        {
          HttpResponseMessage response = await client.GetAsync(query, ct);
          result = await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
        }
      }
      return result;
    }

    private static async Task<string> PostAsync(
      string requestURL,
      Dictionary<string, string> parameters,
      CancellationToken ct = default (CancellationToken))
    {
      string result = string.Empty;
      using (HttpClient client = new HttpClient())
      {
        try
        {
          HttpResponseMessage response = await client.PostAsync(requestURL, (HttpContent) new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>) parameters));
          result = await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
        }
      }
      return result;
    }

    private static string ConvertDictionaryToQueryString(
      Dictionary<string, string> parameters,
      bool escapeString = true)
    {
      if (parameters == null || parameters.Count == 0)
        return string.Empty;
      StringBuilder stringBuilder = new StringBuilder();
      foreach (KeyValuePair<string, string> parameter in parameters)
      {
        if (parameter.Key != null && parameter.Value != null)
        {
          if (stringBuilder.Length > 0)
            stringBuilder = stringBuilder.Append("&");
          string str = escapeString ? Uri.EscapeDataString(parameter.Value) : parameter.Value;
          stringBuilder = stringBuilder.AppendFormat("{0}={1}", (object) parameter.Key, (object) str);
        }
      }
      return stringBuilder.ToString();
    }

    public static byte[] Combine(byte[] first, byte[] second, byte[] third)
    {
      byte[] dst = new byte[first.Length + second.Length + third.Length];
      Buffer.BlockCopy((Array) first, 0, (Array) dst, 0, first.Length);
      Buffer.BlockCopy((Array) second, 0, (Array) dst, first.Length, second.Length);
      Buffer.BlockCopy((Array) third, 0, (Array) dst, first.Length + second.Length, third.Length);
      return dst;
    }

    public static async Task<T> Upload<T>(
      string uri,
      byte[] data,
      string paramName = "",
      string uploadContentType = "",
      string fileName = null,
      Action<double> progressCallback = null)
    {
      T ret = default (T);
      try
      {
        string str1 = string.Format("----------{0:N}", (object) Guid.NewGuid());
        string str2 = "multipart/form-data; boundary=" + str1;
        string header = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\";\r\nContent-Type: {3}\r\n\r\n", (object) str1, (object) paramName, (object) (fileName ?? "myDataFile"), (object) uploadContentType);
        string footer = string.Format("\r\n--{0}--\r\n", (object) str1);
        HttpClient httpClient = new HttpClient();
        ByteArrayContent byteArrayContent = new ByteArrayContent(RequestsDispatcher.Combine(Encoding.UTF8.GetBytes(header), data, Encoding.UTF8.GetBytes(footer)));
        byteArrayContent.Headers.Remove("Content-Type");
        byteArrayContent.Headers.TryAddWithoutValidation("Content-Type", str2);
        byteArrayContent.Headers.ContentLength = new long?((long) Encoding.UTF8.GetByteCount(header) + (long) data.Length + (long) Encoding.UTF8.GetByteCount(footer));
        HttpResponseMessage response = await httpClient.PostAsync(new Uri(uri), (HttpContent) byteArrayContent);
        byte[] o = await response.Content.ReadAsByteArrayAsync();
        string str = Encoding.UTF8.GetString(o, 0, o.Length);
        ret = JsonConvert.DeserializeObject<T>(str);
      }
      catch (Exception ex)
      {
      }
      return ret;
    }

    public static async void Upload<T>(
      string uri,
      byte[] data,
      string paramName,
      string uploadContentType,
      Action<T> resultCallback,
      string fileName = null,
      Action<double> progressCallback = null)
    {
      T ret = default (T);
      try
      {
        string str1 = string.Format("----------{0:N}", (object) Guid.NewGuid());
        string str2 = "multipart/form-data; boundary=" + str1;
        string header = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\";\r\nContent-Type: {3}\r\n\r\n", (object) str1, (object) paramName, (object) (fileName ?? "myDataFile"), (object) uploadContentType);
        string footer = string.Format("\r\n--{0}--\r\n", (object) str1);
        HttpWebRequest request = (HttpWebRequest) WebRequest.Create(uri);
        request.Method = "POST";
        request.ContentType = str2;
        request.CookieContainer = new CookieContainer();
        request.BeginGetRequestStream((AsyncCallback) (async ar =>
        {
          try
          {
            Stream requestStream = request.EndGetRequestStream(ar);
            requestStream.Write(Encoding.UTF8.GetBytes(header), 0, Encoding.UTF8.GetByteCount(header));
            StreamUtils.CopyStream(data, requestStream, progressCallback);
            requestStream.Write(Encoding.UTF8.GetBytes(footer), 0, Encoding.UTF8.GetByteCount(footer));
            requestStream.Dispose();
            WebResponse web = await request.GetResponseAsync();
            Stream s = web.GetResponseStream();
            byte[] buffer = new byte[web.ContentLength];
            s.Read(buffer, 0, (int) web.ContentLength);
            string str0 = Encoding.UTF8.GetString(buffer, 0, (int) web.ContentLength);
            ret = JsonConvert.DeserializeObject<T>(str0);
            resultCallback(ret);
          }
          catch (Exception ex)
          {
          }
        }), (object) null);
      }
      catch (Exception ex)
      {
      }
    }

    private class MyProxy : IWebProxy
    {
      private readonly Uri _proxyUri;

      public MyProxy(string proxyUri)
      {
        if (!Uri.TryCreate(proxyUri, UriKind.Absolute, out Uri _))
          return;
        this._proxyUri = new Uri(proxyUri);
      }

      public ICredentials Credentials { get; set; }

      public Uri GetProxy(Uri destination) => this._proxyUri;

      public bool IsBypassed(Uri destination) => false;
    }

    public enum AuthState
    {
      NotInitialized,
      NewLocation,
      NeedLoginAndPass,
      GrantAccess,
    }
  }
}
