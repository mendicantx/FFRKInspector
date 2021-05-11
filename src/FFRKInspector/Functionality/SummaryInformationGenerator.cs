using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FFRKInspector.GameData;
using FFRKInspector.GameData.Converters;
using FFRKInspector.Proxy;

namespace FFRKInspector.Functionality
{
    public class SummaryInformationGenerator
    {
        public StringBuilder GetSummaryInformation(ComboBox comboBoxEnemySelection, CheckBox checkBoxCastTimes, CheckBox checkBoxEnumerate, CheckBox checkBoxRatesAsFractions, CheckBox checkBoxTranslate)
        {
            var itemNameGetter = new ItemNameGetter();
            var statusVulnerabilityGetter = new StatusVulnerabilityGetter();
            var abilitySummaryHelper = new AbilitySummaryHelper();

            var summary = new StringBuilder();
            if (FFRKProxy.Instance.GameState.ActiveDungeon != null)
            {
                var activeDungeon = FFRKProxy.Instance.GameState.ActiveDungeon;
                var battleId = (int)FFRKProxy.Instance.GameState.ActiveBattle.Battle.BattleId;
                DataBattle dataBattle = null;
                foreach (var battle in activeDungeon.Battles)
                    if (battle.Id == battleId)
                        dataBattle = battle;

                summary.Append(string.Format("*****\n\n##{0}  \n\n", dataBattle.Name));

                var medalConditions = GetMedalConditions(activeDungeon, battleId);
                if (medalConditions.Length > 0)
                    summary.Append("**Medal Conditions:** " + medalConditions + "\n\n");

                var prizes = activeDungeon.UserDungeon.Prizes;

                var dungeonPrizes = GetDungeonPrizes(prizes, itemNameGetter);
                if (dungeonPrizes.Length > 0)
                    summary.Append(string.Format("**Clear Reward{0}:**  \n\n{1}\n", dungeonPrizes.Length != 1 ? "s" : (object)"", dungeonPrizes));


                var masteryRewards = GetMasteryRewards(prizes, itemNameGetter);
                if (masteryRewards.Length > 0)
                    summary.Append(string.Format("**Mastery Reward{0}:**  \n\n{1}\n", masteryRewards.Length != 1 ? "s" : (object)"", masteryRewards));

                var firstClearRewards = GetFirstClearRewards(prizes, itemNameGetter);
                if (firstClearRewards.Length > 0)
                    summary.Append(string.Format("**First Clear Reward{0}:**  \n\n{1}\n", firstClearRewards.Length != 1 ? "s" : (object)"", firstClearRewards));

            }

            var basicEnemyParentInfoList = new List<BasicEnemyParentInfo>();
            foreach (var obj in comboBoxEnemySelection.Items)
                basicEnemyParentInfoList.Add((BasicEnemyParentInfo)obj);

            var hasStatusVulnerabilities = true;
            var statusVulnerabilities = statusVulnerabilityGetter.GetStatusVulnerabilities(basicEnemyParentInfoList[0].Phases.First().EnemyStatusImmunity);
            foreach (var basicEnemyParentInfo in basicEnemyParentInfoList)
                foreach (var phase in basicEnemyParentInfo.Phases)
                    if (!statusVulnerabilities.Equals(statusVulnerabilityGetter.GetStatusVulnerabilities(phase.EnemyStatusImmunity)))
                        hasStatusVulnerabilities = false;

            AddEnemyStats(basicEnemyParentInfoList, summary, hasStatusVulnerabilities, statusVulnerabilityGetter);

            AddElementalResitances(summary, basicEnemyParentInfoList);

            AddBreakEffectiveness(summary, basicEnemyParentInfoList);

            if (hasStatusVulnerabilities)
                summary.Append(string.Format("**Status Vulnerabilities{0}**: {1}  \n", basicEnemyParentInfoList.Count == 1 ? "" : (object)" (all)", statusVulnerabilities));

            if (basicEnemyParentInfoList.Count == 1 && basicEnemyParentInfoList[0].Phases.Count() == 1)
                summary.Append("\n##Moveset\n\n");
            else
                summary.Append("\n##Movesets\n\n");

            var hasVariableCastTimes = true;
            var enemyCastTime = basicEnemyParentInfoList[0].Phases.First().EnemyCastTime;
            foreach (var basicEnemyParentInfo in basicEnemyParentInfoList)
                foreach (var phase in basicEnemyParentInfo.Phases)
                    if (phase.EnemyCastTime.Equals("Variable") || !phase.EnemyCastTime.Equals(enemyCastTime))
                        hasVariableCastTimes = false;

            if (hasVariableCastTimes)
            {
                var str2 = enemyCastTime.Replace("sec", "seconds");
                if (str2.Equals("1 seconds"))
                    str2 = "1 second";
                summary.Append(string.Format(
                    "All (non-interrupt) enemy abilities in this fight have a cast time of {0}.\n\n", str2));
            }

            AppendBossHealthGatesAndMoveSummary(comboBoxEnemySelection, checkBoxCastTimes, checkBoxEnumerate, checkBoxRatesAsFractions, checkBoxTranslate, basicEnemyParentInfoList, summary, hasVariableCastTimes, abilitySummaryHelper);

            return summary;
        }

        private static void AppendBossHealthGatesAndMoveSummary(ComboBox comboBoxEnemySelection, CheckBox checkBoxCastTimes,
            CheckBox checkBoxEnumerate, CheckBox checkBoxRatesAsFractions, CheckBox checkBoxTranslate,
            List<BasicEnemyParentInfo> basicEnemyParentInfoList, StringBuilder summary, bool hasVariableCastTimes,
            AbilitySummaryHelper abilitySummaryHelper)
        {
            for (var index1 = 0; index1 < basicEnemyParentInfoList.Count; ++index1)
            {
                var myEnemyParent = basicEnemyParentInfoList[index1];
                var basicEnemyInfoList = new List<BasicEnemyInfo>();
                foreach (var phase in myEnemyParent.Phases)
                    basicEnemyInfoList.Add(phase);
                for (var index2 = 0; index2 < basicEnemyInfoList.Count; ++index2)
                {
                    var enemy = basicEnemyInfoList[index2];
                    var enemyName = myEnemyParent.EnemyName;
                    if (basicEnemyInfoList.Count == 2 || basicEnemyInfoList.Count == 3)
                    {
                        if (index2 == 0)
                            enemyName += " - Default";
                        if (index2 == 1)
                            enemyName += " - Weak";
                        if (index2 == 2)
                            enemyName += " - Very Weak";
                    }
                    else if (basicEnemyInfoList.Count >= 4)
                    {
                        enemyName += string.Format(" - Phase {0}", index2 + 1);
                    }

                    if (basicEnemyInfoList.Count >= 2)
                    {
                        var flag2 = false;
                        var flag3 = false;
                        var str2 = "hp_rate_to_phase_" + basicEnemyInfoList.Count;
                        var str3 = "hp_rate_to_phase_" + (basicEnemyInfoList.Count + 1);
                        if (basicEnemyInfoList.Count == 6)
                            str2 = "hp_rate_to_phase_5";
                        foreach (var aiArg in myEnemyParent.AiArgs)
                            if (aiArg.Tag.Equals(str2) ||
                                basicEnemyInfoList.Count == 2 && aiArg.Tag.Equals("hp_rate_to_weak") ||
                                basicEnemyInfoList.Count == 3 && aiArg.Tag.Equals("hp_rate_to_very_weak"))
                                flag2 = true;
                            else if (aiArg.Tag.Equals(str3) ||
                                     basicEnemyInfoList.Count == 1 && aiArg.Tag.Equals("hp_rate_to_weak") ||
                                     basicEnemyInfoList.Count == 2 && aiArg.Tag.Equals("hp_rate_to_very_weak"))
                                flag3 = true;
                        if (flag2 && !flag3)
                        {
                            DataAIArgs dataAiArgs1 = null;
                            DataAIArgs dataAiArgs2 = null;
                            var str4 = "hp_rate_to_phase_" + (index2 + 1);
                            var str5 = "hp_rate_to_phase_" + (index2 + 2);
                            var result1 = -1;
                            var result2 = -1;
                            foreach (var aiArg in myEnemyParent.AiArgs)
                                if (aiArg.Tag.Equals(str4) || index2 == 1 && aiArg.Tag.Equals("hp_rate_to_weak") ||
                                    index2 == 2 && aiArg.Tag.Equals("hp_rate_to_very_weak"))
                                    dataAiArgs1 = aiArg;
                                else if (aiArg.Tag.Equals(str5) || index2 == 0 && aiArg.Tag.Equals("hp_rate_to_weak") ||
                                         index2 == 1 && aiArg.Tag.Equals("hp_rate_to_very_weak"))
                                    dataAiArgs2 = aiArg;
                            if (dataAiArgs1 != null && int.TryParse(dataAiArgs1.ArgValue, out result1))
                            {
                                if (index2 == basicEnemyInfoList.Count - 1)
                                    enemyName += string.Format(" ({0}% - 0% HP)", result1);
                                else if (index2 < basicEnemyInfoList.Count - 1 && dataAiArgs2 != null &&
                                         int.TryParse(dataAiArgs2.ArgValue, out result2))
                                    enemyName += string.Format(" ({0}% - {1}% HP)", result1, result2 + 1);
                                else if (index2 < basicEnemyInfoList.Count - 1 && dataAiArgs2 == null)
                                    enemyName += string.Format(" ({0}% - 0% HP)", dataAiArgs1.ArgValue);
                            }
                            else if (index2 == 0 && dataAiArgs2 != null &&
                                     int.TryParse(dataAiArgs2.ArgValue, out result2))
                            {
                                enemyName += string.Format(" (100% - {0}% HP)", result2 + 1);
                            }
                        }
                    }

                    summary.Append(string.Format("**{0}:**  \n\n", enemyName));
                    if (!hasVariableCastTimes && !enemy.EnemyCastTime.Equals("Variable"))
                        summary.Append(string.Format("(These abilities have a cast time of {0})\n\n",
                            enemy.EnemyCastTime));

                    AppendMoveSummary(comboBoxEnemySelection, checkBoxCastTimes, checkBoxEnumerate, checkBoxRatesAsFractions,
                        checkBoxTranslate, abilitySummaryHelper, enemy, myEnemyParent, summary);
                }
            }
        }

        private static void AppendMoveSummary(ComboBox comboBoxEnemySelection, CheckBox checkBoxCastTimes,
            CheckBox checkBoxEnumerate, CheckBox checkBoxRatesAsFractions, CheckBox checkBoxTranslate,
            AbilitySummaryHelper abilitySummaryHelper, BasicEnemyInfo enemy, BasicEnemyParentInfo myEnemyParent,
            StringBuilder summary)
        {
            var str6 = abilitySummaryHelper.GetAbilitySummary(enemy, myEnemyParent, FFRKProxy.Instance.GameState.ActiveBattle,
                    comboBoxEnemySelection, checkBoxCastTimes, checkBoxEnumerate, checkBoxRatesAsFractions, checkBoxTranslate)
                .Replace("\n", "\n* ").Replace("not yet implemented", "**not yet implemented**")
                .Replace("not parsed", "**not parsed**").Replace(" 【", "【");
            if (!str6.Equals(""))
                summary.Append(string.Format("* {0}\n\n", str6));
        }

        private static void AddBreakEffectiveness(StringBuilder summary, List<BasicEnemyParentInfo> basicEnemyParentInfoList)
        {
            summary.Append("\n**Break Effectiveness:**  ");
            var flag7 = true;
            var enemyAtkBrkDef = basicEnemyParentInfoList[0].Phases.First().EnemyAtkBrkDef;
            foreach (var basicEnemyParentInfo in basicEnemyParentInfoList)
            foreach (var phase in basicEnemyParentInfo.Phases)
                if (!enemyAtkBrkDef.Equals(phase.EnemyAtkBrkDef) || !enemyAtkBrkDef.Equals(phase.EnemyDefBrkDef) ||
                    !enemyAtkBrkDef.Equals(phase.EnemyMagBrkDef) || !enemyAtkBrkDef.Equals(phase.EnemyResBrkDef) ||
                    !enemyAtkBrkDef.Equals(phase.EnemyMndBrkDef))
                    flag7 = false;
            if (flag7)
                summary.Append(string.Format("{0} (all)  \n", enemyAtkBrkDef));
            else
                for (var index1 = 0; index1 < basicEnemyParentInfoList.Count; ++index1)
                {
                    var basicEnemyParentInfo = basicEnemyParentInfoList[index1];
                    var basicEnemyInfoList = new List<BasicEnemyInfo>();
                    foreach (var phase in basicEnemyParentInfo.Phases)
                        basicEnemyInfoList.Add(phase);
                    var flag2 = basicEnemyParentInfoList.Count > 1;
                    var flag3 = basicEnemyInfoList.Count > 1;
                    if (index1 == 0)
                    {
                        summary.Append(string.Format("\n\n{0}", flag2 ? "Enemy" : (object) "Phase"));
                        summary.Append(" | ATK | DEF | MAG| RES | MND | \n");
                        summary.Append(":--|:--:|:--:|:--:|:--:|:--:|\n");
                    }
                    else
                    {
                        summary.Append("| \n");
                    }

                    var flag4 = true;
                    var basicEnemyInfo1 = basicEnemyParentInfo.Phases.First();
                    foreach (var phase in basicEnemyParentInfo.Phases)
                        if (!basicEnemyInfo1.EnemyAtkBrkDef.Equals(phase.EnemyAtkBrkDef) ||
                            !basicEnemyInfo1.EnemyDefBrkDef.Equals(phase.EnemyDefBrkDef) ||
                            !basicEnemyInfo1.EnemyMagBrkDef.Equals(phase.EnemyMagBrkDef) ||
                            !basicEnemyInfo1.EnemyResBrkDef.Equals(phase.EnemyResBrkDef) ||
                            !basicEnemyInfo1.EnemyMndBrkDef.Equals(phase.EnemyMndBrkDef))
                            flag4 = false;
                    if (flag4)
                    {
                        var flag5 = basicEnemyParentInfo.Phases.Count() > 1;
                        summary.Append(string.Format("{0}{1} | ", basicEnemyParentInfo.EnemyName,
                            flag5 ? " (all phases)" : (object) ""));
                        summary.Append(
                            string.Format("{0} | {1} | {2} | ", basicEnemyInfo1.EnemyAtkBrkDef,
                                basicEnemyInfo1.EnemyDefBrkDef, basicEnemyInfo1.EnemyMagBrkDef) +
                            string.Format("{0} | {1} \n", basicEnemyInfo1.EnemyResBrkDef,
                                basicEnemyInfo1.EnemyMndBrkDef));
                    }
                    else
                    {
                        for (var index2 = 0; index2 < basicEnemyInfoList.Count; ++index2)
                        {
                            var basicEnemyInfo2 = basicEnemyInfoList[index2];
                            if (flag2)
                            {
                                summary.Append(basicEnemyParentInfo.EnemyName);
                                if (flag3)
                                    summary.Append(" - ");
                                else
                                    summary.Append(" |");
                            }

                            if (flag3)
                            {
                                if (basicEnemyInfoList.Count <= 3)
                                {
                                    if (index2 == 0)
                                        summary.Append("Default |");
                                    if (index2 == 1)
                                        summary.Append("Weak |");
                                    if (index2 == 2)
                                        summary.Append("Very Weak |");
                                }
                                else
                                {
                                    summary.Append(string.Format("Phase {0} |", index2 + 1));
                                }
                            }

                            summary.Append(
                                string.Format(" {0} | {1} | {2} | ", basicEnemyInfoList[index2].EnemyAtkBrkDef,
                                    basicEnemyInfoList[index2].EnemyDefBrkDef,
                                    basicEnemyInfoList[index2].EnemyMagBrkDef) + string.Format("{0} | {1} \n",
                                    basicEnemyInfoList[index2].EnemyResBrkDef,
                                    basicEnemyInfoList[index2].EnemyMndBrkDef));
                        }
                    }
                }
        }

        private static void AddElementalResitances(StringBuilder summary, List<BasicEnemyParentInfo> basicEnemyParentInfoList)
        {
            summary.Append("\n**Elemental Damage Taken:**  \n\n");
            for (var index1 = 0; index1 < basicEnemyParentInfoList.Count; ++index1)
            {
                var basicEnemyParentInfo1 = basicEnemyParentInfoList[index1];
                var basicEnemyInfoList = new List<BasicEnemyInfo>();
                foreach (var phase in basicEnemyParentInfo1.Phases)
                    basicEnemyInfoList.Add(phase);
                var flag2 = basicEnemyParentInfoList.Count > 1;
                var flag3 = basicEnemyInfoList.Count > 1;
                if (index1 == 0)
                {
                    var flag4 = true;
                    var basicEnemyInfo = basicEnemyParentInfoList[0].Phases.First();
                    foreach (var basicEnemyParentInfo2 in basicEnemyParentInfoList)
                    foreach (var phase in basicEnemyParentInfo2.Phases)
                        if (!basicEnemyInfo.EnemyFireDef.Equals(phase.EnemyFireDef) ||
                            !basicEnemyInfo.EnemyIceDef.Equals(phase.EnemyIceDef) ||
                            !basicEnemyInfo.EnemyLitDef.Equals(phase.EnemyLitDef) ||
                            !basicEnemyInfo.EnemyEarthDef.Equals(phase.EnemyEarthDef) ||
                            !basicEnemyInfo.EnemyWindDef.Equals(phase.EnemyWindDef) ||
                            !basicEnemyInfo.EnemyWaterDef.Equals(phase.EnemyWaterDef) ||
                            !basicEnemyInfo.EnemyHolyDef.Equals(phase.EnemyHolyDef) ||
                            !basicEnemyInfo.EnemyDarkDef.Equals(phase.EnemyDarkDef) ||
                            !basicEnemyInfo.EnemyBioDef.Equals(phase.EnemyBioDef))
                            flag4 = false;
                    if (flag4)
                    {
                        var flag5 = true;
                        if (basicEnemyParentInfoList.Count == 1 && basicEnemyParentInfoList[0].Phases.Count() == 1)
                            flag5 = false;
                        if (flag5)
                            summary.Append(" | ");
                        summary.Append("Fire | Ice | Lightning | Earth | Wind | Water | Holy | Dark | Bio \n");
                        if (flag5)
                            summary.Append(":--|");
                        summary.Append(":--:|:--:|:--:|:--:|:--:|:--:|:--:|:--:|:--:\n");
                        if (flag5)
                        {
                            if (basicEnemyParentInfoList.Count == 1)
                                summary.Append("All Phases | ");
                            else
                                summary.Append("All Enemies | ");
                        }

                        summary.Append(int.Parse(basicEnemyInfo.EnemyFireDef.Replace("%", "")) <= 100
                            ? string.Format("{0} | ", basicEnemyInfo.EnemyFireDef)
                            : string.Format("**{0}** | ", basicEnemyInfo.EnemyFireDef));
                        summary.Append(int.Parse(basicEnemyInfo.EnemyIceDef.Replace("%", "")) <= 100
                            ? string.Format("{0} | ", basicEnemyInfo.EnemyIceDef)
                            : string.Format("**{0}** | ", basicEnemyInfo.EnemyIceDef));
                        summary.Append(int.Parse(basicEnemyInfo.EnemyLitDef.Replace("%", "")) <= 100
                            ? string.Format("{0} | ", basicEnemyInfo.EnemyLitDef)
                            : string.Format("**{0}** | ", basicEnemyInfo.EnemyLitDef));
                        summary.Append(int.Parse(basicEnemyInfo.EnemyEarthDef.Replace("%", "")) <= 100
                            ? string.Format("{0} | ", basicEnemyInfo.EnemyEarthDef)
                            : string.Format("**{0}** | ", basicEnemyInfo.EnemyEarthDef));
                        summary.Append(int.Parse(basicEnemyInfo.EnemyWindDef.Replace("%", "")) <= 100
                            ? string.Format("{0} | ", basicEnemyInfo.EnemyWindDef)
                            : string.Format("**{0}** | ", basicEnemyInfo.EnemyWindDef));
                        summary.Append(int.Parse(basicEnemyInfo.EnemyWaterDef.Replace("%", "")) <= 100
                            ? string.Format("{0} | ", basicEnemyInfo.EnemyWaterDef)
                            : string.Format("**{0}** | ", basicEnemyInfo.EnemyWaterDef));
                        summary.Append(int.Parse(basicEnemyInfo.EnemyHolyDef.Replace("%", "")) <= 100
                            ? string.Format("{0} | ", basicEnemyInfo.EnemyHolyDef)
                            : string.Format("**{0}** | ", basicEnemyInfo.EnemyHolyDef));
                        summary.Append(int.Parse(basicEnemyInfo.EnemyDarkDef.Replace("%", "")) <= 100
                            ? string.Format("{0} | ", basicEnemyInfo.EnemyDarkDef)
                            : string.Format("**{0}** | ", basicEnemyInfo.EnemyDarkDef));
                        summary.Append(int.Parse(basicEnemyInfo.EnemyBioDef.Replace("%", "")) <= 100
                            ? string.Format("{0} \n", basicEnemyInfo.EnemyBioDef)
                            : string.Format("**{0}** \n", basicEnemyInfo.EnemyBioDef));
                        break;
                    }

                    summary.Append(string.Format("{0}", flag2 ? "Enemy" : (object) "Phase"));
                    summary.Append(" | Fire | Ice | Lightning | Earth | Wind | Water | Holy | Dark | Bio \n");
                    summary.Append(":--|:--:|:--:|:--:|:--:|:--:|:--:|:--:|:--:|:--:\n");
                }
                else
                {
                    summary.Append("| \n");
                }

                var flag6 = true;
                var basicEnemyInfo1 = basicEnemyParentInfo1.Phases.First();
                foreach (var phase in basicEnemyParentInfo1.Phases)
                    if (!basicEnemyInfo1.EnemyFireDef.Equals(phase.EnemyFireDef) ||
                        !basicEnemyInfo1.EnemyIceDef.Equals(phase.EnemyIceDef) ||
                        !basicEnemyInfo1.EnemyLitDef.Equals(phase.EnemyLitDef) ||
                        !basicEnemyInfo1.EnemyEarthDef.Equals(phase.EnemyEarthDef) ||
                        !basicEnemyInfo1.EnemyWindDef.Equals(phase.EnemyWindDef) ||
                        !basicEnemyInfo1.EnemyWaterDef.Equals(phase.EnemyWaterDef) ||
                        !basicEnemyInfo1.EnemyHolyDef.Equals(phase.EnemyHolyDef) ||
                        !basicEnemyInfo1.EnemyDarkDef.Equals(phase.EnemyDarkDef) ||
                        !basicEnemyInfo1.EnemyBioDef.Equals(phase.EnemyBioDef))
                        flag6 = false;
                if (flag6)
                {
                    var flag4 = basicEnemyParentInfo1.Phases.Count() > 1;
                    summary.Append(string.Format("{0}{1} | ", basicEnemyParentInfo1.EnemyName,
                        flag4 ? " (all phases)" : (object) ""));
                    summary.Append(int.Parse(basicEnemyInfo1.EnemyFireDef.Replace("%", "")) <= 100
                        ? string.Format("{0} | ", basicEnemyInfo1.EnemyFireDef)
                        : string.Format("**{0}** | ", basicEnemyInfo1.EnemyFireDef));
                    summary.Append(int.Parse(basicEnemyInfo1.EnemyIceDef.Replace("%", "")) <= 100
                        ? string.Format("{0} | ", basicEnemyInfo1.EnemyIceDef)
                        : string.Format("**{0}** | ", basicEnemyInfo1.EnemyIceDef));
                    summary.Append(int.Parse(basicEnemyInfo1.EnemyLitDef.Replace("%", "")) <= 100
                        ? string.Format("{0} | ", basicEnemyInfo1.EnemyLitDef)
                        : string.Format("**{0}** | ", basicEnemyInfo1.EnemyLitDef));
                    summary.Append(int.Parse(basicEnemyInfo1.EnemyEarthDef.Replace("%", "")) <= 100
                        ? string.Format("{0} | ", basicEnemyInfo1.EnemyEarthDef)
                        : string.Format("**{0}** | ", basicEnemyInfo1.EnemyEarthDef));
                    summary.Append(int.Parse(basicEnemyInfo1.EnemyWindDef.Replace("%", "")) <= 100
                        ? string.Format("{0} | ", basicEnemyInfo1.EnemyWindDef)
                        : string.Format("**{0}** | ", basicEnemyInfo1.EnemyWindDef));
                    summary.Append(int.Parse(basicEnemyInfo1.EnemyWaterDef.Replace("%", "")) <= 100
                        ? string.Format("{0} | ", basicEnemyInfo1.EnemyWaterDef)
                        : string.Format("**{0}** | ", basicEnemyInfo1.EnemyWaterDef));
                    summary.Append(int.Parse(basicEnemyInfo1.EnemyHolyDef.Replace("%", "")) <= 100
                        ? string.Format("{0} | ", basicEnemyInfo1.EnemyHolyDef)
                        : string.Format("**{0}** | ", basicEnemyInfo1.EnemyHolyDef));
                    summary.Append(int.Parse(basicEnemyInfo1.EnemyDarkDef.Replace("%", "")) <= 100
                        ? string.Format("{0} | ", basicEnemyInfo1.EnemyDarkDef)
                        : string.Format("**{0}** | ", basicEnemyInfo1.EnemyDarkDef));
                    summary.Append(int.Parse(basicEnemyInfo1.EnemyBioDef.Replace("%", "")) <= 100
                        ? string.Format("{0} \n", basicEnemyInfo1.EnemyBioDef)
                        : string.Format("**{0}** \n", basicEnemyInfo1.EnemyBioDef));
                }
                else
                {
                    for (var index2 = 0; index2 < basicEnemyInfoList.Count; ++index2)
                    {
                        var basicEnemyInfo2 = basicEnemyInfoList[index2];
                        if (flag2)
                        {
                            summary.Append(basicEnemyParentInfo1.EnemyName);
                            if (flag3)
                                summary.Append(" - ");
                            else
                                summary.Append(" |");
                        }

                        if (flag3)
                        {
                            if (basicEnemyInfoList.Count <= 3)
                            {
                                if (index2 == 0)
                                    summary.Append("Default |");
                                if (index2 == 1)
                                    summary.Append("Weak |");
                                if (index2 == 2)
                                    summary.Append("Very Weak |");
                            }
                            else
                            {
                                summary.Append(string.Format("Phase {0} |", index2 + 1));
                            }
                        }

                        summary.Append(int.Parse(basicEnemyInfo2.EnemyFireDef.Replace("%", "")) <= 100
                            ? string.Format("{0} | ", basicEnemyInfo2.EnemyFireDef)
                            : string.Format("**{0}** | ", basicEnemyInfo2.EnemyFireDef));
                        summary.Append(int.Parse(basicEnemyInfo2.EnemyIceDef.Replace("%", "")) <= 100
                            ? string.Format("{0} | ", basicEnemyInfo2.EnemyIceDef)
                            : string.Format("**{0}** | ", basicEnemyInfo2.EnemyIceDef));
                        summary.Append(int.Parse(basicEnemyInfo2.EnemyLitDef.Replace("%", "")) <= 100
                            ? string.Format("{0} | ", basicEnemyInfo2.EnemyLitDef)
                            : string.Format("**{0}** | ", basicEnemyInfo2.EnemyLitDef));
                        summary.Append(int.Parse(basicEnemyInfo2.EnemyEarthDef.Replace("%", "")) <= 100
                            ? string.Format("{0} | ", basicEnemyInfo2.EnemyEarthDef)
                            : string.Format("**{0}** | ", basicEnemyInfo2.EnemyEarthDef));
                        summary.Append(int.Parse(basicEnemyInfo2.EnemyWindDef.Replace("%", "")) <= 100
                            ? string.Format("{0} | ", basicEnemyInfo2.EnemyWindDef)
                            : string.Format("**{0}** | ", basicEnemyInfo2.EnemyWindDef));
                        summary.Append(int.Parse(basicEnemyInfo2.EnemyWaterDef.Replace("%", "")) <= 100
                            ? string.Format("{0} | ", basicEnemyInfo2.EnemyWaterDef)
                            : string.Format("**{0}** | ", basicEnemyInfo2.EnemyWaterDef));
                        summary.Append(int.Parse(basicEnemyInfo2.EnemyHolyDef.Replace("%", "")) <= 100
                            ? string.Format("{0} | ", basicEnemyInfo2.EnemyHolyDef)
                            : string.Format("**{0}** | ", basicEnemyInfo2.EnemyHolyDef));
                        summary.Append(int.Parse(basicEnemyInfo2.EnemyDarkDef.Replace("%", "")) <= 100
                            ? string.Format("{0} | ", basicEnemyInfo2.EnemyDarkDef)
                            : string.Format("**{0}** | ", basicEnemyInfo2.EnemyDarkDef));
                        summary.Append(int.Parse(basicEnemyInfo2.EnemyBioDef.Replace("%", "")) <= 100
                            ? string.Format("{0} \n", basicEnemyInfo2.EnemyBioDef)
                            : string.Format("**{0}** \n", basicEnemyInfo2.EnemyBioDef));
                    }
                }
            }
        }

        private static void AddEnemyStats(List<BasicEnemyParentInfo> basicEnemyParentInfoList, StringBuilder summary, bool flag1,
            StatusVulnerabilityGetter statusVulnerabilityGetter)
        {
            for (var index1 = 0; index1 < basicEnemyParentInfoList.Count; ++index1)
            {
                var basicEnemyParentInfo = basicEnemyParentInfoList[index1];
                var basicEnemyInfoList = new List<BasicEnemyInfo>();
                foreach (var phase in basicEnemyParentInfo.Phases)
                    basicEnemyInfoList.Add(phase);
                var flag2 = basicEnemyParentInfoList.Count > 1;
                var flag3 = basicEnemyInfoList.Count > 1;
                if (!flag2)
                    summary.Append(string.Format("**{0}**\n\n", basicEnemyParentInfo.EnemyName));
                else if (index1 == 0)
                    summary.Append("**Stats:**\n\n");
                if (index1 == 0)
                {
                    if (flag2)
                        summary.Append("Enemy | ");
                    else if (flag3)
                        summary.Append("Phase | ");
                    summary.Append("LV | HP | ATK | DEF | MAG | RES | MND | SPD |");
                    if (!flag1)
                        summary.Append(" Status Vuln. |");
                    summary.Append("\n");
                    if (flag2 | flag3)
                        summary.Append(":--|");
                    summary.Append(":--:|:--:|:--:|:--:|:--:|:--:|:--:|:--:|");
                    if (!flag1)
                        summary.Append(":--|");
                    summary.Append("\n");
                }
                else
                {
                    summary.Append("| \n");
                }

                for (var index2 = 0; index2 < basicEnemyInfoList.Count; ++index2)
                {
                    var basicEnemyInfo = basicEnemyInfoList[index2];
                    if (flag2)
                    {
                        summary.Append(basicEnemyParentInfo.EnemyName);
                        if (flag3)
                            summary.Append(" - ");
                        else
                            summary.Append(" |");
                    }

                    if (flag3)
                    {
                        if (basicEnemyInfoList.Count <= 3)
                        {
                            if (index2 == 0)
                                summary.Append("Default |");
                            if (index2 == 1)
                                summary.Append("Weak |");
                            if (index2 == 2)
                                summary.Append("Very Weak |");
                        }
                        else
                        {
                            summary.Append(string.Format("Phase {0} |", index2 + 1));
                        }
                    }

                    if (index2 == 0)
                    {
                        summary.Append(
                            string.Format(" {0} | {1} | {2} | {3} | ", basicEnemyInfo.EnemyLv,
                                basicEnemyInfo.EnemyMaxHp.ToString("N0"), basicEnemyInfo.EnemyAtk.ToString("N0"),
                                basicEnemyInfo.EnemyDef.ToString("N0")) + string.Format("{0} | {1} | {2} | {3} |",
                                basicEnemyInfo.EnemyMag.ToString("N0"), basicEnemyInfo.EnemyRes.ToString("N0"),
                                basicEnemyInfo.EnemyMnd.ToString("N0"), basicEnemyInfo.EnemySpd.ToString("N0")));
                        if (!flag1)
                            summary.Append(string.Format(" {0} |",
                                statusVulnerabilityGetter.GetStatusVulnerabilities(basicEnemyInfo.EnemyStatusImmunity)));
                        summary.Append("\n");
                    }
                    else
                    {
                        summary.Append(" | | ");
                        summary.Append(
                            (int) basicEnemyInfo.EnemyAtk == (int) basicEnemyInfoList[index2 - 1].EnemyAtk
                                ? string.Format("{0} | ", basicEnemyInfo.EnemyAtk.ToString("N0"))
                                : string.Format("**{0}** | ", basicEnemyInfo.EnemyAtk.ToString("N0")));
                        summary.Append(
                            (int) basicEnemyInfo.EnemyDef == (int) basicEnemyInfoList[index2 - 1].EnemyDef
                                ? string.Format("{0} | ", basicEnemyInfo.EnemyDef.ToString("N0"))
                                : string.Format("**{0}** | ", basicEnemyInfo.EnemyDef.ToString("N0")));
                        summary.Append(
                            (int) basicEnemyInfo.EnemyMag == (int) basicEnemyInfoList[index2 - 1].EnemyMag
                                ? string.Format("{0} | ", basicEnemyInfo.EnemyMag.ToString("N0"))
                                : string.Format("**{0}** | ", basicEnemyInfo.EnemyMag.ToString("N0")));
                        summary.Append(
                            (int) basicEnemyInfo.EnemyRes == (int) basicEnemyInfoList[index2 - 1].EnemyRes
                                ? string.Format("{0} | ", basicEnemyInfo.EnemyRes.ToString("N0"))
                                : string.Format("**{0}** | ", basicEnemyInfo.EnemyRes.ToString("N0")));
                        summary.Append(
                            (int) basicEnemyInfo.EnemyMnd == (int) basicEnemyInfoList[index2 - 1].EnemyMnd
                                ? string.Format("{0} | ", basicEnemyInfo.EnemyMnd.ToString("N0"))
                                : string.Format("**{0}** | ", basicEnemyInfo.EnemyMnd.ToString("N0")));
                        summary.Append(
                            (int) basicEnemyInfo.EnemySpd == (int) basicEnemyInfoList[index2 - 1].EnemySpd
                                ? string.Format("{0} |", basicEnemyInfo.EnemySpd.ToString("N0"))
                                : string.Format("**{0}** |", basicEnemyInfo.EnemySpd.ToString("N0")));
                        if (!flag1)
                            summary.Append(string.Format(" {0} |",
                                statusVulnerabilityGetter.GetStatusVulnerabilities(basicEnemyInfo.EnemyStatusImmunity)));
                        summary.Append("\n");
                    }
                }
            }
        }

        private static StringBuilder GetFirstClearRewards(DataDungeonPrizes prizes, ItemNameGetter itemNameGetter)
        {
            var results = new StringBuilder();
            if (prizes.FirstClearRewards != null)
            {
                foreach (var firstClearReward in prizes.FirstClearRewards)
                    results.Append(string.Format("* {0}  \n",
                        itemNameGetter.GetItemName(firstClearReward.Id, firstClearReward.Quantity, firstClearReward.Name)));
            }

            return results;
        }

        private static StringBuilder GetDungeonPrizes(DataDungeonPrizes prizes, ItemNameGetter itemNameGetter)
        {
            var results = new StringBuilder();
            if (prizes.ClearRewards != null)
            {
                foreach (var clearReward in prizes.ClearRewards)
                    results.Append(string.Format("* {0}  \n",
                        itemNameGetter.GetItemName(clearReward.Id, clearReward.Quantity, clearReward.Name)));
            }

            return results;
        }

        private static StringBuilder GetMedalConditions(EventListBattles activeDungeon, int battleId)
        {
            var results = new StringBuilder();
            foreach (var capture in activeDungeon.UserDungeon.Captures)

            foreach (var dataDungeonSpScore in capture.SpScore)
                if ((int) battleId == (int) dataDungeonSpScore.BattleID)
                    results.Append(string.Format("{0}. ",
                        new MedalConditionParser(dataDungeonSpScore.Title).translate(false)));
            return results;
        }

        private static StringBuilder GetMasteryRewards(DataDungeonPrizes prizes, ItemNameGetter itemNameGetter)
        {
            var results = new StringBuilder();

            if (prizes.MasteryRewards != null)
            {
                foreach (var masteryReward in prizes.MasteryRewards)
                    results.Append(string.Format("* {0}  \n",
                        itemNameGetter.GetItemName(masteryReward.Id, masteryReward.Quantity, masteryReward.Name)));
              
            }

            return results;
        }
    }
}