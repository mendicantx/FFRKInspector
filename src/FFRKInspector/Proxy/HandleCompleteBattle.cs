// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Proxy.HandleCompleteBattle
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.Database;
using FFRKInspector.DataCache.Battles;
using FFRKInspector.GameData;
using Fiddler;

namespace FFRKInspector.Proxy
{
  internal class HandleCompleteBattle : SimpleResponseHandler
  {
    public override bool CanHandle(Session Session)
    {
      string requestPath = ((ClientChatter) Session.oRequest).headers.RequestPath;
      return requestPath.Equals("/dff/battle/win") || requestPath.EndsWith("/win_battle") || (requestPath.Equals("/dff/battle/lose") || requestPath.Equals("/dff/battle/escape")) || (requestPath.EndsWith("/escape_battle") || requestPath.StartsWith("/dff/world/fail") || requestPath.Equals("/dff/battle/quit")) || requestPath.StartsWith("/dff/event/") && requestPath.EndsWith("/quit_battle");
    }

    public override void Handle(Session Session)
    {
      GameState gameState = FFRKProxy.Instance.GameState;
      if (gameState.ActiveBattle == null)
        return;
      EventBattleInitiated activeBattle = gameState.ActiveBattle;
      gameState.ActiveBattle = (EventBattleInitiated) null;
      lock (FFRKProxy.Instance.Cache.SyncRoot)
      {
        Key key = new Key()
        {
          BattleId = activeBattle.Battle.BattleId
        };
        Data data = (Data) null;
        if (FFRKProxy.Instance.Cache.Battles.TryGetValue(key, out data))
        {
          ++data.Samples;
          ++data.HistoSamples;
        }
      }
      FFRKProxy.Instance.RaiseBattleComplete(activeBattle);
    }
  }
}
