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

    private Dictionary<ERaceType, BuildingsData> buildingsDictionary = new Dictionary<ERaceType, BuildingsData>();
    private CityBuilderPanels cityBuilderPanels;
    private Transform interactionPointer;
    private Building selectedBuilding;
    private BuildingBlueprint buildingBlueprint;
    private Construction construction;
    private LocalManagers localManagers;
    private bool blueprintSelected = false;

    public void Setup(LocalController localController)
    {
        cityBuilderPanels = localController.GameUiManager.CityBuilderPanels;
        interactionPointer = localController.Player.PlayerActionController.InteractionPointer;
        localManagers = localController.LocalManagers;

        SetupBuildingsDictionary();
        InstantiateBuildingBlueprint();
        cityBuilderPanels.BuildingBuilderPanel.SetupBuildingsDataPanel(buildingsDictionary[localController.Player.RaceType], this);

        GameManager.Instance.InputManager.InputController.Player.CancelAction.performed += ctx => DeselectedBuilding();

        cityBuilderPanels.OnToggleBuilderPanel += OnTogglePanel;
    }

    public void SelectedBuilding(Building building)
    {
        if (selectedBuilding == building) return;

        DeselectedBuilding();
        selectedBuilding = building;
        BuildingBlueprintActive(true);
        buildingBlueprint.Setup(selectedBuilding, interactionPointer, buildBlockingLayers);
        blueprintSelected = true;
    }

    public void DeselectedBuilding()
    {
        if (blueprintSelected)
        {
            blueprintSelected = false;
            BuildingBlueprintActive(false);
            selectedBuilding = null;
        }
    }

    public void Build()
    {
        if (buildingBlueprint.CanBuild())
        {
            construction = Instantiate(constructionPrefab, buildingsParent);
            construction.transform.position = buildingBlueprint.transform.position;
            construction.Setup(selectedBuilding, localManagers.DropItemManager);

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

    private void OnTogglePanel(bool isActive)
    {
        if (!isActive)
        {
            DeselectedBuilding();
        }
    }

    private void InstantiateBuildingBlueprint()
    {
        buildingBlueprint = Instantiate(buildingBlueprintPrefab, interactionPointer);
        BuildingBlueprintActive(false);
    }

    private void BuildingBlueprintActive(bool active)
    {
        buildingBlueprint.gameObject.SetActive(active);
    }
}
