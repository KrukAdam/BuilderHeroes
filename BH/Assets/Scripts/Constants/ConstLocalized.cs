using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(menuName = "Game/Constants/Localized")]
public class ConstLocalized : ScriptableObject
{
    //Stats
    public LocalizedString StatMinValue;
    public LocalizedString StatMaxValue;

    //Time
    public LocalizedString ForTime;
    public LocalizedString Seconds;

    //Skills
    public LocalizedString SkillTypeMelee;
    public LocalizedString SkillTypeRange;
    public LocalizedString SkillTypeAura;
    public LocalizedString SkillTypeMovement;
    public LocalizedString SkillSingleTarget;
    public LocalizedString SkillMultiTargets;
    public LocalizedString SkillCastTime;
    public LocalizedString SkillPushPower;
    public LocalizedString SkillAmmoArrows;
    public LocalizedString SkillAmmoBolts;
    public LocalizedString SkillAmmoRuns;
    public LocalizedString SkillAmmoPerShot;
    public LocalizedString SkillMissileRange;
    public LocalizedString SkillBuff;
    public LocalizedString SkillDebuff;
    public LocalizedString SkillUseOnlyOnCaster;
    public LocalizedString SkillUseOnAround;
    public LocalizedString SkillAuraType;
    public LocalizedString SkillHitEnemy;
    public LocalizedString SkillDontHitEnemy;
    public LocalizedString SkillMovementRange;
    public LocalizedString SkillUseStat;
    public LocalizedString SkillOffenseStat;
    public LocalizedString SkillBuffEffect;

    //Simple
    public LocalizedString NEW;

    //Items
    public LocalizedString DamageRawMaterial;
    public LocalizedString ItemDurability;
}
