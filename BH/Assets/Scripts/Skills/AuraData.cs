using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class AuraData : MonoBehaviour
{
	[SerializeField] private float buffBaseValue;
	[SerializeField] private float buffMinValue;
	[SerializeField] private float buffMaxValue;
	[SerializeField] private float duration;
	[SerializeField] private EStatsTypes buffStatType;
	[SerializeField] private bool isPercentMult = false;

	public void ExecuteEffect(UsableItem parentItem, PlayerCharacter character)
	{
		StatModifier baseValueStatModifier;
		StatModifier minValueStatModifier;
		StatModifier maxValueStatModifier;

		if (isPercentMult)
		{
			baseValueStatModifier = new StatModifier(buffBaseValue, StatModType.PercentMult, parentItem);
			minValueStatModifier = new StatModifier(buffMinValue, StatModType.PercentMult, parentItem);
			maxValueStatModifier = new StatModifier(buffMaxValue, StatModType.PercentMult, parentItem);
		}
		else
		{
			baseValueStatModifier = new StatModifier(buffBaseValue, StatModType.Flat, parentItem);
			minValueStatModifier = new StatModifier(buffMinValue, StatModType.Flat, parentItem);
			maxValueStatModifier = new StatModifier(buffMaxValue, StatModType.Flat, parentItem);
		}

		int index = 0;
		List<StatModifier> modifiers = new List<StatModifier>();
		for (int i = 0; i < character.Stats.AllStats.Count; i++)
		{
			if (buffStatType == character.Stats.AllStats[i].StatType)
			{
				character.Stats.AllStats[i].BaseValue.AddModifier(baseValueStatModifier);
				character.Stats.AllStats[i].MinValue.AddModifier(minValueStatModifier);
				character.Stats.AllStats[i].MaxValue.AddModifier(maxValueStatModifier);
				index = i;
				modifiers.Add(baseValueStatModifier);
				modifiers.Add(minValueStatModifier);
				modifiers.Add(maxValueStatModifier);
			}
		}

		character.UpdateStatValues();
		character.StartCoroutine(RemoveBuff(character, modifiers, index, duration));
	}

	public string GetDescription()
	{
		return "Grants " + buffBaseValue + " Agility for " + duration + " seconds.";
	}

	private static IEnumerator RemoveBuff(PlayerCharacter character, List<StatModifier> statModifiers, int index, float duration)
	{
		yield return new WaitForSeconds(duration);
		foreach (var modifier in statModifiers)
		{
			character.Stats.AllStats[index].RemoveAllModifiers(modifier);
		}
		character.UpdateStatValues();
	}
}
