using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioManager : MonoBehaviour
{
    public static EnemyAudioManager instance;

    AudioSource audioSource;

    public AudioClip Hit;
    public AudioClip Dead;

    void Awake()
    {
        instance = this;

        audioSource = GetComponent<AudioSource>();
    }


    public void PlayHit()
    {
        audioSource.PlayOneShot(Hit);
    }

    public void PlayDead()
    {
        audioSource.PlayOneShot(Dead);
    }
}
