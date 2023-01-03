using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : CharacterBase, IHealth
{
    // Class that stored player input data
    private PlayerInputHandler inputHandler;

    // Event called when player health value is adjusted. Used for UI 
    public delegate void onHealthChanged(int _currentHealth);
    public onHealthChanged OnHealthChangedCallback;

    // Event called when player character dies. Used mainly to display failure screen
    public delegate void onDead();
    public onDead OnDeadPlayerCallback;

    #region UnityCallbackFunctions

    protected override void Awake()
    {
        base.Awake();

        inputHandler = new PlayerInputHandler(this);
    }

    private void OnDisable()
    {
        inputHandler?.Disable();

        OnHealthChangedCallback = null;
        OnDeadPlayerCallback = null;
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
            if (transform.position.y + c_feetYOffset > objCollidedWith.transform.position.y)
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

    // Difference between IHealth in player and enemy is that playercharacter, in addition to taking damage, must reflect changes in UI
    #region IHealth override functions
    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        OnHealthChangedCallback?.Invoke(Health);
    }

    public override void Heal(int amount)
    {
        base.Heal(amount);
        OnHealthChangedCallback?.Invoke(Health);
    }

    public override void Die()
    {
        OnDeadPlayerCallback?.Invoke();

        base.Die();
    }
    #endregion

    #region Checks
    // Move check function
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

    // Jump check function
    public void CheckJump()
    {
        //check grounded
        if (IsGrounded && inputHandler.JumpInput)
        {
            audioHandler.PlaySFX(audioData?.jumpAudio);
            Jump();
        }
    }
    #endregion

    #region CharacterBase Movement override functions
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
    #endregion

}
