using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{
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
        LevelData.coinsCollected++;
        LevelManager.Instance.CheckWinStatus();

        GameObject.Destroy(this);
    }
}
