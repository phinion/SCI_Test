using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public string sceneName = "";
    [SerializeField] private AudioClip audioClip;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerCharacter objCollidedWith = collision.gameObject.GetComponent<PlayerCharacter>();

        if (objCollidedWith != null && sceneName != "")
        {
            SFXHandler.Instance.PlaySFX(audioClip);
            SceneController.Instance.LoadScene(sceneName);
        }
    }
}
