using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    #region Variables
    [SerializeField] private int pipeID = -1;

    // scene name of the connected pipe
    public string sceneNameOfConnectedPipe = "";
    // connected pipe id unique to it's own scene
    [SerializeField] private int connectedToPipeID = -1;

    // audio clip played when going through pipe
    [SerializeField] private AudioClip audioClip;

    // Position to store where the character will spawn when coming out of pip
    public Vector3 GetEntryPointPosition => transform.GetChild(0).position;

    // Returns the pipe id of the pipe this pipe is connected to
    public int GetConnectedToPipeID => pipeID;

    #endregion

    // Collide to go through pipe
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerCharacter objCollidedWith = collision.gameObject.GetComponent<PlayerCharacter>();

        if (objCollidedWith != null && sceneNameOfConnectedPipe != "")
        {
            SFXHandler.Instance.PlaySFX(audioClip);
            GameData.SetNextPipeID(connectedToPipeID);
            SceneController.Instance.LoadScene(sceneNameOfConnectedPipe);
        }
        else
        {
            Debug.Log("PIPE:: ONCOLLISIONENTER:: Failed to move scene");
        }
    }
}
