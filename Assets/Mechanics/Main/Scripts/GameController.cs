using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameSettings GameSettings;
    [SerializeField]
    private TimeController TimeController;
    [SerializeField]
    private GameScreenController GameScreenController;
    [SerializeField]
    private UIScreenController UIScreenController;
    [SerializeField]
    private KeyPressController KeyPressController;
    [SerializeField]
    private QuestStarter QuestStarter;
    [SerializeField]
    public PlayerInput Player;

    [Space]
    [Header("Debug")]
    [SerializeField]
    private DebugView _debugView;

    private GameData _gameData;
    private UIEventMediator _uiEventMediator;

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        _uiEventMediator = new UIEventMediator();
        _uiEventMediator.PauseGameRequested += PauseGame;
        _uiEventMediator.ResumeGameRequested += ResumeGame;
        _uiEventMediator.QuitRequested += Quit;
        _uiEventMediator.StartNewGameRequested += StartNewGame;
        _uiEventMediator.MainMenuRequested += LoadMainMenu;

        UIScreenController.Init(_uiEventMediator);

        _gameData = new GameData();
        _gameData.Init(GameSettings);

        Player.DestinationPointReached += OnPlayerReachedDestinationPoint;
        GameScreenController.MainGameScreen.DestinationPointClicked += OnDestinationPointClicked;

        TimeController.DeathTimeOver += OnDeathTimeOver;
        TimeController.WinTimeReached += OnWinTimeReached;

        TimeController.Init(GameSettings, _gameData, _debugView);
        TimeController.StartTime();

        KeyPressController.Init(_uiEventMediator, UIScreenController);

        ResumeGame();

        RefreshDebugView();
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        KeyPressController.SetMenuMode();
        UIScreenController.ShowMainMenuScreen();
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
        KeyPressController.SetGameplayMode();
        UIScreenController.HideCurrentScreen();
    }

    private void Quit()
    {
        Application.Quit();
    }

    private void StartNewGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(GameSettings.SceneSettings.GameSceneName);
    }

    private void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(GameSettings.SceneSettings.MainMenuSceneName);
    }

    private bool IsWinReached()
    {
        return _gameData.GoodItemCount >= GameSettings.TimeSettings.GoodItemCountToWin
            && TimeController.IsWinTimeReached();
    }

    private void StopCurrentInteraction()
    {
        if (_gameData.CurrentInteractingItem == null)
        {
            return;
        }

        StopCurrentInteraction(_gameData.CurrentInteractingItem.TimerType);
    }

    private void StopCurrentInteraction(ItemTimerType timerType)
    {
        switch (timerType)
        {
            case ItemTimerType.BadItem:
                TimeController.StopBadInteraction();
                _gameData.AddBadItemInteraction();
                break;
            case ItemTimerType.GoodItem:
                TimeController.StopGoodInteraction();
                _gameData.AddGoodItemInteraction();

                if (IsWinReached())
                {
                    ProcessWinAfterDelay(GameSettings.TimeSettings.WinDelayAfterLastItemUse);
                }

                break;
            default:
                break;
        }
        _gameData.ResetCurrentInteraction();
        QuestStarter.Disable();

        RefreshDebugView();
    }

    private void ProcessWinAfterDelay(int delay)
    {
        KeyPressController.SetNotListeningMode();
        TimeController.StopTime();
        Invoke(nameof(ProcessWinActions), delay);
    }

    private void ProcessWinActions()
    {
        KeyPressController.SetNotListeningMode();
        TimeController.StopTime();
        _debugView.ShowWinScreen();
        Invoke(nameof(ShowCredits), 2);
    }

    private void ProcessLoseActions()
    {
        KeyPressController.SetNotListeningMode();
        TimeController.StopTime();
        _debugView.ShowLoseScreen();
        UIScreenController.ShowGameOverScreen();
    }

    private void ProcessItemInteraction(InteractableItem item)
    {
        _gameData.SetCurrentInteraction(item);

        if (item.TimerType == ItemTimerType.BadItem)
        {
            TimeController.StartBadInteraction();
        }
        else
        {
            TimeController.StartGoodInteraction();
            GameScreenController.BlackScreen.Activate(() => StartMiniGame(item));
        }
    }

    private void ShowCredits()
    {
        UIScreenController.ShowCreditsScreen();
    }

    private void StartMiniGame(InteractableItem item)
    {
        GameScreenController.ShowItemScreen(item.Type);
        GameScreenController.CurrentScreen.CloseRequested += OnGameScreenCloseRequested;
    }

    private void OnWinTimeReached()
    {
        if (IsWinReached())
        {
            ProcessWinActions();
        }
    }

    private void OnDeathTimeOver()
    {
        ProcessLoseActions();
    }

    private void OnDestinationPointClicked(DestinationPoint destinationPoint)
    {
        StopCurrentInteraction();
        Player.SetNewTargetPosition(destinationPoint);

        if (destinationPoint.item != null)
        {
            TimeController.PauseTime();
            if (destinationPoint.item.TimerType == ItemTimerType.BadItem)
            {
                QuestStarter.Enable(destinationPoint);
            }
        }
    }

    private void OnPlayerReachedDestinationPoint(DestinationPoint destinationPoint)
    {
        if (destinationPoint.item != null)
        {
            TimeController.ResumeTime();
            destinationPoint.item.ResetDraw();
            ProcessItemInteraction(destinationPoint.item);
        }
    }

    private void OnGameScreenCloseRequested(GameScreenResult gameScreenResult)
    {
        GameScreenController.CurrentScreen.CloseRequested -= OnGameScreenCloseRequested;
        GameScreenController.CloseCurrentScreen();

        if (gameScreenResult == GameScreenResult.WinGame)
        {
            StopCurrentInteraction();
        }
        else
        {
            StopCurrentInteraction(ItemTimerType.BadItem);
        }
    }

    private void RefreshDebugView()
    {
        _debugView.SetInteractionsCount(_gameData);
    }

    //private IEnumerator Restart()
    //{
    //    // Начинаем загрузку сцены
    //    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(GameSettings.SceneSettings.GameSceneName);

    //    // Ждём, пока сцена полностью загрузится
    //    while (!asyncLoad.isDone)
    //    {
    //        // Прерываемся, раз ещё не загружено
    //        yield return null;
    //    }
    //    // Выгрузить единственную открытую сцену нельзя
    //    // Сперва загружаем, а потом выгружаем
    //    SceneManager.UnloadSceneAsync("game");
    //}
}
