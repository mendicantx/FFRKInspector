// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.BasicItemDropStats
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.DataCache.Battles;
using FFRKInspector.Proxy;
using FFRKInspector.Utility;

namespace FFRKInspector.GameData
{
  internal class BasicItemDropStats
  {
    public uint ItemId;
    public uint BattleId;
    public uint DungeonId;
    public uint WorldId;
    public string ItemName;
    public string BattleName;
    public string DungeonName;
    public string WorldName;
    public SchemaConstants.DungeonType DungeonType;
    public SchemaConstants.ItemType Type;
    public SchemaConstants.Rarity Rarity;
    public RealmSynergy.SynergyValue Synergy;
    public uint TotalDrops;
    public uint TimesRun;
    public uint TimesRunWithHistogram;
    public ushort BattleStamina;
    public float DropsPerRunF;
    public Histogram Histogram;

    public double DropsAverage => (double) this.TotalDrops / (double) this.TimesRun;

    public ushort StaminaToReachBattle
    {
      get
      {
        Data data;
        return !FFRKProxy.Instance.Cache.Battles.TryGetValue(new Key()
        {
          BattleId = this.BattleId
        }, out data) ? (ushort) 0 : data.StaminaToReach;
      }
    }

    public bool IsBattleRepeatable
    {
      get
      {
        Data data;
        return !FFRKProxy.Instance.Cache.Battles.TryGetValue(new Key()
        {
          BattleId = this.BattleId
        }, out data) || data.Repeatable;
      }
    }

    public string EffectiveDungeonName => this.DungeonType == SchemaConstants.DungeonType.Elite ? this.DungeonName + " (Elite)" : this.DungeonName;

    public float DropsPerRun => (float) this.TotalDrops / (float) this.TimesRun;

    public float StaminaPerDrop => (float) this.BattleStamina * (float) this.TimesRun / (float) this.TotalDrops;

    public BasicItemDropStats() => this.Histogram = new Histogram(6);
  }
}
