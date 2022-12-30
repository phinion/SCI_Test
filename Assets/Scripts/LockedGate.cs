using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LockedGate : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;

    //[SerializeField] UnityAction OnGateOpenedCallback;

    public string requiredCollectable = "Key";

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
