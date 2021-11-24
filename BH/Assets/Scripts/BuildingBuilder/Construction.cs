using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construction : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer = null;
    [SerializeField] private BoxCollider2D boxCollider = null;

    private Building building;
    private Vector2Int size;

    public void Setup(Building building)
    {
        this.building = building;
        size = building.Size;
        boxCollider.size = size;
        spriteRenderer.sprite = building.Sprite;
        SetSpritePosition();
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
}
