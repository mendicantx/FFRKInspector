// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.BasicEnemyInfo
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using System.Collections.Generic;

namespace FFRKInspector.GameData
{
  internal class BasicEnemyInfo
  {
    public uint EnemyId;
    public string EnemyName;
    public uint EnemyMaxHp;
    public uint EnemyLv;
    public string EnemyFireDef;
    public string EnemyIceDef;
    public string EnemyLitDef;
    public string EnemyEarthDef;
    public string EnemyWindDef;
    public string EnemyWaterDef;
    public string EnemyHolyDef;
    public string EnemyDarkDef;
    public string EnemyBioDef;
    public string EnemyAtkBrkDef;
    public string EnemyDefBrkDef;
    public string EnemyMagBrkDef;
    public string EnemyResBrkDef;
    public string EnemyMndBrkDef;
    public string EnemySpdBrkDef;
    public uint EnemyAtk;
    public uint EnemyDef;
    public uint EnemyMag;
    public uint EnemyRes;
    public uint EnemyMnd;
    public uint EnemySpd;
    public uint EnemyAcc;
    public uint EnemyEva;
    public uint EnemyCrit;
    public uint EnemyExp;
    public string EnemyCastTime;
    public string EnemyBackupImgId;
    public List<string> EnemyStatusImmunity;
    public DataEnemy EnemyParent;
    public BasicEnemyParentInfo EnemyParentInfo;
    public List<DataEnemyParamAbilities> EnemyAbilities;
    public List<DataEnemyParamCounters> EnemyCounters;

    public DataEnemyParamAbilities getAbilityByTag(string tag)
    {
      foreach (DataEnemyParamAbilities enemyAbility in this.EnemyAbilities)
      {
        if (enemyAbility.Tag.Equals(tag))
          return enemyAbility;
      }
      foreach (DataEnemyParam dataEnemyParam in this.EnemyParentInfo.Params)
      {
        foreach (DataEnemyParamAbilities ability in dataEnemyParam.Abilities)
        {
          if (ability.Tag.Equals(tag))
            return ability;
        }
      }
      return (DataEnemyParamAbilities) null;
    }

    public override bool Equals(object obj) => obj is BasicEnemyInfo basicEnemyInfo && (int) basicEnemyInfo.EnemyId == (int) this.EnemyId;

    public override int GetHashCode() => this.EnemyId.GetHashCode();

    public override string ToString() => this.EnemyName + " (" + (object) this.EnemyId + ")";

    public List<DataEnemyParamAbilities> getAbilities(
      List<DataEnemyConstraints> myList)
    {
      List<DataEnemyParamAbilities> enemyParamAbilitiesList = new List<DataEnemyParamAbilities>();
      foreach (DataEnemyParamAbilities enemyAbility in this.EnemyAbilities)
        enemyParamAbilitiesList.Add(enemyAbility);
      foreach (DataEnemyConstraints my in myList)
      {
        if (my.EnemyStatusId <= 0U && my.ConstraintType <= 1100U)
        {
          bool flag1 = false;
          foreach (DataEnemyParamAbilities enemyParamAbilities in enemyParamAbilitiesList)
          {
            if (my.AbilityTag.Equals(enemyParamAbilities.Tag))
              flag1 = true;
          }
          if (!flag1)
          {
            bool flag2 = false;
            foreach (DataEnemyParam dataEnemyParam in this.EnemyParentInfo.Params)
            {
              foreach (DataEnemyParamAbilities ability in dataEnemyParam.Abilities)
              {
                if (ability.Tag.Equals(my.AbilityTag))
                {
                  enemyParamAbilitiesList.Add(ability);
                  flag2 = true;
                  break;
                }
              }
              if (flag2)
                break;
            }
          }
        }
      }
      return enemyParamAbilitiesList;
    }
  }
}
