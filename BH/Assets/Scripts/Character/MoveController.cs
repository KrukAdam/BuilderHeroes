using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Stats))]
public class MoveController : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb = null;

    protected Stats stats = null;
    protected float moveSpeed;
    protected float timeBlockMove;

    //This move settings used by movement skill. Work when timeBlockMove > 0
    protected float speedMoveToTarget;
    protected float distanceToTarget;
    protected bool moveToTarget = false;
    protected Vector3 targetMovePosition;
    protected Vector2 targetMoveDirection;
    protected Vector2 directionAnimationMove;
    protected MovementSkill movementSkillUse = null;
    protected WaitForSeconds timeToMoveToTarget = new WaitForSeconds(Constant.TimeToBlockMoveCaster);
    //

    public virtual void Init(Character character, Stats stats) 
    {
        this.stats = stats;
        distanceToTarget = Constant.DistanceToTarget;
        SetEvents();
        SetMoveSpeed();
    }

    public virtual void SetTimeBlockMove(float timeToBlock, bool resetTime = false) 
    {
        moveToTarget = false;
    }

    public virtual void StartMoveToTarget(Vector3 targetPos, MovementSkill movementSkill)
    {
        if(Vector2.Distance(targetPos, rb.position) > movementSkill.CasterMoveRange)
        {
            Debug.Log("The target is too far: " + Vector2.Distance(targetPos, rb.position));
            return;
        }
        SetTimeBlockMove(Constant.TimeToBlockMoveCaster);
        targetMovePosition = targetPos;
        this.speedMoveToTarget = movementSkill.CasterMoveSpeed;
        moveToTarget = true;
        directionAnimationMove = Vector2.zero;
        movementSkillUse = movementSkill;

        if (targetMovePosition.y > transform.position.y)
        {
            directionAnimationMove.y = 1f;
        }
        else
        {
            directionAnimationMove.y = -1f;
        }

        StartCoroutine(OffMoveToTarget());
    }

    protected virtual void SetMoveAnimation(Vector2 direction, bool animWhenBlockMove = false) { }


    protected virtual void MoveToTarget()
    {
        if (!moveToTarget) return;
        if (Vector2.Distance(rb.position, targetMovePosition) < distanceToTarget)
        {
            moveToTarget = false;
            SetTimeBlockMove(0, true);
            return;
        }

        Vector3 move = targetMovePosition - transform.position;
        move.Normalize();
        targetMoveDirection = move;

        rb.MovePosition((Vector2)transform.position + (targetMoveDirection * speedMoveToTarget * Time.deltaTime));

        SetMoveAnimation(directionAnimationMove, true);
    }

    protected void SetMoveSpeed()
    {
        moveSpeed = stats.GetMoveSpeed();
    }

    protected IEnumerator OffMoveToTarget()
    {
        yield return timeToMoveToTarget;
        moveToTarget = false;
        movementSkillUse = null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (moveToTarget && movementSkillUse != null) movementSkillUse.HitTargets();
    }

    private void SetEvents()
    {
        stats.OnStatsChange += SetMoveSpeed;
    }
}
