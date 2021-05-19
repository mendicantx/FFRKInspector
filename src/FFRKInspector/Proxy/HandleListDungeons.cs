// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Proxy.HandleListDungeons
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.Database;
using FFRKInspector.DataCache.Dungeons;
using FFRKInspector.GameData;
using Fiddler;
using Newtonsoft.Json;

namespace FFRKInspector.Proxy
{
  internal class HandleListDungeons : SimpleResponseHandler
  {
    public override bool CanHandle(Session Session)
    {
      string requestPath = ((ClientChatter) Session.oRequest).headers.RequestPath;
      return requestPath.StartsWith("/dff/world/dungeons") | (requestPath.StartsWith("/dff/event/") && requestPath.Contains("/dungeons?")) | requestPath.StartsWith("/dff/mo/common/world/dungeons");
    }

    public override void Handle(Session Session)
    {
      EventListDungeons dungeons = JsonConvert.DeserializeObject<EventListDungeons>(Session.GetResponseBodyAsString());
      lock (FFRKProxy.Instance.Cache.SyncRoot)
      {
        foreach (DataDungeon dungeon in dungeons.Dungeons)
        {
          Key key = new Key() { DungeonId = dungeon.Id };
          Data data = (Data) null;
          if (!FFRKProxy.Instance.Cache.Dungeons.TryGetValue(key, out data))
          {
            Data v = new Data()
            {
              Difficulty = dungeon.Difficulty,
              Name = dungeon.Name,
              Series = dungeon.SeriesId,
              Type = dungeon.Type,
              WorldId = dungeon.WorldId
            };
            FFRKProxy.Instance.Cache.Dungeons.Update(key, v);
          }
        }
      }
      FFRKProxy.Instance.RaiseListDungeons(dungeons);
    }
  }
}
