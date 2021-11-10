using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUiManager : MonoBehaviour
{
    public event Action<bool> OnTogglePanels = delegate { };
    
    public Inventory Inventory { get => inventory; }
    public EquipmentPanel EquipmentPanel { get => equipmentPanel; }
    public StatPanel StatPanel { get => statPanel; }
    public ItemSlot AmmoSlot { get => ammoSlot; }

    [SerializeField] private GameObject eqAndStatsPanelObj = null;
    [SerializeField] private CharacterPanel characterPanel = null;
    [SerializeField] private Inventory inventory = null;
    [SerializeField] private StatPanel statPanel = null;
    [SerializeField] private EquipmentPanel equipmentPanel = null;
    [SerializeField] private ItemTooltip itemTooltip = null;
    [SerializeField] private ItemSlot ammoSlot = null;
    [SerializeField] private CraftingPanel[] craftingPanels = null;

    private GameManager gameManager;
    private LevelController levelController;

    public void Init(LevelController levelController)
    {
        gameManager = GameManager.Instance;
        this.levelController = levelController;

        characterPanel.Init(this);

        // Pointer Enter
        inventory.OnPointerEnterEvent += ShowTooltip;
        equipmentPanel.OnPointerEnterEvent += ShowTooltip;

        // Pointer Exit
        inventory.OnPointerExitEvent += HideTooltip;
        equipmentPanel.OnPointerExitEvent += HideTooltip;

        InitCraftingPanels();
        ToggleCharacterPanel();

        gameManager.InputManager.InputController.Player.CharacterAndInventory.performed += ctx => ToggleCharacterPanel();

    }

    public void SetStatsOnStatsPanel(Stats stats)
    {
        statPanel.SetStats(stats);
        statPanel.UpdateStatValues();
    }


    public void UpdateStatValues()
    {
        statPanel.UpdateStatValues();
    }


    public void ShowTooltip(BaseItemSlot itemSlot)
    {
        if (itemSlot.Item != null)
        {
            itemTooltip.ShowTooltip(itemSlot.Item);
        }
    }

    public void HideTooltip(BaseItemSlot itemSlot)
    {
        if (itemTooltip.gameObject.activeSelf)
        {
            itemTooltip.HideTooltip();
        }
    }

    public void ToggleEqAndStatsPanel()
    {
        eqAndStatsPanelObj.SetActive(!eqAndStatsPanelObj.activeSelf);
    }

    private void InitCraftingPanels()
    {
        foreach (var craftPanel in craftingPanels)
        {
            craftPanel.OnPointerEnterEvent += ShowTooltip;
            craftPanel.OnPointerExitEvent += HideTooltip;
        }
    }

    private void ToggleCharacterPanel()
    {
        characterPanel.gameObject.SetActive(!characterPanel.gameObject.activeSelf);
        OnTogglePanels(!CheckForActivePanels());
    }

    private bool CheckForActivePanels()
    {
        if (characterPanel.gameObject.activeSelf) return true;

        foreach (var panel in craftingPanels)
        {
            if (panel.gameObject.activeSelf) return true;
        }

        return false;
    }
}
