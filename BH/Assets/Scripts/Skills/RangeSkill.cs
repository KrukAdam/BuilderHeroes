using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Skills/Range Skill")]
public class RangeSkill : Skill
{
    public EItemAmmoType AmmoType { get => ammoType; }
    public int AmmoUsePerShot { get => ammoUsePerShot; }
    public float MissileSpeed { get => missileSpeed; }
    public float MissileRange { get => missileRange; }
    public Sprite MissileSprite { get => missileSprite; }

    [Space]
    [SerializeField] protected EItemAmmoType ammoType = EItemAmmoType.None;
    [SerializeField] protected int ammoUsePerShot = 1;
    [SerializeField] protected float missileSpeed = 10;
    [SerializeField] protected float missileRange = 5;
    [SerializeField] protected Sprite missileSprite = null;
    [SerializeField] protected SkillMissile missilePrefab = null;   //Kind of missile collider

    public override void UseSkill()
    {
        base.UseSkill();
        GameObject missile = Instantiate(missilePrefab.gameObject, skillSetupInfo.UserTransform);
        SkillMissile sm = missile.GetComponent<SkillMissile>();
        sm.SetupMissile(this);
    }

    public virtual void RangeSkillEffect(Transform effectPos)
    {

        foreach (var target in GetRangeTargets(effectPos, SkillSetupInfo.EnemyLayerMask))
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

    protected Collider2D[] GetRangeTargets(Transform effectPos, LayerMask targetLayer)
    {
        return Physics2D.OverlapCircleAll(effectPos.position, skillRange, targetLayer);
    }
}
