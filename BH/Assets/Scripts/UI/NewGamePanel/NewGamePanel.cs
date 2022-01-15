using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGamePanel : MonoBehaviour
{
    public event Action OnBtnStartGameClick = delegate { };

    [SerializeField] private BasicButton btnStartGame = null;
    [SerializeField] private DropdownRace dropdownRace = null;
    
    public void Setup()
    {
        dropdownRace.Setup();

        btnStartGame.SetupListener(BtnStartGameClick);

        gameObject.SetActive(false);
    }

    private void BtnStartGameClick()
    {
        OnBtnStartGameClick();
    }
}
