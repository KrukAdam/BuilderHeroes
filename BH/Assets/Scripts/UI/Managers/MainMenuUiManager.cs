using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class MainMenuUiManager : MonoBehaviour
{
    public MainMenuPanel MainMenuPanel { get => mainMenuPanel; }
    public NewGamePanel NewGamePanel { get => newGamePanel; }

    [SerializeField] private MainMenuPanel mainMenuPanel = null;
    [SerializeField] private OptionsPanel optionsPanel = null;
    [SerializeField] private NewGamePanel newGamePanel = null;

    public void Setup(LocalMainMenuController localMainMenuController)
    {
        StartCoroutine(SetupPanels(localMainMenuController));
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
        newGamePanel.Setup();
    }
}
