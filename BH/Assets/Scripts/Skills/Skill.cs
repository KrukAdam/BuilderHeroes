using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Skill : ScriptableObject
{
    public float SkillCooldawn { get => skillCooldawn; }
    public float TimeToBlockCasterMove { get => timeToBlockCasterMove; }
    public float TimeToBlockCasterAction { get => timeToBlockCasterAction; }
    public WaitForSeconds WaitForCastTime { get => waitForCastTime; }
    public SkillSetupInfo SkillSetupInfo { get => skillSetupInfo; }
    public Sprite SkillSprite { get => skillSprite; }

    [SerializeField] protected Sprite skillSprite = null;
    [SerializeField] protected string skillName = null;
    [SerializeField] protected bool singleTarget = false;
    [SerializeField] protected LayerMask targetLayer;
    [SerializeField] protected ESkillUsePositionType skillUsePositionType = ESkillUsePositionType.InteractionPoint;
    [SerializeField] protected float skillRange = 1f;
    [SerializeField] protected float skillCooldawn = 1f;
    [SerializeField] protected float skillCastTime = 0f;
    [SerializeField] protected float timeToBlockCasterMove = 0f;
    [Tooltip("Set min skill cast time, You cant cast more than one skill")]
    [SerializeField] protected float timeToBlockCasterAction = 0f;
    [SerializeField] protected float pushPower = 0f;
    [Header("Stat used to skill use")]
    [SerializeField] private SkillStatUsed[] skillStatsUsed = null;
    [Space]
    [SerializeField] protected SkillOffenseStat[] offenseStatsType;

    protected Transform skillUsePosition;
    protected SkillSetupInfo skillSetupInfo;
    protected float pushPowerMultiplier;
    protected WaitForSeconds waitForCastTime;

    public virtual void SetupSkill(SkillSetupInfo skillSetupInfo)
    {
        this.skillSetupInfo = skillSetupInfo;

        pushPowerMultiplier = Constant.PushPowerMultiplier;
        waitForCastTime = new WaitForSeconds(skillCastTime);

        if (TimeToBlockCasterAction < skillCastTime) timeToBlockCasterAction = skillCastTime;    //You cant cast more than one skill

        foreach (var offenseStatType in offenseStatsType)
        {
            offenseStatType.SetOffenseStat(skillSetupInfo.UserStats.GetStat(offenseStatType.StatType));
        }
    }

    public virtual void UseSkill()
    {
        UseStats();
        Debug.Log("Use skill: " + skillName);
    }

    public bool CanUse()
    {
        if (skillStatsUsed.Length <= 0) return true;   //No needed any stats

        int usedStat = 0;
        foreach (var statUsed in skillStatsUsed)
        {
            if (skillSetupInfo.UserStats.GetStat(statUsed.StatType).BaseValue.Value >= statUsed.StatValue) usedStat++;
        }
        if (usedStat == skillStatsUsed.Length) return true;

        Debug.Log("No have energy to use this skill");
        return false;
    }

    protected void PushTarget(Character targetCharacter)
    {
        if (pushPower <= 0) return;

        targetCharacter.MoveController.SetTimeBlockMove(1f);
        targetCharacter.CharacterRigidbody.AddForce(GetPushVector(targetCharacter) * pushPower * pushPowerMultiplier);
    }

    protected void UseStats()
    {
        foreach (var statUsed in skillStatsUsed)
        {
            skillSetupInfo.UserStats.GetStat(statUsed.StatType).BaseValue.Value -= statUsed.StatValue;
        }
    }

    protected Vector3 GetSkillUsePosition()
    {
        switch (skillUsePositionType)
        {
            case ESkillUsePositionType.None:
                Debug.LogError("No position use type on Skill!");
                return Vector3.zero;
            case ESkillUsePositionType.Character:
                return skillSetupInfo.UserTransform.position;
            case ESkillUsePositionType.MousePosition:
                Debug.LogError("Wrong position use type on skill!");
                return Vector3.zero;
            case ESkillUsePositionType.InteractionPoint:
                return skillSetupInfo.InteractionPointer.position;
            default:
                Debug.LogError("Wrong position use type on skill!");
                return Vector3.zero;
        }
    }

    protected Collider2D[] GetTargets()
    {
       return Physics2D.OverlapCircleAll(GetSkillUsePosition(), skillRange, targetLayer);
    }

    protected Vector2 GetPushVector(Character targetCharacter)
    {
        Vector2 pushVector = skillSetupInfo.InteractionPointer.localPosition;

        if(pushVector.x > 0)
        {
            if(skillSetupInfo.UserTransform.position.x > targetCharacter.transform.position.x)
            {
                pushVector.x = -1;
                return pushVector;
            }
        }
        if (pushVector.x < 0)
        {
            if (skillSetupInfo.UserTransform.position.x < targetCharacter.transform.position.x)
            {
                pushVector.x = 1;
                return pushVector;
            }
        }
        if (pushVector.y > 0)
        {
            if (skillSetupInfo.UserTransform.position.y > targetCharacter.transform.position.y)
            {
                pushVector.y = -1;
                return pushVector;
            }
        }
        if (pushVector.y < 0)
        {
            if (skillSetupInfo.UserTransform.position.y < targetCharacter.transform.position.y)
            {
                pushVector.y = 1;
                return pushVector;
            }
        }

        return pushVector;
    }
}
