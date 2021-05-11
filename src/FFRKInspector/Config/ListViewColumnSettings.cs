// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Config.ListViewColumnSettings
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.UI;
using Newtonsoft.Json;

namespace FFRKInspector.Config
{
  public class ListViewColumnSettings
  {
    [JsonProperty("width_style")]
    public FieldWidthStyle WidthStyle;
    [JsonProperty("width")]
    public int Width;
    [JsonProperty("visible")]
    public bool Visible;
  }
}
