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
    private QuestStarter QuestStarter;
    [SerializeField]
    public PlayerInput Player;

    private TimeState _timeState;
    private TimeLogic _timeLogic;

    private GameData _gameData;

    private ActionTimer _deathTimer;

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
    }

    private void OnDestinationPointClicked(DestinationPoint destinationPoint)
    {
        StopCurrentInteraction();
        Player.SetNewTargetPosition(destinationPoint);

        if (destinationPoint.item != null && destinationPoint.item.TimerType == ItemTimerType.BadItem)
        {
            QuestStarter.Enable(destinationPoint);
        }
    }

    private void StopCurrentInteraction()
    {
        if (_gameData.CurrentInteractingItem == null)
        {
            return;
        }

        int newTime = 0;

        switch (_gameData.CurrentInteractingItem.TimerType)
        {
            case ItemTimerType.BadItem:
                _gameData.AddBadItemInteraction();
                if (_deathTimer.SecondsLeft >= _gameData.CurrentTimeOfBadItemUse / 2)
                {
                    newTime = GameSettings.TimeSettings.TimeAfterShortBadItemUse;
                }
                else
                {
                    newTime = GameSettings.TimeSettings.TimeAfterLongBadItemUse;
                }
                break;
            case ItemTimerType.GoodItem:
                _gameData.AddGoodItemInteraction();
                newTime = GameSettings.TimeSettings.MinTimeAfterGoodItemUse;
                if (_deathTimer.SecondsLeft >= GameSettings.TimeSettings.MinTimeAfterGoodItemUse)
                {
                    newTime += _deathTimer.SecondsLeft;
                }
                break;
            default:
                break;
        }

        Debug.Log($"New time: {newTime}");

        _timeLogic.Start();
        _deathTimer.StartTimer(newTime);

        _gameData.ResetCurrentInteraction();
        QuestStarter.Disable();
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
            ProcessItemInteraction(destinationPoint.item);
        }
    }

    private void ProcessItemInteraction(InteractableItem item)
    {
        _gameData.SetCurrentInteraction(item);

        if (item.TimerType == ItemTimerType.BadItem)
        {
            _deathTimer.StartTimer(_gameData.CurrentTimeOfBadItemUse);
            Debug.Log($"Bad Item Use. Time left: {_gameData.CurrentTimeOfBadItemUse}");
        }
        else
        {
            _deathTimer.StopTimer();
            _timeLogic.Stop();
            GameScreenController.ShowItemScreen(item.Type);
            GameScreenController.CurrentScreen.CloseRequested += OnGameScreenCloseRequested;
        }

    }

    private void OnGameScreenCloseRequested()
    {
        GameScreenController.CurrentScreen.CloseRequested -= OnGameScreenCloseRequested;
        GameScreenController.CloseCurrentScreen();
    }
}
