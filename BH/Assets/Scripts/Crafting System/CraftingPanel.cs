using System;
using System.Collections.Generic;
using UnityEngine;

public class CraftingPanel : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private GameObject recipeUIPrefab;
	[SerializeField] private RectTransform recipeUIParent;
	public List<CraftingRecipeUI> craftingRecipeUIs;

	[Header("Public Variables")]
	public ItemContainer ItemContainer;
	public List<CraftingRecipe> CraftingRecipes;

	public event Action<BaseItemSlot> OnPointerEnterEvent;
	public event Action<BaseItemSlot> OnPointerExitEvent;


	public void OnOpenPanel()
    {
		//recipeUIParent.GetComponentsInChildren<CraftingRecipeUI>(includeInactive: true, result: craftingRecipeUIs);
		UpdateCraftingRecipes();

		foreach (CraftingRecipeUI craftingRecipeUI in craftingRecipeUIs)
		{
			craftingRecipeUI.OnPointerEnterEvent += slot => OnPointerEnterEvent(slot);
			craftingRecipeUI.OnPointerExitEvent += slot => OnPointerExitEvent(slot);
		}
	}

	public void UpdateCraftingRecipes()
	{
		for (int i = 0; i < CraftingRecipes.Count; i++)
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
			craftingRecipeUIs.Add(uiRecipleObj.GetComponent<CraftingRecipeUI>());
			Debug.Log(craftingRecipeUIs[i] + "    " + ItemContainer);
			craftingRecipeUIs[i].ItemContainer = ItemContainer;
			craftingRecipeUIs[i].CraftingRecipe = CraftingRecipes[i];
		}

		for (int i = CraftingRecipes.Count; i < craftingRecipeUIs.Count; i++)
		{
			craftingRecipeUIs[i].CraftingRecipe = null;
		}
	}
}
