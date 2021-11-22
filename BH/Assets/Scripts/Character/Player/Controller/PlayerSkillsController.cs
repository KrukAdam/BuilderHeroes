using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillsController : MonoBehaviour
{
    public event Action OnMainSkillUse = delegate { };
    public event Action OnSecondSkillUse = delegate { };

    public Skill MainRaceSkill { get => Instantiate(mainSkillRacePrefab); }
    public Skill SecondRaceSkill { get => Instantiate(secondSkillRacePrefab); }

    [SerializeField] private Skill mainSkillRacePrefab = null;
    [SerializeField] private Skill secondSkillRacePrefab = null;

    private Skill mainSkill = null;
    private Skill secondSkill = null;
    private PlayerCharacter playerCharacter;
    private SkillSetupInfo skillSetupInfo;
    private float mainSkillCooldawn;
    private float secondSkillCooldawn;
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

    public void Setup(LevelController levelController)
    {
        this.playerCharacter = levelController.Player;

        skillSetupInfo = new SkillSetupInfo(playerCharacter, gameObject.transform);

        mainSkill = Instantiate(mainSkillRacePrefab);
        secondSkill = Instantiate(secondSkillRacePrefab);

        mainSkill.SetupSkill(skillSetupInfo);
        secondSkill.SetupSkill(skillSetupInfo);

        ammoSlot = levelController.GameUiManager.CharacterPanels.EquipmentWeaponSkillsPanel.EquipmentPanel.AmmoSlot;

        playerCharacter.Stats.OnDamage += StopSkillCasting;
        playerCharacter.Stats.OnDeath += StopSkillCasting;

        levelController.GameUiManager.OnTogglePanels += BindSkillToMouse;

        GameManager.Instance.InputManager.InputController.Player.CancelAction.performed += ctx => StopSkillCasting();
        GameManager.Instance.InputManager.InputController.Player.MainSkill.performed += ctx => UseMainSkill();
        GameManager.Instance.InputManager.InputController.Player.SecondSkill.performed += ctx => UseSecondSkill();

        BindSkillToMouse(true);
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

    public void BindSkillToMouse(bool bind)
    {
        Debug.Log("Use mouse to skill: " + bind);
        if (bind)
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

    public void SetTimeBlockAction(float timeBlock, bool resetTime = false)
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

    public void UseMainSkill()
    {
        if (CanUseSkill(mainSkill) && mainSkillCooldawn <= 0)
        {
            castSkill = StartCoroutine(SkillCasting(mainSkill));
            mainSkillCooldawn = mainSkill.SkillCooldawn;
            playerCharacter.MoveController.SetTimeBlockMove(mainSkill.TimeToBlockCasterMove);
            SetTimeBlockAction(mainSkill.TimeToBlockCasterAction);
            OnMainSkillUse();
        }
    }

    public void UseSecondSkill()
    {
        if (CanUseSkill(secondSkill) && secondSkillCooldawn <= 0)
        {
            castSkill = StartCoroutine(SkillCasting(secondSkill));
            secondSkillCooldawn = secondSkill.SkillCooldawn;
            playerCharacter.MoveController.SetTimeBlockMove(secondSkill.TimeToBlockCasterMove);
            SetTimeBlockAction(secondSkill.TimeToBlockCasterAction);
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

    public void StopSkillCasting()
    {
        if(castSkill != null)
        {
            Debug.Log("StopCasting");
            StopCoroutine(castSkill);
            SetTimeBlockAction(0, true);
            playerCharacter.MoveController.SetTimeBlockMove(0, true);
        }
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
