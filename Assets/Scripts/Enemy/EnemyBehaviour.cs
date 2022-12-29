using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyBehaviour", menuName = "ScriptableObjects/EnemyBehaviour/BasicEnemy")]
public abstract class EnemyBehaviour : ScriptableObject
{
    private EnemyCharacter enemy;

    public void SetupBehaviour(EnemyCharacter _enemy)
    {
        enemy = _enemy;
    }

    public abstract void AI();

    public abstract void Start();

    public abstract void Move();

    public abstract void Jump();

    public abstract void TakeDamage();

    //public void AI()



    //private EnemyCharacter self;
    //private Vector2 nextDirection = Vector2.zero;

    //[SerializeField] private float travelTime = 3f;
    //[SerializeField] private float waitTime = 3f;

    //public BasicEnemyBehaviour(EnemyCharacter _self)
    //{
    //    self = _self;
    //}

    //private EnemyCharacter.MoveDelegate moveDel;

    //public void SetMoveDelegate(EnemyCharacter.MoveDelegate md)
    //{
    //    moveDel = md;
    //}

    //private EnemyCharacter.WalkOffDelegate walkOffDelegate;

    //public void SetWalkOffDelegate(EnemyCharacter.WalkOffDelegate walkOffDel)
    //{
    //    walkOffDelegate = walkOffDel;
    //}

    //public void AIBehaviour()
    //{
    //    if (nextDirection != Vector2.zero)
    //    {
    //        moveDel(nextDirection);
    //    }
    //    else
    //    {
    //        walkOffDelegate();
    //    }
    //}

    //IEnumerator GeneralAILoop()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(3f);
    //        nextDirection = Vector2.right;
    //        Debug.Log("Right");
    //        yield return new WaitForSeconds(3f);
    //        nextDirection = Vector2.zero;
    //        Debug.Log("Wait");
    //        yield return new WaitForSeconds(3f);
    //        nextDirection = Vector2.left;
    //        Debug.Log("Left");
    //        yield return new WaitForSeconds(3f);
    //        nextDirection = Vector2.zero;
    //        Debug.Log("Wait");
    //    }
    //}



}
