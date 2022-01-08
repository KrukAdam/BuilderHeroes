using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPanels : MonoBehaviour
{
    public InventoryPanel InventoryPanel => inventoryPanel;
    public StatsPanel StatsPanel => statsPanel;
    public EquipmentWeaponSkillsPanel EquipmentWeaponSkillsPanel => equipmentWeaponSkillsPanel;

    [SerializeField] private InventoryPanel inventoryPanel = null;
    [SerializeField] private StatsPanel statsPanel = null;
    [SerializeField] private EquipmentWeaponSkillsPanel equipmentWeaponSkillsPanel = null;

    public void Setup(LocalController levelController)
    {
        inventoryPanel.Setup(this);
        StatsPanel.SetupPanel(levelController);
        EquipmentWeaponSkillsPanel.Setup(levelController);
    }

    public void ToggleCharacterPanel()
    {
        bool isActive = !EquipmentWeaponSkillsPanel.gameObject.activeSelf;
        EquipmentWeaponSkillsPanel.gameObject.SetActive(isActive);
        StatsPanel.gameObject.SetActive(isActive);
    }

    public void ToggleCharacterInventoryPanels()
    {
        bool isActive = !InventoryPanel.gameObject.activeSelf;
        EquipmentWeaponSkillsPanel.gameObject.SetActive(isActive);
        StatsPanel.gameObject.SetActive(isActive);
        InventoryPanel.gameObject.SetActive(isActive);
    }

    public bool CheckActivePanels()
    {
        return EquipmentWeaponSkillsPanel.gameObject.activeSelf;
    }
}
