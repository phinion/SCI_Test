using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{
    // Collectable value, score currently not used
    public int scoreValue = 100;

    // Audio clip that will play when coin collected
    [SerializeField] private AudioClip collectSFX;

    // Ontrigger to collect
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Collect();
        }
    }

    // Collect function
    public void Collect()
    {
        GameData.AddCoin();
        LevelManager.AddScore(scoreValue);
        SFXHandler.Instance.PlaySFX(collectSFX);
        
        GameObject.Destroy(this.gameObject);
    }
}
