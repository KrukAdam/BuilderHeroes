using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Skills/Movement Skill")]
public class MovementSkill : Skill
{
    [Space]
    [Header("Movement skill settings")]
    [SerializeField] private float casterMoveRange = 0;

    public override void UseSkill()
    {
        base.UseSkill();
        CasterMove();

        foreach (var target in GetTargets(SkillSetupInfo.EnemyLayerMask))
        {
            if (target.TryGetComponent(out Character character))
            {
                bool push = true;
                foreach (var offenseStatType in offenseStatsType)
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
        }
    }

    private void CasterMove()
    {
        SkillSetupInfo.SkillOwner.MoveController.StartMoveToTarget(GetSkillUsePosition(), casterMoveRange);
    }
}
