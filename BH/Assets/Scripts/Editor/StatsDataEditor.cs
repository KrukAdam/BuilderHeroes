using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StatsData))]
public class StatsDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        StatsData data = (StatsData)target;

        if (GUILayout.Button("Set empty Stats"))
        {
            data.SetBaseStats();
        }
    }
}
