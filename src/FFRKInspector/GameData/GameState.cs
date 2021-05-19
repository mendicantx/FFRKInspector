// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.GameState
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.GameData.AppInit;
using FFRKInspector.GameData.Party;

namespace FFRKInspector.GameData
{
  internal class GameState
  {
        public EventBattleInitiated ActiveBattle { get; set; }
        public EventListBattles ActiveDungeon { get; set; }
        public DataGachaSeriesList GachaSeries { get; set; }
        public DataPartyDetails PartyDetails { get; set; }
        public AppInitData AppInitData { get; set; }
        public LabrynthSessionData LabrynthSessionData { get; set; }


  }
}
