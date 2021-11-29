using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionLabel : MonoBehaviour
{
    [SerializeField] private BaseItemSlot itemSlot = null;
    [SerializeField] private Text itemNeeded = null;
    [SerializeField] private Text itemHas = null;
    [SerializeField] private BasicButton btnAddItem = null;

    private ConstructionPanel constructionPanel;
    private ConstructItemData data;

    public void Setup(ConstructionPanel constructionPanel)
    {
        this.constructionPanel = constructionPanel;

        btnAddItem.SetupListener(AddItem);
    }

    public void SetupItem(ConstructItemData itemNeeded)
    {
        data = itemNeeded;

        itemSlot.Item = itemNeeded.Item;
        this.itemNeeded.text = itemNeeded.ItemNeeded.ToString();
        this.itemHas.text = itemNeeded.ItemHas.ToString();

        SetButtonVisible();
    }

    private void AddItem()
    {
        if (constructionPanel.AddItemToConstruction(data))
        {
            this.itemHas.text = data.ItemHas.ToString();
            SetButtonVisible();
        }
    }

    private void SetButtonVisible()
    {
        if (data.ItemHas >= data.ItemNeeded)
        {
            btnAddItem.ChangeSelectableBlockState(false);
        }
        else
        {
            btnAddItem.ChangeSelectableBlockState(true);
        }
    }
}
