using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour, IHealth
{
    protected CharacterFacingDirection currentFacingDirection = CharacterFacingDirection.left;


    protected const float feetYOffset = -0.5f;
    protected const float sideOffset = 0.5f;
    protected const float groundCheckRadius = 0.2f;
    protected const float sideCheckRadius = 0.2f;

    protected CharacterLocomotion locomotion;
    protected CharacterAnimationHandler animationHandler;
    protected CharacterAudioHandler audioHandler;
    protected AudioSource audioSource;

    protected bool isActivelyMoving = false;


    [Header("Base Character Info")]
    [SerializeField] protected CharacterAudioData audioData;

    public float moveSpeed = 10f;
    public float jumpSpeed = 15f;

    public int startingHealth = 1;

    public bool IsGrounded { get; private set; }
    public bool IsFacingWall { get; private set; }
    public int Health { get; private set; }

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

        audioSource = GetComponent<AudioSource>();
        audioHandler = new CharacterAudioHandler(audioSource);

        Heal(startingHealth);
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

    protected bool CheckIfCollideWithTerrain()
    {
        float xOffset = sideOffset * (currentFacingDirection == CharacterFacingDirection.left ? -1 : 1);

        return Physics2D.OverlapCircle(new Vector2(transform.position.x + xOffset, transform.position.y), sideCheckRadius, LayerMask.GetMask("Ground"));

    }

    protected void UpdateFacingDirection(CharacterFacingDirection _newDirection)
    {
        if (currentFacingDirection != _newDirection)
        {
            transform.localScale = new Vector3(-gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            currentFacingDirection = _newDirection;
        }
    }
    protected CharacterFacingDirection GetDirectionFromInput(float _xValue) => (_xValue < 0 ? CharacterFacingDirection.left : CharacterFacingDirection.right);



    // Moves the character
    protected abstract void Move();

    // Allows the character to Jump
    protected virtual void Jump()
    {
        locomotion.SetVelocityY(jumpSpeed);
    }

    public virtual void TakeDamage(int amount)
    {
        Health -= amount;

        animationHandler.SetHurtTrigger();
        audioHandler.PlaySFX(audioData?.takeDamageAudio);

        Debug.Log(gameObject.name + " says ouch");

        DeathCheck();

    }

    public virtual void Heal(int amount)
    {
        Health += amount;
    }

    public void Die()
    {
        //
        SFXHandler.Instance.PlaySFX(audioData?.dieAudio);

        Debug.Log(gameObject.name + " says Ach >.<");
        Destroy(this.gameObject);
    }

    IEnumerator WaitBeforeDie()
    {
        locomotion.Rigidbody().simulated = false;
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
