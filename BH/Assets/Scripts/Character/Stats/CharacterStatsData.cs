using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Kruk/CharacterStats/Character Stats Database")]
public class CharacterStatsData : ScriptableObject
{
	public List<CharacterStat> AllStats { get => allStats; }

	[SerializeField] private List<CharacterStat> allStats = null;

}
