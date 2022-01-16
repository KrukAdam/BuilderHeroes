using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class MainMenuUiManager : MonoBehaviour
{
    public MainMenuPanel MainMenuPanel { get => mainMenuPanel; }
    public NewGamePanel NewGamePanel { get => newGamePanel; }
    public TooltipsPanels TooltipsPanels { get => tooltipsPanels; }

    [SerializeField] private MainMenuPanel mainMenuPanel = null;
    [SerializeField] private OptionsPanel optionsPanel = null;
    [SerializeField] private TooltipsPanels tooltipsPanels = null;
    [SerializeField] private NewGamePanel newGamePanel = null;

    private void OnDestroy()
    {
        newGamePanel.RaceSkillWindow.BtnMainSkill.OnPointerEnterAction -= tooltipsPanels.ShowMainSkillTooltip;
        newGamePanel.RaceSkillWindow.BtnSecondSkill.OnPointerEnterAction -= tooltipsPanels.ShowMainSkillTooltip;

        newGamePanel.RaceSkillWindow.BtnMainSkill.OnPointerExitAction -= tooltipsPanels.HideMainSkillTooltip;
        newGamePanel.RaceSkillWindow.BtnSecondSkill.OnPointerExitAction -= tooltipsPanels.HideMainSkillTooltip;
    }

    public void Setup(LocalMainMenuController localMainMenuController)
    {
        StartCoroutine(SetupPanels(localMainMenuController));

        SetupEvents();
     
    }

    public void SwichOptionPanel()
    {
        optionsPanel.gameObject.SetActive(!optionsPanel.gameObject.activeSelf);

        if (newGamePanel.gameObject.activeSelf) newGamePanel.gameObject.SetActive(false);
    }

    public void SwichNewGamePanel()
    {
        newGamePanel.gameObject.SetActive(!newGamePanel.gameObject.activeSelf);

        if (optionsPanel.gameObject.activeSelf) optionsPanel.gameObject.SetActive(false);
    }

    private IEnumerator SetupPanels(LocalMainMenuController localMainMenuController)
    {
        // Wait for the localization system to initialize
        yield return LocalizationSettings.InitializationOperation;
        mainMenuPanel.Setup(this);
        optionsPanel.Setup();
        newGamePanel.Setup(this);
    }

    private void SetupEvents()
    {
        newGamePanel.RaceSkillWindow.BtnMainSkill.OnPointerEnterAction += tooltipsPanels.ShowMainSkillTooltip;
        newGamePanel.RaceSkillWindow.BtnSecondSkill.OnPointerEnterAction += tooltipsPanels.ShowMainSkillTooltip;

        newGamePanel.RaceSkillWindow.BtnMainSkill.OnPointerExitAction += tooltipsPanels.HideMainSkillTooltip;
        newGamePanel.RaceSkillWindow.BtnSecondSkill.OnPointerExitAction += tooltipsPanels.HideMainSkillTooltip;
    }
}
