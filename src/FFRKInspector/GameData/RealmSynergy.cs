// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.RealmSynergy
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using System;
using System.Collections.Generic;

namespace FFRKInspector.GameData
{
  internal static class RealmSynergy
  {
    private static Dictionary<string, RealmSynergy.SynergyValue> mTextLookup = new Dictionary<string, RealmSynergy.SynergyValue>((IEqualityComparer<string>) StringComparer.CurrentCultureIgnoreCase);
    private static Dictionary<uint, RealmSynergy.SynergyValue> mSeriesLookup = new Dictionary<uint, RealmSynergy.SynergyValue>();
    private static Dictionary<RealmSynergy.Value, RealmSynergy.SynergyValue> mRealmLookup = new Dictionary<RealmSynergy.Value, RealmSynergy.SynergyValue>();

    public static IEnumerable<RealmSynergy.SynergyValue> Values => (IEnumerable<RealmSynergy.SynergyValue>) RealmSynergy.mRealmLookup.Values;

    static RealmSynergy()
    {
      RealmSynergy.SynergyValue[] synergyValueArray = new RealmSynergy.SynergyValue[21]
      {
        new RealmSynergy.SynergyValue("None", 1U, RealmSynergy.Value.None),
        new RealmSynergy.SynergyValue("None ", 2U, RealmSynergy.Value.None_2),
        new RealmSynergy.SynergyValue("Core", 200001U, RealmSynergy.Value.Core),
        new RealmSynergy.SynergyValue("I", 101001U, RealmSynergy.Value.FF1),
        new RealmSynergy.SynergyValue("II", 102001U, RealmSynergy.Value.FF2),
        new RealmSynergy.SynergyValue("III", 103001U, RealmSynergy.Value.FF3),
        new RealmSynergy.SynergyValue("IV", 104001U, RealmSynergy.Value.FF4),
        new RealmSynergy.SynergyValue("V", 105001U, RealmSynergy.Value.FF5),
        new RealmSynergy.SynergyValue("VI", 106001U, RealmSynergy.Value.FF6),
        new RealmSynergy.SynergyValue("VII", 107001U, RealmSynergy.Value.FF7),
        new RealmSynergy.SynergyValue("VIII", 108001U, RealmSynergy.Value.FF8),
        new RealmSynergy.SynergyValue("IX", 109001U, RealmSynergy.Value.FF9),
        new RealmSynergy.SynergyValue("X", 110001U, RealmSynergy.Value.FF10),
        new RealmSynergy.SynergyValue("XI", 111001U, RealmSynergy.Value.FF11),
        new RealmSynergy.SynergyValue("XII", 112001U, RealmSynergy.Value.FF12),
        new RealmSynergy.SynergyValue("XIII", 113001U, RealmSynergy.Value.FF13),
        new RealmSynergy.SynergyValue("XIV", 114001U, RealmSynergy.Value.FF14),
        new RealmSynergy.SynergyValue("XV", 115001U, RealmSynergy.Value.FF15),
        new RealmSynergy.SynergyValue("FFT", 150001U, RealmSynergy.Value.FFT),
        new RealmSynergy.SynergyValue("Type-0", 160001U, RealmSynergy.Value.Type_0),
        new RealmSynergy.SynergyValue("Beyond", 190001U, RealmSynergy.Value.Beyond)
      };
      foreach (RealmSynergy.SynergyValue synergyValue in synergyValueArray)
      {
        RealmSynergy.mTextLookup.Add(synergyValue.Text, synergyValue);
        RealmSynergy.mSeriesLookup.Add(synergyValue.GameSeries, synergyValue);
        RealmSynergy.mRealmLookup.Add(synergyValue.Realm, synergyValue);
      }
    }

    public static RealmSynergy.SynergyValue FromName(string Name)
    {
      RealmSynergy.SynergyValue synergyValue;
      if (RealmSynergy.mTextLookup.TryGetValue(Name, out synergyValue))
        return synergyValue;
      RealmSynergy.Value result;
      if (Enum.TryParse<RealmSynergy.Value>(Name, true, out result))
        return RealmSynergy.FromRealm(result);
      throw new KeyNotFoundException();
    }

    public static RealmSynergy.SynergyValue FromSeries(uint Series) => RealmSynergy.mSeriesLookup[Series];

    public static RealmSynergy.SynergyValue FromRealm(RealmSynergy.Value Realm) => RealmSynergy.mRealmLookup[Realm];

    public enum Value
    {
      None = -1, // 0xFFFFFFFF
      Core = 0,
      FF1 = 1,
      FF2 = 2,
      FF3 = 3,
      FF4 = 4,
      FF5 = 5,
      FF6 = 6,
      FF7 = 7,
      FF8 = 8,
      FF9 = 9,
      FF10 = 10, // 0x0000000A
      FF11 = 11, // 0x0000000B
      FF12 = 12, // 0x0000000C
      FF13 = 13, // 0x0000000D
      FF14 = 14, // 0x0000000E
      FF15 = 15, // 0x0000000F
      FFT = 16, // 0x00000010
      Type_0 = 17, // 0x00000011
      Beyond = 18, // 0x00000012
      None_2 = 19, // 0x00000013
    }

    public class SynergyValue
    {
      private string mText;
      private uint mGameSeries;
      private RealmSynergy.Value mRealmValue;

      public string Text => this.mText;

      public uint GameSeries => this.mGameSeries;

      public RealmSynergy.Value Realm => this.mRealmValue;

      public SynergyValue(string Text, uint Series, RealmSynergy.Value Realm)
      {
        this.mText = Text;
        this.mGameSeries = Series;
        this.mRealmValue = Realm;
      }

      public override string ToString() => this.mText;
    }
  }
}
