using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] private int pipeID = -1;


    public string sceneName = "";
    [SerializeField] private int connectedToPipeID = -1;

    [SerializeField] private AudioClip audioClip;

    public Vector3 GetEntryPointPosition => transform.GetChild(0).position;

    public int GetConnectedToPipeID => pipeID;
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerCharacter objCollidedWith = collision.gameObject.GetComponent<PlayerCharacter>();

        if (objCollidedWith != null && sceneName != "")
        {
            SFXHandler.Instance.PlaySFX(audioClip);
            GameData.SetNextPipeID(connectedToPipeID);
            SceneController.Instance.LoadScene(sceneName);
        }
        else
        {
            Debug.Log("PIPE:: ONCOLLISIONENTER:: Failed to move scene");
        }
    }
}
