using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Faith_controller fc;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        Footsteps();
    }
    public void Footsteps()
    {
        if (fc.isGrounded && !fc.isRoll && fc.move != 0)
        {
            StartFootsteps();
        }
        else
        {
            StopFootsteps();
        }
    }

    void StartFootsteps()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    void StopFootsteps()
    {
        audioSource.Stop();
    }
}
