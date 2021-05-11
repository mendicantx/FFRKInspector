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
  internal class HandleInitiateBattle : SimpleResponseHandler
  {
    public override bool CanHandle(Session Session) => ((ClientChatter) Session.oRequest).headers.RequestPath.EndsWith("get_battle_init_data");

    public override void Handle(Session Session)
    {
      EventBattleInitiated battle = JsonConvert.DeserializeObject<EventBattleInitiated>(Session.GetResponseBodyAsString());
      FFRKProxy.Instance.GameState.ActiveBattle = battle;
      FFRKProxy.Instance.RaiseBattleInitiated(battle);
    }
  }
}
