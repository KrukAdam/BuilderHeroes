using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Buildings/Buildings Data")]
public class BuildingsData : ScriptableObject
{
    public List<Building> Buildings { get => buildings; }
    public ERaceType RaceType { get => raceType; }

    [SerializeField] private ERaceType raceType = ERaceType.None;
    [SerializeField] private List<Building> buildings = new List<Building>();
}
