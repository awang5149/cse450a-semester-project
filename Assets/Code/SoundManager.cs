using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;


    // Outlets
    AudioSource audioSource;
    public AudioClip missSound;
    public AudioClip hitSound;
    public AudioClip jumpSound;
    public AudioClip seedCollectedSound;
    public AudioClip pauseResumeSound;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundHit()
    {
        audioSource.PlayOneShot(hitSound);
    }
    public void PlaySoundMiss()
    {
        // add for all terrain blocks, but unsure what they are all called. 
        // tags: block, t_shape, stair, l_shape, stair
        audioSource.PlayOneShot(missSound);
    }

    public void PlaySoundJump()
    {
        audioSource.PlayOneShot(jumpSound);
    }
    public void PlaySoundSeedCollected()
    {
        audioSource.PlayOneShot(seedCollectedSound);
    }
    public void PlaySoundPauseResume()
    {
        audioSource.PlayOneShot(pauseResumeSound);
    }

}
