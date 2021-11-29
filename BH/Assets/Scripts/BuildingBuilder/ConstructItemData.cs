using System;

[Serializable]
public class ConstructItemData
{
    public Item Item { get => item; }
    public int ItemNeeded { get => itemNeeded; }
    public int ItemHas { get => itemHas; set => itemHas = value; }

    private Item item;
    private int itemNeeded;
    private int itemHas;

    public ConstructItemData(ItemAmount itemAmount)
    {
        item = itemAmount.Item;
        itemNeeded = itemAmount.Amount;
    }
}
