// Decompiled with JetBrains decompiler
// Type: FFRKInspector.UI.DatabaseUI.FFRKDataBoundPanel
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

namespace FFRKInspector.UI.DatabaseUI
{
  internal interface FFRKDataBoundPanel
  {
    void InitializeConnection();

    void Reload();

    void Commit();
  }
}
