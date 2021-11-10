using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BaseObjectOnMap : MonoBehaviour, IObjectOnMap
{
    [SerializeField] protected SpriteRenderer objectSpriteRenderer;

    private void Awake()
    {
        if (!objectSpriteRenderer) objectSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public virtual void InteractionOnWorldMap(EquipmentManager equipmentManager)
    {
        throw new System.NotImplementedException();
    }
}
