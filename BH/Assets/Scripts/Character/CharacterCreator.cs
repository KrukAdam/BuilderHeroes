using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCreator : MonoBehaviour
{
    public RaceData RaceData { get => raceData; }

    private RaceData raceData;

    public void SetupRace(RaceData raceData)
    {
        this.raceData = raceData;

        //TODO reset all value to basic 
    }
}
