//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[CreateAssetMenu(fileName = "BasicEnemyBehaviour", menuName = "ScriptableObjects/EnemyBehaviour/BasicEnemy")]

//public class BasicEnemyBehaviour : EnemyBehaviour
//{
//    public Vector2 nextDirection = Vector2.zero;

//    public override void AI()
//    {
//        if (nextDirection != Vector2.zero)
//        {
//            Move();
//        }
//        else if (enemy.isActivelyMoving != false)
//        {
//            TurnOffWalking();
//        }

//        if (CheckIfCollideWithTerrain())
//        {
//            nextDirection *= -1;
//        }
//    }

//    public override void Jump()
//    {
//        throw new System.NotImplementedException();
//    }

//    public override void Move()
//    {
//        throw new System.NotImplementedException();
//    }

//    public override void Start()
//    {
//        nextDirection = Random.Range(0, 2) == 1 ? Vector2.left : Vector2.right;
//    }

//    public override void TakeDamage()
//    {
//        throw new System.NotImplementedException();
//    }
//}
