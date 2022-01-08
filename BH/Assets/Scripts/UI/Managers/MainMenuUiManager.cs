using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class MainMenuUiManager : MonoBehaviour
{
    public MainMenuPanel MainMenuPanel { get => mainMenuPanel; }

    [SerializeField] private MainMenuPanel mainMenuPanel = null;
    [SerializeField] private OptionsPanel optionsPanel = null;

    public void Setup(LocalMainMenuController localMainMenuController)
    {
        StartCoroutine(SetupPanels(localMainMenuController));
    }

    public void SwichOptionPanel()
    {
        optionsPanel.gameObject.SetActive(!optionsPanel.gameObject.activeSelf);
    }

    private IEnumerator SetupPanels(LocalMainMenuController localMainMenuController)
    {
        // Wait for the localization system to initialize
        yield return LocalizationSettings.InitializationOperation;
        mainMenuPanel.Setup(this);
        optionsPanel.Setup();
    }
}
