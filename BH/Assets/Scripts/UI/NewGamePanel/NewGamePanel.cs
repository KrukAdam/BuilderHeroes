using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGamePanel : MonoBehaviour
{
    public event Action OnBtnStartGameClick = delegate { };

    [SerializeField] private BasicButton btnStartGame = null;
    [SerializeField] private DropdownRace dropdownRace = null;
    [SerializeField] private RaceSkillWindow raceSkillWindow = null;

    private void OnDestroy()
    {
        dropdownRace.OnValueChange -= raceSkillWindow.SetupSkills;
    }

    public void Setup()
    {
        dropdownRace.OnValueChange += raceSkillWindow.SetupSkills;
        dropdownRace.Setup();

        btnStartGame.SetupListener(BtnStartGameClick);

        gameObject.SetActive(false);
    }

    private void BtnStartGameClick()
    {
        OnBtnStartGameClick();
    }
}
