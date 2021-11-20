using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class EquipmentManager : MonoBehaviour
{
	public event Action OnEquipmentChange = delegate { };

    [SerializeField] private DropItemArea dropItemArea = null;
	[SerializeField] private Image draggableItem = null;
	[SerializeField] private QuestionDialog reallyDropItemDialog = null;
	[SerializeField] private ItemOnMap itemOnMapPrefab = null;
	[SerializeField] private Transform itemsDropParent = null;
	[SerializeField] private WeaponSkillsPanel weaponSkillsPanel = null;

	private BaseItemSlot dragItemSlot;

	private GameUiManager gameUiManager;
	private InventoryPanel inventory;
    private EquipmentPanel equipmentPanel;
	private PlayerCharacter playerCharacter;
	private ItemContainer openItemContainer;

	public void Setup(GameUiManager gameUiManager, PlayerCharacter playerCharacter)
    {
		this.gameUiManager = gameUiManager;
		this.playerCharacter = playerCharacter;

		inventory = gameUiManager.Inventory;
		equipmentPanel = gameUiManager.EquipmentPanel;

		weaponSkillsPanel.Setup(playerCharacter.PlayerSkillsController);

        inventory.OnRightClickEvent += InventoryRightClick;
        equipmentPanel.OnRightClickEvent += EquipmentPanelRightClick;
        // Begin Drag
        inventory.OnBeginDragEvent += BeginDrag;
        equipmentPanel.OnBeginDragEvent += BeginDrag;
        // End Drag
        inventory.OnEndDragEvent += EndDrag;
        equipmentPanel.OnEndDragEvent += EndDrag;
        // Drag
        inventory.OnDragEvent += Drag;
        equipmentPanel.OnDragEvent += Drag;
        // Drop
        inventory.OnDropEvent += Drop;
        equipmentPanel.OnDropEvent += Drop;
        dropItemArea.OnDropEvent += DropItemOutsideUI;
    }

    public ItemTool GetItemTool(EItemToolType toolNeeded)
    {
        if (!gameUiManager.ToolSlot.Item) return null;

        ItemTool tool = gameUiManager.ToolSlot.Item as ItemTool;
        if (tool)
        {
            if (tool.ToolType == toolNeeded) return tool;
        }
        return null;
    }


    public void Equip(ItemEquippable item)
	{
		if (inventory.RemoveItem(item))
		{
			ItemEquippable previousItem;
			if (equipmentPanel.AddItem(item, out previousItem))
			{
				if (previousItem != null)
				{
					inventory.AddItem(previousItem);
					previousItem.Unequip(playerCharacter);
					weaponSkillsPanel.UnsetupWeaponSkills(previousItem);
				}
				item.Equip(playerCharacter);
				weaponSkillsPanel.SetupWeaponSkills(item);
			}
			else
			{
				inventory.AddItem(item);
			}
		}
		OnEquipmentChange();
	}

	public void Unequip(ItemEquippable item)
	{
		if (inventory.CanAddItem(item) && equipmentPanel.RemoveItem(item))
		{
			item.Unequip(playerCharacter);
			weaponSkillsPanel.UnsetupWeaponSkills(item);
			inventory.AddItem(item);
			OnEquipmentChange();
		}
	}

	public bool AddItemToInventory(Item item)
    {
		if (item != null)
		{
			bool addItem = inventory.AddItem(item);
			return addItem;
		}
		return false;
	}

	public void OpenItemContainer(ItemContainer itemContainer)
	{
		openItemContainer = itemContainer;

		inventory.OnRightClickEvent -= InventoryRightClick;
		inventory.OnRightClickEvent += TransferToItemContainer;

		itemContainer.OnRightClickEvent += TransferToInventory;

		itemContainer.OnPointerEnterEvent += gameUiManager.ShowTooltip;
		itemContainer.OnPointerExitEvent += gameUiManager.HideTooltip;
		itemContainer.OnBeginDragEvent += BeginDrag;
		itemContainer.OnEndDragEvent += EndDrag;
		itemContainer.OnDragEvent += Drag;
		itemContainer.OnDropEvent += Drop;
	}

	public void CloseItemContainer(ItemContainer itemContainer)
	{
		openItemContainer = null;

		inventory.OnRightClickEvent += InventoryRightClick;
		inventory.OnRightClickEvent -= TransferToItemContainer;

		itemContainer.OnRightClickEvent -= TransferToInventory;

		itemContainer.OnPointerEnterEvent -= gameUiManager.ShowTooltip;
		itemContainer.OnPointerExitEvent -= gameUiManager.HideTooltip;
		itemContainer.OnBeginDragEvent -= BeginDrag;
		itemContainer.OnEndDragEvent -= EndDrag;
		itemContainer.OnDragEvent -= Drag;
		itemContainer.OnDropEvent -= Drop;
	}


	private void TransferToItemContainer(BaseItemSlot itemSlot)
	{
		Item item = itemSlot.Item;
		if (item != null && openItemContainer.CanAddItem(item))
		{
			inventory.RemoveItem(item);
			openItemContainer.AddItem(item);
		}
	}

	private void TransferToInventory(BaseItemSlot itemSlot)
	{
		Item item = itemSlot.Item;
		if (item != null && inventory.CanAddItem(item))
		{
			openItemContainer.RemoveItem(item);
			inventory.AddItem(item);
		}
	}

	private void InventoryRightClick(BaseItemSlot itemSlot)
	{
		if (itemSlot.Item is ItemEquippable)
		{
			Equip((ItemEquippable)itemSlot.Item);
		}
		else if (itemSlot.Item is ItemUsable)
		{
			ItemUsable usableItem = (ItemUsable)itemSlot.Item;
			usableItem.Use(playerCharacter);

			if (usableItem.IsConsumable)
			{
				itemSlot.Amount--;
				usableItem.Destroy();
			}
		}
	}

	private void EquipmentPanelRightClick(BaseItemSlot itemSlot)
	{
		if (itemSlot.Item is ItemEquippable)
		{
			Unequip((ItemEquippable)itemSlot.Item);
		}
	}

	private void BeginDrag(BaseItemSlot itemSlot)
	{
		if (itemSlot.Item != null)
		{
			dragItemSlot = itemSlot;
			draggableItem.sprite = itemSlot.Item.Icon;
			draggableItem.transform.position = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
			draggableItem.gameObject.SetActive(true);
		}
	}

	private void Drag(BaseItemSlot itemSlot)
	{
		draggableItem.transform.position = Mouse.current.position.ReadValue();
	}

	private void EndDrag(BaseItemSlot itemSlot)
	{
		dragItemSlot = null;
		draggableItem.gameObject.SetActive(false);
	}

	private void Drop(BaseItemSlot dropItemSlot)
	{
		if (dragItemSlot == null) return;

		if (dropItemSlot.CanAddStack(dragItemSlot.Item))
		{
			AddStacks(dropItemSlot);
		}
		else if (dropItemSlot.CanReceiveItem(dragItemSlot.Item) && dragItemSlot.CanReceiveItem(dropItemSlot.Item))
		{
			SwapItems(dropItemSlot);
		}
	}

	private void DropItemOutsideUI()
	{
		if (dragItemSlot == null) return;

		reallyDropItemDialog.Show();
		BaseItemSlot slot = dragItemSlot;
		reallyDropItemDialog.OnYesEvent += () => DestroyItemInSlot(slot, true);
	}

	private void DropItemOnMap(BaseItemSlot slot)
    {
		GameObject itemOnMapGO = Instantiate(itemOnMapPrefab.gameObject, itemsDropParent);
		itemOnMapGO.transform.position = playerCharacter.transform.position;
		itemOnMapGO.GetComponent<ItemOnMap>().Setup(slot);
    }

	private void AddStacks(BaseItemSlot dropItemSlot)
	{
		int numAddableStacks = dropItemSlot.Item.MaximumStacks - dropItemSlot.Amount;
		int stacksToAdd = Mathf.Min(numAddableStacks, dragItemSlot.Amount);

		dropItemSlot.Amount += stacksToAdd;
		dragItemSlot.Amount -= stacksToAdd;
	}

	private void SwapItems(BaseItemSlot dropItemSlot)
	{
		ItemEquippable dragEquipItem = dragItemSlot.Item as ItemEquippable;
		ItemEquippable dropEquipItem = dropItemSlot.Item as ItemEquippable;

		if (dropItemSlot is EquipmentSlot)
		{
			if (dragEquipItem != null)
			{
				dragEquipItem.Equip(playerCharacter);
				weaponSkillsPanel.SetupWeaponSkills(dragEquipItem);
			}

			if (dropEquipItem != null) 
			{
				dropEquipItem.Unequip(playerCharacter);
				weaponSkillsPanel.UnsetupWeaponSkills(dropEquipItem);
			}
		}
		if (dragItemSlot is EquipmentSlot)
		{
			if (dragEquipItem != null)
			{
				dragEquipItem.Unequip(playerCharacter);
				weaponSkillsPanel.UnsetupWeaponSkills(dragEquipItem);
			}
			if (dropEquipItem != null)
			{
				dropEquipItem.Equip(playerCharacter);
				weaponSkillsPanel.SetupWeaponSkills(dropEquipItem);
			}
		}
		OnEquipmentChange();

		Item draggedItem = dragItemSlot.Item;
		int draggedItemAmount = dragItemSlot.Amount;

		dragItemSlot.Item = dropItemSlot.Item;
		dragItemSlot.Amount = dropItemSlot.Amount;

		dropItemSlot.Item = draggedItem;
		dropItemSlot.Amount = draggedItemAmount;
	}


	private void DestroyItemInSlot(BaseItemSlot itemSlot, bool dropOnMap)
	{
		// If the item is equiped, unequip first
		if (itemSlot is EquipmentSlot)
		{
			ItemEquippable equippableItem = (ItemEquippable)itemSlot.Item;
			equippableItem.Unequip(playerCharacter);
			weaponSkillsPanel.UnsetupWeaponSkills(equippableItem);
		}
        //Drop item
        if (dropOnMap)
        {
			DropItemOnMap(itemSlot);
		}

		itemSlot.Item.Destroy();
		itemSlot.Item = null;
	}
}
