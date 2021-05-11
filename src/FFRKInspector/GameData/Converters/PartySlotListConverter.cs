// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.Converters.PartySlotListConverter
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace FFRKInspector.GameData.Converters
{
  internal class PartySlotListConverter : JsonCreationConverter<uint[]>
  {
    protected override uint[] Create(Type ObjectType, JObject Object)
    {
      uint[] numArray = new uint[5];
      foreach (KeyValuePair<string, JToken> keyValuePair in (JObject) Object.GetValue("slot_to_buddy_id"))
      {
        uint uint32_1 = Convert.ToUInt32(keyValuePair.Key);
        uint uint32_2 = Convert.ToUInt32(((JValue) keyValuePair.Value).Value);
        numArray[(int) (IntPtr) (long) (uint32_1 - 1U)] = uint32_2;
      }
      return numArray;
    }
  }
}
