// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.StatCalculator
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using System;
using System.Diagnostics;

namespace FFRKInspector.GameData
{
  internal static class StatCalculator
  {
    public static byte MaxLevel(SchemaConstants.Rarity rarity) => rarity == SchemaConstants.Rarity.One ? (byte) 3 : (byte) (5U * (uint) (rarity - (byte) 1));

    public static SchemaConstants.Rarity Evolve(
      SchemaConstants.Rarity rarity,
      int num_times)
    {
      Debug.Assert(num_times <= 2);
      return rarity + (byte) num_times;
    }

    public static SchemaConstants.Rarity Evolve(
      SchemaConstants.Rarity rarity,
      SchemaConstants.EvolutionLevel evo)
    {
      return StatCalculator.Evolve(rarity, (int) evo);
    }

    public static SchemaConstants.Rarity EvolveAsMuchAsPossible(
      SchemaConstants.Rarity base_rarity,
      SchemaConstants.Rarity cur_rarity,
      int max_times)
    {
      int num_times = Math.Min((int) (cur_rarity - base_rarity) + max_times, 2);
      return StatCalculator.Evolve(base_rarity, num_times);
    }

    public static short ComputeStatForLevel(
      SchemaConstants.Rarity base_rarity,
      short? base_stat,
      short? max_stat,
      byte target_level)
    {
      return StatCalculator.ComputeStatForLevel2(base_stat.GetValueOrDefault((short) 0), (byte) 1, max_stat.GetValueOrDefault((short) 0), StatCalculator.MaxLevel(StatCalculator.Evolve(base_rarity, SchemaConstants.EvolutionLevel.PlusPlus)), target_level);
    }

    public static short ComputeStatForLevel2(
      short vstat1,
      byte vlevel1,
      short vstat2,
      byte vlevel2,
      byte target_level)
    {
      double num = ((double) vstat2 - (double) vstat1) / (double) ((int) vlevel2 - (int) vlevel1);
      return (short) Math.Ceiling((double) vstat1 + (double) ((int) target_level - 1) * num);
    }

    public static byte EffectiveLevelWithSynergy(byte current_level)
    {
      if (current_level < (byte) 5)
        return (byte) ((uint) current_level + 15U);
      byte num = (byte) (20 + 10 * ((int) current_level / 5 - 1));
      return (byte) ((uint) current_level + (uint) num);
    }

    public static EquipStats ComputeStatsForLevel(
      SchemaConstants.Rarity base_rarity,
      EquipStats base_stats,
      EquipStats max_stats,
      byte target_level)
    {
      return new EquipStats()
      {
        Acc = new short?(StatCalculator.ComputeStatForLevel(base_rarity, base_stats.Acc, max_stats.Acc, target_level)),
        Atk = new short?(StatCalculator.ComputeStatForLevel(base_rarity, base_stats.Atk, max_stats.Atk, target_level)),
        Def = new short?(StatCalculator.ComputeStatForLevel(base_rarity, base_stats.Def, max_stats.Def, target_level)),
        Res = new short?(StatCalculator.ComputeStatForLevel(base_rarity, base_stats.Res, max_stats.Res, target_level)),
        Eva = new short?(StatCalculator.ComputeStatForLevel(base_rarity, base_stats.Eva, max_stats.Eva, target_level)),
        Mag = new short?(StatCalculator.ComputeStatForLevel(base_rarity, base_stats.Mag, max_stats.Mag, target_level)),
        Mnd = new short?(StatCalculator.ComputeStatForLevel(base_rarity, base_stats.Mnd, max_stats.Mnd, target_level))
      };
    }
  }
}
