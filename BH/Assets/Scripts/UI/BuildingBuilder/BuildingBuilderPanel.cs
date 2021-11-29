using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBuilderPanel : MonoBehaviour
{
    [SerializeField] private BasicButton btnCloseBuilderPanel = null;
    [SerializeField] private BuildingButton buildingButtonPrefab = null;
    [SerializeField] private Transform parentBuildingsButtons = null;

    private BuildingsData buildingsData;

    public void Setup(GameUiManager gameUiManager)
    {
        //TODO pooling object
        btnCloseBuilderPanel.SetupListener(gameUiManager.ToggleBuildingBuilderPanel);
    }

    public void SetupBuildingsDataPanel(BuildingsData buildingsData, BuildingBuilderManager buildingBuilderManager)
    {
        this.buildingsData = buildingsData;
        SetupBuildingsButtons(buildingBuilderManager);
    }

    private void SetupBuildingsButtons(BuildingBuilderManager buildingBuilderManager)
    {
        foreach (var building in buildingsData.Buildings)
        {
            BuildingButton button;
            button = Instantiate(buildingButtonPrefab, parentBuildingsButtons);
            button.Setup(building, buildingBuilderManager);
            button.ClickOnBuildingButton += buildingBuilderManager.SelectedBuilding;
        }
    }
}
