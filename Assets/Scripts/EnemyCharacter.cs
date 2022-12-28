using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyCharacter : CharacterBase
{
    public EnemyBehaviour enemBe;
    public delegate void WalkOffDelegate();

    protected override void Start()
    {
        base.Start();

        Heal(1);

        enemBe = ScriptableObject.Instantiate(enemBe);
        enemBe.Setup(this);

        enemBe.SetWalkOffDelegate(TurnOffWalking);

        StartCoroutine(enemBe.GeneralAILoop());
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        enemBe.AIBehaviour();
    }

    protected override void Jump()
    {
        throw new System.NotImplementedException();
    }

    protected void TurnOffWalking()
    {
        isActivelyMoving = false;
        animationHandler.SetWalkValue(0);
    }

}
