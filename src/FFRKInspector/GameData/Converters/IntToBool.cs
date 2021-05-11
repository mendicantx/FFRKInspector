// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.Converters.IntToBool
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using Newtonsoft.Json;
using System;

namespace FFRKInspector.GameData.Converters
{
  internal class IntToBool : JsonConverter
  {
    public override bool CanConvert(Type objectType) => objectType == typeof (bool);

    public override object ReadJson(
      JsonReader reader,
      Type objectType,
      object existingValue,
      JsonSerializer serializer)
    {
      return (object) (reader.Value.ToString() != "0");
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => writer.WriteValue((bool) value ? 1 : 0);
  }
}
