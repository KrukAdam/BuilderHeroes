using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBuilderManager : MonoBehaviour
{
    //public event Action OnSelectedBuilding = delegate { };
    //public event Action OnDeselectedBuilding = delegate { };

    public bool BuildingSelected => buildingSelected;

    [SerializeField] private LayerMask buildBlockingLayers = LayerMask.GetMask();
    [SerializeField] private BuildingBlueprint buildingBlueprintPrefab = null;
    [SerializeField] private BuildingsData[] buildingsDatas = null;

    private LocalManagers localManagers;
    private Dictionary<ERaceType, BuildingsData> buildingsDictionary = new Dictionary<ERaceType, BuildingsData>();
    private CityBuilderPanels cityBuilderPanels;
    private Transform interactionPointer;
    private Building selectedBuilding;
    private BuildingBlueprint buildingBlueprint;
    private bool buildingSelected = false;

    public void Setup(LevelController levelController)
    {
        cityBuilderPanels = levelController.GameUiManager.CityBuilderPanels;
        interactionPointer = levelController.Player.PlayerActionController.InteractionPointer;

        SetupBuildingsDictionary();
        cityBuilderPanels.BuildingBuilderPanel.SetupBuildingsDataPanel(buildingsDictionary[levelController.Player.RaceType], this);

        GameManager.Instance.InputManager.InputController.Player.CancelAction.performed += ctx => DeselectedBuilding();
    }

    public void SelectedBuilding(Building building)
    {
        if (selectedBuilding == building) return;

        DeselectedBuilding();
        selectedBuilding = building;
        buildingBlueprint = Instantiate(buildingBlueprintPrefab, interactionPointer);
        buildingBlueprint.Setup(selectedBuilding, interactionPointer, buildBlockingLayers);
        buildingSelected = true;
    }

    public void DeselectedBuilding()
    {
        if (buildingSelected)
        {
            buildingSelected = false;
            Destroy(buildingBlueprint.gameObject);
            selectedBuilding = null;
        }
    }

    public bool Build()
    {
        return buildingBlueprint.CanBuild();
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
