using UnityEngine;
using Kryz.CharacterStats;
using System.Collections.Generic;
using System;

public class StatsPanel : MonoBehaviour
{

    [SerializeField] private GameObject statDisplayPrefab = null;
    [SerializeField] private Transform CtxStatDisplayParent = null;

	private List<StatDisplay> statDisplays = new List<StatDisplay>();
    private Stats stats;

	public void SetupPanel(LevelController levelController)
    {
        stats = levelController.Player.Stats;
        SetStats();
        UpdateStatValues();
        SetupEvents(levelController);
    }

    public void SetStats()
	{
		for (int i = 0; i < this.stats.AllStats.Count; i++)
		{
            if (this.stats.AllStats[i].StatUseValues == EStatsValueUse.OnlyBaseValue)
            {
                GameObject displayObj = Instantiate(statDisplayPrefab, CtxStatDisplayParent);
				displayObj.transform.SetParent(CtxStatDisplayParent);
                StatDisplay display = displayObj.GetComponent<StatDisplay>();
				display.Name = this.stats.AllStats[i].StatName;
				display.Stat = this.stats.AllStats[i].BaseValue;

                statDisplays.Add(display);
			}
            else if(this.stats.AllStats[i].StatUseValues == EStatsValueUse.MinAndMaxValue)
            {
                GameObject displayObjMin = Instantiate(statDisplayPrefab, CtxStatDisplayParent);
				displayObjMin.transform.SetParent(CtxStatDisplayParent);
                StatDisplay displayMin = displayObjMin.GetComponent<StatDisplay>();
				displayMin.Name = "Min. "+ this.stats.AllStats[i].StatName;
				displayMin.Stat = this.stats.AllStats[i].MinValue;

                GameObject displayObjMax = Instantiate(statDisplayPrefab, CtxStatDisplayParent);
				displayObjMax.transform.SetParent(CtxStatDisplayParent);
                StatDisplay displayMax = displayObjMax.GetComponent<StatDisplay>();
				displayMax.Name = "Max. "+ this.stats.AllStats[i].StatName;
				displayMax.Stat = this.stats.AllStats[i].MaxValue;

                statDisplays.Add(displayMin);
                statDisplays.Add(displayMax);
            }
            else if(this.stats.AllStats[i].StatUseValues == EStatsValueUse.BaseAndMaxValue)
            {
                GameObject displayObjBaseValue = Instantiate(statDisplayPrefab, CtxStatDisplayParent);
				displayObjBaseValue.transform.SetParent(CtxStatDisplayParent);
                StatDisplay displayBaseValue = displayObjBaseValue.GetComponent<StatDisplay>();
				displayBaseValue.Name = "Current " + this.stats.AllStats[i].StatName;
				displayBaseValue.Stat = this.stats.AllStats[i].BaseValue;

                GameObject displayObjMax = Instantiate(statDisplayPrefab, CtxStatDisplayParent);
				displayObjMax.transform.SetParent(CtxStatDisplayParent);
                StatDisplay displayMax = displayObjMax.GetComponent<StatDisplay>();
				displayMax.Name = "Max. " + this.stats.AllStats[i].StatName;
				displayMax.Stat = this.stats.AllStats[i].MaxValue;

                statDisplays.Add(displayBaseValue);
                statDisplays.Add(displayMax);
			}
		}
	}

	public void UpdateStatValues()
	{
        foreach (var stat in statDisplays)
        {
			stat.UpdateStatValue();
        }
	}

    private void SetupEvents(LevelController levelController)
    {
        levelController.LocalManagers.EquipmentManager.OnEquipmentChange += UpdateStatValues;
    }
}
