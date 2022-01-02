using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipsPanels : MonoBehaviour
{
    [SerializeField] private ItemTooltip itemTooltip = null;
    [SerializeField] private StatTooltip statTooltip = null;
    [SerializeField] private Color statPositive = Color.green;
    [SerializeField] private Color statNegative = Color.red;

    private void Start()
    {
        statTooltip.Setup(statPositive, statNegative);
        itemTooltip.Setup(statPositive, statNegative);
    }

    public void ShowItemTooltip(BaseItemSlot itemSlot)
    {
        if (itemSlot.Item != null)
        {
            itemTooltip.ShowTooltip(itemSlot.Item);
        }
    }

    public void HideItemTooltip(BaseItemSlot itemSlot)
    {
        if (itemTooltip.gameObject.activeSelf)
        {
            itemTooltip.HideTooltip();
        }
    }

    public void ShowStatTooltip(CharacterStat characterStat)
    {
        statTooltip.ShowTooltip(characterStat);
    }

    public void HideStatTooltip()
    {
        statTooltip.HideTooltip();
    }

}
