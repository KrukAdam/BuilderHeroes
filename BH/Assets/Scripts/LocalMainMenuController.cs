using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalMainMenuController : MonoBehaviour
{
    [SerializeField] private MainMenuUiManager mainMenuUiManager = null;

    private void Awake()
    {
        mainMenuUiManager.Setup(this);
    }
}
