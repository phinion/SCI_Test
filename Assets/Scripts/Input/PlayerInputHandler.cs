using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler
{
    private PlayerCharacter player;
    private PlayerControls playerControls;
    
    public Vector2 MoveVector { get; private set; }
    public bool JumpInput { get; private set; }
    
    public PlayerInputHandler(PlayerCharacter _player)
    {
        player = _player;

        SetupInputs();
    }

    public void SetupInputs()
    {

        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.Player.Move.performed += i =>    MoveVector = i.ReadValue<Vector2>();
            playerControls.Player.Move.canceled += i =>     MoveVector = Vector2.zero;

            playerControls.Player.Jump.performed += i =>    JumpInput = true;
            playerControls.Player.Jump.canceled += i =>     JumpInput = false;
        }

        playerControls.Enable();
        Debug.Log("Input Setup");
    }



    public void Disable()
    {
        playerControls.Disable();
    }

}
