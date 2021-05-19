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
    private GameState mGameState;
    private List<IResponseHandler> mResponseHandlers;
    private FFRKDataCache mCache;
    private AppSettings mSettings;
    private static FFRKProxy mInstance;

    public static FFRKProxy Instance => FFRKProxy.mInstance;

    internal ResponseHistory ResponseHistory => this.mHistory;

    internal GameState GameState => this.mGameState;


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

    internal event LabrynthSessionUpdatedDelegate OnLabrynthSessionUpdated;

    public void OnLoad()
    {
      this.LoadAppSettings();
      FFRKProxy.mInstance = this;
      this.mResponseHandlers = new List<IResponseHandler>();
      this.mResponseHandlers.Add(new HandleAppInitData());
      this.mResponseHandlers.Add(new HandlePartyList());
      this.mResponseHandlers.Add(new HandleListBattles());
      this.mResponseHandlers.Add(new HandleListDungeons());
      this.mResponseHandlers.Add(new HandleLeaveDungeon());
      this.mResponseHandlers.Add(new HandleInitiateBattle());
      this.mResponseHandlers.Add(new HandleGachaSeriesList());
      this.mResponseHandlers.Add(new HandleGachaSeriesDetails());
      this.mResponseHandlers.Add(new HandleCompleteBattle());
      this.mResponseHandlers.Add(new HandleGachaDraw());
      this.mResponseHandlers.Add(new HandleLabrynthSession());
      this.mHistory = new ResponseHistory();
      this.mGameState = new GameState();
      this.mCache = new FFRKDataCache();
      this.mTabPage = new TabPage("FFRK Inspector");
      this.mInspectorView = new FFRKTabInspector();
      this.mInspectorView.Dock = DockStyle.Fill;
      this.mTabPage.Controls.Add((Control) this.mInspectorView);
      ((TabControl) FiddlerApplication.UI.tabsViews).TabPages.Add(this.mTabPage);
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

    internal void RaiseLabrynthSessionUpdated(LabrynthSessionData labrynthSession)
    {
        if (OnLabrynthSessionUpdated == null)
            return;
        OnLabrynthSessionUpdated(labrynthSession);
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

    internal delegate void LabrynthSessionUpdatedDelegate(LabrynthSessionData labrynthSession);

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
