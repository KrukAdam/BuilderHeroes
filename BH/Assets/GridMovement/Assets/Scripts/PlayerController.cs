using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //[SerializeField]
    ////private LayerMask whatStopsMovement;
    ////[SerializeField]
    ////private float moveSpeed;
    ////[SerializeField]
    ////private Transform nextMovePoint = null;
    ////[SerializeField]
    ////private Rigidbody2D rb;

    ////private Vector2 movement;

    //private void Start()
    //{
    //    //nextMovePoint.parent = null;
    //}

    //private void Update()
    //{

    //    movement.x = Input.GetAxisRaw("Horizontal");
    //    movement.y = Input.GetAxisRaw("Vertical");

    //    //transform.position = Vector3.MoveTowards(transform.position, nextMovePoint.position, moveSpeed * Time.deltaTime);

    //    //if(Vector3.Distance(transform.position, nextMovePoint.position) <= 0.05f)
    //    //{
    //    //    if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
    //    //    {
    //    //        if (!Physics2D.OverlapCircle(nextMovePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), 0.2f, whatStopsMovement))
    //    //        {
    //    //            nextMovePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
    //    //        }
    //    //    }
    //    //    if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
    //    //    {
    //    //        if (!Physics2D.OverlapCircle(nextMovePoint.position + new Vector3(Input.GetAxisRaw("Vertical"), 0f, 0f), 0.2f, whatStopsMovement))
    //    //        {
    //    //            nextMovePoint.position += new Vector3(0,Input.GetAxisRaw("Vertical"), 0f);
    //    //        }
    //    //    }
    //    //}
    //}

    //private void FixedUpdate()
    //{
    //    rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    //}
}
