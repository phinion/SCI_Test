using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{
    public int scoreValue = 100;

    [SerializeField] private AudioClip collectSFX;

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
        GameData.AddCoin();
        LevelManager.AddScore(scoreValue);
        SFXHandler.Instance.PlaySFX(collectSFX);
        
        GameObject.Destroy(this.gameObject);
    }
}
