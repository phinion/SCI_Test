using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour, ICollectable
{
    public int scoreValue = 500;

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
        GameData.AddGem();
        LevelManager.AddScore(scoreValue);
        SFXHandler.Instance.PlaySFX(collectSFX);
        
        GameObject.Destroy(this.gameObject);
    }
}
