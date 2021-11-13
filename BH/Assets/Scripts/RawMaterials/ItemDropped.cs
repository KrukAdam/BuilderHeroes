using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ItemDropped
{
    public Item ItemDroppedPrefab;
    [Range(0.001f, 100f)] public float DropChance = 1;
    [Range(1, 100)] public int DropMax = 1;
}
