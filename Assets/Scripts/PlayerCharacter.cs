using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : CharacterBase
{

    Rigidbody2D rigidBody;

    PlayerInputHandler inputHandler;
    CharacterLocomotion locomotion;


    public float moveSpeed = 10f;
    public float jumpSpeed = 10f;

    public bool isGrounded = false;

    private void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z), 0.2f);
    }

    // Start is called before the first frame update
    private void Start()
    {
        inputHandler = new PlayerInputHandler(this);
        rigidBody = GetComponent<Rigidbody2D>();

        locomotion = new CharacterLocomotion(rigidBody);
    }

    private void OnDisable()
    {
        inputHandler.Disable();
    }

    private void Update()
    {
        locomotion.SetVelocityUpdate();     // maybe remove
        isGrounded = CheckIfGrounded();
    }

    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - 0.5f), 0.2f, LayerMask.GetMask("Ground"));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Jump();
    }
    
    protected override void Move()
    {
        //if grounded

        // Deadzone check for horizontal input
        if (Mathf.Abs(inputHandler.MoveVector.x) > 0.2f)
        {
            locomotion.HorizontalMovement(moveSpeed, inputHandler.MoveVector.x);
        }
        else
        {
            locomotion.SimulateDrag(moveSpeed);
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
