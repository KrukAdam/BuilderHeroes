using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButton : BasicButton
{
    [SerializeField] private Text buildingName = null;

    Building building;

    public void Setup(Building building)
    {
        this.building = building;

        buildingName.text = building.BuildingName;
    }
}
