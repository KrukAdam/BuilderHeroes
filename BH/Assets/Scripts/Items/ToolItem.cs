using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Stats))]
[CreateAssetMenu(menuName = "Items/Tool Item")]
public class ToolItem : EquippableItem
{
    public EItemToolType ToolType { get => toolType; }

    [Space]
    [SerializeField] private EItemToolType toolType = EItemToolType.None;
}
