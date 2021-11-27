using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Crafting/Reciple Data")]
public class CraftingRecipeData : ScriptableObject
{
    public List<CraftingRecipe> CraftingRecipes { get => craftingRecipes; }

    [SerializeField] private List<CraftingRecipe> craftingRecipes = new List<CraftingRecipe>();
}
