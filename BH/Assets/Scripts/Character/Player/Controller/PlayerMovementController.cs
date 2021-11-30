using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerActionController))]
public class PlayerMovementController : MoveController
{
    [SerializeField] private Animator animator = null;
    [SerializeField] private SpriteRenderer spriteRenderer = null;

    private PlayerCharacter playerCharacter;
    private Vector2 currentDirection;
    private Vector3 interactionPointerPos;
    private float baseDirectionX;
    private float baseDirectionY;
    private int playerOrderLayer;
    private bool refreshMove = false;
    //Anim hash
    private int hashMove;
    private int hashPosX;
    private int hashPosY;

    private void Awake()
    {
        hashMove = Animator.StringToHash("Move");
        hashPosX = Animator.StringToHash("PosX");
        hashPosY = Animator.StringToHash("PosY");
    }

    private void Start()
    {
        currentDirection = Vector2.zero;
        playerOrderLayer = Constant.BaseStartOrderLayer;

        GameManager.Instance.InputManager.InputController.Player.HorizontalMove.performed += ctx => MoveHorizontal(ctx.ReadValue<float>());
        GameManager.Instance.InputManager.InputController.Player.VerticalMove.performed += ctx => MoveVertical(ctx.ReadValue<float>());
    }

    private void FixedUpdate()
    {

        spriteRenderer.sortingOrder = playerOrderLayer - (int)transform.position.y;
        if (timeBlockMove > 0)
        {
            timeBlockMove -= Time.deltaTime;
            MoveToTarget();
            return;
        }
        else
        {
            if (refreshMove) RefreshMove();
            
            rb.MovePosition(rb.position + currentDirection * moveSpeed * Time.fixedDeltaTime);
        }
    }

    public override void Init(Character playerCharacter, Stats stats)
    {
        base.Init(playerCharacter,stats);
        this.playerCharacter = playerCharacter as PlayerCharacter;
        rb = playerCharacter.CharacterRigidbody;
    }

    
    public override void SetTimeBlockMove(float timeToBlock, bool resetTime = false)
    {
        base.SetTimeBlockMove(timeToBlock, resetTime);
        if (resetTime)
        {
                timeBlockMove = timeToBlock;
                refreshMove = true;
                transform.position = rb.position;
        }
        else
        {
            if (timeToBlock > timeBlockMove)
            {
                timeBlockMove = timeToBlock;
                SetMoveAnimation(Vector2.zero);
                refreshMove = true;
                transform.position = rb.position;
            }
        }
    }

    protected override void SetMoveAnimation(Vector2 direction, bool animWhenBlockMove = false)
    {
        if (direction == Vector2.zero || timeBlockMove > 0 && !animWhenBlockMove)
        {
            animator.SetBool(hashMove, false);
        }
        else
        {
            animator.SetBool(hashMove, true);
            animator.SetFloat(hashPosX, direction.x);
            animator.SetFloat(hashPosY, direction.y);
        }

    }

    private void RefreshMove()
    {
        refreshMove = false;
        InteractionPointerPositionSet(currentDirection);
        SetMoveAnimation(currentDirection);
    }

    private void MoveHorizontal(float direction)
    {
        Move(direction, 0, true);
    }
    private void MoveVertical(float direction)
    {
        Move(0, direction, false);
    }

    private void Move(float xDirection, float yDirection, bool moveHorizontal)
    {

        if (moveHorizontal)
        {
            baseDirectionX = xDirection;
            if (xDirection != 0)
            {
                currentDirection.x = xDirection;
                currentDirection.y = 0;
            }
            else
            {
                currentDirection.y = baseDirectionY;
                currentDirection.x = xDirection;
            }
        }
        if (!moveHorizontal)
        {
            baseDirectionY = yDirection;
            if (yDirection != 0)
            {
                currentDirection.x = 0;
                currentDirection.y = yDirection;
            }
            else
            {
                currentDirection.x = baseDirectionX;
                currentDirection.y = yDirection;
            }
        }

        InteractionPointerPositionSet(currentDirection);
        SetMoveAnimation(currentDirection);
    }

    private void InteractionPointerPositionSet(Vector2 direction)
    {
        if (direction.x == 0 && direction.y == 0 || timeBlockMove > 0) return;

        interactionPointerPos.x = direction.x;
        interactionPointerPos.y = direction.y;

        if (direction.y != 0 || direction.x != 0)
        {
           playerCharacter.PlayerActionController.InteractionPointer.localPosition = interactionPointerPos / 2;
        }
    }
}
