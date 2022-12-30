using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LockedGate : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;

    //[SerializeField] UnityAction OnGateOpenedCallback;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerCharacter objCollidedWith = collision.gameObject.GetComponent<PlayerCharacter>();

        if (GameData.keysCollected > 0)
        {
            OpenGate();
        }
    }

    private void OpenGate()
    {
        GameData.UseKey();
        SFXHandler.Instance.PlaySFX(audioClip);
        //OnGateOpenedCallback?.Invoke();
        GameObject.Destroy(this.gameObject);
    }
}
