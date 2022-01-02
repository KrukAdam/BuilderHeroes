using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "Item Effects/Stat Buff")]

[Serializable]
public class ItemStatBuffEffect
{
    public float BuffValue { get => buffValue; }
    public float Duration { get => duration; }
    public EBaseStatType BaseStatType { get => baseStatType; }
    public EStatsTypes BuffStatType { get => buffStatType; }
    public bool IsPercentMult { get => isPercentMult; }

    [SerializeField] private float buffValue = 0;
    [SerializeField] private float duration = 0;
    [SerializeField] private EBaseStatType baseStatType = EBaseStatType.None;
    [SerializeField] private EStatsTypes buffStatType = EStatsTypes.None;
    [SerializeField] private bool isPercentMult = false;

    public void ExecuteEffect(ItemUsable parentItem, PlayerCharacter character)
    {
        StatModifier statModifier;

        if (isPercentMult)
        {
            statModifier = new StatModifier(buffValue, EStatModifierType.PercentAdd, parentItem);
        }
        else
        {
            statModifier = new StatModifier(buffValue, EStatModifierType.Constants, parentItem);
        }

        int index = 0;
        for (int i = 0; i < character.Stats.AllStats.Count; i++)
        {
            if (buffStatType == character.Stats.AllStats[i].StatType)
            {
                character.Stats.AllStats[i].AddModifier(statModifier, baseStatType);
                index = i;
            }
        }

        character.Stats.Refresh();
        character.StartCoroutine(RemoveBuff(character, statModifier, index, duration));
    }

    public string GetDescription()
    {
        return "Grants " + buffValue + " of " + buffStatType + " to " + duration + " seconds.";
    }

    private IEnumerator RemoveBuff(PlayerCharacter character, StatModifier statModifiers, int index, float duration)
    {
        yield return new WaitForSeconds(duration);
        character.Stats.AllStats[index].RemoveModifiers(statModifiers, baseStatType);
        character.Stats.Refresh();
    }
}
