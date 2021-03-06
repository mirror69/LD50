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
    private InteractableItemsController InteractableItemsController;
    [SerializeField]
    private UIScreenController UIScreenController;
    [SerializeField]
    private CameraController CameraController;
    [SerializeField]
    private GameSoundController SoundController;
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

    [Header("CutScenes")]
    [SerializeField] private PlayableDirector finalTimeLine;
    [SerializeField] private GameObject startTimeLine;

    private bool _goToChairToWatchTV;

    private void Start()
    {
        _uiEventMediator = new UIEventMediator();
        SoundController.Init(_uiEventMediator);
        UIScreenController.Init(GameSettings, _uiEventMediator);

        UIScreenController.ShowBlackScreen();
        Initializer.InitializationCompleted += StartGame;
        Initializer.Instance.Init();
    }

    private void OnDisable()
    {
        Initializer.InitializationCompleted -= StartGame;
    }

    private void StartGame()
    {
        Initializer.InitializationCompleted -= StartGame;

        _uiEventMediator.PauseGameRequested += PauseGame;
        _uiEventMediator.ResumeGameRequested += ResumeGame;
        _uiEventMediator.QuitRequested += Quit;
        _uiEventMediator.StartNewGameRequested += StartNewGame;
        _uiEventMediator.MainMenuRequested += LoadMainMenu;
        _uiEventMediator.SetEnabledMinigameMusicRequested += SetEnabledMinigameMusic;

        GameScreenController.Init(_uiEventMediator);
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

        startTimeLine.SetActive(true);

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
        UIScreenController.ShowPauseScreen();
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

    private void SetEnabledMinigameMusic(bool enabled)
    {
        if (enabled)
        {
            MusicChanger.SetMinigameMode();
        }
        else
        {
            MusicChanger.SetMutedMode();
        }
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
        StartCoroutine(ProcessWinActions(delay));
    }

    private IEnumerator ProcessWinActions(int delay)
    {
        TimeController.StopTime();
        InteractableItemsController.SetEnabledSelection(false);
        InteractableItemsController.ClearSelection();
        GameScreenController.MainGameScreen.DestinationPointClicked -= OnDestinationPointClicked;
        KeyPressController.SetNotListeningMode();

        yield return new WaitForSeconds(delay);

        StopCurrentInteraction();
        TimeController.StopTime();

        while (_currentTimeline != null && _currentTimeline.state == PlayState.Playing)
        {
            yield return null;
        }

        Player.ProcessWin();
        MusicChanger.SetWinMode(GameSettings.SoundSettings.MainMusicFadeOutTimeAfterWin);
        CameraController.FollowRightEdge(GameSettings.CameraSettings.GoodEndingCameraMoveSpeed);
        CameraController.ZoomCamera(GameSettings.CameraSettings.GoodEndingCameraZoomTime);

        finalTimeLine.Play();
        finalTimeLine.stopped += (d) => LoadMainMenu();
    }


    private IEnumerator ProcessLoseActions()
    {
        TimeController.StopTime();

        InteractableItemsController.SetEnabledSelection(false);
        InteractableItemsController.ClearSelection();
        GameScreenController.MainGameScreen.DestinationPointClicked -= OnDestinationPointClicked;
        KeyPressController.SetNotListeningMode();

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

        yield return new WaitForSeconds(GameSettings.UISettings.GameOverShowDelay);

        UIScreenController.ShowGameOverScreen();
        DeathTextsController.StartDeathTextMethod();

        MusicChanger.SetMutedMode();

        yield return new WaitForSeconds(GameSettings.SoundSettings.LoseMusicDelayAfterBlacking);
        MusicChanger.PlayLoseMusic();
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
            GameScreenController.MainGameScreen.DestinationPointClicked -= OnDestinationPointClicked;
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
        MusicChanger.SetMinigameMode();
        GameScreenController.ShowItemScreen(item.Type);
        GameScreenController.CurrentScreen.CloseRequested += OnGameScreenCloseRequested;
    }

    private void OnWinTimeReached()
    {
        if (IsWinReached())
        {
            ProcessWinAfterDelay(0);
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
                if (_gameData.CurrentInteractingItem.Type == ItemType.TV)
                {
                    // Forbid to interact the item which is using now,
                    // but allow to turn off TV
                    StopCurrentInteraction();
                    _gameData.SetCurrentInteraction(ChairItem);
                    QuestStarter.Disable();
                }
                else if (_gameData.CurrentInteractingItem.Type == ItemType.Lamp)
                {
                    // Allow to turn on/off lamp if we are already interacting whith it
                    ProcessItemInteraction(_gameData.CurrentInteractingItem);
                }
                return;
            }
            else if (destinationPoint.item.Type == ItemType.TV)
            {
                // If we sit on chair, then turn on TV immediately.
                // Otherwise, we need to go to the chair first
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
                // Turn off TV if we clicked on chair
                StopCurrentInteraction();
                QuestStarter.Disable();
                return;
            }
        }

        StopCurrentInteraction();

        if (!destinationPoint.IsEqual(_gameData.LastReachedDestinationPoint))
        {
            // If we sit on chair, then we need to stand up before to go to another point
            if (_gameData.LastReachedDestinationPoint != null && _gameData.LastReachedDestinationPoint.item != null
                && _gameData.LastReachedDestinationPoint.item.Type == ItemType.Chair)
            {
                _gameData.SetCurrentInteraction(ChairItem);
                StopCurrentInteraction();
            }        
            StartCoroutine(StartMoveToPoint(destinationPoint));
        }
    }

    private IEnumerator StartMoveToPoint(DestinationPoint destinationPoint)
    {
        _gameData.SetLastReachedDestinationPoint(null);

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
        GameScreenController.BlackScreen.Activate(() => OnMinigameEnded(gameScreenResult));      
    }

    private void OnMinigameEnded(GameScreenResult gameScreenResult)
    {
        GameScreenController.CloseCurrentScreen();
        GameScreenController.MainGameScreen.DestinationPointClicked += OnDestinationPointClicked;
        if (gameScreenResult == GameScreenResult.WinGame)
        {
            _gameData.CurrentInteractingItem.SetAvailableToInteract(false);
            StopCurrentInteraction();
        }
        else
        {
            StopCurrentInteraction(ItemTimerType.BadItem);
        }

        MusicChanger.SetMainGameMode();
    }

    private void RefreshDebugView()
    {
        _debugView.SetInteractionsCount(_gameData);
    }
}
