using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBuilderManager : MonoBehaviour
{
    public bool BuildingSelected => blueprintSelected;

    [SerializeField] private LayerMask buildBlockingLayers = LayerMask.GetMask();
    [SerializeField] private BuildingBlueprint buildingBlueprintPrefab = null;
    [SerializeField] private Construction constructionPrefab = null;
    [SerializeField] private BuildingsData[] buildingsDatas = null;
    [SerializeField] private Transform buildingsParent = null;

    private LocalManagers localManagers;
    private Dictionary<ERaceType, BuildingsData> buildingsDictionary = new Dictionary<ERaceType, BuildingsData>();
    private CityBuilderPanels cityBuilderPanels;
    private Transform interactionPointer;
    private Building selectedBuilding;
    private BuildingBlueprint buildingBlueprint;
    private Construction construction;
    private bool blueprintSelected = false;

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
        blueprintSelected = true;
    }

    public void DeselectedBuilding()
    {
        if (blueprintSelected)
        {
            blueprintSelected = false;
            Destroy(buildingBlueprint.gameObject);
            selectedBuilding = null;
        }
    }

    public void Build()
    {
        if (buildingBlueprint.CanBuild())
        {
            //TODO take resources
            construction = Instantiate(constructionPrefab, buildingsParent);
            construction.transform.position = buildingBlueprint.transform.position;
            construction.Setup(selectedBuilding);

            DeselectedBuilding();
        }
        else
        {
            //TODO info why cant build
        }
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
