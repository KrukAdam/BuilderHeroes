using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSkillsController : MonoBehaviour
{
    public event Action OnMainSkillUse = delegate { };
    public event Action OnSecondSkillUse = delegate { };

    public Skill MainRaceSkill { get => mainSkill; }
    public Skill SecondRaceSkill { get => secondSkill; }

    private Skill mainSkill = null;
    private Skill secondSkill = null;
    private PlayerCharacter playerCharacter;
    private SkillSetupInfo skillSetupInfo;
    private float mainSkillCooldawn;
    private float secondSkillCooldawn;
    private float attackSpeed;
    private Coroutine castSkill;
    private ItemSlot ammoSlot;

    private void Update()
    {
        if (mainSkillCooldawn > 0)
        {
            mainSkillCooldawn -= Time.deltaTime;
        }
        if (secondSkillCooldawn > 0)
        {
            secondSkillCooldawn -= Time.deltaTime;
        }
    }

    public void Setup(LocalController levelController)
    {
        this.playerCharacter = levelController.Player;

        ammoSlot = levelController.GameUiManager.CharacterPanels.EquipmentWeaponSkillsPanel.EquipmentPanel.AmmoSlot;

        skillSetupInfo = new SkillSetupInfo(playerCharacter, gameObject.transform);
        mainSkill.SetupSkill(skillSetupInfo);
        secondSkill.SetupSkill(skillSetupInfo);

        playerCharacter.Stats.OnDamage += StopSkillCasting;
        playerCharacter.Stats.OnDeath += StopSkillCasting;

        levelController.GameUiManager.OnTogglePanels += BindSkillToMouse;

        GameManager.Instance.InputManager.InputController.Player.CancelAction.performed += StopSkillCasting;
        GameManager.Instance.InputManager.InputController.Player.MainSkill.performed += UseMainSkill;
        GameManager.Instance.InputManager.InputController.Player.SecondSkill.performed += UseSecondSkill;

        playerCharacter.Stats.OnStatsChange += SetSpeedAttack;

        SetSpeedAttack();
        BindSkillToMouse(true);
    }

    private void OnDestroy()
    {

        playerCharacter.Stats.OnDamage -= StopSkillCasting;
        playerCharacter.Stats.OnDeath -= StopSkillCasting;

        GameManager.Instance.InputManager.InputController.Player.CancelAction.performed -= StopSkillCasting;
        GameManager.Instance.InputManager.InputController.Player.MainSkill.performed -= UseMainSkill;
        GameManager.Instance.InputManager.InputController.Player.SecondSkill.performed -= UseSecondSkill;
    }

    public void SetupRaceSkill(Skill mainSkill, Skill secondSkill)
    {
        this.mainSkill = mainSkill;
        this.secondSkill = secondSkill;
    }

    public void SetMainSkill(Skill skill)
    {
        mainSkill = Instantiate(skill);
        mainSkill.SetupSkill(skillSetupInfo);
    }

    public void SetSecondSkill(Skill skill)
    {
        secondSkill = Instantiate(skill);
        secondSkill.SetupSkill(skillSetupInfo);
    }

    public void BindSkillToMouse(bool panelsAreActive)
    {
        Debug.Log("Use mouse to skill: " + panelsAreActive);
        if (!panelsAreActive)
        {
            GameManager.Instance.InputManager.InputController.Player.MainSkill.Enable();
            GameManager.Instance.InputManager.InputController.Player.SecondSkill.Enable();
        }
        else
        {
            GameManager.Instance.InputManager.InputController.Player.MainSkill.Disable();
            GameManager.Instance.InputManager.InputController.Player.SecondSkill.Disable();
        }
    }

    public void SetTimeBlockInteraction(float timeBlock, bool resetTime = false)
    {
        if (resetTime)
        {
            mainSkillCooldawn = timeBlock;
            secondSkillCooldawn = timeBlock;
        }
        else
        {
            if (mainSkillCooldawn < timeBlock) mainSkillCooldawn = timeBlock;
            if (secondSkillCooldawn < timeBlock) secondSkillCooldawn = timeBlock;
        }

        playerCharacter.PlayerActionController.AddTimeToNextInteraction(timeBlock, resetTime);
    }

    public void UseMainSkill(InputAction.CallbackContext context)
    {
        if (CanUseSkill(mainSkill) && mainSkillCooldawn <= 0)
        {
            castSkill = StartCoroutine(SkillCasting(mainSkill));
            mainSkillCooldawn = GetTimeCooldawn(mainSkill);
            playerCharacter.MoveController.SetTimeBlockMove(mainSkill.TimeToBlockCasterMove);
            SetTimeBlockInteraction(mainSkill.TimeToBlockCasterAction);
            OnMainSkillUse();
        }
    }

    public void UseSecondSkill(InputAction.CallbackContext context)
    {
        if (CanUseSkill(secondSkill) && secondSkillCooldawn <= 0)
        {
            castSkill = StartCoroutine(SkillCasting(secondSkill));
            secondSkillCooldawn = GetTimeCooldawn(secondSkill);
            playerCharacter.MoveController.SetTimeBlockMove(secondSkill.TimeToBlockCasterMove);
            SetTimeBlockInteraction(secondSkill.TimeToBlockCasterAction);
            OnSecondSkillUse();
        }
    }

    public IEnumerator SkillCasting(Skill skill = null)
    {
        yield return skill.WaitForCastTime;
        if (HasAmmo(skill, true))
        {
            skill.UseSkill();
        }
    }

    public void StopSkillCasting(InputAction.CallbackContext context)
    {
        if(castSkill != null)
        {
            Debug.Log("StopCasting");
            StopCoroutine(castSkill);
            SetTimeBlockInteraction(0, true);
            playerCharacter.MoveController.SetTimeBlockMove(0, true);
        }
    }

    public void StopSkillCasting()
    {
        if (castSkill != null)
        {
            Debug.Log("StopCasting");
            StopCoroutine(castSkill);
            SetTimeBlockInteraction(0, true);
            playerCharacter.MoveController.SetTimeBlockMove(0, true);
        }
    }

    private void SetSpeedAttack()
    {
        attackSpeed = playerCharacter.Stats.GetAttackSpeed();
    }

    private float GetTimeCooldawn(Skill skill)
    {
        if (skill.SkillCooldawn <= 0) return 0;

        float time = skill.SkillCooldawn - attackSpeed;
        if(time < skill.SkillCooldawn / 10) time = skill.SkillCooldawn / 10;

        Debug.Log("Time to cooldawn skill: " + skill + "  - " + time);
        return time;
    }

    private bool HasAmmo(Skill skill, bool useAmmo = false)
    {
        RangeSkill rangeSkill = skill as RangeSkill;

        if (!rangeSkill || !useAmmo) return true;           //Skill not use ammo, it is not a range skill, and return true. Can use skill

        if (rangeSkill.AmmoType != EItemAmmoType.None)
        {
            if (ammoSlot.Item != null)
            {
                ItemAmmo ammoItem = ammoSlot.Item as ItemAmmo;
                if (ammoItem)
                {
                    if (rangeSkill.AmmoType == ammoItem.AmmoType)
                    {
                        if (rangeSkill.AmmoUsePerShot <= ammoSlot.Amount)
                        {
                            //Can use skill
                            if (useAmmo) ammoSlot.Amount -= rangeSkill.AmmoUsePerShot;
                            return true;
                        }
                        else
                        {
                            Debug.Log("You need more ammo to shot");
                            return false;
                        }
                    }
                    else
                    {
                        Debug.Log("Bad type of ammo");
                        return false;
                    }
                }
                else
                {
                    Debug.Log("Wrong ammo in slot");
                    return false;
                }
            }
            else
            {
                Debug.Log("No have any ammo");
                return false;
            }
        }
        else
        {
            Debug.Log("No need ammo");
            return true;
        }
    }

    private bool CanUseSkill(Skill skill)
    {
        if (HasAmmo(skill))
        {
            if (skill.CanUse())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
