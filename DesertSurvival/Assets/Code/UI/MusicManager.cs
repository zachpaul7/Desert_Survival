using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioClip musicOnStart;
    AudioSource audioSource;

    AudioClip switchTo;

    float volume;
    [SerializeField] float timeToSwitch;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>(); 
    }

    private void Start()
    {
        Play(musicOnStart, true);
    }

    public void Play(AudioClip music, bool interrupt = false)
    {
        if(interrupt == true)
        {
            volume = 0.1f;
            audioSource.volume = volume;
            audioSource.clip = music;
            audioSource.Play();
        }
        else
        {
            switchTo = music;
            StartCoroutine(SmoothSwitchMusic());
        }
    }

    IEnumerator SmoothSwitchMusic()
    {
        volume = 0.5f;

        while (volume > 0f)
        {
            volume -= Time.deltaTime / timeToSwitch;
            if(volume < 0f) { volume = 0f; }
            audioSource.volume = volume;
            yield return new WaitForEndOfFrame();
        }

        Play(switchTo, true);
    }
}
