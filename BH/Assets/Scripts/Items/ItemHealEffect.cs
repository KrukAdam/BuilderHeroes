using UnityEngine;

//[CreateAssetMenu(menuName = "Item Effects/Heal")]
public class ItemHealEffect 
{
	public int HealAmount;

	public void ExecuteEffect(ItemUsable usableItem, PlayerCharacter character)
	{
		// character.Health += HealAmount; 

	}

	public string GetDescription()
	{
		return "Heals for " + HealAmount + " health.";
	}
}
