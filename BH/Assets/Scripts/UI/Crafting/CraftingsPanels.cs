using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingsPanels : MonoBehaviour
{
    [SerializeField] private CraftingPanel craftingPanel = null;
    [SerializeField] private BasicButton buttonClose = null;

    private GameUiManager gameUiManager;

    public void Setup(LocalController localController)
    {
        gameUiManager = localController.GameUiManager;

        craftingPanel.Setup(localController.GameUiManager.CharacterPanels.InventoryPanel);
        craftingPanel.OnPointerEnterEvent += gameUiManager.TooltipsPanels.ShowItemTooltip;
        craftingPanel.OnPointerExitEvent += gameUiManager.TooltipsPanels.HideItemTooltip;

        buttonClose.SetupListener(ToggleCraftingPanel);
    }

    public void OnOpenCraftingPanel(Building building)
    {
        craftingPanel.OnOpen(building);
    }

    public void ToggleCraftingPanel()
    {
        bool isActive = !craftingPanel.gameObject.activeSelf;
        craftingPanel.gameObject.SetActive(isActive);
        buttonClose.gameObject.SetActive(isActive);
    }

    public bool CheckActivePanels()
    {
        return craftingPanel.gameObject.activeSelf;
    }
}
