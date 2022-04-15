using SimpleMan.CoroutineExtensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicPlayerMiniGame : MonoBehaviour
{
    public event Action OnMusicPlayerMiniGameEnded;
    public event Action<bool> SetEnabledMinigameMusicRequested;

    public AudioClip musicMemoryRecords;
    public float TimeToFadeOutScreen;
    public AudioSource audio;
    public RadioController radioController; 
    public Canvas miniGameCanvas;

    public void SetCamera (Camera camera)
    {
        miniGameCanvas.worldCamera = camera;
    }

    private void OnEnable()
    {
        radioController.RadioIsFounded += RadioController_RadioIsFounded;
        radioController.SetEnabledMinigameMusicRequested += RadioController_SetEnabledMinigameMusicRequested;
    }

    private void OnDisable()
    {
        radioController.RadioIsFounded -= RadioController_RadioIsFounded;
        radioController.SetEnabledMinigameMusicRequested -= RadioController_SetEnabledMinigameMusicRequested;
    }

    private void RadioController_RadioIsFounded()
    {
        audio.PlayOneShot(musicMemoryRecords);

        this.Delay(TimeToFadeOutScreen, () => OnMusicPlayerMiniGameEnded?.Invoke());
    }

    private void RadioController_SetEnabledMinigameMusicRequested(bool enabled)
    {
        SetEnabledMinigameMusicRequested?.Invoke(enabled);
    }
}
