using System;
using System.Collections.Generic;
using UnityEngine;

public class CraftingRecipeUI : MonoBehaviour
{

	public event Action<BaseItemSlot> OnPointerEnterEvent;
	public event Action<BaseItemSlot> OnPointerExitEvent;

	[SerializeField] private RectTransform arrowParent;
	[SerializeField] private BaseItemSlot[] itemSlots;

	private CraftingRecipe craftingRecipe;
	private ItemContainer itemContainer;

	public void Setup(CraftingRecipe craftingRecipe, ItemContainer itemContainer)
    {
		this.itemContainer = itemContainer;
		SetCraftingRecipe(craftingRecipe);

		foreach (BaseItemSlot itemSlot in itemSlots)
		{
			itemSlot.OnPointerEnterEvent += slot => OnPointerEnterEvent(slot);
			itemSlot.OnPointerExitEvent += slot => OnPointerExitEvent(slot);
		}
	}

	public void OnCraftButtonClick()
	{
		if (craftingRecipe != null && itemContainer != null)
		{
			craftingRecipe.Craft(itemContainer);
		}
	}

	private void SetCraftingRecipe(CraftingRecipe newCraftingRecipe)
	{
		craftingRecipe = newCraftingRecipe;

		if (craftingRecipe != null)
		{
			int slotIndex = 0;
			slotIndex = SetSlots(craftingRecipe.Materials, slotIndex);
			arrowParent.SetSiblingIndex(slotIndex);
			slotIndex = SetSlots(craftingRecipe.Results, slotIndex);

			for (int i = slotIndex; i < itemSlots.Length; i++)
			{
				itemSlots[i].transform.parent.gameObject.SetActive(false);
			}

			gameObject.SetActive(true);
		}
		else
		{
			gameObject.SetActive(false);
		}
	}

	private int SetSlots(IList<ItemAmount> itemAmountList, int slotIndex)
	{
		for (int i = 0; i < itemAmountList.Count; i++, slotIndex++)
		{
			ItemAmount itemAmount = itemAmountList[i];
			BaseItemSlot itemSlot = itemSlots[slotIndex];

			itemSlot.Item = itemAmount.Item;
			itemSlot.Amount = itemAmount.Amount;
			itemSlot.transform.parent.gameObject.SetActive(true);
		}
		return slotIndex;
	}
}
