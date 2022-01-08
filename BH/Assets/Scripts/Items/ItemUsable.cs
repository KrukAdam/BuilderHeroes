using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Items/Usable Item")]
public class ItemUsable : Item
{
	public List<AuraData> Effects { get => effects; }
	public bool IsConsumable;

    [SerializeField] private List<AuraData> effects; 

	public virtual void Use(PlayerCharacter character)
	{
		foreach (AuraData effect in effects)
		{
			effect.ExecuteEffect(this, character);
		}
	}
}
