using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Components;
using System.Collections;
using UnityEngine.Localization.Settings;
using System;

public class StatDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public event Action<CharacterStat> OnPointerEnterEvent;
	public event Action OnPointerExitEvent;

	[SerializeField] private LocalizeStringEvent nameText;
	[SerializeField] private Text valueBaseText;
	[SerializeField] private StatTooltip tooltip;

	private CharacterStat stat;

	public void Setup(CharacterStat characterStat, LocalController localController)
	{
		stat = characterStat;
		nameText.StringReference = stat.StatName;

		UpdateStatValue();

		OnPointerEnterEvent += localController.GameUiManager.TooltipsPanels.ShowStatTooltip;
		OnPointerExitEvent += localController.GameUiManager.TooltipsPanels.HideStatTooltip;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		OnPointerEnterEvent(stat);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		OnPointerExitEvent();
	}

	public void UpdateStatValue()
	{
		if (stat.StatUseValues == EStatsValueUse.OnlyBaseValue)
		{
			valueBaseText.text = stat.BaseValue.Value.ToString();
		}
		if (stat.StatUseValues == EStatsValueUse.MinAndMaxValue)
		{
			valueBaseText.text = stat.MinValue.Value.ToString() + " - " + stat.MaxValue.Value.ToString();
		}
		if (stat.StatUseValues == EStatsValueUse.BaseAndMaxValue)
		{
			valueBaseText.text = stat.BaseValue.Value.ToString() + " / " + stat.MaxValue.Value.ToString();
		}
	}

}
