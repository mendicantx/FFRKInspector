// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Proxy.ResponseHistory
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using Fiddler;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace FFRKInspector.Proxy
{
  internal class ResponseHistory
  {
    private List<ResponseHistory.HistoryItem> mResponseHistory;

    public ResponseHistory.HistoryItem this[int Index] => this.mResponseHistory[Index];

    public int Size => this.mResponseHistory.Count;

    public ResponseHistory() => this.mResponseHistory = new List<ResponseHistory.HistoryItem>();

    public void AddItem(Session Session, IResponseHandler Handler) => this.mResponseHistory.Add(new ResponseHistory.HistoryItem()
    {
      Timestamp = DateTime.Now,
      Session = Session,
      Handler = Handler
    });

    public void Clear() => this.mResponseHistory.Clear();

    public class HistoryItem
    {
      public DateTime Timestamp;
      public Session Session;
      public JObject JsonObject;
      public IResponseHandler Handler;
    }
  }
}
