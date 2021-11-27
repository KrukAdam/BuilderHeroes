using System;
using System.Collections.Generic;
using UnityEngine;

public class CraftingPanel : MonoBehaviour
{
    public event Action<BaseItemSlot> OnPointerEnterEvent;
    public event Action<BaseItemSlot> OnPointerExitEvent;

    [Header("References")]
    [SerializeField] private CraftingRecipeUI recipeUIPrefab = null;
    [SerializeField] private int startInstantiateRecipe = 10;
    [SerializeField] private RectTransform recipeUIParent = null;

    private ItemContainer itemContainer;
    private List<CraftingRecipe> craftingRecipes = new List<CraftingRecipe>();
    private List<CraftingRecipeUI> craftingRecipesUI = new List<CraftingRecipeUI>();
    private BuildingCraft buildingCraft;

    public void Setup(ItemContainer itemContainer)
    {
        this.itemContainer = itemContainer;
        InstantiateRecipesUi();
    }

    public void OnOpen(Building building)
    {
        buildingCraft = building as BuildingCraft;
        if (!buildingCraft) return;

        craftingRecipes = buildingCraft.CraftingRecipeData.CraftingRecipes;

        OffRecipesUi();
        SetAndOnRecipes();

        foreach (CraftingRecipeUI craftingRecipeUI in craftingRecipesUI)
        {
            craftingRecipeUI.OnPointerEnterEvent += slot => OnPointerEnterEvent(slot);
            craftingRecipeUI.OnPointerExitEvent += slot => OnPointerExitEvent(slot);
        }
    }

    private void SetAndOnRecipes()
    {
        for (int i = 0; i < craftingRecipes.Count; i++)
        {
            if(craftingRecipesUI.Count <= i)
            {
                CraftingRecipeUI recipeUI = Instantiate(recipeUIPrefab, recipeUIParent);
                craftingRecipesUI.Add(recipeUI);
            }
            craftingRecipesUI[i].gameObject.SetActive(true);
            craftingRecipesUI[i].Setup(craftingRecipes[i], itemContainer);
        }
    }

    private void InstantiateRecipesUi()
    {
        for (int i = 0; i < startInstantiateRecipe; i++)
        {
            CraftingRecipeUI recipeUI = Instantiate(recipeUIPrefab, recipeUIParent);
            craftingRecipesUI.Add(recipeUI);
            recipeUI.gameObject.SetActive(false);
        }
    }

    private void OffRecipesUi()
    {
        foreach (var recipe in craftingRecipesUI)
        {
            if (recipe.gameObject.activeSelf)
            {
                recipe.gameObject.SetActive(false);
            }
        }
    }
}
