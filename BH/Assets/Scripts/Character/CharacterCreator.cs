using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCreator : MonoBehaviour
{
    public ERaceType RaceType { get => raceType; }

    private ERaceType raceType;

    public void SetupRace(ERaceType raceType)
    {
        this.raceType = raceType;

        //TODO reset all value to basic 
    }
}
