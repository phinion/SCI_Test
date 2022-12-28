using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour, IHealth
{
    private CharacterFacingDirection currentFacingDirection = CharacterFacingDirection.left;


    protected const float feetYOffset = -0.5f;
    protected const float groundCheckRadius = 0.2f;


    protected CharacterLocomotion locomotion;
    protected CharacterAnimationHandler animationHandler;

    protected bool isActivelyMoving = false;

    public float moveSpeed = 10f;
    public float jumpSpeed = 15f;

    public bool IsGrounded { get; private set; }
    public int Health { get; set; }

    private void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y + feetYOffset, transform.position.z), groundCheckRadius);
    }

    protected virtual void Start()
    {
        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
        locomotion = new CharacterLocomotion(rigidBody);

        Animator animator = GetComponent<Animator>();
        animationHandler = new CharacterAnimationHandler(animator);
    }
    protected virtual void Update()
    {
        locomotion.SetVelocityUpdate();     // maybe remove

        GroundCheck();
    }

    protected virtual void FixedUpdate()
    {
        if (IsGrounded && !isActivelyMoving)
            locomotion.SimulateDrag(moveSpeed);
    }

    public void GroundCheck()
    {
        IsGrounded = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + feetYOffset), groundCheckRadius, LayerMask.GetMask("Ground"));

        // Check so that animator bool value is only set once
        // reminder: one is for grounded and the other is for in air. If they are equal, then IsGrounded has changed so animator should be updated
        if (animationHandler.GetInAirBool == IsGrounded)
        {
            animationHandler.SetInAirBool(!IsGrounded);
        }

        //return Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - 0.5f), 0.2f, LayerMask.GetMask("Ground"));
    }

    protected void UpdateFacingDirection(CharacterFacingDirection _newDirection)
    {
        if (currentFacingDirection != _newDirection)
        {
            transform.Rotate(0.0f, 180.0f, 0.0f);
            currentFacingDirection = _newDirection;
        }
    }

    // Moves the character
    protected abstract void Move();

    // Allows the character to Jump
    protected abstract void Jump();

    public void TakeDamage(int amount)
    {
        Health -= amount;

        animationHandler.SetHurtTrigger();

        Debug.Log(gameObject.name + " says ouch");

        DeathCheck();

    }

    public void Heal(int amount)
    {
        Health += amount;
    }

    public void Die()
    {
        //
        Debug.Log(gameObject.name + " says Ach >.<");
        Destroy(this.gameObject);
    }

    IEnumerator WaitBeforeDie()
    {
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<BoxCollider2D>().enabled = false;

        yield return new WaitForSeconds(1.5f);
        Die();
    }

    public void DeathCheck()
    {
        if (Health <= 0)
        {
            StartCoroutine(WaitBeforeDie());
        }
    }

}
