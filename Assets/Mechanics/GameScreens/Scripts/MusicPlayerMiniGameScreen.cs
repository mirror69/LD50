using UnityEngine;

public class MusicPlayerMiniGameScreen : GameScreen
{
    [SerializeField]
    private Camera cameraToMiniGame;

    [SerializeField]
    private GameObject MusicPlayerMiniGamePrefab;

    private GameObject _musicPlayerMiniGameObject;

    public override void Show()
    {
        base.Show();
        if (_musicPlayerMiniGameObject == null)
        {
            _musicPlayerMiniGameObject = Instantiate(MusicPlayerMiniGamePrefab, transform);
            _musicPlayerMiniGameObject.GetComponent<MusicPlayerMiniGame>().SetCamera(cameraToMiniGame);
            _musicPlayerMiniGameObject.GetComponent<MusicPlayerMiniGame>().OnMusicPlayerMiniGameEnded += MusicPlayerMiniGameScreen_OnMusicPlayerMiniGameEnded;
        }
    }

    private void MusicPlayerMiniGameScreen_OnMusicPlayerMiniGameEnded()
    {
        wasAlreadyChoosen = true;
        Debug.Log("MusicPlayerQuest is ended");
        CloseRequested?.Invoke(GameScreenResult.WinGame);
    }

    public override void Close()
    {
        base.Close();
    }
}
