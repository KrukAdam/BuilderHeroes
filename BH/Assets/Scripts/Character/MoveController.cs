using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveController : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb = null;

    protected float timeBlockMove;
    //This move settings used by movement skill. Work when timeBlockMove > 0
    protected float speedMoveToTarget;
    protected bool moveToTarget = false;
    protected Vector3 targetMovePosition;
    protected Vector2 targetMoveDirection;
    protected Vector2 directionAnimationMove;
    protected WaitForSeconds timeToMoveToTarget = new WaitForSeconds(Constant.TimeToBlockMoveCaster);
    //

    public virtual void Init(PlayerCharacter character) { }
    public virtual void Init(EnemyCharacter character) { }

    public virtual void SetTimeBlockMove(float timeToBlock, bool resetTime = false) 
    {
        moveToTarget = false;
    }

    public virtual void StartMoveToTarget(Vector3 targetPos, float speedMoveToTarget)
    {
        SetTimeBlockMove(Constant.TimeToBlockMoveCaster);
        targetMovePosition = targetPos;
        this.speedMoveToTarget = speedMoveToTarget;
        moveToTarget = true;
        directionAnimationMove = Vector2.zero;

        if (targetMovePosition.y > transform.position.y)
        {
            directionAnimationMove.y = 1f;
        }
        else
        {
            directionAnimationMove.y = -1f;
        }

        OffMoveToTarget();
    }

    protected virtual void SetMoveAnimation(Vector2 direction, bool animWhenBlockMove = false) { }


    protected virtual void MoveToTarget()
    {
        if (!moveToTarget) return;
        if (Vector2.Distance(rb.position, targetMovePosition) < 1f)
        {
            moveToTarget = false;
            return;
        }

        Vector3 move = targetMovePosition - transform.position;
        move.Normalize();
        targetMoveDirection = move;

        rb.MovePosition((Vector2)transform.position + (targetMoveDirection * speedMoveToTarget * Time.deltaTime));

        SetMoveAnimation(directionAnimationMove, true);
    }

    protected IEnumerator OffMoveToTarget()
    {
        yield return timeToMoveToTarget;
        moveToTarget = false;
    }
}
