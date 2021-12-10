using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemManager : MonoBehaviour
{
	[SerializeField] private Transform parentItemDrop = null;
	[SerializeField] private ItemOnMap itemOnMapPrefab = null;


	public void Setup(LocalController localController)
    {
		
    }

	public void DropItemOnMap(BaseItemSlot slot, Vector3 position)
	{
		ItemOnMap itemOnMap = Instantiate(itemOnMapPrefab, parentItemDrop);
		itemOnMap.transform.position = position;
		itemOnMap.Setup(slot);
	}

	public void DropItemOnMap(Item item, Vector3 position)
	{
		ItemOnMap itemOnMap = Instantiate(itemOnMapPrefab, parentItemDrop);
		itemOnMap.transform.position = position;
		itemOnMap.Setup(item,1);
	}

	public void DropItemOnMap(Item item, int amount, Vector3 position)
	{
		ItemOnMap itemOnMap = Instantiate(itemOnMapPrefab, parentItemDrop);
		itemOnMap.transform.position = position;
		itemOnMap.Setup(item, amount);
	}
}
