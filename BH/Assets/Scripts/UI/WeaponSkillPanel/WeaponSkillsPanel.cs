using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSkillsPanel : MonoBehaviour
{
    public event Action<Skill> OnPointerEnterSkillButton = delegate { };
    public event Action OnPointerExitSkillButton = delegate { };

    [SerializeField] private Color hoverMainSkillColor = Color.green;
    [SerializeField] private Color hoverSecondSkillColor = Color.green;
    [SerializeField] private Color basicColor = Color.white;
    //MH = main hand, SH = second hand, BR = basic race skills
    [SerializeField] private CharacterWeaponSkillButton mainSkillButtonBR = null;
    [SerializeField] private CharacterWeaponSkillButton secondSkillButtonBR = null;
    [SerializeField] private CharacterWeaponSkillButton mainSkillButtonMH = null;
    [SerializeField] private CharacterWeaponSkillButton secondSkillButtonMH = null;
    [SerializeField] private CharacterWeaponSkillButton mainSkillButtonSH = null;
    [SerializeField] private CharacterWeaponSkillButton secondSkillButtonSH = null;

    private PlayerSkillsController playerSkillsController;
    private List<CharacterWeaponSkillButton> characterWeaponSkillButtons;
    private ECharacterWeaponSkillButtonType mainSkillButtonSelected;
    private ECharacterWeaponSkillButtonType secondSkillButtonSelected;
    private Dictionary<ECharacterWeaponSkillButtonType, Skill> skillsSet = new Dictionary<ECharacterWeaponSkillButtonType, Skill>();

    public void Setup(PlayerSkillsController playerSkillsController)
    {
        this.playerSkillsController = playerSkillsController;
        SetupImageOnStart();
        InitButtons();
        SetupBaseRaceSkill(playerSkillsController.MainRaceSkill, playerSkillsController.SecondRaceSkill);
    }

    public void SetMainSkillSelected(ECharacterWeaponSkillButtonType buttonType)
    {
        if (!skillsSet.ContainsKey(buttonType)) return;

        UnselectedSkillButton(mainSkillButtonSelected);
        foreach (var button in characterWeaponSkillButtons)
        {
            if(button.ButtonType == buttonType)
            {
                mainSkillButtonSelected = buttonType;
                playerSkillsController.SetMainSkill(skillsSet[buttonType]);

                if (secondSkillButtonSelected == buttonType)
                {
                    if(buttonType == ECharacterWeaponSkillButtonType.SecondRaceSkill)
                    {
                        SetSecondSkillSelected(ECharacterWeaponSkillButtonType.MainRaceSkill);
                    }
                    else
                    {
                        SetSecondSkillSelected(ECharacterWeaponSkillButtonType.SecondRaceSkill);
                    }
                }
                button.ButtonImage.color = hoverMainSkillColor;
            }
        }
    }

    public void SetSecondSkillSelected(ECharacterWeaponSkillButtonType buttonType)
    {
        if (!skillsSet.ContainsKey(buttonType)) return;

        UnselectedSkillButton(secondSkillButtonSelected);
        foreach (var button in characterWeaponSkillButtons)
        {
            if (button.ButtonType == buttonType)
            {
                secondSkillButtonSelected = buttonType;
                playerSkillsController.SetSecondSkill(skillsSet[buttonType]);

                if (mainSkillButtonSelected == buttonType)
                {
                    if (buttonType == ECharacterWeaponSkillButtonType.MainRaceSkill)
                    {
                        SetMainSkillSelected(ECharacterWeaponSkillButtonType.SecondRaceSkill);
                    }
                    else
                    {
                        SetMainSkillSelected(ECharacterWeaponSkillButtonType.MainRaceSkill);
                    }
                }
                button.ButtonImage.color = hoverSecondSkillColor;
            }
        }
    }

    public void SetupBaseRaceSkill(Skill mainSkill, Skill secondSkill)
    {
        mainSkillButtonBR.SkillImage.sprite = mainSkill.SkillSprite;
        secondSkillButtonBR.SkillImage.sprite = secondSkill.SkillSprite;

        AddSkillToList(ECharacterWeaponSkillButtonType.MainRaceSkill, mainSkill);
        AddSkillToList(ECharacterWeaponSkillButtonType.SecondRaceSkill, secondSkill);

        SetMainSkillSelected(ECharacterWeaponSkillButtonType.MainRaceSkill);
        SetSecondSkillSelected(ECharacterWeaponSkillButtonType.SecondRaceSkill);
    }

    public void UnsetupWeaponSkills(ItemEquippable item)
    {
        if (item.EquipmentType == EEquipmentType.MainHand)
        {
            mainSkillButtonMH.SkillImage.enabled = false;
            secondSkillButtonMH.SkillImage.enabled = false;

            skillsSet.Remove(ECharacterWeaponSkillButtonType.MainSkillMainHand);
            skillsSet.Remove(ECharacterWeaponSkillButtonType.SecondSkillMainHand);
            ResetSelectedButton(ECharacterWeaponSkillButtonType.MainSkillMainHand);
            ResetSelectedButton(ECharacterWeaponSkillButtonType.SecondSkillMainHand);
        }
        else if (item.EquipmentType == EEquipmentType.SecondHand)
        {
            mainSkillButtonSH.SkillImage.enabled = false;
            secondSkillButtonSH.SkillImage.enabled = false;

            skillsSet.Remove(ECharacterWeaponSkillButtonType.MainSkillSecondHand);
            skillsSet.Remove(ECharacterWeaponSkillButtonType.SecondSkillSecondHand);
            ResetSelectedButton(ECharacterWeaponSkillButtonType.MainSkillSecondHand);
            ResetSelectedButton(ECharacterWeaponSkillButtonType.SecondSkillSecondHand);
        }

    }

    public void SetupWeaponSkills(ItemEquippable item)
    {
        if (item.EquipmentType == EEquipmentType.MainHand)
        {
            if (item.MainSkill)
            {
                mainSkillButtonMH.SkillImage.sprite = item.MainSkill.SkillSprite;
                mainSkillButtonMH.SkillImage.enabled = true;
                AddSkillToList(ECharacterWeaponSkillButtonType.MainSkillMainHand, item.MainSkill);
            }
            else
            {
                mainSkillButtonMH.SkillImage.enabled = false;
            }

            if (item.SecondSkill)
            {
                secondSkillButtonMH.SkillImage.sprite = item.SecondSkill.SkillSprite;
                secondSkillButtonMH.SkillImage.enabled = true;
                AddSkillToList(ECharacterWeaponSkillButtonType.SecondSkillMainHand, item.SecondSkill);
            }
            else
            {
                secondSkillButtonMH.SkillImage.enabled = false;
            }
        }
        else if (item.EquipmentType == EEquipmentType.SecondHand)
        {
            if (item.MainSkill)
            {
                mainSkillButtonSH.SkillImage.sprite = item.MainSkill.SkillSprite;
                mainSkillButtonSH.SkillImage.enabled = true;
                AddSkillToList(ECharacterWeaponSkillButtonType.MainSkillSecondHand, item.MainSkill);
            }
            else
            {
                mainSkillButtonSH.SkillImage.enabled = false;
            }

            if (item.SecondSkill)
            {
                secondSkillButtonSH.SkillImage.sprite = item.SecondSkill.SkillSprite;
                secondSkillButtonSH.SkillImage.enabled = true;
                AddSkillToList(ECharacterWeaponSkillButtonType.SecondSkillSecondHand, item.SecondSkill);
            }
            else
            {
                secondSkillButtonSH.SkillImage.enabled = false;
            }
        }
    }

    public void AddToButtonsList(CharacterWeaponSkillButton characterWeaponSkillButton)
    {
        characterWeaponSkillButtons.Add(characterWeaponSkillButton);
    }

    public void OnPointerEnterOnSkillButton(ECharacterWeaponSkillButtonType buttonType)
    {
        if(skillsSet.TryGetValue(buttonType, out Skill skill))
        {
            OnPointerEnterSkillButton(skill);
        }
    }

    public void OnPointerExitOnSkillButton()
    {
        OnPointerExitSkillButton();
    }

    private void ResetSelectedButton(ECharacterWeaponSkillButtonType buttonType)
    {
        if(buttonType == mainSkillButtonSelected)
        {
            SetMainSkillSelected(ECharacterWeaponSkillButtonType.MainRaceSkill);
        }
        if (buttonType == secondSkillButtonSelected)
        {
            SetSecondSkillSelected(ECharacterWeaponSkillButtonType.SecondRaceSkill);
        }
    }

    private void AddSkillToList(ECharacterWeaponSkillButtonType buttonType, Skill skill)
    {
        if (skillsSet.ContainsKey(buttonType))
        {
            skillsSet.Remove(buttonType);
            skillsSet.Add(buttonType, skill);
        }
        else
        {
            skillsSet.Add(buttonType, skill);
        }
    }

    private void SetupImageOnStart()
    {
        mainSkillButtonMH.SkillImage.enabled = false;
        secondSkillButtonMH.SkillImage.enabled = false;
        mainSkillButtonSH.SkillImage.enabled = false;
        secondSkillButtonSH.SkillImage.enabled = false;
    }

    private void InitButtons()
    {
        characterWeaponSkillButtons = new List<CharacterWeaponSkillButton>();

        mainSkillButtonBR.InitButton(this);
        mainSkillButtonMH.InitButton(this);
        mainSkillButtonSH.InitButton(this);
        secondSkillButtonBR.InitButton(this);
        secondSkillButtonMH.InitButton(this);
        secondSkillButtonSH.InitButton(this);
    }

    private void UnselectedSkillButton(ECharacterWeaponSkillButtonType buttonType)
    {
        foreach (var button in characterWeaponSkillButtons)
        {
            if(button.ButtonType == buttonType)
            {
                button.ButtonImage.color = basicColor;
            }
        }
    }
}
