// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.Converters.EquipUsageListConverter
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FFRKInspector.GameData.Converters
{
  internal class EquipUsageListConverter : CustomCreationConverter<List<DataBuddyEquipUsage>>
  {
    public override List<DataBuddyEquipUsage> Create(Type objectType) => new List<DataBuddyEquipUsage>();

    public override object ReadJson(
      JsonReader reader,
      Type objectType,
      object existingValue,
      JsonSerializer serializer)
    {
      JObject jobject = JObject.Load(reader);
      List<DataBuddyEquipUsage> dataBuddyEquipUsageList = this.Create(objectType);
      foreach (JToken jtoken in (IEnumerable<JToken>) jobject.Children().ToList<JToken>())
      {
        DataBuddyEquipUsage dataBuddyEquipUsage = JsonConvert.DeserializeObject<DataBuddyEquipUsage>(jtoken.First.ToString());
        dataBuddyEquipUsageList.Add(dataBuddyEquipUsage);
      }
      return (object) dataBuddyEquipUsageList;
    }
  }
}
