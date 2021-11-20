using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUiManager : MonoBehaviour
{
    public event Action<bool> OnTogglePanels = delegate { };

    public CharacterPanels CharacterPanels { get => characterPanels; }
    public TooltipsPanels TooltipsPanels { get => tooltipsPanels; }
    public CraftingsPanels CraftingsPanels { get => craftingsPanels; }

    [SerializeField] private TooltipsPanels tooltipsPanels = null;
    [SerializeField] private CraftingsPanels craftingsPanels = null;
    [SerializeField] private CharacterPanels characterPanels = null;

    private LevelController levelController;

    public void Setup(LevelController levelController)
    {
        this.levelController = levelController;

        craftingsPanels.Setup(this);
        characterPanels.SetupPanel(levelController);

        ToggleCharacterPanel();
        SetEvents();

        GameManager.Instance.InputManager.InputController.Player.CharacterAndInventory.performed += ctx => ToggleCharacterPanel();
    }

    private void SetEvents()
    {
        // Pointer Enter
        CharacterPanels.InventoryPanel.OnPointerEnterEvent += TooltipsPanels.ShowItemTooltip;
        CharacterPanels.EquipmentWeaponSkillsPanel.EquipmentPanel.OnPointerEnterEvent += TooltipsPanels.ShowItemTooltip;
        // Pointer Exit
        CharacterPanels.InventoryPanel.OnPointerExitEvent += TooltipsPanels.HideItemTooltip;
        CharacterPanels.EquipmentWeaponSkillsPanel.EquipmentPanel.OnPointerExitEvent += TooltipsPanels.HideItemTooltip;
    }

    private void ToggleCharacterPanel()
    {
        CharacterPanels.ToggleCharacterInventoryPanels();
        OnTogglePanels(!CheckForActivePanels());
    }

    private bool CheckForActivePanels()
    {
        if (CharacterPanels.CheckActivePanels()) return true;

        //TODo check crafting panels

        return false;
    }
}
