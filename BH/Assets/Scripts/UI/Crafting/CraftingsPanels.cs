using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingsPanels : MonoBehaviour
{
    [SerializeField] private CraftingPanel[] craftingPanels = null;

    private GameUiManager gameUiManager;

    public void Setup(LevelController levelController)
    {
        gameUiManager = levelController.GameUiManager;

        InitCraftingPanels();
    }

    private void InitCraftingPanels()
    {
        foreach (var craftPanel in craftingPanels)
        {
            craftPanel.OnPointerEnterEvent += gameUiManager.TooltipsPanels.ShowItemTooltip;
            craftPanel.OnPointerExitEvent += gameUiManager.TooltipsPanels.HideItemTooltip;
        }
    }
}