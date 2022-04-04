using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Контроллер звукового сопровождения игры
/// </summary>
public class GameSoundController : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer = null;

    [SerializeField]
    private AudioSource[] gameplayBackgroundSounds = null;

    [SerializeField]
    private AudioSource gamePlayMusic = null;

    public void ApplyToAudioMixer(string paramName)
    {
        SoundOptions soundOptions = StoredGameDataManager.SoundOptions;
        soundOptions.ApplyToAudioMixer(audioMixer, paramName);
    }

    public void PlayBackgroundSound()
    {
        foreach (var item in gameplayBackgroundSounds)
        {
            SoundFunc.Play(item);
        }       
    }

    public void StopBackgroundSound()
    {
        foreach (var item in gameplayBackgroundSounds)
        {
            SoundFunc.Stop(item);
        }
    }

    public void PlayGamePlayMusic() 
    {
        SoundFunc.Play(gamePlayMusic);
    }

    public void StopGamePlayMusic() 
    {
        SoundFunc.Stop(gamePlayMusic);
    }

    public void Init()
    {
        StoredGameDataManager.SoundOptions.ApplyAllToAudioMixer(audioMixer);
    }
}
