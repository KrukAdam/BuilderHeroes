
public class EquipmentSlot : ItemSlot
{
	public EEquipmentType EquipmentType;

	public override bool CanReceiveItem(Item item)
	{
		if (item == null)
			return true;

		ItemEquippable equippableItem = item as ItemEquippable;
		return equippableItem != null && equippableItem.EquipmentType == EquipmentType;
	}
}
