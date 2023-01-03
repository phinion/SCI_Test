using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sign : MonoBehaviour
{

    #region variables
    // private storage of speech bubble gameobject
    [SerializeField] private GameObject speechBubblePrefab;

    // private storage of the object that has collided with sign
    private GameObject triggerObj;

    // audio clip played when sign is successfully trigger
    private AudioSource audioSource;

    // speech bubble text box reference
    private TextMeshProUGUI textBox;
    #endregion

    #region Unity Callback functions
    // sign setup
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        textBox = GetComponentInChildren<TextMeshProUGUI>();
    }

    // hides speech bubble when game starts
    private void Start()
    {
        speechBubblePrefab.SetActive(false);
    }

    // on trigger check to display speech bubble
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && IsText())
        {
            triggerObj = collision.gameObject;

            audioSource.Play();
            speechBubblePrefab.SetActive(true);
        }
    }

    // on trigger exit check to hide speech bubble
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == triggerObj)
        {
            speechBubblePrefab.SetActive(false);
        }
    }
    #endregion

    // Check if text is null. Only used to not display text box if no text has been added to sign
    private bool IsText()
    {
        if(textBox.text != "")
        {
            return true;
        }
        return false;
    }
}
