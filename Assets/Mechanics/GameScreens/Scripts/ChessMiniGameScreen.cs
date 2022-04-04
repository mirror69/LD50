using UnityEngine;

public class ChessMiniGameScreen : GameScreen
{
    [SerializeField]
    private GameObject ChessMiniGamePrefab;

    private GameObject _chessMiniGameObject;

    public override void Show()
    {
        base.Show();
        if (_chessMiniGameObject == null)
        {
            _chessMiniGameObject = Instantiate(ChessMiniGamePrefab, transform);
            _chessMiniGameObject.GetComponent<ChessController>().OnFinish += ChessMiniGameScreen_OnFinish;
        }
    }

    private void ChessMiniGameScreen_OnFinish()
    {
        Debug.Log("Chess is ended");
        CloseRequested?.Invoke(GameScreenResult.WinGame);
    }

    public override void Close()
    {
        base.Close();
    }
}
