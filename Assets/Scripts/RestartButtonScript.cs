using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButtonScript : MonoBehaviour
{
    public void Restart()
    {
        SceneController.Instance.RestartGame();
    }
}
