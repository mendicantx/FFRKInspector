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
            singleEnemy = single;
        }

        public string parseAbility(DataEnemyParamAbilities paramAbility, List<DataEnemyConstraints> constraints, BasicEnemyInfo enemy, EnemyAbilityParserOptions parseOpt)
        {
            return parseAbility(paramAbility, constraints, enemy, parseOpt, true, 0, 0);
        }

        public string parseAbility(DataEnemyParamAbilities paramAbility, List<DataEnemyConstraints> constraints, BasicEnemyInfo enemy, EnemyAbilityParserOptions parseOpt,
            bool include1001, int enumeratedTurn)
        {
            return parseAbility(paramAbility, constraints, enemy, parseOpt, include1001, enumeratedTurn, 0);
        }

        public string parseAbility(DataEnemyParamAbilities paramAbility, List<DataEnemyConstraints> constraints, BasicEnemyInfo enemy, EnemyAbilityParserOptions parseOpt,
            bool include1001, int enumeratedTurn, int turnPeriod)
        {
            var list1 = constraints.OrderBy(x => x.ConstraintValue, new semiNumericComparer())
                .OrderBy(x => x.ConstraintType).ToList();
            ConstraintSanityChecks(list1, enemy.EnemyId);
            var ability = battle.Battle.getAbility(paramAbility.AbilityId);
            var abilityStringBuilder = new StringBuilder();
            var num1 = paramAbility.Weight;
            var flag1 = false;
            if (paramAbility.Tag.Equals(""))
                flag1 = true;
            else
                foreach (var enemyAbility in enemy.EnemyAbilities)
                    if (enemyAbility.Tag.Equals(paramAbility.Tag))
                    {
                        flag1 = true;
                        break;
                    }

            if (!flag1)
                num1 = 0U;
            if (enumeratedTurn > 0)
            {
                abilityStringBuilder.Append(string.Format("Turn {0}{1}", enumeratedTurn,
                    turnPeriod > 0 ? string.Format(" + {0}n", turnPeriod) : (object) ""));
            }
            else if (num1 > 0U)
            {
                if (parseOpt.displayFractions)
                {
                    abilityStringBuilder.Append(string.Format("{0}/{1} chance", paramAbility.Weight.ToString("N0"),
                        totalWeight.ToString("N0")));
                }
                else
                {
                    var num2 = Convert.ToDecimal(100.0 * paramAbility.Weight / totalWeight);
                    abilityStringBuilder.Append(num2.ToString("0.##") + "% chance");
                }

                if (paramAbility.UnlockTurn > 0U)
                    abilityStringBuilder.Append(string.Format(", unlocks on {0} ATB", AddOrdinal(paramAbility.UnlockTurn)));
            }
            else
            {
                abilityStringBuilder.Append("Special");
            }

            var str1 = parseOpt.translateAbilityNames ? translateAbility(ability.name) : ability.name;
            abilityStringBuilder.Append(": " + str1);
            if (parseOpt.displayCastTimes)
            {
                decimal castTime = ability.Options.CastTime;
                abilityStringBuilder.Append(" - " + (castTime / 1000M).ToString("0.###") + "s CT");
            }

            abilityStringBuilder.Append(" (");
            var input = parseAbilityEffects(ability);
            input = SetAbilityTargets(enemy, enumeratedTurn, ability, input);

            abilityStringBuilder.Append(input);
            abilityStringBuilder.Append(")");
            var flag2 = false;
            var source1 = new List<string>();
            var source2 = new List<string>();
            foreach (var enemyConstraints in list1)
                if ((enemyConstraints.EnemyStatusId == 0U ||
                     (int) enemyConstraints.EnemyStatusId == (int) enemy.EnemyId) &&
                    enemyConstraints.AbilityTag.Equals(paramAbility.Tag))
                {
                    var str2 = enemyConstraints.EnemyStatusId == 0U ? "global" : "local";
                    if (enemyConstraints.ConstraintType != 1001U || include1001 || enemyConstraints.EnemyStatusId == 0U)
                        flag2 = true;
                    uint result = 0;
                    uint.TryParse(enemyConstraints.ConstraintValue, out result);
                    var str3 = result == 1U ? "" : "s";
                    switch (enemyConstraints.ConstraintType)
                    {
                        case 1001:
                            if (include1001 || enemyConstraints.EnemyStatusId == 0U)
                                source1.Add(string.Format("on {0} turn {1}", str2, result));
                            break;
                        case 1002:
                            source1.Add(string.Format("on {0} turn after last use", AddOrdinal(result)));
                            break;
                        case 1003:
                            var name = battle.Battle
                                .getAbility(enemy.getAbilityByTag(enemyConstraints.ConstraintValue).AbilityId).name;
                            source1.Add(string.Format("after {0} is used",
                                parseOpt.translateAbilityNames ? translateAbility(name) : (object) name));
                            break;
                        case 1004:
                            source1.Add(string.Format("on {0} turn {1} if not yet used", str2, result));
                            break;
                        case 1005:
                            source1.Add(string.Format("when below {0}% HP if not yet used", (uint) ((int) result + 1)));
                            break;
                        case 2001:
                            source2.Add(string.Format("until {0} turn {1}", str2, result));
                            break;
                        case 2002:
                            source2.Add(string.Format("for {0} turn{1} after use", (uint) ((int) result - 1),
                                (int) result - 1 == 1 ? "" : (object) "s"));
                            break;
                        case 2003:
                            source2.Add(string.Format("after being used {0} time{1}", result, str3));
                            break;
                        case 2004:
                            source2.Add(string.Format("when above {0}% HP", result));
                            break;
                        case 2005:
                            source2.Add(string.Format("when below {0}% HP", (uint) ((int) result + 1)));
                            break;
                        case 2006:
                            source2.Add(string.Format("after {0} turn {1}", str2, result));
                            break;
                        default:
                            source1.Add("Error: constraint type not found");
                            break;
                    }
                }

            if (flag2 && enumeratedTurn == 0)
            {
                abilityStringBuilder.Append(" [");
                if (source1.Count >= 1)
                {
                    abilityStringBuilder.Append("Forced ");
                    if (source1.Count == 1)
                        abilityStringBuilder.Append(source1[0]);
                    else
                        abilityStringBuilder.Append(string.Join(", ", source1.ToArray(), 0, source1.Count - 1) + ", and " +
                                             source1.LastOrDefault());
                    if (source2.Count >= 1)
                        abilityStringBuilder.Append(". ");
                }

                if (source2.Count >= 1)
                {
                    abilityStringBuilder.Append("Locked ");
                    if (source2.Count == 1)
                        abilityStringBuilder.Append(source2[0]);
                    else
                        abilityStringBuilder.Append(string.Join(", ", source2.ToArray(), 0, source2.Count - 1) + ", and " +
                                             source2.LastOrDefault());
                }

                abilityStringBuilder.Append("]");
            }

            AddSavageIncreases(abilityStringBuilder, enemy, enumeratedTurn);

            return abilityStringBuilder.ToString();
        }

        private void AddSavageIncreases(StringBuilder abilityStringBuilder, BasicEnemyInfo enemy, int enumeratedTurn)
        {
            if (enumeratedTurn > 0 &&
                (enemy.EnemyParentInfo.Phases.Count() >= 4 || enemy.EnemyParentInfo.ChildPosId == 1U) &&
                enemy.EnemyParentInfo.AiArgs.Any(x => x.Tag.Contains("added_mad_lv_with_turn_condition_of_phase_")))
            {
                var myPhase = enemy.EnemyId % 10U;

                var enemyRageIncreases = enemy.EnemyParentInfo.AiArgs
                    .FirstOrDefault(aiarg => aiarg.Tag == "added_mad_lv_with_turn_condition_of_phase_" + myPhase).ArgValue
                    .Split(
                        new string[1]
                        {
                            "\n"
                        }, StringSplitOptions.None).ToList();

                var turnMatchingPattern = new Regex(string.Format("^{0}:", enumeratedTurn));
                var turnTargets = enemyRageIncreases.FirstOrDefault(x => turnMatchingPattern.Match(x).Success);
                
                if (turnTargets != null)
                {
                    var splitArg = turnTargets.Split(new string[] {":"}, StringSplitOptions.None);

                    if (splitArg.Length >= 2)
                        abilityStringBuilder.Append("\nSavage Level+" + splitArg[1]);
                }
            }
        }

        private string SetAbilityTargets(BasicEnemyInfo enemy, int enumeratedTurn, DataEnemyAbility ability, string input)
        {
            if (enumeratedTurn > 0 &&
                (enemy.EnemyParentInfo.Phases.Count() >= 4 || enemy.EnemyParentInfo.ChildPosId == 1U) &&
                enemy.EnemyParentInfo.AiArgs.Any(x => x.Tag.Contains("ABILITY_TARGET_MAP_TURN_STATUS_NO_")))
            {
                var target = parseTarget(ability.Options);
                var myPhase = enemy.EnemyId % 10U;
                var enemyTargets = enemy.EnemyParentInfo.AiArgs
                    .FirstOrDefault(aiarg => aiarg.Tag == "ABILITY_TARGET_MAP_TURN_STATUS_NO_" + myPhase).ArgValue.Split(
                        new string[1]
                        {
                            "\n"
                        }, StringSplitOptions.None).ToList();
                var turnMatchingPattern = new Regex(string.Format("^{0}:", enumeratedTurn));
                var turnTargets = enemyTargets.FirstOrDefault(x => turnMatchingPattern.Match(x).Success);
                if (turnTargets != null)
                {
                    var hitSlots = turnTargets.Replace(enumeratedTurn + ":", "");
                    hitSlots = hitSlots.Replace("buddy(", "").Replace(")", "");

                    var strArray = hitSlots.Split(',');
                    strArray[0] = strArray[0].Replace(" ", "");
                    var replacement = "";
                    if (strArray.Length == 5)
                    {
                        replacement = "AoE";
                    }
                    else if (strArray.Length == 1)
                    {
                        replacement = string.Format("Slot [{0}]", strArray[0]);
                    }
                    else if (strArray.Length >= 2)
                    {
                        replacement = string.Format("Slots [{0}]", string.Join("-", strArray));
                    }

                    if (replacement != "")
                        input = new Regex(Regex.Escape(target)).Replace(input, replacement, 1);
                }
            }

            return input;
        }

        public string parseCounter(DataEnemyParamCounters counter, EnemyAbilityParserOptions parseOpt)
        {
            var ability = battle.Battle.getAbility(counter.AbilityId);
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(string.Format("{0}% chance to counter ", counter.Rate));
            switch (counter.CondType)
            {
                case 1:
                    stringBuilder.Append("any attack ");
                    break;
                case 2:
                    var name1 = Enum.GetName(typeof(SchemaConstants.ElementID), counter.CondValue);
                    stringBuilder.Append(string.Format("{0}-element attacks ", name1));
                    break;
                case 3:
                    var name2 = Enum.GetName(typeof(SchemaConstants.ExerciseAbbr), counter.CondValue);
                    stringBuilder.Append(string.Format("{0} attacks ", name2));
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
                    var str1 = Enum.GetName(typeof(SchemaConstants.AbilityCategoryID), counter.CondValue)
                        .Replace('_', ' ');
                    stringBuilder.Append(string.Format("{0} attacks ", str1));
                    break;
                case 6:
                    var str2 = Enum.GetName(typeof(SchemaConstants.StatusID), counter.CondValue).Replace('_', ' ');
                    stringBuilder.Append(string.Format("{0}-inflicting attacks ", str2));
                    break;
                default:
                    stringBuilder.Append("Counter condition type error ");
                    break;
            }

            stringBuilder.Append(string.Format("with: {0} (",
                parseOpt.translateAbilityNames ? translateAbility(ability.name) : (object) ability.name));
            stringBuilder.Append(parseAbilityEffects(ability));
            stringBuilder.Append(")");
            return stringBuilder.ToString();
        }

        private string parseAbilityEffects(DataEnemyAbility ability)
        {
            var options1 = ability.Options;
            var o = options1;
            var stringBuilder = new StringBuilder();
            var name = Enum.GetName(typeof(SchemaConstants.ExerciseAbbr), ability.ExerciseType);
            var target1 = parseTarget(options1);
            var damageThreshold1 = parseDamageThreshold(o.MaxDamageThreshold);
            var str1 = "";
            var str2 = "";
            var str3 = "";
            var str4 = "";
            var str5 = "";
            var str6 = "";
            var str7 = "";
            var str8 = "";
            var source = new List<string>();
            var stringList = new List<string>();
            var options2 = new DataEnemyAbilityOptions();
            var flag1 = true;
            switch (ability.ActionId)
            {
                case 1:
                    str1 = parseUndefArgs(new int[3]
                    {
                        o.Arg9,
                        o.Arg10,
                        o.Arg11
                    });
                    var barterRate = parseBarterRate(o.Arg2);
                    var multipleElements1 = parseMultipleElements(new int[2]
                    {
                        o.Arg3,
                        o.Arg8
                    });
                    var atkType1 = parseAtkType(o.Arg4);
                    var forceHit1 = parseForceHit(o.Arg5);
                    var sameTarget1 = parseSameTarget(o.Arg7, o.TargetRange);
                    if (o.Arg6 == 1)
                    {
                        stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}{5}physical {6}damage{7}", name,
                            target1, o.Arg1, forceHit1, atkType1, damageThreshold1, multipleElements1, barterRate));
                        break;
                    }

                    if (o.Arg6 > 1)
                        stringBuilder.Append(string.Format(
                            "{0}: {1}x {2}{3} attacks, {4}% {5}{6}{7}physical {8}damage{9}", name, o.Arg6, sameTarget1,
                            target1, o.Arg1, forceHit1, atkType1, damageThreshold1, multipleElements1, barterRate));
                    break;
                case 3:
                    var atkType2 = parseAtkType(o.Arg2);
                    var forceHit2 = parseForceHit(o.Arg3);
                    stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}{5}physical damage", name, target1, o.Arg1,
                        forceHit2, atkType2, damageThreshold1));
                    if ((uint) o.Arg4 > 0U)
                        stringBuilder.Append(string.Format(" [damage calculation modified using \"{0}\" param adjust]",
                            damageCalculateParamLookup(o.Arg4)));
                    break;
                case 4:
                    var atkType3 = parseAtkType(o.Arg3);
                    var forceHit3 = parseForceHit(o.Arg4);
                    var drain1 = parseDrain(o.Arg2);
                    stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}{5}physical damage{6}", name, target1,
                        o.Arg1, forceHit3, atkType3, damageThreshold1, drain1));
                    break;
                case 5:
                    var element1 = parseElement(o.Arg2);
                    var ignoreReflect1 = parseIgnoreReflect(o.Arg4, o.TargetRange, ability.ExerciseType);
                    var minDmg1 = parseMinDmg(o.Arg3);
                    stringBuilder.Append(string.Format("{0}: {1} {2}% {3}magic {4}damage{5}{6}", name, target1, o.Arg1,
                        damageThreshold1, element1, minDmg1, ignoreReflect1));
                    break;
                case 7:
                    str1 = parseUndefArgs(new int[6]
                    {
                        o.Arg9,
                        o.Arg10,
                        o.Arg11,
                        o.Arg12,
                        o.Arg13,
                        o.Arg15
                    });
                    var atkType4 = parseAtkType(o.Arg3);
                    var forceHit4 = parseForceHit(o.Arg4);
                    var element2 = parseElement(o.Arg5);
                    var crit1 = parseCrit(o.Arg7);
                    var sameTarget2 = parseSameTarget(o.Arg6, o.TargetRange);
                    var multiTarget1 = parseMultiTarget(o.Arg2, sameTarget2, target1);
                    var damageCalcType1 = parseDamageCalcType(o.Arg14, o.Arg16);
                    var ignoreBlink1 = parseIgnoreBlink(o.Arg17);
                    stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}{5}{6}physical {7}damage{8}{9}", name,
                        multiTarget1, o.Arg1, forceHit4, atkType4, damageCalcType1, damageThreshold1, element2, crit1,
                        ignoreBlink1));
                    if ((uint) o.Arg8 > 0U)
                        stringBuilder.Append(string.Format(" [damage calculation modified using \"{0}\" param adjust]",
                            damageCalculateParamLookup(o.Arg8)));
                    break;
                case 8:
                    flag1 = false;
                    var removeStatusBundle1 = parseRemoveStatusBundle(o.Arg1);
                    var ignoreReflect2 = parseIgnoreReflect(o.Arg2, o.TargetRange, ability.ExerciseType);
                    stringBuilder.Append(string.Format("{0}: {1} - {2}{3}", name, target1, removeStatusBundle1,
                        ignoreReflect2));
                    break;
                case 11:
                    var ignoreReflect3 = parseIgnoreReflect(o.Arg3, o.TargetRange, ability.ExerciseType);
                    stringBuilder.Append(string.Format("{0}: {1} - {2}% chance to Raise with {3}% HP{4}", name, target1,
                        o.Arg1, o.Arg2, ignoreReflect3));
                    break;
                case 12:
                    str1 = parseUndefArgs(new int[1] {o.Arg3});
                    var damageThreshold2 = parseDamageThreshold(o.MinDamageThreshold);
                    var ignoreReflect4 = parseIgnoreReflect(o.Arg4, o.TargetRange, ability.ExerciseType);
                    stringBuilder.Append(string.Format("{0}: {1} - Factor {2} {3}heal{4}", name, target1, o.Arg1,
                        damageThreshold2, ignoreReflect4));
                    break;
                case 14:
                    str1 = parseUndefArgs(new int[1] {o.Arg7});
                    flag1 = false;
                    var multipleStatus1 = parseMultipleStatus(new int[5]
                    {
                        o.Arg1,
                        o.Arg2,
                        o.Arg3,
                        o.Arg4,
                        o.Arg5
                    });
                    var str9 = parseStatusFactorPhrase(o.StatusAilmentsFactor, o).Replace(", ", "");
                    var duration1 = parseDuration(o.Arg6);
                    var ignoreAstra =
                        parseIgnoreAstra(o.Arg8, o.TargetSegment, o.Arg1 == 0 ? o.StatusAilmentsId : o.Arg1);
                    var ignoreReflect5 = parseIgnoreReflect(o.Arg9, o.TargetRange, ability.ExerciseType);
                    stringBuilder.Append(string.Format("{0}: {1} {2} {3}{4}{5}{6}{7}", name, target1, str9,
                        multipleStatus1, duration1, parseForceInflictMulti(o.StatusAilmentsFactor, multipleStatus1, o),
                        ignoreAstra, ignoreReflect5));
                    break;
                case 15:
                    flag1 = false;
                    var multipleStatus2 = parseMultipleStatus(new int[5]
                    {
                        o.Arg1,
                        o.Arg2,
                        o.Arg3,
                        o.Arg4,
                        o.Arg5
                    });
                    var ignoreReflect6 = parseIgnoreReflect(o.Arg6, o.TargetRange, ability.ExerciseType);
                    stringBuilder.Append(string.Format("{0}: {1} auto-hit {2}{3}", name, target1, multipleStatus2,
                        ignoreReflect6));
                    break;
                case 16:
                case 147:
                    str1 = parseUndefArgs(new int[6]
                    {
                        o.Arg7,
                        o.Arg8,
                        o.Arg9,
                        o.Arg10,
                        o.Arg11,
                        o.Arg12
                    });
                    var element3 = parseElement(o.Arg2);
                    var minDmg2 = parseMinDmg(o.Arg3);
                    var sameTarget3 = parseSameTarget(o.Arg5, o.TargetRange);
                    var ignoreReflect7 = parseIgnoreReflect(o.Arg6, o.TargetRange, ability.ExerciseType);
                    var damageCalcType2 = parseDamageCalcType(o.Arg13, o.Arg14);
                    var ignoreMblink1 = parseIgnoreMblink(o.Arg15);
                    var multiTarget2 = parseMultiTarget(o.Arg4, sameTarget3, target1);
                    stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}magic {5}damage{6}{7}{8}", name,
                        multiTarget2, o.Arg1, damageCalcType2, damageThreshold1, element3, minDmg2, ignoreReflect7,
                        ignoreMblink1));
                    break;
                case 17:
                    flag1 = false;
                    var status1 = parseStatus(o.StatusAilmentsId);
                    var statusFactor1 = parseStatusFactor(o.StatusAilmentsFactor, o);
                    var str10 = o.StatusAilmentsFactor >= 998
                        ? "auto-hit"
                        : string.Format("{0}% chance to deal", statusFactor1);
                    stringBuilder.Append(string.Format("{0}: {1} {2} {3}% current HP {4}damage", name, target1, str10,
                        o.Arg1, damageThreshold1));
                    if (o.StatusAilmentsId != 0 && o.StatusAilmentsFactor < 998)
                        stringBuilder.Append(string.Format(", subject to {0} reistance", status1));
                    break;
                case 19:
                case 93:
                    stringBuilder.Append(string.Format(
                        "{0}: {1} deals {2}damage equal to {3}% of own current HP and kills self", name, target1,
                        damageThreshold1, o.Arg1));
                    break;
                case 28:
                case 138:
                    flag1 = false;
                    var customParam1 = parseCustomParam(o.StatusAilmentsId);
                    var statusFactorPhrase1 = parseStatusFactorPhrase(o.StatusAilmentsFactor, o);
                    var buffAmount1 = parseBuffAmount(o.Arg2);
                    var duration2 = parseDuration(o.Arg3);
                    var atkType5 = parseAtkType(o.Arg4);
                    var forceHit5 = parseForceHit(o.Arg5);
                    stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}{5}physical damage{6} {7} {8}{9}", name,
                        target1, o.Arg1, forceHit5, atkType5, damageThreshold1, statusFactorPhrase1, customParam1,
                        buffAmount1, duration2));
                    break;
                case 29:
                    flag1 = false;
                    var status2 = parseStatus(o.StatusAilmentsId);
                    var statusFactor2 = parseStatusFactor(o.StatusAilmentsFactor, o);
                    var str11 = o.StatusAilmentsFactor >= 998
                        ? "auto-hit"
                        : string.Format("{0}% chance to deal", statusFactor2);
                    stringBuilder.Append(string.Format("{0}: {1} {2} {3}% max HP {4}damage", name, target1, str11,
                        o.Arg1, damageThreshold1));
                    if (o.StatusAilmentsId != 0 && o.StatusAilmentsFactor < 998)
                        stringBuilder.Append(string.Format(", subject to {0} reistance", status2));
                    break;
                case 30:
                case 150:
                    var element4 = parseElement(o.Arg5);
                    var atkType6 = parseAtkType(o.Arg2);
                    var forceHit6 = parseForceHit(o.Arg3);
                    var expFactor1 = parseExpFactor(o.Arg4);
                    var ignoreBlink2 = parseIgnoreBlink(o.Arg6);
                    stringBuilder.Append(string.Format("{0}: {1} {2}% piercing {3} {4}{5}{6}physical {7}damage{8}",
                        name, target1, o.Arg1, expFactor1, forceHit6, atkType6, damageThreshold1, element4,
                        ignoreBlink2));
                    break;
                case 31:
                case 104:
                    var element5 = parseElement(o.Arg2);
                    var minDmg3 = parseMinDmg(o.Arg3);
                    var expFactor2 = parseExpFactor(o.Arg4);
                    var ignoreMblink2 = parseIgnoreMblink(o.Arg5);
                    stringBuilder.Append(string.Format("{0}: {1} {2}% piercing {3} {4}magic {5}damage{6}{7}", name,
                        target1, o.Arg1, expFactor2, damageThreshold1, element5, ignoreMblink2, minDmg3));
                    break;
                case 32:
                    var element6 = parseElement(o.Arg2);
                    var ignoreReflect8 = parseIgnoreReflect(o.Arg4, o.TargetRange, ability.ExerciseType);
                    var minDmg4 = parseMinDmg(o.Arg3);
                    stringBuilder.Append(string.Format("{0}: {1} {2}% {3}magic {4}damage{5}{6}", name, target1, o.Arg1,
                        damageThreshold1, element6, minDmg4, ignoreReflect8));
                    break;
                case 33:
                    flag1 = false;
                    var customParam2 = parseCustomParam(o.StatusAilmentsId);
                    var statusFactorPhrase2 = parseStatusFactorPhrase(o.StatusAilmentsFactor, o);
                    var buffAmount2 = parseBuffAmount(o.Arg1);
                    var duration3 = parseDuration(o.Arg2);
                    str2 = parseForceHit(o.Arg3);
                    var ignoreReflect9 = parseIgnoreReflect(o.Arg4, o.TargetRange, ability.ExerciseType);
                    if (o.Arg3 > 0)
                    {
                        stringBuilder.Append(string.Format("{0}: {1} auto-hit {2} {3}{4}{5}", name, target1,
                            customParam2, buffAmount2, duration3, ignoreReflect9));
                        break;
                    }

                    stringBuilder.Append(string.Format("{0}: {1}{2} {3} {4}{5}{6}", name, target1, statusFactorPhrase2,
                        customParam2, buffAmount2, duration3, ignoreReflect9));
                    break;
                case 35:
                    var element7 = parseElement(o.Arg2);
                    var atkType7 = parseAtkType(o.Arg3);
                    var forceHit7 = parseForceHit(o.Arg4);
                    stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}{5}physical {6}damage", name, target1,
                        o.Arg1, forceHit7, atkType7, damageThreshold1, element7));
                    break;
                case 36:
                    var multipleElements2 = parseMultipleElements(new int[2]
                    {
                        o.Arg2,
                        o.Arg7
                    });
                    var minDmg5 = parseMinDmg(o.Arg3);
                    var ignoreReflect10 = parseIgnoreReflect(o.Arg8, o.TargetRange, ability.ExerciseType);
                    var drain2 = parseDrain(o.Arg4);
                    var damageCalcType3 = parseDamageCalcType(o.Arg5, o.Arg6);
                    stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}magic {5}damage{6}{7}{8}", name, target1,
                        o.Arg1, damageCalcType3, damageThreshold1, multipleElements2, drain2, minDmg5,
                        ignoreReflect10));
                    break;
                case 37:
                    stringBuilder.Append(string.Format("{0}: {1} {2}% of own missing HP as {3}damage", name, target1,
                        o.Arg1, damageThreshold1));
                    break;
                case 40:
                    flag1 = false;
                    stringBuilder.Append(string.Format(
                        "{0}: {1} {2}% chance to reduce an ability's uses by {3}% of max uses", name, target1, o.Arg2,
                        o.Arg1));
                    if (o.Arg1 != 100) stringBuilder.Append(", rounded up");
                    break;
                case 41:
                case 113:
                    var str12 = o.Arg2 != 0 || isSingleTarget(o.TargetRange)
                        ? ""
                        : ", divided by number of targets hit";
                    stringBuilder.Append(string.Format("{0}: {1} {2} {3}fixed damage{4}", name, target1, o.Arg1,
                        damageThreshold1, str12));
                    break;
                case 42:
                    flag1 = false;
                    var atkType8 = parseAtkType(o.Arg2);
                    var forceHit8 = parseForceHit(o.Arg3);
                    var str13 = o.TargetMethod == 4
                        ? target1
                        : target1 + string.Format("[cannot target units with {0}]", parseStatus(o.StatusAilmentsId));
                    stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}{5}physical damage", name, str13, o.Arg1,
                        forceHit8, atkType8, damageThreshold1));
                    break;
                case 43:
                    flag1 = false;
                    var element8 = parseElement(o.Arg2);
                    var minDmg6 = parseMinDmg(o.Arg3);
                    var customParam3 = parseCustomParam(o.StatusAilmentsId);
                    var statusFactorPhrase3 = parseStatusFactorPhrase(o.StatusAilmentsFactor, o);
                    var buffAmount3 = parseBuffAmount(o.Arg4);
                    var duration4 = parseDuration(o.Arg5);
                    stringBuilder.Append(string.Format("{0}: {1} {2}% {3}magic {4}damage{5}{6} {7} {8}{9}", name,
                        target1, o.Arg1, damageThreshold1, element8, minDmg6, statusFactorPhrase3, customParam3,
                        buffAmount3, duration4));
                    break;
                case 44:
                case 71:
                    stringBuilder.Append(string.Format("{0}: {1} - reduces HP to {2}", name, target1, o.Arg1));
                    if ((uint) o.MaxDamageThreshold > 0U) stringBuilder.Append(", overflowable");
                    break;
                case 45:
                    flag1 = false;
                    var fracHp1 = parseFracHP(o.Arg2);
                    str4 = parseStatusFactor(o.StatusAilmentsFactor, o);
                    if (o.Arg3 == 0)
                    {
                        stringBuilder.Append(string.Format("{0}: {1} {2}% {3} HP {4}damage (ignores KO resist)", name,
                            target1, o.Arg1, fracHp1, damageThreshold1));
                        break;
                    }

                    stringBuilder.Append(string.Format("{0}: {1} auto-hit {2}% {3} HP {4}damage", name, target1, o.Arg1,
                        fracHp1, damageThreshold1));
                    break;
                case 47:
                    flag1 = false;
                    var atkType9 = parseAtkType(o.Arg2);
                    var str14 = o.Arg3 <= 0 || o.StatusAilmentsId <= 0
                        ? ""
                        : string.Format(", {0} {1}",
                            o.Arg3 >= 100 ? "removes" : (object) (o.Arg3 + "% chance to remove"),
                            parseStatus(o.StatusAilmentsId));
                    stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}physical damage{5}", name, target1, o.Arg1,
                        atkType9, damageThreshold1, str14));
                    break;
                case 48:
                    str1 = parseUndefArgs(new int[1]
                    {
                        o.StatusAilmentsId
                    });
                    var atkType10 = parseAtkType(o.Arg2);
                    var statusBundle1 = parseStatusBundle(o.Arg3);
                    var statusFactorPhrase4 = parseStatusFactorPhrase(o.StatusAilmentsFactor, o);
                    var damageCalcType4 = parseDamageCalcType(o.Arg4, o.Arg5);
                    stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}{5}physical damage{6} {7}{8}", name,
                        target1, o.Arg1, atkType10, damageCalcType4, damageThreshold1, statusFactorPhrase4,
                        statusBundle1, parseForceInflict(o.StatusAilmentsFactor, o.Arg3, o)));
                    break;
                case 51:
                    str1 = parseUndefArgs(new int[1] {o.Arg5});
                    flag1 = false;
                    var multipleStatus3 = parseMultipleStatus(new int[5]
                    {
                        o.Arg1,
                        o.Arg2,
                        o.Arg3,
                        o.Arg4,
                        o.StatusAilmentsId
                    });
                    stringBuilder.Append(string.Format("{0}: {1} auto-hit {2}", name, target1, multipleStatus3));
                    break;
                case 52:
                    flag1 = false;
                    stringBuilder.Append("does nothing");
                    break;
                case 53:
                    var atkType11 = parseAtkType(o.Arg3);
                    var forceHit9 = parseForceHit(o.Arg4);
                    var element9 = parseElement(o.Arg5);
                    var ignoreReflect11 = parseIgnoreReflect(o.Arg7, o.TargetRange, ability.ExerciseType);
                    var sameTarget4 = parseSameTarget(o.Arg6, o.TargetRange);
                    var multiTarget3 = parseMultiTarget(o.Arg2, sameTarget4, target1);
                    var damageCalcType5 = parseDamageCalcType(o.Arg8, o.Arg9);
                    stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}{5}{6}physical {7}damage{8}", name,
                        multiTarget3, o.Arg1, forceHit9, atkType11, damageCalcType5, damageThreshold1, element9,
                        ignoreReflect11));
                    break;
                case 55:
                    var element10 = parseElement(o.Arg4);
                    var atkType12 = parseAtkType(o.Arg2);
                    var forceHit10 = parseForceHit(o.Arg3);
                    stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}{5}physical {6}damage", name, target1,
                        o.Arg1, forceHit10, atkType12, damageThreshold1, element10));
                    break;
                case 56:
                    var fracHp2 = parseFracHP(o.Arg2);
                    str4 = parseStatusFactor(o.StatusAilmentsFactor, o);
                    if (o.Arg3 == 0)
                    {
                        stringBuilder.Append(string.Format("{0}: {1} {2}% {3} HP {4}damage (ignores KO resist)", name,
                            target1, o.Arg1, fracHp2, damageThreshold1));
                        break;
                    }

                    stringBuilder.Append(string.Format("{0}: {1} auto-hit {2}% {3} HP {4}damage", name, target1, o.Arg1,
                        fracHp2, damageThreshold1));
                    break;
                case 59:
                    flag1 = false;
                    var status3 = parseStatus(o.Arg2);
                    var str15 = o.Arg4 == 0
                        ? string.Format("{0} {1}{2}", parseStatusFactorPhrase(o.Arg1, o).Replace(", ", ""), status3,
                            parseForceInflict(o.Arg1, o.Arg2, o))
                        : string.Format("auto-hit {0}", status3);
                    var status4 = parseStatus(o.StatusAilmentsId);
                    var str16 = string.Format("{0}% chance to remove {1}", parseStatusFactor(o.StatusAilmentsFactor, o),
                        status4);
                    var ignoreReflect12 = parseIgnoreReflect(o.Arg5, o.TargetRange, ability.ExerciseType);
                    stringBuilder.Append(string.Format("{0}: {1} {2}, {3}{4}", name, target1, str15, str16,
                        ignoreReflect12));
                    break;
                case 61:
                    var atkType13 = parseAtkType(o.Arg2);
                    stringBuilder.Append(string.Format("{0}: {1} {2}% of own missing HP as {3}{4}physical damage", name,
                        target1, o.Arg1, atkType13, damageThreshold1));
                    break;
                case 65:
                    flag1 = false;
                    var customParam4 = parseCustomParam(o.StatusAilmentsId);
                    var statusFactorPhrase5 = parseStatusFactorPhrase(o.StatusAilmentsFactor, o);
                    var buffAmount4 = parseBuffAmount(o.Arg1);
                    var buffAmount5 = parseBuffAmount(o.Arg2);
                    str3 = parseDuration(o.Arg3);
                    if (o.Arg5 == 0)
                    {
                        stringBuilder.Append(string.Format("{0}: {1}{2} {3} {4}, and applies {5} {6} to self", name,
                            target1, statusFactorPhrase5, customParam4, buffAmount4, customParam4, buffAmount5));
                        break;
                    }

                    stringBuilder.Append(string.Format("{0}: {1} auto-hit {2} {3}, and applies {4} {5} to self", name,
                        target1, customParam4, buffAmount4, customParam4, buffAmount5));
                    break;
                case 69:
                    parseUndefArgs(new int[2] {o.Arg6, o.Arg7});
                    var atkType14 = parseAtkType(o.Arg2);
                    var forceHit11 = parseForceHit(o.Arg3);
                    var str17 = o.Arg4 == 0 ? "" : string.Format(", applies {0} to self", parseStatus(o.Arg4));
                    if (o.Arg4 != 0 && o.Arg5 > 0)
                        str17 += parseDuration(o.Arg5);
                    stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}{5}physical damage{6}", name, target1,
                        o.Arg1, forceHit11, atkType14, damageThreshold1, str17));
                    break;
                case 74:
                    flag1 = false;
                    var atkType15 = parseAtkType(o.Arg3);
                    var sameTarget5 = parseSameTarget(o.Arg6, o.TargetRange);
                    var multiTarget4 = parseMultiTarget(o.Arg2, sameTarget5, target1);
                    var forceHit12 = parseForceHit(o.Arg4);
                    var multipleElements3 = parseMultipleElements(new int[2]
                    {
                        o.Arg5,
                        o.Arg9
                    });
                    var generalStatus1 =
                        parseGeneralStatus(o.StatusAilmentsId, 0, o.StatusAilmentsFactor, o.Arg7, o.Arg8, o);
                    var str18 = parseRemoveStatusBundle(o.Arg10).Replace("Removes", "remove");
                    if (!str18.Equals(""))
                    {
                        var str19 = str18.Contains("remove") ? " from" : "";
                        var flag2 = o.Arg11 == o.TargetRange && o.Arg12 == o.TargetSegment;
                        str18 = string.Format(", {0}% chance to {1}", o.Arg13, str18);
                        if (!flag2)
                        {
                            options2.TargetMethod = o.TargetMethod;
                            options2.TargetRange = o.Arg11;
                            options2.TargetSegment = o.Arg12;
                            var target2 = parseTarget(options2);
                            str18 = str18 + str19 + target2;
                        }
                    }

                    if ((uint) o.Arg14 > 0U)
                        str8 = o.Arg15 != 2
                            ? o.Arg15 != 3 ? string.Format(", heals [error parsing target] for {0}% max HP", o.Arg14) :
                            string.Format(", heals self for {0}% max HP", o.Arg14)
                            : string.Format(", heals all enemies for {0}% max HP", o.Arg14);
                    stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}{5}physical {6}damage{7}{8}{9}", name,
                        multiTarget4, o.Arg1, forceHit12, atkType15, damageThreshold1, multipleElements3,
                        generalStatus1, str18, str8));
                    break;
                case 77:
                    str1 = parseUndefArgs(new int[3]
                    {
                        o.Arg11,
                        o.Arg12,
                        o.Arg13
                    });
                    flag1 = false;
                    var str20 = o.Arg14 == 0
                        ? string.Format("{0}", parseStatusFactorPhrase(o.StatusAilmentsFactor, o).Replace(", ", ""))
                        : "auto-hit";
                    var duration5 = parseDuration(o.Arg1);
                    if ((uint) o.Arg2 > 0U)
                        source.Add(string.Format("{0} {1}", parseCustomParam(o.Arg2), parseBuffAmount(o.Arg3)));
                    if ((uint) o.Arg4 > 0U)
                        source.Add(string.Format("{0} {1}", parseCustomParam(o.Arg4), parseBuffAmount(o.Arg5)));
                    if ((uint) o.Arg6 > 0U)
                        source.Add(string.Format("{0} {1}", parseCustomParam(o.Arg6), parseBuffAmount(o.Arg7)));
                    var str21 = source.Count != 1
                        ? string.Join(", ", source.ToArray(), 0, source.Count - 1) + " and " + source.LastOrDefault()
                        : source[0];
                    if (source.Count > 0)
                        str7 = str20 + " " + str21 + duration5;
                    var multipleStatus4 = parseMultipleStatus(new int[4]
                    {
                        o.Arg8,
                        o.Arg9,
                        o.Arg10,
                        o.StatusAilmentsId
                    });
                    if (multipleStatus4 != "")
                    {
                        if (str7 != "")
                            str7 = string.Format("{0} {1}{2} and {3}{4}", str20, str21, duration5, multipleStatus4,
                                o.Arg14 == 0
                                    ? parseForceInflictMulti(o.StatusAilmentsFactor, multipleStatus4, o)
                                    : (object) "");
                        else
                            str7 = str20 + " " + multipleStatus4 + (o.Arg14 == 0
                                ? parseForceInflictMulti(o.StatusAilmentsFactor, multipleStatus4, o)
                                : "");
                    }

                    stringBuilder.Append(string.Format("{0}: {1} {2}", name, target1, str7));
                    break;
                case 81:
                    flag1 = false;
                    stringBuilder.Append("does nothing");
                    break;
                case 82:
                    str1 = parseUndefArgs(new int[3]
                    {
                        o.Arg10,
                        o.Arg11,
                        o.Arg12
                    });
                    flag1 = false;
                    var str22 = o.Arg3 == 0
                        ? string.Format("{0}", parseStatusFactor(o.Arg2, o).Replace(", ", ""))
                        : "auto-hit";
                    var duration6 = parseDuration(o.Arg1);
                    if ((uint) o.Arg4 > 0U)
                        source.Add(string.Format("{0} {1}", parseCustomParam(o.Arg4), parseBuffAmount(o.Arg5)));
                    if ((uint) o.Arg6 > 0U)
                        source.Add(string.Format("{0} {1}", parseCustomParam(o.Arg6), parseBuffAmount(o.Arg7)));
                    if ((uint) o.Arg8 > 0U)
                        source.Add(string.Format("{0} {1}", parseCustomParam(o.Arg8), parseBuffAmount(o.Arg9)));
                    var str23 = source.Count != 1
                        ? string.Join(", ", source.ToArray(), 0, source.Count - 1) + " and " + source.LastOrDefault()
                        : source[0];
                    stringBuilder.Append(string.Format("{0}: {1} {2} {3}{4}", name, target1, str22, str23, duration6));
                    break;
                case 85:
                    var atkType16 = parseAtkType(o.Arg2);
                    var multipleStatus5 = parseMultipleStatus(new int[5]
                    {
                        o.Arg4,
                        o.Arg5,
                        o.Arg6,
                        o.Arg7,
                        o.Arg8
                    });
                    var str24 = o.Arg3 == 0 || multipleStatus5.Equals("")
                        ? ""
                        : string.Format(", {0}% chance to remove {1}", parseStatusFactor(o.Arg3, o), multipleStatus5);
                    var str25 = o.Arg3 == 0 || o.Arg9 == 0
                        ? ""
                        : string.Format(", {0}% chance to {1}", parseStatusFactor(o.Arg3, o),
                            parseRemoveStatusBundle(o.Arg9).Replace("Removes", "remove"));
                    stringBuilder.Append(string.Format("{0}: {1} {2}{3}physical damage{4}{5}", name, target1, atkType16,
                        damageThreshold1, str24, str25));
                    break;
                case 88:
                    str1 = parseUndefArgs(new int[6]
                    {
                        o.Arg7,
                        o.Arg8,
                        o.Arg9,
                        o.Arg13,
                        o.Arg14,
                        o.Arg15
                    });
                    var multipleElements4 = parseMultipleElements(new int[3]
                    {
                        o.Arg3,
                        o.Arg12,
                        o.Arg16
                    });
                    var sameTarget6 = parseSameTarget(o.Arg4, o.TargetRange);
                    var ignoreReflect13 = parseIgnoreReflect(o.Arg10, o.TargetRange, ability.ExerciseType);
                    var multiTarget5 = parseMultiTarget(o.Arg2, sameTarget6, target1);
                    var damageCalcType6 = parseDamageCalcType(o.Arg18, o.Arg19);
                    var ignoreMblink3 = parseIgnoreMblink(o.Arg20);
                    var str26 = o.Arg5 == 0 ? "" : string.Format(", applies {0} to self", parseStatusOrBundle(o.Arg5));
                    if (o.Arg5 != 0 && o.Arg6 > 0)
                        str26 += parseDuration(o.Arg6);
                    stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}magic {5}damage{6}{7}{8}", name,
                        multiTarget5, o.Arg1, damageCalcType6, damageThreshold1, multipleElements4, ignoreReflect13,
                        ignoreMblink3, str26));
                    break;
                case 90:
                    flag1 = false;
                    str1 = parseUndefArgs(new int[2]
                    {
                        o.Arg2,
                        o.Arg3
                    });
                    var ignoreReflect14 = parseIgnoreReflect(o.Arg4, o.TargetRange, ability.ExerciseType);
                    var str27 = o.Arg5 != 0 ? string.Format(", {0}", parseRemoveStatusBundle(o.Arg5)) : "";
                    if ((uint) o.Arg6 > 0U)
                        source.Add(parseStatus(o.Arg6));
                    if ((uint) o.Arg7 > 0U)
                        source.Add(parseStatus(o.Arg7));
                    if ((uint) o.Arg8 > 0U)
                        source.Add(parseStatus(o.Arg8));
                    if (source.Count > 0)
                        str7 = string.Format(", removes {0} and {1}",
                            string.Join(", ", source.ToArray(), 0, source.Count - 1), source.LastOrDefault());
                    stringBuilder.Append(string.Format("{0}: {1} - Factor {2} {3}heal{4}{5}{6}", name, target1, o.Arg1,
                        damageThreshold1, str27, str7, ignoreReflect14));
                    break;
                case 95:
                    flag1 = false;
                    str1 = parseUndefArgs(new int[2]
                    {
                        o.Arg3,
                        o.Arg4
                    });
                    var element11 = parseElement(o.Arg2);
                    var multipleStatus6 = parseMultipleStatus(new int[6]
                    {
                        o.Arg5,
                        o.Arg6,
                        o.Arg7,
                        o.Arg8,
                        o.Arg9,
                        o.StatusAilmentsId
                    });
                    var statusFactorPhrase6 = parseStatusFactorPhrase(o.StatusAilmentsFactor, o);
                    stringBuilder.Append(string.Format("{0}: {1} {2}% {3}magic {4}damage{5} {6}{7}", name, target1,
                        o.Arg1, damageThreshold1, element11, statusFactorPhrase6, multipleStatus6,
                        parseForceInflictMulti(o.StatusAilmentsFactor, multipleStatus6, o)));
                    break;
                case 96:
                    str1 = parseUndefArgs(new int[2]
                    {
                        o.Arg2,
                        o.Arg3
                    });
                    var damageThreshold3 = parseDamageThreshold(o.MinDamageThreshold);
                    flag1 = false;
                    stringBuilder.Append(string.Format("{0}: {1} {2}% max HP {3}heal", name, target1, o.Arg1,
                        damageThreshold3));
                    break;
                case 100:
                    var atkType17 = parseAtkType(o.Arg3);
                    var sameTarget7 = parseSameTarget(o.Arg6, o.TargetRange);
                    var multiTarget6 = parseMultiTarget(o.Arg2, sameTarget7, target1);
                    var forceHit13 = parseForceHit(o.Arg4);
                    var element12 = parseElement(o.Arg5);
                    var str28 = o.Arg7 == 0 || o.Arg8 == 0
                        ? ""
                        : string.Format(", {0}% chance to {1}", parseStatusFactor(o.Arg8, o),
                            parseRemoveStatusBundle(o.Arg7).Replace("Removes", "remove"));
                    stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}{5}physical {6}damage{7}", name,
                        multiTarget6, o.Arg1, forceHit13, atkType17, damageThreshold1, element12, str28));
                    break;
                case 101:
                    flag1 = false;
                    str1 = parseUndefArgs(new int[9]
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
                    var atkType18 = parseAtkType(o.Arg3);
                    var forceHit14 = parseForceHit(o.Arg4);
                    var multipleElements5 = parseMultipleElements(new int[3]
                    {
                        o.Arg5,
                        o.Arg10,
                        o.Arg19
                    });
                    var sameTarget8 = parseSameTarget(o.Arg6, o.TargetRange);
                    var multiTarget7 = parseMultiTarget(o.Arg2, sameTarget8, target1);
                    var statusBundle2 = parseStatusBundle(o.Arg7);
                    var statusFactorPhrase7 = parseStatusFactorPhrase(o.StatusAilmentsFactor, o);
                    var str29 = o.Arg7 != 0
                        ? string.Format("{0} {1}{2}", statusFactorPhrase7, statusBundle2,
                            parseForceInflictBundle(o.StatusAilmentsFactor, o.Arg7, o))
                        : "";
                    var str30 = o.Arg20 != 0 ? string.Format(", {0}", parseRemoveStatusBundle(o.Arg20)) : "";
                    var damageCalcType7 = parseDamageCalcType(o.Arg8, o.Arg9);
                    stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}{5}{6}physical {7}damage{8}{9}", name,
                        multiTarget7, o.Arg1, forceHit14, atkType18, damageCalcType7, damageThreshold1,
                        multipleElements5, str29, str30));
                    break;
                case 105:
                    flag1 = false;
                    var multipleElements6 = parseMultipleElements(new int[2]
                    {
                        o.Arg2,
                        o.Arg15
                    });
                    var minDmg7 = parseMinDmg(o.Arg3);
                    var sameTarget9 = parseSameTarget(o.Arg5, o.TargetRange);
                    var multiTarget8 = parseMultiTarget(o.Arg4, sameTarget9, target1);
                    var ignoreReflect15 = parseIgnoreReflect(o.Arg12, o.TargetRange, ability.ExerciseType);
                    if (o.Arg8 > 0 && o.Arg9 > 0)
                    {
                        str6 += string.Format("{0} {1}{2}", parseStatusFactorPhrase(o.Arg9, o), parseStatus(o.Arg8),
                            parseForceInflict(o.Arg9, o.Arg8, o));
                        stringList.Add(parseStatus(o.Arg8));
                    }

                    if (o.Arg6 > 0 && o.Arg7 > 0 && !stringList.Contains(parseStatus(o.Arg6)))
                    {
                        str6 += string.Format("{0} {1}{2}", parseStatusFactorPhrase(o.Arg7, o), parseStatus(o.Arg6),
                            parseForceInflict(o.Arg7, o.Arg6, o));
                        stringList.Add(parseStatus(o.Arg6));
                    }

                    if (o.StatusAilmentsId > 0 && o.StatusAilmentsFactor > 0 &&
                        !stringList.Contains(parseStatus(o.StatusAilmentsId)))
                        str6 += string.Format("{0} {1}{2}", parseStatusFactorPhrase(o.StatusAilmentsFactor, o),
                            parseStatus(o.StatusAilmentsId),
                            parseForceInflict(o.StatusAilmentsFactor, o.StatusAilmentsId, o));
                    var str31 = o.Arg13 <= 0 || o.Arg14 <= 0
                        ? ""
                        : string.Format("{0} {1}{2}", parseStatusFactorPhrase(o.Arg14, o), parseStatusBundle(o.Arg13),
                            parseForceInflictBundle(o.Arg14, o.Arg13, o));
                    var str32 = o.Arg10 <= 0 || o.Arg11 <= 0
                        ? ""
                        : string.Format(", {0}% chance to {1}", parseStatusFactor(o.Arg11, o),
                            parseRemoveStatusBundle(o.Arg10).Replace("Removes", "remove"));
                    stringBuilder.Append(string.Format("{0}: {1} {2}% {3}magic {4}damage{5}{6}{7}{8}{9}", name,
                        multiTarget8, o.Arg1, damageThreshold1, multipleElements6, minDmg7, str6, str31, str32,
                        ignoreReflect15));
                    break;
                case 106:
                    var element13 = parseElement(o.Arg2);
                    var minDmg8 = parseMinDmg(o.Arg3);
                    stringBuilder.Append(string.Format("{0}: {1} buff-ignoring {2}% {3}magic {4}damage{5}", name,
                        target1, o.Arg1, damageThreshold1, element13, minDmg8));
                    break;
                case 117:
                case 118:
                    flag1 = false;
                    var element14 = parseElement(o.Arg2);
                    var minDmg9 = parseMinDmg(o.Arg3);
                    var factor = Math.Max(o.Arg8, o.StatusAilmentsFactor);
                    var ignoreMblink4 = parseIgnoreMblink(o.Arg13);
                    var generalStatus2 =
                        parseGeneralStatus(o.StatusAilmentsId, o.Arg4, factor, o.Arg6, o.Arg7, o.Arg5, o);
                    var str33 = o.Arg9 != 0 ? string.Format(", {0}", parseRemoveStatusBundle(o.Arg9)) : "";
                    var damageCalcType8 = parseDamageCalcType(o.Arg11, o.Arg12);
                    stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}magic {5}damage{6}{7}{8}{9}", name, target1,
                        o.Arg1, damageCalcType8, damageThreshold1, element14, minDmg9, generalStatus2, str33,
                        ignoreMblink4));
                    break;
                case 123:
                    flag1 = false;
                    var status5 = parseStatus(o.StatusAilmentsId);
                    var statusFactor3 = parseStatusFactor(o.StatusAilmentsFactor, o);
                    var str34 = o.StatusAilmentsFactor >= 998
                        ? "auto-hit"
                        : string.Format("{0}% chance to deal", statusFactor3);
                    stringBuilder.Append(string.Format("{0}: {1} {2} [attacker's % missing HP]% current HP {3}damage",
                        name, target1, str34, damageThreshold1));
                    if (o.StatusAilmentsId != 0 && o.StatusAilmentsFactor < 998)
                        stringBuilder.Append(string.Format(", subject to {0} resistance", status5));
                    break;
                case 124:
                    var atkType19 = parseAtkType(o.Arg3);
                    var forceHit15 = parseForceHit(o.Arg4);
                    var drain3 = parseDrain(o.Arg2);
                    stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}{5}physical damage{6}", name, target1,
                        o.Arg1, forceHit15, atkType19, damageThreshold1, drain3));
                    break;
                case 129:
                case 130:
                    flag1 = false;
                    var fracHp3 = parseFracHP(o.Arg2);
                    var str35 = parseIgnoreBlink(o.Arg11).Equals("")
                        ? ", ignores KO resist"
                        : ", ignores blinks and KO resist";
                    var generalStatus3 = parseGeneralStatus(o.StatusAilmentsId, o.Arg4,
                        o.Arg8 == 0 ? o.StatusAilmentsFactor : o.Arg8, o.Arg6, o.Arg7, o);
                    var removeStatusBundle2 = parseRemoveStatusBundle(o.Arg9);
                    var str36 = removeStatusBundle2.Equals("") ? "" : ", " + removeStatusBundle2;
                    var status6 = parseStatus(o.Arg10);
                    var str37 = status6.Equals("") ? "" : ", removes " + status6;
                    stringBuilder.Append(string.Format("{0}: {1} {2}% {3} HP {4}damage{5}{6}{7}{8}", name, target1,
                        o.Arg1, fracHp3, damageThreshold1, generalStatus3, str36, str37, str35));
                    break;
                case 136:
                    parseUndefArgs(new int[11]
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
                    var multipleElements7 = parseMultipleElements(new int[4]
                    {
                        o.Arg2,
                        o.Arg23,
                        o.Arg24,
                        o.Arg25
                    });
                    var sameTarget10 = parseSameTarget(o.Arg6, o.TargetRange);
                    var multiTarget9 = parseMultiTarget(o.Arg3, sameTarget10, target1);
                    var ignoreMblink5 = parseIgnoreMblink(o.Arg18);
                    var damageCalcType9 = parseDamageCalcType(o.Arg19, o.Arg20);
                    var str38 = o.Arg8 == 3 ? "self" : "[**target not parsed**]";
                    var str39 = o.Arg4 != 0
                        ? string.Format(", {0} {1} {2}heal", str38,
                            o.Arg22 == 0
                                ? string.Format("factor {0}", o.Arg4)
                                : (object) string.Format("{0} fixed HP", o.Arg4),
                            o.Arg21 <= 0 || o.Arg22 != 0 && o.Arg4 > 9999 ? "" : (object) "overflow ")
                        : "";
                    stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}magic {5}damage{6}{7}", name, multiTarget9,
                        o.Arg1, damageCalcType9, damageThreshold1, multipleElements7, ignoreMblink5, str39));
                    break;
                case 137:
                    str1 = parseUndefArgs(new int[5]
                    {
                        o.Arg10,
                        o.Arg11,
                        o.Arg12,
                        o.Arg13,
                        o.Arg14
                    });
                    flag1 = false;
                    var atkType20 = parseAtkType(o.Arg3);
                    var forceHit16 = parseForceHit(o.Arg4);
                    var multipleElements8 = parseMultipleElements(new int[2]
                    {
                        o.Arg5,
                        o.Arg15
                    });
                    var sameTarget11 = parseSameTarget(o.Arg6, o.TargetRange);
                    var multiTarget10 = parseMultiTarget(o.Arg2, sameTarget11, target1);
                    var damageCalcType10 = parseDamageCalcType(o.Arg17, o.Arg18);
                    var variableImperil1 = parseVariableImperil(o.Arg7, o.Arg8, o.Arg9, o.Arg16, o);
                    var str40 = o.Arg19 != 0
                        ? string.Format(", applies {0} to {1}", parseStatus(o.Arg19),
                            singleEnemy ? "self" : (object) "all enemies")
                        : "";
                    var str41 = o.Arg20 != 0
                        ? string.Format(", applies {0} to {1}", parseStatusBundle(o.Arg20),
                            singleEnemy ? "self" : (object) "all enemies")
                        : "";
                    stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}{5}{6}physical {7}damage{8}{9}{10}", name,
                        multiTarget10, o.Arg1, forceHit16, atkType20, damageCalcType10, damageThreshold1,
                        multipleElements8, variableImperil1, str40, str41));
                    break;
                case 140:
                    str1 = parseUndefArgs(new int[7]
                    {
                        o.Arg12,
                        o.Arg13,
                        o.Arg14,
                        o.Arg15,
                        o.Arg16,
                        o.Arg17,
                        o.Arg18
                    });
                    var minDmg10 = parseMinDmg(o.Arg2);
                    var multipleElements9 = parseMultipleElements(new int[4]
                    {
                        o.Arg5,
                        o.Arg6,
                        o.Arg7,
                        o.Arg8
                    });
                    var sameTarget12 = parseSameTarget(o.Arg4, o.TargetRange);
                    var multiTarget11 = parseMultiTarget(o.Arg3, sameTarget12, target1);
                    var ignoreReflect16 = parseIgnoreReflect(o.Arg9, o.TargetRange, ability.ExerciseType);
                    var damageCalcType11 = parseDamageCalcType(o.Arg10, o.Arg11);
                    stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}magic {5}damage{6}{7}", name, multiTarget11,
                        o.Arg1, damageCalcType11, damageThreshold1, multipleElements9, minDmg10, ignoreReflect16));
                    break;
                case 141:
                    str1 = parseUndefArgs(new int[8]
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
                    var atkType21 = parseAtkType(o.Arg3);
                    var forceHit17 = parseForceHit(o.Arg4);
                    var multipleElements10 = parseMultipleElements(new int[4]
                    {
                        o.Arg6,
                        o.Arg7,
                        o.Arg8,
                        o.Arg9
                    });
                    var critDamage = parseCritDamage(o.Arg10);
                    var crit2 = parseCrit(o.Arg19);
                    var sameTarget13 = parseSameTarget(o.Arg5, o.TargetRange);
                    var multiTarget12 = parseMultiTarget(o.Arg2, sameTarget13, target1);
                    stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}{5}physical {6}damage{7}{8}", name,
                        multiTarget12, o.Arg1, forceHit17, atkType21, damageThreshold1, multipleElements10, crit2,
                        critDamage));
                    break;
                case 145:
                case 146:
                    str1 = parseUndefArgs(new int[2]
                    {
                        o.Arg3,
                        o.Arg11
                    });
                    flag1 = false;
                    var damageThreshold4 = parseDamageThreshold(o.MinDamageThreshold);
                    var generalStatus4 = parseGeneralStatus(o.StatusAilmentsId, o.Arg4,
                        o.Arg8 == 0 ? o.StatusAilmentsFactor : o.Arg8, o.Arg6, o.Arg7, o.Arg5, o);
                    var removeStatusBundle3 = parseRemoveStatusBundle(o.Arg9);
                    var str42 = removeStatusBundle3.Equals("") ? "" : ", " + removeStatusBundle3;
                    var status7 = parseStatus(o.Arg10);
                    var str43 = status7.Equals("") ? "" : string.Format(", removes {0}", status7);
                    stringBuilder.Append(string.Format("{0}: {1} - Factor {2} {3}heal{4}{5}{6}", name, target1, o.Arg1,
                        damageThreshold4, generalStatus4, str42, str43));
                    break;
                case 151:
                    str1 = parseUndefArgs(new int[2]
                    {
                        o.Arg19,
                        o.Arg20
                    });
                    var damageCalcType12 = parseDamageCalcType(o.Arg17, o.Arg18);
                    var minDmg11 = parseMinDmg(o.Arg3);
                    var multipleElements11 = parseMultipleElements(new int[2]
                    {
                        o.Arg4,
                        o.Arg10
                    });
                    var sameTarget14 = parseSameTarget(o.Arg5, o.TargetRange);
                    var multiTarget13 = parseMultiTarget(o.Arg2, sameTarget14, target1);
                    var ignoreMblink6 = parseIgnoreMblink(o.Arg24);
                    var ignoreReflect17 = parseIgnoreReflect(o.Arg6, o.TargetRange, ability.ExerciseType);
                    var variableImperil2 = parseVariableImperil(o.Arg7, o.Arg8, o.Arg9, o.Arg16, o);
                    var customParam5 = parseCustomParam(o.Arg11);
                    var buffAmount6 = parseBuffAmount(o.Arg12);
                    var duration7 = parseDuration(o.Arg13);
                    var str44 = o.Arg11 != 0
                        ? string.Format(", self {0} {1}{2}", customParam5, buffAmount6, duration7)
                        : "";
                    var status8 = parseStatus(o.Arg14);
                    var statusFactorPhrase8 = parseStatusFactorPhrase(o.Arg15, o);
                    var str45 = o.Arg14 != 0
                        ? string.Format("{0} {1}{2}", statusFactorPhrase8, status8,
                            parseForceInflict(o.Arg15, o.Arg14, o))
                        : "";
                    var status9 = parseStatus(o.Arg21);
                    var str46 = o.Arg21 != 0 ? string.Format(", applies {0} to all allies", status9) : "";
                    stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}magic {5}damage{6}{7}{8}{9}{10}{11}{12}",
                        name, multiTarget13, o.Arg1, damageCalcType12, damageThreshold1, multipleElements11,
                        variableImperil2, ignoreReflect17, ignoreMblink6, minDmg11, str45, (object) str44,
                        (object) str46));
                    break;
                case 155:
                case 156:
                    flag1 = false;
                    var atkType22 = parseAtkType(o.Arg3);
                    var forceHit18 = parseForceHit(o.Arg12);
                    var multipleElements12 = parseMultipleElements(new int[3]
                    {
                        o.Arg2,
                        o.Arg15,
                        o.Arg16
                    });
                    var sameTarget15 = parseSameTarget(o.Arg14, o.TargetRange);
                    var multiTarget14 = parseMultiTarget(o.Arg13, sameTarget15, target1);
                    var generalStatus5 = parseGeneralStatus(0, o.Arg4, o.Arg8 == 0 ? o.StatusAilmentsFactor : o.Arg8,
                        o.Arg6, o.Arg7, o.Arg5, o);
                    var str47 = o.Arg9 != 0 ? string.Format(", {0}", parseRemoveStatusBundle(o.Arg9)) : "";
                    var str48 = o.Arg10 != 0 ? string.Format(", Removes {0}", parseStatus(o.Arg10)) : "";
                    var ignoreBlink3 = parseIgnoreBlink(o.Arg11);
                    stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}{5}physical {6}damage{7}{8}{9}{10}", name,
                        multiTarget14, o.Arg1, forceHit18, atkType22, damageThreshold1, multipleElements12,
                        generalStatus5, str47, str48, ignoreBlink3));
                    break;
                case 161:
                case 162:
                    flag1 = false;
                    var fracHp4 = parseFracHP(o.Arg2);
                    var statusFactor4 = parseStatusFactor(o.Arg3, o);
                    str2 = parseForceHit(o.Arg5);
                    var generalStatus6 = parseGeneralStatus(o.StatusAilmentsId, o.Arg4,
                        o.Arg8 == 0 ? o.StatusAilmentsFactor : o.Arg8, o.Arg6, o.Arg7, o);
                    var str49 = o.Arg8 == 0 || o.Arg9 == 0
                        ? ""
                        : ", " + parseStatusFactor(o.Arg8, o) + "% chance to " +
                          parseRemoveStatusBundle(o.Arg9).Replace("Removes", "remove");
                    var str50 = o.Arg8 == 0 || o.Arg10 == 0
                        ? ""
                        : string.Format(", {0}% chance to remove {1}", parseStatusFactor(o.Arg8, o),
                            parseStatus(o.Arg10));
                    stringBuilder.Append(string.Format(
                        "{0}: {1} {2} {3}% {4} HP {5}damage, subject to Instant KO resist{6}{7}{8}", name, target1,
                        o.Arg5 == 0 ? string.Format("{0}% chance to deal", statusFactor4) : (object) "auto-hit", o.Arg1,
                        fracHp4, damageThreshold1, generalStatus6, str49, str50));
                    break;
                case 174:
                    var damageThreshold5 = parseDamageThreshold(o.MinDamageThreshold);
                    flag1 = false;
                    var str51 = o.Arg2 <= 0 || o.Arg3 == 0
                        ? ""
                        : string.Format(", applies {0} {1}{2}", parseCustomParam(o.Arg2), parseBuffAmount(o.Arg3),
                            parseDuration(o.Arg4));
                    var str52 = o.Arg5 > 0 ? string.Format(", {0}", parseRemoveStatusBundle(o.Arg5)) : "";
                    if (o.Arg6 > 0)
                        str51 = str51 == ""
                            ? string.Format(", applies {0}", parseStatus(o.Arg6))
                            : str51 + string.Format(" and {0}", parseStatus(o.Arg6));
                    stringBuilder.Append(string.Format("{0}: {1} {2}% max HP {3}heal{4}{5}", name, target1, o.Arg1,
                        damageThreshold5, str51, str52));
                    break;
                case 180:
                    var str53 = o.Arg1 <= 9999 || o.MinDamageThreshold <= 0
                        ? ""
                        : parseDamageThreshold(o.MinDamageThreshold);
                    stringBuilder.Append(string.Format("{0}: {1} {2} fixed HP {3}heal", name, target1, o.Arg1, str53));
                    break;
                case 188:
                    var str54 = o.Arg3 == 0 ? "an ability's" : "all abilities'";
                    stringBuilder.Append(string.Format("{0}: {1} {2}% chance to reduce {3} uses by {4}", name, target1,
                        o.Arg2, str54, o.Arg1));
                    break;
                case 189:
                    str1 = parseUndefArgs(new int[2]
                    {
                        o.Arg7,
                        o.Arg12
                    });
                    var damageCalcType13 = parseDamageCalcType(o.Arg4, o.Arg5);
                    var element15 = parseElement(o.Arg2);
                    var atkType23 = parseAtkType(o.Arg3);
                    var generalStatus7 = parseGeneralStatus(o.Arg6, o.Arg7, o.Arg8, o.Arg9, o.Arg10, o);
                    var generalStatus8 = parseGeneralStatus(o.Arg11, o.Arg12, o.Arg13, o.Arg14, o.Arg15, o);
                    stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}{5}physical {6}damage{7}{8}", name, target1,
                        o.Arg1, damageCalcType13, atkType23, damageThreshold1, element15, generalStatus7,
                        generalStatus8));
                    break;
                case 190:
                    str1 = parseUndefArgs(new int[2]
                    {
                        o.Arg7,
                        o.Arg12
                    });
                    var damageCalcType14 = parseDamageCalcType(o.Arg4, o.Arg5);
                    var element16 = parseElement(o.Arg2);
                    var atkType24 = parseAtkType(o.Arg3);
                    var generalStatus9 = parseGeneralStatus(o.Arg6, o.Arg7, o.Arg8, o.Arg9, o.Arg10, o);
                    var generalStatus10 = parseGeneralStatus(o.Arg11, o.Arg12, o.Arg13, o.Arg14, o.Arg15, o);
                    stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}{5}physical {6}damage{7}{8}", name, target1,
                        o.Arg1, damageCalcType14, atkType24, damageThreshold1, element16, generalStatus9,
                        generalStatus10));
                    break;
                case 192:
                    str1 = parseUndefArgs(new int[3]
                    {
                        o.Arg8,
                        o.Arg9,
                        o.Arg10
                    });
                    flag1 = false;
                    var str55 = o.Arg7 == 0
                        ? string.Format("{0}", parseStatusFactorPhrase(o.StatusAilmentsFactor, o).Replace(", ", ""))
                        : "auto-hit";
                    var duration8 = parseDuration(o.Arg4);
                    if ((uint) o.Arg5 > 0U)
                        str7 = string.Format("{0} {1} {2}{3}", str55, parseCustomParam(o.Arg5), parseBuffAmount(o.Arg6),
                            duration8);
                    var multipleStatus7 = parseMultipleStatus(new int[4]
                    {
                        o.Arg1,
                        o.Arg2,
                        o.Arg3,
                        o.StatusAilmentsId
                    });
                    if (multipleStatus7 != "")
                    {
                        if (str7 != "")
                            str7 = string.Format("{0} and {1} {2}{3}", str7,
                                o.Arg7 == 0
                                    ? ""
                                    : (object) parseStatusFactorPhrase(o.StatusAilmentsFactor, o).Replace(", ", ""),
                                multipleStatus7, parseForceInflictMulti(o.StatusAilmentsFactor, multipleStatus7, o));
                        else
                            str7 = parseStatusFactorPhrase(o.StatusAilmentsFactor, o).Replace(", ", "") + " " +
                                   multipleStatus7 + parseForceInflictMulti(o.StatusAilmentsFactor, multipleStatus7, o);
                    }

                    stringBuilder.Append(string.Format("{0}: {1} {2}", name, target1, str7));
                    break;
                case 208:
                    if (o.Arg2 != 1)
                        str6 = string.Format("{0}x {1}", o.Arg2, parseSameTarget(o.Arg3, o.TargetRange));
                    stringBuilder.Append(string.Format("{0}: {1} {2}{3}-HP fixed {4}heal", name, target1, str6, o.Arg1,
                        damageThreshold1));
                    break;
                case 211:
                    str1 = parseUndefArgs(new int[2]
                    {
                        o.Arg14,
                        o.Arg15
                    });
                    flag1 = false;
                    var enemyActionStatus1 = parseEnemyActionStatus(o);
                    var atkType25 = parseAtkType(o.Arg2);
                    var forceHit19 = parseForceHit(o.Arg8);
                    var multipleElements13 = parseMultipleElements(new int[3]
                    {
                        o.Arg5,
                        o.Arg6,
                        o.Arg7
                    });
                    var crit3 = parseCrit(o.Arg12);
                    var drain4 = parseDrain(o.Arg13);
                    var sameTarget16 = parseSameTarget(o.Arg4, o.TargetRange);
                    var multiTarget15 = parseMultiTarget(o.Arg3, sameTarget16, target1);
                    var damageCalcType15 = parseDamageCalcType(o.Arg10, o.Arg11);
                    var ignoreBlink4 = parseIgnoreBlink(o.Arg9);
                    stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}{5}{6}physical {7}damage{8}{9}{10}{11}",
                        name, multiTarget15, o.Arg1, forceHit19, atkType25, damageCalcType15, damageThreshold1,
                        multipleElements13, drain4, crit3, ignoreBlink4, (object) enemyActionStatus1));
                    break;
                case 212:
                    str1 = parseUndefArgs(new int[3]
                    {
                        o.Arg13,
                        o.Arg14,
                        o.Arg15
                    });
                    flag1 = false;
                    var enemyActionStatus2 = parseEnemyActionStatus(o);
                    var minDmg12 = parseMinDmg(o.Arg2);
                    var sameTarget17 = parseSameTarget(o.Arg4, o.TargetRange);
                    var multiTarget16 = parseMultiTarget(o.Arg3, sameTarget17, target1);
                    var multipleElements14 = parseMultipleElements(new int[3]
                    {
                        o.Arg5,
                        o.Arg6,
                        o.Arg7
                    });
                    var ignoreReflect18 = parseIgnoreReflect(o.Arg8, o.TargetRange, ability.ExerciseType);
                    var ignoreMblink7 = parseIgnoreMblink(o.Arg9);
                    var damageCalcType16 = parseDamageCalcType(o.Arg10, o.Arg11);
                    var drain5 = parseDrain(o.Arg12);
                    stringBuilder.Append(string.Format("{0}: {1} {2}% {3}{4}magic {5}damage{6}{7}{8}{9}{10}", name,
                        multiTarget16, o.Arg1, damageCalcType16, damageThreshold1, multipleElements14, drain5,
                        ignoreReflect18, ignoreMblink7, minDmg12, enemyActionStatus2));
                    break;
                case 213:
                    str1 = parseUndefArgs(new int[9]
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
                    var enemyActionStatus3 = parseEnemyActionStatus(o);
                    var fracHp5 = parseFracHP(o.Arg2);
                    var sameTarget18 = parseSameTarget(o.Arg4, o.TargetRange);
                    var multiTarget17 = parseMultiTarget(o.Arg3, sameTarget18, target1);
                    var ignoreReflect19 = parseIgnoreReflect(o.Arg5, o.TargetRange, ability.ExerciseType);
                    var str56 = parseIgnoreBlink(o.Arg6).Equals("")
                        ? ", ignores KO resist"
                        : ", ignores blinks and KO resist";
                    stringBuilder.Append(string.Format("{0}: {1} {2}% {3} HP {4}damage{5}{6}{7}", name, multiTarget17,
                        o.Arg1, fracHp5, damageThreshold1, ignoreReflect19, str56, enemyActionStatus3));
                    break;
                case 214:
                    str1 = parseUndefArgs(new int[9]
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
                    var enemyActionStatus4 = parseEnemyActionStatus(o);
                    var str57 = o.Arg2 != 0 || isSingleTarget(o.TargetRange)
                        ? ""
                        : ", divided by number of targets hit";
                    var sameTarget19 = parseSameTarget(o.Arg4, o.TargetRange);
                    var multiTarget18 = parseMultiTarget(o.Arg3, sameTarget19, target1);
                    var ignoreReflect20 = parseIgnoreReflect(o.Arg5, o.TargetRange, ability.ExerciseType);
                    var ignoreBlink5 = parseIgnoreBlink(o.Arg6);
                    stringBuilder.Append(string.Format("{0}: {1} {2} {3}fixed damage{4}{5}{6}{7}", name, multiTarget18,
                        o.Arg1, o.Arg1 > 9999 ? damageThreshold1 : (object) "", str57, ignoreReflect20, ignoreBlink5,
                        enemyActionStatus4));
                    break;
                case 215:
                    str1 = parseUndefArgs(new int[12]
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
                    var enemyActionStatus5 = parseEnemyActionStatus(o);
                    str5 = parseIgnoreBlink(o.Arg3);
                    stringBuilder.Append(string.Format("{0}: {1} - reduces HP to {2}{3}", name, target1, o.Arg1,
                        enemyActionStatus5));
                    if ((uint) o.MaxDamageThreshold > 0U) stringBuilder.Append(", overflowable");
                    break;
                case 217:
                    str1 = parseUndefArgs(new int[11]
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
                        str6 = string.Format("{0}x {1}", o.Arg3, parseSameTarget(o.Arg4, o.TargetRange));
                    var enemyActionStatus6 = parseEnemyActionStatus(o);
                    var ignoreReflect21 = parseIgnoreReflect(o.Arg6, o.TargetRange, ability.ExerciseType);
                    stringBuilder.Append(string.Format("{0}: {1} {2} factor {3} heal {4}heal{5}{6}", name, target1,
                        str6, o.Arg1, damageThreshold1, enemyActionStatus6, ignoreReflect21));
                    break;
                case 218:
                    str1 = parseUndefArgs(new int[11]
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
                    var str58 = o.Arg1 <= 9999 || o.MinDamageThreshold != 0 ? string.Concat(o.Arg1) : "9999";
                    if (o.Arg3 != 1)
                        str6 = string.Format("{0}x {1}", o.Arg3, parseSameTarget(o.Arg4, o.TargetRange));
                    var enemyActionStatus7 = parseEnemyActionStatus(o);
                    var ignoreReflect22 = parseIgnoreReflect(o.Arg6, o.TargetRange, ability.ExerciseType);
                    stringBuilder.Append(string.Format("{0}: {1} {2}{3}-HP fixed {4}heal{5}{6}", name, target1, str6,
                        str58, o.Arg1 > 9999 ? damageThreshold1 : (object) "", enemyActionStatus7, ignoreReflect22));
                    break;
                case 220:
                    str1 = parseUndefArgs(new int[10]
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
                    var enemyActionStatus8 = parseEnemyActionStatus(o);
                    var generalStatus11 = parseGeneralStatus(0, o.Arg1, o.Arg2, o.Arg3, o.Arg4, o);
                    if (generalStatus11.Contains("[Status bundle not found]"))
                        generalStatus11 = parseGeneralStatus(o.Arg1, 0, o.Arg2, o.Arg3, o.Arg4, o);
                    var str59 = generalStatus11.Replace(", ", "");
                    var ignoreReflect23 = parseIgnoreReflect(o.Arg5, o.TargetRange, ability.ExerciseType);
                    stringBuilder.Append(string.Format("{0}: {1} {2}{3}{4}", name, target1, str59, enemyActionStatus8,
                        ignoreReflect23));
                    break;
                case 221:
                    str1 = parseUndefArgs(new int[12]
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
                    var enemyActionStatus9 = parseEnemyActionStatus(o);
                    var ignoreReflect24 = parseIgnoreReflect(o.Arg3, o.TargetRange, ability.ExerciseType);
                    var str60 = parseStatusFactor(o.Arg2, o) + "% chance to " +
                                parseRemoveStatusBundle(o.Arg1).Replace("Removes", "remove");
                    stringBuilder.Append(string.Format("{0}: {1} {2}{3}{4}", name, target1, str60, enemyActionStatus9,
                        ignoreReflect24));
                    break;
                default:
                    stringBuilder.Append(string.Format("Action ID {0} not yet implemented", ability.ActionId));
                    break;
            }

            if (flag1 && options1.StatusAilmentsId != 0 && statusFound(o.StatusAilmentsId))
                stringBuilder.Append(parseStatusAndFactor(o.StatusAilmentsFactor, o.StatusAilmentsId, o));
            if ((options1.CounterEnable == 0) & flag1 && options1.TargetRange != 3 && options1.TargetSegment != 2)
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
                    return num + "th";
                default:
                    switch (num % 10U)
                    {
                        case 1:
                            return num + "st";
                        case 2:
                            return num + "nd";
                        case 3:
                            return num + "rd";
                        default:
                            return num + "th";
                    }
            }
        }

        private void ConstraintSanityChecks(List<DataEnemyConstraints> constrList, uint enemyId)
        {
            foreach (var enemyConstraintsList in new List<List<DataEnemyConstraints>>
            {
                constrList.FindAll(x => x.ConstraintType == 1002U && x.EnemyStatusId == 0U).OrderBy(x => x.AbilityTag)
                    .ToList(),
                constrList.FindAll(x => x.ConstraintType == 1002U && (int) x.EnemyStatusId == (int) enemyId)
                    .OrderBy(x => x.AbilityTag).ToList(),
                constrList.FindAll(x => x.ConstraintType == 1004U && x.EnemyStatusId == 0U)
                    .OrderBy((Func<DataEnemyConstraints, string>) (x => x.AbilityTag)).ToList<DataEnemyConstraints>(),
                constrList
                    .FindAll((Predicate<DataEnemyConstraints>) (x =>
                        x.ConstraintType == 1004U && (int) x.EnemyStatusId == (int) enemyId))
                    .OrderBy<DataEnemyConstraints, string>((Func<DataEnemyConstraints, string>) (x => x.AbilityTag))
                    .ToList<DataEnemyConstraints>(),
                constrList
                    .FindAll(
                        (Predicate<DataEnemyConstraints>) (x => x.ConstraintType == 2003U && x.EnemyStatusId == 0U))
                    .OrderBy<DataEnemyConstraints, string>((Func<DataEnemyConstraints, string>) (x => x.AbilityTag))
                    .ToList<DataEnemyConstraints>(),
                constrList
                    .FindAll((Predicate<DataEnemyConstraints>) (x =>
                        x.ConstraintType == 2003U && (int) x.EnemyStatusId == (int) enemyId))
                    .OrderBy<DataEnemyConstraints, string>((Func<DataEnemyConstraints, string>) (x => x.AbilityTag))
                    .ToList<DataEnemyConstraints>(),
                constrList
                    .FindAll(
                        (Predicate<DataEnemyConstraints>) (x => x.ConstraintType == 2004U && x.EnemyStatusId == 0U))
                    .OrderBy<DataEnemyConstraints, string>((Func<DataEnemyConstraints, string>) (x => x.AbilityTag))
                    .ToList<DataEnemyConstraints>(),
                constrList
                    .FindAll((Predicate<DataEnemyConstraints>) (x =>
                        x.ConstraintType == 2004U && (int) x.EnemyStatusId == (int) enemyId))
                    .OrderBy<DataEnemyConstraints, string>((Func<DataEnemyConstraints, string>) (x => x.AbilityTag))
                    .ToList<DataEnemyConstraints>(),
                constrList
                    .FindAll(
                        (Predicate<DataEnemyConstraints>) (x => x.ConstraintType == 2006U && x.EnemyStatusId == 0U))
                    .OrderBy<DataEnemyConstraints, string>((Func<DataEnemyConstraints, string>) (x => x.AbilityTag))
                    .ToList<DataEnemyConstraints>(),
                constrList
                    .FindAll((Predicate<DataEnemyConstraints>) (x =>
                        x.ConstraintType == 2006U && (int) x.EnemyStatusId == (int) enemyId))
                    .OrderBy<DataEnemyConstraints, string>((Func<DataEnemyConstraints, string>) (x => x.AbilityTag))
                    .ToList<DataEnemyConstraints>()
            })
                for (var index = 0; index < enemyConstraintsList.Count - 1; ++index)
                {
                    uint result1;
                    uint result2;
                    if (enemyConstraintsList[index].AbilityTag == enemyConstraintsList[index + 1].AbilityTag &&
                        uint.TryParse(enemyConstraintsList[index].ConstraintValue, out result1) &&
                        uint.TryParse(enemyConstraintsList[index + 1].ConstraintValue, out result2))
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

            foreach (var enemyConstraintsList in new List<List<DataEnemyConstraints>>
            {
                constrList.FindAll(x => x.ConstraintType == 1005U && x.EnemyStatusId == 0U).OrderBy(x => x.AbilityTag)
                    .ToList(),
                constrList.FindAll(x => x.ConstraintType == 1005U && (int) x.EnemyStatusId == (int) enemyId)
                    .OrderBy(x => x.AbilityTag).ToList(),
                constrList.FindAll(x => x.ConstraintType == 2001U && x.EnemyStatusId == 0U)
                    .OrderBy((Func<DataEnemyConstraints, string>) (x => x.AbilityTag)).ToList<DataEnemyConstraints>(),
                constrList
                    .FindAll((Predicate<DataEnemyConstraints>) (x =>
                        x.ConstraintType == 2001U && (int) x.EnemyStatusId == (int) enemyId))
                    .OrderBy<DataEnemyConstraints, string>((Func<DataEnemyConstraints, string>) (x => x.AbilityTag))
                    .ToList<DataEnemyConstraints>(),
                constrList
                    .FindAll(
                        (Predicate<DataEnemyConstraints>) (x => x.ConstraintType == 2002U && x.EnemyStatusId == 0U))
                    .OrderBy<DataEnemyConstraints, string>((Func<DataEnemyConstraints, string>) (x => x.AbilityTag))
                    .ToList<DataEnemyConstraints>(),
                constrList
                    .FindAll((Predicate<DataEnemyConstraints>) (x =>
                        x.ConstraintType == 2002U && (int) x.EnemyStatusId == (int) enemyId))
                    .OrderBy<DataEnemyConstraints, string>((Func<DataEnemyConstraints, string>) (x => x.AbilityTag))
                    .ToList<DataEnemyConstraints>(),
                constrList
                    .FindAll(
                        (Predicate<DataEnemyConstraints>) (x => x.ConstraintType == 2005U && x.EnemyStatusId == 0U))
                    .OrderBy<DataEnemyConstraints, string>((Func<DataEnemyConstraints, string>) (x => x.AbilityTag))
                    .ToList<DataEnemyConstraints>(),
                constrList
                    .FindAll((Predicate<DataEnemyConstraints>) (x =>
                        x.ConstraintType == 2005U && (int) x.EnemyStatusId == (int) enemyId))
                    .OrderBy<DataEnemyConstraints, string>((Func<DataEnemyConstraints, string>) (x => x.AbilityTag))
                    .ToList<DataEnemyConstraints>()
            })
                for (var index = 0; index < enemyConstraintsList.Count - 1; ++index)
                {
                    uint result1;
                    uint result2;
                    if (enemyConstraintsList[index].AbilityTag == enemyConstraintsList[index + 1].AbilityTag &&
                        uint.TryParse(enemyConstraintsList[index].ConstraintValue, out result1) &&
                        uint.TryParse(enemyConstraintsList[index + 1].ConstraintValue, out result2))
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

        private string parseTarget(DataEnemyAbilityOptions options)
        {
            var str = parseTargetHelper(options);
            if (str.Contains("STATUSEFFECT"))
            {
                if ((uint) options.StatusAilmentsId > 0U)
                    str = str.Replace("STATUSEFFECT", parseStatus(options.StatusAilmentsId));
                else if ((uint) options.Arg1 > 0U)
                    try
                    {
                        str = str.Replace("STATUSEFFECT",
                            Enum.GetName(typeof(SchemaConstants.StatusID), options.Arg1).Replace("_", " "));
                    }
                    catch (NullReferenceException ex)
                    {
                    }

                str.Replace("STATUSEFFECT", "Status Effect");
            }

            return str;
        }

        private string parseTargetHelper(DataEnemyAbilityOptions options)
        {
            var targetRange = options.TargetRange;
            var targetMethod = options.TargetMethod;
            var targetSegment = options.TargetSegment;
            if (targetRange == 1 || targetRange == 151 || targetRange == 153)
            {
                var str = "";
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
                            return targetRange == 1
                                ? "[ST - highest %HP]"
                                : string.Format("[ST - {0}unit with highest %HP]", str);
                        case 2:
                            return targetRange == 1
                                ? "[ST - lowest %HP]"
                                : string.Format("[ST - {0}unit with lowest %HP]", str);
                        case 3:
                            return string.Format("[ST - {0}unit with STATUSEFFECT]", str);
                        case 4:
                            return string.Format("[ST - {0}unit without STATUSEFFECT]", str);
                        case 5:
                            num = 1;
                            break;
                        default:
                            num = targetMethod == 6 ? 1 : 0;
                            break;
                    }

                    if (num != 0)
                        return targetRange == 1 ? "ST" : string.Format("[ST - {0}unit]", str);
                    switch (targetMethod)
                    {
                        case 7:
                            return targetRange == 1
                                ? "[ST - highest HP]"
                                : string.Format("[ST - {0}unit with highest HP]", str);
                        case 8:
                            return targetRange == 1
                                ? "[ST - lowest HP]"
                                : string.Format("[ST - {0}unit with lowest HP]", str);
                        case 9:
                            return string.Format("[ST - {0}unit with status removable by Esuna]", str);
                        case 10:
                            return string.Format("[ST - {0}unit with status removable by Dispel]", str);
                        default:
                            if (targetMethod == 12)
                                return string.Format("[ST - {0}unit with KO, or lowest %HP]", str);
                            if (targetMethod == 13)
                                return targetRange == 1
                                    ? "[ST - weighted towards high HP]"
                                    : string.Format("[ST - {0}unit, weighted towards high HP]", str);
                            if (targetMethod == 201)
                                return targetRange == 1
                                    ? "[ST - uses Smart AI targeting]"
                                    : string.Format("[ST - targets a {0}unit using Smart AI]", str);
                            if (targetMethod == 202)
                                return targetRange == 1
                                    ? "[ST - uses reflect-ignoring Smart AI targeting]"
                                    : string.Format("[ST - targets a {0}unit using reflect-ignoring Smart AI]", str);
                            break;
                    }
                }

                if (targetSegment == 2)
                {
                    if (singleEnemy)
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

        private string parseMultiTarget(int arg, string sameTarget, string target)
        {
            return arg != 1 && (uint) arg > 0U
                ? string.Format("{0}x {1}{2} attacks,", arg, sameTarget, target)
                : string.Format("{0}", target);
        }

        private string damageCalculateParamLookup(int arg)
        {
            string name;
            try
            {
                name = Enum.GetName(typeof(SchemaConstants.damageCalculateParamAdjust), arg);
            }
            catch (NullReferenceException ex)
            {
                return "[Param Adjust type not found]";
            }

            return name;
        }

        private string parseElement(int arg)
        {
            return arg == 0 || arg == 199 ? "" : Enum.GetName(typeof(SchemaConstants.ElementID), arg).ToLower() + " ";
        }

        private string parseMultipleElements(int[] args)
        {
            var stringList = new List<string>();
            foreach (var num in args)
                switch (num)
                {
                    case 0:
                        continue;
                    case 199:
                        stringList.Add("NE");
                        break;
                    default:
                        var element = Enum.GetName(typeof(SchemaConstants.ElementID), num);
                        if (element == null)
                            element = num.ToString();
                        stringList.Add(element.ToLower());
                        break;
                }

            var str = string.Join("/", stringList);
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
                str = Enum.GetName(typeof(SchemaConstants.StatusID), arg).Replace("_", " ");
            }
            catch (NullReferenceException ex)
            {
                return "[Status type not found]";
            }

            return str == "Doom" ? "Doom 60" : str;
        }

        private string parseMultipleStatus(int[] arg)
        {
            var intList = new List<int>();
            var source = new List<string>();
            var str = "";
            foreach (var key in arg)
                if (key != 0 && !intList.Contains(key) && !SchemaConstants.customParams.TryGetValue(key, out str) &&
                    (key < 828 || key > 836) && key != 535)
                {
                    var status = parseStatus(key);
                    if (!status.Equals(""))
                        source.Add(status);
                    intList.Add(key);
                }

            if (source.Count == 0)
                return "";
            if (source.Count == 1)
                return source[0];
            if (source.Count == 2)
                return string.Format("{0} and {1}", source[0], source[1]);
            return source.Count >= 3
                ? string.Join(", ", source.ToArray(), 0, source.Count - 1) + ", and " + source.LastOrDefault()
                : "";
        }

        private string parseRemoveStatusBundle(int arg)
        {
            if (arg == 0)
                return "";
            var statusBundle = parseStatusBundle(arg);
            return arg == 2 || arg == 3 || arg == 4 || arg == 9 ? statusBundle : "Removes " + statusBundle;
        }

        private string parseStatusBundle(int arg)
        {
            var str1 = "";
            if (arg == 0)
                return str1;
            string str2;
            try
            {
                str2 = Enum.GetName(typeof(SchemaConstants.StatusAilmentsBundle), arg).Replace("_", " ");
            }
            catch (NullReferenceException ex)
            {
                return "[Status bundle not found]";
            }

            return str2;
        }

        private string parseStatusOrBundle(int arg)
        {
            var str = parseStatusBundle(arg);
            if (str == "[Status bundle not found]")
                str = parseStatus(arg);
            return str;
        }

        private bool statusFound(int arg)
        {
            try
            {
                Enum.GetName(typeof(SchemaConstants.StatusID), arg);
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
            return o.TargetRange == 3 || o.TargetSegment == 2
                ? string.Format(", {0}% chance to apply", arg)
                : string.Format(", {0}% chance to apply", arg * 3 + 3);
        }

        private string parseForceInflict(int rate, int status, DataEnemyAbilityOptions o)
        {
            return rate == 998 && o.TargetRange != 3 && o.TargetSegment != 2 && (status == 200 || status == 201 ||
                status == 202 || status == 203 || status == 205 || status == 206 || status == 210 || status == 211 ||
                status == 212 || status == 215 || status == 229 || status == 242 || status == 292)
                ? " (blockable by Astra)"
                : "";
        }

        private string parseForceInflictMulti(int rate, string statuses, DataEnemyAbilityOptions o)
        {
            var lower = statuses.ToLower();
            return rate == 998 && o.TargetRange != 3 && o.TargetSegment != 2 && (lower.Contains("poison") ||
                lower.Contains("silence") || lower.Contains("paralyze") || lower.Contains("confuse") ||
                lower.Contains("slow") || lower.Contains("stop") || lower.Contains("blind") ||
                lower.Contains("sleep") || lower.Contains("sap") || lower.Contains("stun") ||
                lower.Contains("frozen") || lower.Contains("petrify") || lower.Contains("berserk"))
                ? " (blockable by Astra)"
                : "";
        }

        private string parseForceInflictBundle(int rate, int bundle, DataEnemyAbilityOptions o)
        {
            var lower = parseStatusBundle(bundle).ToLower();
            return rate == 998 && o.TargetRange != 3 && o.TargetSegment != 2 && (lower.Contains("poison") ||
                lower.Contains("silence") || lower.Contains("paralyze") || lower.Contains("paralysis") ||
                lower.Contains("confuse") || lower.Contains("confusion") || lower.Contains("slow") ||
                lower.Contains("stop") || lower.Contains("blind") || lower.Contains("blinded") ||
                lower.Contains("sleep") || lower.Contains("petrify") || lower.Contains("petrifaction") ||
                lower.Contains("berserk") || lower.Contains("berserker") || lower.Contains("sap") ||
                lower.Contains("stun") || lower.Contains("stan") || lower.Contains("frozen"))
                ? " (blockable by Astra)"
                : "";
        }

        private string parseStatusAndFactor(int factor, int status, DataEnemyAbilityOptions o)
        {
            return factor == 0 || status == 0
                ? ""
                : string.Format("{0} {1}{2}", parseStatusFactorPhrase(factor, o), parseStatus(status),
                    parseForceInflict(factor, status, o));
        }

        private string parseStatusOrBundleAndFactor(int factor, int id, DataEnemyAbilityOptions o)
        {
            if (factor == 0 || id == 0)
                return "";
            return parseStatusBundle(id) == "[Status bundle not found]"
                ? string.Format("{0} {1}{2}", parseStatusFactorPhrase(factor, o), parseStatus(id),
                    parseForceInflict(factor, id, o))
                : string.Format("{0} {1}{2}", parseStatusFactorPhrase(factor, o), parseStatusBundle(id),
                    parseForceInflictBundle(factor, id, o));
        }

        private string parseCustomParam(int arg)
        {
            if (arg == 0)
                return "";
            var str = "";
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
            return parseGeneralStatus(id, bundleid, factor, boostrate, duration, 0, o);
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
            var str = "";
            if ((uint) id > 0U)
            {
                if (SchemaConstants.customParams.TryGetValue(id, out str))
                {
                    var customParam = parseCustomParam(id);
                    var buffAmount = parseBuffAmount(boostrate);
                    var duration1 = parseDuration(duration);
                    if (forcehit == 0)
                        str = string.Format("{0} {1} {2}{3}", parseStatusFactorPhrase(factor, o), customParam,
                            buffAmount, duration1);
                    else
                        str = string.Format(", auto-hit {0} {1}{2}", customParam, buffAmount, duration1);
                }
                else
                {
                    str = (id < 828 || id > 836) && (o.StatusAilmentsId != 535 || id < 100 || id > 108)
                        ? forcehit != 0 ? string.Format(", auto-hit {0}", parseStatus(id)) :
                        string.Format("{0} {1}{2}", parseStatusFactorPhrase(factor, o), parseStatus(id),
                            parseForceInflict(factor, id, o))
                        : parseVariableImperil(id, factor, duration, boostrate, o);
                }
            }

            if ((uint) bundleid > 0U)
            {
                var statusBundle = parseStatusBundle(bundleid);
                parseStatusFactor(factor, o);
                str = forcehit != 0
                    ? str + string.Format(", auto-hit {0}", statusBundle)
                    : str + string.Format("{0} {1}{2}", parseStatusFactorPhrase(factor, o), statusBundle,
                        parseForceInflictBundle(factor, id, o));
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
            var str1 = "";
            if (id == 0 || factor == 0)
                return str1;
            var statusBundle = parseStatusBundle(id);
            string str2;
            if (statusBundle != "[Status bundle not found]")
            {
                parseStatusFactor(factor, o);
                str2 = forcehit != 0
                    ? str1 + string.Format(", auto-hit {0}", statusBundle)
                    : str1 + string.Format("{0} {1}{2}", parseStatusFactorPhrase(factor, o), statusBundle,
                        parseForceInflictBundle(factor, id, o));
            }
            else if (SchemaConstants.customParams.TryGetValue(id, out str1))
            {
                var customParam = parseCustomParam(id);
                var buffAmount = parseBuffAmount(boostrate);
                var duration1 = parseDuration(duration);
                if (forcehit == 0)
                    str2 = string.Format("{0} {1} {2}{3}", parseStatusFactorPhrase(factor, o), customParam, buffAmount,
                        duration1);
                else
                    str2 = string.Format(", auto-hit {0} {1}{2}", customParam, buffAmount, duration1);
            }
            else
            {
                str2 = (id < 828 || id > 836) && (o.StatusAilmentsId != 535 || id < 100 || id > 108)
                    ? forcehit != 0 ? string.Format(", auto-hit {0}", parseStatus(id)) :
                    string.Format("{0} {1}{2}", parseStatusFactorPhrase(factor, o), parseStatus(id),
                        parseForceInflict(factor, id, o))
                    : parseVariableImperil(id, factor, duration, boostrate, o);
            }

            return str2;
        }

        private string parseEnemyActionStatus(DataEnemyAbilityOptions o)
        {
            var stringBuilder = new StringBuilder();
            var flag = false;
            var str1 = parseGeneralStatusOrBundle(o.Arg21, o.Arg22, o.Arg23, o.Arg24, 0, o);
            if ((uint) o.Arg25 > 0U)
                str1 = str1.Replace(", ", ", self ");
            else if (parseIgnoreAstra(o.Arg26, o.TargetSegment, o.Arg21) != "")
                flag = true;
            var str2 = parseGeneralStatusOrBundle(o.Arg16, o.Arg17, o.Arg18, o.Arg19, 0, o);
            if ((uint) o.Arg20 > 0U)
                str2 = str2.Replace(", ", ", self ");
            else if (!flag && parseIgnoreAstra(o.Arg26, o.TargetSegment, o.Arg16) != "")
                flag = true;
            if ((uint) o.Arg25 > 0U == (uint) o.Arg20 > 0U && o.Arg21 == o.Arg16)
                str2 = "";
            var str3 = parseStatusOrBundleAndFactor(o.StatusAilmentsFactor, o.StatusAilmentsId, o);
            if (!flag && parseIgnoreAstra(o.Arg26, o.TargetSegment, o.StatusAilmentsId) != "")
                flag = true;
            if (o.Arg20 == 0 && o.Arg16 == o.StatusAilmentsId || o.Arg25 == 0 && o.Arg21 == o.StatusAilmentsId)
                str3 = "";
            if (str2 != "" && str1 != "" && (uint) o.Arg25 > 0U == (uint) o.Arg20 > 0U && o.Arg22 == o.Arg17)
            {
                str1 = str1.Replace(parseStatusFactorPhrase(o.Arg22, o), " and");
                if (str3 != "" && o.Arg20 == 0 && o.Arg17 == o.StatusAilmentsFactor)
                    str2 = str2.Replace(parseStatusFactorPhrase(o.Arg17, o), ", ");
            }
            else if (str3 != "" && str2 != "" && o.Arg20 == 0 && o.Arg17 == o.StatusAilmentsFactor)
            {
                str2 = str2.Replace(parseStatusFactorPhrase(o.Arg17, o), " and");
            }

            var str4 = "";
            if (o.Arg27 != 0 && (uint) o.Arg28 > 0U)
            {
                var statusOrBundle = parseStatusOrBundle(o.Arg27);
                str4 = (uint) o.Arg29 <= 0U
                    ? string.Format(", {0}% chance to remove {1}", parseStatusFactor(o.Arg28, o), statusOrBundle)
                    : string.Format(", {0}% chance to remove {1} from self", o.Arg28, statusOrBundle);
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
            return parseVariableImperil(id, factor, duration, rate, 0, o);
        }

        private string parseVariableImperil(
            int id,
            int factor,
            int duration,
            int rate,
            int forcehit,
            DataEnemyAbilityOptions o)
        {
            var num1 = 0;
            num1 = o.TargetRange != 3 && o.TargetSegment != 2 ? rate * 3 + 3 : rate;
            if (factor == 0)
                return "";
            var str = "";
            if (id >= 828 && id <= 836)
            {
                if (SchemaConstants.imperilX.TryGetValue(id, out str))
                {
                    var duration1 = parseDuration(duration == 0 ? 25000 : duration);
                    var num2 = -10 * factor;
                    if (forcehit != 0)
                        return string.Format(", auto-hit {0} {1}%{2}", str, num2, duration1);
                    return string.Format("{0} {1} {2}%{3}", parseStatusFactorPhrase(rate, o), str, num2, duration1);
                }
            }
            else if (o.StatusAilmentsId == 535 && id >= 100 && id <= 108)
            {
                var name = Enum.GetName(typeof(SchemaConstants.ElementID), id);
                var duration1 = parseDuration(duration == 0 ? 25000 : duration);
                var num2 = -10 * factor;
                if (forcehit != 0)
                    return string.Format(", auto-hit {0} {1}%{2}", name, num2, duration1);
                return string.Format("{0} Imperil {1} {2}%{3}", parseStatusFactorPhrase(rate, o), name, num2,
                    duration1);
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
                return string.Format("piercing ^{0} ", (expFactor / 100M).ToString("0.##"));
            return type == 2 ? "buff-ignoring " : "";
        }

        private string parseFracHP(int useMax)
        {
            return useMax == 0 ? "current" : "max";
        }

        private bool isSingleTarget(int range)
        {
            return range == 1 || range == 151 || range == 153;
        }

        private string parseDuration(int arg)
        {
            return arg > 0 ? string.Format(" for {0} seconds", (arg / 1000M).ToString("0.###")) : "";
        }

        private string parseIgnoreAstra(int arg, int segment, int statusID)
        {
            if (arg == 0 || segment == 2 || segment == 5 || segment == 7)
                return "";
            int num;
            if (arg > 0)
                num = new int[13]
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
                }.Contains(statusID)
                    ? 1
                    : 0;
            else
                num = 0;
            return num != 0 ? ", ignores Astra" : "";
        }

        private string parseIgnoreReflect(int arg, int range, uint exType)
        {
            return arg > 0 && isSingleTarget(range) && (exType == 3U || exType == 4U) ? ", ignores Reflect" : "";
        }

        private string parseIgnoreMblink(int arg)
        {
            return arg == 0 ? "" : ", ignores Magic Blink";
        }

        private string parseIgnoreBlink(int arg)
        {
            return arg == 0 ? "" : ", ignores Blink";
        }

        private string parseCrit(int arg)
        {
            return arg > 0 ? string.Format(", with a +{0}% chance to crit", arg) : "";
        }

        private string parseCritDamage(int arg)
        {
            decimal num = arg;
            return arg > 0 ? string.Format(", with a crit damage multiplier of {0}x", (num / 10M).ToString("0.#")) : "";
        }

        private string parseMinDmg(int arg)
        {
            return arg <= 1 ? "" : string.Format(" [minimum damage: {0}]", arg);
        }

        private string parseDrain(int arg)
        {
            return arg == 0 ? "" : string.Format(" with {0}% HP drain", arg);
        }

        private string parseExpFactor(int arg)
        {
            return "^" + (arg / 100M).ToString("0.##");
        }

        private string parseBuffAmount(int arg)
        {
            return arg == 0 ? "" : arg.ToString("+0;-#") + "%";
        }

        private string parseUndefArgs(int[] args)
        {
            foreach (uint num in args)
                if (num > 0U)
                    return " [One or more args not parsed]";
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

        private string parseBarterRate(int arg)
        {
            return arg == 0
                ? ""
                : string.Format(", damages the user for {0}% of Max HP", (arg / 10M).ToString("0.#"));
        }

        public string translateAbility(string name)
        {
            var str1 = name;
            var str2 = "";
            if (str1.Contains('】'))
            {
                str2 = name.Substring(0, name.LastIndexOf('】') + 1);
                str1 = str1.Substring(str1.LastIndexOf('】') + 1);
            }

            if (SchemaConstants.abilityNames.ContainsKey(str1))
                return str2 + SchemaConstants.abilityNames[str1];
            if (new Regex("[0-9]+$").Match(str1).Success)
            {
                var str3 = Regex.Match(str1, "[0-9]+", RegexOptions.RightToLeft).ToString();
                var key = str1.Substring(0, str1.IndexOf(str3));
                if (SchemaConstants.abilityNames.ContainsKey(key))
                    return str2 + SchemaConstants.abilityNames[key] + str3;
            }

            return name;
        }
    }
}