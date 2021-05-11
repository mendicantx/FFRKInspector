// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Proxy.SimpleResponseHandler
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using Fiddler;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FFRKInspector.Proxy
{
  public abstract class SimpleResponseHandler : IResponseHandler
  {
    public abstract bool CanHandle(Session Session);

    public abstract void Handle(Session Session);

    public virtual string GetResponseBody(Session session) => session.GetResponseBodyAsString();

    public JObject CreateJsonObject(Session Session) => JsonConvert.DeserializeObject<JObject>(this.GetResponseBody(Session));
  }
}
