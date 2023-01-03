using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler
{
    #region Skeleton scripts
    private PlayerCharacter player;
    private PlayerControls playerControls;
    #endregion

    #region variables
    private const float c_moveThreshhold = 0.2f;

    public bool MoveThresholdMet => (Mathf.Abs(MoveInput.x) > c_moveThreshhold ? true : false);
    public Vector2 MoveInput { get; private set; }
    public bool JumpInput { get; private set; }
    #endregion

    // Constructor
    public PlayerInputHandler(PlayerCharacter _player)
    {
        player = _player;

        SetupInputs();
    }

    // Setup inputs
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

    // Disable inputs
    public void Disable()
    {
        playerControls.Disable();
    }

}
