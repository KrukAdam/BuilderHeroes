using UnityEngine;

public class RespawnPlayerManager : MonoBehaviour
{
    public PlayerCharacter PlayerCharacter { get => playerCharacter; }

    [SerializeField] private PlayerCharacter playerCharacterPrefab = null;
    [SerializeField] private Transform respawnPoint = null;

    private PlayerCharacter playerCharacter;
    private CharacterCreator characterCreator;

    public void Setup()
    {
        characterCreator = GameManager.Instance.CharacterCreator;

        RespawnPlayer();
        SetRace();
        SetStats();
        SetRaceSkill();
    }

    private void SetStats()
    {
        playerCharacter.SetupStats();
        foreach (var buffStat in characterCreator.RaceData.RaceBasicStats.Auras)
        {
            buffStat.ExecuteEffect(characterCreator.RaceData.RaceBasicStats, playerCharacter.Stats);
        }
        playerCharacter.Stats.ResetToMaxValue();
    }

    private void RespawnPlayer()
    {
        playerCharacter = Instantiate(playerCharacterPrefab, respawnPoint.position, Quaternion.identity);
    }

    private void SetRace()
    {
        playerCharacter.SetupRace(characterCreator.RaceData.RaceType);
    }

    private void SetRaceSkill()
    {
        Skill main = Instantiate(characterCreator.RaceData.MainRaceSkill);
        Skill second = Instantiate(characterCreator.RaceData.SecondRaceSkill);
        playerCharacter.PlayerSkillsController.SetupRaceSkill(main, second);
    }
}
