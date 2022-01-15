using System.Collections;
using System.Collections.Generic;
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
    }

    private void RespawnPlayer()
    {
        playerCharacter = Instantiate(playerCharacterPrefab, respawnPoint.position, Quaternion.identity);
    }

    private void SetRace()
    {
        playerCharacter.SetupRace(characterCreator.RaceType);
    }
}
