using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentWeaponSkillsPanel : MonoBehaviour
{
    public EquipmentPanel EquipmentPanel => equipmentPanel;
    public WeaponSkillsPanel WeaponSkillsPanel => weaponSkillsPanel;

    [SerializeField] private EquipmentPanel equipmentPanel = null;
    [SerializeField] private WeaponSkillsPanel weaponSkillsPanel = null;

    public void Setup(LevelController levelController)
    {
        EquipmentPanel.Setup();
        WeaponSkillsPanel.Setup(levelController.Player.PlayerSkillsController);
    }
}
