using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler
{
    PlayerCharacter player;
    PlayerControls playerControls;
    
    public Vector2 movementInput { get; private set; }
    
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

            playerControls.Player.Move.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.Player.Move.canceled += i => movementInput = Vector2.zero;

        }

        playerControls.Enable();
        Debug.Log("Input Setup");
    }

    public void Disable()
    {
        playerControls.Disable();
    }

}
