using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public List<Image> healthIcons;

    public Sprite fullHealthSprite;
    public Sprite emptyHealthSprite;

    private void Start()
    {
        PlayerCharacter player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
        player.OnHealthChangedCallback += SetHealthUI;

        SetHealthUI(player.Health);
    }

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
