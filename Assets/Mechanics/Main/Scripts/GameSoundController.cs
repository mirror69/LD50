using System;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Контроллер звукового сопровождения игры
/// </summary>
public class GameSoundController : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer = null;

    private UIEventMediator _uiEventMediator;

    public void Init(UIEventMediator uiEventMediator)
    {
        _uiEventMediator = uiEventMediator;
        _uiEventMediator.ApplyToAudioMixerRequested += ApplyToAudioMixer;

        StoredGameDataManager.SoundOptions.ApplyAllToAudioMixer(audioMixer);
    }

    public void ApplyToAudioMixer(string paramName)
    {
        SoundOptions soundOptions = StoredGameDataManager.SoundOptions;
        soundOptions.ApplyToAudioMixer(audioMixer, paramName);
    }
}
