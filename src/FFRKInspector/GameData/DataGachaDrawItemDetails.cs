// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.DataGachaDrawItemDetails
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace FFRKInspector.GameData
{
  internal class DataGachaDrawItemDetails
  {
    [JsonProperty("name")]
    public string Name;

    public bool isJapanese => new Regex("[　-〿]|[\u3040-ゟ]|[゠-ヿ]|[\uFF00-\uFFEF]|[一-龯]|[★-☆]|[←-↕]|※").Match(this.Name).Success;
  }
}
