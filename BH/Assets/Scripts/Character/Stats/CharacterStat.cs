using System;
using UnityEngine;
using UnityEngine.Localization;

[Serializable]
public class CharacterStat
{
    //Display name
    public LocalizedString StatName;

    public EStatsValueUse StatUseValues = EStatsValueUse.None;
    public EStatsTypes StatType = EStatsTypes.None;
    [Header("If stat if offensive, set defense stat")]
    public EStatsTypes StatDefenseType = EStatsTypes.None;
    public BaseStat BaseValue;
    public BaseStat MinValue;
    public BaseStat MaxValue;

    public void AddModifierOnEquip(CharacterStat characterStat, ItemEquippable item, EStatModifierType typeStatMode)
    {
        if (characterStat.BaseValue.Value != 0)
            BaseValue.AddModifier(new StatModifier(characterStat.BaseValue.Value, typeStatMode, item));
        if (characterStat.MinValue.Value != 0)
            MinValue.AddModifier(new StatModifier(characterStat.MinValue.Value, typeStatMode, item));
        if (characterStat.MaxValue.Value != 0)
            MaxValue.AddModifier(new StatModifier(characterStat.MaxValue.Value, typeStatMode, item));

        CheckOverride();
    }

    public void RemoveAllModifiersFromSource(object obj)
    {
        BaseValue.RemoveAllModifiersFromSource(obj);
        MinValue.RemoveAllModifiersFromSource(obj);
        MaxValue.RemoveAllModifiersFromSource(obj);

        CheckOverride();
    }

    public void RemoveModifiers(StatModifier modifier, EBaseStatType baseStatType)
    {
        if (baseStatType == EBaseStatType.Base)
        {
            BaseValue.RemoveModifier(modifier);
        }
        if (baseStatType == EBaseStatType.Min)
        {
            MinValue.RemoveModifier(modifier);
        }
        if (baseStatType == EBaseStatType.Max)
        {
            MaxValue.RemoveModifier(modifier);
        }

        CheckOverride();
    }

    public void AddModifier(StatModifier mod, EBaseStatType baseStatType)
    {
        if(baseStatType == EBaseStatType.Base)
        {
            BaseValue.AddModifier(mod);
        }
        if (baseStatType == EBaseStatType.Min)
        {
            MinValue.AddModifier(mod);
        }
        if (baseStatType == EBaseStatType.Max)
        {
            MaxValue.AddModifier(mod);
        }

        CheckOverride();
    }

    private void CheckOverride()
    {
        if(StatUseValues == EStatsValueUse.BaseAndMaxValue)
        {
            if (BaseValue.Value > MaxValue.Value) BaseValue.Value = MaxValue.Value;
        }
        if (StatUseValues == EStatsValueUse.MinAndMaxValue)
        {
            if (MinValue.Value > MaxValue.Value) MinValue.Value = MaxValue.Value;
        }
    }
}
