// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Proxy.HandleListBattles
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.Database;
using FFRKInspector.DataCache.Battles;
using FFRKInspector.GameData;
using Fiddler;
using Newtonsoft.Json;
using System;

namespace FFRKInspector.Proxy
{
  internal class HandleListBattles : SimpleResponseHandler
  {
    public override bool CanHandle(Session Session)
    {
      string requestPath = ((ClientChatter) Session.oRequest).headers.RequestPath;
      return requestPath.Equals("/dff/world/battles") | requestPath.Equals("/dff/mo/single/world/battles");
    }

    public override void Handle(Session Session)
    {
      EventListBattles battles = JsonConvert.DeserializeObject<EventListBattles>(Session.GetResponseBodyAsString());
      FFRKProxy.Instance.GameState.ActiveDungeon = battles;
      lock (FFRKProxy.Instance.Cache.SyncRoot)
      {
        battles.Battles.Sort((Comparison<DataBattle>) ((x, y) => x.Id.CompareTo(y.Id)));
        ushort num = 0;
        for (int index = 0; index < battles.Battles.Count; ++index)
        {
          DataBattle battle = battles.Battles[index];
          Key key = new Key() { BattleId = battle.Id };
          Data v = (Data) null;
          if (!FFRKProxy.Instance.Cache.Battles.TryGetValue(key, out v))
          {
            v = new Data()
            {
              DungeonId = battle.DungeonId,
              HistoSamples = 1U,
              Name = battle.Name,
              Repeatable = index < battles.Battles.Count - 1,
              Samples = 1U,
              Stamina = battle.Stamina,
              StaminaToReach = num
            };
            FFRKProxy.Instance.Cache.Battles.Update(key, v);
          }
          num += battle.Stamina;
        }
      }
      FFRKProxy.Instance.RaiseListBattles(battles);
    }
  }
}
