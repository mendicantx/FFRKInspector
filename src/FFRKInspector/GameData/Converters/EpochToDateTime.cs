// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.Converters.EpochToDateTime
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using Newtonsoft.Json;
using System;

namespace FFRKInspector.GameData.Converters
{
  internal class EpochToDateTime : JsonConverter
  {
    private static readonly DateTime mEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public override bool CanConvert(Type objectType) => objectType == typeof (DateTime);

    public override object ReadJson(
      JsonReader reader,
      Type objectType,
      object existingValue,
      JsonSerializer serializer)
    {
      ulong uint64 = Convert.ToUInt64(reader.Value);
      return (object) EpochToDateTime.mEpoch.AddSeconds((double) uint64);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
      TimeSpan timeSpan = ((DateTime) value).Subtract(EpochToDateTime.mEpoch);
      writer.WriteComment(timeSpan.TotalSeconds.ToString());
    }
  }
}
