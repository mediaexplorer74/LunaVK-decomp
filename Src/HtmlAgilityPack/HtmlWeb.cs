// Decompiled with JetBrains decompiler
// Type: HtmlAgilityPack.HtmlWeb
// Assembly: HtmlAgilityPack, Version=1.7.1.0, Culture=neutral, PublicKeyToken=bd319b19eaf3b43a
// MVID: B5498F67-4C4E-4FFB-BDE4-EB084EEE38F2
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\HtmlAgilityPack.dll

using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

#nullable disable
namespace HtmlAgilityPack
{
  public class HtmlWeb
  {
    public Action<HtmlDocument> PreHandleDocument { get; set; }

    public async Task<HtmlDocument> LoadFromWebAsync(string url)
    {
      return await this.LoadFromWebAsync(new Uri(url), (Encoding) null, (NetworkCredential) null);
    }

    public async Task<HtmlDocument> LoadFromWebAsync(string url, Encoding encoding)
    {
      return await this.LoadFromWebAsync(new Uri(url), encoding, (NetworkCredential) null);
    }

    public async Task<HtmlDocument> LoadFromWebAsync(
      string url,
      Encoding encoding,
      string userName,
      string password)
    {
      return await this.LoadFromWebAsync(new Uri(url), encoding, new NetworkCredential(userName, password));
    }

    public async Task<HtmlDocument> LoadFromWebAsync(
      string url,
      Encoding encoding,
      string userName,
      string password,
      string domain)
    {
      return await this.LoadFromWebAsync(new Uri(url), encoding, new NetworkCredential(userName, password, domain));
    }

    public async Task<HtmlDocument> LoadFromWebAsync(
      string url,
      string userName,
      string password,
      string domain)
    {
      return await this.LoadFromWebAsync(new Uri(url), (Encoding) null, new NetworkCredential(userName, password, domain));
    }

    public async Task<HtmlDocument> LoadFromWebAsync(string url, string userName, string password)
    {
      return await this.LoadFromWebAsync(new Uri(url), (Encoding) null, new NetworkCredential(userName, password));
    }

    public async Task<HtmlDocument> LoadFromWebAsync(string url, NetworkCredential credentials)
    {
      return await this.LoadFromWebAsync(new Uri(url), (Encoding) null, credentials);
    }

    public async Task<HtmlDocument> LoadFromWebAsync(
      Uri uri,
      Encoding encoding,
      NetworkCredential credentials)
    {
      HttpClientHandler handler = new HttpClientHandler();
      if (credentials == null)
        handler.UseDefaultCredentials = true;
      else
        handler.Credentials = (ICredentials) credentials;
      HttpResponseMessage e = await new HttpClient((HttpMessageHandler) handler).GetAsync(uri);
      if (e.StatusCode != HttpStatusCode.OK)
        throw new Exception("Error downloading html");
      string html = string.Empty;
      if (encoding != null)
      {
        using (StreamReader streamReader = new StreamReader(await e.Content.ReadAsStreamAsync(), encoding))
          html = streamReader.ReadToEnd();
      }
      else
        html = await e.Content.ReadAsStringAsync();
      HtmlDocument htmlDocument = new HtmlDocument();
      if (this.PreHandleDocument != null)
        this.PreHandleDocument(htmlDocument);
      htmlDocument.LoadHtml(html);
      return htmlDocument;
    }
  }
}
