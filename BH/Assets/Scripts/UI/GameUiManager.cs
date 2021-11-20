using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUiManager : MonoBehaviour
{
    public event Action<bool> OnTogglePanels = delegate { };

    public CharacterPanels CharacterPanels { get => characterPanels; }

    public InventoryPanel Inventory { get => inventory; }
    public EquipmentPanel EquipmentPanel { get => equipmentPanel; }
    public ItemSlot AmmoSlot { get => ammoSlot; }
    public ItemSlot ToolSlot { get => toolSlot; }

    [SerializeField] private EquipmentPanel characterPanel = null;
    [SerializeField] private InventoryPanel inventory = null;
    [SerializeField] private EquipmentPanel equipmentPanel = null;
    [SerializeField] private ItemTooltip itemTooltip = null;
    [SerializeField] private ItemSlot ammoSlot = null;
    [SerializeField] private ItemSlot toolSlot = null;
    [SerializeField] private CraftingsPanels craftingsPanels = null;
    [SerializeField] private CharacterPanels characterPanels = null;

    private LevelController levelController;

    public void Setup(LevelController levelController)
    {
        this.levelController = levelController;

        craftingsPanels.Setup(this);
        characterPanels.SetupPanel(levelController);

        ToggleCharacterPanel();
        SetEvents();

        GameManager.Instance.InputManager.InputController.Player.CharacterAndInventory.performed += ctx => ToggleCharacterPanel();
    }

    private void OnDestroy()
    {
        inventory.OnPointerEnterEvent -= ShowTooltip;
        equipmentPanel.OnPointerEnterEvent -= ShowTooltip;
        inventory.OnPointerExitEvent -= HideTooltip;
        equipmentPanel.OnPointerExitEvent -= HideTooltip;
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

    private void SetEvents()
    {
        // Pointer Enter
        inventory.OnPointerEnterEvent += ShowTooltip;
        equipmentPanel.OnPointerEnterEvent += ShowTooltip;
        // Pointer Exit
        inventory.OnPointerExitEvent += HideTooltip;
        equipmentPanel.OnPointerExitEvent += HideTooltip;
    }

    private void ToggleCharacterPanel()
    {
        characterPanel.gameObject.SetActive(!characterPanel.gameObject.activeSelf);
        OnTogglePanels(!CheckForActivePanels());
    }

    private bool CheckForActivePanels()
    {
        if (characterPanel.gameObject.activeSelf) return true;

        //TODo check crafting panels

        return false;
    }
}
