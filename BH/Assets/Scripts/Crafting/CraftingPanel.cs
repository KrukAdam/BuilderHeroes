using System;
using System.Collections.Generic;
using UnityEngine;

public class CraftingPanel : MonoBehaviour
{
    public event Action<BaseItemSlot> OnPointerEnterEvent;
    public event Action<BaseItemSlot> OnPointerExitEvent;

    [SerializeField] private CraftingRecipeUI recipeUIPrefab = null;
    [SerializeField] private int startInstantiateRecipe = 10;
    [SerializeField] private RectTransform recipeUIParent = null;

    private List<CraftingRecipe> craftingRecipes = new List<CraftingRecipe>();
    private List<CraftingRecipeUI> craftingRecipesUI = new List<CraftingRecipeUI>();
    private BuildingCraft buildingCraft;
    private ItemContainer itemContainer;
    private ItemDiscoveryManager itemDiscoveryManager;

    public void Setup(LocalController localController)
    {
        this.itemContainer = localController.GameUiManager.CharacterPanels.InventoryPanel;
        this.itemDiscoveryManager = localController.LocalManagers.ItemDiscoveryManager;

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
            if (itemDiscoveryManager.HasRecipe(craftingRecipes[i]))
            {
                if (craftingRecipesUI.Count <= i)
                {
                    CraftingRecipeUI recipeUI = Instantiate(recipeUIPrefab, recipeUIParent);
                    craftingRecipesUI.Add(recipeUI);
                }
                craftingRecipesUI[i].gameObject.SetActive(true);
                craftingRecipesUI[i].Setup(craftingRecipes[i], itemContainer);
            }
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
