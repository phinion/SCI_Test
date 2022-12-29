using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudioHandler
{
    public AudioSource audioSource { get; private set; }

    public CharacterAudioHandler(AudioSource _audioSource)
    {
        audioSource = _audioSource;
    }

    public void PlaySFX(AudioClip _clip)
    {
        audioSource.clip = _clip;
        audioSource.Play();
    }
}
