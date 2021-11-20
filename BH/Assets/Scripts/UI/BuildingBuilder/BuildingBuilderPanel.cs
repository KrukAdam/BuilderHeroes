using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBuilderPanel : MonoBehaviour
{
    [SerializeField] private BuildingButton buildingButtonPrefab = null;

    private BuildingsData buildingsData;

    public void Setup()
    {

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
            button = Instantiate(buildingButtonPrefab, transform);
            button.Setup(building, buildingBuilderManager);
            button.ClickOnBuildingButton += buildingBuilderManager.SelectedBuilding;
        }
    }
}
