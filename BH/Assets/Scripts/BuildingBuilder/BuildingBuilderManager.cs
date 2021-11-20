using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBuilderManager : MonoBehaviour
{
    [SerializeField] private BuildingsData[] buildingsDatas = null;

    private LocalManagers localManagers;
    private Dictionary<ERaceType, BuildingsData> buildingsDictionary = new Dictionary<ERaceType, BuildingsData>();
    private CityBuilderPanels cityBuilderPanels;
    private Transform interactionPointer;
    private Building selectedBuilding;

    public void Setup(LevelController levelController)
    {
        cityBuilderPanels = levelController.GameUiManager.CityBuilderPanels;
        interactionPointer = levelController.Player.PlayerActionController.InteractionPointer;

        SetupBuildingsDictionary();
        cityBuilderPanels.BuildingBuilderPanel.SetupBuildingsDataPanel(buildingsDictionary[levelController.Player.RaceType], this);
    }

    public void SelectedBuilding(Building building)
    {
        if (selectedBuilding == building) return;

        selectedBuilding = building;
    }

    private void SetupBuildingsDictionary()
    {
        buildingsDictionary.Clear();
        foreach (var buildingsData in buildingsDatas)
        {
            if(buildingsData.RaceType != ERaceType.None)
            buildingsDictionary.Add(buildingsData.RaceType, buildingsData);
        }
    }
}
