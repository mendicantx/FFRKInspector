// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.BasicEnemyParentInfo
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace FFRKInspector.GameData
{
  public class BasicEnemyParentInfo
  {
    public uint EnemyId;
    public ulong AiId;
    public uint ChildPosId;
    public uint Id;
    public ulong ParentAiId;
    public string EnemyName;
    public uint EnemyInitHp;
    public uint EnemyMaxHp;
    public uint Level;
    public List<DataAIArgs> AiArgs;
    public List<DataEnemyConstraints> Constraints;
    public List<DataEnemyParam> Params;
    public uint[] Appearances;

    public string BackupImgId => this.Params.Count == 0 ? "" : this.Params[0].AnimationInfo.Path;

    public IEnumerable<BasicEnemyInfo> Phases
    {
      get
      {
        List<BasicEnemyInfo> basicEnemyInfoList = new List<BasicEnemyInfo>();
        foreach (DataEnemyParam dataEnemyParam in this.Params)
        {
          string str1 = "100%";
          string str2 = "100%";
          string str3 = "100%";
          string str4 = "100%";
          string str5 = "100%";
          string str6 = "100%";
          string str7 = "100%";
          string str8 = "100%";
          string str9 = "100%";
          string str10 = "100%";
          string str11 = "100%";
          string str12 = "100%";
          string str13 = "100%";
          string str14 = "100%";
          string str15 = "100%";
          List<string> stringList = new List<string>();
          foreach (DataDefAttributes defAttribute in dataEnemyParam.DefAttributes)
          {
            if (defAttribute.Id < 200U)
            {
              Type enumType = typeof (SchemaConstants.ElementVulnerability);
              string name = Enum.GetName(enumType, (object) defAttribute.Factor);
              string description = ((DescriptionAttribute) enumType.GetMember(name)[0].GetCustomAttributes(typeof (DescriptionAttribute), false)[0]).Description;
              switch (defAttribute.Id)
              {
                case 100:
                  str1 = description;
                  break;
                case 101:
                  str2 = description;
                  break;
                case 102:
                  str3 = description;
                  break;
                case 103:
                  str4 = description;
                  break;
                case 104:
                  str5 = description;
                  break;
                case 105:
                  str6 = description;
                  break;
                case 106:
                  str7 = description;
                  break;
                case 107:
                  str8 = description;
                  break;
                case 108:
                  str9 = description;
                  break;
              }
            }
            else if (defAttribute.Id >= 200U && (defAttribute.Id < 216U || defAttribute.Id == 242U) && defAttribute.Factor == 1U)
              stringList.Add(Enum.GetName(typeof (SchemaConstants.StatusID), (object) defAttribute.Id));
            else if (defAttribute.Id >= 401U && defAttribute.Id <= 406U)
            {
              Type enumType = typeof (SchemaConstants.BreakEffectiveness);
              string name = Enum.GetName(enumType, (object) defAttribute.Factor);
              string description = ((DescriptionAttribute) enumType.GetMember(name)[0].GetCustomAttributes(typeof (DescriptionAttribute), false)[0]).Description;
              switch (defAttribute.Id)
              {
                case 401:
                  str10 = description;
                  break;
                case 402:
                  str11 = description;
                  break;
                case 403:
                  str12 = description;
                  break;
                case 404:
                  str13 = description;
                  break;
                case 405:
                  str14 = description;
                  break;
                case 406:
                  str15 = description;
                  break;
              }
            }
          }
          basicEnemyInfoList.Add(new BasicEnemyInfo()
          {
            EnemyId = dataEnemyParam.Id,
            EnemyName = dataEnemyParam.Name,
            EnemyMaxHp = dataEnemyParam.MaxHp,
            EnemyLv = dataEnemyParam.Lv,
            EnemyFireDef = str1,
            EnemyIceDef = str2,
            EnemyLitDef = str3,
            EnemyEarthDef = str4,
            EnemyWindDef = str5,
            EnemyWaterDef = str6,
            EnemyHolyDef = str7,
            EnemyDarkDef = str8,
            EnemyBioDef = str9,
            EnemyAtk = dataEnemyParam.Atk,
            EnemyDef = dataEnemyParam.Def,
            EnemyMag = dataEnemyParam.Mag,
            EnemyRes = dataEnemyParam.Res,
            EnemyMnd = dataEnemyParam.Mnd,
            EnemySpd = dataEnemyParam.Spd,
            EnemyAcc = dataEnemyParam.Acc,
            EnemyEva = dataEnemyParam.Eva,
            EnemyCrit = dataEnemyParam.Crit,
            EnemyExp = dataEnemyParam.Exp,
            EnemyStatusImmunity = stringList,
            EnemyCastTime = dataEnemyParam.CastTime,
            EnemyBackupImgId = dataEnemyParam.AnimationInfo.Path,
            EnemyAtkBrkDef = str10,
            EnemyDefBrkDef = str11,
            EnemyMagBrkDef = str12,
            EnemyResBrkDef = str13,
            EnemyMndBrkDef = str14,
            EnemySpdBrkDef = str15,
            EnemyAbilities = dataEnemyParam.Abilities,
            EnemyCounters = dataEnemyParam.Counters,
            EnemyParentInfo = this
          });
        }
        return (IEnumerable<BasicEnemyInfo>) basicEnemyInfoList;
      }
    }

    public override bool Equals(object obj) => obj is BasicEnemyParentInfo basicEnemyParentInfo && ((int) basicEnemyParentInfo.EnemyId == (int) this.EnemyId && (int) basicEnemyParentInfo.Params[0].Id == (int) this.Params[0].Id);

    public override int GetHashCode() => (this.EnemyId ^ this.Params[0].Id).GetHashCode();

    public override string ToString()
    {
      string[] strArray = new string[this.Params.Count];
      int index1 = 0;
      foreach (DataEnemyParam dataEnemyParam in this.Params)
      {
        string name = dataEnemyParam.Name;
        bool flag = false;
        for (int index2 = 0; index2 < index1; ++index2)
        {
          if (strArray[index2] != null && strArray[index2].Equals(name))
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          strArray[index1] = name;
        ++index1;
      }
      string str = "";
      for (int index2 = 0; index2 < strArray.Length; ++index2)
      {
        if (strArray[index2] != null)
        {
          if (!str.Equals(""))
            str += "/";
          str += strArray[index2];
        }
      }
      return str;
    }
  }
}
