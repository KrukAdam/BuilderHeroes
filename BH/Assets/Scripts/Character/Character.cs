using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stats))]
public class Character : MonoBehaviour
{
    public Stats Stats { get => stats; }
    public Rigidbody2D CharacterRigidbody { get => characterRigidbody; }
    public MoveController MoveController { get => moveController; }
    public GameUiManager GameUiManager { get => gameUiManager; }
    public LayerMask AllyLayersMask { get => allyLayersMask; }
    public LayerMask EnemyLayerMask { get => enemyLayerMask; }

    [SerializeField] protected Stats stats;
    [SerializeField] protected Rigidbody2D characterRigidbody;
    [SerializeField] protected MoveController moveController;

    protected GameUiManager gameUiManager;
    protected LayerMask allyLayersMask;
    protected LayerMask enemyLayerMask;

    protected void InitLayerMask(bool isPlayer)
    {
        if (isPlayer)
        {
            allyLayersMask = GameManager.Instance.AllyLayersMask;
            enemyLayerMask = GameManager.Instance.EnemyLayersMask;
        }
        else
        {
            allyLayersMask = GameManager.Instance.AllyAILayersMask;
            enemyLayerMask = GameManager.Instance.EnemyAILayersMask;
        }
    }

}
