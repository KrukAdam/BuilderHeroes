using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StatDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public BaseStat Stat {
		get { return stat; }
		set {
			stat = value;
			UpdateStatValue();
		}
	}

	public string Name
	{
		get { return displayName; }
		set
		{
			displayName = value;
			nameText.text = displayName.ToLower();
		}
	}

	private string displayName;
	private BaseStat stat;

	[SerializeField] Text nameText;
	[SerializeField] Text valueText;
	[SerializeField] StatTooltip tooltip;

	private bool showingTooltip;

	//private void OnValidate()
	//{
	//	Text[] texts = GetComponentsInChildren<Text>();
	//	nameText = texts[0];
	//	valueText = texts[1];

	//	if (tooltip == null)
	//		tooltip = FindObjectOfType<StatTooltip>();
	//}

	public void OnPointerEnter(PointerEventData eventData)
	{
		tooltip.ShowTooltip(Stat, Name);
		showingTooltip = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		tooltip.HideTooltip();
		showingTooltip = false;
	}

	public void UpdateStatValue()
	{
		valueText.text = stat.Value.ToString();
		if (showingTooltip) {
			tooltip.ShowTooltip(Stat, Name);
		}
	}
}
