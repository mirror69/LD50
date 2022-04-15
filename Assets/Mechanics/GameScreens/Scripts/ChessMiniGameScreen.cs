using UnityEngine;

public class ChessMiniGameScreen : GameScreen
{
    [SerializeField]
    private ChessController ChessMiniGamePrefab;

    private ChessController _chessMiniGameObject;

    public override void Show()
    {
        base.Show();
        if (_chessMiniGameObject == null)
        {
            _chessMiniGameObject = Instantiate(ChessMiniGamePrefab, transform);
            _chessMiniGameObject.OnFinish += ChessMiniGameScreen_OnFinish;
            _chessMiniGameObject.SetEnabledMinigameMusicRequested += RequestSetEnabledMinigameMusic;
        }
    }

    public override void Close()
    {
        base.Close();
    }

    private void ChessMiniGameScreen_OnFinish()
    {
        Debug.Log("Chess is ended");
        wasAlreadyChoosen = true;
        RequestClose(GameScreenResult.WinGame);
    }
}
