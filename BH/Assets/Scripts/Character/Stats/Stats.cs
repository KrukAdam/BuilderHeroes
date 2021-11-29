using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour, IDamage
{
    public event Action OnStatsChange = delegate { };
    public event Action OnDamage = delegate { };
    public event Action OnDeath = delegate { };
    
	public List<CharacterStat> AllStats { get => allStats; }

    [SerializeField] private StatsData statsData = null;   //TODO gracz wybiera rase i dostaje odpowiednie staty poczatkowe
    [SerializeField] private List<CharacterStat> allStats = null;    //Init on start, SF to see in inspector

	private StatsData characterStatsData = null;
    private int minDamage;


	public void Init()
    {
		characterStatsData = Instantiate(statsData);
        characterStatsData.Setup();
		allStats = characterStatsData.Stats;

        minDamage = Constant.MinDamage;
    }

    public void TakeDamage(SkillOffenseStat skillOffenseStat)
    {
        float damage = skillOffenseStat.GetDamage() - GetStat(skillOffenseStat.GetDefenseStatType()).BaseValue.Value;

        if (damage < 0) damage = minDamage;


        GetStat(skillOffenseStat.DamagedStatType).BaseValue.Value -= damage;
        OnDamage();
        OnStatsChange();
        if (GetStat(skillOffenseStat.DamagedStatType).BaseValue.Value <= 0 && skillOffenseStat.DamagedStatType == EStatsTypes.Health)
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
        Refresh();
    }

    public void Refresh()
    {
        OnStatsChange();
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
