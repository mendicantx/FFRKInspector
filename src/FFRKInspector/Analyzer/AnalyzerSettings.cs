// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Analyzer.AnalyzerSettings
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using System.Collections.Generic;

namespace FFRKInspector.Analyzer
{
  internal class AnalyzerSettings
  {
    private static AnalyzerSettings mDefaultSettings = new AnalyzerSettings();
    private AnalyzerSettings.ItemLevelConsideration mLevelConsideration;
    private Dictionary<string, AnalyzerSettings.PartyMemberSettings> mConfiguration;
    private static AnalyzerSettings.PartyMemberSettings mDefaultMemberSettings;

    public static AnalyzerSettings DefaultSettings => AnalyzerSettings.mDefaultSettings.Clone();

    public AnalyzerSettings.PartyMemberSettings this[string Name]
    {
      get
      {
        AnalyzerSettings.PartyMemberSettings partyMemberSettings = (AnalyzerSettings.PartyMemberSettings) null;
        if (this.mConfiguration.TryGetValue(Name, out partyMemberSettings))
          return partyMemberSettings;
        return this == AnalyzerSettings.mDefaultSettings ? AnalyzerSettings.mDefaultMemberSettings.Clone() : AnalyzerSettings.mDefaultSettings[Name];
      }
    }

    public AnalyzerSettings.ItemLevelConsideration LevelConsideration
    {
      get => this.mLevelConsideration;
      set => this.mLevelConsideration = value;
    }

    static AnalyzerSettings()
    {
      AnalyzerSettings.mDefaultSettings.mLevelConsideration = AnalyzerSettings.ItemLevelConsideration.Current;
      AnalyzerSettings.mDefaultMemberSettings = new AnalyzerSettings.PartyMemberSettings()
      {
        Score = true,
        DefensiveStat = AnalyzerSettings.DefensiveStat.DEF,
        OffensiveStat = AnalyzerSettings.OffensiveStat.ATK
      };
      AnalyzerSettings.AddDefault("Tyro", false, AnalyzerSettings.OffensiveStat.ATK, AnalyzerSettings.DefensiveStat.DEF);
      AnalyzerSettings.AddDefault("Warrior", false, AnalyzerSettings.OffensiveStat.ATK, AnalyzerSettings.DefensiveStat.DEF);
      AnalyzerSettings.AddDefault("Knight", false, AnalyzerSettings.OffensiveStat.ATK, AnalyzerSettings.DefensiveStat.DEF);
      AnalyzerSettings.AddDefault("Red Mage", false, AnalyzerSettings.OffensiveStat.MAG, AnalyzerSettings.DefensiveStat.RES);
      AnalyzerSettings.AddDefault("Black Mage", false, AnalyzerSettings.OffensiveStat.MAG, AnalyzerSettings.DefensiveStat.RES);
      AnalyzerSettings.AddDefault("White Mage", false, AnalyzerSettings.OffensiveStat.MND, AnalyzerSettings.DefensiveStat.RES);
      AnalyzerSettings.AddDefault("Summoner", false, AnalyzerSettings.OffensiveStat.MAG, AnalyzerSettings.DefensiveStat.RES);
      AnalyzerSettings.AddDefault("Ranger", false, AnalyzerSettings.OffensiveStat.ATK, AnalyzerSettings.DefensiveStat.DEF);
      AnalyzerSettings.AddDefault("Bard", false, AnalyzerSettings.OffensiveStat.MND, AnalyzerSettings.DefensiveStat.RES);
      AnalyzerSettings.AddDefault("Warrior of Light", false, AnalyzerSettings.OffensiveStat.ATK, AnalyzerSettings.DefensiveStat.DEF);
      AnalyzerSettings.AddDefault("Gordon", true, AnalyzerSettings.OffensiveStat.ATK, AnalyzerSettings.DefensiveStat.DEF);
      AnalyzerSettings.AddDefault("Josef", true, AnalyzerSettings.OffensiveStat.ATK, AnalyzerSettings.DefensiveStat.DEF);
      AnalyzerSettings.AddDefault("Luneth", true, AnalyzerSettings.OffensiveStat.ATK, AnalyzerSettings.DefensiveStat.DEF);
      AnalyzerSettings.AddDefault("Cecil", true, AnalyzerSettings.OffensiveStat.ATK, AnalyzerSettings.DefensiveStat.DEF);
      AnalyzerSettings.AddDefault("Kain", true, AnalyzerSettings.OffensiveStat.ATK, AnalyzerSettings.DefensiveStat.DEF);
      AnalyzerSettings.AddDefault("Rydia", true, AnalyzerSettings.OffensiveStat.MAG, AnalyzerSettings.DefensiveStat.RES);
      AnalyzerSettings.AddDefault("Lenna", true, AnalyzerSettings.OffensiveStat.MND, AnalyzerSettings.DefensiveStat.RES);
      AnalyzerSettings.AddDefault("Terra", true, AnalyzerSettings.OffensiveStat.MAG, AnalyzerSettings.DefensiveStat.RES);
      AnalyzerSettings.AddDefault("Celes", true, AnalyzerSettings.OffensiveStat.MAG, AnalyzerSettings.DefensiveStat.RES);
      AnalyzerSettings.AddDefault("Cyan", true, AnalyzerSettings.OffensiveStat.ATK, AnalyzerSettings.DefensiveStat.DEF);
      AnalyzerSettings.AddDefault("Cloud", true, AnalyzerSettings.OffensiveStat.ATK, AnalyzerSettings.DefensiveStat.DEF);
      AnalyzerSettings.AddDefault("Tifa", true, AnalyzerSettings.OffensiveStat.ATK, AnalyzerSettings.DefensiveStat.DEF);
      AnalyzerSettings.AddDefault("Aerith", true, AnalyzerSettings.OffensiveStat.MND, AnalyzerSettings.DefensiveStat.RES);
      AnalyzerSettings.AddDefault("Sephiroth", true, AnalyzerSettings.OffensiveStat.ATK, AnalyzerSettings.DefensiveStat.DEF);
      AnalyzerSettings.AddDefault("Rinoa", true, AnalyzerSettings.OffensiveStat.MAG, AnalyzerSettings.DefensiveStat.RES);
      AnalyzerSettings.AddDefault("Tidus", true, AnalyzerSettings.OffensiveStat.ATK, AnalyzerSettings.DefensiveStat.DEF);
      AnalyzerSettings.AddDefault("Wakka", true, AnalyzerSettings.OffensiveStat.ATK, AnalyzerSettings.DefensiveStat.DEF);
      AnalyzerSettings.AddDefault("Snow", true, AnalyzerSettings.OffensiveStat.ATK, AnalyzerSettings.DefensiveStat.DEF);
      AnalyzerSettings.AddDefault("Vanille", true, AnalyzerSettings.OffensiveStat.MND, AnalyzerSettings.DefensiveStat.RES);
    }

    public AnalyzerSettings() => this.mConfiguration = new Dictionary<string, AnalyzerSettings.PartyMemberSettings>();

    private AnalyzerSettings Clone()
    {
      AnalyzerSettings analyzerSettings = (AnalyzerSettings) this.MemberwiseClone();
      analyzerSettings.mConfiguration = new Dictionary<string, AnalyzerSettings.PartyMemberSettings>((IDictionary<string, AnalyzerSettings.PartyMemberSettings>) analyzerSettings.mConfiguration);
      return analyzerSettings;
    }

    private static void AddDefault(
      string Name,
      bool Score,
      AnalyzerSettings.OffensiveStat Off,
      AnalyzerSettings.DefensiveStat Def)
    {
      AnalyzerSettings.mDefaultSettings.mConfiguration.Add(Name, new AnalyzerSettings.PartyMemberSettings()
      {
        Score = Score,
        OffensiveStat = Off,
        DefensiveStat = Def
      });
    }

    public enum ItemLevelConsideration
    {
      Current,
      CurrentRankMaxLevel,
      FullyMaxed,
    }

    public enum OffensiveStat
    {
      ATK,
      MAG,
      MND,
    }

    public enum DefensiveStat
    {
      DEF,
      RES,
    }

    public class PartyMemberSettings
    {
      public bool Score;
      public AnalyzerSettings.OffensiveStat OffensiveStat;
      public AnalyzerSettings.DefensiveStat DefensiveStat;

      public AnalyzerSettings.PartyMemberSettings Clone() => (AnalyzerSettings.PartyMemberSettings) this.MemberwiseClone();
    }
  }
}
