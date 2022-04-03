using System;
using UnityEngine;
using UnityEngine.UI;

public enum GameScreenResult
{
    None = 0,
    WinGame = 1,
    LoseGame = 2,
}

public abstract class GameScreen : MonoBehaviour
{
    [SerializeField]
    private Button CloseButton;

    [SerializeField]
    private Button CloseWinButton;

    public bool IsActive => gameObject.activeSelf;

    public Action<GameScreenResult> CloseRequested;

    private void OnEnable()
    {
        CloseButton?.onClick.AddListener(() => CloseRequested?.Invoke(GameScreenResult.LoseGame));
        CloseWinButton?.onClick.AddListener(() => CloseRequested?.Invoke(GameScreenResult.WinGame));
    }

    private void OnDisable()
    {
        CloseButton?.onClick.RemoveAllListeners();
        CloseWinButton?.onClick.RemoveAllListeners();
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }
    public virtual void Close()
    {
        gameObject.SetActive(false);
    }
}
