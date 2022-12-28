using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : CharacterBase
{

    //Rigidbody2D rigidBody;

    PlayerInputHandler inputHandler;
    CharacterLocomotion locomotion;
    CharacterAnimationHandler animationHandler;


    public float moveSpeed = 10f;
    public float jumpSpeed = 15f;

    public bool isGrounded = false;

    private const float feetYOffset = -0.5f;
    private const float groundCheckRadius = 0.2f;

    private CharacterFacingDirection currentFacingDirection = CharacterFacingDirection.left;

    #region UnityCallbackFunctions
    private void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y + feetYOffset, transform.position.z), groundCheckRadius);
    }

    // Start is called before the first frame update
    private void Start()
    {
        inputHandler = new PlayerInputHandler(this);


        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
        locomotion = new CharacterLocomotion(rigidBody);

        Animator animator = GetComponent<Animator>();
        animationHandler = new CharacterAnimationHandler(animator);
    }

    private void OnDisable()
    {
        inputHandler.Disable();
    }

    private void Update()
    {
        locomotion.SetVelocityUpdate();     // maybe remove

        GroundCheck();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Jump();
    }

    #endregion

    public void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + feetYOffset), groundCheckRadius, LayerMask.GetMask("Ground"));

        // Check so that animator bool value is only set once
        // reminder: one is for grounded and the other is for in air. If they are equal, then IsGrounded has changed so animator should be updated
        if (animationHandler.GetInAirBool == isGrounded)
        {
            animationHandler.SetInAirBool(!isGrounded);
        }

        //return Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - 0.5f), 0.2f, LayerMask.GetMask("Ground"));
    }

    public void CheckFacingDirection()
    {
        if (currentFacingDirection != inputHandler.MoveDirection)
        {
            UpdateFacingDirection(inputHandler.MoveDirection);
        }
    }

    private void UpdateFacingDirection(CharacterFacingDirection _newDirection)
    {
        transform.Rotate(0.0f, 180.0f, 0.0f);
        currentFacingDirection = _newDirection;
    }

    protected override void Move()
    {
        // Deadzone check for horizontal input
        if (inputHandler.MoveThresholdMet)
        {
            Debug.Log("moving");
            locomotion.HorizontalMovement(moveSpeed, inputHandler.MoveVector.x);

            CheckFacingDirection();
            animationHandler.SetWalkValue(1);
        }
        else if (isGrounded)
        {
            locomotion.SimulateDrag(moveSpeed);
            animationHandler.SetWalkValue(0);
        }
    }

    protected override void Jump()
    {
        //check grounded
        if (isGrounded)
        {
            if (inputHandler.JumpInput)
            {
                Debug.Log("Jump");
                locomotion.SetVelocityY(jumpSpeed);
            }
        }
    }

}
