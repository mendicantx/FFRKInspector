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
    private EventBattleInitiated mActiveBattle;
    private EventListBattles mActiveDungeon;
    private DataGachaSeriesList mGachas;
    private DataPartyDetails mParty;
    private AppInitData mAppInitData;

    public EventBattleInitiated ActiveBattle
    {
      get => this.mActiveBattle;
      set => this.mActiveBattle = value;
    }

    public EventListBattles ActiveDungeon
    {
      get => this.mActiveDungeon;
      set => this.mActiveDungeon = value;
    }

    public DataGachaSeriesList GachaSeries
    {
      get => this.mGachas;
      set => this.mGachas = value;
    }

    public DataPartyDetails PartyDetails
    {
      get => this.mParty;
      set => this.mParty = value;
    }

    public AppInitData AppInitData
    {
      get => this.mAppInitData;
      set => this.mAppInitData = value;
    }

    public GameState()
    {
      this.mActiveBattle = (EventBattleInitiated) null;
      this.mActiveDungeon = (EventListBattles) null;
      this.mGachas = (DataGachaSeriesList) null;
      this.mAppInitData = (AppInitData) null;
    }
  }
}
