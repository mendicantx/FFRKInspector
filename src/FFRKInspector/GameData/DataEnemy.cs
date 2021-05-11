// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.DataEnemy
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace FFRKInspector.GameData
{
  public class DataEnemy
  {
    [JsonProperty("id")]
    public uint Id;
    [JsonProperty("ai_id")]
    public ulong AiId;
    [JsonProperty("children")]
    public List<DataEnemyChild> Children;
    [JsonProperty("ai_arguments")]
    public List<DataAIArgs> AIArgs;
    [JsonExtensionData]
    public Dictionary<string, JToken> UnknownValues;

    public IEnumerable<DropEvent> Drops
    {
      get
      {
        foreach (DataEnemyChild child1 in this.Children)
        {
          DataEnemyChild child = child1;
          foreach (DataEnemyDropItem dropItem1 in child.DropItems)
          {
            DataEnemyDropItem dropItem = dropItem1;
            DropEvent drop = new DropEvent();
            drop.ItemId = dropItem.Id;
            drop.Rarity = dropItem.Rarity;
            drop.ItemType = dropItem.Type;
            drop.GoldAmount = dropItem.GoldAmount;
            drop.NumberOfItems = dropItem.NumberOfItems;
            drop.EnemyId = child.EnemyId;
            if (child.Params.Count > 0)
              drop.EnemyName = child.Params[0].Name;
            yield return drop;
            drop = (DropEvent) null;
            dropItem = (DataEnemyDropItem) null;
          }
          child = (DataEnemyChild) null;
        }
      }
    }
  }
}
