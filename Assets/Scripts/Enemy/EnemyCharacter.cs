using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : CharacterBase
{
    //public EnemyBehaviour enemyBehaviour;

    //public delegate void MoveDelegate(Vector2 _moveInput);
    //public delegate void WalkOffDelegate();
    public enum Enemy { Basic, Rage, Dropper };

    [Header("EnemyCharacter")]
    public Enemy type;

    // basic
    protected Vector2 nextDirection = Vector2.zero;

    // rage
    [SerializeField] private float randomJumpChance = 0.05f;
    private bool enraged = false;

    //dropper
    private bool waitingToDrop = true;
    private bool dropComplete = false;

    private float startingYPos;

    [SerializeField] private float gravityScale = 5f;
    //[SerializeField] private float pounceRandomChance = 0.3f;
    protected override void Start()
    {
        base.Start();
        //enemBe.SetMoveDelegate(Move);

        switch (type)
        {
            case Enemy.Dropper:
                locomotion.Rigidbody().simulated = false;
                startingYPos = transform.position.y;
                break;
            //rage shares this with basic
            default:
                nextDirection = Random.Range(0, 2) == 1 ? Vector2.left : Vector2.right;
                break;
        }

    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        AI();
    }
    protected void AI()
    {
        switch (type)
        {
            case Enemy.Rage:
                RageAI();
                break;
            case Enemy.Dropper:
                DropperAI();
                break;
            default:
                BasicAI();
                break;
        }
    }

    void BasicAI()
    {
        if (nextDirection != Vector2.zero)
        {
            Move();
        }
        else if (isActivelyMoving != false)
        {
            TurnOffWalking();
        }

        if (CheckIfCollideWithTerrain())
        {
            nextDirection *= -1;
        }
    }

    void RageAI()
    {
        BasicAI();

        if (enraged)
        {
            if (Random.Range(0f, 1f) < randomJumpChance)
            {
                if (IsGrounded)
                {
                    Jump();
                }
            }

        }
    }

    void DropperAI()
    {

        if (waitingToDrop)
        {
            CheckIfPlayerisUnderneath();
        }
        else if (!dropComplete)
        {
            if (IsGrounded)
            {
                dropComplete = true;
                locomotion.Rigidbody().gravityScale = 0f;
            }
        }
        else
        {
            DropperFallReset();
        }

    }



    void DropperFallReset()
    {
        if (transform.position.y < startingYPos)
        {
            locomotion.SetVelocityY(jumpSpeed);
        }
        else
        {
            waitingToDrop = true;
            dropComplete = false;
            locomotion.SetVelocityZero();
            locomotion.Rigidbody().simulated = false;
        }
    }



    protected void TurnOffWalking()
    {
        isActivelyMoving = false;
        animationHandler.SetWalkValue(0);
    }

    protected override void Move()
    {
        //check to only set once
        if (!isActivelyMoving)
        {
            isActivelyMoving = true;
        }
        locomotion.HorizontalMovement(moveSpeed, nextDirection.x);

        UpdateFacingDirection(GetDirectionFromInput(nextDirection.x));
        animationHandler.SetWalkValue(1);
    }

    protected override void Jump()
    {
        base.Jump();
    }

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);

        switch (type)
        {
            case Enemy.Rage:
                RageTakeDamage();
                break;
            case Enemy.Dropper:
                break;
            default:
                nextDirection = Vector2.zero;
                break;
        }
    }

    void RageTakeDamage()
    {
        nextDirection = Random.Range(0, 2) == 1 ? Vector2.left : Vector2.right;
        enraged = true;
        moveSpeed *= 2;
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    private bool CheckIfPlayerisUnderneath()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))
            {
                waitingToDrop = false;
                locomotion.Rigidbody().simulated = true;
                locomotion.Rigidbody().gravityScale = gravityScale;
                return true;
            }
            // The player is underneath the gameobject
        }
        return false;
    }
}
