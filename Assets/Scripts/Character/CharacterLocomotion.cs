using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLocomotion
{
    private Rigidbody2D rigidbody;
    private Vector2 workspace;

    private const float dragThreshold = 0.1f;

    public Vector2 CurrentVelocity { get; private set; }

    public CharacterLocomotion(Rigidbody2D _rigidbody)
    {
        rigidbody = _rigidbody;
    }

    #region Set Functions

    public void HorizontalMovement(float _moveSpeedModifier, float _xInput)
    {
        // Nested IF statement that lets the player slow down twice as fast if input is opposite direction to current x velocity direciton
        rigidbody.AddForce(new Vector2(_moveSpeedModifier * _xInput * (((CurrentVelocity.x < 0 && _xInput > 0) || (CurrentVelocity.x > 0 && _xInput < 0)) ? 2 : 1), 0));

        // Clamping the velocity so it doesn't continue accelerating

        SetVelocityX(Mathf.Clamp(CurrentVelocity.x, -_moveSpeedModifier, _moveSpeedModifier));
        //rigidbody.velocity = new Vector2(Mathf.Clamp(CurrentVelocity.x, -_moveSpeedModifier, _moveSpeedModifier), CurrentVelocity.y);
    }

    public void SimulateDrag(float _moveSpeedModifier)
    {
        if (Mathf.Abs(rigidbody.velocity.x) > dragThreshold)
        {
            rigidbody.AddForce(new Vector2(_moveSpeedModifier * (CurrentVelocity.x < 0 ? 1 : -1), 0));
        }
        // Checks if x velocity hasn't already been set to zero
        else if(Mathf.Abs(rigidbody.velocity.x) != 0f)
        {
            SetVelocityX(0);
        }
    }

    public void SetVelocityZero()
    {
        rigidbody.velocity = Vector2.zero;
        CurrentVelocity = Vector2.zero;
    }

    public void SetVelocityX(float _xVelocity)
    {
        workspace.Set(_xVelocity, CurrentVelocity.y);
        rigidbody.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityY(float _yVelocity)
    {
        workspace.Set(CurrentVelocity.x, _yVelocity);
        rigidbody.velocity = workspace;
        CurrentVelocity = workspace;
    }

    #endregion

    #region updates
    public void SetVelocityUpdate()
    {
        CurrentVelocity = rigidbody.velocity;
    }
    #endregion

}
