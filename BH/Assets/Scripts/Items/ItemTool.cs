using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Stats))]
[CreateAssetMenu(menuName = "Items/Tool Item")]
public class ItemTool : ItemEquippable
{
    public EItemToolType ToolType { get => toolType; }

    [Space]
    [SerializeField] private EItemToolType toolType = EItemToolType.None;
}
