using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    #region Variables
    public static int CoinsCollected { get; private set; }
    public static int KeysCollected { get; private set; }
    public static int GemsCollected { get; private set; }

    public static int NextPipeID { get; private set; }

    #endregion

    #region events
    public delegate void onCoinCountChanged(int _value);
    public static onCoinCountChanged OnCoinCountCallback;

    public delegate void onKeyCountChanged(int _value);
    public static onKeyCountChanged OnKeyCountCallback;

    public delegate void onGemCountChanged(int _value);
    public static onGemCountChanged OnGemCountCallback;
    #endregion

    // Reset data function
    public static void ResetData()
    {
        SetCoin(0);
        SetKey(0);
        SetGem(0);
    }

    #region Setters
    public static void AddCoin()
    {
        CoinsCollected++;
        OnCoinCountCallback?.Invoke(CoinsCollected);

        if (CoinsCollected >= 10)
        {
            SetCoin(CoinsCollected - 10);
            AddKey();
        }
    }

    public static void SetCoin(int _value)
    {
        CoinsCollected = _value;
        OnCoinCountCallback?.Invoke(CoinsCollected);
    }

    public static void AddKey()
    {
        KeysCollected++;
        SFXHandler.Instance.PlayCorrectSFX();
        OnKeyCountCallback?.Invoke(KeysCollected);
    }
    public static void UseKey()
    {
        KeysCollected--;
        OnKeyCountCallback?.Invoke(KeysCollected);
    }

    public static void SetKey(int _value)
    {
        KeysCollected = _value;
        OnKeyCountCallback?.Invoke(KeysCollected);
    }

    public static void AddGem()
    {
        GemsCollected++;
        OnGemCountCallback?.Invoke(GemsCollected);
    }
    public static void LoseGem()
    {
        GemsCollected--;
        OnGemCountCallback?.Invoke(GemsCollected);
    }

    public static void SetGem(int _value)
    {
        GemsCollected = _value;
        OnGemCountCallback?.Invoke(GemsCollected);
    }

    #endregion

    // Next pipeID getter
    public static void SetNextPipeID(int _nextPipeID) => NextPipeID = _nextPipeID;
}
