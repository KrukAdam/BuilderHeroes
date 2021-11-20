using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBuilderManager : MonoBehaviour
{
    [SerializeField] private BuildingsData[] buildingsDatas = null;

    private LocalManagers localManagers;
    private Dictionary<ERaceType, BuildingsData> buildingsDictionary = new Dictionary<ERaceType, BuildingsData>();
    private CityBuilderPanels cityBuilderPanels;

    public void Setup(LevelController levelController)
    {
        cityBuilderPanels = levelController.GameUiManager.CityBuilderPanels;

        SetupBuildingsDictionary();
        cityBuilderPanels.BuildingBuilderPanel.SetupBuildingsData(buildingsDictionary[levelController.Player.RaceType]);
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
