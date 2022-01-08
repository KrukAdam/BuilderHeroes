using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalMainMenuController : MonoBehaviour
{
    [SerializeField] private MainMenuUiManager mainMenuUiManager = null;

    private void Awake()
    {
        mainMenuUiManager.Setup(this);

        mainMenuUiManager.MainMenuPanel.OnBtnExitClick += ExitGame;
    }

    private void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
