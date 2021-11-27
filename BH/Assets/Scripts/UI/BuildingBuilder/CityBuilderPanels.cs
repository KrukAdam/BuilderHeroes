using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityBuilderPanels : MonoBehaviour
{
    public BuildingBuilderPanel BuildingBuilderPanel { get => buildingBuilderPanel; }

    [SerializeField] private BuildingBuilderPanel buildingBuilderPanel = null;

    private GameUiManager gameUiManager;

    public void Setup(LocalController levelController)
    {
        gameUiManager = levelController.GameUiManager;

        buildingBuilderPanel.Setup();
    }

    public void ToggleBuildingBuilderPanel()
    {
        bool isActive = !BuildingBuilderPanel.gameObject.activeSelf;
        BuildingBuilderPanel.gameObject.SetActive(isActive);
    }

    public bool CheckActivePanels()
    {
        return BuildingBuilderPanel.gameObject.activeSelf;
    }
}
