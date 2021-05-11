// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Utility.Log
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using Fiddler;

namespace FFRKInspector.Utility
{
  internal static class Log
  {
    public static void LogString(string s) => FiddlerApplication.Log.LogString("FFRKInspector: " + s);

    public static void LogFormat(string s, params object[] args) => FiddlerApplication.Log.LogFormat("FFRKInspector: " + s, args);
  }
}
