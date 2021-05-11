// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Proxy.HandlePartyList
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.Database;
using FFRKInspector.GameData;
using FFRKInspector.GameData.Party;
using Fiddler;
using Newtonsoft.Json;
using System;

namespace FFRKInspector.Proxy
{
  internal class HandlePartyList : SimpleResponseHandler
  {
    public override bool CanHandle(Session Session) => ((ClientChatter) Session.oRequest).headers.RequestPath.Equals("/dff/party/list", StringComparison.CurrentCultureIgnoreCase);

    public override void Handle(Session Session)
    {
      DataPartyDetails party = JsonConvert.DeserializeObject<DataPartyDetails>(this.GetResponseBody(Session));
      DbOpInsertItems dbOpInsertItems = new DbOpInsertItems();
      foreach (DataEquipmentInformation equipment in party.Equipments)
        dbOpInsertItems.Items.Add(new DbOpInsertItems.ItemRecord()
        {
          EquipCategory = new SchemaConstants.EquipmentCategory?(equipment.Category),
          Id = equipment.EquipmentId,
          Name = equipment.Name.TrimEnd(' ', '+', '＋'),
          Type = equipment.Type,
          Rarity = equipment.BaseRarity,
          Series = new uint?(equipment.SeriesId)
        });
      FFRKProxy.Instance.Database.BeginExecuteRequest((IDbRequest) dbOpInsertItems);
      FFRKProxy.Instance.GameState.PartyDetails = party;
      FFRKProxy.Instance.RaisePartyList(party);
    }
  }
}
