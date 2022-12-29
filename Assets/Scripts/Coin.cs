using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{
    public int scoreValue = 100;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Collect();
        }
    }

    public void Collect()
    {
        Debug.Log("Coin Collected");
        LevelManager.AddScore(scoreValue);
        GameObject.Destroy(this.gameObject);
    }
}
