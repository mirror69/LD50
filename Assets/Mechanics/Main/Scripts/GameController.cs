using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    public GameSettings GameSettings;
    [SerializeField]
    public TimeService TimeService;
    [SerializeField]
    public GameScreenController GameScreenController;
    [SerializeField]
    public PlayerInput Player;

    private TimeState _timeState;
    private TimeLogic _timeLogic;

    private GameData _gameData;

    private ActionTimer _deathTimer;
    //private ActionTimer _badItemTimer;

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        _gameData = new GameData();
        _timeState = new TimeState();
        _timeLogic = new TimeLogic();
        _deathTimer = new ActionTimer(_timeLogic, _timeState, OnTimeToDeathIsOver);

        _gameData.Init(GameSettings);
        _timeLogic.Init(GameSettings.TimeSettings, _timeState, TimeService);
        _timeLogic.Start();

        _deathTimer.StartTimer(GameSettings.TimeSettings.SecondsToDeath);

        Player.DestinationPointReached += OnPlayerReachedDestinationPoint;
        GameScreenController.MainGameScreen.DestinationPointClicked += OnDestinationPointClicked;
        //_badItemTimer = new ActionTimer(_timeLogic, _timeState, OnTimeToDeathIsOver);
    }

    private void OnDestinationPointClicked(DestinationPoint destinationPoint)
    {
        Player.SetNewTargetPosition(destinationPoint);
    }

    private void OnTimeToDeathIsOver()
    {
        Debug.Log("Time to death is over. You lose.");
        _timeLogic.Stop();
    }

    private void OnPlayerReachedDestinationPoint(DestinationPoint destinationPoint)
    {
        if (destinationPoint.item != null)
        {
            destinationPoint.item.ResetDraw();
            _gameData.SetCurrentInteraction(destinationPoint.item);
            ProcessItemAction(destinationPoint.item.Type);
            ProcessTimerAction(destinationPoint.item.TimerType);
        }
    }

    private void ProcessTimerAction(ItemTimerType timerType)
    {
        //_timeLogic.Stop();
        _deathTimer.StopTimer();
        switch (timerType)
        {
            case ItemTimerType.SpeedUpDeath:
                _gameData.AddBadObjectInteraction();
                _deathTimer.StartTimer(_gameData.CurrentTimeOfBadItemUse);
                Debug.Log($"Time left: {_gameData.CurrentTimeOfBadItemUse}");
                break;
            case ItemTimerType.SlowDownDeath:
                _timeLogic.Stop();
                break;
            default:
                break;
        }
    }

    private void ProcessItemAction(ItemType itemType)
    {
        GameScreenController.ShowItemScreen(itemType);
        GameScreenController.CurrentScreen.CloseRequested += OnGameScreenCloseRequested;
        switch (itemType)
        {
            case ItemType.Table:
                Debug.Log("Table action processed!");
                break;
            default:
                break;
        }
    }

    private void OnGameScreenCloseRequested()
    {
        GameScreenController.CurrentScreen.CloseRequested -= OnGameScreenCloseRequested;
        GameScreenController.CloseCurrentScreen();

        int newTime = 0;
        if (_gameData.CurrentInteractingItem != null)
        {
            switch (_gameData.CurrentInteractingItem.TimerType)
            {
                case ItemTimerType.SpeedUpDeath:
                    if (_deathTimer.SecondsLeft >= _gameData.CurrentTimeOfBadItemUse / 2)
                    {
                        newTime = GameSettings.TimeSettings.TimeAfterShortBadItemUse;
                    }
                    else
                    {
                        newTime = GameSettings.TimeSettings.TimeAfterLongBadItemUse;
                    }
                    break;
                case ItemTimerType.SlowDownDeath:
                    newTime = GameSettings.TimeSettings.MinTimeAfterGoodItemUse;
                    if (_deathTimer.SecondsLeft >= GameSettings.TimeSettings.MinTimeAfterGoodItemUse)
                    {
                        newTime += _deathTimer.SecondsLeft;
                    }
                    break;
                default:
                    break;
            }
        }
        Debug.Log($"New time: {newTime}");

        _timeLogic.Start();
        _deathTimer.StartTimer(newTime);
    }
}
