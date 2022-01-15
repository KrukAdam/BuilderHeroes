using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPanel : MonoBehaviour
{
    public event Action OnBtnExitClick = delegate { };

    [SerializeField] private BasicButton btnExitGame = null;
    [SerializeField] private BasicButton btnOption = null;
    [SerializeField] private BasicButton btnNewGame = null;

    public void Setup(MainMenuUiManager mainMenuUiManager)
    {
        btnExitGame.SetupListener(BtnExitClick);
        btnOption.SetupListener(mainMenuUiManager.SwichOptionPanel);
        btnNewGame.SetupListener(mainMenuUiManager.SwichNewGamePanel);
    }

    private void BtnExitClick()
    {
        OnBtnExitClick();
    }
}
