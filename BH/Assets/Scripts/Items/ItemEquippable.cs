﻿using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Stats))]
[CreateAssetMenu(menuName = "Game/Items/Equippable Item")]
public class ItemEquippable : Item
{
	[Space]
	public EEquipmentType EquipmentType;

	public List<CharacterStat> ItemStats { get => itemStats; }
	public List<CharacterStat> ItemStatsPercentMult { get => itemStatsPercentMult; }
	public Skill MainSkill { get => mainSkill; }
	public Skill SecondSkill { get => secondSkill; }

	[SerializeField] private List<CharacterStat> itemStats = null;
	[SerializeField] private List<CharacterStat> itemStatsPercentMult = null;
	[SerializeField] private CharacterStatsData gameCharacterStatsData;
	[Space]
	[Header("If a weapon add skills")]
	[SerializeField] private Skill mainSkill = null;
	[SerializeField] private Skill secondSkill = null;

	private CharacterStatsData characterStatsData = null;

	public override Item GetCopy()
	{
		return Instantiate(this);
	}

	public override void Destroy()
	{
		Destroy(this);
	}

	public void Equip(PlayerCharacter c)
	{

        for (int i = 0; i < c.Stats.AllStats.Count; i++)
        {
            c.Stats.AllStats[i].AddModifierOnEquip(itemStats[i], this, EStatModifierType.Constants);
			c.Stats.AllStats[i].AddModifierOnEquip(itemStatsPercentMult[i], this, EStatModifierType.PercentAdd);
		}
	}

	public void Unequip(PlayerCharacter c)
	{
        foreach (var stat in c.Stats.AllStats)
        {
			stat.RemoveAllModifiersFromSource(this);
        }
	}

	public override string GetDescription()
	{
        sb.Length = 0;

        foreach (var stat in itemStats)
        {
            if (stat.StatUseValues == EStatsValueUse.OnlyBaseValue)
            {
				AddStat(stat.BaseValue.Value, stat.StatName.GetLocalizedString());
			}
            else if(stat.StatUseValues == EStatsValueUse.MinAndMaxValue)
            {
				AddStat(stat.MinValue.Value, "Min. " + stat.StatName.GetLocalizedString());
				AddStat(stat.MaxValue.Value, "Max. " + stat.StatName.GetLocalizedString());
			}
			else if (stat.StatUseValues == EStatsValueUse.BaseAndMaxValue)
			{
				AddStat(stat.BaseValue.Value, "Current stats value cant by use in eq items");
				AddStat(stat.MaxValue.Value, "Max. " + stat.StatName.GetLocalizedString());
			}
		}
		foreach (var stat in itemStatsPercentMult)
		{
			if (stat.StatUseValues == EStatsValueUse.OnlyBaseValue)
			{
				AddStat(stat.BaseValue.Value, stat.StatName.GetLocalizedString(), true);
			}
			else if (stat.StatUseValues == EStatsValueUse.MinAndMaxValue)
			{
				AddStat(stat.MinValue.Value, "Min. " + stat.StatName.GetLocalizedString(), true);
				AddStat(stat.MaxValue.Value, "Max. " + stat.StatName.GetLocalizedString(), true);
			}
			else if (stat.StatUseValues == EStatsValueUse.BaseAndMaxValue)
			{
				AddStat(stat.BaseValue.Value, "Current stats value cant by use in eq items");
				AddStat(stat.MaxValue.Value, "Max. " + stat.StatName.GetLocalizedString(), true);
			}
		}
		return sb.ToString();
	}


	public virtual void SetEmptyStats()
	{
		characterStatsData = Instantiate(gameCharacterStatsData);
		itemStats = characterStatsData.AllStats;
		itemStatsPercentMult = itemStats;
	}

	private void AddStat(float value, string statName, bool isPercent = false)
	{
		if (value != 0)
		{
			if (sb.Length > 0)
				sb.AppendLine();

			if (value > 0)
				sb.Append("+");

			if (isPercent) {
				sb.Append(value * 100);
				sb.Append("% ");
			} else {
				sb.Append(value);
				sb.Append(" ");
			}
			sb.Append(statName);
		}
	}
}

