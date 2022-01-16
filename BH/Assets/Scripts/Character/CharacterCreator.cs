using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCreator : MonoBehaviour
{
    public RaceData RaceData { get => raceData; }
    public Stats Stats { get => stats; }

    [SerializeField] private Stats stats = null;

    private RaceData raceData;
    

    public void SetupRace(RaceData raceData)
    {
        this.raceData = raceData;

        stats.Init();
        ExecuteStartStatsEffect();

        //TODO reset all value to basic 
    }

    private void ExecuteStartStatsEffect()
    {
        foreach (var stat in raceData.RaceBasicStats.Auras)
        {
            stat.ExecuteEffect(raceData.RaceBasicStats, stats);
        }
    }
}
