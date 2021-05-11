// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.Converters.semiNumericComparer
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using System;
using System.Collections.Generic;

namespace FFRKInspector.GameData.Converters
{
  internal class semiNumericComparer : IComparer<string>
  {
    public int Compare(string s1, string s2)
    {
      if (semiNumericComparer.IsNumeric((object) s1) && semiNumericComparer.IsNumeric((object) s2))
      {
        if (Convert.ToInt32(s1) > Convert.ToInt32(s2))
          return 1;
        if (Convert.ToInt32(s1) < Convert.ToInt32(s2))
          return -1;
        if (Convert.ToInt32(s1) == Convert.ToInt32(s2))
          return 0;
      }
      if (semiNumericComparer.IsNumeric((object) s1) && !semiNumericComparer.IsNumeric((object) s2))
        return -1;
      return !semiNumericComparer.IsNumeric((object) s1) && semiNumericComparer.IsNumeric((object) s2) ? 1 : string.Compare(s1, s2, true);
    }

    public static bool IsNumeric(object value) => int.TryParse(value.ToString(), out int _);
  }
}
