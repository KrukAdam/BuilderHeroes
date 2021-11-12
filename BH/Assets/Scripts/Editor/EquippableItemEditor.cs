using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EquippableItem))]
public class EquippableItemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EquippableItem item = (EquippableItem)target;

        if (GUILayout.Button("Set empty Stats"))
        {
            item.SetEmptyStats();
        }
    }
}

[CustomEditor(typeof(AmmoItem))]
public class AmmoItemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        AmmoItem item = (AmmoItem)target;

        if (GUILayout.Button("Set empty Stats"))
        {
            item.SetEmptyStats();
        }
    }
}

[CustomEditor(typeof(ToolItem))]
public class ToolItemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ToolItem item = (ToolItem)target;

        if (GUILayout.Button("Set empty Stats"))
        {
            item.SetEmptyStats();
        }
    }
}
