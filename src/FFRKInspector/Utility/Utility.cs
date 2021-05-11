// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Utility.Utility
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using System.Collections.Generic;

namespace FFRKInspector.Utility
{
  internal static class Utility
  {
    private static Dictionary<string, uint> romans = new Dictionary<string, uint>()
    {
      {
        "I",
        1U
      },
      {
        "II",
        2U
      },
      {
        "III",
        3U
      },
      {
        "IV",
        4U
      },
      {
        "V",
        5U
      },
      {
        "VI",
        6U
      },
      {
        "VII",
        7U
      },
      {
        "VIII",
        8U
      },
      {
        "IX",
        9U
      },
      {
        "X",
        10U
      },
      {
        "XI",
        11U
      },
      {
        "XII",
        12U
      },
      {
        "XIII",
        13U
      },
      {
        "XIV",
        14U
      }
    };

    public static uint RomanNumeralToNumber(string Roman)
    {
      uint num = 0;
      FFRKInspector.Utility.Utility.romans.TryGetValue(Roman, out num);
      return num;
    }
  }
}
