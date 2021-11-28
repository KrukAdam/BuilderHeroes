using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
    public CharacterStatsData CharacterStatsData { get => characterStatsData; }
    public CraftingRecipesDatabase CraftingRecipesDatabase { get => craftingRecipesDatabase; }
    public LayerMask AllyLayersMask { get => allyLayersMask; }
    public LayerMask EnemyLayersMask { get => enemyLayerMask; }
    public LayerMask AllyAILayersMask { get => allyLayersMask; }
    public LayerMask EnemyAILayersMask { get => enemyLayerMask; }
    public static GameManager Instance { get; private set; }
    public BHInputManager.InputManager InputManager { get; private set; }

    [SerializeField] private CharacterStatsData characterStatsData = null;
    [SerializeField] private CraftingRecipesDatabase craftingRecipesDatabase = null;
    [SerializeField] private LayerMask allyLayersMask = LayerMask.GetMask();
    [SerializeField] private LayerMask enemyLayerMask = LayerMask.GetMask();
    [SerializeField] private LayerMask allyAILayersMask = LayerMask.GetMask();
    [SerializeField] private LayerMask enemyAILayerMask = LayerMask.GetMask();

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        Assert.IsNull(Instance);
        Instance = this;
        DontDestroyOnLoad(gameObject);

        InputManager = new BHInputManager.InputManager();
        InputManager.Init();
    }

    private void OnEnable()
    {
        InputManager.Enable();
    }

    private void OnDisable()
    {
        if (InputManager == null)
            return;
        InputManager.Enable();
    }

}
