using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioSO", menuName = "Sound/AudioSO")]
public class AudioSO : ScriptableObject
{
    public AudioClip clip;
    public float volume = 1f;
    public float delayTime = 0f;
    public float fadeInTime = 0f;
    public float fadeOutTime = 0f;
}
