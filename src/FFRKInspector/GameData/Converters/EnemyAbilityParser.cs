// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.Converters.EnemyAbilityParser
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FFRKInspector.GameData.Converters
{
  internal class EnemyAbilityParser
  {
    public uint totalWeight;
    public EventBattleInitiated battle;
    public bool singleEnemy;

    public EnemyAbilityParser(uint totalWeight, EventBattleInitiated battle, bool single)
    {
      this.battle = battle;
      this.totalWeight = totalWeight;
      this.singleEnemy = single;
    }

    public string parseAbility(
      DataEnemyParamAbilities paramAbility,
      List<DataEnemyConstraints> constraints,
      BasicEnemyInfo enemy,
      EnemyAbilityParserOptions parseOpt)
    {
      return this.parseAbility(paramAbility, constraints, enemy, parseOpt, true, 0, 0);
    }

    public string parseAbility(
      DataEnemyParamAbilities paramAbility,
      List<DataEnemyConstraints> constraints,
      BasicEnemyInfo enemy,
      EnemyAbilityParserOptions parseOpt,
      bool include1001,
      int enumeratedTurn)
    {
      return this.parseAbility(paramAbility, constraints, enemy, parseOpt, include1001, enumeratedTurn, 0);
    }

    public string parseAbility(
      DataEnemyParamAbilities paramAbility,
      List<DataEnemyConstraints> constraints,
      BasicEnemyInfo enemy,
      EnemyAbilityParserOptions parseOpt,
      bool include1001,
      int enumeratedTurn,
      int turnPeriod)
    {
      List<DataEnemyConstraints> list1 = constraints.OrderBy<DataEnemyConstraints, string>((Func<DataEnemyConstraints, string>) (x => x.ConstraintValue), (IComparer<string>) new semiNumericComparer()).OrderBy<DataEnemyConstraints, uint>((Func<DataEnemyConstraints, uint>) (x => x.ConstraintType)).ToList<DataEnemyConstraints>();
      this.ConstraintSanityChecks(list1, enemy.EnemyId);
      DataEnemyAbility ability = this.battle.Battle.getAbility(paramAbility.AbilityId);
      StringBuilder stringBuilder = new StringBuilder();
      uint num1 = paramAbility.Weight;
      bool flag1 = false;
      if (paramAbility.Tag.Equals(""))
      {
        flag1 = true;
      }
      else
      {
        foreach (DataEnemyParamAbilities enemyAbility in enemy.EnemyAbilities)
        {
          if (enemyAbility.Tag.Equals(paramAbility.Tag))
          {
            flag1 = true;
            break;
          }
        }
      }
      if (!flag1)
        num1 = 0U;
      if (enumeratedTurn > 0)
        stringBuilder.Append(string.Format("Turn {0}{1}", (object) enumeratedTurn, turnPeriod > 0 ? (object) string.Format(" + {0}n", (object) turnPeriod) : (object) ""));
      else if (num1 > 0U)
      {
        if (parseOpt.displayFractions)
        {
          stringBuilder.Append(string.Format("{0}/{1} chance", (object) paramAbility.Weight.ToString("N0"), (object) this.totalWeight.ToString("N0")));
        }
        else
        {
          Decimal num2 = Convert.ToDecimal(100.0 * (double) paramAbility.Weight / (double) this.totalWeight);
          stringBuilder.Append(num2.ToString("0.##") + "% chance");
        }
        if (paramAbility.UnlockTurn > 0U)
          stringBuilder.Append(string.Format(", unlocks on {0} ATB", (object) this.AddOrdinal(paramAbility.UnlockTurn)));
      }
      else
        stringBuilder.Append("Special");
      string str1 = parseOpt.translateAbilityNames ? this.translateAbility(ability.name) : ability.name;
      stringBuilder.Append(": " + str1);
      if (parseOpt.displayCastTimes)
      {
        Decimal castTime = (Decimal) ability.Options.CastTime;
        stringBuilder.Append(" - " + (castTime / 1000M).ToString("0.###") + "s CT");
      }
      stringBuilder.Append(" (");
      string input = this.parseAbilityEffects(ability);
      if (enumeratedTurn > 0 && (enemy.EnemyParentInfo.Phases.Count<BasicEnemyInfo>() >= 4 || enemy.EnemyParentInfo.ChildPosId == 1U) && enemy.EnemyParentInfo.AiArgs.Any<DataAIArgs>((Func<DataAIArgs, bool>) (x => x.Tag.Contains("ability_target_map_in_phase_"))))
      {
        string target = this.parseTarget(ability.Options);
        uint myPhase = enemy.EnemyId % 10U;
        List<string> list2 = ((IEnumerable<string>) enemy.EnemyParentInfo.AiArgs.FirstOrDefault<DataAIArgs>((Func<DataAIArgs, bool>) (x => x.Tag == "ability_target_map_in_phase_" + (object) myPhase)).ArgValue.Split(new string[1]
        {
          "\n"
        }, StringSplitOptions.None)).ToList<string>();
        Regex match = new Regex(string.Format("^{0}:", (object) enumeratedTurn));
        string str2 = list2.FirstOrDefault<string>((Func<string, bool>) (x => match.Match(x).Success));
        if (str2 != null)
        {
          string str3 = str2.Replace(enumeratedTurn.ToString() + ":", "");
          string[] strArray = str3.Split(',');
          string replacement = "";
          if (str3 == "1,2,3,4,5")
            replacement = "AoE";
          else if (strArray.Length == 1)
            replacement = string.Format("Slot [{0}]", (object) strArray[0]);
          else if (strArray.Length >= 2)
          {
            for (int index = 0; index < strArray.Length; ++index)
              strArray[index] = string.Format("[{0}]", (object) strArray[index]);
            replacement = string.Format("Slots {0}", (object) string.Join("", strArray));
          }
          if (replacement != "")
            input = new Regex(Regex.Escape(target)).Replace(input, replacement, 1);
        }
      }
      stringBuilder.Append(input);
      stringBuilder.Append(")");
      bool flag2 = false;
      List<string> source1 = new List<string>();
      List<string> source2 = new List<string>();
      foreach (DataEnemyConstraints enemyConstraints in list1)
      {
        if ((enemyConstraints.EnemyStatusId == 0U || (int) enemyConstraints.EnemyStatusId == (int) enemy.EnemyId) && enemyConstraints.AbilityTag.Equals(paramAbility.Tag))
        {
          string str2 = enemyConstraints.EnemyStatusId == 0U ? "global" : "local";
          if (enemyConstraints.ConstraintType != 1001U || include1001 || enemyConstraints.EnemyStatusId == 0U)
            flag2 = true;
          uint result = 0;
          uint.TryParse(enemyConstraints.ConstraintValue, out result);
          string str3 = result == 1U ? "" : "s";
          switch (enemyConstraints.ConstraintType)
          {
            case 1001:
              if (include1001 || enemyConstraints.EnemyStatusId == 0U)
              {
                source1.Add(string.Format("on {0} turn {1}", (object) str2, (object) result));
                break;
              }
              break;
            case 1002:
              source1.Add(string.Format("on {0} turn after last use", (object) this.AddOrdinal(result)));
              break;
            case 1003:
              string name = this.battle.Battle.getAbility(enemy.getAbilityByTag(enemyConstraints.ConstraintValue).AbilityId).name;
              source1.Add(string.Format("after {0} is used", parseOpt.translateAbilityNames ? (object) this.translateAbility(name) : (object) name));
              break;
            case 1004:
              source1.Add(string.Format("on {0} turn {1} if not yet used", (object) str2, (object) result));
              break;
            case 1005:
              source1.Add(string.Format("when below {0}% HP if not yet used", (object) (uint) ((int) result + 1)));
              break;
            case 2001:
              source2.Add(string.Format("until {0} turn {1}", (object) str2, (object) result));
              break;
            case 2002:
              source2.Add(string.Format("for {0} turn{1} after use", (object) (uint) ((int) result - 1), (int) result - 1 == 1 ? (object) "" : (object) "s"));
              break;
            case 2003:
              source2.Add(string.Format("after being used {0} time{1}", (object) result, (object) str3));
              break;
            case 2004:
              source2.Add(string.Format("when above {0}% HP", (object) result));
              break;
            case 2005:
              source2.Add(string.Format("when below {0}% HP", (object) (uint) ((int) result + 1)));
              break;
            case 2006:
              source2.Add(string.Format("after {0} turn {1}", (object) str2, (object) result));
              break;
            default:
              source1.Add("Error: constraint type not found");
              break;
          }
        }
      }
      if (flag2 && enumeratedTurn == 0)
      {
        stringBuilder.Append(" [");
        if (source1.Count >= 1)
        {
          stringBuilder.Append("Forced ");
          if (source1.Count == 1)
            stringBuilder.Append(source1[0]);
          else
            stringBuilder.Append(string.Join(", ", source1.ToArray(), 0, source1.Count - 1) + ", and " + source1.LastOrDefault<string>());
          if (source2.Count >= 1)
            stringBuilder.Append(". ");
        }
        if (source2.Count >= 1)
        {
          stringBuilder.Append("Locked ");
          if (source2.Count == 1)
            stringBuilder.Append(source2[0]);
          else
            stringBuilder.Append(string.Join(", ", source2.ToArray(), 0, source2.Count - 1) + ", and " + source2.LastOrDefault<string>());
        }
        stringBuilder.Append("]");
      }
      return stringBuilder.ToString();
    }

    public string parseCounter(DataEnemyParamCounters counter, EnemyAbilityParserOptions parseOpt)
    {
      DataEnemyAbility ability = this.battle.Battle.getAbility(counter.AbilityId);
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(string.Format("{0}% chance to counter ", (object) counter.Rate));
      switch (counter.CondType)
      {
        case 1:
          stringBuilder.Append("any attack ");
          break;
        case 2:
          string name1 = Enum.GetName(typeof (SchemaConstants.ElementID), (object) counter.CondValue);
          stringBuilder.Append(string.Format("{0}-element attacks ", (object) name1));
          break;
        case 3:
          string name2 = Enum.GetName(typeof (SchemaConstants.ExerciseAbbr), (object) counter.CondValue);
          stringBuilder.Append(string.Format("{0} attacks ", (object) name2));
          break;
        case 4:
          switch (counter.CondValue)
          {
            case 1:
              stringBuilder.Append("BLK and WHT attacks ");
              break;
            case 2:
              stringBuilder.Append("BLK and WHT attacks if Reflect is not active ");
              break;
            case 3:
              stringBuilder.Append("[Custom Condition: COUNTER_AIMING] ");
              break;
            case 4:
              stringBuilder.Append("PHY Lightning attacks ");
              break;
            case 5:
              stringBuilder.Append("[Custom Condition: MAGIC_COUNTER_AIMING] ");
              break;
            case 6:
              stringBuilder.Append("[Custom Condition: ULTIMATE_POWER_GOT_IN_HELL] ");
              break;
            case 7:
              stringBuilder.Append("[Custom Condition: PHYSICAL_AND_BLACK_MAGIC_DAMAGED_ONLY_BY_ENEMY] ");
              break;
            default:
              stringBuilder.Append("CUSTOM CONDITION ");
              break;
          }
          break;
        case 5:
          string str1 = Enum.GetName(typeof (SchemaConstants.AbilityCategoryID), (object) counter.CondValue).Replace('_', ' ');
          stringBuilder.Append(string.Format("{0} attacks ", (object) str1));
          break;
        case 6:
          string str2 = Enum.GetName(typeof (SchemaConstants.StatusID), (object) counter.CondValue).Replace('_', ' ');
          stringBuilder.Append(string.Format("{0}-inflicting attacks ", (object) str2));
          break;
        default:
          stringBuilder.Append("Counter condition type error ");
          break;
      }
      stringBuilder.Append(string.Format("with: {0} (", parseOpt.translateAbilityNames ? (object) this.translateAbility(ability.name) : (object) ability.name));
      stringBuilder.Append(this.parseAbilityEffects(ability));
      stringBuilder.Append(")");
      return stringBuilder.ToString();
    }

    private string parseAbilityEffects(DataEnemyAbility ability)
    {
      DataEnemyAbilityOptions options1 = ability.Options;
      DataEnemyAbilityOptions o = options1;
      StringBuilder stringBuilder = new StringBuilder();
      string name = Enum.GetName(typeof (SchemaConstants.ExerciseAbbr), (object) ability.ExerciseType);
      string target1 = this.parseTarget(options1);
      string damageThreshold1 = this.parseDamageThreshold(o.MaxDamageThreshold);
      string str1 = "";
      string str2 = "";
      string str3 = "";
      string str4 = "";
      string str5 = "";
      string str6 = "";
      string str7 = "";
      string str8 = "";
      List<string> source = new List<string>();
      List<string> stringList = new List<string>();
      DataEnemyAbilityOptions options2 = new DataEnemyAbilityOptions();
      bool flag1 = true;
      switch (ability.ActionId)
      {
        case 1:
          str1 = this.parseUndefArgs(new int[3]
          {
            o.Arg9,
            o.Arg10,
            o.Arg11
          });
          string barterRate = this.parseBarterRate(o.Arg2);
          string multipleElements1 = this.parseMultipleElements(new int[2]
          {
            o.Arg3,
            o.Arg8
          });
          string atkType1 = this.parseAtkType(o.Arg4);
          string forceHit1 = this.parseForceHit(o.Arg5);
          string sameTarget1 = this.parseSameTarget(o.Arg7, o.TargetRange);
          if (o.Arg6 == 1)
          {
            stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}{5}physical {6}damage{7}", (object) name, (object) target1, (object) o.Arg1, (object) forceHit1, (object) atkType1, (object) damageThreshold1, (object) multipleElements1, (object) barterRate));
            break;
          }
          if (o.Arg6 > 1)
          {
            stringBuilder.Append(string.Format("{0}: {1}x {2}{3} attacks, {4}% {5}{6}{7}physical {8}damage{9}", (object) name, (object) o.Arg6, (object) sameTarget1, (object) target1, (object) o.Arg1, (object) forceHit1, (object) atkType1, (object) damageThreshold1, (object) multipleElements1, (object) barterRate));
            break;
          }
          break;
        case 3:
          string atkType2 = this.parseAtkType(o.Arg2);
          string forceHit2 = this.parseForceHit(o.Arg3);
          stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}{5}physical damage", (object) name, (object) target1, (object) o.Arg1, (object) forceHit2, (object) atkType2, (object) damageThreshold1));
          if ((uint) o.Arg4 > 0U)
          {
            stringBuilder.Append(string.Format(" [damage calculation modified using \"{0}\" param adjust]", (object) this.damageCalculateParamLookup(o.Arg4)));
            break;
          }
          break;
        case 4:
          string atkType3 = this.parseAtkType(o.Arg3);
          string forceHit3 = this.parseForceHit(o.Arg4);
          string drain1 = this.parseDrain(o.Arg2);
          stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}{5}physical damage{6}", (object) name, (object) target1, (object) o.Arg1, (object) forceHit3, (object) atkType3, (object) damageThreshold1, (object) drain1));
          break;
        case 5:
          string element1 = this.parseElement(o.Arg2);
          string ignoreReflect1 = this.parseIgnoreReflect(o.Arg4, o.TargetRange, ability.ExerciseType);
          string minDmg1 = this.parseMinDmg(o.Arg3);
          stringBuilder.Append(string.Format("{0}: {1} {2}% {3}magic {4}damage{5}{6}", (object) name, (object) target1, (object) o.Arg1, (object) damageThreshold1, (object) element1, (object) minDmg1, (object) ignoreReflect1));
          break;
        case 7:
          str1 = this.parseUndefArgs(new int[6]
          {
            o.Arg9,
            o.Arg10,
            o.Arg11,
            o.Arg12,
            o.Arg13,
            o.Arg15
          });
          string atkType4 = this.parseAtkType(o.Arg3);
          string forceHit4 = this.parseForceHit(o.Arg4);
          string element2 = this.parseElement(o.Arg5);
          string crit1 = this.parseCrit(o.Arg7);
          string sameTarget2 = this.parseSameTarget(o.Arg6, o.TargetRange);
          string multiTarget1 = this.parseMultiTarget(o.Arg2, sameTarget2, target1);
          string damageCalcType1 = this.parseDamageCalcType(o.Arg14, o.Arg16);
          string ignoreBlink1 = this.parseIgnoreBlink(o.Arg17);
          stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}{5}{6}physical {7}damage{8}{9}", (object) name, (object) multiTarget1, (object) o.Arg1, (object) forceHit4, (object) atkType4, (object) damageCalcType1, (object) damageThreshold1, (object) element2, (object) crit1, (object) ignoreBlink1));
          if ((uint) o.Arg8 > 0U)
          {
            stringBuilder.Append(string.Format(" [damage calculation modified using \"{0}\" param adjust]", (object) this.damageCalculateParamLookup(o.Arg8)));
            break;
          }
          break;
        case 8:
          flag1 = false;
          string removeStatusBundle1 = this.parseRemoveStatusBundle(o.Arg1);
          string ignoreReflect2 = this.parseIgnoreReflect(o.Arg2, o.TargetRange, ability.ExerciseType);
          stringBuilder.Append(string.Format("{0}: {1} - {2}{3}", (object) name, (object) target1, (object) removeStatusBundle1, (object) ignoreReflect2));
          break;
        case 11:
          string ignoreReflect3 = this.parseIgnoreReflect(o.Arg3, o.TargetRange, ability.ExerciseType);
          stringBuilder.Append(string.Format("{0}: {1} - {2}% chance to Raise with {3}% HP{4}", (object) name, (object) target1, (object) o.Arg1, (object) o.Arg2, (object) ignoreReflect3));
          break;
        case 12:
          str1 = this.parseUndefArgs(new int[1]{ o.Arg3 });
          string damageThreshold2 = this.parseDamageThreshold(o.MinDamageThreshold);
          string ignoreReflect4 = this.parseIgnoreReflect(o.Arg4, o.TargetRange, ability.ExerciseType);
          stringBuilder.Append(string.Format("{0}: {1} - Factor {2} {3}heal{4}", (object) name, (object) target1, (object) o.Arg1, (object) damageThreshold2, (object) ignoreReflect4));
          break;
        case 14:
          str1 = this.parseUndefArgs(new int[1]{ o.Arg7 });
          flag1 = false;
          string multipleStatus1 = this.parseMultipleStatus(new int[5]
          {
            o.Arg1,
            o.Arg2,
            o.Arg3,
            o.Arg4,
            o.Arg5
          });
          string str9 = this.parseStatusFactorPhrase(o.StatusAilmentsFactor, o).Replace(", ", "");
          string duration1 = this.parseDuration(o.Arg6);
          string ignoreAstra = this.parseIgnoreAstra(o.Arg8, o.TargetSegment, o.Arg1 == 0 ? o.StatusAilmentsId : o.Arg1);
          string ignoreReflect5 = this.parseIgnoreReflect(o.Arg9, o.TargetRange, ability.ExerciseType);
          stringBuilder.Append(string.Format("{0}: {1} {2} {3}{4}{5}{6}{7}", (object) name, (object) target1, (object) str9, (object) multipleStatus1, (object) duration1, (object) this.parseForceInflictMulti(o.StatusAilmentsFactor, multipleStatus1, o), (object) ignoreAstra, (object) ignoreReflect5));
          break;
        case 15:
          flag1 = false;
          string multipleStatus2 = this.parseMultipleStatus(new int[5]
          {
            o.Arg1,
            o.Arg2,
            o.Arg3,
            o.Arg4,
            o.Arg5
          });
          string ignoreReflect6 = this.parseIgnoreReflect(o.Arg6, o.TargetRange, ability.ExerciseType);
          stringBuilder.Append(string.Format("{0}: {1} auto-hit {2}{3}", (object) name, (object) target1, (object) multipleStatus2, (object) ignoreReflect6));
          break;
        case 16:
        case 147:
          str1 = this.parseUndefArgs(new int[6]
          {
            o.Arg7,
            o.Arg8,
            o.Arg9,
            o.Arg10,
            o.Arg11,
            o.Arg12
          });
          string element3 = this.parseElement(o.Arg2);
          string minDmg2 = this.parseMinDmg(o.Arg3);
          string sameTarget3 = this.parseSameTarget(o.Arg5, o.TargetRange);
          string ignoreReflect7 = this.parseIgnoreReflect(o.Arg6, o.TargetRange, ability.ExerciseType);
          string damageCalcType2 = this.parseDamageCalcType(o.Arg13, o.Arg14);
          string ignoreMblink1 = this.parseIgnoreMblink(o.Arg15);
          string multiTarget2 = this.parseMultiTarget(o.Arg4, sameTarget3, target1);
          stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}magic {5}damage{6}{7}{8}", (object) name, (object) multiTarget2, (object) o.Arg1, (object) damageCalcType2, (object) damageThreshold1, (object) element3, (object) minDmg2, (object) ignoreReflect7, (object) ignoreMblink1));
          break;
        case 17:
          flag1 = false;
          string status1 = this.parseStatus(o.StatusAilmentsId);
          string statusFactor1 = this.parseStatusFactor(o.StatusAilmentsFactor, o);
          string str10 = o.StatusAilmentsFactor >= 998 ? "auto-hit" : string.Format("{0}% chance to deal", (object) statusFactor1);
          stringBuilder.Append(string.Format("{0}: {1} {2} {3}% current HP {4}damage", (object) name, (object) target1, (object) str10, (object) o.Arg1, (object) damageThreshold1));
          if (o.StatusAilmentsId != 0 && o.StatusAilmentsFactor < 998)
          {
            stringBuilder.Append(string.Format(", subject to {0} reistance", (object) status1));
            break;
          }
          break;
        case 19:
        case 93:
          stringBuilder.Append(string.Format("{0}: {1} deals {2}damage equal to {3}% of own current HP and kills self", (object) name, (object) target1, (object) damageThreshold1, (object) o.Arg1));
          break;
        case 28:
        case 138:
          flag1 = false;
          string customParam1 = this.parseCustomParam(o.StatusAilmentsId);
          string statusFactorPhrase1 = this.parseStatusFactorPhrase(o.StatusAilmentsFactor, o);
          string buffAmount1 = this.parseBuffAmount(o.Arg2);
          string duration2 = this.parseDuration(o.Arg3);
          string atkType5 = this.parseAtkType(o.Arg4);
          string forceHit5 = this.parseForceHit(o.Arg5);
          stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}{5}physical damage{6} {7} {8}{9}", (object) name, (object) target1, (object) o.Arg1, (object) forceHit5, (object) atkType5, (object) damageThreshold1, (object) statusFactorPhrase1, (object) customParam1, (object) buffAmount1, (object) duration2));
          break;
        case 29:
          flag1 = false;
          string status2 = this.parseStatus(o.StatusAilmentsId);
          string statusFactor2 = this.parseStatusFactor(o.StatusAilmentsFactor, o);
          string str11 = o.StatusAilmentsFactor >= 998 ? "auto-hit" : string.Format("{0}% chance to deal", (object) statusFactor2);
          stringBuilder.Append(string.Format("{0}: {1} {2} {3}% max HP {4}damage", (object) name, (object) target1, (object) str11, (object) o.Arg1, (object) damageThreshold1));
          if (o.StatusAilmentsId != 0 && o.StatusAilmentsFactor < 998)
          {
            stringBuilder.Append(string.Format(", subject to {0} reistance", (object) status2));
            break;
          }
          break;
        case 30:
        case 150:
          string element4 = this.parseElement(o.Arg5);
          string atkType6 = this.parseAtkType(o.Arg2);
          string forceHit6 = this.parseForceHit(o.Arg3);
          string expFactor1 = this.parseExpFactor(o.Arg4);
          string ignoreBlink2 = this.parseIgnoreBlink(o.Arg6);
          stringBuilder.Append(string.Format("{0}: {1} {2}% piercing {3} {4}{5}{6}physical {7}damage{8}", (object) name, (object) target1, (object) o.Arg1, (object) expFactor1, (object) forceHit6, (object) atkType6, (object) damageThreshold1, (object) element4, (object) ignoreBlink2));
          break;
        case 31:
        case 104:
          string element5 = this.parseElement(o.Arg2);
          string minDmg3 = this.parseMinDmg(o.Arg3);
          string expFactor2 = this.parseExpFactor(o.Arg4);
          string ignoreMblink2 = this.parseIgnoreMblink(o.Arg5);
          stringBuilder.Append(string.Format("{0}: {1} {2}% piercing {3} {4}magic {5}damage{6}{7}", (object) name, (object) target1, (object) o.Arg1, (object) expFactor2, (object) damageThreshold1, (object) element5, (object) ignoreMblink2, (object) minDmg3));
          break;
        case 32:
          string element6 = this.parseElement(o.Arg2);
          string ignoreReflect8 = this.parseIgnoreReflect(o.Arg4, o.TargetRange, ability.ExerciseType);
          string minDmg4 = this.parseMinDmg(o.Arg3);
          stringBuilder.Append(string.Format("{0}: {1} {2}% {3}magic {4}damage{5}{6}", (object) name, (object) target1, (object) o.Arg1, (object) damageThreshold1, (object) element6, (object) minDmg4, (object) ignoreReflect8));
          break;
        case 33:
          flag1 = false;
          string customParam2 = this.parseCustomParam(o.StatusAilmentsId);
          string statusFactorPhrase2 = this.parseStatusFactorPhrase(o.StatusAilmentsFactor, o);
          string buffAmount2 = this.parseBuffAmount(o.Arg1);
          string duration3 = this.parseDuration(o.Arg2);
          str2 = this.parseForceHit(o.Arg3);
          string ignoreReflect9 = this.parseIgnoreReflect(o.Arg4, o.TargetRange, ability.ExerciseType);
          if (o.Arg3 > 0)
          {
            stringBuilder.Append(string.Format("{0}: {1} auto-hit {2} {3}{4}{5}", (object) name, (object) target1, (object) customParam2, (object) buffAmount2, (object) duration3, (object) ignoreReflect9));
            break;
          }
          stringBuilder.Append(string.Format("{0}: {1}{2} {3} {4}{5}{6}", (object) name, (object) target1, (object) statusFactorPhrase2, (object) customParam2, (object) buffAmount2, (object) duration3, (object) ignoreReflect9));
          break;
        case 35:
          string element7 = this.parseElement(o.Arg2);
          string atkType7 = this.parseAtkType(o.Arg3);
          string forceHit7 = this.parseForceHit(o.Arg4);
          stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}{5}physical {6}damage", (object) name, (object) target1, (object) o.Arg1, (object) forceHit7, (object) atkType7, (object) damageThreshold1, (object) element7));
          break;
        case 36:
          string multipleElements2 = this.parseMultipleElements(new int[2]
          {
            o.Arg2,
            o.Arg7
          });
          string minDmg5 = this.parseMinDmg(o.Arg3);
          string ignoreReflect10 = this.parseIgnoreReflect(o.Arg8, o.TargetRange, ability.ExerciseType);
          string drain2 = this.parseDrain(o.Arg4);
          string damageCalcType3 = this.parseDamageCalcType(o.Arg5, o.Arg6);
          stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}magic {5}damage{6}{7}{8}", (object) name, (object) target1, (object) o.Arg1, (object) damageCalcType3, (object) damageThreshold1, (object) multipleElements2, (object) drain2, (object) minDmg5, (object) ignoreReflect10));
          break;
        case 37:
          stringBuilder.Append(string.Format("{0}: {1} {2}% of own missing HP as {3}damage", (object) name, (object) target1, (object) o.Arg1, (object) damageThreshold1));
          break;
        case 40:
          flag1 = false;
          stringBuilder.Append(string.Format("{0}: {1} {2}% chance to reduce an ability's uses by {3}% of max uses", (object) name, (object) target1, (object) o.Arg2, (object) o.Arg1));
          if (o.Arg1 != 100)
          {
            stringBuilder.Append(", rounded up");
            break;
          }
          break;
        case 41:
        case 113:
          string str12 = o.Arg2 != 0 || this.isSingleTarget(o.TargetRange) ? "" : ", divided by number of targets hit";
          stringBuilder.Append(string.Format("{0}: {1} {2} {3}fixed damage{4}", (object) name, (object) target1, (object) o.Arg1, (object) damageThreshold1, (object) str12));
          break;
        case 42:
          flag1 = false;
          string atkType8 = this.parseAtkType(o.Arg2);
          string forceHit8 = this.parseForceHit(o.Arg3);
          string str13 = o.TargetMethod == 4 ? target1 : target1 + string.Format("[cannot target units with {0}]", (object) this.parseStatus(o.StatusAilmentsId));
          stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}{5}physical damage", (object) name, (object) str13, (object) o.Arg1, (object) forceHit8, (object) atkType8, (object) damageThreshold1));
          break;
        case 43:
          flag1 = false;
          string element8 = this.parseElement(o.Arg2);
          string minDmg6 = this.parseMinDmg(o.Arg3);
          string customParam3 = this.parseCustomParam(o.StatusAilmentsId);
          string statusFactorPhrase3 = this.parseStatusFactorPhrase(o.StatusAilmentsFactor, o);
          string buffAmount3 = this.parseBuffAmount(o.Arg4);
          string duration4 = this.parseDuration(o.Arg5);
          stringBuilder.Append(string.Format("{0}: {1} {2}% {3}magic {4}damage{5}{6} {7} {8}{9}", (object) name, (object) target1, (object) o.Arg1, (object) damageThreshold1, (object) element8, (object) minDmg6, (object) statusFactorPhrase3, (object) customParam3, (object) buffAmount3, (object) duration4));
          break;
        case 44:
        case 71:
          stringBuilder.Append(string.Format("{0}: {1} - reduces HP to {2}", (object) name, (object) target1, (object) o.Arg1));
          if ((uint) o.MaxDamageThreshold > 0U)
          {
            stringBuilder.Append(", overflowable");
            break;
          }
          break;
        case 45:
          flag1 = false;
          string fracHp1 = this.parseFracHP(o.Arg2);
          str4 = this.parseStatusFactor(o.StatusAilmentsFactor, o);
          if (o.Arg3 == 0)
          {
            stringBuilder.Append(string.Format("{0}: {1} {2}% {3} HP {4}damage (ignores KO resist)", (object) name, (object) target1, (object) o.Arg1, (object) fracHp1, (object) damageThreshold1));
            break;
          }
          stringBuilder.Append(string.Format("{0}: {1} auto-hit {2}% {3} HP {4}damage", (object) name, (object) target1, (object) o.Arg1, (object) fracHp1, (object) damageThreshold1));
          break;
        case 47:
          flag1 = false;
          string atkType9 = this.parseAtkType(o.Arg2);
          string str14 = o.Arg3 <= 0 || o.StatusAilmentsId <= 0 ? "" : string.Format(", {0} {1}", o.Arg3 >= 100 ? (object) "removes" : (object) (o.Arg3.ToString() + "% chance to remove"), (object) this.parseStatus(o.StatusAilmentsId));
          stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}physical damage{5}", (object) name, (object) target1, (object) o.Arg1, (object) atkType9, (object) damageThreshold1, (object) str14));
          break;
        case 48:
          str1 = this.parseUndefArgs(new int[1]
          {
            o.StatusAilmentsId
          });
          string atkType10 = this.parseAtkType(o.Arg2);
          string statusBundle1 = this.parseStatusBundle(o.Arg3);
          string statusFactorPhrase4 = this.parseStatusFactorPhrase(o.StatusAilmentsFactor, o);
          string damageCalcType4 = this.parseDamageCalcType(o.Arg4, o.Arg5);
          stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}{5}physical damage{6} {7}{8}", (object) name, (object) target1, (object) o.Arg1, (object) atkType10, (object) damageCalcType4, (object) damageThreshold1, (object) statusFactorPhrase4, (object) statusBundle1, (object) this.parseForceInflict(o.StatusAilmentsFactor, o.Arg3, o)));
          break;
        case 51:
          str1 = this.parseUndefArgs(new int[1]{ o.Arg5 });
          flag1 = false;
          string multipleStatus3 = this.parseMultipleStatus(new int[5]
          {
            o.Arg1,
            o.Arg2,
            o.Arg3,
            o.Arg4,
            o.StatusAilmentsId
          });
          stringBuilder.Append(string.Format("{0}: {1} auto-hit {2}", (object) name, (object) target1, (object) multipleStatus3));
          break;
        case 52:
          flag1 = false;
          stringBuilder.Append("does nothing");
          break;
        case 53:
          string atkType11 = this.parseAtkType(o.Arg3);
          string forceHit9 = this.parseForceHit(o.Arg4);
          string element9 = this.parseElement(o.Arg5);
          string ignoreReflect11 = this.parseIgnoreReflect(o.Arg7, o.TargetRange, ability.ExerciseType);
          string sameTarget4 = this.parseSameTarget(o.Arg6, o.TargetRange);
          string multiTarget3 = this.parseMultiTarget(o.Arg2, sameTarget4, target1);
          string damageCalcType5 = this.parseDamageCalcType(o.Arg8, o.Arg9);
          stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}{5}{6}physical {7}damage{8}", (object) name, (object) multiTarget3, (object) o.Arg1, (object) forceHit9, (object) atkType11, (object) damageCalcType5, (object) damageThreshold1, (object) element9, (object) ignoreReflect11));
          break;
        case 55:
          string element10 = this.parseElement(o.Arg4);
          string atkType12 = this.parseAtkType(o.Arg2);
          string forceHit10 = this.parseForceHit(o.Arg3);
          stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}{5}physical {6}damage", (object) name, (object) target1, (object) o.Arg1, (object) forceHit10, (object) atkType12, (object) damageThreshold1, (object) element10));
          break;
        case 56:
          string fracHp2 = this.parseFracHP(o.Arg2);
          str4 = this.parseStatusFactor(o.StatusAilmentsFactor, o);
          if (o.Arg3 == 0)
          {
            stringBuilder.Append(string.Format("{0}: {1} {2}% {3} HP {4}damage (ignores KO resist)", (object) name, (object) target1, (object) o.Arg1, (object) fracHp2, (object) damageThreshold1));
            break;
          }
          stringBuilder.Append(string.Format("{0}: {1} auto-hit {2}% {3} HP {4}damage", (object) name, (object) target1, (object) o.Arg1, (object) fracHp2, (object) damageThreshold1));
          break;
        case 59:
          flag1 = false;
          string status3 = this.parseStatus(o.Arg2);
          string str15 = o.Arg4 == 0 ? string.Format("{0} {1}{2}", (object) this.parseStatusFactorPhrase(o.Arg1, o).Replace(", ", ""), (object) status3, (object) this.parseForceInflict(o.Arg1, o.Arg2, o)) : string.Format("auto-hit {0}", (object) status3);
          string status4 = this.parseStatus(o.StatusAilmentsId);
          string str16 = string.Format("{0}% chance to remove {1}", (object) this.parseStatusFactor(o.StatusAilmentsFactor, o), (object) status4);
          string ignoreReflect12 = this.parseIgnoreReflect(o.Arg5, o.TargetRange, ability.ExerciseType);
          stringBuilder.Append(string.Format("{0}: {1} {2}, {3}{4}", (object) name, (object) target1, (object) str15, (object) str16, (object) ignoreReflect12));
          break;
        case 61:
          string atkType13 = this.parseAtkType(o.Arg2);
          stringBuilder.Append(string.Format("{0}: {1} {2}% of own missing HP as {3}{4}physical damage", (object) name, (object) target1, (object) o.Arg1, (object) atkType13, (object) damageThreshold1));
          break;
        case 65:
          flag1 = false;
          string customParam4 = this.parseCustomParam(o.StatusAilmentsId);
          string statusFactorPhrase5 = this.parseStatusFactorPhrase(o.StatusAilmentsFactor, o);
          string buffAmount4 = this.parseBuffAmount(o.Arg1);
          string buffAmount5 = this.parseBuffAmount(o.Arg2);
          str3 = this.parseDuration(o.Arg3);
          if (o.Arg5 == 0)
          {
            stringBuilder.Append(string.Format("{0}: {1}{2} {3} {4}, and applies {5} {6} to self", (object) name, (object) target1, (object) statusFactorPhrase5, (object) customParam4, (object) buffAmount4, (object) customParam4, (object) buffAmount5));
            break;
          }
          stringBuilder.Append(string.Format("{0}: {1} auto-hit {2} {3}, and applies {4} {5} to self", (object) name, (object) target1, (object) customParam4, (object) buffAmount4, (object) customParam4, (object) buffAmount5));
          break;
        case 69:
          this.parseUndefArgs(new int[2]{ o.Arg6, o.Arg7 });
          string atkType14 = this.parseAtkType(o.Arg2);
          string forceHit11 = this.parseForceHit(o.Arg3);
          string str17 = o.Arg4 == 0 ? "" : string.Format(", applies {0} to self", (object) this.parseStatus(o.Arg4));
          if (o.Arg4 != 0 && o.Arg5 > 0)
            str17 += this.parseDuration(o.Arg5);
          stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}{5}physical damage{6}", (object) name, (object) target1, (object) o.Arg1, (object) forceHit11, (object) atkType14, (object) damageThreshold1, (object) str17));
          break;
        case 74:
          flag1 = false;
          string atkType15 = this.parseAtkType(o.Arg3);
          string sameTarget5 = this.parseSameTarget(o.Arg6, o.TargetRange);
          string multiTarget4 = this.parseMultiTarget(o.Arg2, sameTarget5, target1);
          string forceHit12 = this.parseForceHit(o.Arg4);
          string multipleElements3 = this.parseMultipleElements(new int[2]
          {
            o.Arg5,
            o.Arg9
          });
          string generalStatus1 = this.parseGeneralStatus(o.StatusAilmentsId, 0, o.StatusAilmentsFactor, o.Arg7, o.Arg8, o);
          string str18 = this.parseRemoveStatusBundle(o.Arg10).Replace("Removes", "remove");
          if (!str18.Equals(""))
          {
            string str19 = str18.Contains("remove") ? " from" : "";
            bool flag2 = o.Arg11 == o.TargetRange && o.Arg12 == o.TargetSegment;
            str18 = string.Format(", {0}% chance to {1}", (object) o.Arg13, (object) str18);
            if (!flag2)
            {
              options2.TargetMethod = o.TargetMethod;
              options2.TargetRange = o.Arg11;
              options2.TargetSegment = o.Arg12;
              string target2 = this.parseTarget(options2);
              str18 = str18 + str19 + target2;
            }
          }
          if ((uint) o.Arg14 > 0U)
            str8 = o.Arg15 != 2 ? (o.Arg15 != 3 ? string.Format(", heals [error parsing target] for {0}% max HP", (object) o.Arg14) : string.Format(", heals self for {0}% max HP", (object) o.Arg14)) : string.Format(", heals all enemies for {0}% max HP", (object) o.Arg14);
          stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}{5}physical {6}damage{7}{8}{9}", (object) name, (object) multiTarget4, (object) o.Arg1, (object) forceHit12, (object) atkType15, (object) damageThreshold1, (object) multipleElements3, (object) generalStatus1, (object) str18, (object) str8));
          break;
        case 77:
          str1 = this.parseUndefArgs(new int[3]
          {
            o.Arg11,
            o.Arg12,
            o.Arg13
          });
          flag1 = false;
          string str20 = o.Arg14 == 0 ? string.Format("{0}", (object) this.parseStatusFactorPhrase(o.StatusAilmentsFactor, o).Replace(", ", "")) : "auto-hit";
          string duration5 = this.parseDuration(o.Arg1);
          if ((uint) o.Arg2 > 0U)
            source.Add(string.Format("{0} {1}", (object) this.parseCustomParam(o.Arg2), (object) this.parseBuffAmount(o.Arg3)));
          if ((uint) o.Arg4 > 0U)
            source.Add(string.Format("{0} {1}", (object) this.parseCustomParam(o.Arg4), (object) this.parseBuffAmount(o.Arg5)));
          if ((uint) o.Arg6 > 0U)
            source.Add(string.Format("{0} {1}", (object) this.parseCustomParam(o.Arg6), (object) this.parseBuffAmount(o.Arg7)));
          string str21 = source.Count != 1 ? string.Join(", ", source.ToArray(), 0, source.Count - 1) + " and " + source.LastOrDefault<string>() : source[0];
          if (source.Count > 0)
            str7 = str20 + " " + str21 + duration5;
          string multipleStatus4 = this.parseMultipleStatus(new int[4]
          {
            o.Arg8,
            o.Arg9,
            o.Arg10,
            o.StatusAilmentsId
          });
          if (multipleStatus4 != "")
          {
            if (str7 != "")
              str7 = string.Format("{0} {1}{2} and {3}{4}", (object) str20, (object) str21, (object) duration5, (object) multipleStatus4, o.Arg14 == 0 ? (object) this.parseForceInflictMulti(o.StatusAilmentsFactor, multipleStatus4, o) : (object) "");
            else
              str7 = str20 + " " + multipleStatus4 + (o.Arg14 == 0 ? this.parseForceInflictMulti(o.StatusAilmentsFactor, multipleStatus4, o) : "");
          }
          stringBuilder.Append(string.Format("{0}: {1} {2}", (object) name, (object) target1, (object) str7));
          break;
        case 81:
          flag1 = false;
          stringBuilder.Append("does nothing");
          break;
        case 82:
          str1 = this.parseUndefArgs(new int[3]
          {
            o.Arg10,
            o.Arg11,
            o.Arg12
          });
          flag1 = false;
          string str22 = o.Arg3 == 0 ? string.Format("{0}", (object) this.parseStatusFactor(o.Arg2, o).Replace(", ", "")) : "auto-hit";
          string duration6 = this.parseDuration(o.Arg1);
          if ((uint) o.Arg4 > 0U)
            source.Add(string.Format("{0} {1}", (object) this.parseCustomParam(o.Arg4), (object) this.parseBuffAmount(o.Arg5)));
          if ((uint) o.Arg6 > 0U)
            source.Add(string.Format("{0} {1}", (object) this.parseCustomParam(o.Arg6), (object) this.parseBuffAmount(o.Arg7)));
          if ((uint) o.Arg8 > 0U)
            source.Add(string.Format("{0} {1}", (object) this.parseCustomParam(o.Arg8), (object) this.parseBuffAmount(o.Arg9)));
          string str23 = source.Count != 1 ? string.Join(", ", source.ToArray(), 0, source.Count - 1) + " and " + source.LastOrDefault<string>() : source[0];
          stringBuilder.Append(string.Format("{0}: {1} {2} {3}{4}", (object) name, (object) target1, (object) str22, (object) str23, (object) duration6));
          break;
        case 85:
          string atkType16 = this.parseAtkType(o.Arg2);
          string multipleStatus5 = this.parseMultipleStatus(new int[5]
          {
            o.Arg4,
            o.Arg5,
            o.Arg6,
            o.Arg7,
            o.Arg8
          });
          string str24 = o.Arg3 == 0 || multipleStatus5.Equals("") ? "" : string.Format(", {0}% chance to remove {1}", (object) this.parseStatusFactor(o.Arg3, o), (object) multipleStatus5);
          string str25 = o.Arg3 == 0 || o.Arg9 == 0 ? "" : string.Format(", {0}% chance to {1}", (object) this.parseStatusFactor(o.Arg3, o), (object) this.parseRemoveStatusBundle(o.Arg9).Replace("Removes", "remove"));
          stringBuilder.Append(string.Format("{0}: {1} {2}{3}physical damage{4}{5}", (object) name, (object) target1, (object) atkType16, (object) damageThreshold1, (object) str24, (object) str25));
          break;
        case 88:
          str1 = this.parseUndefArgs(new int[6]
          {
            o.Arg7,
            o.Arg8,
            o.Arg9,
            o.Arg13,
            o.Arg14,
            o.Arg15
          });
          string multipleElements4 = this.parseMultipleElements(new int[3]
          {
            o.Arg3,
            o.Arg12,
            o.Arg16
          });
          string sameTarget6 = this.parseSameTarget(o.Arg4, o.TargetRange);
          string ignoreReflect13 = this.parseIgnoreReflect(o.Arg10, o.TargetRange, ability.ExerciseType);
          string multiTarget5 = this.parseMultiTarget(o.Arg2, sameTarget6, target1);
          string damageCalcType6 = this.parseDamageCalcType(o.Arg18, o.Arg19);
          string ignoreMblink3 = this.parseIgnoreMblink(o.Arg20);
          string str26 = o.Arg5 == 0 ? "" : string.Format(", applies {0} to self", (object) this.parseStatusOrBundle(o.Arg5));
          if (o.Arg5 != 0 && o.Arg6 > 0)
            str26 += this.parseDuration(o.Arg6);
          stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}magic {5}damage{6}{7}{8}", (object) name, (object) multiTarget5, (object) o.Arg1, (object) damageCalcType6, (object) damageThreshold1, (object) multipleElements4, (object) ignoreReflect13, (object) ignoreMblink3, (object) str26));
          break;
        case 90:
          flag1 = false;
          str1 = this.parseUndefArgs(new int[2]
          {
            o.Arg2,
            o.Arg3
          });
          string ignoreReflect14 = this.parseIgnoreReflect(o.Arg4, o.TargetRange, ability.ExerciseType);
          string str27 = o.Arg5 != 0 ? string.Format(", {0}", (object) this.parseRemoveStatusBundle(o.Arg5)) : "";
          if ((uint) o.Arg6 > 0U)
            source.Add(this.parseStatus(o.Arg6));
          if ((uint) o.Arg7 > 0U)
            source.Add(this.parseStatus(o.Arg7));
          if ((uint) o.Arg8 > 0U)
            source.Add(this.parseStatus(o.Arg8));
          if (source.Count > 0)
            str7 = string.Format(", removes {0} and {1}", (object) string.Join(", ", source.ToArray(), 0, source.Count - 1), (object) source.LastOrDefault<string>());
          stringBuilder.Append(string.Format("{0}: {1} - Factor {2} {3}heal{4}{5}{6}", (object) name, (object) target1, (object) o.Arg1, (object) damageThreshold1, (object) str27, (object) str7, (object) ignoreReflect14));
          break;
        case 95:
          flag1 = false;
          str1 = this.parseUndefArgs(new int[2]
          {
            o.Arg3,
            o.Arg4
          });
          string element11 = this.parseElement(o.Arg2);
          string multipleStatus6 = this.parseMultipleStatus(new int[6]
          {
            o.Arg5,
            o.Arg6,
            o.Arg7,
            o.Arg8,
            o.Arg9,
            o.StatusAilmentsId
          });
          string statusFactorPhrase6 = this.parseStatusFactorPhrase(o.StatusAilmentsFactor, o);
          stringBuilder.Append(string.Format("{0}: {1} {2}% {3}magic {4}damage{5} {6}{7}", (object) name, (object) target1, (object) o.Arg1, (object) damageThreshold1, (object) element11, (object) statusFactorPhrase6, (object) multipleStatus6, (object) this.parseForceInflictMulti(o.StatusAilmentsFactor, multipleStatus6, o)));
          break;
        case 96:
          str1 = this.parseUndefArgs(new int[2]
          {
            o.Arg2,
            o.Arg3
          });
          string damageThreshold3 = this.parseDamageThreshold(o.MinDamageThreshold);
          flag1 = false;
          stringBuilder.Append(string.Format("{0}: {1} {2}% max HP {3}heal", (object) name, (object) target1, (object) o.Arg1, (object) damageThreshold3));
          break;
        case 100:
          string atkType17 = this.parseAtkType(o.Arg3);
          string sameTarget7 = this.parseSameTarget(o.Arg6, o.TargetRange);
          string multiTarget6 = this.parseMultiTarget(o.Arg2, sameTarget7, target1);
          string forceHit13 = this.parseForceHit(o.Arg4);
          string element12 = this.parseElement(o.Arg5);
          string str28 = o.Arg7 == 0 || o.Arg8 == 0 ? "" : string.Format(", {0}% chance to {1}", (object) this.parseStatusFactor(o.Arg8, o), (object) this.parseRemoveStatusBundle(o.Arg7).Replace("Removes", "remove"));
          stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}{5}physical {6}damage{7}", (object) name, (object) multiTarget6, (object) o.Arg1, (object) forceHit13, (object) atkType17, (object) damageThreshold1, (object) element12, (object) str28));
          break;
        case 101:
          flag1 = false;
          str1 = this.parseUndefArgs(new int[9]
          {
            o.Arg11,
            o.Arg12,
            o.Arg13,
            o.Arg14,
            o.Arg15,
            o.Arg16,
            o.Arg17,
            o.Arg18,
            o.Arg21
          });
          string atkType18 = this.parseAtkType(o.Arg3);
          string forceHit14 = this.parseForceHit(o.Arg4);
          string multipleElements5 = this.parseMultipleElements(new int[3]
          {
            o.Arg5,
            o.Arg10,
            o.Arg19
          });
          string sameTarget8 = this.parseSameTarget(o.Arg6, o.TargetRange);
          string multiTarget7 = this.parseMultiTarget(o.Arg2, sameTarget8, target1);
          string statusBundle2 = this.parseStatusBundle(o.Arg7);
          string statusFactorPhrase7 = this.parseStatusFactorPhrase(o.StatusAilmentsFactor, o);
          string str29 = o.Arg7 != 0 ? string.Format("{0} {1}{2}", (object) statusFactorPhrase7, (object) statusBundle2, (object) this.parseForceInflictBundle(o.StatusAilmentsFactor, o.Arg7, o)) : "";
          string str30 = o.Arg20 != 0 ? string.Format(", {0}", (object) this.parseRemoveStatusBundle(o.Arg20)) : "";
          string damageCalcType7 = this.parseDamageCalcType(o.Arg8, o.Arg9);
          stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}{5}{6}physical {7}damage{8}{9}", (object) name, (object) multiTarget7, (object) o.Arg1, (object) forceHit14, (object) atkType18, (object) damageCalcType7, (object) damageThreshold1, (object) multipleElements5, (object) str29, (object) str30));
          break;
        case 105:
          flag1 = false;
          string multipleElements6 = this.parseMultipleElements(new int[2]
          {
            o.Arg2,
            o.Arg15
          });
          string minDmg7 = this.parseMinDmg(o.Arg3);
          string sameTarget9 = this.parseSameTarget(o.Arg5, o.TargetRange);
          string multiTarget8 = this.parseMultiTarget(o.Arg4, sameTarget9, target1);
          string ignoreReflect15 = this.parseIgnoreReflect(o.Arg12, o.TargetRange, ability.ExerciseType);
          if (o.Arg8 > 0 && o.Arg9 > 0)
          {
            str6 += string.Format("{0} {1}{2}", (object) this.parseStatusFactorPhrase(o.Arg9, o), (object) this.parseStatus(o.Arg8), (object) this.parseForceInflict(o.Arg9, o.Arg8, o));
            stringList.Add(this.parseStatus(o.Arg8));
          }
          if (o.Arg6 > 0 && o.Arg7 > 0 && !stringList.Contains(this.parseStatus(o.Arg6)))
          {
            str6 += string.Format("{0} {1}{2}", (object) this.parseStatusFactorPhrase(o.Arg7, o), (object) this.parseStatus(o.Arg6), (object) this.parseForceInflict(o.Arg7, o.Arg6, o));
            stringList.Add(this.parseStatus(o.Arg6));
          }
          if (o.StatusAilmentsId > 0 && o.StatusAilmentsFactor > 0 && !stringList.Contains(this.parseStatus(o.StatusAilmentsId)))
            str6 += string.Format("{0} {1}{2}", (object) this.parseStatusFactorPhrase(o.StatusAilmentsFactor, o), (object) this.parseStatus(o.StatusAilmentsId), (object) this.parseForceInflict(o.StatusAilmentsFactor, o.StatusAilmentsId, o));
          string str31 = o.Arg13 <= 0 || o.Arg14 <= 0 ? "" : string.Format("{0} {1}{2}", (object) this.parseStatusFactorPhrase(o.Arg14, o), (object) this.parseStatusBundle(o.Arg13), (object) this.parseForceInflictBundle(o.Arg14, o.Arg13, o));
          string str32 = o.Arg10 <= 0 || o.Arg11 <= 0 ? "" : string.Format(", {0}% chance to {1}", (object) this.parseStatusFactor(o.Arg11, o), (object) this.parseRemoveStatusBundle(o.Arg10).Replace("Removes", "remove"));
          stringBuilder.Append(string.Format("{0}: {1} {2}% {3}magic {4}damage{5}{6}{7}{8}{9}", (object) name, (object) multiTarget8, (object) o.Arg1, (object) damageThreshold1, (object) multipleElements6, (object) minDmg7, (object) str6, (object) str31, (object) str32, (object) ignoreReflect15));
          break;
        case 106:
          string element13 = this.parseElement(o.Arg2);
          string minDmg8 = this.parseMinDmg(o.Arg3);
          stringBuilder.Append(string.Format("{0}: {1} buff-ignoring {2}% {3}magic {4}damage{5}", (object) name, (object) target1, (object) o.Arg1, (object) damageThreshold1, (object) element13, (object) minDmg8));
          break;
        case 117:
        case 118:
          flag1 = false;
          string element14 = this.parseElement(o.Arg2);
          string minDmg9 = this.parseMinDmg(o.Arg3);
          int factor = Math.Max(o.Arg8, o.StatusAilmentsFactor);
          string ignoreMblink4 = this.parseIgnoreMblink(o.Arg13);
          string generalStatus2 = this.parseGeneralStatus(o.StatusAilmentsId, o.Arg4, factor, o.Arg6, o.Arg7, o.Arg5, o);
          string str33 = o.Arg9 != 0 ? string.Format(", {0}", (object) this.parseRemoveStatusBundle(o.Arg9)) : "";
          string damageCalcType8 = this.parseDamageCalcType(o.Arg11, o.Arg12);
          stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}magic {5}damage{6}{7}{8}{9}", (object) name, (object) target1, (object) o.Arg1, (object) damageCalcType8, (object) damageThreshold1, (object) element14, (object) minDmg9, (object) generalStatus2, (object) str33, (object) ignoreMblink4));
          break;
        case 123:
          flag1 = false;
          string status5 = this.parseStatus(o.StatusAilmentsId);
          string statusFactor3 = this.parseStatusFactor(o.StatusAilmentsFactor, o);
          string str34 = o.StatusAilmentsFactor >= 998 ? "auto-hit" : string.Format("{0}% chance to deal", (object) statusFactor3);
          stringBuilder.Append(string.Format("{0}: {1} {2} [attacker's % missing HP]% current HP {3}damage", (object) name, (object) target1, (object) str34, (object) damageThreshold1));
          if (o.StatusAilmentsId != 0 && o.StatusAilmentsFactor < 998)
          {
            stringBuilder.Append(string.Format(", subject to {0} resistance", (object) status5));
            break;
          }
          break;
        case 124:
          string atkType19 = this.parseAtkType(o.Arg3);
          string forceHit15 = this.parseForceHit(o.Arg4);
          string drain3 = this.parseDrain(o.Arg2);
          stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}{5}physical damage{6}", (object) name, (object) target1, (object) o.Arg1, (object) forceHit15, (object) atkType19, (object) damageThreshold1, (object) drain3));
          break;
        case 129:
        case 130:
          flag1 = false;
          string fracHp3 = this.parseFracHP(o.Arg2);
          string str35 = this.parseIgnoreBlink(o.Arg11).Equals("") ? ", ignores KO resist" : ", ignores blinks and KO resist";
          string generalStatus3 = this.parseGeneralStatus(o.StatusAilmentsId, o.Arg4, o.Arg8 == 0 ? o.StatusAilmentsFactor : o.Arg8, o.Arg6, o.Arg7, o);
          string removeStatusBundle2 = this.parseRemoveStatusBundle(o.Arg9);
          string str36 = removeStatusBundle2.Equals("") ? "" : ", " + removeStatusBundle2;
          string status6 = this.parseStatus(o.Arg10);
          string str37 = status6.Equals("") ? "" : ", removes " + status6;
          stringBuilder.Append(string.Format("{0}: {1} {2}% {3} HP {4}damage{5}{6}{7}{8}", (object) name, (object) target1, (object) o.Arg1, (object) fracHp3, (object) damageThreshold1, (object) generalStatus3, (object) str36, (object) str37, (object) str35));
          break;
        case 136:
          this.parseUndefArgs(new int[11]
          {
            o.Arg6,
            o.Arg7,
            o.Arg9,
            o.Arg10,
            o.Arg11,
            o.Arg12,
            o.Arg13,
            o.Arg14,
            o.Arg15,
            o.Arg16,
            o.Arg17
          });
          string multipleElements7 = this.parseMultipleElements(new int[4]
          {
            o.Arg2,
            o.Arg23,
            o.Arg24,
            o.Arg25
          });
          string sameTarget10 = this.parseSameTarget(o.Arg6, o.TargetRange);
          string multiTarget9 = this.parseMultiTarget(o.Arg3, sameTarget10, target1);
          string ignoreMblink5 = this.parseIgnoreMblink(o.Arg18);
          string damageCalcType9 = this.parseDamageCalcType(o.Arg19, o.Arg20);
          string str38 = o.Arg8 == 3 ? "self" : "[**target not parsed**]";
          string str39 = o.Arg4 != 0 ? string.Format(", {0} {1} {2}heal", (object) str38, o.Arg22 == 0 ? (object) string.Format("factor {0}", (object) o.Arg4) : (object) string.Format("{0} fixed HP", (object) o.Arg4), o.Arg21 <= 0 || o.Arg22 != 0 && o.Arg4 > 9999 ? (object) "" : (object) "overflow ") : "";
          stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}magic {5}damage{6}{7}", (object) name, (object) multiTarget9, (object) o.Arg1, (object) damageCalcType9, (object) damageThreshold1, (object) multipleElements7, (object) ignoreMblink5, (object) str39));
          break;
        case 137:
          str1 = this.parseUndefArgs(new int[5]
          {
            o.Arg10,
            o.Arg11,
            o.Arg12,
            o.Arg13,
            o.Arg14
          });
          flag1 = false;
          string atkType20 = this.parseAtkType(o.Arg3);
          string forceHit16 = this.parseForceHit(o.Arg4);
          string multipleElements8 = this.parseMultipleElements(new int[2]
          {
            o.Arg5,
            o.Arg15
          });
          string sameTarget11 = this.parseSameTarget(o.Arg6, o.TargetRange);
          string multiTarget10 = this.parseMultiTarget(o.Arg2, sameTarget11, target1);
          string damageCalcType10 = this.parseDamageCalcType(o.Arg17, o.Arg18);
          string variableImperil1 = this.parseVariableImperil(o.Arg7, o.Arg8, o.Arg9, o.Arg16, o);
          string str40 = o.Arg19 != 0 ? string.Format(", applies {0} to {1}", (object) this.parseStatus(o.Arg19), this.singleEnemy ? (object) "self" : (object) "all enemies") : "";
          string str41 = o.Arg20 != 0 ? string.Format(", applies {0} to {1}", (object) this.parseStatusBundle(o.Arg20), this.singleEnemy ? (object) "self" : (object) "all enemies") : "";
          stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}{5}{6}physical {7}damage{8}{9}{10}", (object) name, (object) multiTarget10, (object) o.Arg1, (object) forceHit16, (object) atkType20, (object) damageCalcType10, (object) damageThreshold1, (object) multipleElements8, (object) variableImperil1, (object) str40, (object) str41));
          break;
        case 140:
          str1 = this.parseUndefArgs(new int[7]
          {
            o.Arg12,
            o.Arg13,
            o.Arg14,
            o.Arg15,
            o.Arg16,
            o.Arg17,
            o.Arg18
          });
          string minDmg10 = this.parseMinDmg(o.Arg2);
          string multipleElements9 = this.parseMultipleElements(new int[4]
          {
            o.Arg5,
            o.Arg6,
            o.Arg7,
            o.Arg8
          });
          string sameTarget12 = this.parseSameTarget(o.Arg4, o.TargetRange);
          string multiTarget11 = this.parseMultiTarget(o.Arg3, sameTarget12, target1);
          string ignoreReflect16 = this.parseIgnoreReflect(o.Arg9, o.TargetRange, ability.ExerciseType);
          string damageCalcType11 = this.parseDamageCalcType(o.Arg10, o.Arg11);
          stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}magic {5}damage{6}{7}", (object) name, (object) multiTarget11, (object) o.Arg1, (object) damageCalcType11, (object) damageThreshold1, (object) multipleElements9, (object) minDmg10, (object) ignoreReflect16));
          break;
        case 141:
          str1 = this.parseUndefArgs(new int[8]
          {
            o.Arg11,
            o.Arg12,
            o.Arg13,
            o.Arg14,
            o.Arg15,
            o.Arg16,
            o.Arg17,
            o.Arg18
          });
          string atkType21 = this.parseAtkType(o.Arg3);
          string forceHit17 = this.parseForceHit(o.Arg4);
          string multipleElements10 = this.parseMultipleElements(new int[4]
          {
            o.Arg6,
            o.Arg7,
            o.Arg8,
            o.Arg9
          });
          string critDamage = this.parseCritDamage(o.Arg10);
          string crit2 = this.parseCrit(o.Arg19);
          string sameTarget13 = this.parseSameTarget(o.Arg5, o.TargetRange);
          string multiTarget12 = this.parseMultiTarget(o.Arg2, sameTarget13, target1);
          stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}{5}physical {6}damage{7}{8}", (object) name, (object) multiTarget12, (object) o.Arg1, (object) forceHit17, (object) atkType21, (object) damageThreshold1, (object) multipleElements10, (object) crit2, (object) critDamage));
          break;
        case 145:
        case 146:
          str1 = this.parseUndefArgs(new int[2]
          {
            o.Arg3,
            o.Arg11
          });
          flag1 = false;
          string damageThreshold4 = this.parseDamageThreshold(o.MinDamageThreshold);
          string generalStatus4 = this.parseGeneralStatus(o.StatusAilmentsId, o.Arg4, o.Arg8 == 0 ? o.StatusAilmentsFactor : o.Arg8, o.Arg6, o.Arg7, o.Arg5, o);
          string removeStatusBundle3 = this.parseRemoveStatusBundle(o.Arg9);
          string str42 = removeStatusBundle3.Equals("") ? "" : ", " + removeStatusBundle3;
          string status7 = this.parseStatus(o.Arg10);
          string str43 = status7.Equals("") ? "" : string.Format(", removes {0}", (object) status7);
          stringBuilder.Append(string.Format("{0}: {1} - Factor {2} {3}heal{4}{5}{6}", (object) name, (object) target1, (object) o.Arg1, (object) damageThreshold4, (object) generalStatus4, (object) str42, (object) str43));
          break;
        case 151:
          str1 = this.parseUndefArgs(new int[2]
          {
            o.Arg19,
            o.Arg20
          });
          string damageCalcType12 = this.parseDamageCalcType(o.Arg17, o.Arg18);
          string minDmg11 = this.parseMinDmg(o.Arg3);
          string multipleElements11 = this.parseMultipleElements(new int[2]
          {
            o.Arg4,
            o.Arg10
          });
          string sameTarget14 = this.parseSameTarget(o.Arg5, o.TargetRange);
          string multiTarget13 = this.parseMultiTarget(o.Arg2, sameTarget14, target1);
          string ignoreMblink6 = this.parseIgnoreMblink(o.Arg24);
          string ignoreReflect17 = this.parseIgnoreReflect(o.Arg6, o.TargetRange, ability.ExerciseType);
          string variableImperil2 = this.parseVariableImperil(o.Arg7, o.Arg8, o.Arg9, o.Arg16, o);
          string customParam5 = this.parseCustomParam(o.Arg11);
          string buffAmount6 = this.parseBuffAmount(o.Arg12);
          string duration7 = this.parseDuration(o.Arg13);
          string str44 = o.Arg11 != 0 ? string.Format(", self {0} {1}{2}", (object) customParam5, (object) buffAmount6, (object) duration7) : "";
          string status8 = this.parseStatus(o.Arg14);
          string statusFactorPhrase8 = this.parseStatusFactorPhrase(o.Arg15, o);
          string str45 = o.Arg14 != 0 ? string.Format("{0} {1}{2}", (object) statusFactorPhrase8, (object) status8, (object) this.parseForceInflict(o.Arg15, o.Arg14, o)) : "";
          string status9 = this.parseStatus(o.Arg21);
          string str46 = o.Arg21 != 0 ? string.Format(", applies {0} to all allies", (object) status9) : "";
          stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}magic {5}damage{6}{7}{8}{9}{10}{11}{12}", (object) name, (object) multiTarget13, (object) o.Arg1, (object) damageCalcType12, (object) damageThreshold1, (object) multipleElements11, (object) variableImperil2, (object) ignoreReflect17, (object) ignoreMblink6, (object) minDmg11, (object) str45, (object) str44, (object) str46));
          break;
        case 155:
        case 156:
          flag1 = false;
          string atkType22 = this.parseAtkType(o.Arg3);
          string forceHit18 = this.parseForceHit(o.Arg12);
          string multipleElements12 = this.parseMultipleElements(new int[3]
          {
            o.Arg2,
            o.Arg15,
            o.Arg16
          });
          string sameTarget15 = this.parseSameTarget(o.Arg14, o.TargetRange);
          string multiTarget14 = this.parseMultiTarget(o.Arg13, sameTarget15, target1);
          string generalStatus5 = this.parseGeneralStatus(0, o.Arg4, o.Arg8 == 0 ? o.StatusAilmentsFactor : o.Arg8, o.Arg6, o.Arg7, o.Arg5, o);
          string str47 = o.Arg9 != 0 ? string.Format(", {0}", (object) this.parseRemoveStatusBundle(o.Arg9)) : "";
          string str48 = o.Arg10 != 0 ? string.Format(", Removes {0}", (object) this.parseStatus(o.Arg10)) : "";
          string ignoreBlink3 = this.parseIgnoreBlink(o.Arg11);
          stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}{5}physical {6}damage{7}{8}{9}{10}", (object) name, (object) multiTarget14, (object) o.Arg1, (object) forceHit18, (object) atkType22, (object) damageThreshold1, (object) multipleElements12, (object) generalStatus5, (object) str47, (object) str48, (object) ignoreBlink3));
          break;
        case 161:
        case 162:
          flag1 = false;
          string fracHp4 = this.parseFracHP(o.Arg2);
          string statusFactor4 = this.parseStatusFactor(o.Arg3, o);
          str2 = this.parseForceHit(o.Arg5);
          string generalStatus6 = this.parseGeneralStatus(o.StatusAilmentsId, o.Arg4, o.Arg8 == 0 ? o.StatusAilmentsFactor : o.Arg8, o.Arg6, o.Arg7, o);
          string str49 = o.Arg8 == 0 || o.Arg9 == 0 ? "" : ", " + this.parseStatusFactor(o.Arg8, o) + "% chance to " + this.parseRemoveStatusBundle(o.Arg9).Replace("Removes", "remove");
          string str50 = o.Arg8 == 0 || o.Arg10 == 0 ? "" : string.Format(", {0}% chance to remove {1}", (object) this.parseStatusFactor(o.Arg8, o), (object) this.parseStatus(o.Arg10));
          stringBuilder.Append(string.Format("{0}: {1} {2} {3}% {4} HP {5}damage, subject to Instant KO resist{6}{7}{8}", (object) name, (object) target1, o.Arg5 == 0 ? (object) string.Format("{0}% chance to deal", (object) statusFactor4) : (object) "auto-hit", (object) o.Arg1, (object) fracHp4, (object) damageThreshold1, (object) generalStatus6, (object) str49, (object) str50));
          break;
        case 174:
          string damageThreshold5 = this.parseDamageThreshold(o.MinDamageThreshold);
          flag1 = false;
          string str51 = o.Arg2 <= 0 || o.Arg3 == 0 ? "" : string.Format(", applies {0} {1}{2}", (object) this.parseCustomParam(o.Arg2), (object) this.parseBuffAmount(o.Arg3), (object) this.parseDuration(o.Arg4));
          string str52 = o.Arg5 > 0 ? string.Format(", {0}", (object) this.parseRemoveStatusBundle(o.Arg5)) : "";
          if (o.Arg6 > 0)
            str51 = str51 == "" ? string.Format(", applies {0}", (object) this.parseStatus(o.Arg6)) : str51 + string.Format(" and {0}", (object) this.parseStatus(o.Arg6));
          stringBuilder.Append(string.Format("{0}: {1} {2}% max HP {3}heal{4}{5}", (object) name, (object) target1, (object) o.Arg1, (object) damageThreshold5, (object) str51, (object) str52));
          break;
        case 180:
          string str53 = o.Arg1 <= 9999 || o.MinDamageThreshold <= 0 ? "" : this.parseDamageThreshold(o.MinDamageThreshold);
          stringBuilder.Append(string.Format("{0}: {1} {2} fixed HP {3}heal", (object) name, (object) target1, (object) o.Arg1, (object) str53));
          break;
        case 188:
          string str54 = o.Arg3 == 0 ? "an ability's" : "all abilities'";
          stringBuilder.Append(string.Format("{0}: {1} {2}% chance to reduce {3} uses by {4}", (object) name, (object) target1, (object) o.Arg2, (object) str54, (object) o.Arg1));
          break;
        case 189:
          str1 = this.parseUndefArgs(new int[2]
          {
            o.Arg7,
            o.Arg12
          });
          string damageCalcType13 = this.parseDamageCalcType(o.Arg4, o.Arg5);
          string element15 = this.parseElement(o.Arg2);
          string atkType23 = this.parseAtkType(o.Arg3);
          string generalStatus7 = this.parseGeneralStatus(o.Arg6, o.Arg7, o.Arg8, o.Arg9, o.Arg10, o);
          string generalStatus8 = this.parseGeneralStatus(o.Arg11, o.Arg12, o.Arg13, o.Arg14, o.Arg15, o);
          stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}{5}physical {6}damage{7}{8}", (object) name, (object) target1, (object) o.Arg1, (object) damageCalcType13, (object) atkType23, (object) damageThreshold1, (object) element15, (object) generalStatus7, (object) generalStatus8));
          break;
        case 190:
          str1 = this.parseUndefArgs(new int[2]
          {
            o.Arg7,
            o.Arg12
          });
          string damageCalcType14 = this.parseDamageCalcType(o.Arg4, o.Arg5);
          string element16 = this.parseElement(o.Arg2);
          string atkType24 = this.parseAtkType(o.Arg3);
          string generalStatus9 = this.parseGeneralStatus(o.Arg6, o.Arg7, o.Arg8, o.Arg9, o.Arg10, o);
          string generalStatus10 = this.parseGeneralStatus(o.Arg11, o.Arg12, o.Arg13, o.Arg14, o.Arg15, o);
          stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}{5}physical {6}damage{7}{8}", (object) name, (object) target1, (object) o.Arg1, (object) damageCalcType14, (object) atkType24, (object) damageThreshold1, (object) element16, (object) generalStatus9, (object) generalStatus10));
          break;
        case 192:
          str1 = this.parseUndefArgs(new int[3]
          {
            o.Arg8,
            o.Arg9,
            o.Arg10
          });
          flag1 = false;
          string str55 = o.Arg7 == 0 ? string.Format("{0}", (object) this.parseStatusFactorPhrase(o.StatusAilmentsFactor, o).Replace(", ", "")) : "auto-hit";
          string duration8 = this.parseDuration(o.Arg4);
          if ((uint) o.Arg5 > 0U)
            str7 = string.Format("{0} {1} {2}{3}", (object) str55, (object) this.parseCustomParam(o.Arg5), (object) this.parseBuffAmount(o.Arg6), (object) duration8);
          string multipleStatus7 = this.parseMultipleStatus(new int[4]
          {
            o.Arg1,
            o.Arg2,
            o.Arg3,
            o.StatusAilmentsId
          });
          if (multipleStatus7 != "")
          {
            if (str7 != "")
              str7 = string.Format("{0} and {1} {2}{3}", (object) str7, o.Arg7 == 0 ? (object) "" : (object) this.parseStatusFactorPhrase(o.StatusAilmentsFactor, o).Replace(", ", ""), (object) multipleStatus7, (object) this.parseForceInflictMulti(o.StatusAilmentsFactor, multipleStatus7, o));
            else
              str7 = this.parseStatusFactorPhrase(o.StatusAilmentsFactor, o).Replace(", ", "") + " " + multipleStatus7 + this.parseForceInflictMulti(o.StatusAilmentsFactor, multipleStatus7, o);
          }
          stringBuilder.Append(string.Format("{0}: {1} {2}", (object) name, (object) target1, (object) str7));
          break;
        case 208:
          if (o.Arg2 != 1)
            str6 = string.Format("{0}x {1}", (object) o.Arg2, (object) this.parseSameTarget(o.Arg3, o.TargetRange));
          stringBuilder.Append(string.Format("{0}: {1} {2}{3}-HP fixed {4}heal", (object) name, (object) target1, (object) str6, (object) o.Arg1, (object) damageThreshold1));
          break;
        case 211:
          str1 = this.parseUndefArgs(new int[2]
          {
            o.Arg14,
            o.Arg15
          });
          flag1 = false;
          string enemyActionStatus1 = this.parseEnemyActionStatus(o);
          string atkType25 = this.parseAtkType(o.Arg2);
          string forceHit19 = this.parseForceHit(o.Arg8);
          string multipleElements13 = this.parseMultipleElements(new int[3]
          {
            o.Arg5,
            o.Arg6,
            o.Arg7
          });
          string crit3 = this.parseCrit(o.Arg12);
          string drain4 = this.parseDrain(o.Arg13);
          string sameTarget16 = this.parseSameTarget(o.Arg4, o.TargetRange);
          string multiTarget15 = this.parseMultiTarget(o.Arg3, sameTarget16, target1);
          string damageCalcType15 = this.parseDamageCalcType(o.Arg10, o.Arg11);
          string ignoreBlink4 = this.parseIgnoreBlink(o.Arg9);
          stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}{5}{6}physical {7}damage{8}{9}{10}{11}", (object) name, (object) multiTarget15, (object) o.Arg1, (object) forceHit19, (object) atkType25, (object) damageCalcType15, (object) damageThreshold1, (object) multipleElements13, (object) drain4, (object) crit3, (object) ignoreBlink4, (object) enemyActionStatus1));
          break;
        case 212:
          str1 = this.parseUndefArgs(new int[3]
          {
            o.Arg13,
            o.Arg14,
            o.Arg15
          });
          flag1 = false;
          string enemyActionStatus2 = this.parseEnemyActionStatus(o);
          string minDmg12 = this.parseMinDmg(o.Arg2);
          string sameTarget17 = this.parseSameTarget(o.Arg4, o.TargetRange);
          string multiTarget16 = this.parseMultiTarget(o.Arg3, sameTarget17, target1);
          string multipleElements14 = this.parseMultipleElements(new int[3]
          {
            o.Arg5,
            o.Arg6,
            o.Arg7
          });
          string ignoreReflect18 = this.parseIgnoreReflect(o.Arg8, o.TargetRange, ability.ExerciseType);
          string ignoreMblink7 = this.parseIgnoreMblink(o.Arg9);
          string damageCalcType16 = this.parseDamageCalcType(o.Arg10, o.Arg11);
          string drain5 = this.parseDrain(o.Arg12);
          stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}magic {5}damage{6}{7}{8}{9}{10}", (object) name, (object) multiTarget16, (object) o.Arg1, (object) damageCalcType16, (object) damageThreshold1, (object) multipleElements14, (object) drain5, (object) ignoreReflect18, (object) ignoreMblink7, (object) minDmg12, (object) enemyActionStatus2));
          break;
        case 213:
          str1 = this.parseUndefArgs(new int[9]
          {
            o.Arg7,
            o.Arg8,
            o.Arg9,
            o.Arg10,
            o.Arg11,
            o.Arg12,
            o.Arg13,
            o.Arg14,
            o.Arg15
          });
          flag1 = false;
          string enemyActionStatus3 = this.parseEnemyActionStatus(o);
          string fracHp5 = this.parseFracHP(o.Arg2);
          string sameTarget18 = this.parseSameTarget(o.Arg4, o.TargetRange);
          string multiTarget17 = this.parseMultiTarget(o.Arg3, sameTarget18, target1);
          string ignoreReflect19 = this.parseIgnoreReflect(o.Arg5, o.TargetRange, ability.ExerciseType);
          string str56 = this.parseIgnoreBlink(o.Arg6).Equals("") ? ", ignores KO resist" : ", ignores blinks and KO resist";
          stringBuilder.Append(string.Format("{0}: {1} {2}% {3} HP {4}damage{5}{6}{7}", (object) name, (object) multiTarget17, (object) o.Arg1, (object) fracHp5, (object) damageThreshold1, (object) ignoreReflect19, (object) str56, (object) enemyActionStatus3));
          break;
        case 214:
          str1 = this.parseUndefArgs(new int[9]
          {
            o.Arg7,
            o.Arg8,
            o.Arg9,
            o.Arg10,
            o.Arg11,
            o.Arg12,
            o.Arg13,
            o.Arg14,
            o.Arg15
          });
          flag1 = false;
          string enemyActionStatus4 = this.parseEnemyActionStatus(o);
          string str57 = o.Arg2 != 0 || this.isSingleTarget(o.TargetRange) ? "" : ", divided by number of targets hit";
          string sameTarget19 = this.parseSameTarget(o.Arg4, o.TargetRange);
          string multiTarget18 = this.parseMultiTarget(o.Arg3, sameTarget19, target1);
          string ignoreReflect20 = this.parseIgnoreReflect(o.Arg5, o.TargetRange, ability.ExerciseType);
          string ignoreBlink5 = this.parseIgnoreBlink(o.Arg6);
          stringBuilder.Append(string.Format("{0}: {1} {2} {3}fixed damage{4}{5}{6}{7}", (object) name, (object) multiTarget18, (object) o.Arg1, o.Arg1 > 9999 ? (object) damageThreshold1 : (object) "", (object) str57, (object) ignoreReflect20, (object) ignoreBlink5, (object) enemyActionStatus4));
          break;
        case 215:
          str1 = this.parseUndefArgs(new int[12]
          {
            o.Arg2,
            o.Arg4,
            o.Arg5,
            o.Arg7,
            o.Arg8,
            o.Arg9,
            o.Arg10,
            o.Arg11,
            o.Arg12,
            o.Arg13,
            o.Arg14,
            o.Arg15
          });
          flag1 = false;
          string enemyActionStatus5 = this.parseEnemyActionStatus(o);
          str5 = this.parseIgnoreBlink(o.Arg3);
          stringBuilder.Append(string.Format("{0}: {1} - reduces HP to {2}{3}", (object) name, (object) target1, (object) o.Arg1, (object) enemyActionStatus5));
          if ((uint) o.MaxDamageThreshold > 0U)
          {
            stringBuilder.Append(", overflowable");
            break;
          }
          break;
        case 217:
          str1 = this.parseUndefArgs(new int[11]
          {
            o.Arg2,
            o.Arg5,
            o.Arg7,
            o.Arg8,
            o.Arg9,
            o.Arg10,
            o.Arg11,
            o.Arg12,
            o.Arg13,
            o.Arg14,
            o.Arg15
          });
          flag1 = false;
          if (o.Arg3 != 1)
            str6 = string.Format("{0}x {1}", (object) o.Arg3, (object) this.parseSameTarget(o.Arg4, o.TargetRange));
          string enemyActionStatus6 = this.parseEnemyActionStatus(o);
          string ignoreReflect21 = this.parseIgnoreReflect(o.Arg6, o.TargetRange, ability.ExerciseType);
          stringBuilder.Append(string.Format("{0}: {1} {2} factor {3} heal {4}heal{5}{6}", (object) name, (object) target1, (object) str6, (object) o.Arg1, (object) damageThreshold1, (object) enemyActionStatus6, (object) ignoreReflect21));
          break;
        case 218:
          str1 = this.parseUndefArgs(new int[11]
          {
            o.Arg2,
            o.Arg5,
            o.Arg7,
            o.Arg8,
            o.Arg9,
            o.Arg10,
            o.Arg11,
            o.Arg12,
            o.Arg13,
            o.Arg14,
            o.Arg15
          });
          flag1 = false;
          string str58 = o.Arg1 <= 9999 || o.MinDamageThreshold != 0 ? string.Concat((object) o.Arg1) : "9999";
          if (o.Arg3 != 1)
            str6 = string.Format("{0}x {1}", (object) o.Arg3, (object) this.parseSameTarget(o.Arg4, o.TargetRange));
          string enemyActionStatus7 = this.parseEnemyActionStatus(o);
          string ignoreReflect22 = this.parseIgnoreReflect(o.Arg6, o.TargetRange, ability.ExerciseType);
          stringBuilder.Append(string.Format("{0}: {1} {2}{3}-HP fixed {4}heal{5}{6}", (object) name, (object) target1, (object) str6, (object) str58, o.Arg1 > 9999 ? (object) damageThreshold1 : (object) "", (object) enemyActionStatus7, (object) ignoreReflect22));
          break;
        case 220:
          str1 = this.parseUndefArgs(new int[10]
          {
            o.Arg6,
            o.Arg7,
            o.Arg8,
            o.Arg9,
            o.Arg10,
            o.Arg11,
            o.Arg12,
            o.Arg13,
            o.Arg14,
            o.Arg15
          });
          flag1 = false;
          string enemyActionStatus8 = this.parseEnemyActionStatus(o);
          string generalStatus11 = this.parseGeneralStatus(0, o.Arg1, o.Arg2, o.Arg3, o.Arg4, o);
          if (generalStatus11.Contains("[Status bundle not found]"))
            generalStatus11 = this.parseGeneralStatus(o.Arg1, 0, o.Arg2, o.Arg3, o.Arg4, o);
          string str59 = generalStatus11.Replace(", ", "");
          string ignoreReflect23 = this.parseIgnoreReflect(o.Arg5, o.TargetRange, ability.ExerciseType);
          stringBuilder.Append(string.Format("{0}: {1} {2}{3}{4}", (object) name, (object) target1, (object) str59, (object) enemyActionStatus8, (object) ignoreReflect23));
          break;
        case 221:
          str1 = this.parseUndefArgs(new int[12]
          {
            o.Arg4,
            o.Arg5,
            o.Arg6,
            o.Arg7,
            o.Arg8,
            o.Arg9,
            o.Arg10,
            o.Arg11,
            o.Arg12,
            o.Arg13,
            o.Arg14,
            o.Arg15
          });
          flag1 = false;
          string enemyActionStatus9 = this.parseEnemyActionStatus(o);
          string ignoreReflect24 = this.parseIgnoreReflect(o.Arg3, o.TargetRange, ability.ExerciseType);
          string str60 = this.parseStatusFactor(o.Arg2, o) + "% chance to " + this.parseRemoveStatusBundle(o.Arg1).Replace("Removes", "remove");
          stringBuilder.Append(string.Format("{0}: {1} {2}{3}{4}", (object) name, (object) target1, (object) str60, (object) enemyActionStatus9, (object) ignoreReflect24));
          break;
        default:
          stringBuilder.Append(string.Format("Action ID {0} not yet implemented", (object) ability.ActionId));
          break;
      }
      if (flag1 && options1.StatusAilmentsId != 0 && this.statusFound(o.StatusAilmentsId))
        stringBuilder.Append(this.parseStatusAndFactor(o.StatusAilmentsFactor, o.StatusAilmentsId, o));
      if (options1.CounterEnable == 0 & flag1 && options1.TargetRange != 3 && options1.TargetSegment != 2)
        stringBuilder.Append(", uncounterable");
      stringBuilder.Append(str1);
      return stringBuilder.ToString();
    }

    private string AddOrdinal(uint num)
    {
      switch (num % 100U)
      {
        case 11:
        case 12:
        case 13:
          return num.ToString() + "th";
        default:
          switch (num % 10U)
          {
            case 1:
              return num.ToString() + "st";
            case 2:
              return num.ToString() + "nd";
            case 3:
              return num.ToString() + "rd";
            default:
              return num.ToString() + "th";
          }
      }
    }

    private void ConstraintSanityChecks(List<DataEnemyConstraints> constrList, uint enemyId)
    {
      foreach (List<DataEnemyConstraints> enemyConstraintsList in new List<List<DataEnemyConstraints>>()
      {
        constrList.FindAll((Predicate<DataEnemyConstraints>) (x => x.ConstraintType == 1002U && x.EnemyStatusId == 0U)).OrderBy<DataEnemyConstraints, string>((Func<DataEnemyConstraints, string>) (x => x.AbilityTag)).ToList<DataEnemyConstraints>(),
        constrList.FindAll((Predicate<DataEnemyConstraints>) (x => x.ConstraintType == 1002U && (int) x.EnemyStatusId == (int) enemyId)).OrderBy<DataEnemyConstraints, string>((Func<DataEnemyConstraints, string>) (x => x.AbilityTag)).ToList<DataEnemyConstraints>(),
        constrList.FindAll((Predicate<DataEnemyConstraints>) (x => x.ConstraintType == 1004U && x.EnemyStatusId == 0U)).OrderBy<DataEnemyConstraints, string>((Func<DataEnemyConstraints, string>) (x => x.AbilityTag)).ToList<DataEnemyConstraints>(),
        constrList.FindAll((Predicate<DataEnemyConstraints>) (x => x.ConstraintType == 1004U && (int) x.EnemyStatusId == (int) enemyId)).OrderBy<DataEnemyConstraints, string>((Func<DataEnemyConstraints, string>) (x => x.AbilityTag)).ToList<DataEnemyConstraints>(),
        constrList.FindAll((Predicate<DataEnemyConstraints>) (x => x.ConstraintType == 2003U && x.EnemyStatusId == 0U)).OrderBy<DataEnemyConstraints, string>((Func<DataEnemyConstraints, string>) (x => x.AbilityTag)).ToList<DataEnemyConstraints>(),
        constrList.FindAll((Predicate<DataEnemyConstraints>) (x => x.ConstraintType == 2003U && (int) x.EnemyStatusId == (int) enemyId)).OrderBy<DataEnemyConstraints, string>((Func<DataEnemyConstraints, string>) (x => x.AbilityTag)).ToList<DataEnemyConstraints>(),
        constrList.FindAll((Predicate<DataEnemyConstraints>) (x => x.ConstraintType == 2004U && x.EnemyStatusId == 0U)).OrderBy<DataEnemyConstraints, string>((Func<DataEnemyConstraints, string>) (x => x.AbilityTag)).ToList<DataEnemyConstraints>(),
        constrList.FindAll((Predicate<DataEnemyConstraints>) (x => x.ConstraintType == 2004U && (int) x.EnemyStatusId == (int) enemyId)).OrderBy<DataEnemyConstraints, string>((Func<DataEnemyConstraints, string>) (x => x.AbilityTag)).ToList<DataEnemyConstraints>(),
        constrList.FindAll((Predicate<DataEnemyConstraints>) (x => x.ConstraintType == 2006U && x.EnemyStatusId == 0U)).OrderBy<DataEnemyConstraints, string>((Func<DataEnemyConstraints, string>) (x => x.AbilityTag)).ToList<DataEnemyConstraints>(),
        constrList.FindAll((Predicate<DataEnemyConstraints>) (x => x.ConstraintType == 2006U && (int) x.EnemyStatusId == (int) enemyId)).OrderBy<DataEnemyConstraints, string>((Func<DataEnemyConstraints, string>) (x => x.AbilityTag)).ToList<DataEnemyConstraints>()
      })
      {
        for (int index = 0; index < enemyConstraintsList.Count - 1; ++index)
        {
          uint result1;
          uint result2;
          if (enemyConstraintsList[index].AbilityTag == enemyConstraintsList[index + 1].AbilityTag && uint.TryParse(enemyConstraintsList[index].ConstraintValue, out result1) && uint.TryParse(enemyConstraintsList[index + 1].ConstraintValue, out result2))
          {
            if (result1 < result2)
            {
              constrList.Remove(enemyConstraintsList[index + 1]);
              enemyConstraintsList.RemoveAt(index + 1);
            }
            else
            {
              constrList.Remove(enemyConstraintsList[index]);
              enemyConstraintsList.RemoveAt(index);
            }
            --index;
          }
        }
      }
      foreach (List<DataEnemyConstraints> enemyConstraintsList in new List<List<DataEnemyConstraints>>()
      {
        constrList.FindAll((Predicate<DataEnemyConstraints>) (x => x.ConstraintType == 1005U && x.EnemyStatusId == 0U)).OrderBy<DataEnemyConstraints, string>((Func<DataEnemyConstraints, string>) (x => x.AbilityTag)).ToList<DataEnemyConstraints>(),
        constrList.FindAll((Predicate<DataEnemyConstraints>) (x => x.ConstraintType == 1005U && (int) x.EnemyStatusId == (int) enemyId)).OrderBy<DataEnemyConstraints, string>((Func<DataEnemyConstraints, string>) (x => x.AbilityTag)).ToList<DataEnemyConstraints>(),
        constrList.FindAll((Predicate<DataEnemyConstraints>) (x => x.ConstraintType == 2001U && x.EnemyStatusId == 0U)).OrderBy<DataEnemyConstraints, string>((Func<DataEnemyConstraints, string>) (x => x.AbilityTag)).ToList<DataEnemyConstraints>(),
        constrList.FindAll((Predicate<DataEnemyConstraints>) (x => x.ConstraintType == 2001U && (int) x.EnemyStatusId == (int) enemyId)).OrderBy<DataEnemyConstraints, string>((Func<DataEnemyConstraints, string>) (x => x.AbilityTag)).ToList<DataEnemyConstraints>(),
        constrList.FindAll((Predicate<DataEnemyConstraints>) (x => x.ConstraintType == 2002U && x.EnemyStatusId == 0U)).OrderBy<DataEnemyConstraints, string>((Func<DataEnemyConstraints, string>) (x => x.AbilityTag)).ToList<DataEnemyConstraints>(),
        constrList.FindAll((Predicate<DataEnemyConstraints>) (x => x.ConstraintType == 2002U && (int) x.EnemyStatusId == (int) enemyId)).OrderBy<DataEnemyConstraints, string>((Func<DataEnemyConstraints, string>) (x => x.AbilityTag)).ToList<DataEnemyConstraints>(),
        constrList.FindAll((Predicate<DataEnemyConstraints>) (x => x.ConstraintType == 2005U && x.EnemyStatusId == 0U)).OrderBy<DataEnemyConstraints, string>((Func<DataEnemyConstraints, string>) (x => x.AbilityTag)).ToList<DataEnemyConstraints>(),
        constrList.FindAll((Predicate<DataEnemyConstraints>) (x => x.ConstraintType == 2005U && (int) x.EnemyStatusId == (int) enemyId)).OrderBy<DataEnemyConstraints, string>((Func<DataEnemyConstraints, string>) (x => x.AbilityTag)).ToList<DataEnemyConstraints>()
      })
      {
        for (int index = 0; index < enemyConstraintsList.Count - 1; ++index)
        {
          uint result1;
          uint result2;
          if (enemyConstraintsList[index].AbilityTag == enemyConstraintsList[index + 1].AbilityTag && uint.TryParse(enemyConstraintsList[index].ConstraintValue, out result1) && uint.TryParse(enemyConstraintsList[index + 1].ConstraintValue, out result2))
          {
            if (result1 > result2)
            {
              constrList.Remove(enemyConstraintsList[index + 1]);
              enemyConstraintsList.RemoveAt(index + 1);
            }
            else
            {
              constrList.Remove(enemyConstraintsList[index]);
              enemyConstraintsList.RemoveAt(index);
            }
            --index;
          }
        }
      }
    }

    private string parseTarget(DataEnemyAbilityOptions options)
    {
      string str = this.parseTargetHelper(options);
      if (str.Contains("STATUSEFFECT"))
      {
        if ((uint) options.StatusAilmentsId > 0U)
          str = str.Replace("STATUSEFFECT", this.parseStatus(options.StatusAilmentsId));
        else if ((uint) options.Arg1 > 0U)
        {
          try
          {
            str = str.Replace("STATUSEFFECT", Enum.GetName(typeof (SchemaConstants.StatusID), (object) options.Arg1).Replace("_", " "));
          }
          catch (NullReferenceException ex)
          {
          }
        }
        str.Replace("STATUSEFFECT", "Status Effect");
      }
      return str;
    }

    private string parseTargetHelper(DataEnemyAbilityOptions options)
    {
      int targetRange = options.TargetRange;
      int targetMethod = options.TargetMethod;
      int targetSegment = options.TargetSegment;
      if (targetRange == 1 || targetRange == 151 || targetRange == 153)
      {
        string str = "";
        if (targetRange == 151)
          str = "front-row ";
        if (targetRange == 153)
          str = "back-row ";
        if (targetSegment == 1)
        {
          int num;
          switch (targetMethod)
          {
            case 1:
              return targetRange == 1 ? "[ST - highest %HP]" : string.Format("[ST - {0}unit with highest %HP]", (object) str);
            case 2:
              return targetRange == 1 ? "[ST - lowest %HP]" : string.Format("[ST - {0}unit with lowest %HP]", (object) str);
            case 3:
              return string.Format("[ST - {0}unit with STATUSEFFECT]", (object) str);
            case 4:
              return string.Format("[ST - {0}unit without STATUSEFFECT]", (object) str);
            case 5:
              num = 1;
              break;
            default:
              num = targetMethod == 6 ? 1 : 0;
              break;
          }
          if (num != 0)
            return targetRange == 1 ? "ST" : string.Format("[ST - {0}unit]", (object) str);
          switch (targetMethod)
          {
            case 7:
              return targetRange == 1 ? "[ST - highest HP]" : string.Format("[ST - {0}unit with highest HP]", (object) str);
            case 8:
              return targetRange == 1 ? "[ST - lowest HP]" : string.Format("[ST - {0}unit with lowest HP]", (object) str);
            case 9:
              return string.Format("[ST - {0}unit with status removable by Esuna]", (object) str);
            case 10:
              return string.Format("[ST - {0}unit with status removable by Dispel]", (object) str);
            default:
              if (targetMethod == 12)
                return string.Format("[ST - {0}unit with KO, or lowest %HP]", (object) str);
              if (targetMethod == 13)
                return targetRange == 1 ? "[ST - weighted towards high HP]" : string.Format("[ST - {0}unit, weighted towards high HP]", (object) str);
              if (targetMethod == 201)
                return targetRange == 1 ? "[ST - uses Smart AI targeting]" : string.Format("[ST - targets a {0}unit using Smart AI]", (object) str);
              if (targetMethod == 202)
                return targetRange == 1 ? "[ST - uses reflect-ignoring Smart AI targeting]" : string.Format("[ST - targets a {0}unit using reflect-ignoring Smart AI]", (object) str);
              break;
          }
        }
        if (targetSegment == 2)
        {
          if (this.singleEnemy)
            return "Self";
          int num;
          switch (targetMethod)
          {
            case 1:
              return "[Ally with highest %HP]";
            case 2:
              return "[Ally with lowest %HP]";
            case 3:
              return "[Ally with STATUSEFFECT]";
            case 4:
              return "[Ally without STATUSEFFECT]";
            case 5:
              num = 1;
              break;
            default:
              num = targetMethod == 6 ? 1 : 0;
              break;
          }
          if (num != 0)
            return "[Random ally]";
          switch (targetMethod)
          {
            case 7:
              return "[Ally with highest HP]";
            case 8:
              return "[Ally with lowest HP]";
            case 9:
              return "[Ally with status removable by Esuna]";
            case 10:
              return "[Ally with status removable by Dispel]";
            default:
              if (targetMethod == 12)
                return "[Ally with KO, or lowest %HP]";
              if (targetMethod == 13)
                return "[Ally, weighted toward high HP]";
              if (targetMethod == 201)
                return "[Ally, using Smart AI]";
              if (targetMethod == 202)
                return "[Ally, using reflect-ignoring Smart AI]";
              break;
          }
        }
        if (targetSegment == 3)
        {
          int num;
          switch (targetMethod)
          {
            case 1:
              return "[Party member or enemy, with highest %HP]";
            case 2:
              return "[Party member or enemy, with lowest %HP]";
            case 3:
              return "[Party member or enemy, with STATUSEFFECT]";
            case 4:
              return "[Party member or enemy, without STATUSEFFECT]";
            case 5:
              num = 1;
              break;
            default:
              num = targetMethod == 6 ? 1 : 0;
              break;
          }
          if (num != 0)
            return "[Random party member or enemy]";
          switch (targetMethod)
          {
            case 7:
              return "[Party member or enemy, with highest HP]";
            case 8:
              return "[Party member or enemy, with lowest HP]";
            case 9:
              return "[Party member or enemy, with status removable by Esuna]";
            case 10:
              return "[Party member or enemy, with status removable by Dispel]";
            default:
              if (targetMethod == 12)
                return "[Party member or enemy, with KO, or lowest %HP]";
              if (targetMethod == 13)
                return "[Party member or enemy, weighted toward high HP]";
              if (targetMethod == 201)
                return "[Party member or enemy, using Smart AI]";
              if (targetMethod == 202)
                return "[Party member or enemy, using reflect-ignoring Smart AI]";
              break;
          }
        }
        if (targetSegment == 4)
        {
          int num;
          switch (targetMethod)
          {
            case 1:
              return "[Party member or other enemy, with highest %HP]";
            case 2:
              return "[Party member or other enemy, with lowest %HP]";
            case 3:
              return "[Party member or other enemy, with STATUSEFFECT]";
            case 4:
              return "[Party member or other enemy, without STATUSEFFECT]";
            case 5:
              num = 1;
              break;
            default:
              num = targetMethod == 6 ? 1 : 0;
              break;
          }
          if (num != 0)
            return "[Random party member or other enemy]";
          switch (targetMethod)
          {
            case 7:
              return "[Party member or other enemy, with highest HP]";
            case 8:
              return "[Party member or other enemy, with lowest HP]";
            case 9:
              return "[Party member or other enemy, with status removable by Esuna]";
            case 10:
              return "[Party member or other enemy, with status removable by Dispel]";
            default:
              if (targetMethod == 12)
                return "[Party member or other enemy, with KO, or lowest %HP]";
              if (targetMethod == 13)
                return "[Party member or other enemy, weighted toward high HP]";
              if (targetMethod == 201)
                return "[Party member or other enemy, using Smart AI]";
              if (targetMethod == 202)
                return "[Party member or other enemy, using reflect-ignoring Smart AI]";
              break;
          }
        }
        if (targetSegment == 5)
        {
          int num;
          switch (targetMethod)
          {
            case 1:
              return "[Other enemy with highest %HP]";
            case 2:
              return "[Other enemy with lowest %HP]";
            case 3:
              return "[Other enemy with STATUSEFFECT]";
            case 4:
              return "[Other enemy without STATUSEFFECT]";
            case 5:
              num = 1;
              break;
            default:
              num = targetMethod == 6 ? 1 : 0;
              break;
          }
          if (num != 0)
            return "[Random other enemy]";
          switch (targetMethod)
          {
            case 7:
              return "[Other enemy with highest HP]";
            case 8:
              return "[Other enemy with lowest HP]";
            case 9:
              return "[Other enemy with status removable by Esuna]";
            case 10:
              return "[Other enemy with status removable by Dispel]";
            default:
              if (targetMethod == 12)
                return "[Other enemy with KO, or lowest %HP]";
              if (targetMethod == 13)
                return "[Other enemy, weighted toward high HP]";
              if (targetMethod == 201)
                return "[Other enemy, using Smart AI]";
              if (targetMethod == 202)
                return "[Other enemy, using reflect-ignoring Smart AI]";
              break;
          }
        }
      }
      else
      {
        switch (targetRange)
        {
          case 2:
            switch (targetSegment)
            {
              case 1:
                return "AoE";
              case 2:
                return "[All allies]";
              case 3:
                return "[All party members and enemies]";
              case 4:
                return "[All party members and other enemies]";
              case 5:
                return "[All other enemies]";
              case 6:
                return "[Self and all party members]";
            }
            break;
          case 3:
            return "Self";
          case 102:
            return "[Two adjacent party members]";
          case 103:
            return "[Three adjacent party members]";
          case 104:
            return "[Four adjacent party members]";
          case 105:
            return "AoE";
          case 152:
            switch (targetSegment)
            {
              case 1:
                return "[Front Row]";
              case 2:
                return "[All allies]";
              case 3:
                return "[All allies and all front-row party members]";
              case 4:
                return "[All other enemies and front-row party members]";
              case 5:
                return "[All other enemies]";
              case 6:
                return "[Self and all front-row party members]";
            }
            break;
          case 154:
            switch (targetSegment)
            {
              case 1:
                return "[Back Row]";
              case 2:
                return "[All allies]";
              case 3:
                return "[All allies and back-row party members]";
              case 4:
                return "[All other enemies and back-row party members]";
              case 5:
                return "[All other enemies]";
              case 6:
                return "[Self and all back-row party members]";
            }
            break;
        }
      }
      return "[Error parsing target]";
    }

    private string parseDamageThreshold(int arg)
    {
      switch (arg)
      {
        case 0:
          return "";
        case 1:
          return "overflow ";
        case 99:
          return "overflow [up to 999999999] ";
        default:
          return "";
      }
    }

    private string parseAtkType(int arg)
    {
      if (arg == 1)
        return "";
      return arg == 2 ? "ranged " : "atkType Error ";
    }

    private string parseForceHit(int arg)
    {
      if (arg == 1)
        return "auto-hit ";
      return arg == 0 ? "" : "forceHit Error ";
    }

    private string parseMultiTarget(int arg, string sameTarget, string target) => arg != 1 && (uint) arg > 0U ? string.Format("{0}x {1}{2} attacks,", (object) arg, (object) sameTarget, (object) target) : string.Format("{0}", (object) target);

    private string damageCalculateParamLookup(int arg)
    {
      string name;
      try
      {
        name = Enum.GetName(typeof (SchemaConstants.damageCalculateParamAdjust), (object) arg);
      }
      catch (NullReferenceException ex)
      {
        return "[Param Adjust type not found]";
      }
      return name;
    }

    private string parseElement(int arg) => arg == 0 || arg == 199 ? "" : Enum.GetName(typeof (SchemaConstants.ElementID), (object) arg).ToLower() + " ";

    private string parseMultipleElements(int[] args)
    {
      List<string> stringList = new List<string>();
      foreach (int num in args)
      {
        switch (num)
        {
          case 0:
            continue;
          case 199:
            stringList.Add("NE");
            break;
          default:
            var element = Enum.GetName(typeof(SchemaConstants.ElementID), (object)num);
            if (element == null)
                element = num.ToString();
            stringList.Add(element.ToLower());
            break;
        }
      }
      string str = string.Join("/", (IEnumerable<string>) stringList);
      if (str.Equals("NE"))
        return "";
      return str.Equals("") ? str : str + " ";
    }

    private string parseStatus(int arg)
    {
      if (arg == 0)
        return "";
      string str;
      try
      {
        str = Enum.GetName(typeof (SchemaConstants.StatusID), (object) arg).Replace("_", " ");
      }
      catch (NullReferenceException ex)
      {
        return "[Status type not found]";
      }
      return str == "Doom" ? "Doom 60" : str;
    }

    private string parseMultipleStatus(int[] arg)
    {
      List<int> intList = new List<int>();
      List<string> source = new List<string>();
      string str = "";
      foreach (int key in arg)
      {
        if (key != 0 && !intList.Contains(key) && (!SchemaConstants.customParams.TryGetValue(key, out str) && (key < 828 || key > 836) && key != 535))
        {
          string status = this.parseStatus(key);
          if (!status.Equals(""))
            source.Add(status);
          intList.Add(key);
        }
      }
      if (source.Count == 0)
        return "";
      if (source.Count == 1)
        return source[0];
      if (source.Count == 2)
        return string.Format("{0} and {1}", (object) source[0], (object) source[1]);
      return source.Count >= 3 ? string.Join(", ", source.ToArray(), 0, source.Count - 1) + ", and " + source.LastOrDefault<string>() : "";
    }

    private string parseRemoveStatusBundle(int arg)
    {
      if (arg == 0)
        return "";
      string statusBundle = this.parseStatusBundle(arg);
      return arg == 2 || arg == 3 || arg == 4 || arg == 9 ? statusBundle : "Removes " + statusBundle;
    }

    private string parseStatusBundle(int arg)
    {
      string str1 = "";
      if (arg == 0)
        return str1;
      string str2;
      try
      {
        str2 = Enum.GetName(typeof (SchemaConstants.StatusAilmentsBundle), (object) arg).Replace("_", " ");
      }
      catch (NullReferenceException ex)
      {
        return "[Status bundle not found]";
      }
      return str2;
    }

    private string parseStatusOrBundle(int arg)
    {
      string str = this.parseStatusBundle(arg);
      if (str == "[Status bundle not found]")
        str = this.parseStatus(arg);
      return str;
    }

    private bool statusFound(int arg)
    {
      try
      {
        Enum.GetName(typeof (SchemaConstants.StatusID), (object) arg);
      }
      catch (NullReferenceException ex)
      {
        return false;
      }
      return true;
    }

    private string parseStatusFactor(int arg, DataEnemyAbilityOptions o)
    {
      if (arg >= 998)
        return "auto-hit";
      return o.TargetRange == 3 || o.TargetSegment == 2 ? arg.ToString() : (arg * 3 + 3).ToString();
    }

    private string parseStatusFactorPhrase(int arg, DataEnemyAbilityOptions o)
    {
      if (arg >= 998)
        return ", auto-hit";
      return o.TargetRange == 3 || o.TargetSegment == 2 ? string.Format(", {0}% chance to apply", (object) arg) : string.Format(", {0}% chance to apply", (object) (arg * 3 + 3));
    }

    private string parseForceInflict(int rate, int status, DataEnemyAbilityOptions o) => rate == 998 && o.TargetRange != 3 && o.TargetSegment != 2 && (status == 200 || status == 201 || (status == 202 || status == 203) || (status == 205 || status == 206 || (status == 210 || status == 211)) || (status == 212 || status == 215 || (status == 229 || status == 242)) || status == 292) ? " (blockable by Astra)" : "";

    private string parseForceInflictMulti(int rate, string statuses, DataEnemyAbilityOptions o)
    {
      string lower = statuses.ToLower();
      return rate == 998 && o.TargetRange != 3 && o.TargetSegment != 2 && (lower.Contains("poison") || lower.Contains("silence") || (lower.Contains("paralyze") || lower.Contains("confuse")) || (lower.Contains("slow") || lower.Contains("stop") || (lower.Contains("blind") || lower.Contains("sleep"))) || (lower.Contains("sap") || lower.Contains("stun") || (lower.Contains("frozen") || lower.Contains("petrify"))) || lower.Contains("berserk")) ? " (blockable by Astra)" : "";
    }

    private string parseForceInflictBundle(int rate, int bundle, DataEnemyAbilityOptions o)
    {
      string lower = this.parseStatusBundle(bundle).ToLower();
      return rate == 998 && o.TargetRange != 3 && o.TargetSegment != 2 && (lower.Contains("poison") || lower.Contains("silence") || (lower.Contains("paralyze") || lower.Contains("paralysis")) || (lower.Contains("confuse") || lower.Contains("confusion") || (lower.Contains("slow") || lower.Contains("stop"))) || (lower.Contains("blind") || lower.Contains("blinded") || (lower.Contains("sleep") || lower.Contains("petrify")) || (lower.Contains("petrifaction") || lower.Contains("berserk") || (lower.Contains("berserker") || lower.Contains("sap")))) || (lower.Contains("stun") || lower.Contains("stan")) || lower.Contains("frozen")) ? " (blockable by Astra)" : "";
    }

    private string parseStatusAndFactor(int factor, int status, DataEnemyAbilityOptions o) => factor == 0 || status == 0 ? "" : string.Format("{0} {1}{2}", (object) this.parseStatusFactorPhrase(factor, o), (object) this.parseStatus(status), (object) this.parseForceInflict(factor, status, o));

    private string parseStatusOrBundleAndFactor(int factor, int id, DataEnemyAbilityOptions o)
    {
      if (factor == 0 || id == 0)
        return "";
      return this.parseStatusBundle(id) == "[Status bundle not found]" ? string.Format("{0} {1}{2}", (object) this.parseStatusFactorPhrase(factor, o), (object) this.parseStatus(id), (object) this.parseForceInflict(factor, id, o)) : string.Format("{0} {1}{2}", (object) this.parseStatusFactorPhrase(factor, o), (object) this.parseStatusBundle(id), (object) this.parseForceInflictBundle(factor, id, o));
    }

    private string parseCustomParam(int arg)
    {
      if (arg == 0)
        return "";
      string str = "";
      return SchemaConstants.customParams.TryGetValue(arg, out str) ? str : "[CustomParam not defined]";
    }

    private string parseGeneralStatus(
      int id,
      int bundleid,
      int factor,
      int boostrate,
      int duration,
      DataEnemyAbilityOptions o)
    {
      return this.parseGeneralStatus(id, bundleid, factor, boostrate, duration, 0, o);
    }

    private string parseGeneralStatus(
      int id,
      int bundleid,
      int factor,
      int boostrate,
      int duration,
      int forcehit,
      DataEnemyAbilityOptions o)
    {
      string str = "";
      if ((uint) id > 0U)
      {
        if (SchemaConstants.customParams.TryGetValue(id, out str))
        {
          string customParam = this.parseCustomParam(id);
          string buffAmount = this.parseBuffAmount(boostrate);
          string duration1 = this.parseDuration(duration);
          if (forcehit == 0)
            str = string.Format("{0} {1} {2}{3}", (object) this.parseStatusFactorPhrase(factor, o), (object) customParam, (object) buffAmount, (object) duration1);
          else
            str = string.Format(", auto-hit {0} {1}{2}", (object) customParam, (object) buffAmount, (object) duration1);
        }
        else
          str = (id < 828 || id > 836) && (o.StatusAilmentsId != 535 || id < 100 || id > 108) ? (forcehit != 0 ? string.Format(", auto-hit {0}", (object) this.parseStatus(id)) : string.Format("{0} {1}{2}", (object) this.parseStatusFactorPhrase(factor, o), (object) this.parseStatus(id), (object) this.parseForceInflict(factor, id, o))) : this.parseVariableImperil(id, factor, duration, boostrate, o);
      }
      if ((uint) bundleid > 0U)
      {
        string statusBundle = this.parseStatusBundle(bundleid);
        this.parseStatusFactor(factor, o);
        str = forcehit != 0 ? str + string.Format(", auto-hit {0}", (object) statusBundle) : str + string.Format("{0} {1}{2}", (object) this.parseStatusFactorPhrase(factor, o), (object) statusBundle, (object) this.parseForceInflictBundle(factor, id, o));
      }
      return str;
    }

    private string parseGeneralStatusOrBundle(
      int id,
      int factor,
      int boostrate,
      int duration,
      int forcehit,
      DataEnemyAbilityOptions o)
    {
      string str1 = "";
      if (id == 0 || factor == 0)
        return str1;
      string statusBundle = this.parseStatusBundle(id);
      string str2;
      if (statusBundle != "[Status bundle not found]")
      {
        this.parseStatusFactor(factor, o);
        str2 = forcehit != 0 ? str1 + string.Format(", auto-hit {0}", (object) statusBundle) : str1 + string.Format("{0} {1}{2}", (object) this.parseStatusFactorPhrase(factor, o), (object) statusBundle, (object) this.parseForceInflictBundle(factor, id, o));
      }
      else if (SchemaConstants.customParams.TryGetValue(id, out str1))
      {
        string customParam = this.parseCustomParam(id);
        string buffAmount = this.parseBuffAmount(boostrate);
        string duration1 = this.parseDuration(duration);
        if (forcehit == 0)
          str2 = string.Format("{0} {1} {2}{3}", (object) this.parseStatusFactorPhrase(factor, o), (object) customParam, (object) buffAmount, (object) duration1);
        else
          str2 = string.Format(", auto-hit {0} {1}{2}", (object) customParam, (object) buffAmount, (object) duration1);
      }
      else
        str2 = (id < 828 || id > 836) && (o.StatusAilmentsId != 535 || id < 100 || id > 108) ? (forcehit != 0 ? string.Format(", auto-hit {0}", (object) this.parseStatus(id)) : string.Format("{0} {1}{2}", (object) this.parseStatusFactorPhrase(factor, o), (object) this.parseStatus(id), (object) this.parseForceInflict(factor, id, o))) : this.parseVariableImperil(id, factor, duration, boostrate, o);
      return str2;
    }

    private string parseEnemyActionStatus(DataEnemyAbilityOptions o)
    {
      StringBuilder stringBuilder = new StringBuilder();
      bool flag = false;
      string str1 = this.parseGeneralStatusOrBundle(o.Arg21, o.Arg22, o.Arg23, o.Arg24, 0, o);
      if ((uint) o.Arg25 > 0U)
        str1 = str1.Replace(", ", ", self ");
      else if (this.parseIgnoreAstra(o.Arg26, o.TargetSegment, o.Arg21) != "")
        flag = true;
      string str2 = this.parseGeneralStatusOrBundle(o.Arg16, o.Arg17, o.Arg18, o.Arg19, 0, o);
      if ((uint) o.Arg20 > 0U)
        str2 = str2.Replace(", ", ", self ");
      else if (!flag && this.parseIgnoreAstra(o.Arg26, o.TargetSegment, o.Arg16) != "")
        flag = true;
      if ((uint) o.Arg25 > 0U == (uint) o.Arg20 > 0U && o.Arg21 == o.Arg16)
        str2 = "";
      string str3 = this.parseStatusOrBundleAndFactor(o.StatusAilmentsFactor, o.StatusAilmentsId, o);
      if (!flag && this.parseIgnoreAstra(o.Arg26, o.TargetSegment, o.StatusAilmentsId) != "")
        flag = true;
      if (o.Arg20 == 0 && o.Arg16 == o.StatusAilmentsId || o.Arg25 == 0 && o.Arg21 == o.StatusAilmentsId)
        str3 = "";
      if (str2 != "" && str1 != "" && (uint) o.Arg25 > 0U == (uint) o.Arg20 > 0U && o.Arg22 == o.Arg17)
      {
        str1 = str1.Replace(this.parseStatusFactorPhrase(o.Arg22, o), " and");
        if (str3 != "" && o.Arg20 == 0 && o.Arg17 == o.StatusAilmentsFactor)
          str2 = str2.Replace(this.parseStatusFactorPhrase(o.Arg17, o), ", ");
      }
      else if (str3 != "" && str2 != "" && o.Arg20 == 0 && o.Arg17 == o.StatusAilmentsFactor)
        str2 = str2.Replace(this.parseStatusFactorPhrase(o.Arg17, o), " and");
      string str4 = "";
      if (o.Arg27 != 0 && (uint) o.Arg28 > 0U)
      {
        string statusOrBundle = this.parseStatusOrBundle(o.Arg27);
        str4 = (uint) o.Arg29 <= 0U ? string.Format(", {0}% chance to remove {1}", (object) this.parseStatusFactor(o.Arg28, o), (object) statusOrBundle) : string.Format(", {0}% chance to remove {1} from self", (object) o.Arg28, (object) statusOrBundle);
      }
      stringBuilder.Append(str3);
      if (o.Arg20 == 0)
        stringBuilder.Append(str2);
      if (o.Arg25 == 0)
        stringBuilder.Append(str1);
      if (flag)
        stringBuilder.Append(", ignores Astra");
      if (o.Arg29 == 0)
        stringBuilder.Append(str4);
      if ((uint) o.Arg20 > 0U)
        stringBuilder.Append(str2);
      if ((uint) o.Arg25 > 0U)
        stringBuilder.Append(str1);
      if ((uint) o.Arg29 > 0U)
        stringBuilder.Append(str4);
      return stringBuilder.ToString();
    }

    private string parseVariableImperil(
      int id,
      int factor,
      int duration,
      int rate,
      DataEnemyAbilityOptions o)
    {
      return this.parseVariableImperil(id, factor, duration, rate, 0, o);
    }

    private string parseVariableImperil(
      int id,
      int factor,
      int duration,
      int rate,
      int forcehit,
      DataEnemyAbilityOptions o)
    {
      int num1 = 0;
      num1 = o.TargetRange != 3 && o.TargetSegment != 2 ? rate * 3 + 3 : rate;
      if (factor == 0)
        return "";
      string str = "";
      if (id >= 828 && id <= 836)
      {
        if (SchemaConstants.imperilX.TryGetValue(id, out str))
        {
          string duration1 = this.parseDuration(duration == 0 ? 25000 : duration);
          int num2 = -10 * factor;
          if (forcehit != 0)
            return string.Format(", auto-hit {0} {1}%{2}", (object) str, (object) num2, (object) duration1);
          return string.Format("{0} {1} {2}%{3}", (object) this.parseStatusFactorPhrase(rate, o), (object) str, (object) num2, (object) duration1);
        }
      }
      else if (o.StatusAilmentsId == 535 && id >= 100 && id <= 108)
      {
        string name = Enum.GetName(typeof (SchemaConstants.ElementID), (object) id);
        string duration1 = this.parseDuration(duration == 0 ? 25000 : duration);
        int num2 = -10 * factor;
        if (forcehit != 0)
          return string.Format(", auto-hit {0} {1}%{2}", (object) name, (object) num2, (object) duration1);
        return string.Format("{0} Imperil {1} {2}%{3}", (object) this.parseStatusFactorPhrase(rate, o), (object) name, (object) num2, (object) duration1);
      }
      return "";
    }

    private string parseDamageCalcType(int type, int expFactor)
    {
      int num;
      switch (type)
      {
        case 0:
          return "";
        case 1:
          num = (uint) expFactor > 0U ? 1 : 0;
          break;
        default:
          num = 0;
          break;
      }
      if (num != 0)
        return string.Format("piercing ^{0} ", (object) ((Decimal) expFactor / 100M).ToString("0.##"));
      return type == 2 ? "buff-ignoring " : "";
    }

    private string parseFracHP(int useMax) => useMax == 0 ? "current" : "max";

    private bool isSingleTarget(int range) => range == 1 || range == 151 || range == 153;

    private string parseDuration(int arg) => arg > 0 ? string.Format(" for {0} seconds", (object) ((Decimal) arg / 1000M).ToString("0.###")) : "";

    private string parseIgnoreAstra(int arg, int segment, int statusID)
    {
      if (arg == 0 || (segment == 2 || segment == 5 || segment == 7))
        return "";
      int num;
      if (arg > 0)
        num = ((IEnumerable<int>) new int[13]
        {
          200,
          201,
          202,
          203,
          205,
          206,
          210,
          211,
          212,
          215,
          229,
          242,
          292
        }).Contains<int>(statusID) ? 1 : 0;
      else
        num = 0;
      return num != 0 ? ", ignores Astra" : "";
    }

    private string parseIgnoreReflect(int arg, int range, uint exType) => arg > 0 && this.isSingleTarget(range) && (exType == 3U || exType == 4U) ? ", ignores Reflect" : "";

    private string parseIgnoreMblink(int arg) => arg == 0 ? "" : ", ignores Magic Blink";

    private string parseIgnoreBlink(int arg) => arg == 0 ? "" : ", ignores Blink";

    private string parseCrit(int arg) => arg > 0 ? string.Format(", with a +{0}% chance to crit", (object) arg) : "";

    private string parseCritDamage(int arg)
    {
      Decimal num = (Decimal) arg;
      return arg > 0 ? string.Format(", with a crit damage multiplier of {0}x", (object) (num / 10M).ToString("0.#")) : "";
    }

    private string parseMinDmg(int arg) => arg <= 1 ? "" : string.Format(" [minimum damage: {0}]", (object) arg);

    private string parseDrain(int arg) => arg == 0 ? "" : string.Format(" with {0}% HP drain", (object) arg);

    private string parseExpFactor(int arg) => "^" + ((Decimal) arg / 100M).ToString("0.##");

    private string parseBuffAmount(int arg) => arg == 0 ? "" : arg.ToString("+0;-#") + "%";

    private string parseUndefArgs(int[] args)
    {
      foreach (uint num in args)
      {
        if (num > 0U)
          return " [One or more args not parsed]";
      }
      return "";
    }

    private string parseSameTarget(int arg, int range)
    {
      if (range != 1 && range != 151 && range != 153)
        return "";
      return arg == 0 ? "independently-targeted " : "same-target ";
    }

    private string parseSameTarget(int arg, bool isSingleTarget)
    {
      if (!isSingleTarget)
        return "";
      return arg == 0 ? "independently-targeted " : "same-target ";
    }

    private string parseBarterRate(int arg) => arg == 0 ? "" : string.Format(", damages the user for {0}% of Max HP", (object) ((Decimal) arg / 10M).ToString("0.#"));

    public string translateAbility(string name)
    {
      string str1 = name;
      string str2 = "";
      if (str1.Contains<char>('】'))
      {
        str2 = name.Substring(0, name.LastIndexOf('】') + 1);
        str1 = str1.Substring(str1.LastIndexOf('】') + 1);
      }
      if (SchemaConstants.abilityNames.ContainsKey(str1))
        return str2 + SchemaConstants.abilityNames[str1];
      if (new Regex("[0-9]+$").Match(str1).Success)
      {
        string str3 = Regex.Match(str1, "[0-9]+", RegexOptions.RightToLeft).ToString();
        string key = str1.Substring(0, str1.IndexOf(str3));
        if (SchemaConstants.abilityNames.ContainsKey(key))
          return str2 + SchemaConstants.abilityNames[key] + str3;
      }
      return name;
    }
  }
}
