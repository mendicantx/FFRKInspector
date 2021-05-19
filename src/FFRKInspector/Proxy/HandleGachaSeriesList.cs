// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Proxy.HandleGachaSeriesList
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.Database;
using FFRKInspector.GameData;
using Fiddler;
using Newtonsoft.Json;

namespace FFRKInspector.Proxy
{
  internal class HandleGachaSeriesList : SimpleResponseHandler
  {
    public override bool CanHandle(Session Session) => ((ClientChatter) Session.oRequest).headers.RequestPath.StartsWith("/dff/gacha/show");

    public override void Handle(Session Session)
    {
      DataGachaSeriesList SeriesList = JsonConvert.DeserializeObject<DataGachaSeriesList>(Session.GetResponseBodyAsString());
      FFRKProxy.Instance.GameState.GachaSeries = SeriesList;
      FFRKProxy.Instance.RaiseGachaSeriesList();
    }
  }
}
