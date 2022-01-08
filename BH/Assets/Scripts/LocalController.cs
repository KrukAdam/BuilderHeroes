using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocalController : MonoBehaviour
{
    public PlayerCharacter Player { get => player; }
    public GameUiManager GameUiManager { get => gameUiManager; }
    public LocalManagers LocalManagers { get => localManagers; }

    [SerializeField] private PlayerCharacter player = null;
    [SerializeField] private GameUiManager gameUiManager = null;
    [SerializeField] private LocalManagers localManagers = null;

    private void Awake()
    {
        player.SetupCharacter(this);
        localManagers.SetupManagers(this);
        gameUiManager.Setup(this);

        gameUiManager.GameMenuPanel.OnClickBtnExitScene += ExitScene;
    }

    private void OnDestroy()
    {
        gameUiManager.GameMenuPanel.OnClickBtnExitScene -= ExitScene;
    }

    public void ExitScene()
    {
        SceneManager.LoadScene(Constant.SceneMainMenu);
    }

}
