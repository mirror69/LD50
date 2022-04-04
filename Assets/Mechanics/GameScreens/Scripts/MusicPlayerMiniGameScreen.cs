using UnityEngine;

public class MusicPlayerMiniGameScreen : GameScreen
{
    [SerializeField]
    private GameObject MusicPlayerMiniGamePrefab;

    private GameObject _musicPlayerMiniGameObject;

    public override void Show()
    {
        base.Show();
        if (_musicPlayerMiniGameObject == null)
        {
            _musicPlayerMiniGameObject = Instantiate(MusicPlayerMiniGamePrefab, transform);
           // _musicPlayerMiniGameObject.GetComponent<ChessController>().OnFinish += MusicPlayerMiniGameScreen_OnFinish;
        }
    }

    private void MusicPlayerMiniGameScreen_OnFinish()
    {
        Debug.Log("MusicPlayerQuest is ended");
        CloseRequested?.Invoke(GameScreenResult.WinGame);
    }

    public override void Close()
    {
        base.Close();
    }
}
