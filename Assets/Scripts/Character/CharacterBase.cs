using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour, IHealth
{
    #region Skeleton Scripts
    // Skeleton scripts required to make characterbase class work
    protected CharacterLocomotion locomotion;
    protected CharacterAnimationHandler animationHandler;
    protected CharacterAudioHandler audioHandler;
    protected AudioSource audioSource;
    #endregion

    // character facing direction and isactively moving
    #region Protected variables
    // Variable to check if character is actively moving. Used to apply drag 
    protected bool isActivelyMoving = false;
    
    // Enum for character facing direction
    protected CharacterFacingDirection currentFacingDirection = CharacterFacingDirection.left;

    #endregion

    // Const variables used for check functions
    #region Protected const variables
    // Feet y offset from character center used to check grounded and collisions on feet
    protected const float c_feetYOffset = -0.5f;
    // radius of Physics circle used to check feet collision
    protected const float c_groundCheckRadius = 0.2f;
    // Side x offset from character center used to check if colliding with terrain
    protected const float c_sideOffset = 0.5f;
    // radius of physics circle used to check side collisions
    protected const float c_sideCheckRadius = 0.2f;
    // Time to wait before killing character. Used to emphasis character death
    protected const float c_timeWaitBeforeDeath = 1.5f;
    #endregion

    // Scriptable object holding character sound effects and audio data
    #region character stats
    [Header("Base Character Info")]
    [SerializeField] protected CharacterAudioData audioData;

    // Base character locomotion variables
    public float moveSpeed = 10f;
    public float jumpSpeed = 15f;

    // base character health variable
    public int startingHealth = 1;
    
    // Character grounded bool
    public bool IsGrounded { get; private set; }

    // Character bool for if they are colliding with wall
    public bool IsFacingWall { get; private set; }

    // Character health variable
    public int Health { get; private set; }
    
    #endregion

    #region Unity Callback functions

    // Gizmos to visualize grounded circle check
    private void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y + c_feetYOffset, transform.position.z), c_groundCheckRadius);
    }

    // Skeleton scripts initialization
    protected virtual void Awake()
    {
        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
        locomotion = new CharacterLocomotion(rigidBody);

        Animator animator = GetComponent<Animator>();
        animationHandler = new CharacterAnimationHandler(animator);

        audioSource = GetComponent<AudioSource>();
        audioHandler = new CharacterAudioHandler(audioSource);

    }

    // setting health to initial value
    protected virtual void Start()
    {
        Heal(startingHealth);
    }

    // Updates the current velocity variable inside character locomotions and checks if character is grounded
    protected virtual void Update()
    {
        locomotion.SetVelocityUpdate();     // maybe remove

        GroundCheck();
    }

    // Update to apply drag if check is met
    protected virtual void FixedUpdate()
    {
        CheckToSimulateDrag();
    }

    #endregion

    #region Checks

    // Simulates Drag if character is grounded but not actively moving (sliding on floor)
    public void CheckToSimulateDrag()
    {
        if (IsGrounded && !isActivelyMoving)
            locomotion.SimulateDrag(moveSpeed);
    }

    // Groundcheck function
    public void GroundCheck()
    {
        IsGrounded = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + c_feetYOffset), c_groundCheckRadius, LayerMask.GetMask("Ground"));

        // Check so that animator bool value is only set once
        // reminder: one is for grounded and the other is for in air. If they are equal, then IsGrounded has changed so animator should be updated
        if (animationHandler.GetInAirBool == IsGrounded)
        {
            animationHandler.SetInAirBool(!IsGrounded);
        }

        //return Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - 0.5f), 0.2f, LayerMask.GetMask("Ground"));
    }

    // WallCollision check function
    protected bool WallCollisionCheck()
    {
        float xOffset = c_sideOffset * (currentFacingDirection == CharacterFacingDirection.left ? -1 : 1);

        return Physics2D.OverlapCircle(new Vector2(transform.position.x + xOffset, transform.position.y), c_sideCheckRadius, LayerMask.GetMask("Ground"));

    }
    
    // DeathCheck function
    public void DeathCheck()
    {
        if (Health <= 0)
        {
            StartCoroutine(WaitBeforeDie());
        }
    }

    #endregion

    #region Facing Direction getter and setter
    // Function that checks if facing direction has changed and updates the sprite
    protected void UpdateFacingDirection(CharacterFacingDirection _newDirection)
    {
        if (currentFacingDirection != _newDirection)
        {
            transform.localScale = new Vector3(-gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            currentFacingDirection = _newDirection;
        }
    }

    // Getter to return character's facing direction
    protected CharacterFacingDirection GetDirectionFromInput(float _xValue) => (_xValue < 0 ? CharacterFacingDirection.left : CharacterFacingDirection.right);

    #endregion

    #region Base movement functions
    // Moves the character
    protected abstract void Move();

    // Allows the character to Jump
    protected virtual void Jump()
    {
        locomotion.SetVelocityY(jumpSpeed);
    }

    #endregion

    #region IHealth Interface implementation

    // Takes damage
    public virtual void TakeDamage(int amount)
    {
        Health -= amount;

        animationHandler.SetHurtTrigger();
        audioHandler.PlaySFX(audioData?.takeDamageAudio);

        Debug.Log(gameObject.name + " says ouch");

        DeathCheck();

    }

    // Heals character health
    public virtual void Heal(int amount)
    {
        Health += amount;
    }

    // Kills characer and plays death sfx
    public virtual void Die()
    {
        //
        SFXHandler.Instance.PlaySFX(audioData?.dieAudio);

        Debug.Log(gameObject.name + " says Ach >.<");
        Destroy(this.gameObject);
    }

    #endregion

    // Small wait before killing character which disables collisions and rigidbody movements for a small amount of time
    IEnumerator WaitBeforeDie()
    {
        locomotion.SimulateRigidBody(false);
        GetComponent<BoxCollider2D>().enabled = false;

        yield return new WaitForSeconds(c_timeWaitBeforeDeath);
        Die();
    }

    

}
