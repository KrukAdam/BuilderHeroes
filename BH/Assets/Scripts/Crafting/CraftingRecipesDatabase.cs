using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/Recipes Database")]
public class CraftingRecipesDatabase : ScriptableObject
{
    public CraftingRecipeData[] Datas { get => datas; }
    public List<CraftingRecipe> CraftingRecipes { get => craftingRecipes; }

    [SerializeField] private CraftingRecipeData[] datas;

    private List<CraftingRecipe> craftingRecipes = new List<CraftingRecipe>();

#if UNITY_EDITOR
    private void OnValidate()
    {
        LoadItems();
    }

    private void OnEnable()
    {
        EditorApplication.projectChanged -= LoadItems;
        EditorApplication.projectChanged += LoadItems;
    }

    private void OnDisable()
    {
        EditorApplication.projectChanged -= LoadItems;
    }

    private void LoadItems()
    {
        Debug.Log("Load Crafting Recipes Data");
        datas = FindAssetsByType<CraftingRecipeData>("Assets/Data/CraftingRecipes");
        RecipesListSet();
    }

    // Slightly modified version of this answer: http://answers.unity.com/answers/1216386/view.html
    public static T[] FindAssetsByType<T>(params string[] folders) where T : Object
    {
        string type = typeof(T).Name;

        string[] guids;
        if (folders == null || folders.Length == 0)
        {
            guids = AssetDatabase.FindAssets("t:" + type);
        }
        else
        {
            guids = AssetDatabase.FindAssets("t:" + type, folders);
        }

        T[] assets = new T[guids.Length];

        for (int i = 0; i < guids.Length; i++)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
            assets[i] = AssetDatabase.LoadAssetAtPath<T>(assetPath);
        }
        return assets;
    }

    private void RecipesListSet()
    {
        craftingRecipes.Clear();
        foreach (var data in datas)
        {
            foreach (var recipe in data.CraftingRecipes)
            {
                craftingRecipes.Add(recipe);
            }
        }
    }
#endif
}
