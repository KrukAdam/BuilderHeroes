﻿using System;
using UnityEngine;

public class EquipmentPanel : MonoBehaviour
{
	public event Action<BaseItemSlot> OnPointerEnterEvent;
	public event Action<BaseItemSlot> OnPointerExitEvent;
	public event Action<BaseItemSlot> OnRightClickEvent;
	public event Action<BaseItemSlot> OnBeginDragEvent;
	public event Action<BaseItemSlot> OnEndDragEvent;
	public event Action<BaseItemSlot> OnDragEvent;
	public event Action<BaseItemSlot> OnDropEvent;

	public ItemSlot AmmoSlot { get => ammoSlot; }
	public ItemSlot ToolSlot { get => toolSlot; }

	[SerializeField] private ItemSlot ammoSlot = null;
	[SerializeField] private ItemSlot toolSlot = null;
	[SerializeField] private EquipmentSlot[] equipmentSlots;
	//[SerializeField] private Transform equipmentSlotsParent;

	public void Setup()
    {
		SetupSlots();
    }

	public void SetupSlots()
    {
		for (int i = 0; i < equipmentSlots.Length; i++)
		{
			equipmentSlots[i].OnPointerEnterEvent += slot => OnPointerEnterEvent(slot);
			equipmentSlots[i].OnPointerExitEvent += slot => OnPointerExitEvent(slot);
			equipmentSlots[i].OnRightClickEvent += slot => OnRightClickEvent(slot);
			equipmentSlots[i].OnBeginDragEvent += slot => OnBeginDragEvent(slot);
			equipmentSlots[i].OnEndDragEvent += slot => OnEndDragEvent(slot);
			equipmentSlots[i].OnDragEvent += slot => OnDragEvent(slot);
			equipmentSlots[i].OnDropEvent += slot => OnDropEvent(slot);
		}
	}

	public bool AddItem(ItemEquippable item, out ItemEquippable previousItem)
	{
		for (int i = 0; i < equipmentSlots.Length; i++)
		{
			if (equipmentSlots[i].EquipmentType == item.EquipmentType)
			{
				previousItem = (ItemEquippable)equipmentSlots[i].Item;
				equipmentSlots[i].Item = item;
				equipmentSlots[i].Amount = 1;
				return true;
			}
		}
		previousItem = null;
		return false;
	}

	public bool RemoveItem(ItemEquippable item)
	{
		for (int i = 0; i < equipmentSlots.Length; i++)
		{
			if (equipmentSlots[i].Item == item)
			{
				equipmentSlots[i].Item = null;
				equipmentSlots[i].Amount = 0;
				return true;
			}
		}
		return false;
	}
}
