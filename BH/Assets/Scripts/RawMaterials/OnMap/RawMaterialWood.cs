using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawMaterialWood : RawMaterial
{
    [SerializeField] private SpriteRenderer treeBranches = null;

    protected override void SetOrderLayer()
    {
        base.SetOrderLayer();
        treeBranches.sortingOrder = Constant.PlayerStartOrderLayer - (int)transform.position.y + 1;
    }
}
