using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Locked gate class. Requires condition to be met to destroy gate
public class LockedGate : MonoBehaviour
{
    // Audio clip played when gate unlocked
    [SerializeField] private AudioClip audioClip;

    //[SerializeField] UnityAction OnGateOpenedCallback;

    // Collectable type required to open gate
    public string requiredCollectable = "Key";

    // Collision function to check if gate can be opened
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerCharacter objCollidedWith = collision.gameObject.GetComponent<PlayerCharacter>();

        if (requiredCollectable == "Key")
        {
            if (GameData.KeysCollected > 0)
            {
                OpenGate();
            }
            else
            {
                SFXHandler.Instance.PlayInCorrectSFX();
            }
        }
        else if (requiredCollectable == "Gem")
        {
            if (GameData.GemsCollected > 0)
            {
                OpenGate();
            }
            else
            {
                SFXHandler.Instance.PlayInCorrectSFX();
            }
        }
    }

    // Open gate function
    private void OpenGate()
    {
        if (requiredCollectable == "Key")
        {
            GameData.UseKey();
        }
        else if (requiredCollectable == "Gem")
        {
            GameData.LoseGem();
        }
        SFXHandler.Instance.PlaySFX(audioClip);
        //OnGateOpenedCallback?.Invoke();
        GameObject.Destroy(this.gameObject);
    }
}
