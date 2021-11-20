using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalManagers : MonoBehaviour
{
    public EquipmentManager EquipmentManager { get => equipmentManager; }

    [SerializeField]
    private EquipmentManager equipmentManager = null;

    public void SetupManagers(LevelController levelController)
    {
        equipmentManager.Setup(levelController);
    }
}
