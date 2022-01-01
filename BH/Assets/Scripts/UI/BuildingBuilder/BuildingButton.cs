using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class BuildingButton : BasicButton
{

    public event Action<Building> ClickOnBuildingButton = delegate { };

    [SerializeField] private LocalizeStringEvent localizeBuildingName = null;

    Building building;

    public void Setup(Building building, BuildingBuilderManager buildingBuilderManager)
    {
        this.building = building;
        localizeBuildingName.StringReference = building.BuildingName;
    }

    protected override void AfterButtonClick()
    {
        base.AfterButtonClick();
        ClickOnBuildingButton(building);
    }
}
