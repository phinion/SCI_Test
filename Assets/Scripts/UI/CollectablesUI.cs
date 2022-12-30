using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CollectablesUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI textBox;
    [SerializeField] private Image image;
    
    public void SetTextValue(int _value)
    {
        if(_value != 0)
        {
            if (!image.enabled)
            {
                image.enabled = true;
            }

            textBox.text = _value.ToString() + "x";
        }
        else
        {
            textBox.text = "";
            image.enabled = false;
        }
    }

}
