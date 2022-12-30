using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static int coinsCollected { get; private set; }
    public static int keysCollected { get; private set; }
    public static int gemsCollected { get; private set; }

    public static void ResetData()
    {
        coinsCollected = 0;
        keysCollected = 0;
        gemsCollected = 0;
    }

    public static void AddCoin()
    {
        coinsCollected++;
        if(coinsCollected >= 10)
        {
            coinsCollected -= 10;
            AddKey();
            Debug.Log("KeyAdded");
            SFXHandler.Instance.PlayCorrectSFX();
            Debug.Log("KeyAdded2");
        }
    }

    public static void AddKey() => keysCollected++;
    public static void UseKey() => keysCollected--;

    public static void AddGem() => gemsCollected++;
    public static void LoseGem() => gemsCollected--;
}
