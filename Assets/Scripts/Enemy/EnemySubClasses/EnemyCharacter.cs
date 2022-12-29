using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyCharacter : CharacterBase
{
    //public BasicEnemyBehaviour enemBe;

    //public delegate void MoveDelegate(Vector2 _moveInput);
    //public delegate void WalkOffDelegate();

    protected abstract void AI();

    protected override void Start()
    {
        base.Start();
        //enemBe.SetMoveDelegate(Move);

    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        AI();
    }

    protected void TurnOffWalking()
    {
        isActivelyMoving = false;
        animationHandler.SetWalkValue(0);
    }

}
