using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnMap : BaseObjectOnMap
{
    private Item item;
    private int amount;

    public override void InteractionOnWorldMap(LocalController localController)
    {
        PickUp(localController.LocalManagers.EquipmentManager);
    }

    public void Setup(BaseItemSlot slot)
    {
        item = slot.Item.GetCopy();
        amount = slot.Amount;

        objectSpriteRenderer.sprite = slot.Item.Icon;
        objectSpriteRenderer.sortingOrder = Constant.ItemOnMapOrderLayer - (int)transform.position.y;
    }

    public void Setup(Item item, int amount)
    {
        this.item = item.GetCopy();
        this.amount = amount;

        objectSpriteRenderer.sprite = item.Icon;
        objectSpriteRenderer.sortingOrder = Constant.ItemOnMapOrderLayer - (int)transform.position.y;
    }

    private void PickUp(EquipmentManager equipmentManager)
    {
        int itemPickUp = 0;
        for (int i = 0; i < amount; i++)
        {
            if (equipmentManager.AddItemToInventory(item))
            {
                itemPickUp++;
            }
            else
            {
                break;
            }
        }
        amount -= itemPickUp;
       
        if (amount <= 0)
        {
            Destroy(gameObject);
        }
    }
}
