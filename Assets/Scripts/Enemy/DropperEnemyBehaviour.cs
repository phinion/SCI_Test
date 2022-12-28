using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "DropperEnemyBehaviour", menuName = "ScriptableObjects/EnemyBehaviour/Dropper")]

public class DropperEnemyBehaviour : EnemyBehaviour
{
    public float detectDistance = 10f;

    public override void AIBehaviour()
    {
        Ray ray = new Ray(self.transform.position, Vector2.down);

        // Declare a variable for the raycast hit
        RaycastHit2D hit;

        // Do the raycast and store the result in the hit variable
        hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, LayerMask.GetMask("Player"));

        // Check if the raycast hit an object
        if (hit.collider != null)
        {
            // Check if the hit object has the "player" tag
            if (hit.collider.tag == "Player")
            {
                self.GetComponent<Rigidbody2D>().simulated = true;
            }
        }
    }

    public override IEnumerator GeneralAILoop()
    {
        yield return null;
    }
}
