using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class AuraData
{
    [SerializeField] private float buffValue = 0;
    [SerializeField] private float duration = 0;
    [SerializeField] private EBaseStatType baseStatType = EBaseStatType.None;
    [SerializeField] private EStatsTypes buffStatType = EStatsTypes.None;
    [SerializeField] private bool isPercentMult = false;

    public void ExecuteEffect(AuraSkill aura, Stats stats)
    {
        StatModifier statModifier;

        if (isPercentMult)
        {
            statModifier = new StatModifier(buffValue, EStatModifierType.PercentAdd, aura, aura.AuraType);
        }
        else
        {
            statModifier = new StatModifier(buffValue, EStatModifierType.Constants, aura, aura.AuraType);
        }

        int index = 0;
        for (int i = 0; i < stats.AllStats.Count; i++)
        {
            if (buffStatType == stats.AllStats[i].StatType)
            {
                stats.AllStats[i].AddModifier(statModifier, baseStatType);
                index = i;
            }
        }

        stats.StartCoroutine(RemoveBuff(stats, statModifier, index, duration));
        stats.AuraRefresh();
    }

    private IEnumerator RemoveBuff(Stats stats, StatModifier statModifiers, int index, float duration)
    {
        yield return new WaitForSeconds(duration);
        stats.AllStats[index].RemoveModifiers(statModifiers, baseStatType);
        stats.AuraRefresh();
    }
}
