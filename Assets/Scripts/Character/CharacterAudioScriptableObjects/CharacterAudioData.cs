using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterAudioData", menuName = "ScriptableObjects/Character/CharacterAudioData")]
public class CharacterAudioData : ScriptableObject
{
    // Move audio
    public AudioClip moveAudio;
    // Jump audio
    public AudioClip jumpAudio;
    // Take damage audio
    public AudioClip takeDamageAudio;
    // Death audio
    public AudioClip dieAudio;
    // Health audio
    public AudioClip healAudio;
}
