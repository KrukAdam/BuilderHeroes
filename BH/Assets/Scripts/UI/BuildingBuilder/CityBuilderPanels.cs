using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CityBuilderPanels : MonoBehaviour
{
    public event Action<bool> OnToggleBuilderPanel = delegate { };

    public BuildingBuilderPanel BuildingBuilderPanel { get => buildingBuilderPanel; }
    public ConstructionPanel ConstructionPanel { get => constructionPanel; }


    [SerializeField] private BuildingBuilderPanel buildingBuilderPanel = null;
    [SerializeField] private ConstructionPanel constructionPanel = null;

    private GameUiManager gameUiManager;

    private void OnDestroy()
    {
        GameManager.Instance.InputManager.InputController.Player.HorizontalMove.performed -= CloseContructionPanelOnMove;
        GameManager.Instance.InputManager.InputController.Player.VerticalMove.performed -= CloseContructionPanelOnMove;
    }

    public void Setup(LocalController levelController)
    {
        gameUiManager = levelController.GameUiManager;

        buildingBuilderPanel.Setup(levelController.GameUiManager);
        constructionPanel.Setup(levelController.GameUiManager);

    }

    public void ToggleContructionPanel()
    {
        bool isActive = !constructionPanel.gameObject.activeSelf;
        constructionPanel.gameObject.SetActive(isActive);

        if (isActive)
        {
            GameManager.Instance.InputManager.InputController.Player.HorizontalMove.performed += CloseContructionPanelOnMove;
            GameManager.Instance.InputManager.InputController.Player.VerticalMove.performed += CloseContructionPanelOnMove;
        }
        else
        {
            GameManager.Instance.InputManager.InputController.Player.HorizontalMove.performed -= CloseContructionPanelOnMove;
            GameManager.Instance.InputManager.InputController.Player.VerticalMove.performed -= CloseContructionPanelOnMove;
        }
    }

    public void ToggleBuildingBuilderPanel()
    {
        bool isActive = !buildingBuilderPanel.gameObject.activeSelf;
        buildingBuilderPanel.gameObject.SetActive(isActive);

        OnToggleBuilderPanel(isActive);
    }

    public bool CheckActivePanels()
    {
        if (buildingBuilderPanel.gameObject.activeSelf) return true;
        if (constructionPanel.gameObject.activeSelf) return true;

        return false;
    }

    private void CloseContructionPanelOnMove(InputAction.CallbackContext context)
    {
        gameUiManager.ToggleContructionPanel();
    }
}
