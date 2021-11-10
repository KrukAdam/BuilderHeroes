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

	public void ExecuteEffect(AuraSkill aura, Stats stats)
	{
		StatModifier baseValueStatModifier;
		StatModifier minValueStatModifier;
		StatModifier maxValueStatModifier;

		if (isPercentMult)
		{
			baseValueStatModifier = new StatModifier(buffBaseValue, StatModType.PercentMult, aura);
			minValueStatModifier = new StatModifier(buffMinValue, StatModType.PercentMult, aura);
			maxValueStatModifier = new StatModifier(buffMaxValue, StatModType.PercentMult, aura);
		}
		else
		{
			baseValueStatModifier = new StatModifier(buffBaseValue, StatModType.Flat, aura);
			minValueStatModifier = new StatModifier(buffMinValue, StatModType.Flat, aura);
			maxValueStatModifier = new StatModifier(buffMaxValue, StatModType.Flat, aura);
		}

		int index = 0;
		List<StatModifier> modifiers = new List<StatModifier>();
		for (int i = 0; i < stats.AllStats.Count; i++)
		{
			if (buffStatType == stats.AllStats[i].StatType)
			{
				stats.AllStats[i].BaseValue.AddModifier(baseValueStatModifier);
				stats.AllStats[i].MinValue.AddModifier(minValueStatModifier);
				stats.AllStats[i].MaxValue.AddModifier(maxValueStatModifier);
				index = i;
				modifiers.Add(baseValueStatModifier);
				modifiers.Add(minValueStatModifier);
				modifiers.Add(maxValueStatModifier);
			}
		}

		stats.StartCoroutine(RemoveBuff(stats, modifiers, index, duration));
		stats.AuraRefresh();
	}

	private static IEnumerator RemoveBuff(Stats stats, List<StatModifier> statModifiers, int index, float duration)
	{
		yield return new WaitForSeconds(duration);
		foreach (var modifier in statModifiers)
		{
			stats.AllStats[index].RemoveAllModifiers(modifier);
		}
		stats.AuraRefresh();
	}
}
