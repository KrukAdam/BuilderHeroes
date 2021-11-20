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

    public void SetupBuildingsData(BuildingsData buildingsData)
    {
        this.buildingsData = buildingsData;
        SetupBuildingsButtons();
    }

    private void SetupBuildingsButtons()
    {
        foreach (var building in buildingsData.Buildings)
        {
            Debug.Log("Building");
            BuildingButton button;
            button = Instantiate(buildingButtonPrefab, transform);
            button.Setup(building);
        }
    }
}
