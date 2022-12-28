using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public List<Image> healthIcons;

    public Sprite fullHealthSprite;
    public Sprite emptyHealthSprite;

    public void SetHealthUI(int _currentHealth)
    {
        for(int i = 0; i < healthIcons.Count; i++)
        {
            if(i < _currentHealth)
            {
                healthIcons[i].sprite = fullHealthSprite;
            }
            else
            {
                healthIcons[i].sprite = emptyHealthSprite;
            }
        }
    }
}
