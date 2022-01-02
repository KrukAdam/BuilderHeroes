﻿using System.Text;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Localization;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Game/Items/Item")]
public class Item : ScriptableObject
{
	[SerializeField] string id;
	public string ID { get { return id; } }
	public LocalizedString ItemName;
	public LocalizedString ItemType;
	public Sprite Icon;
	[Range(1,999)]
	public int MaximumStacks = 1;

	protected static readonly StringBuilder sb = new StringBuilder();


#if UNITY_EDITOR
    protected virtual void OnValidate()
    {
        string path = AssetDatabase.GetAssetPath(this);
        id = AssetDatabase.AssetPathToGUID(path);
    }
#endif

    public virtual Item GetCopy()
	{
		return this;
	}

	public virtual void Destroy()
	{

	}

	public virtual string GetDescription()
	{
		return "";
	}

}
