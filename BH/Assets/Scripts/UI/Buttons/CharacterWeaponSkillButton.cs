using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterWeaponSkillButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    public Image SkillImage { get => skillImage; }
    public Image ButtonImage { get => buttonImage; }
    public ECharacterWeaponSkillButtonType ButtonType { get => buttonType; }

    [SerializeField] private ECharacterWeaponSkillButtonType buttonType = ECharacterWeaponSkillButtonType.None;
    [SerializeField] private Image skillImage = null;
    [SerializeField] private Image buttonImage = null;

    private WeaponSkillsPanel weaponSkillsPanel;

    public void InitButton(WeaponSkillsPanel weaponSkillsPanel)
    {
        this.weaponSkillsPanel = weaponSkillsPanel;

        weaponSkillsPanel.AddToButtonsList(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        else if(eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    private void OnLeftClick()
    {
        weaponSkillsPanel.SetMainSkillSelected(buttonType);
    }

    private void OnRightClick()
    {
        weaponSkillsPanel.SetSecondSkillSelected(buttonType);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        weaponSkillsPanel.OnPointerEnterOnSkillButton(ButtonType);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        weaponSkillsPanel.OnPointerExitOnSkillButton();
    }
}
