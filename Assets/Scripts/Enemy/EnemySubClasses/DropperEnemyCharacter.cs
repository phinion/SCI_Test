using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropperEnemyCharacter : EnemyCharacter
{

    //private bool waitingToDrop = true;
    //private bool dropComplete = false;

    //private float startingYPos;

    //[SerializeField] private float gravityScale = 5f;
    //[SerializeField] private float pounceRandomChance = 0.3f;

    //private bool CheckIfPlayerisUnderneath()
    //{
    //    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);

    //    if (hit.collider != null)
    //    {
    //        if (hit.collider.CompareTag("Player"))
    //        {
    //            waitingToDrop = false;
    //            locomotion.Rigidbody().simulated = true;
    //            locomotion.Rigidbody().gravityScale = gravityScale;
    //            return true;
    //        }
    //        // The player is underneath the gameobject
    //    }
    //    return false;
    //}

    //private void RandomPounce()
    //{
    //    if (IsGrounded)
    //    {
    //        if (Random.Range(0f, 1f) < pounceRandomChance)
    //        {
    //            Jump();
    //        }
    //    }
    //}

    //protected override void AI()
    //{

    //    if(waitingToDrop)
    //    {
    //        CheckIfPlayerisUnderneath();
    //    }
    //    else if (!dropComplete)
    //    {
    //        if (IsGrounded)
    //        {
    //            dropComplete = true;
    //            locomotion.Rigidbody().gravityScale = 0f;
    //        }
    //    }
    //    else
    //    {
    //        Jump();
    //    }

    //}

    //protected override void Jump()
    //{
    //    if(transform.position.y < startingYPos)
    //    {
    //        locomotion.SetVelocityY(jumpSpeed);
    //    }
    //    else
    //    {
    //        waitingToDrop = true;
    //        dropComplete = false;
    //        locomotion.SetVelocityZero();
    //        locomotion.Rigidbody().simulated = false;
    //    }
    //}

    //protected override void Start()
    //{
    //    base.Start();

    //    locomotion.Rigidbody().simulated = false;
    //    startingYPos = transform.position.y;
    //}
}
