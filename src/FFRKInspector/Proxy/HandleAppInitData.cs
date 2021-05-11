// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Proxy.HandleAppInitData
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.GameData.AppInit;
using Fiddler;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace FFRKInspector.Proxy
{
  internal class HandleAppInitData : SimpleResponseHandler
  {
    public override bool CanHandle(Session Session)
    {
      string requestPath = ((ClientChatter) Session.oRequest).headers.RequestPath;
      return requestPath.Equals("/dff") || requestPath.Equals("/dff/");
    }

    public override void Handle(Session Session) => FFRKProxy.Instance.GameState.AppInitData = JsonConvert.DeserializeObject<AppInitData>(this.GetResponseBody(Session));

    public override string GetResponseBody(Session Session)
    {
      string responseBodyAsString = Session.GetResponseBodyAsString();
      HtmlDocument htmlDocument = new HtmlDocument();
      htmlDocument.LoadHtml(responseBodyAsString);
      return htmlDocument.DocumentNode.SelectSingleNode(".//script[@data-app-init-data]")?.InnerHtml;
    }
  }
}
