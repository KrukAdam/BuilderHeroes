using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Range Skill")]
public class RangeSkill : Skill
{
    public EAmmoType AmmoType { get => ammoType; }
    public int AmmoUsePerShot { get => ammoUsePerShot; }
    public float MissileSpeed { get => missileSpeed; }
    public float MissileRange { get => missileRange; }
    public Sprite MissileSprite { get => missileSprite; }

    [Space]
    [SerializeField] private EAmmoType ammoType = EAmmoType.None;
    [SerializeField] private int ammoUsePerShot = 1;
    [SerializeField] private float missileSpeed = 10;
    [SerializeField] private float missileRange = 5;
    [SerializeField] private Sprite missileSprite = null;
    [SerializeField] private SkillMissile missilePrefab = null;   //Kind of missile collider

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
                foreach (var offenseStatType in offenseStatsType)
                {
                    if (character == SkillSetupInfo.SkillOwner) break;

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

    protected Collider2D[] GetRangeTargets(Transform effectPos, LayerMask targetLayer)
    {
        return Physics2D.OverlapCircleAll(effectPos.position, skillRange, targetLayer);
    }
}
