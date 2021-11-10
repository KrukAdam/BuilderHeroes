using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveController : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb = null;
    protected float timeBlockMove;

    public virtual void Init(PlayerCharacter character) { }
    public virtual void Init(EnemyCharacter character) { }

    public virtual void SetTimeBlockMove(float timeToBlock, bool resetTime = false) { }

    protected virtual void SetMoveAnimation(Vector2 direction) { }
}
