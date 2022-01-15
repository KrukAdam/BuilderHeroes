using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(menuName = "Game/Race/Race")]
public class RaceData : ScriptableObject
{
    public ERaceType RaceType { get => raceType; }
    public LocalizedString RaceName { get => raceName; }

    [SerializeField] private ERaceType raceType = ERaceType.None;
    [SerializeField] private LocalizedString raceName = null;
}
