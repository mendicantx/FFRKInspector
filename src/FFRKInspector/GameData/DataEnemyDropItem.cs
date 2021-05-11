// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.DataEnemyDropItem
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace FFRKInspector.GameData
{
    public class DataEnemyDropItem
  {
    [JsonProperty("rarity")]
    public uint Rarity;
    [JsonProperty("item_id")]
    public uint Id;
    [JsonProperty("type")]
    public DataEnemyDropItem.DropItemType Type;
    [JsonProperty("amount")]
    public uint GoldAmount;
    [JsonProperty("num")]
    public uint NumberOfItems;
    [JsonExtensionData]
    public Dictionary<string, JToken> UnknownValues;

    public enum DropItemType
    {
      Gold = 11, // 0x0000000B
      Equipment = 41, // 0x00000029
      Orb = 51, // 0x00000033
      Magicite = 61, // 0x0000003D
      Potion = 98, // 0x00000062
      Materia = 99, // 0x00000063
    }
  }
}
