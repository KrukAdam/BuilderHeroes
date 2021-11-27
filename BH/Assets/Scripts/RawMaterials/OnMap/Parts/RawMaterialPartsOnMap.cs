using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawMaterialPartsOnMap : BaseObjectOnMap
{
    [SerializeField] private Item itemRawMaterialPrefab = null;

    private Item item;

    private void Start()
    {
        item = itemRawMaterialPrefab.GetCopy();

        objectSpriteRenderer.sortingOrder = Constant.ItemOnMapOrderLayer - (int)transform.position.y;
    }

    public override void InteractionOnWorldMap(LocalController localController)
    {
        PickUp(localController.LocalManagers.EquipmentManager);
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
