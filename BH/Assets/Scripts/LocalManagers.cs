using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalManagers : MonoBehaviour
{
    public EquipmentManager EquipmentManager { get => equipmentManager; }
    public BuildingBuilderManager BuildingBuilder { get => buildingBuilder; }
    public ItemDiscoveryManager ItemDiscoveryManager { get => itemDiscoveryManager; }


    [SerializeField] private EquipmentManager equipmentManager = null;
    [SerializeField] private BuildingBuilderManager buildingBuilder = null;
    [SerializeField] private ItemDiscoveryManager itemDiscoveryManager = null;

    public void SetupManagers(LocalController localController)
    {
        EquipmentManager.Setup(localController);
        BuildingBuilder.Setup(localController);
        ItemDiscoveryManager.Setup(localController);
    }
}
