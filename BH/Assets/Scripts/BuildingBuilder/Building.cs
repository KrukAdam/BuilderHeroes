using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buildings/Building")]
public class Building : ScriptableObject
{
    public string BuildingName { get => buildingName; }

    [SerializeField] private string buildingName = "";
}
