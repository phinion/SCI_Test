using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : CharacterBase
{
    PlayerInputHandler inputHandler;

    #region UnityCallbackFunctions

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        inputHandler = new PlayerInputHandler(this);

        Health = 3;
    }

    private void OnDisable()
    {
        inputHandler.Disable();
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        Move();
        Jump();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CharacterBase objCollidedWith = collision.gameObject.GetComponent<CharacterBase>();

        if (objCollidedWith != null)
        {
            //check if above enemy to deal damage
            if (transform.position.y + feetYOffset > objCollidedWith.transform.position.y)
            {
                objCollidedWith.TakeDamage(1);
                locomotion.SetVelocityY(jumpSpeed);

            }
            // Take damage
            else 
            {
                TakeDamage(1);
                locomotion.SetVelocityX(moveSpeed * (transform.position.x > objCollidedWith.transform.position.x ? 1 : -1));
                locomotion.SetVelocityY(jumpSpeed);
            }

        }
    }

    #endregion

    protected override void Move()
    {
        // Deadzone check for horizontal input
        if (inputHandler.MoveThresholdMet)
        {
            Debug.Log("moving");
            isActivelyMoving = true;
            locomotion.HorizontalMovement(moveSpeed, inputHandler.MoveVector.x);

            UpdateFacingDirection(inputHandler.MoveDirection);
            animationHandler.SetWalkValue(1);
        }
        else if (IsGrounded)
        {
            isActivelyMoving = false;
            animationHandler.SetWalkValue(0);
        }
    }

    protected override void Jump()
    {
        //check grounded
        if (IsGrounded)
        {
            if (inputHandler.JumpInput)
            {
                Debug.Log("Jump");
                locomotion.SetVelocityY(jumpSpeed);
            }
        }
    }

}
