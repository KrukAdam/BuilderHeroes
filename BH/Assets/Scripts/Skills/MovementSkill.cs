using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Game/Skills/Movement Skill")]
public class MovementSkill : Skill
{
    public float CasterMoveSpeed { get => casterMoveSpeed; }
    public float CasterMoveRange { get => casterMoveRange; }
    public bool HitingEnemy { get => hitingEnemy; }

    [Space]
    [Header("Movement skill settings")]
    [SerializeField] private float casterMoveSpeed = 1;
    [SerializeField] private float casterMoveRange = 1;
    [SerializeField] private bool hitingEnemy = true;

    public override void UseSkill()
    {
        base.UseSkill();
        CasterMove();
    }

    public void HitTargets()
    {
        if (!hitingEnemy) return;

        foreach (var target in GetTargets(SkillSetupInfo.EnemyLayerMask, skillSetupInfo.UserTransform.position))
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

    private void CasterMove()
    {
        SkillSetupInfo.SkillOwner.MoveController.StartMoveToTarget(GetSkillUsePosition(), this);
    }
}
