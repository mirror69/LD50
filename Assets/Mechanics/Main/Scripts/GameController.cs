using System;
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
    private CameraController CameraController;
    [SerializeField]
    private KeyPressController KeyPressController;
    [SerializeField]
    private QuestStarter QuestStarter;
    [SerializeField]
    public PlayerInput Player;
    [SerializeField]
    private DeathTextsController DeathTextsController;
    [SerializeField]
    private MainMusicChanger MusicChanger;

    [Space]
    [Header("ChairAndTV")]
    [SerializeField]
    private InteractableItem ChairItem;
    [SerializeField]
    private InteractableItem TVItem;

    [Space]
    [Header("Debug")]
    [SerializeField]
    private DebugView _debugView;

    private GameData _gameData;
    private UIEventMediator _uiEventMediator;

    private PlayableDirector _currentTimeline;

    [SerializeField] private PlayableDirector finalTimeLine;

    private bool _goToChairToWatchTV;
    
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
        QuestStarter.Init(CameraController, GameSettings);

        _gameData = new GameData();
        _gameData.Init(GameSettings);

        Player.DestinationPointReached += OnPlayerReachedDestinationPoint;
        GameScreenController.MainGameScreen.DestinationPointClicked += OnDestinationPointClicked;

        TimeController.DeathTimeOver += OnDeathTimeOver;
        TimeController.WinTimeReached += OnWinTimeReached;

        TimeController.Init(GameSettings, _gameData, _debugView);
        TimeController.StartTime();

        DeathTextsController.DeathCutsceneEnded += OnDeathCutsceneEnded;

        KeyPressController.Init(_uiEventMediator, UIScreenController);

        ResumeGame();

        RefreshDebugView();
    }

    private void OnDeathCutsceneEnded()
    {
        //KeyPressController.SetNotListeningMode();
        //UIScreenController.ShowGameOverScreen();
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

        Player.TryApplyAnimParams(item.OutAnimatorParamSet);

        StopCurrentInteraction(item.TimerType);

        TryPlayTimeline(item.OutTimeline);
        _currentTimeline = item.OutTimeline;

        _gameData.SetCurrentInteraction(null);
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
        _gameData.SetCurrentInteraction(null);
        QuestStarter.Disable();

        RefreshDebugView();
    }

    private void ProcessWinAfterDelay(int delay)
    {
        KeyPressController.SetNotListeningMode();
        GameScreenController.MainGameScreen.DestinationPointClicked -= OnDestinationPointClicked;
        TimeController.StopTime();
        Invoke(nameof(ProcessWinActions), delay);
    }

    private void ProcessWinActions()
    {
        StopCurrentInteraction();
        TimeController.StopTime();

        GameScreenController.MainGameScreen.DestinationPointClicked -= OnDestinationPointClicked;
        KeyPressController.SetNotListeningMode();
        
        Player.ProcessWin();
        MusicChanger.SetWinModeOn(GameSettings.SoundSettings.MainMusicFadeOutTimeAfterWin);
        CameraController.SetWinModeOn();
        CameraController.ZoomCamera(GameSettings.CameraSettings.GoodEndingCameraZoomTime);

        finalTimeLine.Play();
        finalTimeLine.stopped += (d) => LoadMainMenu();
    }


    private IEnumerator ProcessLoseActions()
    {
        TimeController.StopTime();
        GameScreenController.MainGameScreen.DestinationPointClicked -= OnDestinationPointClicked;
        if (_gameData.CurrentInteractingItem != ChairItem && _gameData.CurrentInteractingItem != TVItem)
        {
            StopCurrentInteraction();
            TimeController.StopTime();
            while (_currentTimeline != null && _currentTimeline.state == PlayState.Playing)
            {
                yield return null;
            }
        }

        Player.ProcessDeath();
        KeyPressController.SetNotListeningMode();
        UIScreenController.ShowGameOverScreen();
        DeathTextsController.StartDeathTextMethod();
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
        MusicChanger.SetMinigameModeOn();
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
        StartCoroutine(ProcessLoseActions());
    }

    private void OnDestinationPointClicked(DestinationPoint destinationPoint)
    {
        _goToChairToWatchTV = false;

        if (destinationPoint.item != null)
        {
            if (_gameData.CurrentInteractingItem == destinationPoint.item)
            {
                // forbid to interact the item which is using now,
                // but allow to turn off TV
                if (_gameData.CurrentInteractingItem.Type == ItemType.TV)
                {
                    StopCurrentInteraction();
                    _gameData.SetCurrentInteraction(ChairItem);
                    QuestStarter.Disable();
                }
                return;
            }
            else if (destinationPoint.item.Type == ItemType.TV)
            {
                if (_gameData.LastReachedDestinationPoint != null && _gameData.LastReachedDestinationPoint.item != null 
                    && _gameData.LastReachedDestinationPoint.item.Type == ItemType.Chair)
                {
                    TimeController.ResumeTime();
                    destinationPoint.item.ResetDraw();
                    ProcessItemInteraction(destinationPoint.item);
                    QuestStarter.Enable(destinationPoint.point);
                    return;
                }
                else
                {
                    _goToChairToWatchTV = true;
                    destinationPoint = new DestinationPoint(ChairItem.StayPoint.position, ChairItem);
                }
            }
            else if (_gameData.CurrentInteractingItem != null && _gameData.CurrentInteractingItem.Type == ItemType.TV 
                && destinationPoint.item.Type == ItemType.Chair)
            {
                StopCurrentInteraction();
                QuestStarter.Disable();
                return;
            }
        }

        StopCurrentInteraction();

        if (!destinationPoint.IsEqual(_gameData.LastReachedDestinationPoint))
        {
            if (_gameData.LastReachedDestinationPoint != null && _gameData.LastReachedDestinationPoint.item != null)
            {
                _gameData.SetCurrentInteraction(_gameData.LastReachedDestinationPoint.item);
                StopCurrentInteraction();
            }
            _gameData.SetLastReachedDestinationPoint(null);
            StartCoroutine(StartMoveToPoint(destinationPoint));
        }
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

        if (isBadInteraction || _goToChairToWatchTV)
        {
            QuestStarter.Enable(destinationPoint.point);
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

        _gameData.SetLastReachedDestinationPoint(destinationPoint);
        if (destinationPoint.item != null)
        {
            Player.TryApplyAnimParams(destinationPoint.item.InAnimatorParamSet);

            TimeController.ResumeTime();
            destinationPoint.item.ResetDraw();

            if (_goToChairToWatchTV)
            {
                ProcessItemInteraction(ChairItem);
            }
            else
            {
                ProcessItemInteraction(destinationPoint.item);
            }

        }

        if (_goToChairToWatchTV)
        {
            ProcessItemInteraction(TVItem);
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

        MusicChanger.SetMinigameModeOff();
    }

    private void RefreshDebugView()
    {
        _debugView.SetInteractionsCount(_gameData);
    }
}
