// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.DataActiveBattle
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace FFRKInspector.GameData
{
    public class DataActiveBattle
  {
    [JsonProperty("rounds")]
    public List<DataBattleRound> Rounds;
    [JsonProperty("battle_id")]
    public uint BattleId;
    [JsonProperty("buddy")]
    public List<DataBuddyInformation> Buddy;
    [JsonProperty("enemy_abilities")]
    public List<DataEnemyAbility> EnemyAbilities;
    [JsonProperty("supporter")]
    public List<DataBuddyInformation> Supporter;
    [JsonExtensionData]
    public Dictionary<string, JToken> UnknownValues;

    public IEnumerable<DropEvent> Drops
    {
      get
      {
        foreach (DataBattleRound round1 in this.Rounds)
        {
          DataBattleRound round = round1;
          foreach (DropEvent drop1 in round.Drops)
          {
            DropEvent drop = drop1;
            yield return drop;
            drop = (DropEvent) null;
          }
          round = (DataBattleRound) null;
        }
      }
    }

    public DataEnemyAbility getAbility(uint abilityId)
    {
      foreach (DataEnemyAbility enemyAbility in this.EnemyAbilities)
      {
        if ((int) enemyAbility.AbilityId == (int) abilityId)
          return enemyAbility;
      }
      Log.LogString(string.Format("DataActiveBattle: Ability ID {0} not found", (object) abilityId));
      return (DataEnemyAbility) null;
    }

    public IEnumerable<DataBuddyInformation> Buddies
    {
      get
      {
        foreach (DataBuddyInformation buddyInformation1 in this.Buddy)
        {
          DataBuddyInformation buddyInformation = buddyInformation1;
          yield return buddyInformation;
          buddyInformation = (DataBuddyInformation) null;
        }
      }
    }

    public IEnumerable<DataBuddyInformation> Supporters
    {
      get
      {
        foreach (DataBuddyInformation buddyInformation1 in this.Supporter)
        {
          DataBuddyInformation buddyInformation = buddyInformation1;
          yield return buddyInformation;
          buddyInformation = (DataBuddyInformation) null;
        }
      }
    }

    public IEnumerable<BasicEnemyParentInfo> EnemyParents
    {
      get
      {
        HashSet<BasicEnemyParentInfo> basicEnemyParentInfoSet = new HashSet<BasicEnemyParentInfo>();
        foreach (DataBattleRound round in this.Rounds)
        {
          foreach (DataEnemy enemy in round.Enemies)
          {
            foreach (DataEnemyChild child in enemy.Children)
              basicEnemyParentInfoSet.Add(new BasicEnemyParentInfo()
              {
                EnemyId = child.EnemyId,
                AiId = child.AiId,
                ChildPosId = child.ChildPosId,
                EnemyName = child.Name,
                EnemyInitHp = child.InitHp,
                EnemyMaxHp = child.MaxHp,
                Level = child.Level,
                AiArgs = enemy.AIArgs,
                Id = enemy.Id,
                ParentAiId = enemy.AiId,
                Params = child.Params,
                Constraints = child.Constraints
              });
          }
        }
        IEnumerable<BasicEnemyParentInfo> basicEnemyParentInfos = (IEnumerable<BasicEnemyParentInfo>) basicEnemyParentInfoSet;
        foreach (BasicEnemyParentInfo basicEnemyParentInfo in basicEnemyParentInfos)
          basicEnemyParentInfo.Appearances = new uint[this.Rounds.Count];
        foreach (DataBattleRound round in this.Rounds)
        {
          foreach (DataEnemy enemy in round.Enemies)
          {
            foreach (DataEnemyChild child in enemy.Children)
            {
              foreach (BasicEnemyParentInfo basicEnemyParentInfo in basicEnemyParentInfos)
              {
                if ((int) basicEnemyParentInfo.Params[0].Id == (int) child.Params[0].Id)
                  ++basicEnemyParentInfo.Appearances[(int) round.Index - 1];
              }
            }
          }
        }
        return basicEnemyParentInfos;
      }
    }

    public IEnumerable<BasicEnemyInfo> Enemies
    {
      get
      {
        HashSet<BasicEnemyInfo> basicEnemyInfoSet = new HashSet<BasicEnemyInfo>();
        foreach (DataBattleRound round in this.Rounds)
        {
          foreach (DataEnemy enemy in round.Enemies)
          {
            foreach (DataEnemyChild child in enemy.Children)
            {
              if ((uint) child.Params.Count > 0U)
              {
                foreach (DataEnemyParam dataEnemyParam in child.Params)
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
                  }
                  basicEnemyInfoSet.Add(new BasicEnemyInfo()
                  {
                    EnemyId = dataEnemyParam.Id,
                    EnemyName = dataEnemyParam.Name,
                    EnemyMaxHp = dataEnemyParam.MaxHp,
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
                    EnemyStatusImmunity = stringList,
                    EnemyParent = enemy
                  });
                }
              }
            }
          }
        }
        return (IEnumerable<BasicEnemyInfo>) basicEnemyInfoSet;
      }
    }
  }
}
