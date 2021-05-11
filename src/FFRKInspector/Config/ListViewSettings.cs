// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Config.ListViewSettings
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.UI;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FFRKInspector.Config
{
  public class ListViewSettings
  {
    [JsonProperty("columns")]
    public Dictionary<string, ListViewColumnSettings> Columns;

    public ListViewSettings() => this.Columns = new Dictionary<string, ListViewColumnSettings>();

    public ListViewColumnSettings GetColumnSettings(
      ColumnHeader Header,
      FieldWidthStyle DefaultStyle,
      int DefaultWidth)
    {
      ListViewColumnSettings viewColumnSettings1;
      if (this.Columns.TryGetValue(Header.Name, out viewColumnSettings1))
        return viewColumnSettings1;
      ListViewColumnSettings viewColumnSettings2 = new ListViewColumnSettings();
      viewColumnSettings2.WidthStyle = DefaultStyle;
      viewColumnSettings2.Width = DefaultWidth;
      viewColumnSettings2.Visible = true;
      this.Columns.Add(Header.Name, viewColumnSettings2);
      return viewColumnSettings2;
    }
  }
}
