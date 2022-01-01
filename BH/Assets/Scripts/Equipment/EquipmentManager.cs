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

	private BaseItemSlot dragItemSlot;

	private TooltipsPanels tooltipsPanels;
	private InventoryPanel inventoryPanel;
    private EquipmentPanel equipmentPanel;
	private WeaponSkillsPanel weaponSkillsPanel;
	private PlayerCharacter playerCharacter;
	private ItemContainer openItemContainer;
	private DropItemManager dropItemManager;

	public void Setup(LocalController localController)
    {
		playerCharacter = localController.Player;
		inventoryPanel = localController.GameUiManager.CharacterPanels.InventoryPanel;
		equipmentPanel = localController.GameUiManager.CharacterPanels.EquipmentWeaponSkillsPanel.EquipmentPanel;
		weaponSkillsPanel = localController.GameUiManager.CharacterPanels.EquipmentWeaponSkillsPanel.WeaponSkillsPanel;
		tooltipsPanels = localController.GameUiManager.TooltipsPanels;
		dropItemManager = localController.LocalManagers.DropItemManager;

		SetupEvents();
    }

    public ItemTool GetItemTool(EItemToolType toolNeeded)
    {
        if (!equipmentPanel.ToolSlot.Item) return null;

        ItemTool tool = equipmentPanel.ToolSlot.Item as ItemTool;
        if (tool)
        {
            if (tool.ToolType == toolNeeded) return tool;
        }
        return null;
    }

	public void DamageTool()
    {
		if (!equipmentPanel.ToolSlot.Item) return;

		ItemTool tool = equipmentPanel.ToolSlot.Item as ItemTool;
		if (tool)
		{
			tool.Durability = tool.Durability - 1;  //TODO damage value = !race skill tools use
		}
		if(tool.Durability <= 0)
        {
			equipmentPanel.ToolSlot.Item = null;
			equipmentPanel.ToolSlot.Amount = 0;
		}
	}


    public void Equip(ItemEquippable item)
	{
		if (inventoryPanel.RemoveItem(item))
		{
			ItemEquippable previousItem;
			if (equipmentPanel.AddItem(item, out previousItem))
			{
				if (previousItem != null)
				{
					inventoryPanel.AddItem(previousItem);
					previousItem.Unequip(playerCharacter);
					weaponSkillsPanel.UnsetupWeaponSkills(previousItem);
				}
				item.Equip(playerCharacter);
				weaponSkillsPanel.SetupWeaponSkills(item);
			}
			else
			{
				inventoryPanel.AddItem(item);
			}
		}
		OnEquipmentChange();
	}

	public void Unequip(ItemEquippable item)
	{
		if (inventoryPanel.CanAddItem(item) && equipmentPanel.RemoveItem(item))
		{
			item.Unequip(playerCharacter);
			weaponSkillsPanel.UnsetupWeaponSkills(item);
			inventoryPanel.AddItem(item);
			OnEquipmentChange();
		}
	}

	public bool AddItemToInventory(Item item)
    {
		if (item != null)
		{
			bool addItem = inventoryPanel.AddItem(item);
			return addItem;
		}
		return false;
	}

	public void OpenItemContainer(ItemContainer itemContainer)
	{
		openItemContainer = itemContainer;

		inventoryPanel.OnRightClickEvent -= InventoryRightClick;
		inventoryPanel.OnRightClickEvent += TransferToItemContainer;

		itemContainer.OnRightClickEvent += TransferToInventory;

		itemContainer.OnPointerEnterEvent += tooltipsPanels.ShowItemTooltip;
		itemContainer.OnPointerExitEvent += tooltipsPanels.HideItemTooltip;
		itemContainer.OnBeginDragEvent += BeginDrag;
		itemContainer.OnEndDragEvent += EndDrag;
		itemContainer.OnDragEvent += Drag;
		itemContainer.OnDropEvent += Drop;
	}

	public void CloseItemContainer(ItemContainer itemContainer)
	{
		openItemContainer = null;

		inventoryPanel.OnRightClickEvent += InventoryRightClick;
		inventoryPanel.OnRightClickEvent -= TransferToItemContainer;

		itemContainer.OnRightClickEvent -= TransferToInventory;

		itemContainer.OnPointerEnterEvent -= tooltipsPanels.ShowItemTooltip;
		itemContainer.OnPointerExitEvent -= tooltipsPanels.HideItemTooltip;
		itemContainer.OnBeginDragEvent -= BeginDrag;
		itemContainer.OnEndDragEvent -= EndDrag;
		itemContainer.OnDragEvent -= Drag;
		itemContainer.OnDropEvent -= Drop;
	}

	private void SetupEvents()
    {
		inventoryPanel.OnRightClickEvent += InventoryRightClick;
		equipmentPanel.OnRightClickEvent += EquipmentPanelRightClick;
		// Begin Drag
		inventoryPanel.OnBeginDragEvent += BeginDrag;
		equipmentPanel.OnBeginDragEvent += BeginDrag;
		// End Drag
		inventoryPanel.OnEndDragEvent += EndDrag;
		equipmentPanel.OnEndDragEvent += EndDrag;
		// Drag
		inventoryPanel.OnDragEvent += Drag;
		equipmentPanel.OnDragEvent += Drag;
		// Drop
		inventoryPanel.OnDropEvent += Drop;
		equipmentPanel.OnDropEvent += Drop;
		dropItemArea.OnDropEvent += DropItemOutsideUI;
	}


	private void TransferToItemContainer(BaseItemSlot itemSlot)
	{
		Item item = itemSlot.Item;
		if (item != null && openItemContainer.CanAddItem(item))
		{
			inventoryPanel.RemoveItem(item);
			openItemContainer.AddItem(item);
		}
	}

	private void TransferToInventory(BaseItemSlot itemSlot)
	{
		Item item = itemSlot.Item;
		if (item != null && inventoryPanel.CanAddItem(item))
		{
			openItemContainer.RemoveItem(item);
			inventoryPanel.AddItem(item);
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
			dropItemManager.DropItemOnMap(itemSlot, playerCharacter.transform.position);
		}

		itemSlot.Item.Destroy();
		itemSlot.Item = null;
	}
}
