using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StatDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] private Text nameText;
	[SerializeField] private Text valueBaseText;
	//[SerializeField] private Text valueMaxText;
	[SerializeField] private StatTooltip tooltip;

	private bool showingTooltip;
	private CharacterStat stat;

	public void Setup(CharacterStat characterStat)
    {
		stat = characterStat;
		nameText.text = stat.StatName;

		//if(stat.StatUseValues == EStatsValueUse.OnlyBaseValue)
  //      {
		//	valueMaxText.gameObject.SetActive(false);
  //      }
		UpdateStatValue();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
	//	tooltip.ShowTooltip(Stat, Name);
		showingTooltip = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		tooltip.HideTooltip();
		showingTooltip = false;
	}

	public void UpdateStatValue()
	{
		//if (showingTooltip) {
		//	tooltip.ShowTooltip(Stat, Name);
		//}

		if(stat.StatUseValues == EStatsValueUse.OnlyBaseValue)
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
