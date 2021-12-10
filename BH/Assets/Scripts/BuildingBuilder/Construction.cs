using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construction : MonoBehaviour, IObjectOnMap
{
    public List<ConstructItemData> ItemsNeeded { get => itemsNeeded; }

    [SerializeField] private SpriteRenderer spriteRenderer = null;
    [SerializeField] private BoxCollider2D boxCollider = null;

    private ItemContainer itemContainer;
    private Building building;
    private Vector2Int size;
    private bool built = false;
    private List<ConstructItemData> itemsNeeded;
    private DropItemManager dropItemManager;

    public void InteractionOnWorldMap(LocalController localController)
    {
        if (built)
        {
            localController.GameUiManager.OpenBuildingPanel(building);
        }
        else
        {
            this.itemContainer = localController.GameUiManager.CharacterPanels.InventoryPanel;
            localController.GameUiManager.CityBuilderPanels.ConstructionPanel.SetContruction(this);
            localController.GameUiManager.ToggleContructionPanel();
        }
    }

    public void Setup(Building building, DropItemManager dropItemManager)
    {
        this.dropItemManager = dropItemManager;
        this.building = building;
        size = building.Size;
        boxCollider.size = size;
        spriteRenderer.sprite = building.Sprite;
        SetSpritePosition();
        SetupItemsNeeded();
    }

    public bool AddConstructItem(ConstructItemData constructItemData)
    {
        if (itemContainer.RemoveItem(constructItemData.Item))
        {
            constructItemData.ItemHas++;
            return true;
        }

        return false;
    }

    public bool Build()
    {
        foreach (var item in itemsNeeded)
        {
            if(item.ItemHas < item.ItemNeeded)
            {
                return false;
            }
        }

        built = true;
        return true;
    }

    public void Destroy()
    {
        foreach (var item in itemsNeeded)
        {
            if(item.ItemHas > 1)
            {
                dropItemManager.DropItemOnMap(item.Item, item.ItemHas / 2, transform.position);
            }
        }
    }

    private void SetSpritePosition()
    {
        float yPos = size.x - size.y;
        if (yPos <= 0)
        {
            yPos = 0.45f;
        }
        else
        {
            yPos = yPos / 2;
        }

        Vector2 pos = new Vector2(0, yPos);
        spriteRenderer.gameObject.transform.localPosition = pos;

        spriteRenderer.sortingOrder = Constant.BaseStartOrderLayer - (int)transform.position.y;
    }

    private void SetupItemsNeeded()
    {
        itemsNeeded = new List<ConstructItemData>();
        foreach (var item in building.ItemsToConstruction)
        {
            itemsNeeded.Add(new ConstructItemData(item));
        }
    }
}
