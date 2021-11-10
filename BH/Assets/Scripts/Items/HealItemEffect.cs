using UnityEngine;

//[CreateAssetMenu(menuName = "Item Effects/Heal")]
public class HealItemEffect 
{
	public int HealAmount;

	public void ExecuteEffect(UsableItem usableItem, PlayerCharacter character)
	{
		// character.Health += HealAmount; 

	}

	public string GetDescription()
	{
		return "Heals for " + HealAmount + " health.";
	}
}
