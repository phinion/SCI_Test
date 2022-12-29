using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterAudioData", menuName = "ScriptableObjects/Character/CharacterAudioData")]
public class CharacterAudioData : ScriptableObject
{
    public AudioClip moveAudio;
    public AudioClip jumpAudio;
    public AudioClip takeDamageAudio;
    public AudioClip dieAudio;
    public AudioClip healAudio;
}
