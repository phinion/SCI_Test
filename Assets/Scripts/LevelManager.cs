using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public static int levelScore { get; private set; }

    private void Awake()
    {
        Instance = this;
        ResetData();
    }
    public static void ResetData()
    {
        levelScore = 0;
    }

    public static void AddScore(int _score) => levelScore += _score;
}
