using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPanel : MonoBehaviour
{
    [SerializeField] private BasicButton openEqAndStatButton = null;

    private GameUiManager gameUiManager;

    public void Init(GameUiManager gameUiManager)
    {
        this.gameUiManager = gameUiManager;

        openEqAndStatButton.SetupListener(gameUiManager.ToggleEqAndStatsPanel);
    }
}
