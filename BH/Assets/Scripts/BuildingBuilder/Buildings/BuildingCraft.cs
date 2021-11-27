using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buildings/Craft Building")]
public class BuildingCraft : Building
{
    public CraftingRecipeData CraftingRecipeData { get => craftingRecipeData; }

    [SerializeField] private CraftingRecipeData craftingRecipeData = null;
}
