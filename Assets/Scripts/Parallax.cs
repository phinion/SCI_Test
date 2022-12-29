using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startpos;
    public GameObject cam;

    [SerializeField] private GameObject leftBackground;
    [SerializeField] private GameObject rightBackground;

    public float backgroundWidth = 35f;

    public float parallaxEffect;

    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        //length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        BackgroundMove();
        InfiniteBackgroundCheck();
    }

    void BackgroundMove()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float _distance = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startpos + _distance, transform.position.y, transform.position.z);

        if (temp > startpos + length)
        {
            startpos += length;
        }
        else if (temp < startpos - length)
        {
            startpos -= length;
        }
    }

    void InfiniteBackgroundCheck()
    {
        // If the camera has moved beyond the first background image, move it to the end of the second background image
        if (cam.transform.position.x > rightBackground.transform.position.x)
        {
            leftBackground.transform.localPosition = new Vector2(leftBackground.transform.localPosition.x + backgroundWidth, leftBackground.transform.localPosition.y);
            rightBackground.transform.localPosition = new Vector2(rightBackground.transform.localPosition.x + backgroundWidth, rightBackground.transform.localPosition.z);
        }

        // If the camera has moved beyond the second background image, move it to the end of the first background image
        if (cam.transform.position.x < leftBackground.transform.position.x)
        {
            leftBackground.transform.localPosition = new Vector2(leftBackground.transform.localPosition.x - backgroundWidth, leftBackground.transform.localPosition.y);
            rightBackground.transform.localPosition = new Vector2(rightBackground.transform.localPosition.x - backgroundWidth, rightBackground.transform.localPosition.z);
        }
    }
}

