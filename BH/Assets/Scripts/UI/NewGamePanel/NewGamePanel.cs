using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGamePanel : MonoBehaviour
{
    public event Action OnBtnStartGameClick = delegate { };
    public RaceSkillWindow RaceSkillWindow { get => raceSkillWindow; }

    [SerializeField] private BasicButton btnStartGame = null;
    [SerializeField] private DropdownRace dropdownRace = null;
    [SerializeField] private RaceSkillWindow raceSkillWindow = null;
    [SerializeField] private StatsPanel statsPanel = null;

    private void OnDestroy()
    {
        dropdownRace.OnValueChange -= raceSkillWindow.SetupSkills;
        dropdownRace.OnValueChange -= statsPanel.UpdateStats;
    }

    public void Setup(MainMenuUiManager mainMenuUiManager)
    {
        dropdownRace.OnValueChange += raceSkillWindow.SetupSkills;
        dropdownRace.OnValueChange += statsPanel.UpdateStats;
        dropdownRace.Setup();

        statsPanel.SetupPanel(mainMenuUiManager.TooltipsPanels);

        btnStartGame.SetupListener(BtnStartGameClick);

        gameObject.SetActive(false);
    }

    private void BtnStartGameClick()
    {
        OnBtnStartGameClick();
    }
}
