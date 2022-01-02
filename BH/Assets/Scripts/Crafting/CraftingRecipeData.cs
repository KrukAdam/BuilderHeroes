using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Crafting/Recipe Data")]
public class CraftingRecipeData : ScriptableObject
{
    public List<CraftingRecipe> CraftingRecipes { get => craftingRecipes; }
    public EBuildingType BuildingType { get => buildingType; }

    [SerializeField] private EBuildingType buildingType = EBuildingType.None;
    [SerializeField] private List<CraftingRecipe> craftingRecipes = new List<CraftingRecipe>();

}
