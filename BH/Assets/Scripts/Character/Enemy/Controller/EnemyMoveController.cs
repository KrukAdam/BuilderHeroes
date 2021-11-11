using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveController : MoveController
{
    [SerializeField] private Transform moveTarget = null;
    [SerializeField] private float moveSpeed = 3f;

    private Vector2 direction;
    private bool canMove = true;

    private void Start()
    {
        if (!rb) rb = gameObject.GetComponent<Rigidbody2D>();

        direction.x = moveTarget.position.x;
        direction.y = moveTarget.position.y;
    }

    private void Update()
    {
        if (!canMove) return;
        if (Vector2.Distance(rb.position, moveTarget.position) < 1f) return;

        Vector3 move = moveTarget.position - transform.position;
        move.Normalize();
        direction = move;
    }

    void FixedUpdate()
    {
        if (Vector2.Distance(rb.position, moveTarget.position) < 1f) return;

        if (timeBlockMove > 0)
        {
            timeBlockMove -= Time.deltaTime;
            MoveToTarget();
            return;
        }
        else
        {
            rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
        }
    }

    public override void SetTimeBlockMove(float timeToBlock, bool resetTime = false)
    {
        base.SetTimeBlockMove(timeToBlock, resetTime);
        if (resetTime)
        {
            timeBlockMove = timeToBlock;
            transform.position = rb.position;
        }
        else
        {
            if (timeToBlock > timeBlockMove)
            {
                timeBlockMove = timeToBlock;
                // SetMoveAnimation(Vector2.zero);
                transform.position = rb.position;
            }
        }
    }

    protected override void SetMoveAnimation(Vector2 direction)
    {
        Debug.Log("enemyAnim");
    }
}
