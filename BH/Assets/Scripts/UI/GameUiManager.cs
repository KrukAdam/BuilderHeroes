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
    public CityBuilderPanels CityBuilderPanels { get => cityBuilderPanels; }

    [SerializeField] private TooltipsPanels tooltipsPanels = null;
    [SerializeField] private CraftingsPanels craftingsPanels = null;
    [SerializeField] private CharacterPanels characterPanels = null;
    [SerializeField] private CityBuilderPanels cityBuilderPanels = null;

    public void Setup(LocalController localController)
    {
        craftingsPanels.Setup(localController);
        characterPanels.SetupPanel(localController);
        cityBuilderPanels.Setup(localController);

        SetEvents();
        TogglePanels();

        GameManager.Instance.InputManager.InputController.Player.CharacterAndInventory.performed += ctx => ToggleCharacterPanel();
        GameManager.Instance.InputManager.InputController.Player.BuildingBuilderPanel.performed += ctx => ToggleBuildingBuilderPanel();
    }

    public void OpenBuildingPanel(Building building, Construction construction)
    {

        switch (building.FunctionType)
        {
            case EBuildingFunctionType.None:
                Debug.LogWarning("WORNG building function");
                break;
            case EBuildingFunctionType.Crafting:
                CraftingsPanels.OnOpenCraftingPanel(building, construction);
                ToggleCraftingPanel();
                break;
            default:
                break;
        }
    }

    public void ToggleContructionPanel()
    {
        cityBuilderPanels.ToggleContructionPanel();
        OnTogglePanels(CheckForActivePanels());
    }

    public void ToggleCharacterPanel()
    {
        CharacterPanels.ToggleCharacterInventoryPanels();
        OnTogglePanels(CheckForActivePanels());
    }

    public void ToggleBuildingBuilderPanel()
    {
        CityBuilderPanels.ToggleBuildingBuilderPanel();
        OnTogglePanels(CheckForActivePanels());
    }

    public void ToggleCraftingPanel()
    {
        CraftingsPanels.ToggleBuildingBuilderPanel();
        OnTogglePanels(CheckForActivePanels());
    }

    private void TogglePanels()
    {
        ToggleCharacterPanel();
        ToggleBuildingBuilderPanel();
        ToggleCraftingPanel();
        ToggleContructionPanel();
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

    private bool CheckForActivePanels()
    {
        if (CharacterPanels.CheckActivePanels()) return true;
        if (CityBuilderPanels.CheckActivePanels()) return true;
        if (CraftingsPanels.CheckActivePanels()) return true;

        return false;
    }
}
