using System;
using UnityEngine;

[Serializable]
public class CharacterStat
{
    //Display name
    public string StatName = "No Name";

    public EStatsValueUse StatUseValues = EStatsValueUse.None;
    public EStatsTypes StatType = EStatsTypes.None;
    [Header("If stat if offensive, this is defense stat")]
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
    }

    public void RemoveAllModifiersFromSource(object obj)
    {
        BaseValue.RemoveAllModifiersFromSource(obj);
        MinValue.RemoveAllModifiersFromSource(obj);
        MaxValue.RemoveAllModifiersFromSource(obj);
    }

    public void RemoveAllModifiers(StatModifier modifier)
    {
        BaseValue.RemoveModifier(modifier);
        MinValue.RemoveModifier(modifier);
        MaxValue.RemoveModifier(modifier);
    }
}
