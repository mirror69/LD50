using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum MusicThemes
{
    GoodMusic,
    BadMusic
}

public class MainMusicChanger : MonoBehaviour
{
    [SerializeField]
    private ClickHandler clickHandler;

    [SerializeField]
    private float minVolumeValue = -80f;
    [SerializeField]
    private float maxVolumeValue = 0f;

    [SerializeField]
    private float[] timeCodesToSwitchMusic;

    [SerializeField]
    private AudioSource goodMusicAudioSource;
    [SerializeField]
    private AudioSource badMusicAudioSource;

    private float maxTimeAudio;
    private float currentTime = 0;
    private int timeLineIndex=0;

    private bool needToChangeMusic;

    private MusicThemes currentMusicTheme;
    private MusicThemes nextMusicTheme;

    private float goodMusicTargetVolume;
    private float badMusicTargetVolume;

    private bool _isMinigame;

    public void OnMinigameMode()
    {
        goodMusicAudioSource.volume = minVolumeValue;
        badMusicAudioSource.volume = minVolumeValue;
        enabled = false;
    }

    public void OffMinigameMode()
    {
        enabled = true;
    }

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
        goodMusicAudioSource.volume = maxVolumeValue;
        badMusicAudioSource.volume = minVolumeValue;

        goodMusicTargetVolume = maxVolumeValue;
        badMusicTargetVolume = minVolumeValue;

        maxTimeAudio = goodMusicAudioSource.clip.length;
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

    private void Update()
    {
        goodMusicAudioSource.volume = Mathf.Lerp(goodMusicAudioSource.volume, goodMusicTargetVolume, Time.deltaTime * 5);
        badMusicAudioSource.volume = Mathf.Lerp(badMusicAudioSource.volume, badMusicTargetVolume, Time.deltaTime * 5);

        currentTime += Time.deltaTime;
        if (currentTime >= maxTimeAudio)
            currentTime = 0;

        if (needToChangeMusic && currentTime >= timeCodesToSwitchMusic[timeLineIndex])
        {
            ChangeMusicTheme(nextMusicTheme);
            timeLineIndex++;
            if (timeLineIndex >= timeCodesToSwitchMusic.Length)
            {
                timeLineIndex = 0;
            }
        }
    }
}
