using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalManagers : MonoBehaviour
{
    public EquipmentManager EquipmentManager { get => equipmentManager; }
    public BuildingBuilderManager BuildingBuilder { get => buildingBuilder; }

    [SerializeField] private EquipmentManager equipmentManager = null;
    [SerializeField] private BuildingBuilderManager buildingBuilder = null;


    public void SetupManagers(LevelController levelController)
    {
        EquipmentManager.Setup(levelController);
        BuildingBuilder.Setup(levelController);
    }
}
