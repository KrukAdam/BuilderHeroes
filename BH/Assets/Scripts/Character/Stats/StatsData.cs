using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Kruk/CharacterStats/Stats Data")]
public class StatsData : ScriptableObject
{
    public List<CharacterStat> Stats { get => stats; }

    [SerializeField] private List<CharacterStat> stats = null;
    [SerializeField] private CharacterStatsData characterStatsDatabase = null;

    private CharacterStatsData characterStatsData;

    public void SetBaseStats()
    {
        characterStatsData = Instantiate(characterStatsDatabase);
        stats = characterStatsData.AllStats;
    }

    public void Setup()
    {
        foreach (var stat in stats)
        {
            if (stat.StatUseValues == EStatsValueUse.BaseAndMaxValue)
            {
                stat.BaseValue.Value = stat.MaxValue.Value;
            }
        }
    }
}
