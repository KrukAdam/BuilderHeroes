using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Stats))]
[CreateAssetMenu(menuName = "Items/Ammo Item")]
public class AmmoItem : EquippableItem
{
    public EAmmoType AmmoType { get => ammoType; }

    [Space]
    [SerializeField] private EAmmoType ammoType = EAmmoType.None;
}
