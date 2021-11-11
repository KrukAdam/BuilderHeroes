using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkillMissile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer missileRenderer = null;
    [SerializeField] private Rigidbody2D rb = null;

    private Character missileOwner;
    private RangeSkill rangeSkill;
    private bool canMove = false;
    private bool canHitCaster = false;
    private Vector3 moveDirection;
    private Vector2 moveTarget;
    private Vector2 ownerShotPos;
    private float moveSpeed = 3f;
    private float angleRotation;
    private float missileRange;
    private int orderLayer;
    private int lowCollidersLayer;

    private void Awake()
    {
        if (!rb) rb = gameObject.GetComponent<Rigidbody2D>();

        moveTarget = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        orderLayer = Constant.MissileStartOrderLayer;
        lowCollidersLayer = Constant.LowLayerColliders;

        CalculateMoveDirection();
        RotateMissile();

    }

    void FixedUpdate()
    {
        if (!canMove) return;

        CalculateMoveDirection();
        RotateMissile();
        MoveMissile();
        CalculateSortLayer();
        CalculateMoveRange();

        if (!missileRenderer.enabled) ShowMissile(true);
    }

    public void ShowMissile(bool show)
    {
        missileRenderer.enabled = show;
    }

    public void SetupMissile(RangeSkill rangeSkill, bool canHitCaster = false)
    {
        this.rangeSkill = rangeSkill;
        this.canHitCaster = canHitCaster;
        moveSpeed = rangeSkill.MissileSpeed;
        missileRenderer.sprite = rangeSkill.MissileSprite;
        ownerShotPos = rangeSkill.SkillSetupInfo.UserTransform.position;
        missileRange = rangeSkill.MissileRange;
        missileOwner = rangeSkill.SkillSetupInfo.SkillOwner;

        canMove = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == lowCollidersLayer) return;  //Missile flies over this colliders

        if(collision.TryGetComponent(out Character character))
        {
            if (character == missileOwner && !canHitCaster) return;

            Debug.Log("Enter2");
            rangeSkill.RangeSkillEffect(gameObject.transform);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Enter3");
            Destroy(gameObject);
        }
    }

    private void RotateMissile()
    {
        angleRotation = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        rb.rotation = angleRotation;
    }

    private void MoveMissile()
    {
        moveDirection.Normalize();
        rb.MovePosition((Vector2)transform.position + ((Vector2)moveDirection * moveSpeed * Time.deltaTime));
    }

    private void CalculateMoveDirection()
    {
        moveDirection = (Vector3)moveTarget - transform.position;
    }

    private void CalculateSortLayer()
    {
        missileRenderer.sortingOrder = orderLayer - (int)transform.position.y;
    }

    private void CalculateMoveRange()
    {
        if(Vector2.Distance(transform.position, moveTarget) < 0.2)
        {
            Destroy(gameObject);
        }
        if (Vector2.Distance(transform.position, ownerShotPos) > missileRange)
        {
            Destroy(gameObject);
        }
    }
}
