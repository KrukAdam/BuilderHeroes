using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemEquippable))]
public class EquippableItemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ItemEquippable item = (ItemEquippable)target;

        if (GUILayout.Button("Set empty Stats"))
        {
            item.SetEmptyStats();
        }
    }
}

[CustomEditor(typeof(ItemAmmo))]
public class AmmoItemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ItemAmmo item = (ItemAmmo)target;

        if (GUILayout.Button("Set empty Stats"))
        {
            item.SetEmptyStats();
        }
    }
}

[CustomEditor(typeof(ItemTool))]
public class ToolItemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ItemTool item = (ItemTool)target;

        if (GUILayout.Button("Set empty Stats"))
        {
            item.SetEmptyStats();
        }
    }
}
