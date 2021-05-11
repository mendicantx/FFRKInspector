// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Proxy.HandleGachaSeriesDetails
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.Database;
using FFRKInspector.GameData;
using FFRKInspector.Utility;
using Fiddler;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FFRKInspector.Proxy
{
  internal class HandleGachaSeriesDetails : SimpleResponseHandler
  {
    public override bool CanHandle(Session Session) => ((ClientChatter) Session.oRequest).headers.RequestPath.StartsWith("/dff/gacha/probability");

    public override void Handle(Session Session)
    {
      string responseBodyAsString = Session.GetResponseBodyAsString();
      string requestPath = ((ClientChatter) Session.oRequest).headers.RequestPath;
      JObject jobject = JsonConvert.DeserializeObject<JObject>(responseBodyAsString);
      ulong serverTime = JsonConvert.DeserializeObject<DataGachaProbabilityOverview>(responseBodyAsString).ServerTime;
      int num1 = requestPath.IndexOf('?');
      if (num1 == -1)
      {
        Log.LogFormat("Unrecognized gacha series details request path {0}.  Expected ?series_id=<n>", (object) requestPath);
      }
      else
      {
        Match match = Regex.Match(requestPath.Substring(num1 + 1), "series_id=([0-9]+)");
        if (!match.Success)
        {
          Log.LogFormat("Unrecognized gacha series details request path {0}.  Expected ?series_id=<n>", (object) requestPath);
        }
        else
        {
          uint series_id;
          if (!uint.TryParse(match.Groups[1].Value, out series_id))
          {
            Log.LogFormat("Unrecognized gacha series details request path {0}.  series_id does not appear to be an integer.", (object) requestPath);
          }
          else
          {
            DataGachaSeriesItemsForEntryPoints gacha = new DataGachaSeriesItemsForEntryPoints();
            foreach (KeyValuePair<string, JToken> keyValuePair in jobject)
            {
              try
              {
                uint num2 = uint.Parse(keyValuePair.Key);
                if (keyValuePair.Value.Type == JTokenType.Object)
                {
                  string str = JsonConvert.SerializeObject((object) keyValuePair.Value);
                  DataGachaSeriesItemsForEntryPoints.ItemsForEntryPoint itemsForEntryPoint = new DataGachaSeriesItemsForEntryPoints.ItemsForEntryPoint();
                  itemsForEntryPoint.ItemDetails = JsonConvert.DeserializeObject<DataGachaSeriesItemDetails>(str);
                  FFRKProxy.Instance.Database.BeginExecuteRequest((IDbRequest) new DbOpRecordGachaProbabilities(itemsForEntryPoint.ItemDetails, series_id, serverTime));
                  gacha.Gachas.Add(num2, itemsForEntryPoint);
                  List<DataGachaSeriesInfo> seriesList = FFRKProxy.Instance.GameState.GachaSeries.SeriesList;
                  if (seriesList != null)
                  {
                    DataGachaSeriesInfo series = seriesList.Find((Predicate<DataGachaSeriesInfo>) (x => (int) x.SeriesId == (int) series_id));
                    if (series != null)
                      itemsForEntryPoint.EntryPoint = this.FindEntryPointForSeries(series, num2);
                  }
                }
              }
              catch
              {
              }
            }
            FFRKProxy.Instance.RaiseGachaStats(gacha);
            FFRKProxy.Instance.RaiseGachaDetails();
          }
        }
      }
    }

    private DataGachaSeriesEntryPoint FindEntryPointForSeries(
      DataGachaSeriesInfo series,
      uint entry_point_id)
    {
      foreach (DataGachaSeriesBox box in series.Boxes)
      {
        foreach (DataGachaSeriesEntryPoint entryPoint in box.EntryPoints)
        {
          if ((int) entryPoint.EntryPointId == (int) entry_point_id)
            return entryPoint;
        }
      }
      return (DataGachaSeriesEntryPoint) null;
    }
  }
}
