using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public PlayerCharacter Player { get => player; }

    [SerializeField] private PlayerCharacter player = null;
    [SerializeField] private GameUiManager gameUiManager = null;

    private void Awake()
    {
        gameUiManager.Setup(this);
        player.Init(gameUiManager);
    }
}
