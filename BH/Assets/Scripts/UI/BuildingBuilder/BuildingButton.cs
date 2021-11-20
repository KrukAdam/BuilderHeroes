using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuildingButton : BasicButton
{

    public event Action<Building> ClickOnBuildingButton = delegate { };

    [SerializeField] private Text buildingName = null;

    Building building;

    public void Setup(Building building, BuildingBuilderManager buildingBuilderManager)
    {
        this.building = building;
        buildingName.text = building.BuildingName;
    }

    protected override void AfterButtonClick()
    {
        base.AfterButtonClick();
        ClickOnBuildingButton(building);
    }
}
