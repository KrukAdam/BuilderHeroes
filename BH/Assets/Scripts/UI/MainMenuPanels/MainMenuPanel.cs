using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPanel : MonoBehaviour
{
    public event Action OnBtnExitClick = delegate { };
    public event Action OnBtnStartGameClick = delegate { };

    [SerializeField] private BasicButton btnExitGame = null;
    [SerializeField] private BasicButton btnOption = null;
    [SerializeField] private BasicButton btnStartGame = null;

    public void Setup(MainMenuUiManager mainMenuUiManager)
    {
        btnExitGame.SetupListener(BtnExitClick);
        btnOption.SetupListener(mainMenuUiManager.SwichOptionPanel);
        btnStartGame.SetupListener(BtnStartGameClick);
    }

    private void BtnExitClick()
    {
        OnBtnExitClick();
    }

    private void BtnStartGameClick()
    {
        OnBtnStartGameClick();
    }

}
