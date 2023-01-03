using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public void LoadScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
    public void RestartFromPauseMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
    }

    public void QuitFromPauseMenu()
    {
        Application.Quit();
    }
}
