using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraSkill : RangeSkill
{
    [Space]
    [Header("Settings for aura skill")]
    [SerializeField] private bool isRangeSkill = false;
    [SerializeField] private bool takeDamage = false;
    [SerializeField] private bool isBuff = true;        //If is true is buff, if false debuff

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

        foreach (var target in GetTargets())
        {
            if (target.TryGetComponent(out Character character))
            {
                bool push = true;
                foreach (var offenseStatType in offenseStatsType)
                {
                    if (isBuff)
                    {
                        SkillSetupInfo.UserStats.TakeDamage(offenseStatType);
                    }
                    else
                    {
                        character.Stats.TakeDamage(offenseStatType);
                    }

                    if(takeDamage) character.Stats.TakeDamage(offenseStatType);

                    if (push)
                    {
                        push = false;
                        PushTarget(character);
                    }
                    if (singleTarget) return;
                }
            }
        }
    }
}
