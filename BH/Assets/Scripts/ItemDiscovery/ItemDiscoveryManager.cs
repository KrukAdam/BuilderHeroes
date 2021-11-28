using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDiscoveryManager : MonoBehaviour
{
    private List<Item> itemsDiscovered;
    private List<CraftingRecipe> reciplesDiscovered;
    private CraftingRecipesDatabase craftingRecipesDatabase;

    public void Setup(LocalController localController)
    {
        itemsDiscovered = new List<Item>();
        reciplesDiscovered = new List<CraftingRecipe>();

        craftingRecipesDatabase = GameManager.Instance.CraftingRecipesDatabase;

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
        Debug.Log("New item : " + newItem.ItemName);
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
                    Debug.Log("New Recipe discavered. Item: " + recipe);
                    reciplesDiscovered.Add(recipe);
                }
                else
                {
                    if (!reciplesDiscovered.Contains(recipe))
                    {
                        Debug.Log("New Recipe discavered. Item: " + recipe);
                        reciplesDiscovered.Add(recipe);
                    }
                }
            }
        }
    }
}
