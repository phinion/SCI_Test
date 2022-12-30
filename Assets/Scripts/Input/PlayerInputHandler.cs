using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler
{
    private PlayerCharacter player;
    private PlayerControls playerControls;

    private const float moveThreshhold = 0.2f;

    public bool MoveThresholdMet => (Mathf.Abs(MoveInput.x) > moveThreshhold ? true : false);
    public Vector2 MoveInput { get; private set; }
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

            playerControls.Player.Move.performed += i =>        MoveInput = i.ReadValue<Vector2>();
            playerControls.Player.Move.canceled += i =>         MoveInput = Vector2.zero;

            playerControls.Player.Jump.performed += i =>        JumpInput = true;
            playerControls.Player.Jump.canceled += i =>         JumpInput = false;
        }

        playerControls.Enable();
    }



    public void Disable()
    {
        playerControls.Disable();
    }

}
