using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    public float leftMostX;
    public float rightMostX;


    // Update is called once per frame
    void Update()
    {
        if (player != null) {
            if (player.position.x > leftMostX && player.position.x < rightMostX)
            {
                transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
            }
        }
    }
}
