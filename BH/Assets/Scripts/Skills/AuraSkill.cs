using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Aura Skill")]
public class AuraSkill : RangeSkill
{
    [Space]
    [Header("Settings for aura skill")]
    [SerializeField] private bool isBuff = true;        //If is true is buff, if false debuff
    [SerializeField] private bool isRangeSkill = false;
    [SerializeField] private bool takeDamage = false;
    [SerializeField] private List<AuraData> auras = new List<AuraData>();

    public override void UseSkill()
    {
        if (isRangeSkill)
        {
            base.UseSkill();
        }
        else
        {
            MaleeSkillEffect();
        }
    }

    public override void RangeSkillEffect(Transform effectPos)
    {
        base.RangeSkillEffect(effectPos);
    }

    public void MaleeSkillEffect()
    {
        UseStats();   //Dont use base method "UseSkill". Base method is range skill
        Debug.Log("Use skill: " + skillName);
        Collider2D[] targets;
        if (isBuff)
        {
            targets = GetTargets(SkillSetupInfo.AllyLayersMask);
        }
        else
        {
            targets = GetTargets(SkillSetupInfo.EnemyLayerMask);
        }
        foreach (var target in targets)
        {
            if (target.TryGetComponent(out Character character))
            {
                if (isBuff || character != SkillSetupInfo.SkillOwner && !isBuff)
                {
                    bool push = true;
                    foreach (var offenseStatType in offenseStatsType)
                    {
                        if (takeDamage)
                        {
                            if (SkillOwner(character)) break;
                            character.Stats.TakeDamage(offenseStatType);
                        }

                        if (push)
                        {
                            push = false;
                            PushTarget(character);
                        }
                    }

                    foreach (var aura in auras)
                    {
                        aura.ExecuteEffect(this, character.Stats);
                        Debug.Log("Aura was used");
                    }
                    if (singleTarget) return;
                }

            }
        }
    }
}
