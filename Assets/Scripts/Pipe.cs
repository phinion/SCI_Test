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
            Debug.Log("Pipe setting next pipeID: " + connectedToPipeID);
            Debug.Log("GameData says pipeID is: " + GameData.NextPipeID);
            SceneController.Instance.LoadScene(sceneName);
        }
    }
}
