using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnMap : BaseObjectOnMap
{
    private Item item;

    public override void InteractionOnWorldMap(EquipmentManager equipmentManager)
    {
        PickUp(equipmentManager);
    }

    public void Setup(BaseItemSlot slot)
    {
        item = slot.Item.GetCopy();

        objectSpriteRenderer.sprite = slot.Item.Icon;
        objectSpriteRenderer.sortingOrder = Constant.ItemOnMapOrderLayer - (int)transform.position.y;
    }

    private void PickUp(EquipmentManager equipmentManager)
    {
        bool addItemToInventory = equipmentManager.AddItemToInventory(item);
        if (addItemToInventory)
        {
            Destroy(gameObject);
        }
    }
}