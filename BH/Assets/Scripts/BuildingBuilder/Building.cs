using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class Building : ScriptableObject
{
    public LocalizedString BuildingName { get => buildingName; }
    public Sprite Sprite { get => sprite; }
    public Sprite ContructionSprite { get => contructionSprite; }
    public Vector2Int Size { get => size; }
    public EBuildingFunctionType FunctionType { get => functionType; }
    public EBuildingType BuildingType { get => buildingType; }
    public List<ItemAmount> ItemsToConstruction { get => itemsToConstruction; }
    public StatsData StatsData { get => stats; }


    [SerializeField] private LocalizedString buildingName = null;
    [SerializeField] private Sprite sprite = null;
    [SerializeField] private Sprite contructionSprite = null;
    [SerializeField] private Vector2Int size = Vector2Int.zero;
    [SerializeField] private EBuildingType buildingType = EBuildingType.None;
    [SerializeField] private EBuildingFunctionType functionType = EBuildingFunctionType.None;
    [SerializeField] private List<ItemAmount> itemsToConstruction = new List<ItemAmount>();
    [SerializeField] private StatsData stats = null;
}
