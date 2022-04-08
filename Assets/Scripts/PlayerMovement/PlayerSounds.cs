using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private AudioSource walkSound;
    [SerializeField] private AudioSource deadSound;
    [SerializeField] private AudioSource gruntsSound;
    [SerializeField] private AudioClip[] gruntsClipsArray;

    public void PlayWalkSound()
    {
        if (!walkSound.isPlaying)
        {
            walkSound.Play();
        }
    }

    public void StopWalkSound()
    {
        if (walkSound.isPlaying)
        {
            walkSound.Stop();
        }
    }

    public void PlayDeadSound()
    {
        deadSound.Play();
    }

    public void PlayGrountSound()
    {
        int r = Random.Range(0, gruntsClipsArray.Length);

        gruntsSound.clip = gruntsClipsArray[r];

        gruntsSound.Play();
    }

}
