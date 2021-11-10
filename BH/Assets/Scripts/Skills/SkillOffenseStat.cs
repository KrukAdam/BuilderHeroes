using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SkillOffenseStat
{
    public EStatsTypes StatType;
    public ESkillOffenseStatType SkillOffenseStatType;
    [Header("Set min and max damage if you use Constant Value")]
    public float MinDamage;
    public float MaxDamage;
    [Header("Set min and max percent damage if you use Percent Value")]
    public float MinPercentDamage;
    public float MaxPercentDamage;
    [Header("Damage in stat (health, mana, stamina...")]
    public EStatsTypes DamagedStatType = EStatsTypes.Health;

    private CharacterStat stat;

    public void SetOffenseStat(CharacterStat stat)
    {
        this.stat = stat;
    }

    public EStatsTypes GetDefenseStatType()
    {
        return stat.StatDefenseType;
    }

    public float GetDamage()
    {
        switch (SkillOffenseStatType)
        {
            case ESkillOffenseStatType.None:
                Debug.LogWarning("None type of damage in skill");
                return 0f;
            case ESkillOffenseStatType.StatValue:
                return UnityEngine.Random.Range(stat.MinValue.Value, stat.MaxValue.Value);
            case ESkillOffenseStatType.ConstantValue:
                return UnityEngine.Random.Range(MinDamage, MaxDamage);
            case ESkillOffenseStatType.StatAndConstantValue:
                return UnityEngine.Random.Range(stat.MinValue.Value, stat.MaxValue.Value) + UnityEngine.Random.Range(MinDamage, MaxDamage);
            case ESkillOffenseStatType.PercentageStatValue:
                float percentStat = UnityEngine.Random.Range(MinPercentDamage, MaxPercentDamage) / 100; 
                float damageStat = UnityEngine.Random.Range(stat.MinValue.Value, stat.MaxValue.Value) * percentStat;
                return damageStat;
            case ESkillOffenseStatType.PercenAndConstantValue:
                float percentConst = UnityEngine.Random.Range(MinPercentDamage, MaxPercentDamage) / 100;
                float damageConst = UnityEngine.Random.Range(MinDamage, MaxDamage) * percentConst;
                return damageConst;
            default:
                Debug.LogError("None case type of damage in skill");
                return 0f;
        }
    }
}
