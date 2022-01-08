using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Database/Character Stats Database")]
public class CharacterStatsData : ScriptableObject
{
	public List<CharacterStat> AllStats { get => allStats; }

	[SerializeField] private List<CharacterStat> allStats = null;

	private Dictionary<EStatsTypes, CharacterStat> allStatsDictionary = new Dictionary<EStatsTypes, CharacterStat>();

    public void Setup()
    {
        SetStatsDictionary();
    }

    public CharacterStat GetStat(EStatsTypes type)
    {
        return allStatsDictionary[type];
    }

    private void SetStatsDictionary()
    {
        allStatsDictionary.Clear();

        foreach (var stat in allStats)
        {
            allStatsDictionary.Add(stat.StatType, stat);
        }
    }


}
