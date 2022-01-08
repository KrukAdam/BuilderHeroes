using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocalMainMenuController : MonoBehaviour
{
    [SerializeField] private MainMenuUiManager mainMenuUiManager = null;

    private void Awake()
    {
        mainMenuUiManager.Setup(this);

        mainMenuUiManager.MainMenuPanel.OnBtnExitClick += ExitGame;
        mainMenuUiManager.MainMenuPanel.OnBtnStartGameClick += StartGame;
    }

    private void StartGame()
    {
        SceneManager.LoadScene(Constant.SceneDemo);
    }

    private void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
