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
    [SerializeField] private Construction constructionPrefab = null;
    [SerializeField] private BuildingsData[] buildingsDatas = null;

    private LocalManagers localManagers;
    private Dictionary<ERaceType, BuildingsData> buildingsDictionary = new Dictionary<ERaceType, BuildingsData>();
    private CityBuilderPanels cityBuilderPanels;
    private Transform interactionPointer;
    private Building selectedBuilding;
    private Construction construction;
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

        selectedBuilding = building;
        construction = Instantiate(constructionPrefab, interactionPointer);
        construction.Setup(selectedBuilding);
        buildingSelected = true;
    }

    public void DeselectedBuilding()
    {
        if (buildingSelected)
        {
            buildingSelected = false;
            Destroy(construction.gameObject);
            selectedBuilding = null;
        }
    }

    public bool Build()
    {
        return construction.CheckBuildSpace(buildBlockingLayers);
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
