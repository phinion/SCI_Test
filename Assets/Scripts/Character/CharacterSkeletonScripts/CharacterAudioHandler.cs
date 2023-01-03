using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudioHandler
{
    // private reference to the character's audio source
    public AudioSource audioSource { get; private set; }

    // constructor to setup the audiosource
    public CharacterAudioHandler(AudioSource _audioSource)
    {
        audioSource = _audioSource;
    }

    // Function to play sound effect
    public void PlaySFX(AudioClip _clip)
    {
        audioSource.clip = _clip;
        audioSource.Play();
    }
}
