using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construction : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer = null;

    private Vector2Int size;

    public void Setup(Building building)
    {
        spriteRenderer.sprite = building.Sprite;
        size = building.Size;
    }

    public bool CheckBuildSpace(LayerMask layerMask)
    {
        //TODO kierunek liczenia zalezny od kierunku punktu akcji

        Vector2Int interactionPos = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        Collider2D objectOnRange = Physics2D.OverlapArea(interactionPos, interactionPos + size, layerMask);
        if (objectOnRange)
        {
            Debug.Log("Wrong space to build. Blocking pos: " + objectOnRange.gameObject.transform.position+ " layer: " + objectOnRange.gameObject.layer);
            return false;
        }
        return true;
    }
}
