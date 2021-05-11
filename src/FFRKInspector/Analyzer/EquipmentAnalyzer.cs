// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Analyzer.EquipmentAnalyzer
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.DataCache.Items;
using FFRKInspector.GameData;
using FFRKInspector.GameData.Party;
using FFRKInspector.Proxy;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FFRKInspector.Analyzer
{
  internal class EquipmentAnalyzer
  {
    private static readonly int kDefaultN = 3;
    private List<EquipmentAnalyzer.AnalysisItem> mItems = new List<EquipmentAnalyzer.AnalysisItem>();
    private List<EquipmentAnalyzer.AnalysisBuddy> mBuddies = new List<EquipmentAnalyzer.AnalysisBuddy>();
    private Dictionary<uint, EquipmentAnalyzer.Result> mResults = new Dictionary<uint, EquipmentAnalyzer.Result>();
    private int mTopN = EquipmentAnalyzer.kDefaultN;
    private AnalyzerSettings mSettings;

    public int ItemRankThreshold
    {
      get => this.mTopN;
      set => this.mTopN = value;
    }

    public DataEquipmentInformation[] Items
    {
      set
      {
        this.mResults.Clear();
        this.mItems.Clear();
        foreach (DataEquipmentInformation equipmentInformation in value)
        {
          Key key = new Key()
          {
            ItemId = equipmentInformation.EquipmentId
          };
          Data data = (Data) null;
          EquipmentAnalyzer.AnalysisItem analysisItem = new EquipmentAnalyzer.AnalysisItem()
          {
            Item = equipmentInformation,
            Result = new EquipmentAnalyzer.Result()
          };
          analysisItem.Result.IsValid = true;
          this.mResults[analysisItem.Item.InstanceId] = analysisItem.Result;
          if (!FFRKProxy.Instance.Cache.Items.TryGetValue(key, out data) || !data.AreStatsValid || equipmentInformation.Category == SchemaConstants.EquipmentCategory.Accessory)
          {
            analysisItem.Ignore = true;
            analysisItem.Result.IsValid = false;
          }
          else
          {
            analysisItem.BaseStats = data.BaseStats;
            analysisItem.MaxStats = data.MaxStats;
          }
          this.mItems.Add(analysisItem);
        }
        this.UpdateWhoCanUse();
      }
    }

    public DataBuddyInformation[] Buddies
    {
      set
      {
        this.mBuddies.Clear();
        foreach (DataBuddyInformation buddyInformation in value)
          this.mBuddies.Add(new EquipmentAnalyzer.AnalysisBuddy()
          {
            Buddy = buddyInformation,
            Settings = this.mSettings[buddyInformation.Name]
          });
        this.UpdateWhoCanUse();
      }
    }

    public EquipmentAnalyzer(AnalyzerSettings Settings) => this.mSettings = Settings;

    public EquipmentAnalyzer() => this.mSettings = AnalyzerSettings.DefaultSettings;

    public double GetScore(uint EquipInstanceId)
    {
      EquipmentAnalyzer.Result result = (EquipmentAnalyzer.Result) null;
      return !this.mResults.TryGetValue(EquipInstanceId, out result) || !result.IsValid ? double.NaN : result.Score;
    }

    private void UpdateWhoCanUse()
    {
      foreach (EquipmentAnalyzer.AnalysisBuddy mBuddy in this.mBuddies)
        mBuddy.UsableItems.Clear();
      foreach (EquipmentAnalyzer.AnalysisItem mItem in this.mItems)
        mItem.Users.Clear();
      if (this.mItems.Count == 0 || this.mBuddies.Count == 0)
        return;
      foreach (EquipmentAnalyzer.AnalysisItem mItem in this.mItems)
      {
        EquipmentAnalyzer.AnalysisItem item = mItem;
        foreach (EquipmentAnalyzer.AnalysisBuddy analysisBuddy in this.mBuddies.Where<EquipmentAnalyzer.AnalysisBuddy>((Func<EquipmentAnalyzer.AnalysisBuddy, bool>) (x => x.Buddy.UsableEquipCategories.Contains<SchemaConstants.EquipmentCategory>(item.Item.Category))))
        {
          item.Users.Add(analysisBuddy);
          analysisBuddy.UsableItems.Add(item);
        }
      }
    }

    public void Run()
    {
      this.DebugParallelForEach<EquipmentAnalyzer.AnalysisItem>(this.mItems.Where<EquipmentAnalyzer.AnalysisItem>((Func<EquipmentAnalyzer.AnalysisItem, bool>) (x => !x.Ignore)), (Action<EquipmentAnalyzer.AnalysisItem>) (item =>
      {
        byte num = item.Item.Level;
        switch (this.mSettings.LevelConsideration)
        {
          case AnalyzerSettings.ItemLevelConsideration.Current:
            num = item.Item.Level;
            break;
          case AnalyzerSettings.ItemLevelConsideration.CurrentRankMaxLevel:
            num = item.Item.LevelMax;
            break;
          case AnalyzerSettings.ItemLevelConsideration.FullyMaxed:
            num = StatCalculator.MaxLevel(StatCalculator.Evolve(item.Item.BaseRarity, SchemaConstants.EvolutionLevel.PlusPlus));
            break;
        }
        item.NonSynergizedStats = StatCalculator.ComputeStatsForLevel(item.Item.BaseRarity, item.BaseStats, item.MaxStats, num);
        byte target_level = StatCalculator.EffectiveLevelWithSynergy(num);
        item.SynergizedStats = StatCalculator.ComputeStatsForLevel(item.Item.BaseRarity, item.BaseStats, item.MaxStats, target_level);
      }));
      RealmSynergy.SynergyValue[] synergy_values = RealmSynergy.Values.ToArray<RealmSynergy.SynergyValue>();
      AnalyzerSettings.DefensiveStat[] defensive_stats = Enum.GetValues(typeof (AnalyzerSettings.DefensiveStat)).Cast<AnalyzerSettings.DefensiveStat>().ToArray<AnalyzerSettings.DefensiveStat>();
      AnalyzerSettings.OffensiveStat[] offensive_stats = Enum.GetValues(typeof (AnalyzerSettings.OffensiveStat)).Cast<AnalyzerSettings.OffensiveStat>().ToArray<AnalyzerSettings.OffensiveStat>();
      List<EquipmentAnalyzer.AnalysisItem>[,] best_defensive_items = new List<EquipmentAnalyzer.AnalysisItem>[synergy_values.Length, defensive_stats.Length];
      List<EquipmentAnalyzer.AnalysisItem>[,] best_offensive_items = new List<EquipmentAnalyzer.AnalysisItem>[synergy_values.Length, offensive_stats.Length];
      this.DebugParallelForEach<KeyValuePair<RealmSynergy.SynergyValue, AnalyzerSettings.DefensiveStat>>(this.CartesianProduct<RealmSynergy.SynergyValue, AnalyzerSettings.DefensiveStat>((IEnumerable<RealmSynergy.SynergyValue>) synergy_values, (IEnumerable<AnalyzerSettings.DefensiveStat>) defensive_stats), (Action<KeyValuePair<RealmSynergy.SynergyValue, AnalyzerSettings.DefensiveStat>>) (x =>
      {
        RealmSynergy.SynergyValue synergy = x.Key;
        AnalyzerSettings.DefensiveStat stat = x.Value;
        List<EquipmentAnalyzer.AnalysisItem> analysisItemList = new List<EquipmentAnalyzer.AnalysisItem>(this.mItems.Where<EquipmentAnalyzer.AnalysisItem>((Func<EquipmentAnalyzer.AnalysisItem, bool>) (y => !y.Ignore)));
        analysisItemList.Sort((Comparison<EquipmentAnalyzer.AnalysisItem>) ((a, b) => -this.ChooseDefensiveStat(a.GetEffectiveStats(synergy), stat).CompareTo(this.ChooseDefensiveStat(b.GetEffectiveStats(synergy), stat))));
        best_defensive_items[(int) (synergy.Realm + 1), (int) stat] = analysisItemList;
      }));
      this.DebugParallelForEach<KeyValuePair<RealmSynergy.SynergyValue, AnalyzerSettings.OffensiveStat>>(this.CartesianProduct<RealmSynergy.SynergyValue, AnalyzerSettings.OffensiveStat>((IEnumerable<RealmSynergy.SynergyValue>) synergy_values, (IEnumerable<AnalyzerSettings.OffensiveStat>) offensive_stats), (Action<KeyValuePair<RealmSynergy.SynergyValue, AnalyzerSettings.OffensiveStat>>) (x =>
      {
        RealmSynergy.SynergyValue synergy = x.Key;
        AnalyzerSettings.OffensiveStat stat = x.Value;
        List<EquipmentAnalyzer.AnalysisItem> analysisItemList = new List<EquipmentAnalyzer.AnalysisItem>(this.mItems.Where<EquipmentAnalyzer.AnalysisItem>((Func<EquipmentAnalyzer.AnalysisItem, bool>) (y => !y.Ignore)));
        analysisItemList.Sort((Comparison<EquipmentAnalyzer.AnalysisItem>) ((a, b) => -this.ChooseOffensiveStat(a.GetEffectiveStats(synergy), stat).CompareTo(this.ChooseOffensiveStat(b.GetEffectiveStats(synergy), stat))));
        best_offensive_items[(int) (synergy.Realm + 1), (int) stat] = analysisItemList;
      }));
      this.DebugParallelForEach<EquipmentAnalyzer.AnalysisBuddy>(this.mBuddies.Where<EquipmentAnalyzer.AnalysisBuddy>((Func<EquipmentAnalyzer.AnalysisBuddy, bool>) (x => x.Settings.Score)), (Action<EquipmentAnalyzer.AnalysisBuddy>) (buddy =>
      {
        buddy.TopNDefense = new List<EquipmentAnalyzer.AnalysisItem>[synergy_values.Length, defensive_stats.Length];
        buddy.TopNOffense = new List<EquipmentAnalyzer.AnalysisItem>[synergy_values.Length, offensive_stats.Length];
        for (int index1 = 0; index1 < best_defensive_items.GetLength(0); ++index1)
        {
          for (int index2 = 0; index2 < best_defensive_items.GetLength(1); ++index2)
          {
            List<EquipmentAnalyzer.AnalysisItem> source = best_defensive_items[index1, index2];
            buddy.TopNDefense[index1, index2] = source.Where<EquipmentAnalyzer.AnalysisItem>((Func<EquipmentAnalyzer.AnalysisItem, bool>) (item => item.EnabledUsers.Contains(buddy))).Take<EquipmentAnalyzer.AnalysisItem>(this.mTopN).ToList<EquipmentAnalyzer.AnalysisItem>();
          }
        }
        for (int index1 = 0; index1 < best_offensive_items.GetLength(0); ++index1)
        {
          for (int index2 = 0; index2 < best_offensive_items.GetLength(1); ++index2)
          {
            List<EquipmentAnalyzer.AnalysisItem> source = best_offensive_items[index1, index2];
            buddy.TopNOffense[index1, index2] = source.Where<EquipmentAnalyzer.AnalysisItem>((Func<EquipmentAnalyzer.AnalysisItem, bool>) (item => item.EnabledUsers.Contains(buddy))).Take<EquipmentAnalyzer.AnalysisItem>(this.mTopN).ToList<EquipmentAnalyzer.AnalysisItem>();
          }
        }
      }));
      this.DebugParallelForEach<EquipmentAnalyzer.AnalysisItem>(this.mItems.Where<EquipmentAnalyzer.AnalysisItem>((Func<EquipmentAnalyzer.AnalysisItem, bool>) (x => !x.Ignore)), (Action<EquipmentAnalyzer.AnalysisItem>) (item =>
      {
        bool flag;
        switch (item.Item.Type)
        {
          case SchemaConstants.ItemType.Weapon:
            flag = true;
            break;
          case SchemaConstants.ItemType.Armor:
            flag = false;
            break;
          default:
            return;
        }
        int num1 = this.mTopN * item.EnabledUsers.Count * synergy_values.Length;
        int num2 = 0;
        foreach (EquipmentAnalyzer.AnalysisBuddy analysisBuddy in this.mBuddies.Where<EquipmentAnalyzer.AnalysisBuddy>((Func<EquipmentAnalyzer.AnalysisBuddy, bool>) (b => b.Settings.Score)))
        {
          AnalyzerSettings.PartyMemberSettings mSetting = this.mSettings[analysisBuddy.Buddy.Name];
          List<EquipmentAnalyzer.AnalysisItem>[,] analysisItemListArray = flag ? analysisBuddy.TopNOffense : analysisBuddy.TopNDefense;
          int index1 = flag ? (int) mSetting.OffensiveStat : (int) mSetting.DefensiveStat;
          for (int index2 = 0; index2 < analysisItemListArray.GetLength(0); ++index2)
          {
            List<EquipmentAnalyzer.AnalysisItem> analysisItemList = analysisItemListArray[index2, index1];
            int num3 = analysisItemList.IndexOf(item);
            if (num3 != -1)
              num2 += analysisItemList.Count - num3;
          }
        }
        Debug.Assert(num2 <= num1);
        item.Result.IsValid = true;
        item.Result.Score = (double) num2 / (double) num1;
        item.Result.Score *= 100.0;
      }));
    }

    private void DebugParallelForEach<T>(IEnumerable<T> Source, Action<T> Body) => Parallel.ForEach<T>(Source, Body);

    private IEnumerable<KeyValuePair<T, U>> CartesianProduct<T, U>(
      IEnumerable<T> First,
      IEnumerable<U> Second)
    {
      foreach (T obj in First)
      {
        T key = obj;
        foreach (U u1 in Second)
        {
          U u = u1;
          yield return new KeyValuePair<T, U>(key, u);
          u = default (U);
        }
        key = default (T);
      }
    }

    private short ChooseOffensiveStat(EquipStats stats, AnalyzerSettings.OffensiveStat stat)
    {
      switch (stat)
      {
        case AnalyzerSettings.OffensiveStat.ATK:
          return stats.Atk.GetValueOrDefault((short) 0);
        case AnalyzerSettings.OffensiveStat.MAG:
          return stats.Mag.GetValueOrDefault((short) 0);
        case AnalyzerSettings.OffensiveStat.MND:
          return stats.Mnd.GetValueOrDefault((short) 0);
        default:
          return stats.Atk.GetValueOrDefault((short) 0);
      }
    }

    private short ChooseDefensiveStat(EquipStats stats, AnalyzerSettings.DefensiveStat stat)
    {
      switch (stat)
      {
        case AnalyzerSettings.DefensiveStat.DEF:
          return stats.Def.GetValueOrDefault((short) 0);
        case AnalyzerSettings.DefensiveStat.RES:
          return stats.Res.GetValueOrDefault((short) 0);
        default:
          return stats.Def.GetValueOrDefault((short) 0);
      }
    }

    public class Result
    {
      public double Score;
      public bool IsValid;
    }

    private struct SynergyDefStatCombo
    {
      public RealmSynergy.Value Synergy;
      public AnalyzerSettings.DefensiveStat Stat;
    }

    private struct SynergyOffStatCombo
    {
      public RealmSynergy.Value Synergy;
      public AnalyzerSettings.OffensiveStat Stat;
    }

    private class AnalysisBuddy
    {
      public List<EquipmentAnalyzer.AnalysisItem> UsableItems = new List<EquipmentAnalyzer.AnalysisItem>();
      public DataBuddyInformation Buddy;
      public AnalyzerSettings.PartyMemberSettings Settings;
      public List<EquipmentAnalyzer.AnalysisItem>[,] TopNOffense;
      public List<EquipmentAnalyzer.AnalysisItem>[,] TopNDefense;
    }

    private class AnalysisItem
    {
      private List<EquipmentAnalyzer.AnalysisBuddy> mUsers = new List<EquipmentAnalyzer.AnalysisBuddy>();
      public EquipStats SynergizedStats = new EquipStats();
      public EquipStats NonSynergizedStats = new EquipStats();
      public bool Ignore = false;
      public DataEquipmentInformation Item;
      public EquipmentAnalyzer.Result Result;
      public EquipStats BaseStats;
      public EquipStats MaxStats;

      public List<EquipmentAnalyzer.AnalysisBuddy> Users
      {
        get => this.mUsers;
        set => this.mUsers = value;
      }

      public List<EquipmentAnalyzer.AnalysisBuddy> EnabledUsers => this.mUsers.Where<EquipmentAnalyzer.AnalysisBuddy>((Func<EquipmentAnalyzer.AnalysisBuddy, bool>) (x => x.Settings.Score)).ToList<EquipmentAnalyzer.AnalysisBuddy>();

      public EquipStats GetEffectiveStats(RealmSynergy.SynergyValue synergy) => (int) this.Item.SeriesId == (int) synergy.GameSeries ? this.SynergizedStats : this.NonSynergizedStats;
    }
  }
}
