using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : ScriptableObject
{
    public string BuildingName { get => buildingName; }
    public Sprite Sprite { get => sprite; }
    public Vector2Int Size { get => size; }
    public EBuildingFunctionType FunctionType { get => functionType; }
    public EBuildingType BuildingType { get => buildingType; }


    [SerializeField] private string buildingName = "";
    [SerializeField] private Sprite sprite = null;
    [SerializeField] private Vector2Int size = Vector2Int.zero;
    [SerializeField] private EBuildingType buildingType = EBuildingType.None;
    [SerializeField] private EBuildingFunctionType functionType = EBuildingFunctionType.None;
}
