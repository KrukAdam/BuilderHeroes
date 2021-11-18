using System;
using System.Collections.Generic;
using UnityEngine;

public class CraftingPanel : MonoBehaviour
{

	public event Action<BaseItemSlot> OnPointerEnterEvent;
	public event Action<BaseItemSlot> OnPointerExitEvent;

	[Header("References")]
	[SerializeField] private GameObject recipeUIPrefab;
	[SerializeField] private RectTransform recipeUIParent;
	[SerializeField] private ItemContainer itemContainer;
	[SerializeField] private List<CraftingRecipe> craftingRecipes;
	[SerializeField] private List<CraftingRecipeUI> craftingRecipesUI;

	public void OnOpenPanel()
    {
		//recipeUIParent.GetComponentsInChildren<CraftingRecipeUI>(includeInactive: true, result: craftingRecipeUIs);
		UpdateCraftingRecipes();

		foreach (CraftingRecipeUI craftingRecipeUI in craftingRecipesUI)
		{
			craftingRecipeUI.OnPointerEnterEvent += slot => OnPointerEnterEvent(slot);
			craftingRecipeUI.OnPointerExitEvent += slot => OnPointerExitEvent(slot);
		}
	}

	public void UpdateCraftingRecipes()
	{
		for (int i = 0; i < craftingRecipes.Count; i++)
		{
			//if (craftingRecipeUIs.Count == i)
			//{
			//	craftingRecipeUIs.Add(Instantiate(recipeUIPrefab, recipeUIParent, false));
			//}
			//else if (craftingRecipeUIs[i] == null)
			//{
			//	craftingRecipeUIs[i] = Instantiate(recipeUIPrefab, recipeUIParent, false);
			//}

		    GameObject uiRecipleObj = Instantiate(recipeUIPrefab, recipeUIParent, false);
			craftingRecipesUI.Add(uiRecipleObj.GetComponent<CraftingRecipeUI>());
			Debug.Log(craftingRecipesUI[i] + "    " + itemContainer);
			craftingRecipesUI[i].ItemContainer = itemContainer;
			craftingRecipesUI[i].CraftingRecipe = craftingRecipes[i];
		}

		for (int i = craftingRecipes.Count; i < craftingRecipesUI.Count; i++)
		{
			craftingRecipesUI[i].CraftingRecipe = null;
		}
	}
}
