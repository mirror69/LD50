using System;
using UnityEngine;

public class MusicPlayerMiniGameScreen : GameScreen
{
    [SerializeField]
    private Camera cameraToMiniGame;

    [SerializeField]
    private MusicPlayerMiniGame MusicPlayerMiniGamePrefab;

    private MusicPlayerMiniGame _musicPlayerMiniGameObject;

    public override void Show()
    {
        base.Show();
        if (_musicPlayerMiniGameObject == null)
        {
            _musicPlayerMiniGameObject = Instantiate(MusicPlayerMiniGamePrefab, transform);
            _musicPlayerMiniGameObject.SetCamera(cameraToMiniGame);
            _musicPlayerMiniGameObject.OnMusicPlayerMiniGameEnded += MusicPlayerMiniGameScreen_OnMusicPlayerMiniGameEnded;
            _musicPlayerMiniGameObject.SetEnabledMinigameMusicRequested += RequestSetEnabledMinigameMusic;
        }
    }

    public override void Close()
    {
        base.Close();
    }

    private void MusicPlayerMiniGameScreen_OnMusicPlayerMiniGameEnded()
    {
        wasAlreadyChoosen = true;
        Debug.Log("MusicPlayerQuest is ended");
        RequestClose(GameScreenResult.WinGame);
    }
}
