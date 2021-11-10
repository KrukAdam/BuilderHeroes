using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Skills/Malee Skill")]
public class MaleeSkill : Skill
{

    public override void UseSkill()
    {
        base.UseSkill();


        foreach (var target in GetTargets())
        {
            if (target.TryGetComponent(out Character character))
            {
                bool push = true;
                foreach (var offenseStatType in offenseStatsType)
                {
                    character.Stats.TakeDamage(offenseStatType);
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
