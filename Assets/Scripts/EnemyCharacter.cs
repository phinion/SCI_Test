using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : CharacterBase
{
    [SerializeField] 

    private void AI()
    {

    }

    protected override void Start()
    {
        base.Start();

        Health = 1;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        AI();
    }

    protected override void Jump()
    {
        throw new System.NotImplementedException();
    }

    protected override void Move()
    {
        throw new System.NotImplementedException();
    }

}
