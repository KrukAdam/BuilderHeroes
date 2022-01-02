using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Usable Item")]
public class ItemUsable : Item
{
	public List<ItemStatBuffEffect> Effects { get => effects; }
	public bool IsConsumable;

    [SerializeField] private List<ItemStatBuffEffect> effects; 

	public virtual void Use(PlayerCharacter character)
	{
		foreach (ItemStatBuffEffect effect in effects)
		{
			effect.ExecuteEffect(this, character);
		}
	}

	public override string GetDescription()
	{
		sb.Length = 0;
		foreach (ItemStatBuffEffect effect in effects)
		{
			sb.AppendLine(effect.GetDescription());
		}
		return sb.ToString();
	}
}
