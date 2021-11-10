using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
    public CharacterStatsData CharacterStatsData { get => characterStatsData; }

    public static GameManager Instance { get; private set; }
    public BHInputManager.InputManager InputManager { get; private set; }

    [SerializeField]
    private CharacterStatsData characterStatsData = null;

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
