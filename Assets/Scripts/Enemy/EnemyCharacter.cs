using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : CharacterBase
{
    #region EnemyBehaviour removed code
    //public EnemyBehaviour enemyBehaviour;

    //public delegate void MoveDelegate(Vector2 _moveInput);
    //public delegate void WalkOffDelegate();
    #endregion

    #region variables
    // Enums for enemy varients
    public enum Enemy { Basic, Rage, Dropper };

    [Header("EnemyCharacter")]
    // Enemy varient type
    public Enemy type;

    [Header("Basic enemy variables")]
    // basic
    protected Vector2 nextDirection = Vector2.zero;

    [Header("Rage enemy variables")]
    // rage
    [SerializeField] private float randomJumpChance = 0.05f;
    private bool enraged = false;

    [Header("Dropper enemy variables")]
    //dropper
    private bool waitingToDrop = true;
    private bool dropComplete = false;

    private float startingYPos;

    [SerializeField] private float gravityScale = 5f;
    //[SerializeField] private float pounceRandomChance = 0.3f;

    #endregion

    #region Unity Callback Functions
    protected override void Start()
    {
        base.Start();
        //enemBe.SetMoveDelegate(Move);

        switch (type)
        {
            case Enemy.Dropper:
                locomotion.SimulateRigidBody(false);
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
    #endregion

    #region AI functions and its varients

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

    // Basic enemy AI
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

        if (WallCollisionCheck())
        {
            nextDirection *= -1;
        }
    }
    
    // Function to manually turn of isActivelyMoving variable and set walking off in animator
    protected void TurnOffWalking()
    {
        isActivelyMoving = false;
        animationHandler.SetWalkValue(0);
    }

    // Rage enemy AI
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

    // Dropper Enemy AI
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
                locomotion.SetGravityScale();
            }
        }
        else
        {
            DropperFallReset();
        }

    } 
    
    // Dropper enemy check to see if player is below and to ground pound
    private bool CheckIfPlayerisUnderneath()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))
            {
                waitingToDrop = false;
                locomotion.SimulateRigidBody();
                locomotion.SetGravityScale(gravityScale);
                return true;
            }
            // The player is underneath the gameobject
        }
        return false;
    }

    // Function to reset dropper position after successfully groundpounding
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
            locomotion.SimulateRigidBody(false);
        }
    }
    #endregion

    #region CharacterBase movement override functions
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

    #endregion

    #region IHealth function override
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

    // Varient for when Rage enemy takes damage. Multiplies speed and turns enemy red
    void RageTakeDamage()
    {
        nextDirection = Random.Range(0, 2) == 1 ? Vector2.left : Vector2.right;
        enraged = true;
        moveSpeed *= 2;
        GetComponent<SpriteRenderer>().material.color = Color.red;
    }

    #endregion
}