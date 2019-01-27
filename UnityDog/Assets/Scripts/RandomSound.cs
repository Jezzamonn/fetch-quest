using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSound : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip[] AudioClips;

    public void Play()
    {
        AudioClip clip = AudioClips[Random.Range(0, AudioClips.Length)];
        AudioSource.clip = clip;
        AudioSource.Play();
    }
}
