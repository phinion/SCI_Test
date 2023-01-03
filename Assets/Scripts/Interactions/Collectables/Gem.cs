using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Redundant class. Coin can be expanded to occupy multiple types. Rename Coin to collectable
public class Gem : MonoBehaviour, ICollectable
{
    // Collectable value, score currently not used
    public int scoreValue = 500;

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
        GameData.AddGem();
        LevelManager.AddScore(scoreValue);
        SFXHandler.Instance.PlaySFX(collectSFX);
        
        GameObject.Destroy(this.gameObject);
    }
}
