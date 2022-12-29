using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyCharacter : EnemyCharacter
{
    private Vector2 nextDirection = Vector2.zero;

    [SerializeField] private float travelTime = 3f;
    [SerializeField] private float waitTime = 3f;

    protected override void Start()
    {
        base.Start();

        StartCoroutine(MoveTest());
    }

    protected override void AI()
    {
        if (nextDirection != Vector2.zero)
        {
            Move(nextDirection);
        }
        else
        {
            isActivelyMoving = false;
            animationHandler.SetWalkValue(0);
        }
    }

    IEnumerator MoveTest()
    {
        while (true)
        {
            yield return new WaitForSeconds(travelTime);
            nextDirection = Vector2.right;
            Debug.Log("Right");
            yield return new WaitForSeconds(waitTime);
            nextDirection = Vector2.zero;
            Debug.Log("Wait");
            yield return new WaitForSeconds(travelTime);
            nextDirection = Vector2.left;
            Debug.Log("Left");
            yield return new WaitForSeconds(waitTime);
            nextDirection = Vector2.zero;
            Debug.Log("Wait");
        }
    }
}
