using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipsPanels : MonoBehaviour
{
    [SerializeField] private ItemTooltip itemTooltip = null;

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

}
