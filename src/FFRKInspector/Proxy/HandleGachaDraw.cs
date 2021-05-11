// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Proxy.HandleGachaDraw
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.Database;
using FFRKInspector.GameData;
using Fiddler;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace FFRKInspector.Proxy
{
  internal class HandleGachaDraw : SimpleResponseHandler
  {
    public override bool CanHandle(Session Session) => ((ClientChatter) Session.oRequest).headers.RequestPath.StartsWith("/dff/gacha/execute") | ((ClientChatter) Session.oRequest).headers.RequestPath.StartsWith("/dff/payment/update");

    public override void Handle(Session Session)
    {
      DataGachaDraw dataGachaDraw = new DataGachaDraw();
      DataGachaDraw gachaDraw;
      if (((ClientChatter) Session.oRequest).headers.RequestPath.StartsWith("/dff/gacha/execute"))
      {
        gachaDraw = JsonConvert.DeserializeObject<DataGachaDraw>(Session.GetResponseBodyAsString());
      }
      else
      {
        DataGachaGemsDraw dataGachaGemsDraw = JsonConvert.DeserializeObject<DataGachaGemsDraw>(Session.GetResponseBodyAsString());
        gachaDraw = dataGachaGemsDraw.Draw;
        gachaDraw.ServerTime = (ulong) dataGachaGemsDraw.ServerTime;
      }
      List<DataGachaDrawItem> gachaDrawItems = new List<DataGachaDrawItem>();
      foreach (KeyValuePair<string, JToken> dropItem in gachaDraw.DropItems)
      {
        try
        {
          uint num = uint.Parse(dropItem.Key);
          if (dropItem.Value.Type == JTokenType.Object)
          {
            DataGachaDrawItem dataGachaDrawItem = JsonConvert.DeserializeObject<DataGachaDrawItem>(JsonConvert.SerializeObject((object) dropItem.Value));
            dataGachaDrawItem.ItemID = num;
            gachaDrawItems.Add(dataGachaDrawItem);
          }
        }
        catch
        {
          FiddlerApplication.Log.LogString("HandleGachaDraw Error");
        }
      }
      FFRKProxy.Instance.Database.BeginExecuteRequest((IDbRequest) new DbOpRecordGachaDraw(gachaDraw, gachaDrawItems));
    }
  }
}
