using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : CharacterBase
{
    PlayerInputHandler inputHandler;

    public HealthUI healthUI;

    #region UnityCallbackFunctions

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        inputHandler = new PlayerInputHandler(this);
    }

    private void OnDisable()
    {
        inputHandler.Disable();
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        CheckMove();
        CheckJump();
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

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        healthUI.SetHealthUI(Health);
    }

    public override void Heal(int amount)
    {
        base.Heal(amount);
        healthUI.SetHealthUI(Health);
    }

    public void CheckMove()
    {
        // Deadzone check for horizontal input
        if (inputHandler.MoveThresholdMet)
        {
            Move();
        }
        else if (IsGrounded && isActivelyMoving)
        {
            isActivelyMoving = false;
            animationHandler.SetWalkValue(0);
        }
    }

    public void CheckJump()
    {
        //check grounded
        if (IsGrounded && inputHandler.JumpInput)
        {
            audioHandler.PlaySFX(audioData?.jumpAudio);
            Jump();
        }
    }

    protected override void Move()
    {
        //check to only set once
        if (!isActivelyMoving)
        {
            isActivelyMoving = true;
        }
        locomotion.HorizontalMovement(moveSpeed, inputHandler.MoveInput.x);

        UpdateFacingDirection(GetDirectionFromInput(inputHandler.MoveInput.x));
        animationHandler.SetWalkValue(1);
    }

    protected override void Jump()
    {
        locomotion.SetVelocityY(jumpSpeed);
    }
}
