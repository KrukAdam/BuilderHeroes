using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGamePanel : MonoBehaviour
{
    public event Action OnBtnStartGameClick = delegate { };

    [SerializeField] private BasicButton btnStartGame = null;
    
    public void Setup()
    {
        btnStartGame.SetupListener(BtnStartGameClick);

        gameObject.SetActive(false);
    }

    private void BtnStartGameClick()
    {
        OnBtnStartGameClick();
    }
}
