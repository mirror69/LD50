using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
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
    [SerializeField]
    private DeathTextsController DeathTextsController;

    [Space]
    [Header("Debug")]
    [SerializeField]
    private DebugView _debugView;

    private GameData _gameData;
    private UIEventMediator _uiEventMediator;

    private PlayableDirector _currentTimeline;

    [SerializeField] private PlayableDirector finalTimeLine;

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
        TryStopTimeline(_currentTimeline);
        _currentTimeline = null;

        var item = _gameData.CurrentInteractingItem;
        if (item == null)
        {
            return;
        }

        Player.TryApplyAnimParams(item.OutAnimatorIntParams);

        StopCurrentInteraction(item.TimerType);

        TryPlayTimeline(item.OutTimeline);
        _currentTimeline = item.OutTimeline;
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
        finalTimeLine.Play();
        //_debugView.ShowWinScreen();
        //Invoke(nameof(ShowCredits), 2);
    }


    private IEnumerator ProcessLoseActions()
    {
        TimeController.StopTime();
        GameScreenController.MainGameScreen.DestinationPointClicked -= OnDestinationPointClicked;
        while (_currentTimeline != null && _currentTimeline.state == PlayState.Playing)
        {
            yield return null;
        }
        KeyPressController.SetNotListeningMode();
        DeathTextsController.StartDeathTextMethod();
        Player.SetAnimatorDead();
    }

    private void ProcessItemInteraction(InteractableItem item)
    {
        _gameData.SetCurrentInteraction(item);

        if (item.TimerType == ItemTimerType.BadItem)
        {
            TimeController.StartBadInteraction();
        }
        else if (item.TimerType == ItemTimerType.GoodItem)
        {
            TimeController.StartGoodInteraction();
            GameScreenController.BlackScreen.Activate(() => StartMiniGame(item));
        }

        TryPlayTimeline(item.InTimeline);
        _currentTimeline = item.InTimeline;
    }

    private void TryPlayTimeline(PlayableDirector timeline)
    {
        if (timeline == null)
        {
            return;
        }

        if (timeline.state == PlayState.Playing)
        {
            timeline.Stop();
        }
        timeline.Play();
    }

    private void TryStopTimeline(PlayableDirector timeline)
    {
        if (timeline == null)
        {
            return;
        }
        if (timeline.state == PlayState.Playing)
        {
            timeline.Stop();
        }
    }

    public void ShowCredits()
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
        StopCurrentInteraction();
        StartCoroutine(ProcessLoseActions());
    }

    private void OnDestinationPointClicked(DestinationPoint destinationPoint)
    {
        StopCurrentInteraction();
        StartCoroutine(StartMoveToPoint(destinationPoint));
    }

    private IEnumerator StartMoveToPoint(DestinationPoint destinationPoint)
    {
        GameScreenController.MainGameScreen.DestinationPointClicked -= OnDestinationPointClicked;
        while (_currentTimeline != null && _currentTimeline.state == PlayState.Playing)
        {
            yield return null;
        }
        GameScreenController.MainGameScreen.DestinationPointClicked += OnDestinationPointClicked;

        Player.SetNewTargetPosition(destinationPoint);

        bool isBadInteraction = false;
        if (destinationPoint.item != null)
        {
            TimeController.PauseTime();
            if (destinationPoint.item.TimerType == ItemTimerType.BadItem)
            {
                isBadInteraction = true;
            }
        }
        else
        {
            TimeController.ResumeTime();
        }

        if (isBadInteraction)
        {
            QuestStarter.Enable(destinationPoint);
        }
        else
        {
            QuestStarter.Disable();
        }
    }

    private void OnPlayerReachedDestinationPoint(DestinationPoint destinationPoint)
    {
        TryStopTimeline(_currentTimeline);
        _currentTimeline = null;

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


        GameScreenController.BlackScreen.Activate(() => GameScreenController.CloseCurrentScreen());
        

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
