// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Proxy.HandleLeaveDungeon
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using Fiddler;

namespace FFRKInspector.Proxy
{
  internal class HandleLeaveDungeon : SimpleResponseHandler
  {
    public override bool CanHandle(Session Session) => ((ClientChatter) Session.oRequest).headers.RequestPath.EndsWith("/leave_dungeon");

    public override void Handle(Session Session) => FFRKProxy.Instance.RaiseLeaveDungeon();
  }
}
