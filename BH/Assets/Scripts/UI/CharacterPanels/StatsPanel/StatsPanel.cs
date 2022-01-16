using UnityEngine;
using System.Collections.Generic;

public class StatsPanel : MonoBehaviour
{

    [SerializeField] private StatDisplay statDisplayPrefab = null;
    [SerializeField] private Transform parentStatDisplay = null;

    private List<StatDisplay> statDisplays = new List<StatDisplay>();
    private Stats stats;

    //Use on Gameplay
    public void SetupPanel(LocalController localController)
    {
        stats = localController.Player.Stats;
        SetStats(localController.GameUiManager.TooltipsPanels);
        SetupEvents(localController);
    }

    //Use on MainMenu scene
    public void SetupPanel(TooltipsPanels tooltipsPanels)
    {
        stats = GameManager.Instance.CharacterCreator.Stats;
        SetStats(tooltipsPanels);
    }

    public void SetStats(TooltipsPanels tooltipsPanels)
    {
        for (int i = 0; i < stats.AllStats.Count; i++)
        {
            StatDisplay display = Instantiate(statDisplayPrefab, parentStatDisplay);
            display.Setup(stats.AllStats[i], tooltipsPanels);

            statDisplays.Add(display);
        }
    }

    public void UpdateStats()
    {
        if (!stats) return;

        for (int i = 0; i < stats.AllStats.Count; i++)
        {
            if (statDisplays.Count > i)
            {
                statDisplays[i].SetupNewStat(stats.AllStats[i]);
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

    private void SetupEvents(LocalController levelController)
    {
        levelController.LocalManagers.EquipmentManager.OnEquipmentChange += UpdateStatValues;
    }
}
