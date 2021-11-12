using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Stats))]
[CreateAssetMenu(menuName = "Items/Ammo Item")]
public class ItemAmmo : ItemEquippable
{
    public EItemAmmoType AmmoType { get => ammoType; }

    [Space]
    [SerializeField] private EItemAmmoType ammoType = EItemAmmoType.None;
}
