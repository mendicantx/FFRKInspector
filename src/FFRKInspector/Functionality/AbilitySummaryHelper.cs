using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FFRKInspector.GameData;
using FFRKInspector.GameData.Converters;

namespace FFRKInspector.Functionality
{
    public class AbilitySummaryHelper
    {
        public string GetAbilitySummary(BasicEnemyInfo enemy, BasicEnemyParentInfo myEnemyParent,
            EventBattleInitiated battle, ComboBox comboBoxEnemySelection, CheckBox checkBoxCastTimes, CheckBox checkBoxEnumerate,
            CheckBox checkBoxRatesAsFractions, CheckBox checkBoxTranslate)
        {
            if (enemy == null || myEnemyParent == null || battle == null)
                return "";
            var single = myEnemyParent.Appearances[0] == 1U && comboBoxEnemySelection.Items.Count == 1;

            uint totalWeight = 0;
            foreach (var enemyAbility in enemy.EnemyAbilities)
                totalWeight += enemyAbility.Weight;
            var source = new List<string>();
            var enemyAbilityParser = new EnemyAbilityParser(totalWeight, battle, single);
            var checkState = checkBoxCastTimes.CheckState;
            bool flag1;
            if (checkState.Equals(CheckState.Indeterminate))
            {
                flag1 = enemy.EnemyCastTime.Equals("Variable");
            }
            else
            {
                checkState = checkBoxCastTimes.CheckState;
                flag1 = checkState.Equals(CheckState.Checked);
            }

            checkState = checkBoxEnumerate.CheckState;
            bool flag2;
            if (checkState.Equals(CheckState.Indeterminate))
            {
                var num = 0;
                foreach (var constraint in myEnemyParent.Constraints)
                    if (constraint.ConstraintType == 1001U && (int)constraint.EnemyStatusId == (int)enemy.EnemyId)
                        ++num;
                flag2 = num >= 10;
            }
            else
            {
                checkState = checkBoxEnumerate.CheckState;
                flag2 = checkState.Equals(CheckState.Checked);
            }

            var parseOpt = new EnemyAbilityParserOptions
            {
                displayFractions = checkBoxRatesAsFractions.Checked,
                displayCastTimes = flag1,
                translateAbilityNames = checkBoxTranslate.Checked
            };
            var orderedEnumerable = enemy.getAbilities(myEnemyParent.Constraints).OrderBy(x =>
            {
                var val2 = int.MaxValue;
                foreach (var constraint in myEnemyParent.Constraints)
                    if (constraint.ConstraintType == 1001U && constraint.AbilityTag.Equals(x.Tag))
                    {
                        var val1 = int.Parse(constraint.ConstraintValue);
                        if (constraint.EnemyStatusId == 0U)
                            val1 -= 10000;
                        else if ((int)constraint.EnemyStatusId != (int)enemy.EnemyId)
                            continue;
                        val2 = Math.Min(val1, val2);
                    }

                return val2;
            }).OrderBy(x => x.Weight).OrderBy(x => x.Weight != 0U ? x.UnlockTurn : 0U);
            if (flag2)
            {
                var val1 = 0;
                foreach (var constraint in myEnemyParent.Constraints)
                    if (constraint.ConstraintType == 1001U && (int)constraint.EnemyStatusId == (int)enemy.EnemyId)
                        val1 = Math.Max(val1, int.Parse(constraint.ConstraintValue));
                for (var enumeratedTurn = 1; enumeratedTurn <= val1; ++enumeratedTurn)
                    foreach (var constraint1 in myEnemyParent.Constraints)
                        if (constraint1.ConstraintType == 1001U &&
                            (int)constraint1.EnemyStatusId == (int)enemy.EnemyId &&
                            int.Parse(constraint1.ConstraintValue) == enumeratedTurn)
                            foreach (var paramAbility in orderedEnumerable)
                                if (constraint1.AbilityTag.Equals(paramAbility.Tag))
                                {
                                    var num = 0;
                                    var flag3 = true;
                                    if (paramAbility.Weight > 0U)
                                        flag3 = false;
                                    foreach (var constraint2 in myEnemyParent.Constraints)
                                    {
                                        if (flag3 && constraint2.ConstraintType == 1001U &&
                                            constraint2.AbilityTag.Equals(paramAbility.Tag) &&
                                            (int)constraint2.EnemyStatusId == (int)enemy.EnemyId &&
                                            int.Parse(constraint2.ConstraintValue) > enumeratedTurn)
                                            flag3 = false;
                                        if (flag3 && constraint2.ConstraintType >= 1003U &&
                                            constraint2.ConstraintType <= 1005U &&
                                            constraint2.AbilityTag.Equals(paramAbility.Tag) &&
                                            (int)constraint1.EnemyStatusId == (int)enemy.EnemyId)
                                            flag3 = false;
                                        if (flag3 && constraint2.ConstraintType == 1002U &&
                                            constraint2.AbilityTag.Equals(paramAbility.Tag) &&
                                            (int)constraint2.EnemyStatusId == (int)enemy.EnemyId)
                                            num = num == 0
                                                ? int.Parse(constraint2.ConstraintValue)
                                                : Math.Min(int.Parse(constraint2.ConstraintValue), num);
                                    }

                                    if ((num > 0) & flag3)
                                        source.Add(enemyAbilityParser.parseAbility(paramAbility,
                                            myEnemyParent.Constraints, enemy, parseOpt, false, enumeratedTurn, num));
                                    else
                                        source.Add(enemyAbilityParser.parseAbility(paramAbility,
                                            myEnemyParent.Constraints, enemy, parseOpt, false, enumeratedTurn));
                                }

                foreach (var paramAbility in orderedEnumerable)
                    if (paramAbility.Weight > 0U)
                    {
                        source.Add(enemyAbilityParser.parseAbility(paramAbility, myEnemyParent.Constraints, enemy,
                            parseOpt, false, 0));
                    }
                    else
                    {
                        var flag3 = false;
                        var flag4 = false;
                        foreach (var constraint in myEnemyParent.Constraints)
                            if (constraint.AbilityTag.Equals(paramAbility.Tag))
                            {
                                if ((int)constraint.EnemyStatusId == (int)enemy.EnemyId &&
                                    constraint.ConstraintType == 1001U)
                                    flag3 = true;
                                if (constraint.EnemyStatusId == 0U ||
                                    (int)constraint.EnemyStatusId == (int)enemy.EnemyId &&
                                    constraint.ConstraintType != 1001U && constraint.ConstraintType != 1002U)
                                    flag4 = true;
                            }

                        if (!flag3 | flag4)
                            source.Add(enemyAbilityParser.parseAbility(paramAbility, myEnemyParent.Constraints, enemy,
                                parseOpt, false, 0));
                    }
            }
            else
            {
                foreach (var paramAbility in orderedEnumerable)
                    source.Add(
                        enemyAbilityParser.parseAbility(paramAbility, myEnemyParent.Constraints, enemy, parseOpt));
            }

            var list = source.OrderBy(x => x[0] != 'T').ThenBy(x => x[0] != 'S').ToList();
            foreach (var counter in enemy.EnemyCounters.OrderBy(x => x.Rate))
                list.Add(enemyAbilityParser.parseCounter(counter, parseOpt));
            return string.Join("  \n", list);
        }
    }
}