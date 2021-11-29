using UnityEngine;
using Kryz.CharacterStats;
using System.Collections.Generic;
using System;

public class StatsPanel : MonoBehaviour
{

    [SerializeField] private StatDisplay statDisplayPrefab = null;
    [SerializeField] private Transform parentStatDisplay = null;

    private List<StatDisplay> statDisplays = new List<StatDisplay>();
    private Stats stats;

    public void SetupPanel(LocalController levelController)
    {
        stats = levelController.Player.Stats;
        SetStats();
        SetupEvents(levelController);
    }

    public void SetStats()
    {
        for (int i = 0; i < stats.AllStats.Count; i++)
        {
            StatDisplay display = Instantiate(statDisplayPrefab, parentStatDisplay);
            display.Setup(stats.AllStats[i]);

            statDisplays.Add(display);
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
