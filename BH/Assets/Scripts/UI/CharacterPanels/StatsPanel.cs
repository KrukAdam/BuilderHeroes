using UnityEngine;
using Kryz.CharacterStats;
using System.Collections.Generic;

public class StatsPanel : MonoBehaviour
{
    [SerializeField] private GameObject statDisplayPrefab = null;
    [SerializeField] private Transform CtxStatDisplayParent = null;

	private List<StatDisplay> statDisplays = new List<StatDisplay>();
    private Stats stats = null;

    public void SetStats(Stats charStats)
	{
		stats = charStats;

		for (int i = 0; i < stats.AllStats.Count; i++)
		{
            if (stats.AllStats[i].StatUseValues == EStatsValueUse.OnlyBaseValue)
            {
				GameObject displayObj = Instantiate(statDisplayPrefab, CtxStatDisplayParent);
				displayObj.transform.SetParent(CtxStatDisplayParent);
				StatDisplay display = displayObj.GetComponent<StatDisplay>();
				display.Name = stats.AllStats[i].StatName;
				display.Stat = stats.AllStats[i].BaseValue;

				statDisplays.Add(display);
			}
            else if(stats.AllStats[i].StatUseValues == EStatsValueUse.MinAndMaxValue)
            {
				GameObject displayObjMin = Instantiate(statDisplayPrefab, CtxStatDisplayParent);
				displayObjMin.transform.SetParent(CtxStatDisplayParent);
				StatDisplay displayMin = displayObjMin.GetComponent<StatDisplay>();
				displayMin.Name = "Min. "+stats.AllStats[i].StatName;
				displayMin.Stat = stats.AllStats[i].MinValue;

				GameObject displayObjMax = Instantiate(statDisplayPrefab, CtxStatDisplayParent);
				displayObjMax.transform.SetParent(CtxStatDisplayParent);
				StatDisplay displayMax = displayObjMax.GetComponent<StatDisplay>();
				displayMax.Name = "Max. "+stats.AllStats[i].StatName;
				displayMax.Stat = stats.AllStats[i].MaxValue;

				statDisplays.Add(displayMin);
				statDisplays.Add(displayMax);
            }
            else if(stats.AllStats[i].StatUseValues == EStatsValueUse.BaseAndMaxValue)
            {
				GameObject displayObjBaseValue = Instantiate(statDisplayPrefab, CtxStatDisplayParent);
				displayObjBaseValue.transform.SetParent(CtxStatDisplayParent);
				StatDisplay displayBaseValue = displayObjBaseValue.GetComponent<StatDisplay>();
				displayBaseValue.Name = "Current " + stats.AllStats[i].StatName;
				displayBaseValue.Stat = stats.AllStats[i].BaseValue;

				GameObject displayObjMax = Instantiate(statDisplayPrefab, CtxStatDisplayParent);
				displayObjMax.transform.SetParent(CtxStatDisplayParent);
				StatDisplay displayMax = displayObjMax.GetComponent<StatDisplay>();
				displayMax.Name = "Max. " + stats.AllStats[i].StatName;
				displayMax.Stat = stats.AllStats[i].MaxValue;

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
}
