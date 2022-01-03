using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(menuName = "Skills/Aura Skill")]
public class AuraSkill : RangeSkill
{
    public EAuraSkillsType AuraType { get => auraType; }
    public bool IsBuff { get => isBuff; }
    public bool UseOnlyOnCaster { get => useOnlyOnCaster; }
    public LocalizedString AuraTypeName { get => auraTypeName; }
    public List<AuraData> Auras { get => auras; }

    [Space]
    [Header("Settings for aura skill")]
    [SerializeField] private bool isBuff = true;        //If is true is buff, if false debuff
    [SerializeField] private bool isRangeSkill = false;
    [SerializeField] private bool takeDamage = false;
    [SerializeField] private bool useOnlyOnCaster = false;
    [SerializeField] private EAuraSkillsType auraType = EAuraSkillsType.None;
    [SerializeField] private LocalizedString auraTypeName = null;
    [SerializeField] private List<AuraData> auras = new List<AuraData>();

    public override void UseSkill()
    {
        if (isRangeSkill)
        {
            GameObject missile = Instantiate(missilePrefab.gameObject, skillSetupInfo.UserTransform);
            SkillMissile sm = missile.GetComponent<SkillMissile>();
            sm.SetupMissile(this, true);
        }
        else
        {
            MaleeSkillEffect();
        }
    }

    public override void RangeSkillEffect(Transform effectPos)
    {
        Debug.Log("Use skill: " + skillName);
        Collider2D[] targets;
        if (isBuff)
        {
            targets = GetRangeTargets(effectPos, SkillSetupInfo.AllyLayersMask);
        }
        else
        {
            targets = GetRangeTargets(effectPos, SkillSetupInfo.EnemyLayerMask);
        }
        foreach (var target in targets)
        {
            if (target.TryGetComponent(out Character character))
            {
                if (isBuff || character != SkillSetupInfo.SkillOwner && !isBuff)
                {
                    if (!useOnlyOnCaster || useOnlyOnCaster && SkillOwner(character))
                    {
                        bool push = true;
                        foreach (var offenseStatType in offenseStats)
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
                    if (!useOnlyOnCaster || useOnlyOnCaster && SkillOwner(character))
                    {
                        bool push = true;
                        foreach (var offenseStatType in offenseStats)
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
}
