using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXHandler : MonoBehaviour
{
    public static SFXHandler Instance { get; private set; }

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip correctSFX;
    [SerializeField] private AudioClip incorrectSFX;

    // Start is called before the first frame update
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    public void PlaySFX(AudioClip _clip)
    {
        if (_clip != null)
        {
            audioSource.clip = _clip;
            audioSource.Play();
        }
    }

    public void PlayCorrectSFX() => PlaySFX(correctSFX);

    public void PlayInCorrectSFX() => PlaySFX(incorrectSFX);
}
