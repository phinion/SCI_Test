using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static int CoinsCollected { get; private set; }
    public static int KeysCollected { get; private set; }
    public static int GemsCollected { get; private set; }

    public static int NextPipeID { get; private set;}

    public static void ResetData()
    {
        CoinsCollected = 0;
        KeysCollected = 0;
        GemsCollected = 0;
    }

    public static void AddCoin()
    {
        CoinsCollected++;
        if(CoinsCollected >= 10)
        {
            CoinsCollected -= 10;
            AddKey();
            SFXHandler.Instance.PlayCorrectSFX();
        }
    }

    public static void AddKey() => KeysCollected++;
    public static void UseKey() => KeysCollected--;

    public static void AddGem() => GemsCollected++;
    public static void LoseGem() => GemsCollected--;

    public static void SetNextPipeID(int _nextPipeID) => NextPipeID = _nextPipeID;
}
