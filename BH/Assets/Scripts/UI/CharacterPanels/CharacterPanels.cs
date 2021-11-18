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

    private GameUiManager gameUiManager;

    public void Setup(GameUiManager gameUiManager, LevelController levelController)
    {
        this.gameUiManager = gameUiManager;

        inventoryPanel.Setup(this);
        SetupCharacterPanels(levelController);

    }

    public void ToggleCharacterPanel()
    {
        bool isActive = !EquipmentWeaponSkillsPanel.gameObject.activeSelf;
        EquipmentWeaponSkillsPanel.gameObject.SetActive(isActive);
        StatsPanel.gameObject.SetActive(isActive);
    }


    private void SetupCharacterPanels(LevelController levelController)
    {
        SetWeaponSkillsPanel(levelController.Player.PlayerSkillsController);
        SetStatsOnStatsPanel(levelController.Player.Stats);
    }

    private void SetWeaponSkillsPanel(PlayerSkillsController playerSkillsController)
    {
        EquipmentWeaponSkillsPanel.WeaponSkillsPanel.Setup(playerSkillsController);
    }

    private void SetStatsOnStatsPanel(Stats stats)
    {
        StatsPanel.SetStats(stats);
        StatsPanel.UpdateStatValues();
    }
}
