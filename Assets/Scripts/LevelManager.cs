using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public static int LevelScore { get; private set; }

    [SerializeField] private List<GameObject> pipesInLevel = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            ResetData();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (Instance == this)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (pipesInLevel != null)
            {
                pipesInLevel.Clear();
            }

            pipesInLevel.AddRange(GameObject.FindGameObjectsWithTag("Pipe"));

            Debug.Log("finding output pipe");

            Pipe nextPipe;
            foreach (GameObject g in pipesInLevel)
            {
                nextPipe = g.GetComponent<Pipe>();

                if (nextPipe.GetConnectedToPipeID == GameData.NextPipeID)
                {
                    player.transform.position = nextPipe.GetEntryPointPosition;
                    Debug.Log("pipe: " + GameData.NextPipeID);
                    break;
                }
            }
        }

    }

    public static void ResetData()
    {
        LevelScore = 0;
    }

    public static void AddScore(int _score) => LevelScore += _score;

    private void SpawnPlayerAtPipe()
    {

    }
}
