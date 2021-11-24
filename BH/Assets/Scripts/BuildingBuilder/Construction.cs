using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construction : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer = null;
    [SerializeField] private BoxCollider2D boxCollider = null;
    [SerializeField] private Color colorGoodPlace = Color.green;
    [SerializeField] private Color colorWrongPlace = Color.red;

    private Vector2Int size;
    private Vector2 halfSize;
    private Vector2 contructPos;
    private Transform interactionPointer;
    private LayerMask layerMaskBlockingBuild;

    private void Update()
    {
        SetPosition();
    }

    private void FixedUpdate()
    {
        if (CanBuild())
        {
            spriteRenderer.color = colorGoodPlace;
        }
        else
        {
            spriteRenderer.color = colorWrongPlace;
        }
    }

    public void Setup(Building building, Transform interactionPointer, LayerMask layerMask)
    {
        this.interactionPointer = interactionPointer;
        layerMaskBlockingBuild = layerMask;
        spriteRenderer.sprite = building.Sprite;
        spriteRenderer.sortingOrder = Constant.ContructionOrderLayer;
        size = building.Size;

        boxCollider.size = size;
        SetHalfSize();
    }

    public bool CanBuild()
    {
        Collider2D objectOnRange = Physics2D.OverlapBox(transform.position, size, 0, layerMaskBlockingBuild);
        if (objectOnRange)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void SetPosition()
    {
        contructPos = Vector2.zero;
        if (interactionPointer.localPosition.x != 0)
        {
            if(interactionPointer.localPosition.x > 0)
            {
                contructPos.x = interactionPointer.localPosition.x + halfSize.x;
            }
            else
            {
                contructPos.x = interactionPointer.localPosition.x - halfSize.x;
            }
        }
        else if (interactionPointer.localPosition.y != 0)
        {
            if (interactionPointer.localPosition.y > 0)
            {
                contructPos.y = interactionPointer.localPosition.y + halfSize.y;
            }
            else
            {
                contructPos.y = interactionPointer.localPosition.y - halfSize.y;
            }
        }
        transform.localPosition = contructPos;
    }

    private void SetHalfSize()
    {
        halfSize = Vector2.zero;
        if(size.x >= 0)
        {
            halfSize.x = size.x / 2;
        }
        if (size.y >= 0)
        {
            halfSize.y = size.y / 2;
        }
    }
}
