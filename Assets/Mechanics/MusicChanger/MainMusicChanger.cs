using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MainMusicChanger : MonoBehaviour
{
    public enum MusicThemes
    {
        GoodMusic,
        BadMusic
    }

    public enum MusicMode
    {
        Muted = 0,
        MainGame = 1,
        Minigame = 2,
        Win = 3,
        Lose = 4
    }

    [SerializeField]
    private ClickHandler clickHandler;

    [SerializeField]
    private float minVolumeValue = 0f;
    [SerializeField]
    private float maxVolumeValue = 1f;

    [SerializeField]
    private float[] timeCodesToSwitchMusic;

    [SerializeField]
    private AudioSource goodMusicAudioSource;
    [SerializeField]
    private AudioSource badMusicAudioSource;
    [SerializeField]
    private AudioSource minigameMusicAudioSource;
    [SerializeField]
    private AudioSource loseMusicAudioSource;

    private float maxTimeAudio;
    private float currentTime = 0;
    private int timeLineIndex=0;

    private bool needToChangeMusic;

    private MusicThemes currentMusicTheme;
    private MusicThemes nextMusicTheme;

    private float goodMusicTargetVolume;
    private float badMusicTargetVolume;
    private float minigameMusicTargetVolume;

    private float maxMinigameVolumeValue;

    private MusicMode _currentMode;

    private void OnEnable()
    {
        clickHandler.DestinationPointClicked += ClickHandler_DestinationPointClicked;
    }

    private void OnDisable()
    {
        clickHandler.DestinationPointClicked -= ClickHandler_DestinationPointClicked;
    }

    private void ClickHandler_DestinationPointClicked(DestinationPoint obj)
    {
        if (obj.item == null || obj.item.TimerType == ItemTimerType.GoodItem || obj.item.TimerType == ItemTimerType.NeutralItem)
            nextMusicTheme = MusicThemes.GoodMusic;
        else
            nextMusicTheme = MusicThemes.BadMusic;

        needToChangeMusic = nextMusicTheme != currentMusicTheme;
    }

    private void Start()
    {
        _currentMode = MusicMode.MainGame;

        maxMinigameVolumeValue = minigameMusicAudioSource.volume;
        minigameMusicTargetVolume = 0;
        minigameMusicAudioSource.volume = minVolumeValue;

        goodMusicAudioSource.volume = maxVolumeValue;
        badMusicAudioSource.volume = minVolumeValue;

        goodMusicTargetVolume = maxVolumeValue;
        badMusicTargetVolume = minVolumeValue;

        maxTimeAudio = goodMusicAudioSource.clip.length;

        Invoke(nameof(PlayMusic), 1f);
    }

    private void Update()
    {
        if (_currentMode == MusicMode.Win)
        {
            return;
        }
        goodMusicAudioSource.volume = Mathf.Lerp(goodMusicAudioSource.volume, goodMusicTargetVolume, Time.deltaTime * 3);
        badMusicAudioSource.volume = Mathf.Lerp(badMusicAudioSource.volume, badMusicTargetVolume, Time.deltaTime * 3);
        minigameMusicAudioSource.volume = Mathf.Lerp(minigameMusicAudioSource.volume, minigameMusicTargetVolume, Time.deltaTime * 3);

        currentTime += Time.deltaTime;
        if (currentTime >= maxTimeAudio)
            currentTime = 0;

        if (currentTime >= timeCodesToSwitchMusic[timeLineIndex] && 
            (timeLineIndex != 0 || currentTime < timeCodesToSwitchMusic[^1]))
        {
            if (_currentMode == MusicMode.MainGame && needToChangeMusic)
            {
                ChangeMusicTheme(nextMusicTheme);
            }
            timeLineIndex++;
            if (timeLineIndex >= timeCodesToSwitchMusic.Length)
            {
                timeLineIndex = 0;
            }
        }
    }

    public void SetMinigameMode()
    {
        _currentMode = MusicMode.Minigame;
        goodMusicTargetVolume = minVolumeValue;
        badMusicTargetVolume = minVolumeValue;
        minigameMusicTargetVolume = maxMinigameVolumeValue;
    }

    public void SetMainGameMode()
    {
        _currentMode = MusicMode.MainGame;
        goodMusicTargetVolume = maxVolumeValue;
        badMusicTargetVolume = minVolumeValue;
        minigameMusicTargetVolume = minVolumeValue;

        needToChangeMusic = false;
    }

    public void SetWinMode(float fadeOutDuration)
    {
        _currentMode = MusicMode.Win;
        clickHandler.DestinationPointClicked -= ClickHandler_DestinationPointClicked;

        StartCoroutine(ProcessFadeOut(goodMusicAudioSource, fadeOutDuration));
        StartCoroutine(ProcessFadeOut(badMusicAudioSource, fadeOutDuration));
        minigameMusicAudioSource.volume = minVolumeValue;
    }

    public void PlayLoseMusic()
    {
        loseMusicAudioSource.Play();
    }

    public void SetMutedMode()
    {
        _currentMode = MusicMode.Muted;

        goodMusicTargetVolume = minVolumeValue;
        badMusicTargetVolume = minVolumeValue;
        minigameMusicTargetVolume = minVolumeValue;
    }

    private void PlayMusic ()
    {
        goodMusicAudioSource.Play();
        badMusicAudioSource.Play();
        minigameMusicAudioSource.Play();
    }

    private void ChangeMusicTheme (MusicThemes newMusicTheme)
    {
        needToChangeMusic = false;

        if (currentMusicTheme == newMusicTheme)
            return;
        
        currentMusicTheme = newMusicTheme;

        if (currentMusicTheme == MusicThemes.GoodMusic)
        {
            goodMusicTargetVolume = maxVolumeValue;
            badMusicTargetVolume = minVolumeValue;
        }
        else
        {
            goodMusicTargetVolume = minVolumeValue;
            badMusicTargetVolume = maxVolumeValue;
        }
    }

    private IEnumerator ProcessFadeOut(AudioSource audioSource, float duration)
    {
        float speed = (maxVolumeValue - minVolumeValue) / duration;

        while (audioSource.volume > minVolumeValue)
        {
            audioSource.volume -= speed * Time.deltaTime;
            yield return null;
        }
        audioSource.volume = minVolumeValue;
    }
}
