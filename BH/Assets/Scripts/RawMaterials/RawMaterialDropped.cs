using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class RawMaterialDropped
{
    public RawMaterialPartsOnMap PrefabRawMaterialToPickUp;
    [Range(0.001f, 100f)] public float DropChance = 1;
    [Range(1, 10)] public int DropMax = 1;
}
