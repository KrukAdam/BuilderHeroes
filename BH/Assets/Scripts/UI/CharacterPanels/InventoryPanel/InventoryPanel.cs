using UnityEngine;

public class InventoryPanel : ItemContainer
{
	[SerializeField] private BasicButton openEqAndStatButton = null;
	[SerializeField] private Item[] startingItems;
	//[SerializeField] private Transform itemsParent;

	public void Setup(CharacterPanels characterPanels)
    {
		openEqAndStatButton.SetupListener(characterPanels.ToggleCharacterPanel);

		SetupSlots();
		SetStartingItems();
	}

	private void SetStartingItems()
	{
		Clear();
		foreach (Item item in startingItems)
		{
			AddItem(item.GetCopy());
		}
	}
}
