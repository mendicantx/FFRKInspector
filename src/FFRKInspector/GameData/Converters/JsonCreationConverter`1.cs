// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.Converters.JsonCreationConverter`1
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using Fiddler;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace FFRKInspector.GameData.Converters
{
  internal abstract class JsonCreationConverter<T> : JsonConverter
  {
    protected JsonCreationConverter() => FiddlerApplication.Log.LogString("JsonCreationConverter Error");

    protected abstract T Create(Type ObjectType, JObject Object);

    public override bool CanConvert(Type ObjectType) => typeof (T).IsAssignableFrom(ObjectType);

    public override object ReadJson(
      JsonReader reader,
      Type objectType,
      object existingValue,
      JsonSerializer serializer)
    {
      JObject Object = JObject.Load(reader);
      T obj = this.Create(objectType, Object);
      serializer.Populate(Object.CreateReader(), (object) obj);
      return (object) obj;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => throw new NotImplementedException();
  }
}
