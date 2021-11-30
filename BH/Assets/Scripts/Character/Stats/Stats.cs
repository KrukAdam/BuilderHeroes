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
    private Dictionary<EStatsTypes, CharacterStat> statsDictionary = new Dictionary<EStatsTypes, CharacterStat>();


	public void Init()
    {
		characterStatsData = Instantiate(statsData);
        characterStatsData.Setup();
		allStats = characterStatsData.Stats;

        minDamage = Constant.MinDamage;

        SetupStatsDictionary();
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

        if (statsDictionary.ContainsKey(statsType))
        {
            return statsDictionary[statsType];
        }

        Debug.LogError("No have stats type: " + statsType + " on character stats!");
        return null;
    }

    public float GetMoveSpeed()
    {
        float speed = statsDictionary[EStatsTypes.MoveSpeed].BaseValue.Value;
        if (speed <= 0) speed = 1f;

        speed = speed / 25;

        return speed;
    }

    public float GetAttackSpeed()
    {
        float speed = statsDictionary[EStatsTypes.AttackSpeed].BaseValue.Value;
        if (speed <= 0) speed = 1f;

        speed = speed / 100;

        return speed;
    }

    private void SetupStatsDictionary()
    {
        foreach (var stat in allStats)
        {
            statsDictionary.Add(stat.StatType, stat);
        }
    }
}
