// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.DataBattleRound
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace FFRKInspector.GameData
{
  internal class DataBattleRound
  {
    [JsonProperty("enemy")]
    public List<DataEnemy> Enemies;
    [JsonProperty("round")]
    public uint Index;
    [JsonProperty("drop_materias")]
    public List<DataMateria> Materias;
    [JsonProperty("drop_item_list")]
    public List<DataPotion> Potions;
    [JsonExtensionData]
    public Dictionary<string, JToken> UnknownValues;

    public IEnumerable<DropEvent> Drops
    {
      get
      {
        foreach (DataEnemy enemy1 in this.Enemies)
        {
          DataEnemy enemy = enemy1;
          foreach (DropEvent drop1 in enemy.Drops)
          {
            DropEvent drop = drop1;
            drop.Round = this.Index;
            yield return drop;
            drop = (DropEvent) null;
          }
          enemy = (DataEnemy) null;
        }
        foreach (DataMateria materia1 in this.Materias)
        {
          DataMateria materia = materia1;
          yield return new DropEvent()
          {
            MateriaName = materia.Name,
            Round = this.Index,
            EnemyName = "",
            ItemType = DataEnemyDropItem.DropItemType.Materia
          };
          materia = (DataMateria) null;
        }
        foreach (DataPotion potion1 in this.Potions)
        {
          DataPotion potion = potion1;
          yield return new DropEvent()
          {
            PotionName = potion.Type != 21U ? (potion.Type != 22U ? (potion.Type != 31U ? "Unknown Potion" : "Ether") : "Hi-Potion") : "Potion",
            Round = this.Index,
            EnemyName = "",
            ItemType = DataEnemyDropItem.DropItemType.Potion
          };
          potion = (DataPotion) null;
        }
      }
    }
  }
}
