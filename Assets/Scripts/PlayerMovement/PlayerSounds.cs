using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private AudioSource walkSound;
    [SerializeField] private AudioSource deadSound;

    public void PlayWalkSound()
    {
        walkSound.Play();
    }

    public void StopWalkSound()
    {
        walkSound.Stop();
    }

    public void PlayDeadSound()
    {
        deadSound.Play();
    }
}
