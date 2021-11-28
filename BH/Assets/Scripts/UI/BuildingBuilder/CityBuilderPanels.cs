using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityBuilderPanels : MonoBehaviour
{
    public event Action<bool> OnToggleBuilderPanel = delegate { };

    public BuildingBuilderPanel BuildingBuilderPanel { get => buildingBuilderPanel; }

    [SerializeField] private BasicButton btnCloseBuilderPanel = null;
    [SerializeField] private BuildingBuilderPanel buildingBuilderPanel = null;

    private GameUiManager gameUiManager;

    public void Setup(LocalController levelController)
    {
        gameUiManager = levelController.GameUiManager;

        buildingBuilderPanel.Setup();
        btnCloseBuilderPanel.SetupListener(gameUiManager.ToggleBuildingBuilderPanel);
    }


    public void ToggleBuildingBuilderPanel()
    {
        bool isActive = !BuildingBuilderPanel.gameObject.activeSelf;
        BuildingBuilderPanel.gameObject.SetActive(isActive);
        btnCloseBuilderPanel.gameObject.SetActive(isActive);

        OnToggleBuilderPanel(isActive);
    }

    public bool CheckActivePanels()
    {
        return BuildingBuilderPanel.gameObject.activeSelf;
    }
}
