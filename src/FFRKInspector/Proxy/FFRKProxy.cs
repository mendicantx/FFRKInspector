// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Proxy.FFRKProxy
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.Config;
using FFRKInspector.Database;
using FFRKInspector.DataCache;
using FFRKInspector.GameData;
using FFRKInspector.GameData.Party;
using FFRKInspector.UI;
using FFRKInspector.Utility;
using Fiddler;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FFRKInspector.Proxy
{
  public class FFRKProxy : IAutoTamper, IFiddlerExtension, IHandleExecAction
  {
    private static readonly uint mRequiredSchema = 19;
    private TabPage mTabPage;
    private FFRKTabInspector mInspectorView;
    private ResponseHistory mHistory;
    private FFRKMySqlInstance mDatabaseInstance;
    private GameState mGameState;
    private List<IResponseHandler> mResponseHandlers;
    private FFRKDataCache mCache;
    private AppSettings mSettings;
    private static FFRKProxy mInstance;

    public static FFRKProxy Instance => FFRKProxy.mInstance;

    internal ResponseHistory ResponseHistory => this.mHistory;

    internal GameState GameState => this.mGameState;

    internal FFRKMySqlInstance Database => this.mDatabaseInstance;

    internal FFRKDataCache Cache => this.mCache;

    internal uint MinimumRequiredSchema => FFRKProxy.mRequiredSchema;

    public AppSettings AppSettings => this.mSettings;

    private string SettingsFile => Path.Combine(Path.GetDirectoryName(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath), "ffrk_inspector_settings.config");

    internal event FFRKProxy.BattleInitiatedDelegate OnBattleEngaged;

    internal event FFRKProxy.ListBattlesDelegate OnListBattles;

    internal event FFRKProxy.ListDungeonsDelegate OnListDungeons;

    internal event FFRKProxy.FFRKDefaultDelegate OnLeaveDungeon;

    internal event FFRKProxy.BattleResultDelegate OnCompleteBattle;

    internal event FFRKProxy.GachaStatsDelegate OnGachaStats;

    internal event FFRKProxy.FFRKResponseDelegate OnFFRKResponse;

    internal event FFRKProxy.FFRKDefaultDelegate OnItemCacheRefreshed;

    internal event FFRKProxy.FFRKPartyListDelegate OnPartyList;

    internal event FFRKProxy.GachaDetailsDelegate OnGachaDetails;

    internal event FFRKProxy.GachaSeriesListDelegate OnGachaSeriesList;

    public void OnLoad()
    {
      this.LoadAppSettings();
      FFRKProxy.mInstance = this;
      this.mResponseHandlers = new List<IResponseHandler>();
      this.mResponseHandlers.Add((IResponseHandler) new HandleAppInitData());
      this.mResponseHandlers.Add((IResponseHandler) new HandlePartyList());
      this.mResponseHandlers.Add((IResponseHandler) new HandleListBattles());
      this.mResponseHandlers.Add((IResponseHandler) new HandleListDungeons());
      this.mResponseHandlers.Add((IResponseHandler) new HandleLeaveDungeon());
      this.mResponseHandlers.Add((IResponseHandler) new HandleInitiateBattle());
      this.mResponseHandlers.Add((IResponseHandler) new HandleGachaSeriesList());
      this.mResponseHandlers.Add((IResponseHandler) new HandleGachaSeriesDetails());
      this.mResponseHandlers.Add((IResponseHandler) new HandleCompleteBattle());
      this.mResponseHandlers.Add((IResponseHandler) new HandleGachaDraw());
      this.mHistory = new ResponseHistory();
      this.mGameState = new GameState();
      this.mDatabaseInstance = new FFRKMySqlInstance();
      this.mCache = new FFRKDataCache();
      this.mTabPage = new TabPage("FFRK Inspector");
      this.mInspectorView = new FFRKTabInspector();
      this.mInspectorView.Dock = DockStyle.Fill;
      this.mTabPage.Controls.Add((Control) this.mInspectorView);
      ((TabControl) FiddlerApplication.UI.tabsViews).TabPages.Add(this.mTabPage);
      this.mDatabaseInstance.OnConnectionInitialized += new FFRKMySqlInstance.ConnectionInitializedDelegate(this.mDatabaseInstance_OnConnectionInitialized);
      this.mDatabaseInstance.OnSchemaError += new FFRKMySqlInstance.ConnectionInitializedDelegate(this.mDatabaseInstance_OnSchemaError);
      this.mDatabaseInstance.InitializeConnection(this.MinimumRequiredSchema);
    }

    public void OnBeforeUnload()
    {
      FFRKProxy.mInstance = (FFRKProxy) null;
      this.SaveAppSettings();
    }

    private void LoadAppSettings()
    {
      try
      {
        using (FileStream fileStream = File.Open(this.SettingsFile, FileMode.Open))
        {
          using (StreamReader streamReader = new StreamReader((Stream) fileStream))
          {
            using (JsonReader reader = (JsonReader) new JsonTextReader((TextReader) streamReader))
              this.mSettings = (AppSettings) new JsonSerializer().Deserialize(reader, typeof (AppSettings));
          }
        }
      }
      catch (Exception ex)
      {
        this.mSettings = new AppSettings();
      }
    }

    private void SaveAppSettings()
    {
      using (FileStream fileStream = File.Open(Path.Combine(Path.GetDirectoryName(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath), "ffrk_inspector_settings.config"), FileMode.Create))
      {
        using (StreamWriter streamWriter = new StreamWriter((Stream) fileStream))
        {
          using (JsonWriter jsonWriter = (JsonWriter) new JsonTextWriter((TextWriter) streamWriter))
          {
            jsonWriter.Formatting = Formatting.Indented;
            new JsonSerializer().Serialize(jsonWriter, (object) this.mSettings);
          }
        }
      }
    }

    private void mDatabaseInstance_OnSchemaError(FFRKMySqlInstance.ConnectResult ConnectResult)
    {
      int num;
      this.mTabPage.BeginInvoke((Action) (() => num = (int) MessageBox.Show("FFRK Inspector has been updated.  Please download the latest release from https://github.com/Spirialis/FFRKInspector.  Database connectivity will be unavailable for the remainder of this session.")));
    }

    private void mDatabaseInstance_OnConnectionInitialized(FFRKMySqlInstance.ConnectResult Result)
    {
      switch (Result)
      {
        case FFRKMySqlInstance.ConnectResult.Success:
          this.PopulateDataCache();
          break;
        case FFRKMySqlInstance.ConnectResult.SchemaTooOld:
          int num1;
          this.mTabPage.BeginInvoke((Action) (() => num1 = (int) MessageBox.Show("The database you are connected to is for an older version of FFRK Inspector.  Please point to a newer database instance.  Database connectivity will not be available for this session.", "Database version mismatch")));
          break;
        case FFRKMySqlInstance.ConnectResult.SchemaTooNew:
          int num2;
          this.mTabPage.BeginInvoke((Action) (() => num2 = (int) MessageBox.Show("FFRK Inspector is outdated and needs to be updated.  Please update to the latest version.  Database connectivity will not be available for this session.", "Database version mismatch")));
          break;
      }
    }

    private void PopulateDataCache()
    {
      DbOpLoadAllItems dbOpLoadAllItems = new DbOpLoadAllItems();
      dbOpLoadAllItems.OnRequestComplete += new DbOpLoadAllItems.DataReadyCallback(this.DbOpLoadAllItems_OnRequestComplete);
      this.mDatabaseInstance.BeginExecuteRequest((IDbRequest) dbOpLoadAllItems);
      DbOpLoadAllBattles opLoadAllBattles = new DbOpLoadAllBattles();
      opLoadAllBattles.OnRequestComplete += new DbOpLoadAllBattles.DataReadyCallback(this.DbOpLoadAllBattles_OnRequestComplete);
      this.mDatabaseInstance.BeginExecuteRequest((IDbRequest) opLoadAllBattles);
      DbOpLoadAllDungeons opLoadAllDungeons = new DbOpLoadAllDungeons();
      opLoadAllDungeons.OnRequestComplete += new DbOpLoadAllDungeons.DataReadyCallback(this.DbOpLoadAllDungeons_OnRequestComplete);
      this.mDatabaseInstance.BeginExecuteRequest((IDbRequest) opLoadAllDungeons);
      DbOpLoadAllWorlds dbOpLoadAllWorlds = new DbOpLoadAllWorlds();
      dbOpLoadAllWorlds.OnRequestComplete += new DbOpLoadAllWorlds.DataReadyCallback(this.DbOpLoadAllWorlds_OnRequestComplete);
      this.mDatabaseInstance.BeginExecuteRequest((IDbRequest) dbOpLoadAllWorlds);
      DbOpLoadAllBanners opLoadAllBanners = new DbOpLoadAllBanners();
      opLoadAllBanners.OnRequestComplete += new DbOpLoadAllBanners.DataReadyCallback(this.DbOpLoadAllBanners_OnRequestComplete);
      this.mDatabaseInstance.BeginExecuteRequest((IDbRequest) opLoadAllBanners);
    }

    private void DbOpLoadAllWorlds_OnRequestComplete(FFRKDataCacheTable<FFRKInspector.DataCache.Worlds.Key, FFRKInspector.DataCache.Worlds.Data> worlds)
    {
      lock (this.mCache.SyncRoot)
        this.mCache.Worlds = worlds;
      if (this.OnItemCacheRefreshed == null)
        return;
      this.OnItemCacheRefreshed();
    }

    private void DbOpLoadAllDungeons_OnRequestComplete(FFRKDataCacheTable<FFRKInspector.DataCache.Dungeons.Key, FFRKInspector.DataCache.Dungeons.Data> dungeons)
    {
      lock (this.mCache.SyncRoot)
        this.mCache.Dungeons = dungeons;
      if (this.OnItemCacheRefreshed == null)
        return;
      this.OnItemCacheRefreshed();
    }

    private void DbOpLoadAllBattles_OnRequestComplete(FFRKDataCacheTable<FFRKInspector.DataCache.Battles.Key, FFRKInspector.DataCache.Battles.Data> battles)
    {
      lock (this.mCache.SyncRoot)
        this.mCache.Battles = battles;
      if (this.OnItemCacheRefreshed == null)
        return;
      this.OnItemCacheRefreshed();
    }

    private void DbOpLoadAllItems_OnRequestComplete(FFRKDataCacheTable<FFRKInspector.DataCache.Items.Key, FFRKInspector.DataCache.Items.Data> items)
    {
      lock (this.mCache.SyncRoot)
        this.mCache.Items = items;
      if (this.OnItemCacheRefreshed == null)
        return;
      this.OnItemCacheRefreshed();
    }

    private void DbOpLoadAllBanners_OnRequestComplete(FFRKDataCacheTable<FFRKInspector.DataCache.Banners.Key, FFRKInspector.DataCache.Banners.Data> banners)
    {
      lock (this.mCache.SyncRoot)
        this.mCache.Banners = banners;
      if (this.OnItemCacheRefreshed == null)
        return;
      this.OnItemCacheRefreshed();
    }

    public void AutoTamperRequestBefore(Session oSession)
    {
    }

    public void AutoTamperRequestAfter(Session oSession)
    {
    }

    public void AutoTamperResponseBefore(Session oSession)
    {
    }

    public void AutoTamperResponseAfter(Session oSession)
    {
      if (!((ClientChatter) oSession.oRequest).host.Equals("ffrk.denagames.com", StringComparison.CurrentCultureIgnoreCase) && !((ClientChatter) oSession.oRequest).host.Equals("dff.sp.mbga.jp", StringComparison.CurrentCultureIgnoreCase))
        return;
      string requestPath = ((ClientChatter) oSession.oRequest).headers.RequestPath;
      Log.LogFormat(requestPath);
      IResponseHandler Handler = this.mResponseHandlers.FirstOrDefault<IResponseHandler>((Func<IResponseHandler, bool>) (x => x.CanHandle(oSession)));
      this.mHistory.AddItem(oSession, Handler);
      if (this.OnFFRKResponse != null)
        this.OnFFRKResponse(requestPath);
      if (Handler == null)
        return;
      try
      {
        Handler.Handle(oSession);
      }
      catch (Exception ex)
      {
        Log.LogFormat("An error occurred processing the response from {0}.  {1}", (object) requestPath, (object) ex.Message);
      }
    }

    public void OnBeforeReturningError(Session oSession)
    {
    }

    public bool OnExecAction(string sCommand) => throw new NotImplementedException();

    internal void RaiseGachaStats(DataGachaSeriesItemsForEntryPoints gacha)
    {
      if (this.OnGachaStats == null)
        return;
      this.OnGachaStats(gacha);
    }

    internal void RaiseListBattles(EventListBattles battles)
    {
      if (this.OnListBattles == null)
        return;
      this.OnListBattles(battles);
    }

    internal void RaiseListDungeons(EventListDungeons dungeons)
    {
      if (this.OnListDungeons == null)
        return;
      this.OnListDungeons(dungeons);
    }

    internal void RaiseBattleInitiated(EventBattleInitiated battle)
    {
      if (this.OnBattleEngaged == null)
        return;
      this.OnBattleEngaged(battle);
    }

    internal void RaiseLeaveDungeon()
    {
      if (this.OnLeaveDungeon == null)
        return;
      this.OnLeaveDungeon();
    }

    internal void RaiseBattleComplete(EventBattleInitiated original_battle)
    {
      if (this.OnCompleteBattle == null)
        return;
      this.OnCompleteBattle(original_battle);
    }

    internal void RaisePartyList(DataPartyDetails party)
    {
      if (this.OnPartyList == null)
        return;
      this.OnPartyList(party);
    }

    internal void RaiseGachaDetails()
    {
      if (this.OnGachaDetails == null)
        return;
      this.OnGachaDetails();
    }

    internal void RaiseGachaSeriesList()
    {
      if (this.OnGachaSeriesList == null)
        return;
      this.OnGachaSeriesList();
    }

    internal delegate void BattleInitiatedDelegate(EventBattleInitiated battle);

    internal delegate void BattleResultDelegate(EventBattleInitiated battle);

    internal delegate void ListBattlesDelegate(EventListBattles battles);

    internal delegate void ListDungeonsDelegate(EventListDungeons dungeons);

    internal delegate void GachaStatsDelegate(DataGachaSeriesItemsForEntryPoints gacha);

    internal delegate void FFRKDefaultDelegate();

    internal delegate void FFRKResponseDelegate(string Path);

    internal delegate void FFRKPartyListDelegate(DataPartyDetails party);

    internal delegate void GachaDetailsDelegate();

    internal delegate void GachaSeriesListDelegate();
  }
}
