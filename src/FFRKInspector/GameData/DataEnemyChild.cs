// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.DataEnemyChild
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace FFRKInspector.GameData
{
  internal class DataEnemyChild
  {
    [JsonProperty("enemy_id")]
    public uint EnemyId;
    [JsonProperty("ai_id")]
    public ulong AiId;
    [JsonProperty("child_pos_id")]
    public uint ChildPosId;
    [JsonProperty("init_hp")]
    public uint InitHp;
    [JsonProperty("max_hp")]
    public uint MaxHp;
    [JsonProperty("lv")]
    public uint Level;
    [JsonProperty("constraints")]
    public List<DataEnemyConstraints> Constraints;
    [JsonProperty("params")]
    public List<DataEnemyParam> Params;
    [JsonProperty("drop_item_list")]
    public List<DataEnemyDropItem> DropItems;
    [JsonExtensionData]
    public Dictionary<string, JToken> UnknownValues;

    public string Name => this.Params.Count > 0 ? this.Params[0].Name : "";
  }
}
