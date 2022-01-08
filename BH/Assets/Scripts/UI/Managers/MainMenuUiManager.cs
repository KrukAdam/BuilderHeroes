using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class MainMenuUiManager : MonoBehaviour
{
    [SerializeField] private MainMenuPanel mainMenuPanel = null;

    public void Setup(LocalMainMenuController localMainMenuController)
    {
        StartCoroutine(SetupPanels(localMainMenuController));

    }

    private IEnumerator SetupPanels(LocalMainMenuController localMainMenuController)
    {
        // Wait for the localization system to initialize
        yield return LocalizationSettings.InitializationOperation;
        mainMenuPanel.Setup();
    }
}
