using UnityEngine;
using System.Collections.Generic;

public class StatsPanel : MonoBehaviour
{

    [SerializeField] private StatDisplay statDisplayPrefab = null;
    [SerializeField] private Transform parentStatDisplay = null;

    private List<StatDisplay> statDisplays = new List<StatDisplay>();
    private Stats stats;

    public void SetupPanel(LocalController localController)
    {
        stats = localController.Player.Stats;
        SetStats(localController);
        SetupEvents(localController);
    }

    public void SetStats(LocalController localController)
    {
        for (int i = 0; i < stats.AllStats.Count; i++)
        {
            StatDisplay display = Instantiate(statDisplayPrefab, parentStatDisplay);
            display.Setup(stats.AllStats[i], localController);

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
