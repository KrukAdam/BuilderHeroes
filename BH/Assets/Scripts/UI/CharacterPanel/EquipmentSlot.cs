
public class EquipmentSlot : ItemSlot
{
	public EEquipmentType EquipmentType;

	public override bool CanReceiveItem(Item item)
	{
		if (item == null)
			return true;

		EquippableItem equippableItem = item as EquippableItem;
		return equippableItem != null && equippableItem.EquipmentType == EquipmentType;
	}
}
