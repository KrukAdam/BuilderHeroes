
public interface IItemContainer
{
	public bool CanAddItem(Item item, int amount = 1);
	public bool AddItem(Item item);

	public Item RemoveItem(string itemID);
	public bool RemoveItem(Item item);

	public void Clear();

	public int ItemCount(string itemID);
	public void SetupSlots();
}
