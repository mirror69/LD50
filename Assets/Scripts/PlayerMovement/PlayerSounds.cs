using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private AudioSource walkSound;
    [SerializeField] private AudioSource deadSound;
    [SerializeField] private AudioSource deadSittingSound;
    [SerializeField] private AudioSource gruntsSound;
    [SerializeField] private AudioSource stickSound;
    [SerializeField] private AudioClip[] gruntsClipsArray;
    [SerializeField] private AudioClip[] stepClipsArray;

    public void PlayWalkSound()
    {
        if (walkSound.isPlaying)
        {
            walkSound.Stop();
        }
        walkSound.clip = GetRandomClip(stepClipsArray);
        walkSound.Play();
    }

    public void PlayStickSound()
    {
        if (stickSound.isPlaying)
        {
            stickSound.Stop();
        }
        stickSound.Play();
    }

    public void PlayDeadSound()
    {
        deadSound.Play();
    }

    public void PlayDeadSittingSound()
    {
        deadSittingSound.Play();
    }

    public void PlayGrountSound()
    {
        int r = Random.Range(0, gruntsClipsArray.Length);

        gruntsSound.clip = gruntsClipsArray[r];

        gruntsSound.Play();
    }

    private AudioClip GetRandomClip(AudioClip[] clips)
    {
        return clips[Random.Range(0, clips.Length)];
    }
}
