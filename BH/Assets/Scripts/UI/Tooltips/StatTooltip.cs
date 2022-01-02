using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class StatTooltip : BaseTooltip
{
	[SerializeField] private LocalizeStringEvent localizeStatName = null;

	public override void ShowTooltip(CharacterStat stat)
	{
		base.ShowTooltip(stat);
		localizeStatName.StringReference = stat.StatName;
		ShowModifiers(stat);
		gameObject.SetActive(true);
	}

    public override void HideTooltip()
    {
        base.HideTooltip();
		HideModifiersBars();
		gameObject.SetActive(false);
	}

    protected override void ShowModifiers(CharacterStat characterStat)
    {
        base.ShowModifiers(characterStat);
		Color textColor;
		if (characterStat.StatUseValues == EStatsValueUse.OnlyBaseValue)
		{
			for (int i = 0; i < characterStat.BaseValue.StatModifiers.Count; i++)
			{
				StatModifier mod = characterStat.BaseValue.StatModifiers[i];
				sb.Clear();

				if (mod.Value > 0)
				{
					sb.Append("+");
					textColor = statPositive;
				}
				else
				{
					textColor = statNegative;
				}

				if (mod.Type == EStatModifierType.Constants)
				{
					sb.Append(mod.Value);
				}
				else
				{
					sb.Append(mod.Value);
					sb.Append("%");
				}

				Item item = mod.Source as Item;

				if (item != null)
				{
					sb.Append(" ");
					sb.Append(item.ItemName.GetLocalizedString());
				}
				else
				{
					// Debug.LogWarning("Modifier is not an Item!");
				}

				ShowModifierBar(sb, i, textColor);
			}
		}
		if (characterStat.StatUseValues == EStatsValueUse.BaseAndMaxValue)
		{
			for (int i = 0; i < characterStat.MaxValue.StatModifiers.Count; i++)
			{
				StatModifier mod = characterStat.MaxValue.StatModifiers[i];
				sb.Clear();

				if (mod.Value > 0)
				{
					sb.Append("+");
					textColor = statPositive;
				}
				else
				{
					textColor = statNegative;
				}

				if (mod.Type == EStatModifierType.Constants)
				{
					sb.Append(mod.Value);
				}
				else
				{
					sb.Append(mod.Value);
					sb.Append("%");
				}

				Item item = mod.Source as Item;

				if (item != null)
				{
					sb.Append(" ");
					sb.Append(item.ItemName.GetLocalizedString());
				}
				else
				{
					// Debug.LogWarning("Modifier is not an Item!");
				}

				ShowModifierBar(sb, i, textColor);
			}
		}
		if (characterStat.StatUseValues == EStatsValueUse.MinAndMaxValue)
		{
			int startMaxOnIndex = 0;
			for (int i = 0; i < characterStat.MinValue.StatModifiers.Count; i++)
			{
				StatModifier mod = characterStat.MinValue.StatModifiers[i];
				sb.Clear();
				sb.Append(GameManager.Instance.ConstLocalized.StatMinValue.GetLocalizedString() + " ");

				if (mod.Value > 0)
				{
					sb.Append("+");
					textColor = statPositive;
				}
				else
				{
					textColor = statNegative;
				}

				if (mod.Type == EStatModifierType.Constants)
				{
					sb.Append(mod.Value);
				}
				else
				{
					sb.Append(mod.Value);
					sb.Append("%");
				}

				Item item = mod.Source as Item;

				if (item != null)
				{
					sb.Append(" ");
					sb.Append(item.ItemName.GetLocalizedString());
				}
				else
				{
					// Debug.LogWarning("Modifier is not an Item!");
				}

				ShowModifierBar(sb, i, textColor);
				startMaxOnIndex++;
			}
			for (int i = 0; i < characterStat.MaxValue.StatModifiers.Count; i++)
			{
				StatModifier mod = characterStat.MaxValue.StatModifiers[i];
				sb.Clear();
				sb.Append(GameManager.Instance.ConstLocalized.StatMaxValue.GetLocalizedString() + " ");
				if (mod.Value > 0)
				{
					sb.Append("+");
					textColor = statPositive;
				}
				else
				{
					textColor = statNegative;
				}

				if (mod.Type == EStatModifierType.Constants)
				{
					sb.Append(mod.Value);
				}
				else
				{
					sb.Append(mod.Value);
					sb.Append("%");
				}

				Item item = mod.Source as Item;

				if (item != null)
				{
					sb.Append(" ");
					sb.Append(item.ItemName.GetLocalizedString());
				}
				else
				{
					// Debug.LogWarning("Modifier is not an Item!");
				}

				ShowModifierBar(sb, startMaxOnIndex + i, textColor);
			}
		}
	}
}
