using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

public class TooltipSkill : TooltipBase
{
    [SerializeField] private Image image = null;
    [SerializeField] private Text nameText = null;
    [SerializeField] private Text targetsAmountText = null;
    [SerializeField] private Text typeText = null;
    [SerializeField] private Text descriptionText = null;

    private int barIndex;

    public override void ShowTooltip(Skill skill)
    {
        base.ShowTooltip(skill);

        barIndex = 0;

        SetBaseInfo(skill);
        ShowDescription(skill);
        ShowTargetAmount(skill);
        ShowSkillType(skill);
        ShowCastTime(skill);
        ShowPushPower(skill);
        ShowAmmoType(skill);
        ShowAmmoPerShot(skill);
        ShowMissileRange(skill);
        ShowIsBuff(skill);
        ShowAuraRange(skill);
        ShowAuraType(skill);
        ShowMovementHitEnemy(skill);
        ShowMovementRange(skill);

        barIndex++; //Add space btw bars

        ShowUseStats(skill);
        ShowOffensesStats(skill);
        ShowBuffStats(skill);

        gameObject.SetActive(true);
    }

    public override void HideTooltip()
    {
        base.HideTooltip();
        HideModifiersBars();
        gameObject.SetActive(false);
    }

    private void ShowBuffStats(Skill skill)
    {
        AuraSkill auraSkill = skill as AuraSkill;
        if (auraSkill)
        {
            Color textColor;
            AuraData auraData;

            for (int i = 0; i < auraSkill.Auras.Count; i++)
            {
                auraData = auraSkill.Auras[i];
                sb.Clear();

                if (auraData.BuffValue != 0)
                {
                    if (auraData.BuffValue > 0)
                    {
                        sb.Append("+");
                        textColor = statPositive;
                    }
                    else
                    {
                        textColor = statNegative;
                    }

                    sb.Append(auraData.BuffValue);

                    if (auraData.IsPercentMult) sb.Append("%");

                    sb.Append(" ");

                    if (auraData.BaseStatType == EBaseStatType.Max) sb.Append(GameManager.Instance.ConstLocalized.StatMaxValue.GetLocalizedString() + " ");
                    if (auraData.BaseStatType == EBaseStatType.Min) sb.Append(GameManager.Instance.ConstLocalized.StatMinValue.GetLocalizedString() + " ");

                    sb.Append(GameManager.Instance.CharacterStatsData.GetStat(auraData.BuffStatType).StatName.GetLocalizedString());
                    sb.Append(" ");
                    sb.Append(GameManager.Instance.ConstLocalized.ForTime.GetLocalizedString());
                    sb.Append(" ");
                    sb.Append(auraData.Duration);
                    sb.Append(" ");
                    sb.Append(GameManager.Instance.ConstLocalized.Seconds.GetLocalizedString());

                    ShowModifierBar(sb, i, textColor);
                }
            }
        }
    }

    private void ShowOffensesStats(Skill skill)
    {
        if (skill.OffenseStats.Length <= 0) return;

        sb.Clear();
        sb.Append(GameManager.Instance.ConstLocalized.SkillOffenseStat.GetLocalizedString());
        ShowModifierBar(sb, barIndex, statPositive);
        barIndex++;

        foreach (var stat in skill.OffenseStats)
        {
            sb.Clear();

            if(stat.MinDamage != 0 || stat.MaxDamage != 0)
            {
                if (stat.MinDamage != 0)
                {
                    sb.Append(stat.MinDamage);
                }
                if(stat.MinDamage != 0 && stat.MaxDamage != 0) sb.Append("-");
                if (stat.MaxDamage != 0)
                {
                    sb.Append(stat.MaxDamage);
                }

                sb.Append(" ");
                sb.Append(GameManager.Instance.CharacterStatsData.GetStat(stat.StatType).StatName.GetLocalizedString());
                sb.Append(": ");
                sb.Append(GameManager.Instance.CharacterStatsData.GetStat(stat.DamagedStatType).StatName.GetLocalizedString());

                ShowModifierBar(sb, barIndex, statPositive);
                barIndex++;
            }

            if (stat.MinPercentDamage != 0 || stat.MinPercentDamage != 0)
            {
                if (stat.MinPercentDamage != 0)
                {
                    sb.Append(stat.MinPercentDamage+"%");
                }
                if (stat.MinPercentDamage != 0 && stat.MinPercentDamage != 0) sb.Append("-");
                if (stat.MinPercentDamage != 0)
                {
                    sb.Append(stat.MinPercentDamage + "%");
                }

                sb.Append(" ");
                sb.Append(GameManager.Instance.CharacterStatsData.GetStat(stat.StatType).StatName.GetLocalizedString());
                sb.Append(": ");
                sb.Append(GameManager.Instance.CharacterStatsData.GetStat(stat.DamagedStatType).StatName.GetLocalizedString());

                ShowModifierBar(sb, barIndex, statPositive);
                barIndex++;
            }


        }
    }

    private void ShowUseStats(Skill skill)
    {
        if (skill.SkillStatsUsed.Length <= 0) return;

        sb.Clear();
        sb.Append(GameManager.Instance.ConstLocalized.SkillUseStat.GetLocalizedString());
        ShowModifierBar(sb, barIndex, statNegative);
        barIndex++;
        foreach (var stat in skill.SkillStatsUsed)
        {
            if(stat.StatValue != 0)
            {
                sb.Clear();
                sb.Append(stat.StatValue + " " + GameManager.Instance.CharacterStatsData.GetStat(stat.StatType).StatName.GetLocalizedString());
                ShowModifierBar(sb, barIndex, statNegative);
                barIndex++;
            }
        }
    }

    private void ShowMovementRange(Skill skill)
    {
        MovementSkill movementSkill = skill as MovementSkill;
        if (movementSkill)
        {
            sb.Clear();
            if (movementSkill.CasterMoveRange == 0) return;

            sb.Append(movementSkill.CasterMoveRange + " " + GameManager.Instance.ConstLocalized.SkillMovementRange.GetLocalizedString());
            ShowModifierBar(sb, barIndex, baseColor);
            barIndex++;
        }
    }

    private void ShowMovementHitEnemy(Skill skill)
    {
        MovementSkill movementSkill = skill as MovementSkill;
        if (movementSkill)
        {
            sb.Clear();
            if (movementSkill.HitingEnemy)
            {
                sb.Append(GameManager.Instance.ConstLocalized.SkillHitEnemy.GetLocalizedString());
            }
            else
            {
                sb.Append(GameManager.Instance.ConstLocalized.SkillDontHitEnemy.GetLocalizedString());
            }
            ShowModifierBar(sb, barIndex, baseColor);
            barIndex++;
        }
    }

    private void ShowAuraType(Skill skill)
    {
        AuraSkill auraSkill = skill as AuraSkill;
        if (auraSkill)
        {
            sb.Clear();
            sb.Append(GameManager.Instance.ConstLocalized.SkillTypeAura.GetLocalizedString() + ": " +auraSkill.AuraTypeName.GetLocalizedString());
            ShowModifierBar(sb, barIndex, baseColor);
            barIndex++;
        }
    }

    private void ShowAuraRange(Skill skill)
    {
        AuraSkill auraSkill = skill as AuraSkill;
        if (auraSkill)
        {
            sb.Clear();
            if (auraSkill.UseOnlyOnCaster)
            {
                sb.Append(GameManager.Instance.ConstLocalized.SkillUseOnlyOnCaster.GetLocalizedString());
            }
            else
            {
                sb.Append(GameManager.Instance.ConstLocalized.SkillUseOnAround.GetLocalizedString());
            }

            ShowModifierBar(sb, barIndex, baseColor);
            barIndex++;
        }
    }

    private void ShowIsBuff(Skill skill)
    {
        AuraSkill auraSkill = skill as AuraSkill;
        if (auraSkill)
        {
            sb.Clear();
            if (auraSkill.IsBuff)
            {
                sb.Append(GameManager.Instance.ConstLocalized.SkillBuff.GetLocalizedString());
            }
            else
            {
                sb.Append(GameManager.Instance.ConstLocalized.SkillDebuff.GetLocalizedString());
            }

            ShowModifierBar(sb, barIndex, baseColor);
            barIndex++;
        }
    }

    private void ShowMissileRange(Skill skill)
    {
        RangeSkill rangeSkill = skill as RangeSkill;
        if (rangeSkill)
        {
            if (rangeSkill.MissileRange == 0) return;

            sb.Clear();
            sb.Append(rangeSkill.MissileRange + " " + GameManager.Instance.ConstLocalized.SkillMissileRange.GetLocalizedString());

            ShowModifierBar(sb, barIndex, baseColor);
            barIndex++;
        }
    }

    private void ShowAmmoPerShot(Skill skill)
    {
        RangeSkill rangeSkill = skill as RangeSkill;
        if (rangeSkill)
        {
            if (rangeSkill.AmmoUsePerShot <= 0) return;

            sb.Clear();
            sb.Append(rangeSkill.AmmoUsePerShot + " " + GameManager.Instance.ConstLocalized.SkillAmmoPerShot.GetLocalizedString());

            ShowModifierBar(sb, barIndex, baseColor);
            barIndex++;
        }
    }

    private void ShowAmmoType(Skill skill)
    {
        RangeSkill rangeSkill = skill as RangeSkill;
        if (rangeSkill)
        {
            sb.Clear();
            switch (rangeSkill.AmmoType)
            {
                case EItemAmmoType.None:
                    return;
                case EItemAmmoType.Arrows:
                    sb.Append(GameManager.Instance.ConstLocalized.SkillAmmoArrows.GetLocalizedString());
                    break;
                case EItemAmmoType.Bolts:
                    sb.Append(GameManager.Instance.ConstLocalized.SkillAmmoBolts.GetLocalizedString());
                    break;
                case EItemAmmoType.Runs:
                    sb.Append(GameManager.Instance.ConstLocalized.SkillAmmoRuns.GetLocalizedString());
                    break;
                default:
                    break;
            }

            ShowModifierBar(sb, barIndex, baseColor);
            barIndex++;
        }
    }

    private void ShowPushPower(Skill skill)
    {
        if (skill.PushPower <= 0) return;

        sb.Clear();
        sb.Append(skill.PushPower + " " + GameManager.Instance.ConstLocalized.SkillPushPower.GetLocalizedString());

        ShowModifierBar(sb, barIndex, baseColor);
        barIndex++;
    }

    private void ShowCastTime(Skill skill)
    {
        if (skill.SkillCastTime <= 0) return;

        sb.Clear();
        sb.Append(skill.SkillCastTime + " " + GameManager.Instance.ConstLocalized.SkillCastTime.GetLocalizedString());

        ShowModifierBar(sb, barIndex, baseColor);
        barIndex++;
    }

    private void ShowSkillType(Skill skill)
    {
        MeleeSkill melee = skill as MeleeSkill;
        if (melee)
        {
            typeText.text = GameManager.Instance.ConstLocalized.SkillTypeMelee.GetLocalizedString();
            return;
        }
        RangeSkill range = skill as RangeSkill;
        if (range)
        {
            typeText.text = GameManager.Instance.ConstLocalized.SkillTypeRange.GetLocalizedString();
            return;
        }
        AuraSkill aura = skill as AuraSkill;
        if (aura)
        {
            typeText.text = GameManager.Instance.ConstLocalized.SkillTypeAura.GetLocalizedString();
            return;
        }
        MovementSkill movement = skill as MovementSkill;
        if (movement)
        {
            typeText.text = GameManager.Instance.ConstLocalized.SkillTypeMovement.GetLocalizedString();
            return;
        }
    }

    private void ShowTargetAmount(Skill skill)
    {
        if (skill.SingleTarget)
        {
            targetsAmountText.text = GameManager.Instance.ConstLocalized.SkillSingleTarget.GetLocalizedString();
        }
        else
        {
            targetsAmountText.text = GameManager.Instance.ConstLocalized.SkillMultiTargets.GetLocalizedString();
        }
    }

    private void ShowDescription(Skill skill)
    {
        if (skill.Description.IsEmpty)
        {
            descriptionText.text = "";
        }
        else
        {
            descriptionText.text = skill.Description.GetLocalizedString();
        }
    }

    private void SetBaseInfo(Skill skill)
    {
        image.sprite = skill.SkillSprite;
        nameText.text = skill.SkillName.GetLocalizedString();
    }

}
