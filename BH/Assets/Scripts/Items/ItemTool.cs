using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Stats))]
[CreateAssetMenu(menuName = "Items/Tool Item")]
public class ItemTool : ItemEquippable
{
    public EItemToolType ToolType { get => toolType; }
    public float DamageRawMaterial { get => damageRawMaterial; }
    public int Durability { get => durability; set => durability = value; }


    [Space]
    [SerializeField] private EItemToolType toolType = EItemToolType.None;
    [SerializeField] private float damageRawMaterial = 0;
    [SerializeField] private int durability = 1;
}
