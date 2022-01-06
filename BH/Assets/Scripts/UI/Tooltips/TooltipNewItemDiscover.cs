using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class TooltipNewItemDiscover : TooltipBase
{
    [SerializeField] private Image itemImage = null;
    [SerializeField] private LocalizeStringEvent localizedItemName = null;
    [SerializeField] private LocalizeStringEvent localizedItemType = null;
    [SerializeField] private LocalizeStringEvent localizedNewText = null;

    public override void ShowTooltip(Item item)
    {
        base.ShowTooltip(item);

        itemImage.sprite = item.Icon;
        localizedItemName.StringReference = item.ItemName;
        localizedItemType.StringReference = item.ItemType;
        localizedNewText.StringReference = GameManager.Instance.ConstLocalized.NEW;

        gameObject.SetActive(true);
    }

    public override void HideTooltip()
    {
        base.HideTooltip();
        HideModifiersBars();
        gameObject.SetActive(false);
    }
}
