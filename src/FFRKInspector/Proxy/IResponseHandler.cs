// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Proxy.IResponseHandler
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using Fiddler;
using Newtonsoft.Json.Linq;

namespace FFRKInspector.Proxy
{
  internal interface IResponseHandler
  {
    bool CanHandle(Session Session);

    string GetResponseBody(Session Session);

    JObject CreateJsonObject(Session session);

    void Handle(Session Session);
  }
}
