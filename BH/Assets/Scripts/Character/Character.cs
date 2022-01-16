using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stats))]
public class Character : MonoBehaviour
{
    public ERaceType RaceType { get => raceType; }
    public Stats Stats { get => stats; }
    public Rigidbody2D CharacterRigidbody { get => characterRigidbody; }
    public MoveController MoveController { get => moveController; }
    public LayerMask AllyLayersMask { get => allyLayersMask; }
    public LayerMask EnemyLayerMask { get => enemyLayerMask; }

    [SerializeField] protected ERaceType raceType = ERaceType.None;
    [SerializeField] protected Stats stats = null;
    [SerializeField] protected Rigidbody2D characterRigidbody = null;
    [SerializeField] protected MoveController moveController = null;

    protected LayerMask allyLayersMask;
    protected LayerMask enemyLayerMask;

    public void SetupRace(ERaceType raceType)
    {
        this.raceType = raceType;
    }

    public void SetupStats()
    {
        if (!Stats) stats = gameObject.GetComponent<Stats>();
        this.Stats.Init();
    }

    protected void SetupLayerMask(bool isPlayer)
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
