using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : CharacterBase
{
    PlayerInputHandler inputHandler;
    Rigidbody2D rigidBody;

    private float moveSpeed = 5f;


    // Start is called before the first frame update
    private void Start()
    {
        inputHandler = new PlayerInputHandler(this);
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnDisable()
    {
        inputHandler.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    
    protected override void Jump()
    {
        throw new System.NotImplementedException();
    }

    protected override void Move()
    {
        if (inputHandler.movementInput.magnitude > 0.2f)
        {
            transform.position += new Vector3(inputHandler.movementInput.x, 0f, 0f) * moveSpeed * Time.deltaTime;
        }
        else
        {
            Debug.Log("No Movement");
        }
    }


    
}
