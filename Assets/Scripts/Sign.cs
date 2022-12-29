using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sign : MonoBehaviour
{
    [SerializeField] private GameObject speechBubblePrefab;

    private GameObject triggerObj;

    private AudioSource audioSource;

    private TextMeshProUGUI textBox;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        textBox = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {

        speechBubblePrefab.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && IsText())
        {
            triggerObj = collision.gameObject;

            audioSource.Play();
            speechBubblePrefab.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == triggerObj)
        {
            speechBubblePrefab.SetActive(false);
        }
    }

    private bool IsText()
    {
        if(textBox.text != "")
        {
            return true;
        }
        return false;
    }
}
