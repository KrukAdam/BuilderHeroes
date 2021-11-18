using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingsPanels : MonoBehaviour
{
    [SerializeField] private CraftingPanel[] craftingPanels = null;

    private GameUiManager gameUiManager;

    public void Setup(GameUiManager gameUiManager)
    {
        this.gameUiManager = gameUiManager;

        InitCraftingPanels();
    }

    private void InitCraftingPanels()
    {
        foreach (var craftPanel in craftingPanels)
        {
            craftPanel.OnPointerEnterEvent += gameUiManager.ShowTooltip;
            craftPanel.OnPointerExitEvent += gameUiManager.HideTooltip;
        }
    }
}
