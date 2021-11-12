using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class RawMaterial : BaseObjectOnMap
{
    [SerializeField] protected EItemToolType toolTypeNeeded = EItemToolType.None;
    [SerializeField] protected float durability = 1;
    [SerializeField] protected RawMaterialDropped[] rawMaterialsDroped = null;

    protected ItemTool toolNeeded;

    public override void InteractionOnWorldMap(EquipmentManager equipmentManager)
    {
        toolNeeded = equipmentManager.GetItemTool(toolTypeNeeded);
        if (toolNeeded)
        {

            OnInteraction();
            DamageRawMaterial();
        }
        else
        {
            Debug.Log("Cant get it! Needed a: " + toolTypeNeeded);
        }
    }

    protected virtual void OnInteraction() 
    {

    }

    protected virtual void DamageRawMaterial()
    {
        durability -= toolNeeded.DamageRawMaterial;
        if (durability <= 0) DestroyRawMaterial();
    }

    protected virtual void DestroyRawMaterial()
    {
        Destroy(gameObject);
    }
}
