using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buildings/Building")]
public class Building : ScriptableObject
{
    public string BuildingName { get => buildingName; }
    public Sprite Sprite { get => sprite; }
    public Vector2Int Size { get => size; }

    [SerializeField] private string buildingName = "";
    [SerializeField] private Sprite sprite = null;
    [SerializeField] private Vector2Int size = Vector2Int.zero;
}
