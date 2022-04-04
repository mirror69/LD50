using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoudManagerChess : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _errorSound;

    [SerializeField]
    private AudioClip _shakeSound;

    [SerializeField]
    private AudioClip _moveSound;




    public void PlayErrorSound()
    {
        _audioSource.PlayOneShot(_errorSound);
    }

    public void PlayShakeSound()
    {
        _audioSource.PlayOneShot(_shakeSound);
    }

    public void PlayChessMove()
    {
        _audioSource.PlayOneShot(_moveSound);
    }
}
