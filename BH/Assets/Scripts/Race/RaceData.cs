using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(menuName = "Game/Race/Race")]
public class RaceData : ScriptableObject
{
    public ERaceType RaceType { get => raceType; }
    public LocalizedString RaceName { get => raceName; }
    public Skill MainRaceSkill { get => mainRaceSkill; }
    public Skill SecondRaceSkill { get => secondRaceSkill; }
    public AuraSkill RaceBasciStats { get => raceBasciStats; }

    [SerializeField] private ERaceType raceType = ERaceType.None;
    [SerializeField] private LocalizedString raceName = null;
    [SerializeField] private Skill mainRaceSkill = null;
    [SerializeField] private Skill secondRaceSkill = null;
    [Space]
    [SerializeField] private AuraSkill raceBasciStats = null;
}
