using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class RawMaterial : BaseObjectOnMap
{
    [SerializeField] protected EItemToolType toolTypeNeeded = EItemToolType.None;
    [SerializeField] protected float durability = 1;
    [SerializeField] protected ItemOnMap itemOnMapPrefab = null;
    [SerializeField] protected ItemDropped[] rawMaterialsDroped = null;
    [SerializeField] protected bool randomRawMaterialSprites = true;
    [SerializeField] protected Sprite[] spritesRawMaterial = null;

    protected ItemTool toolNeeded;
    protected Transform itemsDropParent;

    private void Awake()
    {
        itemsDropParent = transform.parent.transform;
        SetOrderLayer();
        SetSprite();
    }

    public override void InteractionOnWorldMap(LocalController localController)
    {
        toolNeeded = localController.LocalManagers.EquipmentManager.GetItemTool(toolTypeNeeded);
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

    protected virtual void SetOrderLayer()
    {
        objectSpriteRenderer.sortingOrder = Constant.BaseStartOrderLayer - (int)transform.position.y;
    }

    protected virtual void OnInteraction()
    {

    }

    protected virtual void DamageRawMaterial()
    {
        durability -= toolNeeded.DamageRawMaterial;
        if (durability <= 0)
        {
            DropItems();
            DestroyRawMaterial();
        }
    }

    protected virtual void DropItems()
    {
        if (rawMaterialsDroped.Length <= 0) return;

        foreach (var item in rawMaterialsDroped)
        {
            if (Random.Range(0f, 100f) <= item.DropChance)
            {
                int numbersOfDroppedItems = Random.Range(1, item.DropMax);
                for (int i = 0; i < numbersOfDroppedItems; i++)
                {
                    GameObject itemOnMapGO = Instantiate(itemOnMapPrefab.gameObject, itemsDropParent);
                    itemOnMapGO.transform.position = transform.position;
                    itemOnMapGO.GetComponent<ItemOnMap>().Setup(item.ItemDroppedPrefab);
                }
            }
        }
    }

    protected virtual void DestroyRawMaterial()
    {
        Destroy(gameObject);
    }

    protected virtual void SetSprite()
    {
        if (!randomRawMaterialSprites || spritesRawMaterial == null || spritesRawMaterial.Length <= 0) return;

        objectSpriteRenderer.sprite = spritesRawMaterial[Random.Range(0, spritesRawMaterial.Length-1)];
    }
}
