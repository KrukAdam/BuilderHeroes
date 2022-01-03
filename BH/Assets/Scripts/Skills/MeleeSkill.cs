using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Game/Skills/Melee Skill")]
public class MeleeSkill : Skill
{

    public override void UseSkill()
    {
        base.UseSkill();

        foreach (var target in GetTargets(SkillSetupInfo.EnemyLayerMask))
        {
            if (target.TryGetComponent(out Character character))
            {
                bool push = true;
                foreach (var offenseStatType in offenseStats)
                {

                    if (SkillOwner(character)) break;

                    character.Stats.TakeDamage(offenseStatType);
                    if (push)
                    {
                        push = false;
                        PushTarget(character);
                    }
                }
                if (singleTarget && !SkillOwner(character)) return;
            }

            if (target.TryGetComponent(out Construction construction))
            {
                foreach (var offenseStatType in offenseStats)
                {
                    construction.Stats.TakeDamage(offenseStatType);
                }
                if (singleTarget) return;
            }
        }
    }
}
