// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Proxy.HandleInitiateBattle
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.GameData;
using Fiddler;
using Newtonsoft.Json;

namespace FFRKInspector.Proxy
{
  internal class HandleLabrynthSession : SimpleResponseHandler
  {
    public override bool CanHandle(Session Session) => Session.oRequest.headers.RequestPath.EndsWith("get_display_paintings") || Session.oRequest.headers.RequestPath.EndsWith("select_painting");

    public override void Handle(Session Session)
    {
        LabrynthSessionData displayPaintings = JsonConvert.DeserializeObject<LabrynthSessionData>(Session.GetResponseBodyAsString());
        FFRKProxy.Instance.GameState.LabrynthSessionData = displayPaintings;
        FFRKProxy.Instance.RaiseLabrynthSessionUpdated(displayPaintings);
    }
  }
}
