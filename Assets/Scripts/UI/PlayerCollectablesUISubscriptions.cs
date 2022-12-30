using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollectablesUISubscriptions : MonoBehaviour
{
    [SerializeField] private CollectablesUI coinsUI;
    [SerializeField] private CollectablesUI keysUI;
    [SerializeField] private CollectablesUI gemsUI;

    // Start is called before the first frame update
    void Start()
    {
        GameData.OnCoinCountCallback += coinsUI.SetTextValue;
        coinsUI.SetTextValue(GameData.CoinsCollected);

        GameData.OnKeyCountCallback += keysUI.SetTextValue;
        keysUI.SetTextValue(GameData.KeysCollected);

        GameData.OnGemCountCallback += gemsUI.SetTextValue;
        gemsUI.SetTextValue(GameData.GemsCollected);
    }

    private void OnDisable()
    {
        GameData.OnCoinCountCallback -= coinsUI.SetTextValue;
        GameData.OnKeyCountCallback -= keysUI.SetTextValue;
        GameData.OnGemCountCallback -= gemsUI.SetTextValue;
    }
}
