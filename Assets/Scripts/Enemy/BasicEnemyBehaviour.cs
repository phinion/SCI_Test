using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicEnemyBehaviour", menuName = "ScriptableObjects/EnemyBehaviour/BasicEnemy")]
public class BasicEnemyBehaviour : EnemyBehaviour
{
    private Vector2 nextDirection = Vector2.zero;

    [SerializeField] private float travelTime = 3f;
    [SerializeField] private float waitTime = 3f;

    public override void AIBehaviour()
    {
        if (nextDirection != Vector2.zero)
        {
            self.locomotion.HorizontalMovement(self.moveSpeed, nextDirection.x);
        }
        else
        {
            walkOffDelegate();
        }
    }

    public override IEnumerator GeneralAILoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            nextDirection = Vector2.right;
            Debug.Log("Right");
            yield return new WaitForSeconds(3f);
            nextDirection = Vector2.zero;
            Debug.Log("Wait");
            yield return new WaitForSeconds(3f);
            nextDirection = Vector2.left;
            Debug.Log("Left");
            yield return new WaitForSeconds(3f);
            nextDirection = Vector2.zero;
            Debug.Log("Wait");
        }
    }

}
