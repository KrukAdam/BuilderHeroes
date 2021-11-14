using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawMaterialWood : RawMaterial
{
    [SerializeField] private SpriteRenderer treeBranches = null;
    [SerializeField] protected bool randomTreeBranchesSprites = true;
    [SerializeField] protected Sprite[] spritesTreeBranches = null;

    protected override void SetOrderLayer()
    {
        base.SetOrderLayer();
        treeBranches.sortingOrder = Constant.PlayerStartOrderLayer - (int)transform.position.y + 1;
    }

    protected override void SetSprite()
    {
        base.SetSprite();

        if (!randomTreeBranchesSprites || spritesTreeBranches == null || spritesTreeBranches.Length <= 0) return;

        treeBranches.sprite = spritesTreeBranches[Random.Range(0, spritesTreeBranches.Length - 1)];
    }
}
