using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyCharacter : EnemyCharacter
{

    //protected Vector2 nextDirection = Vector2.zero;
    //protected override void Start()
    //{
    //    base.Start();

    //    // Randomize first moving direction on start
    //    nextDirection = Random.Range(0, 2) == 1 ? Vector2.left : Vector2.right;
    //}

    //protected override void AI()
    //{
    //    if (nextDirection != Vector2.zero)
    //    {
    //        Move(nextDirection);
    //    }
    //    else if(isActivelyMoving != false)
    //    {
    //        TurnOffWalking();
    //    }

    //    if (CheckIfCollideWithTerrain())
    //    {
    //        nextDirection *= -1;
    //    }

    //}

    //private bool CheckIfCollideWithTerrain()
    //{
    //    float xOffset = sideOffset * (currentFacingDirection == CharacterFacingDirection.left ? -1 : 1);

    //    return Physics2D.OverlapCircle(new Vector2(transform.position.x + xOffset, transform.position.y), sideCheckRadius, LayerMask.GetMask("Ground"));

    //}

    //public override void TakeDamage(int amount)
    //{
    //    base.TakeDamage(amount);
    //    nextDirection = Vector2.zero;
    //}
}
