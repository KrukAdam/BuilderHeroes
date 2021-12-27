using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CraftingsPanels : MonoBehaviour
{
    [SerializeField] private CraftingPanel craftingPanel = null;
    [SerializeField] private BasicButton buttonClose = null;
    [SerializeField] private BasicButton buttonDestroyBuilding = null;

    private GameUiManager gameUiManager;
    private Construction construction;

    public void Setup(LocalController localController)
    {
        gameUiManager = localController.GameUiManager;

        craftingPanel.Setup(localController);
        craftingPanel.OnPointerEnterEvent += gameUiManager.TooltipsPanels.ShowItemTooltip;
        craftingPanel.OnPointerExitEvent += gameUiManager.TooltipsPanels.HideItemTooltip;

        buttonClose.SetupListener(gameUiManager.ToggleCraftingPanel);
        buttonDestroyBuilding.SetupListener(DestroyBuilding);
    }

    public void OnOpenCraftingPanel(Building building, Construction construction)
    {
        this.construction = construction;
        craftingPanel.OnOpen(building);
    }

    public void ToggleBuildingBuilderPanel()
    {
        bool isActive = !craftingPanel.gameObject.activeSelf;
        craftingPanel.gameObject.SetActive(isActive);
        buttonClose.gameObject.SetActive(isActive);
        buttonDestroyBuilding.gameObject.SetActive(isActive);

        if (isActive)
        {
            GameManager.Instance.InputManager.InputController.Player.HorizontalMove.performed += ClosePanelsWhenMove;
            GameManager.Instance.InputManager.InputController.Player.VerticalMove.performed += ClosePanelsWhenMove;
        }
        else
        {
            GameManager.Instance.InputManager.InputController.Player.HorizontalMove.performed -= ClosePanelsWhenMove;
            GameManager.Instance.InputManager.InputController.Player.VerticalMove.performed -= ClosePanelsWhenMove;
        }
    }

    public bool CheckActivePanels()
    {
        return craftingPanel.gameObject.activeSelf;
    }

    private void ClosePanelsWhenMove(InputAction.CallbackContext context)
    {
        gameUiManager.ToggleCraftingPanel();
    }

    private void DestroyBuilding()
    {
        ToggleBuildingBuilderPanel();
        construction.Destroy();
    }
}
