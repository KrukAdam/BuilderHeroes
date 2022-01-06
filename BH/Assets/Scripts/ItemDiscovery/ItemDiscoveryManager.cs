using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDiscoveryManager : MonoBehaviour
{
    private List<Item> itemsDiscovered;
    private List<CraftingRecipe> reciplesDiscovered;
    private CraftingRecipesDatabase craftingRecipesDatabase;
    private TooltipsPanels tooltipsPanels;

    public void Setup(LocalController localController)
    {
        itemsDiscovered = new List<Item>();
        reciplesDiscovered = new List<CraftingRecipe>();

        craftingRecipesDatabase = GameManager.Instance.CraftingRecipesDatabase;
        tooltipsPanels = localController.GameUiManager.TooltipsPanels;

        localController.GameUiManager.CharacterPanels.InventoryPanel.OnAddItem += Discovered;
    }

    public void Discovered(Item item)
    {
        if(itemsDiscovered.Count <= 0)
        {
            DiscaveredNewItem(item);
        }
        else
        {
            if (!itemsDiscovered.Contains(item))
            {
                DiscaveredNewItem(item);
            }
        }
    }

    public bool HasRecipe(CraftingRecipe craftingRecipe)
    {
        return reciplesDiscovered.Contains(craftingRecipe);
    }

    private void DiscaveredNewItem(Item newItem)
    {
        StartShowTooltipForNewItem(newItem);
        itemsDiscovered.Add(newItem);

        DiscaveredNewRecipe(newItem);
    }

    private void DiscaveredNewRecipe(Item itemNeededToCraft)
    {
        foreach (var recipe in craftingRecipesDatabase.CraftingRecipes)
        {
            if (recipe.NeededItemToCraft(itemNeededToCraft))
            {
                if(reciplesDiscovered.Count <= 0)
                {
                    reciplesDiscovered.Add(recipe);
                    StartShowTooltipForNewItem(recipe);
                }
                else
                {
                    if (!reciplesDiscovered.Contains(recipe))
                    {
                        reciplesDiscovered.Add(recipe);
                        StartShowTooltipForNewItem(recipe);
                    }
                }
            }
        }
    }

    private void StartShowTooltipForNewItem(Item item)
    {
        tooltipsPanels.ShowNewItemsTooltip(item);
    }

    private void StartShowTooltipForNewItem(CraftingRecipe craftingRecipe)
    {
        foreach (var item in craftingRecipe.Results)
        {
            tooltipsPanels.ShowNewItemsTooltip(item.Item);
        }
    }
}
