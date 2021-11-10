using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour, IDamage
{
    public event Action OnDamage = delegate { };
    public event Action OnDeath = delegate { };
    
	public List<CharacterStat> AllStats { get => allStats; }

    [SerializeField] private List<CharacterStat> allStats = null;    //Init on start, SF to see in inspector

	private CharacterStatsData characterStatsData = null;
    private int minDamage;


	public void Init()
    {
		characterStatsData = Instantiate(GameManager.Instance.CharacterStatsData);
		allStats = characterStatsData.AllStats;

        minDamage = Constant.MinDamage;
    }

    public void TakeDamage(SkillOffenseStat skillOffenseStat)
    {
        float damage = skillOffenseStat.GetDamage() - GetStat(skillOffenseStat.GetDefenseStatType()).BaseValue.Value;

        if (damage < 0) damage = minDamage;

        OnDamage();
        GetStat(skillOffenseStat.DamagedStatType).BaseValue.Value -= damage;
        if(GetStat(skillOffenseStat.DamagedStatType).BaseValue.Value <= 0 && skillOffenseStat.DamagedStatType == EStatsTypes.Health)
        {
            OnDeath();
            Death();
        }
        Debug.Log("Damage: " + damage+" name target: " +gameObject.name);
    }

    public bool CheckCanTakeDamage(int valueToUse)
    {
        throw new System.NotImplementedException();
    }

    public void AuraRefresh()
    {

    }

    public void Death()
    {
        Debug.Log("Death " + gameObject.name);
    }


    public CharacterStat GetStat(EStatsTypes statsType)
    {
        if (statsType == EStatsTypes.None) return null;

        foreach (var stat in AllStats)
        {
            if (stat.StatType == statsType)
            {
                return stat;
            }
        }

        Debug.LogError("No have stats type: " + statsType + " on character stats!");
        return null;
    }
}
