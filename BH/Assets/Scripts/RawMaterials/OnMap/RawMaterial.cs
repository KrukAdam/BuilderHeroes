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
    [SerializeField] protected bool randomSprites = true;
    [SerializeField] protected Sprite[] spritesRawMaterial = null;
    [SerializeField] private Effects effectsPrefab = null;

    protected ItemTool toolNeeded;
    protected Transform itemsDropParent;
    protected Effects effects;

    private void Awake()
    {
        itemsDropParent = transform.parent.transform;
        SetOrderLayer();
        SetSprite();
        SetupEffects();
    }

    public override void InteractionOnWorldMap(LocalController localController)
    {
        toolNeeded = localController.LocalManagers.EquipmentManager.GetItemTool(toolTypeNeeded);
        if (toolNeeded)
        {
            OnInteraction();
            DamageRawMaterial();
            localController.LocalManagers.EquipmentManager.DamageTool();
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
        if (effects) effects.Show();
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
                GameObject itemOnMapGO = Instantiate(itemOnMapPrefab.gameObject, itemsDropParent);
                itemOnMapGO.transform.position = transform.position;
                itemOnMapGO.GetComponent<ItemOnMap>().Setup(item.ItemDroppedPrefab, numbersOfDroppedItems);
            }
        }
    }

    protected virtual void DestroyRawMaterial()
    {
        Destroy(gameObject);
    }

    protected virtual void SetSprite()
    {
        if (!randomSprites || spritesRawMaterial == null || spritesRawMaterial.Length <= 0) return;

        objectSpriteRenderer.sprite = spritesRawMaterial[Random.Range(0, spritesRawMaterial.Length - 1)];
    }

    private void SetupEffects()
    {
        if (!effectsPrefab) return;

        int layer = objectSpriteRenderer.sortingOrder +1;
        effects = Instantiate(effectsPrefab, transform);
        effects.Setup(layer, transform);
    }
}
