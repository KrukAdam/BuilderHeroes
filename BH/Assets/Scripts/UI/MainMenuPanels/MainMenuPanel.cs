using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPanel : MonoBehaviour
{
    [SerializeField] private BasicButton btnExitGame = null;

    public void Setup()
    {
        btnExitGame.SetupListener(ExitGame);
    }

    private void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
