using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuPanel : MonoBehaviour
{
    public event Action OnClickBtnExitScene = delegate { };

    [SerializeField] private BasicButton btnExitScene = null;

    public void Setup()
    {
        btnExitScene.SetupListener(ClickBtnExitScene);
    }

    public void TogglePanel()
    {
        bool isActive = !gameObject.activeSelf;
        gameObject.SetActive(isActive);
    }

    public bool CheckActivePanels()
    {
        return gameObject.activeSelf;
    }

    private void ClickBtnExitScene()
    {
        OnClickBtnExitScene();
    }
}
